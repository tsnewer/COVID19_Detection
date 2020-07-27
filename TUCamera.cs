
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using HANDLE = System.IntPtr;

enum TUCAMRET
{
    //  success
    TUCAMRET_SUCCESS = 0x00000001,       // no error, general success code, app should check the value is positive   
    TUCAMRET_FAILURE = 0x00000000,       // error  

    //  initialization error
    //     TUCAMRET_NO_MEMORY          = 0x80000101,       // not enough memory
    //     TUCAMRET_NO_RESOURCE        = 0x80000102,       // not enough resource except memory    
    //     TUCAMRET_NO_MODULE          = 0x80000103,       // no sub module
    //     TUCAMRET_NO_DRIVER          = 0x80000104,       // no driver
    //     TUCAMRET_NO_CAMERA          = 0x80000105,       // no camera
    //     TUCAMRET_NO_GRABBER         = 0x80000106,       // no grabber  
    //     TUCAMRET_NO_PROPERTY        = 0x80000107,       // there is no alternative or influence id, or no more property id
    // 
    //     TUCAMRET_FAILOPEN_CAMERA    = 0x80000110,       // fail open the camera
    //     TUCAMRET_FAILOPEN_BULKIN    = 0x80000111,       // fail open the bulk in endpoint
    //     TUCAMRET_FAILOPEN_BULKOUT   = 0x80000112,       // fail open the bulk out endpoint
    //     TUCAMRET_FAILOPEN_CONTROL   = 0x80000113,       // fail open the control endpoint
    //     TUCAMRET_FAILCLOSE_CAMERA   = 0x80000114,       // fail close the camera
    // 
    //     //  status error
    //     TUCAMRET_INIT               = 0x80000201,       // API requires has not initialized state.
    //     TUCAMRET_BUSY               = 0x80000202,       // API cannot process in busy state.
    //     TUCAMRET_NOT_INIT           = 0x80000203,       // API requires has initialized state.
    //     TUCAMRET_EXCLUDED           = 0x80000204,       // some resource is exclusive and already used.
    //     TUCAMRET_NOT_BUSY           = 0x80000205,       // API requires busy state.
    //     TUCAMRET_NOT_READY          = 0x80000206,       // API requires ready state.
    //     
    //     //  wait error
    //     TUCAMRET_ABORT              = 0x80000207,       // abort process
    //     TUCAMRET_TIMEOUT            = 0x80000208,       // timeout
    //     TUCAMRET_LOSTFRAME          = 0x80000209,       // frame data is lost
    //     TUCAMRET_MISSFRAME          = 0x8000020A,       // frame is lost but reason is low lever driver's bug
    // 
    //     // calling error
    //     TUCAMRET_INVALID_CAMERA     = 0x80000301,       // invalid camera
    //     TUCAMRET_INVALID_HANDLE     = 0x80000302,       // invalid camera handle
    //     TUCAMRET_INVALID_OPTION     = 0x80000303,       // invalid the option value of structure
    //     TUCAMRET_INVALID_IDPROP     = 0x80000304,       // invalid property id
    //     TUCAMRET_INVALID_IDCAPA     = 0x80000305,       // invalid capability id
    //     TUCAMRET_INVALID_PARAM      = 0x80000306,       // invalid parameter
    //     TUCAMRET_INVALID_FRAMEIDX   = 0x80000307,       // invalid frame index
    //     TUCAMRET_INVALID_VALUE      = 0x80000308,       // invalid property value
    //     TUCAMRET_INVALID_EQUAL      = 0x80000309,       // invalid property value equal 
    //     TUCAMRET_INVALID_CHANNEL    = 0x8000030A,       // the property id specifies channel but channel is invalid
    //     TUCAMRET_INVALID_SUBARRAY   = 0x8000030B,       // the combination of subarray values are invalid. e.g. TUCAM_IDPROP_SUBARRAYHPOS + TUCAM_IDPROP_SUBARRAYHSIZE is greater than the number of horizontal pixel of sensor.
    //     TUCAMRET_INVALID_VIEW       = 0x8000030C,       // invalid view window handle
    // 
    //     TUCAMRET_NO_VALUETEXT       = 0x80000310,       // the property does not have value text
    //     TUCAMRET_OUT_OF_RANGE       = 0x80000311,       // value is out of range
    // 
    //     TUCAMRET_NOT_SUPPORT        = 0x80000312,       // camera does not support the function or property with current settings
    //     TUCAMRET_NOT_WRITABLE       = 0x80000313,       // the property is not writable	
    //     TUCAMRET_NOT_READABLE       = 0x80000314,       // the property is not readable
    // 
    //              
    //     TUCAMRET_WRONG_HANDSHAKE    = 0x80000310,       // this error happens TUCAM get error code from camera unexpectedly
    //     TUCAMRET_NEWAPI_REQUIRED    = 0x80000311,       // old API does not support the value because only new API supports the value
    //  
    //     TUCAMRET_ACCESSDENY         = 0x80000312,       // the property cannot access during this TUCAM status
    // 
    //     TUCAMRET_NO_CORRECTIONDATA  = 0x80000501,       // not take the dark and shading correction data yet.
    // 
    //     //  camera or bus trouble
    //     TUCAMRET_FAIL_READ_CAMERA   = 0x83001001,       // fail read from camera  
    //     TUCAMRET_FAIL_WRITE_CAMERA  = 0x83001002,       // fail write to camera
    //     TUCAMRET_OPTICS_UNPLUGGED   = 0x83001003,       // optics part is unplugged so please check it.
};

//  typedef enum string id
enum TUCAM_IDINFO
{
    TUIDI_BUS = 0x01,             // the bus type USB2.0/USB3.0
    TUIDI_VENDOR = 0x02,             // the vendor id
    TUIDI_PRODUCT = 0x03,             // the product id 
    TUIDI_VERSION_API = 0x04,             // the API version    
    TUIDI_VERSION_FRMW = 0x05,             // the firmware version
    TUIDI_VERSION_FPGA = 0x06,             // the FPGA version
    TUIDI_VERSION_DRIVER = 0x07,             // the driver version
    TUIDI_TRANSFER_RATE = 0x08,             // the transfer rate
    TUIDI_CAMERA_MODEL = 0x09,             // the camera model (string)
    TUIDI_CURRENT_WIDTH = 0x0A,             // the camera image data current width(must use TUCAM_Dev_GetInfoEx and after calling TUCAM_Buf_Alloc)
    TUIDI_CURRENT_HEIGHT = 0x0B,             // the camera image data current height(must use TUCAM_Dev_GetInfoEx and after calling TUCAM_Buf_Alloc)
    TUIDI_CAMERA_CHANNELS = 0x0C,             // the camera image data channels
    TUIDI_BCDDEVICE = 0x0D,             // the USB bcdDevice
    TUIDI_ENDINFO = 0x0E,             // the string id end
};

// typedef enum capability id 
enum TUCAM_IDCAPA
{
    TUIDC_RESOLUTION = 0x00,             // id capability resolution
    TUIDC_PIXELCLOCK = 0x01,             // id capability pixel clock
    TUIDC_BITOFDEPTH = 0x02,             // id capability bit of depth
    TUIDC_ATEXPOSURE = 0x03,             // id capability automatic exposure time  
    TUIDC_HORIZONTAL = 0x04,             // id capability horizontal
    TUIDC_VERTICAL = 0x05,             // id capability vertical
    TUIDC_ATWBALANCE = 0x06,             // id capability automatic white balance
    TUIDC_FAN_GEAR = 0x07,             // id capability fan gear
    TUIDC_ATLEVELS = 0x08,             // id capability automatic levels
    TUIDC_SHIFT = 0x09,             // (The reserved) id capability shift(15~8, 14~7, 13~6, 12~5, 11~4, 10~3, 9~2, 8~1, 7~0) [16bit]
    TUIDC_HISTC = 0x0A,             // id capability histogram statistic
    TUIDC_CHANNELS = 0x0B,             // id capability current channels(Only color camera support:0-RGB,1-Red,2-Green,3-Blue. Used in the property levels, see enum TUCHN_SELECT)
    TUIDC_ENHANCE = 0x0C,             // id capability enhance
    TUIDC_DFTCORRECTION = 0x0D,             // id capability defect correction (0-not correction, 1-calculate, 3-correction)
    TUIDC_ENABLEDENOISE = 0x0E,             // id capability enable denoise (TUIDP_NOISELEVEL effective)
    TUIDC_FLTCORRECTION = 0x0F,             // id capability flat field correction (0-not correction, 1-grab frame, 2-calculate, 3-correction)
    TUIDC_RESTARTLONGTM = 0x10,             // id capability restart long exposure time (only CCD camera support)
    TUIDC_DATAFORMAT = 0x11,             // id capability the data format(only YUV format data support 0-YUV 1-RAW)
    TUIDC_DRCORRECTION = 0x12,             // (The reserved)id capability dynamic range of correction
    TUIDC_VERCORRECTION = 0x13,             // id capability vertical correction(correction the image data show vertical, in windows os the default value is 1)
    TUIDC_MONOCHROME = 0x14,             // id capability monochromatic
    TUIDC_BLACKBALANCE = 0x15,             // id capability black balance
    TUIDC_IMGMODESELECT = 0x16,             // id capability image mode select(CMS mode)
    TUIDC_CAM_MULTIPLE = 0x17,             // id capability multiple cameras (how many cameras use at the same time, only SCMOS camera support)
    TUIDC_ENABLEPOWEEFREQUENCY = 0x18,             // id capability enable power frequency (50HZ or 60HZ)
    TUIDC_ENDCAPABILITY = 0x19,             // id capability end 
};

// typedef enum property id
enum TUCAM_IDPROP
{
    TUIDP_GLOBALGAIN        = 0x00,             // id property global gain
    TUIDP_EXPOSURETM        = 0x01,             // id property exposure time
    TUIDP_BRIGHTNESS        = 0x02,             // id property brightness (Effective automatic exposure condition)
    TUIDP_BLACKLEVEL        = 0x03,             // id property black level
    TUIDP_TEMPERATURE       = 0x04,             // id capability temperature control
    TUIDP_SHARPNESS         = 0x05,             // id property sharpness
    TUIDP_NOISELEVEL        = 0x06,             // id property the noise level
    TUIDP_HDR_KVALUE        = 0x07,             // id property the HDR K value

    // image process property
    TUIDP_GAMMA             = 0x08,             // id property gamma
    TUIDP_CONTRAST          = 0x09,             // id property contrast
    TUIDP_LFTLEVELS         = 0x0A,             // id property left levels
    TUIDP_RGTLEVELS         = 0x0B,             // id property right levels
    TUIDP_CHNLGAIN          = 0x0C,             // id property channel gain
    TUIDP_SATURATION        = 0x0D,             // id property saturation
    TUIDP_CLRTEMPERATURE    = 0x0E,             // id property color temperature
    TUIDP_CLRMATRIX         = 0x0F,             // id property color matrix setting
    TUIDP_DPCLEVEL          = 0x10,             // id property defect points correction level
    TUIDP_BLACKLEVELHG      = 0x11,             // id property black level high gain
    TUIDP_BLACKLEVELLG      = 0x12,             // id property black level low gain
    TUIDP_POWEEFREQUENCY    = 0x13,             // id property power frequency (50HZ or 60HZ)
    TUIDP_HUE               = 0x14,				// id property hue
    TUIDP_LIGHT             = 0x15,				// id property light
    TUIDP_ENHANCE_STRENGTH  = 0x16,				// id property enhance strength
    TUIDP_ENDPROPERTY       = 0x17,             // id property end 
};

// typedef enum the capture mode
enum TUCAM_CAPTURE_MODES
{
    TUCCM_SEQUENCE = 0x00,             // capture start sequence mode
    TUCCM_TRIGGER_STANDARD = 0x01,             // capture start trigger standard mode
    TUCCM_TRIGGER_SYNCHRONOUS = 0x02,             // capture start trigger synchronous mode
    TUCCM_TRIGGER_GLOBAL = 0x03,             // capture start trigger global
    TUCCM_TRIGGER_SOFTWARE = 0x04,             // capture start trigger software
};

// typedef enum the image formats
enum TUIMG_FORMATS
{
    TUFMT_RAW = 0x01,               // The format RAW
    TUFMT_TIF = 0x02,               // The format TIFF
    TUFMT_PNG = 0x04,               // The format PNG
    TUFMT_JPG = 0x08,               // The format JPEG
    TUFMT_BMP = 0x10,               // The format BMP
};

// typedef enum the register formats
enum TUREG_FORMATS
{
    TUREG_SN = 0x01,              // The format register SN
    TUREG_DATA = 0x02,              // The format register DATA
};

// trigger mode
// typedef enum the trigger exposure time mode
enum TUCAM_TRIGGER_EXP
{
    TUCTE_EXPTM = 0x00,     // use width level
    TUCTE_WIDTH = 0x01,     // use exposure time 
};

// typedef enum the trigger edge mode
enum TUCAM_TRIGGER_EDGE
{
    TUCTD_RISING = 0x01,     // rising edge
    TUCTD_FAILING = 0x00,     // failing edge
};

// typedef enum the frame formats
enum TUFRM_FORMATS
{
    TUFRM_FMT_RAW = 0x10,         // The raw data
    TUFRM_FMT_USUAl = 0x11,         // The usually data
    TUFRM_FMT_RGB888 = 0x12,         // The RGB888 data for drawing
};

public struct TUCAM_INIT
{
    public UInt32 uiCamCount;                         // [out]
    public IntPtr pstrConfigPath;                    // [in] save the path of the camera parameters 
    //    const TUCAM_GUID*	guid;					// [in ptr]
};

// //  the camera open struct

public struct TUCAM_OPEN
{
    public UInt32 uiIdxOpen;                         // [in]
    public IntPtr hIdxTUCam;                         // [out]    
};
// 
// // the camera value text struct
public struct TUCAM_VALUE_INFO
{
    public int nID;                                // [in] TUCAM_IDINFO
    public int nValue;                             // [in] value of information
    public IntPtr pText;					            // [in/out] text of the value
    public int nTextSize;          				// [in] text buf size
};
// 
// // the camera value text struct
public struct TUCAM_VALUE_TEXT
{
    public int nID;                                // [in] TUCAM_IDPROP / TUCAM_IDCAPA
    public double dbValue;                            // [in] value of property
    public IntPtr pText;					            // [in/out] text of the value
    public int nTextSize;          				// [in] text buf size
};
// 
// // the camera capability attribute
public struct TUCAM_CAPA_ATTR
{
    public int idCapa;                             // [in] TUCAM_IDCAPA

    public int nValMin;                            // [out] minimum value
    public int nValMax;                            // [out] maximum value
    public int nValDft;                            // [out] default value
    public int nValStep;                           // [out] minimum stepping between a value and the next

};
// 
// // the camera property attribute
struct TUCAM_PROP_ATTR
{
    public int idProp;                             // [in] TUCAM_IDPROP
    public int nIdxChn;                            // [in/out] the index of channel

    public double dbValMin;                           // [out] minimum value
    public double dbValMax;                           // [out] maximum value
    public double dbValDft;                           // [out] default value
    public double dbValStep;                          // [out] minimum stepping between a value and the next

};
// 
// // the camera roi attribute
public struct TUCAM_ROI_ATTR
{
    public bool bEnable;                            // [in/out] The ROI enable

    public int nHOffset;                           // [in/out] The horizontal offset
    public int nVOffset;                           // [in/out] The vertical offset
    public int nWidth;                             // [in/out] The ROI width
    public int nHeight;                            // [in/out] The ROI height
};

public struct TUCAM_FRAME
{
    // TUCAM_Buf_WaitForFrame() use this structure. Some members have different direction.
    // [i:o] means, the member is input at TUCAM_Buf_WaitForFrame()
    // [i:i] and [o:o] means always input and output at both function.
    // "input" means application has to set the value before calling.
    // "output" means function fills a value at returning.

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public byte[] szSignature;    // [out]Copyright+Version: TU+1.0 ['T', 'U', '1', '\0']

    //  The based information
    public ushort usHeader;        // [out] The frame header size
    public ushort usOffset;        // [out] The frame data offset
    public ushort usWidth;         // [out] The frame width
    public ushort usHeight;        // [out] The frame height
    public UInt32 uiWidthStep;     // [out] The frame width step

    public byte ucDepth;         // [out] The frame data depth 
    public byte ucFormat;        // [out] The frame data format                  
    public byte ucChannels;      // [out] The frame data channels
    public byte ucElemBytes;     // [out] The frame data bytes per element
    public byte ucFormatGet;     // [in]  Which frame data format do you want    see TUFRM_FORMATS

    public UInt32 uiIndex;         // [in/out] The frame index number
    public UInt32 uiImgSize;       // [out] The frame size
    public UInt32 uiRsdSize;       // [in]  The frame reserved size    (how many frames do you want)
    public UInt32 uiHstSize;       // [out] The frame histogram size

    public IntPtr pBuffer;
};
// 
// // the camera trigger attribute
public struct TUCAM_TRIGGER_ATTR
{
    public int nTgrMode;                           // [in/out] The mode of trigger 
    public int nExpMode;                           // [in/out] The mode of exposure [0, 1] 0:Width level   1:Exposure time 
    public int nEdgeMode;                          // [in/out] The mode of edge     [0, 1] 0:Rising edge   1:Falling edge
    public int nDelayTm;                           // [in/out] The time delay
    public int nFrames;                            // [in/out] How many frames per trigger
};
// 
// // the file save struct
struct TUCAM_FILE_SAVE
{
    public int nSaveFmt;               // [in] the format of save file     see TUIMG_FORMATS
    public IntPtr pstrSavePath;           // [in] the path of save file 

    public IntPtr pFrame;            // [in] the struct of camera frame
    //public TUCAM_FRAME pFrame;
};
// 
// // the register read/write struct
struct TUCAM_REG_RW
{
    public int nRegType;                // [in] the format of register     see TUREG_FORMATS

    public IntPtr pBuf;					// [in/out] pointer to the buffer value
    public int nBufSize;          	    // [in] the buffer size
};

// typedef struct drawing
public struct TUCAM_DRAW 
{
    public int nSrcX;                  // [in/out] The x-coordinate, in pixels, of the upper left corner of the source rectangle.
    public int nSrcY;                  // [in/out] The y-coordinate, in pixels, of the upper left corner of the source rectangle.
    public int nSrcWidth;              // [in/out] Width,  in pixels, of the source rectangle.
    public int nSrcHeight;             // [in/out] Height, in pixels, of the source rectangle.

    public int nDstX;                  // [in/out] The x-coordinate, in MM_TEXT client coordinates, of the upper left corner of the destination rectangle.
    public int nDstY;                  // [in/out] The y-coordinate, in MM_TEXT client coordinates, of the upper left corner of the destination rectangle.
    public int nDstWidth;              // [in/out] Width,  in MM_TEXT client coordinates, of the destination rectangle.
    public int nDstHeight;             // [in/out] Height, in MM_TEXT client coordinates, of the destination rectangle.

    public IntPtr pFrame;            // [in] the struct of camera frame
};

// typedef struct draw init
public struct TUCAM_DRAW_INIT 
{

    public HANDLE hWnd;

    public int nMode;                  // [in] (The reserved)Whether use hardware acceleration (If the GPU support) default:TUDRAW_DFT
    public sbyte ucChannels;           // [in] The data channels
    public int nWidth;                 // [in] The drawing data width
    public int nHeight;                // [in] The drawing data height
};

//BitmapInfoHeader定义了位图的头部信息
[StructLayout(LayoutKind.Sequential)]
public struct BITMAPINFOHEADER
{
    public int biSize;
    public int biWidth;
    public int biHeight;
    public short biPlanes;
    public short biBitCount;
    public int biCompression;
    public int biSizeImage;
    public int biXPelsPerMeter;
    public int biYPelsPerMeter;
    public int biClrUsed;
    public int biClrImportant;
}

//BitmapInfo   位图信息
[StructLayout(LayoutKind.Sequential)]
public struct BITMAPINFO
{
    public BITMAPINFOHEADER bmiHeader;
    public int bmiColors;
}

namespace TUCAMERA
{
    class TUCamera
    {
        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Api_Init( ref TUCAM_INIT pInitParam);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Api_Uninit();

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Dev_Open( ref TUCAM_OPEN pOpenParam);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Dev_Close(IntPtr hTUCam);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Dev_GetInfo(IntPtr hTUCam, ref TUCAM_VALUE_INFO pInfo);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern TUCAMRET TUCAM_Dev_GetInfoEx(UInt32 uiICam, ref TUCAM_VALUE_INFO pInfo);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Capa_GetAttr(IntPtr hTUCam, ref TUCAM_CAPA_ATTR pAttr);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Capa_GetValue(IntPtr hTUCam, int nCapa, ref int pnVal);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Capa_SetValue(IntPtr hTUCam, int nCapa, int nVal);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Capa_GetValueText(IntPtr hTUCam, ref TUCAM_VALUE_TEXT pVal);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Prop_GetAttr(IntPtr hTUCam, ref TUCAM_PROP_ATTR pAttr);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Prop_GetValue(IntPtr hTUCam, int nProp, ref double pdbVal, int nChn);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Prop_SetValue(IntPtr hTUCam, int nProp, double dbval, int nChn);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Prop_GetValueText(IntPtr hTUCam, ref TUCAM_VALUE_TEXT pVal, int nChn);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Buf_Alloc(IntPtr hTUCam, ref TUCAM_FRAME pFrame);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Buf_Release(IntPtr hTUCam);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Buf_AbortWait(IntPtr hTUCam);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Buf_WaitForFrame(IntPtr hTUCam, ref TUCAM_FRAME pFrame);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Buf_CopyFrame(IntPtr hTUCam, ref TUCAM_FRAME pFrame);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Cap_SetROI(IntPtr hTUCam, TUCAM_ROI_ATTR roiAttr);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Cap_GetROI(IntPtr hTUCam, ref TUCAM_ROI_ATTR roiAttr);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Cap_SetTrigger(IntPtr hTUCam, TUCAM_TRIGGER_ATTR tgrAttr);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Cap_GetTrigger(IntPtr hTUCam, ref TUCAM_TRIGGER_ATTR tgrAttr);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Cap_DoSoftwareTrigger(IntPtr hTUCam);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Cap_Start(IntPtr hTUCam, UInt32 uiMode);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
         public static extern TUCAMRET TUCAM_Cap_Stop(IntPtr hTUCam);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern TUCAMRET TUCAM_File_SaveImage(IntPtr hTUCam, TUCAM_FILE_SAVE fileSave);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern TUCAMRET TUCAM_Reg_Read(IntPtr hTUCam, TUCAM_REG_RW regRW);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern TUCAMRET TUCAM_Reg_Write(IntPtr hTUCam, TUCAM_REG_RW regRW);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern TUCAMRET TUCAM_Draw_Frame(IntPtr hTUCam, ref TUCAM_DRAW dFrame);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern TUCAMRET TUCAM_Draw_Init(IntPtr hTUCam, TUCAM_DRAW_INIT dFrame);

        [DllImport("TUCam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern TUCAMRET TUCAM_Draw_Uninit(IntPtr hTUCam);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto)]
        public static extern HANDLE CreateEvent(HANDLE lpEventAttributes, [In, MarshalAs(UnmanagedType.Bool)] bool bManualReset, [In, MarshalAs(UnmanagedType.Bool)] bool bIntialState, [In, MarshalAs(UnmanagedType.BStr)] string lpName);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(HANDLE hObject);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto)]
        public static extern void OutputDebugString(string message);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);

        [DllImport("kernel32")]
        public static extern uint GetTickCount();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

        //[DllImport("MSVFW32.dll")]
        //public static extern bool DrawDibDraw(IntPtr hdd, IntPtr hdc, int xDst, int yDst, int dxDst, int dyDst, ref BITMAPINFOHEADER lpbi, byte[] lpBits, int xSrc, int ySrc, int dxSrc, int dySrc, uint wFlags);
 
        [DllImport("MSVFW32.dll")]
        public static extern IntPtr DrawDibOpen();

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr Hwnd);

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        /// <summary>
        /// 自定义的结构
        /// </summary>
        public struct My_lParam
        {
            public int i;
            public string s;
        }
        /// <summary>
        /// 使用COPYDATASTRUCT来传递字符串
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }

        //消息发送API
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(
            IntPtr hWnd,        // 信息发往的窗口的句柄
            int Msg,            // 消息ID
            int wParam,         // 参数1
            int lParam            // 参数2
        );
        
        //消息发送API
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(
            IntPtr hWnd,        // 信息发往的窗口的句柄
            int Msg,            // 消息ID
            int wParam,         // 参数1
            ref My_lParam lParam //参数2
        );
        
        //异步消息发送API
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(
            IntPtr hWnd,        // 信息发往的窗口的句柄
            int Msg,            // 消息ID
            int wParam,         // 参数1
            ref  COPYDATASTRUCT lParam  // 参数2
        );
    }
}
