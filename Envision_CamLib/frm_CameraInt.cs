using ComponentFactory.Krypton.Toolkit;
using DVPCameraType;
using Envision.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Envision_CamLib
{
    public partial class frm_CameraInt : KryptonForm
    {
        public uint m_handle = 0;
        public bool m_bAeOp = false;
        public int m_n_dev_count = 0;
        string m_strFriendlyName;

        public static IntPtr m_ptr_wnd = new IntPtr();
        public static IntPtr m_ptr = new IntPtr();
        public static bool m_b_start = false;

        public string _applicationPath = string.Empty;

        public string _imageLocation = string.Empty;
        private Boolean b_isIDSPeak = false;
        public bool _isWeldMet = false;


        public delegate void LoadImageEvent();
        public LoadImageEvent _loadRefreshimages;

        public string _jobID = string.Empty;

        public frm_CameraInt()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            m_ptr_wnd = pictureBox.Handle;

            System.Timers.Timer t = new System.Timers.Timer(500);

            // Execute the event when the time has arrived. 
            t.Elapsed += new System.Timers.ElapsedEventHandler(theout);

            // Set the method of executing
            t.AutoReset = true;

            // Judge whether execute the System.Timers.Timer.Elapsed event.
            t.Enabled = true;
            InitDevList();
        }


        public void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            if (IsValidHandle(m_handle))
            {
                // Update the information of frame rate
                dvpFrameCount count = new dvpFrameCount();
                dvpStatus status = DVPCamera.dvpGetFrameCount(m_handle, ref count);
                //Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                string str = m_strFriendlyName + " [" + count.uFrameCount.ToString() + " frames, " + string.Format("{0:#0.00}", count.fFrameRate) + " fps]";
                this.Text = str;
            }
        }

        public bool IsValidHandle(uint handle)
        {
            bool bValidHandle = false;
            dvpStatus status = DVPCamera.dvpIsValid(handle, ref bValidHandle);
            if (status == dvpStatus.DVP_STATUS_OK)
            {
                return bValidHandle;
            }
            return false;
        }


        private DVPCamera.dvpStreamCallback _proc;

        // Callback function that is used for receiving data.
        public static int _dvpStreamCallback(/*dvpHandle*/uint handle, dvpStreamEvent _event, /*void **/IntPtr pContext, ref dvpFrame refFrame, /*void **/IntPtr pBuffer)
        {
            // Refresh images' display.
            dvpStatus status = DVPCamera.dvpDrawPicture(ref refFrame, pBuffer, m_ptr_wnd, m_ptr, m_ptr);
            //Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
            return 1;
        }

        private void frm_CameraInt_Load(object sender, EventArgs e)
        {
            COMBO_DEVICES.Text = cls_EnvisionConfig.ReadDetailsFromXML(_applicationPath, @"/configurations/model");


            // Check if the Camera is IDS Peak Camera
            if (COMBO_DEVICES.Text != String.Empty && COMBO_DEVICES.Text != "IDS Peak")
            {
                b_isIDSPeak = false;
            }
            else
                b_isIDSPeak = true;


            if (!b_isIDSPeak)
            {
                InitDevList();
                dvpStatus status;
                if (!IsValidHandle(m_handle))
                {
                    uint i = (uint)COMBO_DEVICES.SelectedIndex;
                    status = DVPCamera.dvpOpen(i, dvpOpenMode.OPEN_NORMAL, ref m_handle);
                    if (status != dvpStatus.DVP_STATUS_OK)
                    {
                        m_handle = 0;
                        MessageBox.Show("Open the device failed!");
                    }
                    else
                    {
                        m_strFriendlyName = COMBO_DEVICES.Text;
                        _proc = _dvpStreamCallback;
                        using (Process curProcess = Process.GetCurrentProcess())
                        using (ProcessModule curModule = curProcess.MainModule)
                        {
                            status = DVPCamera.dvpRegisterStreamCallback(m_handle, _proc, dvpStreamEvent.STREAM_EVENT_PROCESSED, m_ptr);
                            //Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                        }
                    }
                }
                else
                {
                    DVPCamera.dvpStop(m_handle);
                    m_b_start = false;
                    status = DVPCamera.dvpClose(m_handle);
                    //Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                    m_handle = 0;
                    BUTTON_SAVE.Enabled = false;
                    pictureBox.Refresh();
                }

                UpdateControls();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void UpdateControls()
        {
            dvpStatus status = dvpStatus.DVP_STATUS_UNKNOW;
            if (IsValidHandle(m_handle))
            {
                // The device has been opened at this time.
                // Update the basic controls.
                dvpStreamState state = new dvpStreamState();
                status = DVPCamera.dvpGetStreamState(m_handle, ref state);
                //Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                BUTTON_START.Text = (state == dvpStreamState.STATE_STARTED ? ("Stop") : ("Play"));
                BUTTON_OPEN.Text = "Close";
                BUTTON_START.Enabled = true;
                BUTTON_PROPERTY.Enabled = true;
            }
            else
            {
                // No device is opened at this time.
                // Update the basic controls.
                BUTTON_OPEN.Text = "Open";
                BUTTON_START.Enabled = false;
                BUTTON_PROPERTY.Enabled = false;
                BUTTON_SAVE.Enabled = false;

                BUTTON_OPEN.Enabled = m_n_dev_count == 0 ? false : true;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void InitDevList()
        {
            dvpStatus status;
            uint i, n = 0;
            dvpCameraInfo dev_info = new dvpCameraInfo();

            // "n" represent the number of cameras that is enumerated successfully, the drop-down list contains each camera's FriendlyName.
            COMBO_DEVICES.Items.Clear();

            // Get the number of cameras that has been connected to a computer.
            status = DVPCamera.dvpRefresh(ref n);
            //Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
            m_n_dev_count = (int)n;
            if (status == dvpStatus.DVP_STATUS_OK)
            {
                for (i = 0; i < n; i++)
                {
                    // Acquire each camera's information one by one.
                    status = DVPCamera.dvpEnum(i, ref dev_info);

                    if (status == dvpStatus.DVP_STATUS_OK)
                    {
                        // GUI need UNICODE,but the information acquired from cameras is ANSI,so convert the character set from ANSI to UNICODE.
                        int item = COMBO_DEVICES.Items.Add(dev_info.FriendlyName);
                        if (item == 0)
                        {
                            COMBO_DEVICES.SelectedIndex = item;
                        }
                    }
                    else
                    {
                        //Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("Error Loading Cameras with Status" + status.ToString());
            }

            if (n == 0)
            {
                BUTTON_OPEN.Enabled = false;
            }
            else
            {
                BUTTON_OPEN.Enabled = true;
            }

            if (!IsValidHandle(m_handle))
            {
                BUTTON_START.Enabled = false;
                BUTTON_PROPERTY.Enabled = false;
                BUTTON_SAVE.Enabled = false;
            }
        }

        private void frm_CameraInt_Resize(object sender, EventArgs e)
        {
            ResizeWindows();
        }

        private void ResizeWindows()
        {
            if (IsValidHandle(m_handle))
            {
                dvpRegion roi;
                roi = new dvpRegion();
                dvpStatus status;
                status = DVPCamera.dvpGetRoi(m_handle, ref roi);
                //Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                pictureBox.Width = this.Width - pictureBox.Left;
                pictureBox.Height = this.Height - pictureBox.Top;

                if (pictureBox.Width * roi.H > pictureBox.Height * roi.W)
                {
                    pictureBox.Width = pictureBox.Height * roi.W / roi.H;
                }
                else
                {
                    pictureBox.Height = pictureBox.Width * roi.H / roi.W;
                }
            }
        }

        private void BUTTON_START_Click(object sender, EventArgs e)
        {
            dvpStreamState state = dvpStreamState.STATE_STOPED;
            dvpStatus status = dvpStatus.DVP_STATUS_UNKNOW;

            if (IsValidHandle(m_handle))
            {
                // Implement a button to start and stop according to the current video's status.
                status = DVPCamera.dvpGetStreamState(m_handle, ref state);
                //Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                if (state == dvpStreamState.STATE_STARTED)
                {
                    status = DVPCamera.dvpStop(m_handle);
                    m_b_start = status == dvpStatus.DVP_STATUS_OK ? false : true;
                    BUTTON_SAVE.Enabled = false;

                }
                else
                {
                    status = DVPCamera.dvpStart(m_handle);
                    //Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                    m_b_start = status == dvpStatus.DVP_STATUS_OK ? true : false;
                    BUTTON_SAVE.Enabled = true;
                }
            }
            UpdateControls();
        }

        private void BUTTON_PROPERTY_Click(object sender, EventArgs e)
        {
            if (IsValidHandle(m_handle))
            {
                dvpStatus status = DVPCamera.dvpShowPropertyModalDialog(m_handle, this.Handle);

                // At this time some configurations may change, synchronize it to the window GUI.
                UpdateControls();
            }
        }

        private void BUTTON_SAVE_Click(object sender, EventArgs e)
        {
            if (!m_b_start)
                return;

            string file_path = string.Empty;

            //if (_isWeldMet)
            //{
            //    SaveFileDialog sfd = new SaveFileDialog();
            //    sfd.Filter = "bmp(*.bmp)|*.bmp|jpeg(*.jpeg)|*.jpeg|png(*.png)|*.png";
            //    if (sfd.ShowDialog() == DialogResult.OK)
            //    {
            //        file_path = sfd.FileName;
            //    }
            //}
            //else
            
            Random rn = new Random();
            string rnd = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff");
            if (_isWeldMet)
            {
                file_path = _imageLocation  + "\\Images\\" + _jobID + "_" + "x" + "_" + rnd + ".bmp";
            }
            else
            { 
                file_path = _imageLocation + "\\" + "IM_IMG_X" + "_" + rnd + ".bmp";
            }

            if (!b_isIDSPeak)
            {
                IntPtr buffer = new IntPtr();
                dvpFrame frame = new dvpFrame();
                dvpStatus status;

                // Grab a frame image from the video stream within 5000 ms.
                status = DVPCamera.dvpGetFrame(m_handle, ref frame, ref buffer, 5000);
                if (status == dvpStatus.DVP_STATUS_OK)
                {
                    // Save the image as picture file.
                    status = DVPCamera.dvpSavePicture(ref frame, buffer, file_path, 100);
                }
                else if (status == dvpStatus.DVP_STATUS_TIME_OUT)
                {
                    MessageBox.Show(("Acquire image data timeout!"));
                }
                else
                {
                    MessageBox.Show(("Acquire image data error!"));
                }
            }

            if (_loadRefreshimages != null )
            {
                _loadRefreshimages();
            }
        }

        private void frm_CameraInt_FormClosing(object sender, FormClosingEventArgs e)
        {
            dvpStatus status;
            dvpStreamState state = new dvpStreamState();

            if (IsValidHandle(m_handle))
            {
                status = DVPCamera.dvpGetStreamState(m_handle, ref state);
                // Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                if (state == dvpStreamState.STATE_STARTED)
                {
                    status = DVPCamera.dvpStop(m_handle);
                    // Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                }
                status = DVPCamera.dvpClose(m_handle);
                // Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                m_handle = 0;
            }
        }
    }
}
