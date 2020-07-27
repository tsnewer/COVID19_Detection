using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TUCAMERA;
using HANDLE = System.IntPtr;
using System.Windows.Forms.DataVisualization.Charting;

namespace COVID19_Detection
{
    public partial class MainForm : Form
    {


        public Timer timer;

        public TUCAM_INIT m_itApi;                        // 初始化SDK环境
        public TUCAM_OPEN m_opCam;                        // 打开相机参数

        public List<TUCAM_OPEN> m_opCamList;
        public int m_nCamIndex;

        public TUCAM_CAPA_ATTR attrCapa;
        public TUCAM_VALUE_TEXT valText;
        public TUCAM_VALUE_INFO valueinfo;
        public TUCAM_FRAME m_drawframe;                   // Drawing帧对象
        public TUCAM_DRAW m_drawing;
        public TUCAM_DRAW_INIT m_itDraw;                  // 绘制初始化参数
        public TUCAM_TRIGGER_ATTR attrTgr;

        public BITMAPINFOHEADER m_bmpInfo;                // 位图头部信息

        public HANDLE m_hDC;
        public HANDLE m_hDib;
        public HANDLE hDraw;                              //显示区域句柄
        public HANDLE hMainWin;                           //主窗口句柄

        public HANDLE m_hThdWaitForFrame;

        public bool m_bWaitting = false;                  // 捕获相机数据

        public uint m_dwFrmCnt = 0;                       // 帧数统计

        public int m_nTriMode = 0;

        public int m_nCurWidth = 0;                       // 当前宽度
        public int m_nCurHeight = 0;                      // 当前高度
        public int m_nCliWidth = 0;                       // 当前客户端宽度
        public int m_nCliHeight = 0;                      // 当前客户端高度
        public int m_nDrawOffX = 0;                       // 水平偏移  
        public int m_nDrawOffY = 0;                       // 垂直偏移
        public int m_nDrawWidth = 0;                      // 绘制宽度
        public int m_nDrawHeight = 0;                     // 绘制高度

        public float m_fScale = 0;                        // 缩放比例

        public int NumChartArea = 12;


        public TUCAM_ROI_ATTR roiAttr;



        private Queue<double> dataQueue = new Queue<double>(100);

        private int curValue = 0;

        private int num = 5;//每次删除增加几个点

        public double dbExp = 0;

        public int Pic_num = 10;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //IntPtr strPath = Marshal.StringToHGlobalAnsi(System.Environment.CurrentDirectory);
            m_opCamList = new List<TUCAM_OPEN>();

            m_itApi.uiCamCount = 0;
            //m_itApi.pstrConfigPath = strPath;

            TUCamera.TUCAM_Api_Init(ref m_itApi);

            if (0 == m_itApi.uiCamCount)
            {
                MessageBox.Show("Init Camera Error!");
                return;
            }

            for (int i = 0; i < m_itApi.uiCamCount; i++)
            {
                m_opCam.uiIdxOpen = (uint)i;
                TUCamera.TUCAM_Dev_Open(ref m_opCam);
                m_opCamList.Add(m_opCam);

                valueinfo.nValue = 1;
                valueinfo.nID = (int)TUCAM_IDINFO.TUIDI_CAMERA_CHANNELS;
                TUCamera.TUCAM_Dev_GetInfo(m_opCam.hIdxTUCam, ref valueinfo);

                if (1 < m_itApi.uiCamCount)
                    TUCamera.TUCAM_Capa_SetValue(m_opCamList[m_nCamIndex].hIdxTUCam, (int)TUCAM_IDCAPA.TUIDC_CAM_MULTIPLE, (int)m_itApi.uiCamCount);

                m_itDraw.hWnd = ShowpictureBox.Handle;
                m_itDraw.ucChannels = (sbyte)valueinfo.nValue;
                m_itDraw.nWidth = 2048;
                m_itDraw.nHeight = 2048;
                m_itDraw.nMode = 0;
                TUCamera.TUCAM_Draw_Init(m_opCam.hIdxTUCam, m_itDraw);
            }

            if (0 == (long)m_opCam.hIdxTUCam)
            {
                MessageBox.Show("Open Camera Faild!");
                return;
            }
            m_nCamIndex = 0;
            m_opCam = m_opCamList[m_nCamIndex];



            double dbGain = 0;

            // 获取增益值
            TUCamera.TUCAM_Prop_GetValue(m_opCam.hIdxTUCam, (int)TUCAM_IDPROP.TUIDP_GLOBALGAIN, ref dbGain, 0);

            // 获取曝光时间
            TUCamera.TUCAM_Prop_GetValue(m_opCam.hIdxTUCam, (int)TUCAM_IDPROP.TUIDP_EXPOSURETM, ref dbExp, 0);

            // 关闭自动曝光
            TUCamera.TUCAM_Capa_SetValue(m_opCam.hIdxTUCam, (int)TUCAM_IDCAPA.TUIDC_ATEXPOSURE, 0);
            // 关闭自动白平衡
            TUCamera.TUCAM_Capa_SetValue(m_opCam.hIdxTUCam, (int)TUCAM_IDCAPA.TUIDC_ATWBALANCE, 0);

            // 设置曝光时间
            TUCamera.TUCAM_Prop_SetValue(m_opCam.hIdxTUCam, (int)TUCAM_IDPROP.TUIDP_EXPOSURETM, dbExp, 0);

            // 设置增益值
            TUCamera.TUCAM_Prop_SetValue(m_opCam.hIdxTUCam, (int)TUCAM_IDPROP.TUIDP_GLOBALGAIN, dbGain, 0);

            valText.nTextSize = 64;
            string strp = "000000000000000000000000000";
            valText.pText = Marshal.StringToHGlobalAnsi(strp);

            // 获取分辨率范围
            attrCapa.idCapa = (int)TUCAM_IDCAPA.TUIDC_RESOLUTION;
            if (TUCAMRET.TUCAMRET_SUCCESS == TUCamera.TUCAM_Capa_GetAttr(/*m_opCam.hIdxTUCam*/m_opCam.hIdxTUCam, ref attrCapa))
            {
                int nCnt = attrCapa.nValMax - attrCapa.nValMin + 1;
                string[] szRes = new string[nCnt];

                valText.nID = (int)TUCAM_IDCAPA.TUIDC_RESOLUTION;
                for (int i = 0; i < nCnt; ++i)
                {
                    valText.dbValue = i;
                    TUCamera.TUCAM_Capa_GetValueText(/*m_opCam.hIdxTUCam*/m_opCam.hIdxTUCam, ref valText);
                    szRes[i] = Marshal.PtrToStringAnsi(valText.pText);
                }
            }

            InitDrawingResource();

            StartWaitForFrame(m_opCamList[m_nCamIndex]);
            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += timer1_Tick;
            timer.Start();

            InitChart();

            m_drawframe.ucFormatGet = (byte)TUFRM_FORMATS.TUFRM_FMT_RGB888;
            if (TUCAMRET.TUCAMRET_SUCCESS == TUCamera.TUCAM_Buf_WaitForFrame(m_opCamList[m_nCamIndex].hIdxTUCam, ref m_drawframe))
            {
                m_nCurWidth = m_bmpInfo.biWidth = m_drawframe.usWidth;
                m_nCurHeight = m_bmpInfo.biHeight = m_drawframe.usHeight;
            }


            TUCamera.TUCAM_Cap_GetROI(/*m_opCam.hIdxTUCam*/m_opCamList[m_nCamIndex].hIdxTUCam, ref roiAttr);
            //
            roiAttr.bEnable = false;
            roiAttr.nVOffset = 0;
            roiAttr.nHOffset = 0;
            roiAttr.nWidth = m_bmpInfo.biWidth;
            roiAttr.nHeight =m_bmpInfo.biHeight;
        
            TUCamera.TUCAM_Cap_SetROI(/*m_opCam.hIdxTUCam*/m_opCamList[m_nCamIndex].hIdxTUCam,  roiAttr);


        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = 0; i < m_opCamList.Count; i++)
            {
                TUCamera.TUCAM_Dev_Close(m_opCamList[i].hIdxTUCam);
                TUCamera.TUCAM_Draw_Uninit(m_opCamList[i].hIdxTUCam);
            }

            TUCamera.TUCAM_Api_Uninit();                     // 释放SDK 资源环境
        }

        //初始化绘制资源
        private void InitDrawingResource()
        {
            int nSize = 0;
            unsafe
            {
                nSize = sizeof(BITMAPINFOHEADER);
                //memset(&m_bmpInfo, 0, nSize);
            }

            m_bmpInfo.biSize = nSize;
            m_bmpInfo.biPlanes = 1;
            m_bmpInfo.biBitCount = 24;
            m_bmpInfo.biCompression = 0;

            hDraw = ShowpictureBox.Handle;
            hMainWin = TUCamera.FindWindow(null, "COVID19_Detection");
            m_hDC = TUCamera.GetDC(hDraw);
            m_hDib = TUCamera.DrawDibOpen();
        }

        int shit = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //定时执行的内容


            int nCliWidth = 0;
            int nCliHeight = 0;

            m_nDrawOffX = 0;
            m_nDrawOffY = 0;
            m_nDrawWidth = 0;
            m_nDrawHeight = 0;
            m_nCurWidth = 0;
            m_nCurHeight = 0;

            m_drawframe.ucFormatGet = (byte)TUFRM_FORMATS.TUFRM_FMT_RGB888;
            if (TUCAMRET.TUCAMRET_SUCCESS == TUCamera.TUCAM_Buf_WaitForFrame(m_opCamList[m_nCamIndex].hIdxTUCam, ref m_drawframe))
            {
                // 改变显示区域
                if (ShowpictureBox.Width != m_drawframe.usWidth || ShowpictureBox.Height != m_drawframe.usHeight || m_nCliWidth != nCliWidth || m_nCliHeight != nCliHeight)
                {
                    m_nCurWidth = m_bmpInfo.biWidth = m_drawframe.usWidth;
                    m_nCurHeight = m_bmpInfo.biHeight = m_drawframe.usHeight;

                    nCliWidth = ShowpictureBox.Width;
                    nCliHeight = ShowpictureBox.Height;

                    float fScaleX = nCliWidth * 1.0f / m_nCurWidth;
                    float fScaleY = nCliHeight * 1.0f / m_nCurHeight;

                    m_fScale = fScaleX > fScaleY ? fScaleY : fScaleX;
                    m_fScale = (float)((int)(m_fScale * 100) / 100.0f);

                    if (m_fScale < 1)
                    {
                        m_nDrawWidth = (int)(m_fScale * m_nCurWidth);
                        m_nDrawHeight = (int)(m_fScale * m_nCurHeight);
                    }
                    else
                    {
                        m_nDrawWidth = m_nCurWidth;
                        m_nDrawHeight = m_nCurHeight;
                    }

                    m_nDrawWidth = (m_nDrawWidth >> 2) << 2;
                    m_nDrawHeight = (m_nDrawHeight >> 2) << 2;

                    m_nDrawOffX = (nCliWidth - m_nDrawWidth) / 2;
                    m_nDrawOffY = (nCliHeight - m_nDrawHeight) / 2;

                    ShowpictureBox.Refresh();
                }

                // 绘制图像
                if (null != m_drawframe.pBuffer)
                {
                    m_drawing.pFrame = Marshal.AllocHGlobal(Marshal.SizeOf(m_drawframe));
                    Marshal.StructureToPtr(m_drawframe, m_drawing.pFrame, true);

                    m_drawing.nDstX = m_nDrawOffX;
                    m_drawing.nDstY = m_nDrawOffY;
                    m_drawing.nDstWidth = m_nDrawWidth;
                    m_drawing.nDstHeight = m_nDrawHeight;

                    m_drawing.nSrcX = 0;
                    m_drawing.nSrcY = 0;
                    m_drawing.nSrcWidth = m_nCurWidth;
                    m_drawing.nSrcHeight = m_nCurHeight;
                    TUCamera.TUCAM_Draw_Frame(m_opCamList[m_nCamIndex].hIdxTUCam, ref m_drawing);

                    Console.WriteLine(shit);
                    shit++;
                }
            }

            UpdateQueueValue();           
            for (int j = 0; j < NumChartArea; j++)
            {
                chart1.Series[j].Points.Clear();
                for (int i = 0; i < dataQueue.Count; i++)
                {                    
                    chart1.Series[j].Points.AddXY((i + 1), dataQueue.ElementAt(i)); //chart1.Series[j].Points.
                }               
            }
        }
        public void StartWaitForFrame(TUCAM_OPEN openCam)
        {
            //if (m_bWaitting)
            //    return;

            m_bWaitting = true;

            if ((IntPtr)0 == m_hThdWaitForFrame)
            {
                m_drawframe.pBuffer = IntPtr.Zero;
                m_drawframe.ucFormatGet = (byte)TUFRM_FORMATS.TUFRM_FMT_RGB888;
                m_drawframe.uiRsdSize = 1;                                                                                      // how many frames do you want

                int nValue = 0;
                if ((int)TUCAM_CAPTURE_MODES.TUCCM_SEQUENCE != m_nTriMode)
                {
                    nValue = (int)TUCamera.TUCAM_Cap_GetTrigger(openCam.hIdxTUCam, ref attrTgr);
                    attrTgr.nTgrMode = m_nTriMode;
                    attrTgr.nFrames = -1;                                                                                       // how many frames do you want to capture to RAM(the frames less than 0, use maximum frames )
                    nValue = (int)TUCamera.TUCAM_Cap_SetTrigger(openCam.hIdxTUCam, attrTgr);
                }
                else
                {
                    nValue = (int)TUCamera.TUCAM_Cap_GetTrigger(openCam.hIdxTUCam, ref attrTgr);
                    attrTgr.nTgrMode = (int)TUCAM_CAPTURE_MODES.TUCCM_SEQUENCE;
                    attrTgr.nFrames = 1;                                                                                        // TUCCM_SEQUENCE must set 1 frame
                    nValue = (int)TUCamera.TUCAM_Cap_SetTrigger(openCam.hIdxTUCam, attrTgr);
                }

                TUCamera.TUCAM_Buf_Alloc(openCam.hIdxTUCam, ref m_drawframe);                                                   // Alloc buffer after set resolution or set ROI attribute
                TUCamera.TUCAM_Cap_Start(openCam.hIdxTUCam, (uint)attrTgr.nTgrMode);                                            // Start capture

                m_dwFrmCnt = 0;                                                                                                 // reset frame count
                //m_dwSTm = TUCamera.GetTickCount();                                                                              // reset start tick count

                m_hThdWaitForFrame = TUCamera.CreateEvent(HANDLE.Zero, false, true, string.Empty);

//WaitForFrameThread.Start();

            }

            //EnableControl(m_bWaitting);
            //EnableTriggerControl(!m_bWaitting);
        }

        private void InitChart()
        {
            chart1.ChartAreas.Clear();
            chart1.Series.Clear();
            chart1.Titles.Clear();
            for (int i = 0; i < NumChartArea; i++)
            {
                //定义图表区域               
                ChartArea chartArea = new ChartArea();
                chartArea.Name = "ChartArea" + i;
                chart1.ChartAreas.Add(chartArea);
                //设置图表显示样式
                chartArea.AxisY.Minimum = 0;
                chartArea.AxisY.Maximum = 100;
                chartArea.AxisX.Interval = 5;
                chartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
                chartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
                //设置标题  
                Title title = new Title();
                title.DockedToChartArea = "ChartArea" + i;
                title.IsDockedInsideChartArea = false;
                title.Text = "XXX显示";
                title.ForeColor = Color.RoyalBlue;
                title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
                chart1.Titles.Add(title);                
                //定义存储和显示点的容器               
                System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
                series.Name = "emptyPT" + i;
                series.ChartArea = chartArea.Name = "ChartArea" + i; ;
                chart1.Series.Add(series);
                //设置图表显示样式
                series.Color = Color.Red;
                series.ChartType = SeriesChartType.Spline;
                series.Points.Clear();                
            }
            ////定义图表区域
            //this.chart1.ChartAreas.Clear();
            //ChartArea chartArea1 = new ChartArea("C1");
            //this.chart1.ChartAreas.Add(chartArea1);
            ////定义存储和显示点的容器
            //this.chart1.Series.Clear();
            //System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series("S1");
            //series1.ChartArea = "C1";            
            //this.chart1.Series.Add(series1);
            //ChartArea chartArea2 = new ChartArea("C2");
            //this.chart1.ChartAreas.Add(chartArea2);
            //System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series("S2");
            //series2.ChartArea = "C2";
            //this.chart1.Series.Add(series2);
            ////设置图表显示样式
            //this.chart1.ChartAreas[0].AxisY.Minimum = 0;
            //this.chart1.ChartAreas[0].AxisY.Maximum = 100;
            //this.chart1.ChartAreas[0].AxisX.Interval = 5;
            //this.chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            //this.chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            ////设置标题
            //this.chart1.Titles.Clear();
            //this.chart1.Titles.Add("S01");
            //this.chart1.Titles[0].Text = "XXX显示";
            //this.chart1.Titles[0].ForeColor = Color.RoyalBlue;
            //this.chart1.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            ////设置图表显示样式
            //this.chart1.Series[0].Color = Color.Red;
            //this.chart1.Series[0].ChartType = SeriesChartType.Spline;
            //this.chart1.Series[0].Points.Clear();
        }

        //更新队列中的值
        private void UpdateQueueValue()
        {

            if (dataQueue.Count > 100)
            {
                //先出列
                for (int i = 0; i < num; i++)
                {
                    dataQueue.Dequeue();
                }
            }
            for (int i = 0; i < num; i++)
            {
                //对curValue只取[0,360]之间的值
                curValue = curValue % 360;
                //对得到的正玄值，放大50倍，并上移50
                dataQueue.Enqueue((50 * Math.Sin(curValue * Math.PI / 180)) + 50);
                curValue = curValue + 10;
            }
        }

        private void btn_cam_config_Click(object sender, EventArgs e)
        {
            CamConfigForm camConfigForm = new CamConfigForm(this);
            camConfigForm.Show();
            timer.Stop();
        }

        private void ShowpictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}
