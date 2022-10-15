using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krypton.Toolkit;
using DVPCameraType;
using System.Diagnostics;
using Envision.Common;

namespace ImgProcess
{
    public partial class frm_Camera : KryptonForm
    {
        public uint m_handle = 0;
        public bool m_bAeOp = false;
        public int m_n_dev_count = 0;
        string m_strFriendlyName;

        public static IntPtr m_ptr_wnd = new IntPtr();
        public static IntPtr m_ptr = new IntPtr();
        public static bool m_b_start = false;

        string m_model = string.Empty;


        public string imageLocation = string.Empty;

        private Boolean b_isIDSPeak = false;

        public frm_Camera()
        {
            InitializeComponent();
            kryptonPalette1.BasePaletteMode = com.kp;
            m_ptr_wnd = pictureBox.Handle;



            GainEdit.DecimalPlaces = 3;
            GainEdit.Increment = 0.125M;

            nud_RGain.DecimalPlaces = 3;
            nud_RGain.Increment = 0.125M;

            nud_GGain.DecimalPlaces = 3;
            nud_GGain.Increment = 0.125M;

            nud_BGain.DecimalPlaces = 3;
            nud_BGain.Increment = 0.125M;


            System.Timers.Timer t = new System.Timers.Timer(500);

            // Execute the event when the time has arrived. 
            t.Elapsed += new System.Timers.ElapsedEventHandler(theout);

            // Set the method of executing
            t.AutoReset = true;

            // Judge whether execute the System.Timers.Timer.Elapsed event.
            t.Enabled = true;
            
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
            Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
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
                        Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                    }
                }
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
                CHECK_AEOPERATION.Enabled = false;
                COMBO_AE_MODE.Enabled = false;
                ExposureTimeEdit.Enabled = false;
                ExposureTimeApply.Enabled = false;
                //COMBO_FLICK.Enabled = false;
                GainEdit.Enabled = false;
                GainApply.Enabled = false;
                COMBO_RESOLUTION.Enabled = false;
            }
            else
            {
                ExposureTimeEdit.Enabled = true;
                ExposureTimeApply.Enabled = true;
                GainEdit.Enabled = true;
                GainApply.Enabled = true;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            if (IsValidHandle(m_handle))
            {
                // Update the information of frame rate
                dvpFrameCount count = new dvpFrameCount();
                dvpStatus status = DVPCamera.dvpGetFrameCount(m_handle, ref count);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                string str = m_strFriendlyName + " [" + count.uFrameCount.ToString() + " frames, " + string.Format("{0:#0.00}", count.fFrameRate) + " fps]";
                this.Text = str;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
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
            Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
            return 1;
        }

        private void BUTTON_SCAN_Click(object sender, EventArgs e)
        {
            InitDevList();
        }

        private void BUTTON_OPEN_Click(object sender, EventArgs e)
        {
            if (!b_isIDSPeak)
            {
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
                            Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                        }

                        InitControlResolution();
                        InitControlAeMode();
                        //InitControlFlick();
                        UpdateControlExposure();
                        UpdateControlGain();
                    }
                }
                else
                {
                    DVPCamera.dvpStop(m_handle);
                    m_b_start = false;
                    status = DVPCamera.dvpClose(m_handle);
                    Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
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
        public void InitControlResolution()
        {
            uint RoiSel = 0;
            dvpQuickRoi RoiDetail = new dvpQuickRoi();
            dvpStatus status;
            dvpSelectionDescr RoiDescr = new dvpSelectionDescr();
            COMBO_RESOLUTION.Items.Clear();

            if (!IsValidHandle(m_handle))
            {
                COMBO_RESOLUTION.Enabled = false;
                return;
            }

            // Get the index of the ROI.
            status = DVPCamera.dvpGetQuickRoiSelDescr(m_handle, ref RoiDescr);
            if (status == dvpStatus.DVP_STATUS_OK)
            {
                for (uint iNum = 0; iNum < RoiDescr.uCount; iNum++)
                {
                    status = DVPCamera.dvpGetQuickRoiSelDetail(m_handle, iNum, ref RoiDetail);
                    if (status == dvpStatus.DVP_STATUS_OK)
                    {
                        COMBO_RESOLUTION.Items.Add(RoiDetail.selection._string);
                    }
                }
            }
            else
            {
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
            }

            // Get the index of the ROI.
            status = DVPCamera.dvpGetQuickRoiSel(m_handle, ref RoiSel);
            Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
            if (status == dvpStatus.DVP_STATUS_OK)
            {
                COMBO_RESOLUTION.SelectedIndex = (int)RoiSel;
            }
            COMBO_RESOLUTION.Enabled = true;
        }


        /// <summary>
        /// 
        /// </summary>
        public void InitControlAeMode()
        {
            COMBO_AE_MODE.Items.Clear();
            if (!IsValidHandle(m_handle))
            {
                COMBO_AE_MODE.Enabled = false;
                return;
            }

            COMBO_AE_MODE.Items.Add("AE_MODE_AE_AG");
            COMBO_AE_MODE.Items.Add("AE_MODE_AG_AE");
            COMBO_AE_MODE.Items.Add("AE_MODE_AE_ONLY");
            COMBO_AE_MODE.Items.Add("AE_MODE_AG_ONLY");
            COMBO_AE_MODE.SelectedIndex = 0;

            COMBO_AE_MODE.Enabled = true;
        }


        /// <summary>
        /// 
        /// </summary>
        public void UpdateControlExposure()
        {
            double fExpoTime = 0.0f;
            dvpDoubleDescr ExpoTimeDescr = new dvpDoubleDescr(); ;
            dvpStatus status;

            // Get the descriptive information about the exposure time.
            status = DVPCamera.dvpGetExposureDescr(m_handle, ref ExpoTimeDescr);
            if (status == dvpStatus.DVP_STATUS_OK)
            {
                // Set the range of the exposure time.
                ExposureTimeEdit.Maximum = (decimal)ExpoTimeDescr.fMax;
                ExposureTimeEdit.Minimum = (decimal)ExpoTimeDescr.fMin;

            }
            else
            {
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
            }

            // Get the initial value of the exposure time.
            status = DVPCamera.dvpGetExposure(m_handle, ref fExpoTime);
            Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
            if (status == dvpStatus.DVP_STATUS_OK)
            {
                // Set the initial value of the exposure time. 
                ExposureTimeEdit.Value = (decimal)fExpoTime;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void UpdateControlGain()
        {
            dvpStatus status;
            float fAnalogGain = 0.0F;
            dvpFloatDescr AnalogGainDescr = new dvpFloatDescr();

            // Get the descriptive information about the analog gain.
            status = DVPCamera.dvpGetAnalogGainDescr(m_handle, ref AnalogGainDescr);
            Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
            if (status == dvpStatus.DVP_STATUS_OK)
            {
                // Set the range of the analog gain.
                GainEdit.Maximum = (decimal)AnalogGainDescr.fMax;
                GainEdit.Minimum = (decimal)AnalogGainDescr.fMin;
            }



            // Get the value of the analog gain.
            status = DVPCamera.dvpGetAnalogGain(m_handle, ref fAnalogGain);
            Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
            if (status == dvpStatus.DVP_STATUS_OK)
            {
                // Set the initial value of the analog gain.
                GainEdit.Value = (decimal)(fAnalogGain);
            }

            // Get Value of RGB Gain
            float rGain, gGain, bGain;
            rGain = gGain = bGain = 0.0F;

            status = DVPCamera.dvpSetRgbGain(m_handle, rGain, gGain, bGain);

            Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
            if (status == dvpStatus.DVP_STATUS_OK)
            {
                // Set the initial value of the analog gain.
                nud_RGain.Value = (decimal)rGain;
                nud_GGain.Value = (decimal)gGain;
                nud_BGain.Value = (decimal)bGain;
                //GainEdit.Value = (decimal)(fAnalogGain);
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
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                BUTTON_START.Text = (state == dvpStreamState.STATE_STARTED ? ("Stop") : ("Play"));
                BUTTON_OPEN.Text = "Close";
                BUTTON_START.Enabled = true;
                BUTTON_PROPERTY.Enabled = true;


                // Update the related controls.
                COMBO_RESOLUTION.Enabled = true;
                COMBO_AE_MODE.Enabled = true;

                CHECK_AEOPERATION.Enabled = true;

                // Update the AE operation control.
                dvpAeOperation AeOp = new dvpAeOperation();
                dvpAeMode AeMode = new dvpAeMode();

                status = DVPCamera.dvpGetAeOperation(m_handle, ref AeOp);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                status = DVPCamera.dvpGetAeMode(m_handle, ref AeMode);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                if (AeOp == dvpAeOperation.AE_OP_OFF)
                {
                    m_bAeOp = false;
                    ExposureTimeEdit.Enabled = true;
                    ExposureTimeApply.Enabled = true;
                    GainEdit.Enabled = true;
                    GainApply.Enabled = true;
                }
                else if (AeOp == dvpAeOperation.AE_OP_CONTINUOUS || AeOp == dvpAeOperation.AE_OP_ONCE)
                {
                    m_bAeOp = true;
                    ExposureTimeEdit.Enabled = AeMode == dvpAeMode.AE_MODE_AG_ONLY;
                    ExposureTimeApply.Enabled = AeMode == dvpAeMode.AE_MODE_AG_ONLY;
                    GainEdit.Enabled = AeMode == dvpAeMode.AE_MODE_AE_ONLY;
                    GainApply.Enabled = AeMode == dvpAeMode.AE_MODE_AE_ONLY;
                }
                CHECK_AEOPERATION.Checked = m_bAeOp;

                // Get the AE mode.
                COMBO_AE_MODE.SelectedIndex = (int)AeMode;

                // Update the Anti-Flick control.
                dvpAntiFlick AntiFlick = new dvpAntiFlick();
                status = DVPCamera.dvpGetAntiFlick(m_handle, ref AntiFlick);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);


                // Update the exposure time.
                double fExpoTime = 0.0f;
                status = DVPCamera.dvpGetExposure(m_handle, ref fExpoTime);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                if (ExposureTimeEdit.Maximum < (decimal)fExpoTime)
                    ExposureTimeEdit.Maximum = (decimal)fExpoTime;
                ExposureTimeEdit.Value = (decimal)fExpoTime;

                // Update the analog gain.
                float fGain = 0.0f;
                status = DVPCamera.dvpGetAnalogGain(m_handle, ref fGain);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                GainEdit.Value = (decimal)fGain;

                // Update the ROI.
                uint RoiSel = 0;
                status = DVPCamera.dvpGetQuickRoiSel(m_handle, ref RoiSel);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                if (COMBO_RESOLUTION.Items.Count > RoiSel)
                    COMBO_RESOLUTION.SelectedIndex = (int)RoiSel;

                // Update the exposure time and gain.
                UpdateControlExposure();
                UpdateControlGain();
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
                ExposureTimeEdit.Enabled = false;
                ExposureTimeApply.Enabled = false;
                GainEdit.Enabled = false;
                GainApply.Enabled = false;
                COMBO_RESOLUTION.Enabled = false;
                COMBO_AE_MODE.Enabled = false;
                CHECK_AEOPERATION.Enabled = false;
            }
            InitAWB();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitAWB()
        {
            dvpStatus status;
            dvpAwbOperation AwbOperation = new dvpAwbOperation();

            if (IsValidHandle(m_handle))
            {

                // Gets the current white balance property
                status = DVPCamera.dvpGetAwbOperation(m_handle, ref AwbOperation);
                // ASSERT(status == DVP_STATUS_OK);
                if (status != dvpStatus.DVP_STATUS_OK)
                {
                    // Auto white balance is not supported or getting fails
                    AWBEnable.Enabled = false;
                    AWBOnce.Enabled = false;

                    return;
                }
                // Set the controls
                AWBEnable.Enabled = true;

                AWBEnable.Checked = (AwbOperation == dvpAwbOperation.AWB_OP_CONTINUOUS);
                AWBOnce.Enabled = (!AWBEnable.Checked);
            }
            else
            {
                AWBEnable.Enabled = false;
                AWBOnce.Enabled = false;
            }
        }

        private void BUTTON_START_Click(object sender, EventArgs e)
        {
            if (COMBO_DEVICES.Text != String.Empty && COMBO_DEVICES.Text != "IDS Peak")
            {
                dvpStreamState state = dvpStreamState.STATE_STOPED;
                dvpStatus status = dvpStatus.DVP_STATUS_UNKNOW;

                if (IsValidHandle(m_handle))
                {
                    // Implement a button to start and stop according to the current video's status.
                    status = DVPCamera.dvpGetStreamState(m_handle, ref state);
                    Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                    if (state == dvpStreamState.STATE_STARTED)
                    {
                        status = DVPCamera.dvpStop(m_handle);
                        Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                        m_b_start = status == dvpStatus.DVP_STATUS_OK ? false : true;
                        BUTTON_SAVE.Enabled = false;

                    }
                    else
                    {
                        status = DVPCamera.dvpStart(m_handle);
                        Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                        m_b_start = status == dvpStatus.DVP_STATUS_OK ? true : false;
                        UpdateControlExposure();
                        BUTTON_SAVE.Enabled = true;
                    }
                }

                UpdateControls();
            }
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

            string file_path;
            Random rn = new Random();
            string rnd = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff");
            file_path = imageLocation + "\\" + "IM_IMG_X" + "_" + rnd + ".bmp";

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
        }

        private void chkb_CameraSettings_CheckedChanged(object sender, EventArgs e)
        {
            splitContainer.Panel2Collapsed = !chkb_CameraSettings.Checked;
        }

        private void cbhkb_AutoGain_CheckedChanged(object sender, EventArgs e)
        {
            GainEdit.Enabled = cbhkb_AutoGain.Checked;
            GainApply.Enabled = cbhkb_AutoGain.Checked;
        }

        private void GainApply_Click(object sender, EventArgs e)
        {
            float fGain = 0.0f;
            dvpStatus status;

            if (IsValidHandle(m_handle))
            {
                // Get the value of the analog gain slider.
                fGain = (float)GainEdit.Value;

                // Firstly,set the value of the analog gain.
                status = DVPCamera.dvpSetAnalogGain(m_handle, fGain);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                // Secondly,Get the value of the analog gain,there are differences between the set value and the obtained value for the reason of accuracy(step value),
                // it is subject to the obtained value.
                status = DVPCamera.dvpGetAnalogGain(m_handle, ref fGain);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                // Set the text of the analog gain.
                GainEdit.Value = (decimal)fGain;
            }
        }

        private void CHECK_AEOPERATION_CheckStateChanged(object sender, EventArgs e)
        {
            dvpStatus status;
            m_bAeOp = CHECK_AEOPERATION.Checked;

            if (m_bAeOp)
            {
                status = DVPCamera.dvpSetAeOperation(m_handle, dvpAeOperation.AE_OP_CONTINUOUS);
            }
            else
            {
                status = DVPCamera.dvpSetAeOperation(m_handle, dvpAeOperation.AE_OP_OFF);
            }
            Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
            UpdateControls();
        }

        private void ExposureTimeApply_Click(object sender, EventArgs e)
        {
            dvpStatus status;
            double f_time = 0.0f;
            if (IsValidHandle(m_handle))
            {
                if (ExposureTimeEdit.Maximum < ExposureTimeEdit.Value + 1)
                {
                    ExposureTimeEdit.Value = ExposureTimeEdit.Maximum - 1;
                }
                if (ExposureTimeEdit.Minimum > ExposureTimeEdit.Value - 1)
                {
                    ExposureTimeEdit.Value = ExposureTimeEdit.Minimum + 1;
                }

                status = DVPCamera.dvpSetExposure(m_handle, (float)ExposureTimeEdit.Value);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                //  Get the value of the exposure time again,there are differences between the set value and the obtained value for the reason of accuracy(step value),
                //  it is subject to the obtained value.
                status = DVPCamera.dvpGetExposure(m_handle, ref f_time);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                // Set the text of the exposure time.
                ExposureTimeEdit.Value = (long)f_time;
            }
        }

        private void COMBO_AE_MODE_ImeModeChanged(object sender, EventArgs e)
        {
            dvpStatus status = dvpStatus.DVP_STATUS_OK;

            // Get the index of the AE mode.         
            if (COMBO_AE_MODE.SelectedIndex > 3)
            {
                return;
            }

            // Prioritize the auto exposure.
            if (COMBO_AE_MODE.SelectedIndex == 0)
            {
                status = DVPCamera.dvpSetAeMode(m_handle, dvpAeMode.AE_MODE_AE_AG);
            }

            // Prioritize the auto gain.
            else if (COMBO_AE_MODE.SelectedIndex == 1)
            {
                status = DVPCamera.dvpSetAeMode(m_handle, dvpAeMode.AE_MODE_AG_AE);
            }

            // Open the auto exposure only.
            else if (COMBO_AE_MODE.SelectedIndex == 2)
            {
                status = DVPCamera.dvpSetAeMode(m_handle, dvpAeMode.AE_MODE_AE_ONLY);
            }

            // Open the auto gain only.
            else if (COMBO_AE_MODE.SelectedIndex == 3)
            {
                status = DVPCamera.dvpSetAeMode(m_handle, dvpAeMode.AE_MODE_AG_ONLY);
            }
            Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
            UpdateControls();
        }

        private void COMBO_AE_MODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvpStatus status = new dvpStatus();

            // Get the index of the AE mode.
            int iSel = COMBO_AE_MODE.SelectedIndex;
            if (iSel > 3)
            {
                return;
            }

            // Prioritize the auto exposure.
            else if (iSel == 2)
            {
                status = DVPCamera.dvpSetAeMode(m_handle, dvpAeMode.AE_MODE_AE_ONLY);
            }

            // Prioritize the auto gain.
            else if (iSel == 3)
            {
                status = DVPCamera.dvpSetAeMode(m_handle, dvpAeMode.AE_MODE_AG_ONLY);
            }
            // Open the auto exposure only.
            else if (iSel == 1)
            {
                status = DVPCamera.dvpSetAeMode(m_handle, dvpAeMode.AE_MODE_AG_AE);
            }
            // Open the auto gain only.
            if (iSel == 0)
            {
                status = DVPCamera.dvpSetAeMode(m_handle, dvpAeMode.AE_MODE_AE_AG);
            }
            Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
            UpdateControls();
        }

        private void COMBO_RESOLUTION_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvpStatus status;

            // Get the index of the ROI.
            int iSel = COMBO_RESOLUTION.SelectedIndex;
            if (iSel < 0)
            {
                return;
            }

            if (!IsValidHandle(m_handle))
                return;
            dvpStreamState state = new dvpStreamState();

            status = DVPCamera.dvpGetStreamState(m_handle, ref state);
            Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
            if (state == dvpStreamState.STATE_STARTED)
            {
                // Close the video stream.
                status = DVPCamera.dvpStop(m_handle);

                if (status != dvpStatus.DVP_STATUS_OK)
                {
                    MessageBox.Show(("Close the video stream fail!"));
                    return;
                }
                m_b_start = false;
            }


            // Set the ROI.
            status = DVPCamera.dvpSetQuickRoiSel(m_handle, (uint)iSel);
            if (status != dvpStatus.DVP_STATUS_OK)
            {
                MessageBox.Show(("Set the ROI error!"));
                return;
            }

            if (state == dvpStreamState.STATE_STARTED)
            {
                // Open the video stream.
                status = DVPCamera.dvpStart(m_handle);
                if (status != dvpStatus.DVP_STATUS_OK)
                {
                    MessageBox.Show(("Start the video stream fail!"));
                    return;
                }

                m_b_start = true;
            }
            UpdateControlExposure();
            UpdateControlGain();
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
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

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

        private void AWBEnable_CheckedChanged(object sender, EventArgs e)
        {
            dvpStatus status;
            dvpAwbOperation AwbOperation = new dvpAwbOperation();

            if (IsValidHandle(m_handle))
            {
                // Auto white balance,set to AWB_OP_CONTINUOUS stands for continuous operation. 
                if (AWBEnable.Checked)
                {
                    status = DVPCamera.dvpGetAwbOperation(m_handle, ref AwbOperation);
                    if (AwbOperation != dvpAwbOperation.AWB_OP_CONTINUOUS)
                    {
                        status = DVPCamera.dvpSetAwbOperation(m_handle, dvpAwbOperation.AWB_OP_CONTINUOUS);
                        Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                    }
                }
                else
                {
                    // Close auto white balance.
                    status = DVPCamera.dvpSetAwbOperation(m_handle, dvpAwbOperation.AWB_OP_OFF);
                    Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                }
                InitAWB();
            }
        }

        private void AWBOnce_Click(object sender, EventArgs e)
        {
            dvpStatus status;

            if (IsValidHandle(m_handle))
            {
                // Auto white balance,set to AWB_OP_ONCE stands for operating once. 
                status = DVPCamera.dvpSetAwbOperation(m_handle, dvpAwbOperation.AWB_OP_ONCE);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                // The RGB gain is set to 1.00, avoiding a superimposed gain effect
                bool bGainState = false;
                status = DVPCamera.dvpGetRgbGainState(m_handle, ref bGainState);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                if (status == dvpStatus.DVP_STATUS_OK)
                {
                    if (bGainState)
                    {
                        status = DVPCamera.dvpSetRgbGain(m_handle, 1.0f, 1.0f, 1.0f);
                        Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                    }
                    else
                    {
                        status = DVPCamera.dvpSetRgbGainState(m_handle, true);
                        Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                        status = DVPCamera.dvpSetRgbGain(m_handle, 1.0f, 1.0f, 1.0f);
                        Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                        status = DVPCamera.dvpSetRgbGainState(m_handle, bGainState);
                        Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                    }
                }


                InitAWB();
            }
        }

        private void btn_RGBGainApply_Click(object sender, EventArgs e)
        {
            float rGain = 0.0f;
            float gGain = 0.0f;
            float bGain = 0.0f;

            dvpStatus status;

            if (IsValidHandle(m_handle))
            {
                // Get the value of the analog gain slider.
                rGain = (float)nud_RGain.Value;
                gGain = (float)nud_GGain.Value;
                bGain = (float)nud_BGain.Value;

                status = DVPCamera.dvpSetRgbGain(m_handle, rGain, gGain, bGain);

                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                UpdateControlGain();

            }
        }

        private void chkb_ColorTemp_CheckStateChanged(object sender, EventArgs e)
        {
            tbar_ColorTemp.Enabled = chkb_ColorTemp.Checked;
        }

        private void chkb_Saturation_CheckStateChanged(object sender, EventArgs e)
        {
            tbar_Saturation.Enabled = chkb_Saturation.Checked;
        }

        private void tbar_Saturation_Scroll(object sender, EventArgs e)
        {
            int sauration = 0;
            dvpStatus status;

            if (IsValidHandle(m_handle))
            {
                // Get the value of the Saturation slider.
                sauration = tbar_Saturation.Value;

                // Firstly,set the value of the Saturation.
                status = DVPCamera.dvpSetSaturation(m_handle, sauration);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                // Secondly,Get the value of the Saturation,there are differences between the set value and the obtained value for the reason of accuracy(step value),
                // it is subject to the obtained value.
                status = DVPCamera.dvpGetSaturation(m_handle, ref sauration);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                // Set the text of the Saturation.
                tbar_Saturation.Value = sauration;
            }
        }

        private void chkb_Contrast_CheckedChanged(object sender, EventArgs e)
        {
            tbar_Contrast.Enabled = chkb_Contrast.Checked;
        }

        private void tbar_Contrast_Scroll(object sender, EventArgs e)
        {
            int contrast = 0;
            dvpStatus status;

            if (IsValidHandle(m_handle))
            {
                // Get the value of the Contrast slider.
                contrast = tbar_Contrast.Value;

                // Firstly,set the value of the analog gain.
                status = DVPCamera.dvpSetContrast(m_handle, contrast);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                // Secondly,Get the value of the Contrast,there are differences between the set value and the obtained value for the reason of accuracy(step value),
                // it is subject to the obtained value.
                status = DVPCamera.dvpGetContrast(m_handle, ref contrast);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                // Set the text of theContrast.
                tbar_Contrast.Value = contrast;
            }
        }

        private void chkb_Gamma_CheckedChanged(object sender, EventArgs e)
        {
            tbar_Gamma.Enabled = chkb_Gamma.Checked;
        }

        private void tbar_Gamma_Scroll(object sender, EventArgs e)
        {
            int gamma = 0;
            dvpStatus status;

            if (IsValidHandle(m_handle))
            {
                // Get the value of the analog Gamma.
                gamma = tbar_Gamma.Value;

                // Firstly,set the value of the analog gain.
                status = DVPCamera.dvpSetGamma(m_handle, gamma);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                // Secondly,Get the value of the Gamma,there are differences between the set value and the obtained value for the reason of accuracy(step value),
                // it is subject to the obtained value.
                status = DVPCamera.dvpGetGamma(m_handle, ref gamma);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                // Set the text of the Gamma.
                tbar_Gamma.Value = gamma;
            }
        }

        private void chkb_BlackLevel_CheckedChanged(object sender, EventArgs e)
        {
            tbar_BlackLevel.Enabled = chkb_BlackLevel.Checked;
        }

        private void tbar_BlackLevel_Scroll(object sender, EventArgs e)
        {
            float blacklevel = 0.0F;
            dvpStatus status;

            if (IsValidHandle(m_handle))
            {
                // Get the value of the black level.
                blacklevel = tbar_BlackLevel.Value;

                // Firstly,set the value of the black level.
                status = DVPCamera.dvpSetBlackLevel(m_handle, blacklevel);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                // Secondly,Get the value of the black level,there are differences between the set value and the obtained value for the reason of accuracy(step value),
                // it is subject to the obtained value.
                status = DVPCamera.dvpGetBlackLevel(m_handle, ref blacklevel);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                // Set the text of the black level.
                tbar_BlackLevel.Value = (int)blacklevel;
            }
        }

        private void chkb_Sharpness_CheckStateChanged(object sender, EventArgs e)
        {
            tbar_Sharpness.Enabled = chkb_Sharpness.Checked;
        }

        private void chkb_2DNoise_CheckStateChanged(object sender, EventArgs e)
        {
            tbar_2DNoise.Enabled = chkb_2DNoise.Checked;
        }

        private void chkb_3DNoise_CheckStateChanged(object sender, EventArgs e)
        {
            tbar_3DNoise.Enabled = chkb_3DNoise.Checked;
        }

        private void chkb_VFlip_CheckStateChanged(object sender, EventArgs e)
        {
            dvpStatus status;
            if (IsValidHandle(m_handle))
            {
                // set the value of the Vertical Flip.
                status = DVPCamera.dvpSetFlipVerticalState(m_handle, chkb_VFlip.Checked);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
            }
        }

        private void chkb_HFlip_CheckStateChanged(object sender, EventArgs e)
        {
            dvpStatus status;
            if (IsValidHandle(m_handle))
            {
                // set the value of the Horizontal Flip.
                status = DVPCamera.dvpSetFlipHorizontalState(m_handle, chkb_HFlip.Checked);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
            }
        }

        private void tbar_Sharpness_Scroll(object sender, EventArgs e)
        {
            int sharpness = 0;
            dvpStatus status;

            if (IsValidHandle(m_handle))
            {
                // Get the value of the Sharpness slider.
                sharpness = tbar_Sharpness.Value;

                // Firstly,set the value of the Sharpness.
                status = DVPCamera.dvpSetSharpness(m_handle, sharpness);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                // Secondly,Get the value of the Sharpness,there are differences between the set value and the obtained value for the reason of accuracy(step value),
                // it is subject to the obtained value.
                status = DVPCamera.dvpGetSharpness(m_handle, ref sharpness);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                // Set the text of the Sharpness.
                tbar_Sharpness.Value = sharpness;
            }
        }

        private void tbar_2DNoise_Scroll(object sender, EventArgs e)
        {
            int noise2D = 0;
            dvpStatus status;

            if (IsValidHandle(m_handle))
            {
                // Get the value of the 2D Noise slider.
                noise2D = tbar_2DNoise.Value;

                // Firstly,set the value of the 2D Noise.
                status = DVPCamera.dvpSetNoiseReduct2d(m_handle, noise2D);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                // Secondly,Get the value of the 2D Noise,there are differences between the set value and the obtained value for the reason of accuracy(step value),
                // it is subject to the obtained value.
                status = DVPCamera.dvpGetNoiseReduct2d(m_handle, ref noise2D);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                // Set the text of the 2D Noise.
                tbar_2DNoise.Value = noise2D;
            }
        }

        private void tbar_3DNoise_Scroll(object sender, EventArgs e)
        {
            int noise3D = 0;
            dvpStatus status;

            if (IsValidHandle(m_handle))
            {
                // Get the value of the 3D Noise slider.
                noise3D = tbar_2DNoise.Value;

                // Firstly,set the value of the 3D Noise .
                status = DVPCamera.dvpSetNoiseReduct3d(m_handle, noise3D);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                // Secondly,Get the value of the 3D Noise ,there are differences between the set value and the obtained value for the reason of accuracy(step value),
                // it is subject to the obtained value.
                status = DVPCamera.dvpGetNoiseReduct3d(m_handle, ref noise3D);
                Debug.Assert(status == dvpStatus.DVP_STATUS_OK);

                // Set the text of the 3D Noise .
                tbar_2DNoise.Value = noise3D;
            }
        }

        private void frm_Camera_Load(object sender, EventArgs e)
        {
            COMBO_DEVICES.Text = cls_EnvisionConfig.ReadDetailsFromXML(Global._applicationPath, @"/configurations/model");


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
                            Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                        }

                        InitControlResolution();
                        InitControlAeMode();
                        //InitControlFlick();
                        UpdateControlExposure();
                        UpdateControlGain();
                    }
                }
                else
                {
                    DVPCamera.dvpStop(m_handle);
                    m_b_start = false;
                    status = DVPCamera.dvpClose(m_handle);
                    Debug.Assert(status == dvpStatus.DVP_STATUS_OK);
                    m_handle = 0;
                    BUTTON_SAVE.Enabled = false;
                    pictureBox.Refresh();
                }

                UpdateControls();
            }      
            
        }
    }
}
