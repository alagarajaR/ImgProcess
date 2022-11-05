using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Envision.Common;

namespace Envision_CamLib
{
    public partial class frm_uEyeInit : KryptonForm
    {
        public bool m_bAeOp = false;
        public int m_n_dev_count = 0;


        public string _applicationPath = string.Empty;

        public string _imageLocation = string.Empty;
        private Boolean b_isIDSPeak = false;
        public bool _isWeldMet = false;


        public delegate void LoadImageEvent();
        public LoadImageEvent _loadRefreshimages;

        public string _jobID = string.Empty;



        uEye.Camera m_Camera;
        private uint m_u32ImageWidth = 0;
        private uint m_u32ImageHeight = 0;

        // overlay moving
        private Int32 m_s32MovePosX = 1;
        private Int32 m_s32MovePosY = 1;

        // overlay position
        private UInt32 m_u32OverlayPositionX = 0;
        private UInt32 m_u32OverlayPositionY = 0;

        private System.Drawing.Color m_OverlayColor = System.Drawing.Color.Black;

        // overlay moving update timer
        Timer m_OverlayMoveTimer = new Timer();

        // DirectRendererOverlay
        uEye.DirectRendererOverlay m_Overlay = null;
        public frm_uEyeInit()
        {
            InitializeComponent();
        }

        private void chkb_Properties_CheckedChanged(object sender, EventArgs e)
        {
            splt_Container.Panel2Collapsed = !chkb_CameraSettings.Checked;
        }

        private void frm_uEyeInit_Shown(object sender, EventArgs e)
        {
            bool bDirect3D = false;
            bool bOpenGL = false;


            uEye.Defines.Status statusRet;
            m_Camera = new uEye.Camera();
            m_Overlay = new uEye.DirectRendererOverlay(m_Camera);

            cB_Semi_transparent.Enabled = false;

            // open first available camera
            statusRet = m_Camera.Init(0, DisplayWindow.Handle.ToInt32());
            if (statusRet == uEye.Defines.Status.SUCCESS)
            {
                m_OverlayMoveTimer.Interval = 10;
                m_OverlayMoveTimer.Tick += new EventHandler(OnOverlayMove);

                statusRet = m_Camera.PixelFormat.Set(uEye.Defines.ColorMode.BGR8Packed);

                uEye.Defines.DisplayMode supportedMode;
                statusRet = m_Camera.DirectRenderer.GetSupported(out supportedMode);

                if ((supportedMode & uEye.Defines.DisplayMode.Direct3D) == uEye.Defines.DisplayMode.Direct3D)
                {
                    rB_Direct3D.Enabled = true;
                    bDirect3D = true;
                }
                else
                {
                    rB_Direct3D.Enabled = false;
                    bDirect3D = false;
                }

                if ((supportedMode & uEye.Defines.DisplayMode.OpenGL) == uEye.Defines.DisplayMode.OpenGL)
                {
                    rB_OpenGL.Enabled = true;
                    bOpenGL = true;

                    if (rB_Direct3D.Enabled != true)
                    {
                        rB_OpenGL.Checked = true;
                    }
                }
                else
                {
                    rB_OpenGL.Enabled = false;
                    bOpenGL = false;
                }

                if (((supportedMode & uEye.Defines.DisplayMode.Direct3D) == uEye.Defines.DisplayMode.Direct3D) ||
                    ((supportedMode & uEye.Defines.DisplayMode.OpenGL) == uEye.Defines.DisplayMode.OpenGL))
                {

                    if (bOpenGL == true)
                    {
                        // set display mode
                        statusRet = m_Camera.Display.Mode.Set(uEye.Defines.DisplayMode.OpenGL);
                    }

                    if (bDirect3D == true)
                    {
                        // set display mode
                        statusRet = m_Camera.Display.Mode.Set(uEye.Defines.DisplayMode.Direct3D);
                    }

                    // start live
                    BUTTON_START_Click(null, EventArgs.Empty);

                    // update information
                    UpdateOverlayInformation();
                    UpdateImageInformation();

                }
                else
                {
                    MessageBox.Show("Direct3D and OpenGL are not supported");
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Could not open camera...");
                //Close();
            }

            
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateOverlayInformation()
        {
            uEye.Types.Size<UInt32> overlaySize;
            uEye.Defines.Status statusRet;

            statusRet = m_Overlay.GetSize(out overlaySize);

            tB_Overlay_Width.Text = overlaySize.Width.ToString();
            tB_Overlay_Height.Text = overlaySize.Height.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateImageInformation()
        {
            /* open the camera */
            System.Drawing.Rectangle imageRect;
            uEye.Defines.Status statusRet;

            statusRet = m_Camera.Size.AOI.Get(out imageRect);

            m_u32ImageWidth = (uint)imageRect.Width;
            m_u32ImageHeight = (uint)imageRect.Height;

            /* Image Info*/
            tB_Image_Width.Text = imageRect.Width.ToString();
            tB_Image_Height.Text = imageRect.Height.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnOverlayMove(object source, EventArgs e)
        {
            m_Overlay.SetPosition(m_u32OverlayPositionX, m_u32OverlayPositionY);
            m_Overlay.Show();

            // force display update
            if (BUTTON_START.Enabled)
            {
                m_Camera.DirectRenderer.Update();
            }

            if (m_u32OverlayPositionX > this.m_u32ImageWidth)
            {
                m_s32MovePosX = -1;
            }

            if (m_u32OverlayPositionY > this.m_u32ImageHeight)
            {
                m_s32MovePosY = -1;
            }

            if (m_u32OverlayPositionX == 0)
            {
                m_s32MovePosX = 1;
            }

            if (m_u32OverlayPositionY == 0)
            {
                m_s32MovePosY = 1;
            }

            m_u32OverlayPositionX = (uint)((int)m_u32OverlayPositionX + m_s32MovePosX);
            m_u32OverlayPositionY = (uint)((int)m_u32OverlayPositionY + m_s32MovePosY);
        }

        private void BUTTON_START_Click(object sender, EventArgs e)
        {
            /* Start the camera acquisition */
            uEye.Defines.Status statusRet;
            statusRet = m_Camera.Acquisition.Capture();

            /* active keys Strart Live and Stop Live */
            BUTTON_START.Enabled = false;
            BUTTON_STOP.Enabled = true;
        }

        private void BUTTON_STOP_Click(object sender, EventArgs e)
        {
            /* Stop the camera acquisition */
            uEye.Defines.Status statusRet;
            statusRet = m_Camera.Acquisition.Stop();

            /* active keys Strart Live and Stop Live */
            BUTTON_START.Enabled = true;
            BUTTON_STOP.Enabled = false;
        }

        private void BUTTON_SAVE_Click(object sender, EventArgs e)
        {
            if (!BUTTON_START.Enabled)
                return;

            string file_path = string.Empty;

            Random rn = new Random();
            string rnd = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff");
            if (_isWeldMet)
            {
                file_path = _imageLocation + "\\Images\\" + _jobID + "_" + "x" + "_" + rnd + ".bmp";
            }
            else
            {
                file_path = _imageLocation + "\\" + "IM_IMG_X" + "_" + rnd + ".bmp";
            }

            if (!b_isIDSPeak)
            {
               
            }

            if (_loadRefreshimages != null)
            {
                _loadRefreshimages();
            }
        }

        private void frm_uEyeInit_Load(object sender, EventArgs e)
        {
            splt_Container.Panel2Collapsed = !chkb_CameraSettings.Checked;
        }
    }
}
