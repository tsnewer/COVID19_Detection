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
using System.Collections;
using System.IO;

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


        public TUCAM_ROI_ATTR roiAttr;

        

        private Queue<double> dataQueue = new Queue<double>(100);

        private int curValue = 0;

        private int num = 5;//每次删除增加几个点

        public double dbExp = 0;

        public int Pic_num = 10;
       
        public int NumTube = 12;
        public int NumSeries = 10;
        public double[,] value_to_show;
        public List<double>[] dataList; 
        public CamConfigForm camConfigForm;

        string startup_path;
        string data_path;
        public string file_path;

        public bool IsSave = false;

        public int record_time;

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

                //m_itDraw.hWnd = ShowpictureBox.Handle;
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
           // TUCamera.TUCAM_Prop_SetValue(m_opCam.hIdxTUCam, (int)TUCAM_IDPROP.TUIDP_EXPOSURETM, dbExp, 0);

            // 设置增益值
            //TUCamera.TUCAM_Prop_SetValue(m_opCam.hIdxTUCam, (int)TUCAM_IDPROP.TUIDP_GLOBALGAIN, dbGain, 0);

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

            
            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += timer1_Tick;
            timer.Start();

            //Init_Layout(tableLayoutPanel, NumTube, NumSeries);
            //InitDataCache(NumTube, NumSeries);
            StartWaitForFrame(m_opCamList[m_nCamIndex]);

            // InitChart(NumChartArea, 10);

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
            roiAttr.nHeight = m_bmpInfo.biHeight;

            TUCamera.TUCAM_Cap_SetROI(/*m_opCam.hIdxTUCam*/m_opCamList[m_nCamIndex].hIdxTUCam, roiAttr);

        
            //camConfigForm = new CamConfigForm(this);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(camConfigForm!=null)
                camConfigForm.WaitForFrameThread.Abort();
            for (int i = 0; i < m_opCamList.Count; i++)
            {
                TUCamera.TUCAM_Dev_Close(m_opCamList[i].hIdxTUCam);
                TUCamera.TUCAM_Draw_Uninit(m_opCamList[i].hIdxTUCam);
            }

            TUCamera.TUCAM_Api_Uninit();                     // 释放SDK 资源环境
        }

        private Chart chart = null;
        public  void Init_Layout(TableLayoutPanel tableLayoutPanel, int ch, int NumSeries)
        {
            this.SetRowColumn(tableLayoutPanel, ch);
            //tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            for (int i = 0; i < tableLayoutPanel.RowCount; i++)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            }
            for (int i = 0; i < tableLayoutPanel.ColumnCount; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            }
            int count = 0;
            for (int i = 0; i < tableLayoutPanel.RowCount; i++)
            {
                for (int j = 0; j < tableLayoutPanel.ColumnCount; j++)
                {
                    this.chart = new Chart();
                    InitChart(this.chart, NumSeries);
                    tableLayoutPanel.Controls.Add(this.chart, j, i);
                    count++;
                    if (count >= ch)
                    {
                        break;
                    }
                }
            }
        }

        private void SetRowColumn(TableLayoutPanel tableLayoutPanel, int row, int column)
        {
            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.RowCount = row;
            tableLayoutPanel.ColumnCount = column;
        }

        private void SetRowColumn(TableLayoutPanel tableLayoutPanel, int ch)
        {
            if (ch == 1)
            {
                this.SetRowColumn(tableLayoutPanel, 1, 1);
            }
            else if (ch == 2)
            {
                this.SetRowColumn(tableLayoutPanel, 2, 1);
            }
            else if (ch <= 4)
            {
                this.SetRowColumn(tableLayoutPanel, 2, 2);
            }
            else if (ch <= 6)
            {
                this.SetRowColumn(tableLayoutPanel, 3, 2);
            }
            else if (ch <= 9)
            {
                this.SetRowColumn(tableLayoutPanel, 3, 3);
            }
            else if (ch <= 12)
            {
                this.SetRowColumn(tableLayoutPanel, 3, 4);
            }
            else if (ch <= 16)
            {
                this.SetRowColumn(tableLayoutPanel, 4, 4);
            }
            else if (ch <= 20)
            {
                this.SetRowColumn(tableLayoutPanel, 4, 5);
            }
            else if (ch <= 25)
            {
                this.SetRowColumn(tableLayoutPanel, 5, 5);
            }
            else if (ch <= 30)
            {
                this.SetRowColumn(tableLayoutPanel, 5, 6);
            }
            else if (ch <= 36)
            {
                this.SetRowColumn(tableLayoutPanel, 6, 6);
            }
            else if (ch <= 42)
            {
                this.SetRowColumn(tableLayoutPanel, 6, 7);
            }
            else if (ch <= 49)
            {
                this.SetRowColumn(tableLayoutPanel, 7, 7);
            }
            else
            {
                this.SetRowColumn(tableLayoutPanel, 8, 8);
            }
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

            //hDraw = ShowpictureBox.Handle;
            hMainWin = TUCamera.FindWindow(null, "COVID19_Detection");
            m_hDC = TUCamera.GetDC(hDraw);
            m_hDib = TUCamera.DrawDibOpen();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           // Random random = new Random();
           // for (int i = 0; i < NumTube; i++)
           // {
           //     for (int j = 0; j < NumSeries; j++)
           //     {
           //         value_to_show[i,j]= random.Next(0, 99);
           //     }
           // }
           //UpdateListValue();
            DrawWaveForm();
        }

        public void UpdateListValue()
        {
            for (int i = 0; i < NumTube; i++)
            {
                for (int j = 0; j < NumSeries; j++)
                {
                    dataList[i * NumSeries + j].Add(value_to_show[i,j]);
                    if (dataList[i * NumSeries + j].Count > 20)
                    {
                        dataList[i * NumSeries + j].RemoveAt(0);
                    }
                }
            }
        }

        public void DrawWaveForm()//double[,] data
        {
            IEnumerator enumerator = tableLayoutPanel.Controls.GetEnumerator();
            int i = 0;
            while (enumerator.MoveNext())
            {               
                Chart chart1 = (Chart)enumerator.Current;
                for (int j = 0; j < NumSeries; j++)
                {
                    chart1.Series[j].Points.Clear();
                    for (int k = 0; k < dataList[i * NumSeries + j].Count; k++)
                    {
                        chart1.Series[j].Points.AddXY((k + 1), dataList[i * NumSeries + j][k]);
                    }
                }
                i++;
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

        public void InitChart(Chart chart1, int NumSeries)
        {
            chart1.Dock = DockStyle.Fill;
            chart1.Margin = new Padding(0);
            chart1.ChartAreas.Clear();
            chart1.Series.Clear();
            chart1.Titles.Clear();

            //定义图表区域               
            ChartArea chartArea = new ChartArea();
            chartArea.Name = "ChartArea";
            chart1.ChartAreas.Add(chartArea);
            //设置图表显示样式
            //chartArea.AxisY.Minimum = 0;
            //chartArea.AxisY.Maximum = 100;
            //chartArea.AxisX.Interval = 5;
            chartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            //设置标题  
            Title title = new Title();
            title.DockedToChartArea = "ChartArea";
            title.IsDockedInsideChartArea = false;
            title.Text = "XXX显示";
            title.ForeColor = Color.RoyalBlue;
            title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            chart1.Titles.Add(title);
            for (int i = 0; i < NumSeries; i++)
            {
                //定义存储和显示点的容器               
                System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
                series.Name = "emptyPT" + i;
                series.ChartArea = "ChartArea";
                series.IsVisibleInLegend = false;
                chart1.Series.Add(series);
                //设置图表显示样式
                //series.Color = Color.Red;
                series.ChartType = SeriesChartType.Spline;
                series.Points.Clear();
            }
        }

        public void InitDataCache(int ch, int NumSeries)
        {
            value_to_show = null;
            dataList = null;
            value_to_show = new double[ch, NumSeries];
            dataList = new List<double>[ch * NumSeries];
            for (int i = 0; i < ch * NumSeries; i++)
            {
                dataList[i] = new List<double>();
            }
        }

        private void btn_cam_config_Click(object sender, EventArgs e)
        {
            camConfigForm = new CamConfigForm(this);
            camConfigForm.Show();
            //timer.Stop();
        }

        public void InitPath()
        {
            startup_path = Application.StartupPath;
            data_path = startup_path + "\\Data";
            if (!Directory.Exists(data_path))
            {
                Directory.CreateDirectory(data_path);
            }
            file_path = string.Concat(new object[] { data_path, "\\", DateTime.Now.ToString("yyyyMMdd_HHmmss") });
            if (!Directory.Exists(file_path))
            {
                Directory.CreateDirectory(file_path);
            }
        }

        public void WriteDataFile(string file_path, double[] data,bool clear)
        {
            string temp_data = "";
            FileStream fs;
            if (clear==false)
            {
                fs = new FileStream(file_path, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(file_path, FileMode.Create, FileAccess.Write);
            }

            for (int i = 0; i < data.Length; i++)
            {
                if (i == data.Length - 1)
                {
                    temp_data = temp_data + (int)data[i];
                }
                else
                {
                    temp_data = temp_data + (int)data[i] + "\t";
                }
            }
            temp_data += Environment.NewLine;
            StreamWriter writer = new StreamWriter(fs);
            writer.Write(temp_data);
            writer.Flush();
            writer.Close();
            fs.Close();
        }

        public int[] ReadDataFile(string file_path)
        {
            StreamReader reader = new StreamReader(file_path);
            string data = reader.ReadToEnd();
            string[] array = data.Split('\t');
            int[] result = Array.ConvertAll<string, int>(array, (string element) => int.Parse(element));
            reader.Close();
            return result;
        }

        private void btn_start_savedata_Click(object sender, EventArgs e)
        {           
            if (IsSave == false)
            {
                InitPath();
                btn_start_savedata.Text = "停止存储";
                IsSave = true;
            }
            else
            {
                btn_start_savedata.Text = "开始存储";
                IsSave = false;
            }
           
        }

        private void btn_record_time_Click(object sender, EventArgs e)
        {           
            try
            {
                record_time = Convert.ToInt32(tb_record_time.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show("请填写正确的整数时间！");
            }
        }
    }
}
