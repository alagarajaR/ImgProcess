using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImgProcess.test
{
    public partial class sakthipower : Form
    {
        public sakthipower()
        {
            InitializeComponent();
        }

        private void sakthipower_Load(object sender, EventArgs e)
        {

        }

		string key = "A!9HHhi%XjjYY4YP2@Nob009X";

		public string Decrypt(string cipher)
		{
			System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			try
			{
				System.Security.Cryptography.TripleDESCryptoServiceProvider tdes = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
				try
				{
					tdes.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));
					tdes.Mode = System.Security.Cryptography.CipherMode.ECB;
					tdes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
					System.Security.Cryptography.ICryptoTransform transform = tdes.CreateDecryptor();
					try
					{
						byte[] cipherBytes = System.Convert.FromBase64String(cipher);
						byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
						return System.Text.Encoding.UTF8.GetString(bytes);
					}
					finally
					{
						if (transform != null)
						{
							transform.Dispose();
						}
					}
				}
				finally
				{
					if (tdes != null)
					{
						((System.IDisposable)tdes).Dispose();
					}
				}
			}
			finally
			{
				if (md5 != null)
				{
					((System.IDisposable)md5).Dispose();
				}
			}
		}

        private void button1_Click(object sender, EventArgs e)
        {
			textBox2.Text = Decrypt(textBox1.Text);
        }
    }
}
