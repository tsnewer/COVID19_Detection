using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using TUCAMERA;
using System.Threading;
using System.Runtime.InteropServices;
using HANDLE = System.IntPtr;
using System.Windows.Forms.DataVisualization.Charting;


namespace COVID19_Detection
{
    public partial class CamConfigForm : Form
    {
        MainForm mainform;
        Thread WaitForFrameThread;                 //等待线程


        HANDLE m_hDC;
        HANDLE m_hDib;
        HANDLE hDraw;                              //显示区域句柄
        HANDLE hMainWin;                           //主窗口句柄

        System.Windows.Forms.Timer timer;

        //TUCAM_DRAW m_drawing;


        public int m_nCurWidth = 0;                       // 当前宽度
        public int m_nCurHeight = 0;                      // 当前高度
        public int m_nCliWidth = 0;                       // 当前客户端宽度
        public int m_nCliHeight = 0;                      // 当前客户端高度
        public int m_nDrawOffX = 0;                       // 水平偏移  
        public int m_nDrawOffY = 0;                       // 垂直偏移
        public int m_nDrawWidth = 0;                      // 绘制宽度
        public int m_nDrawHeight = 0;                     // 绘制高度

        public float m_fScale = 0;                        // 缩放比例


        public TUCAM_DRAW_INIT m_itDraw;                  // 绘制初始化参数

        int n = 0;
        // flag = true;


        bool draw_flag;
        private Rectangle currRect = new Rectangle(-1, -1, -1, -1);
        
        
        double[] H_averg = new double[2048];
      
        //double[] averg;
        double[] average_temp;
        double[] average = new double[2048 * 1148];
        int[] top = new int[2048];
        int tubenum = 0;

        //SynchronizationContext m_SyncContext = null;

        public CamConfigForm(MainForm form)
        {
            InitializeComponent();

            mainform = form;

            //m_SyncContext = SynchronizationContext.Current;
        }

        private void CamConfigForm_Load(object sender, EventArgs e)
        {

            TUCamera.TUCAM_Draw_Uninit(mainform.m_opCam.hIdxTUCam);
            m_itDraw.hWnd = this.ShowpictureBoxCamera.Handle;
            m_itDraw.ucChannels = (sbyte)mainform.valueinfo.nValue;
            m_itDraw.nWidth = 2048;
            m_itDraw.nHeight = 1148;
            m_itDraw.nMode = 0;
            TUCamera.TUCAM_Draw_Init(mainform.m_opCam.hIdxTUCam, m_itDraw);

            textBox_Expo.Text = mainform.dbExp.ToString();

            H_offset.Text = mainform.roiAttr.nHOffset.ToString();
            V_offset.Text = mainform.roiAttr.nVOffset.ToString();
            ROI_Height.Text = mainform.roiAttr.nHeight.ToString();
            ROI_Width.Text = mainform.roiAttr.nWidth.ToString();

            checkBox_Roi.Checked = true;
            PicNum.Value = mainform.Pic_num;

            Tube_Num.Value = 0;
            H_num.Value = 20;
            V_Pixel.Value = 10;
            check_tube.Checked = false;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 10;
            timer.Tick += timer1_Tick;
            timer.Start();



            WaitForFrameThread = new System.Threading.Thread(new ThreadStart(WaitForFrame));
            WaitForFrameThread.Start();

            InitChart();
        }

        private void CamConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            TUCamera.TUCAM_Draw_Uninit(mainform.m_opCam.hIdxTUCam);
            TUCamera.TUCAM_Draw_Init(mainform.m_opCam.hIdxTUCam, mainform.m_itDraw);
            timer.Stop();

            WaitForFrameThread.Abort();
            mainform.timer.Start();

        }

        private void ShowpictureBox_Click(object sender, EventArgs e)
        {

        }




        public void WaitForFrame()

        {
            Control.CheckForIllegalCrossThreadCalls = false;
            int nCliWidth = 0;
            int nCliHeight = 0;

            m_nDrawOffX = 0;
            m_nDrawOffY = 0;
            m_nDrawWidth = 0;
            m_nDrawHeight = 0;
            m_nCurWidth = 0;
            m_nCurHeight = 0;

            int num = 0;

            while (mainform.m_bWaitting)
            {
                // 计算帧率
                //m_dwFrmCnt++;                                  // 帧数加1
                //m_dwITm = TUCamera.GetTickCount() - m_dwSTm;   // 计算间隔时间 ms
                //if (m_dwITm > 1000)
                //{
                //    m_fFps = m_dwFrmCnt * 1000000.0f / m_dwITm;
                //    TUCamera.PostMessage(hMainWin, REFRESH_FRAMELABEL, 0, (int)m_fFps);
                //    m_dwSTm = TUCamera.GetTickCount();

                //    m_dwFrmCnt = 0;
                //}


                mainform.m_drawframe.ucFormatGet = (byte)TUFRM_FORMATS.TUFRM_FMT_RGB888;
                if (TUCAMRET.TUCAMRET_SUCCESS == TUCamera.TUCAM_Buf_WaitForFrame(mainform.m_opCamList[mainform.m_nCamIndex].hIdxTUCam, ref mainform.m_drawframe))
                {
                    // 改变显示区域
                    if (ShowpictureBoxCamera.Width != mainform.m_drawframe.usWidth || ShowpictureBoxCamera.Height != mainform.m_drawframe.usHeight || m_nCliWidth != nCliWidth || m_nCliHeight != nCliHeight)
                    {
                        m_nCurWidth = mainform.m_bmpInfo.biWidth = mainform.m_drawframe.usWidth;
                        m_nCurHeight = mainform.m_bmpInfo.biHeight = mainform.m_drawframe.usHeight;

                        nCliWidth = ShowpictureBoxCamera.Width;
                        nCliHeight = ShowpictureBoxCamera.Height;

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


                    }


                    // 绘制图像
                    if (null != mainform.m_drawframe.pBuffer)
                    {

                        mainform.m_drawing.pFrame = Marshal.AllocHGlobal(Marshal.SizeOf(mainform.m_drawframe));
                        Marshal.StructureToPtr(mainform.m_drawframe, mainform.m_drawing.pFrame, true);

                        mainform.m_drawing.nDstX = m_nDrawOffX;
                        mainform.m_drawing.nDstY = m_nDrawOffY;
                        mainform.m_drawing.nDstWidth = m_nDrawWidth;
                        mainform.m_drawing.nDstHeight = m_nDrawHeight;

                        mainform.m_drawing.nSrcX = 0;
                        mainform.m_drawing.nSrcY = 0;
                        mainform.m_drawing.nSrcWidth = m_nCurWidth;
                        mainform.m_drawing.nSrcHeight = m_nCurHeight;

                        TUCamera.TUCAM_Draw_Frame(mainform.m_opCamList[mainform.m_nCamIndex].hIdxTUCam, ref mainform.m_drawing);

                        if (draw_flag)
                        {
                            Rectangle _Rect = ShowpictureBoxCamera.RectangleToScreen(currRect);

                            ControlPaint.DrawReversibleFrame(_Rect, Color.Green, FrameStyle.Dashed);
                        }


                    }


                    mainform.m_drawframe.ucFormatGet = (byte)TUFRM_FORMATS.TUFRM_FMT_USUAl;

                    if (TUCAMRET.TUCAMRET_SUCCESS == TUCamera.TUCAM_Buf_CopyFrame(/*m_opCam.hIdxTUCam*/mainform.m_opCamList[mainform.m_nCamIndex].hIdxTUCam, ref mainform.m_drawframe))
                    {
                        int nSize = (int)(mainform.m_drawframe.uiImgSize + mainform.m_drawframe.usHeader);




                        byte[] pBuf = new byte[nSize];

                        if (num == 0)
                            average_temp = new double[mainform.m_drawframe.uiImgSize / mainform.m_drawframe.ucDepth];


                        Marshal.Copy(mainform.m_drawframe.pBuffer, pBuf, 0, nSize);
                        Buffer.BlockCopy(pBuf, (int)mainform.m_drawframe.usHeader, pBuf, 0, (int)mainform.m_drawframe.uiImgSize);

                        double[] sum = new double[mainform.m_drawframe.usWidth];

                        for (int i = 0; i < mainform.m_drawframe.uiWidthStep; i += mainform.m_drawframe.ucDepth)
                            for (int j = 0; j < mainform.m_drawframe.usHeight; j++)
                            {
                                //byte a = pBuf[i + (j) * (int)mainform.m_drawframe.uiWidthStep];
                                //byte b = pBuf[i+1 + (j) * (int)mainform.m_drawframe.uiWidthStep];
                                // sum[i / mainform.m_drawframe.ucDepth] += (double)((ushort)pBuf[i + (j) * (int)mainform.m_drawframe.uiWidthStep] + pBuf[i + 1 + (j) * (int)mainform.m_drawframe.uiWidthStep] * 256);
                                //sum[i / mainform.m_drawframe.ucDepth] += (double)((ushort)pBuf[i +1+ (j) * (int)mainform.m_drawframe.uiWidthStep] );
                                average_temp[i / mainform.m_drawframe.ucDepth + (j) * (int)mainform.m_drawframe.usWidth] += (double)((ushort)pBuf[i + (j) * (int)mainform.m_drawframe.uiWidthStep] + pBuf[i + 1 + (j) * (int)mainform.m_drawframe.uiWidthStep] * 256);
                            }
                        num++;
                        if (num >= mainform.Pic_num)
                        {
                            num = 0;
                            for (int i = 0; i < mainform.m_drawframe.uiWidthStep; i += mainform.m_drawframe.ucDepth)
                                for (int j = 0; j < mainform.m_drawframe.usHeight; j++)
                                {
                                    average[i / mainform.m_drawframe.ucDepth + (j) * (int)mainform.m_drawframe.usWidth] = average_temp[i / mainform.m_drawframe.ucDepth + (j) * (int)mainform.m_drawframe.usWidth] / mainform.Pic_num;




                                }
                            for (int i = 0; i < mainform.m_drawframe.usWidth; i++)
                                for (int j = 0; j < mainform.m_drawframe.usHeight; j++)
                                {
                                    sum[i] += average[i + j * mainform.m_drawframe.usWidth];
                                }
                            double a = 0;
                            for (int i = 0; i < mainform.m_drawframe.usWidth; i++)
                            {
                                H_averg[i] = sum[i] / mainform.m_drawframe.usHeight;
                                a += H_averg[i];
                            }
                            a/= mainform.m_drawframe.usWidth;
                            if (check_tube.Checked == true)
                            {
                                //double[] top = new double[mainform.m_drawframe.usWidth];
                                n = 0;
                                for (int i = decimal.ToInt32( V_Pixel.Value); i < mainform.m_drawframe.usWidth - V_Pixel.Value; i++)
                                {
                                   

                                    if (((H_averg[i] - H_averg[i - 1]) * (H_averg[i+1] - H_averg[i]) < 0) && (H_averg[i]<a))
                                    {
                                        top[n] = i;
                                        n++;
                                    }

                                }
                                bool flag = true;
                             
                                while (flag)
                                {
                                    flag = false;
                                    int nn = n;
                                    for (int i = 1; i < nn; i++)
                                    {
                               
                                        if ((top[i] - top[i - 1] < Convert.ToInt32(V_Pixel.Value) * 2) &&(top[i] != top[i - 1]))
                                        {
                                            flag = true;n--;
                                            if (H_averg[Convert.ToInt32(top[i])] < H_averg[Convert.ToInt32(top[i - 1])])
                                                top[i - 1] = 0;
                                            else
                                                top[i] = 0;
                                        }
                                    }
                                    for (int i = 0; i < nn; i++)
                                    {
                                        if (top[i] == 0)
                                        { 
                                          for (int j =i; j<nn+1; j++)
                                                top[j] = top[j + 1];
                                            if ((top[i] == 0) && (i < n))
                                                i--;
                                         }

                                    }
                                }

                                // if(Tube_Num.Value !=n)
                                //     Tube_Num.Value = n;
                                tubenum = n;
                                for (int nn=0;nn<n;nn++)
                                {
                                    double min = H_averg[Convert.ToInt32(top[nn])];
                                    for (int i = top[nn] - Convert.ToInt32(V_Pixel.Value); i < top[nn] + Convert.ToInt32(V_Pixel.Value); i++)
                                        if (H_averg[top[i]] < min) 
                                         {
                                            min = H_averg[Convert.ToInt32(top[nn])];
                                            top[nn] = i;
                                        }
                                }
                            }

                        }
                    }
                }
            }
            WaitForFrameThread.Abort();
        }



        private void ShowpictureBoxCamera_Resize(object sender, EventArgs e)
        {
            ShowpictureBoxCamera.Refresh();
        }

        private void textBox_Expo_TextChanged(object sender, EventArgs e)

        {
            if (textBox_Expo.Text != "")
            {
                mainform.dbExp = Convert.ToDouble(textBox_Expo.Text);
                TUCamera.TUCAM_Prop_SetValue(mainform.m_opCam.hIdxTUCam, (int)TUCAM_IDPROP.TUIDP_EXPOSURETM, mainform.dbExp, 0);
            }

        }



        private void ShowpictureBoxCamera_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left && checkBox_Roi.Checked == true)
            {

                if (Convert.ToDouble(e.X - m_nDrawOffX) / m_fScale < mainform.roiAttr.nWidth && Convert.ToDouble(e.X - m_nDrawOffX) / m_fScale > 0
                    && Convert.ToDouble(e.Y - m_nDrawOffY) / m_fScale < mainform.roiAttr.nHeight && Convert.ToDouble(e.Y - m_nDrawOffY) / m_fScale > 0)
                {
                    currRect.X = e.X;
                    currRect.Y = e.Y;

                    H_offset.Text = Convert.ToInt32(Convert.ToDouble(e.X - m_nDrawOffX) / m_fScale).ToString();
                    V_offset.Text = Convert.ToInt32(Convert.ToDouble(e.Y - m_nDrawOffY) / m_fScale).ToString();



                    draw_flag = true;
                }
                //bool flag2 = (double)this.pointstt.X * this.xscale < this.m_uiMAXWidth - 150U && (double)this.pointstt.Y * this.yscale < this.m_uiMAXHeight - 150U;
                //if (flag2)
                //{


                // }
            }


        }

        private void ShowpictureBoxCamera_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && checkBox_Roi.Checked == true)
            {
                draw_flag = false;

                if (Convert.ToDouble(e.X - m_nDrawOffX) / m_fScale < mainform.roiAttr.nWidth && Convert.ToDouble(e.X - m_nDrawOffX) / m_fScale > 0 && Convert.ToInt32(Convert.ToDouble(e.X - m_nDrawOffX) / m_fScale) > Convert.ToInt32(H_offset.Text)
                    && Convert.ToDouble(e.Y - m_nDrawOffY) / m_fScale < mainform.roiAttr.nHeight && Convert.ToDouble(e.Y - m_nDrawOffY) / m_fScale > 0 && Convert.ToInt32(Convert.ToDouble(e.Y - m_nDrawOffY) / m_fScale) > Convert.ToInt32(V_offset.Text))
                {
                    mainform.roiAttr.nHOffset += Convert.ToInt32(H_offset.Text);
                    mainform.roiAttr.nVOffset += Convert.ToInt32(V_offset.Text);

                    mainform.roiAttr.nWidth = Convert.ToInt32(Convert.ToDouble(e.X - m_nDrawOffX) / m_fScale) - Convert.ToInt32(H_offset.Text);
                    mainform.roiAttr.nHeight = Convert.ToInt32(Convert.ToDouble(e.Y - m_nDrawOffY) / m_fScale) - Convert.ToInt32(V_offset.Text);

                    mainform.roiAttr.bEnable = true;
                    setRoi(mainform.roiAttr);



                    H_offset.Text = mainform.roiAttr.nHOffset.ToString();
                    V_offset.Text = mainform.roiAttr.nVOffset.ToString();
                    ROI_Width.Text = mainform.roiAttr.nWidth.ToString();
                    ROI_Height.Text = mainform.roiAttr.nHeight.ToString();



                }
                else
                {
                    H_offset.Text = mainform.roiAttr.nHOffset.ToString();
                    V_offset.Text = mainform.roiAttr.nVOffset.ToString();
                }
            }
        }


        private void setRoi(TUCAM_ROI_ATTR roiAttr)
        {
            WaitForFrameThread.Abort();

            TUCamera.TUCAM_Buf_AbortWait(mainform.m_opCamList[mainform.m_nCamIndex].hIdxTUCam);             // If you called TUCAM_Buf_WaitForFrames()


            TUCamera.TUCAM_Cap_Stop(mainform.m_opCamList[mainform.m_nCamIndex].hIdxTUCam);                  // Stop capture   
            TUCamera.TUCAM_Buf_Release(mainform.m_opCamList[mainform.m_nCamIndex].hIdxTUCam);               // Release alloc buffer after stop capture and quit drawing thread


            TUCamera.TUCAM_Cap_SetROI(mainform.m_opCamList[mainform.m_nCamIndex].hIdxTUCam, roiAttr);


            TUCamera.TUCAM_Cap_GetROI(/*m_opCam.hIdxTUCam*/mainform.m_opCamList[mainform.m_nCamIndex].hIdxTUCam, ref roiAttr);


            mainform.m_drawframe.pBuffer = IntPtr.Zero;
            mainform.m_drawframe.ucFormatGet = (byte)TUFRM_FORMATS.TUFRM_FMT_RGB888;
            mainform.m_drawframe.uiRsdSize = 1U;
            TUCamera.TUCAM_Buf_Alloc(mainform.m_opCamList[mainform.m_nCamIndex].hIdxTUCam, ref mainform.m_drawframe);                                                   // Alloc buffer after set resolution or set ROI attribute
            TUCamera.TUCAM_Cap_Start(mainform.m_opCamList[mainform.m_nCamIndex].hIdxTUCam, 0U);


            WaitForFrameThread = new System.Threading.Thread(new ThreadStart(WaitForFrame));
            WaitForFrameThread.Start();

            ShowpictureBoxCamera.Refresh();
        }
        private void checkBox_Roi_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Roi.Checked == false)
            {
                check_tube.Checked = false;
                mainform.roiAttr.bEnable = false;

                mainform.roiAttr.nVOffset = 0;
                mainform.roiAttr.nHOffset = 0;
                mainform.roiAttr.nWidth = 2048;
                mainform.roiAttr.nHeight = 1148;
                setRoi(mainform.roiAttr);


                H_offset.Text = mainform.roiAttr.nHOffset.ToString();
                V_offset.Text = mainform.roiAttr.nVOffset.ToString();
                ROI_Width.Text = mainform.roiAttr.nWidth.ToString();
                ROI_Height.Text = mainform.roiAttr.nHeight.ToString();
            }
        }

        private void ShowpictureBoxCamera_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && checkBox_Roi.Checked == true)
            {
                if (Convert.ToDouble(e.X - m_nDrawOffX) / m_fScale < mainform.roiAttr.nWidth && Convert.ToDouble(e.X - m_nDrawOffX) / m_fScale > 0 && Convert.ToInt32(Convert.ToDouble(e.X - m_nDrawOffX) / m_fScale) > Convert.ToInt32(H_offset.Text)
                    && Convert.ToDouble(e.Y - m_nDrawOffY) / m_fScale < mainform.roiAttr.nHeight && Convert.ToDouble(e.Y - m_nDrawOffY) / m_fScale > 0 && Convert.ToInt32(Convert.ToDouble(e.Y - m_nDrawOffY) / m_fScale) > Convert.ToInt32(V_offset.Text))
                {
                    currRect.Width = e.X - currRect.X;
                    currRect.Height = e.Y - currRect.Y;
                    Rectangle _Rect = ShowpictureBoxCamera.RectangleToScreen(currRect);

                    ControlPaint.DrawReversibleFrame(_Rect, Color.Green, FrameStyle.Thick);
                    //ShowpictureBoxCamera.Refresh();
                }
            }
        }

        private void ShowpictureBoxCamera_Paint(object sender, PaintEventArgs e)
        {
            //if(draw_flag)
            //{
            //    Graphics g = e.Graphics;
            //    g.DrawRectangle(new Pen(Color.Green, 2f), this.currRect);
            //    TUCamera.TUCAM_Draw_Frame(mainform.m_opCamList[mainform.m_nCamIndex].hIdxTUCam, ref mainform.m_drawing);
            //}
        }

        private void H_offset_TextChanged(object sender, EventArgs e)
        {

        }

        private void ROI_Set_Click(object sender, EventArgs e)
        {
            if (checkBox_Roi.Checked == true)
            {
                mainform.roiAttr.nHOffset = Convert.ToInt32(H_offset.Text);
                mainform.roiAttr.nVOffset = Convert.ToInt32(V_offset.Text);
                mainform.roiAttr.nWidth = Convert.ToInt32(ROI_Width.Text);
                mainform.roiAttr.nHeight = Convert.ToInt32(ROI_Height.Text);
                setRoi(mainform.roiAttr);

            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            mainform.Pic_num = decimal.ToInt32(PicNum.Value);
          


        }

        private void InitChart()
        {
            chart1.ChartAreas.Clear();
            chart1.Series.Clear();
            chart1.Titles.Clear();

            //定义图表区域               
            ChartArea chartArea = new ChartArea();
            chartArea.Name = "ChartArea";
            chart1.ChartAreas.Add(chartArea);
            //设置图表显示样式
            //chartArea.AxisX.Minimum = 0;
            // chartArea.AxisX.Maximum = 10000;
            chartArea.AxisY.Minimum = 0;
            chartArea.AxisY.Maximum = 65536;
            chartArea.AxisX.Interval = 20;
            chartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            //设置标题  
            Title title = new Title();
            title.DockedToChartArea = "ChartArea";
            title.IsDockedInsideChartArea = false;
            title.Text = "垂直投影";
            title.ForeColor = Color.RoyalBlue;
            title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            chart1.Titles.Add(title);
            //定义存储和显示点的容器               
            System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
            series.Name = "rawCurver";
            series.ChartArea = chartArea.Name; ;
            chart1.Series.Add(series);
            //设置图表显示样式
            series.Color = Color.Red;
            series.ChartType = SeriesChartType.Spline;
            series.Points.Clear();

            
            for (int i = 0; i < Tube_Num.Value; i++)
            {
                System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
                series.Name = "i";
                series.Color = Color.Green;
                series.ChartArea = chartArea.Name;
                chart1.Series.Add(series2);
            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            for (int i = 0; i < mainform.m_drawframe.usWidth; i++)
            {

                chart1.Series[0].Points.AddXY(i, H_averg[i]);
            }
            if ((check_tube.Checked == true) && (tubenum >= Tube_Num.Value))
                for (int nn = 0; nn < Tube_Num.Value; nn++)
                {
                    chart1.Series[nn + 1].Points.Clear();
                    for (int i = top[nn] - Convert.ToInt32(V_Pixel.Value); i < top[nn] + Convert.ToInt32(V_Pixel.Value); i++)
                        chart1.Series[nn + 1].Points.AddXY(i, H_averg[i]);


                }
        }

        private void Tube_Num_ValueChanged(object sender, EventArgs e)
        {

            //InitChart();
            chart1.Series.Clear();
            //定义存储和显示点的容器               
            System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
            series.Name = "rawCurver";
            series.ChartArea = chart1.ChartAreas[0].Name; ;
            chart1.Series.Add(series);
            //设置图表显示样式
            series.Color = Color.Red;
            series.ChartType = SeriesChartType.Spline;
            series.Points.Clear();


            for (int i = 0; i < Tube_Num.Value; i++)
            {
                System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
                series2.Name = (i + 1).ToString();
                series2.Color = Color.Green;
                series2.ChartType = SeriesChartType.Spline;
                series2.ChartArea = chart1.ChartAreas[0].Name;
                chart1.Series.Add(series2);

            }

        }

        private void check_tube_CheckedChanged(object sender, EventArgs e)
        {
            if (check_tube.Checked == false)
            {
                Tube_Num.Value = 0;
                chart1.Series.Clear();
                //定义存储和显示点的容器               
                System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
                series.Name = "rawCurver";
                series.ChartArea = chart1.ChartAreas[0].Name; ;
                chart1.Series.Add(series);
                //设置图表显示样式
                series.Color = Color.Red;
                series.ChartType = SeriesChartType.Spline;
                series.Points.Clear();
            }
            else
            {
                chart1.Series.Clear();
                //定义存储和显示点的容器               
                System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
                series.Name = "rawCurver";
                series.ChartArea = chart1.ChartAreas[0].Name; ;
                chart1.Series.Add(series);
                //设置图表显示样式
                series.Color = Color.Red;
                series.ChartType = SeriesChartType.Spline;
                series.Points.Clear();


                for (int i = 0; i < Tube_Num.Value; i++)
                {
                    System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
                    series2.Name = (i + 1).ToString();
                    series2.Color = Color.Green;
                    series2.ChartType = SeriesChartType.Spline;
                    series2.ChartArea = chart1.ChartAreas[0].Name;
                    chart1.Series.Add(series2);

                }
            }

        }

        //private void SetTextSafePost()
        //{
        //    chart1.Series[0].Points.Clear();
        //    for (int i = 0; i < mainform.m_drawframe.usWidth; i++)
        //    {

        //       chart1.Series[0].Points.AddXY(i, H_averg[i]);
        //    }
        //}
    }
}
