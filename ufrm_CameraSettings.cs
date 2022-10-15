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
using Envision.Common;

namespace ImgProcess
{
    public partial class ufrm_CameraSettings : KryptonForm
    {

        
        public ufrm_CameraSettings()
        {
            InitializeComponent();
            kryptonPalette.BasePaletteMode = com.kp;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            this.errLbl1.SetVal = "";

            try
            {
                if (cmbResolution.Text.Length == 0)
                {
                    errLbl1.SetVal = "Please Select Resolution";
                    return;
                }

                if (cmbModel.Text.Length == 0)
                {
                    errLbl1.SetVal = "Please Select Model";
                    return;
                }

                txtHeight.Text = com.Val(txtHeight.Text).ToString();
                txtWidth.Text = com.Val(txtWidth.Text).ToString();


                // Write to XML File
                cls_EnvisionConfig.SaveDetailsToXML(Global._applicationPath, @"/configurations/resolution", cmbResolution.Text);
                cls_EnvisionConfig.SaveDetailsToXML(Global._applicationPath, @"/configurations/width", txtWidth.Text);
                cls_EnvisionConfig.SaveDetailsToXML(Global._applicationPath, @"/configurations/height", txtHeight.Text);
                cls_EnvisionConfig.SaveDetailsToXML(Global._applicationPath, @"/configurations/previewmode", cmbPreview.Text);
                cls_EnvisionConfig.SaveDetailsToXML(Global._applicationPath, @"/configurations/model", cmbModel.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error saving details to configuration " + ex.Message);
            }
            finally
            {
                MessageBox.Show("Configuraiton Saved Succesfully !!");
                this.Close();
            }
        }

        private void ufrm_CameraSettings_Load(object sender, EventArgs e)
        {
            try
            {
                cmbResolution.Text = cls_EnvisionConfig.ReadDetailsFromXML(Global._applicationPath, @"/configurations/resolution");
                txtWidth.Text = cls_EnvisionConfig.ReadDetailsFromXML(Global._applicationPath, @"/configurations/width");
                txtHeight.Text = cls_EnvisionConfig.ReadDetailsFromXML(Global._applicationPath, @"/configurations/height");
                cmbPreview.Text = cls_EnvisionConfig.ReadDetailsFromXML(Global._applicationPath, @"/configurations/previewmode");
                cmbModel.Text = cls_EnvisionConfig.ReadDetailsFromXML(Global._applicationPath, @"/configurations/model");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error loading details from configuration " + ex.Message);
            }

        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
