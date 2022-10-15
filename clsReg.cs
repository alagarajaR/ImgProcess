using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Security.Cryptography;
using System.IO;
namespace ImgProcess
{
    internal class clsReg
    {
   

    private byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
    {
        byte[] encryptedBytes = null;

        // Set your salt here, change it to meet your flavor:
        // The salt bytes must be at least 8 bytes.
        byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        using (MemoryStream ms = new MemoryStream())
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                AES.KeySize = 256;
                AES.BlockSize = 128;

                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);

                AES.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                    cs.Close();
                }
                encryptedBytes = ms.ToArray();
            }
        }

        return encryptedBytes;
    }

        public string GenKey()
        {


            string TradeMark = "";

            string key = "";

            ManagementClass manClass = new ManagementClass("Win32_Processor");
            ManagementObjectCollection mObjCol = manClass.GetInstances();

            ManagementClass manClass1 = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection mObjCol1 = manClass1.GetInstances();
            string x = "";
            foreach (ManagementObject m in mObjCol1)
            {
                string xx = m.Properties["MediaType"].Value.ToString();
                if (xx == "Fixed hard disk media")
                {
                    x = m.Properties["SerialNumber"].Value.ToString().Trim();
                    goto l;
                }
            }

        l:

            string pID = "";
            char Ch;
            int ChAsc, ChAsc2;
            string ChAsc1, chKey, Tmp;
            Tmp = "";
            ChAsc1 = "";

            foreach (ManagementObject m in mObjCol)
            {

                pID = m.Properties["ProcessorID"].Value.ToString().Trim();
                goto l2;
            }

        l2:

            //string t1 = TradeMark.Substring(0, 3);
            //string t2 = TradeMark.Substring(3, 3);

            //key = Reverse(pID) + Reverse(t1) + Reverse(x) + Reverse(t2);
            key = Reverse(pID) + Reverse(x);



            int partLength = 4;
            string s = key;
            string key1 = "";
            for (var i = 0; i < s.Length; i += partLength)
            {
                if (key1 == "")
                    key1 = s.Substring(i, Math.Min(partLength, s.Length - i));
                else
                    key1 = key1 + '-' + s.Substring(i, Math.Min(partLength, s.Length - i));
            }



            return key1;

        }

        private byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
    {
        byte[] decryptedBytes = null;

        // Set your salt here, change it to meet your flavor:
        // The salt bytes must be at least 8 bytes.
        byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        using (MemoryStream ms = new MemoryStream())
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                AES.KeySize = 256;
                AES.BlockSize = 128;

                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);

                AES.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                    cs.Close();
                }
                decryptedBytes = ms.ToArray();
            }
        }

        return decryptedBytes;
    }

    private string GetDemoRegKey(string key, DateTime sdate)
    {
        string akey = "";

        akey = "D-" + key + "-" + sdate.ToString("yyyyMMdd");

            //  regdll.clsReg obj = new regdll.clsReg(DateTime.Now.Date, "FCDA");

            string akey1 = "";// obj.EncryptText(akey, "AnivakKavina@2017");
        return akey1;
    }

   
    public string ErrMsg = "";

        public string MatchKey(string akey)
        {
            string result = "";
            try
            {
               

                string key = DecryptText(akey, "Jeevithra@200821");

                string[] key1 = key.Split('|');

                string ckey = GenKey();
                string ckey1 = key1[0];

                if (ckey == ckey1)
                {
                    result = "1";
                }
                else
                {
                    result = "0";
                }
            }
            catch (Exception ex)
            {
                result = "Unknown Error. Contact Software Vendor";
            }
            return result;

            }
    public string RegCheck(string akey)
        {
            string result = "";

            try
            { 
            string key = DecryptText(akey, "Jeevithra@200821");

            string[] key1 = key.Split('|');

            string ckey = GenKey();
            string ckey1 = key1[0];

            if (ckey == ckey1)
            {
                string[] date1= key1[1].Split('-');
                DateTime sdate = new DateTime(Convert.ToInt32(date1[0]), Convert.ToInt32(date1[1]), Convert.ToInt32(date1[2]));

                if (sdate < DateTime.Now.Date)
                {
                    result = "Product Expired On " + sdate.ToString("dd-MM-yyyy") + ".\n" + "Contact Software Vendor.";
                }
                else
                {
                    int days=(sdate- DateTime.Now.Date).Days;
                    int days1 = Convert.ToInt32(key1[2]);

                    if (days <= days1)
                    {
                        result ="RD-" + days.ToString();
                    }
                    else
                    {
                        result = "OK-" + days.ToString();
                    }

                }
            
            }
            else
            {
                result = "Invalid Key. Contact Software Vendor";
            }



            }
            catch (Exception ex)
            {
                result = "Unknown Error. Contact Software Vendor";
            }
            return result;

        }

    private string EncryptText(string input, string password)
    {
        // Get the bytes of the string
        byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        // Hash the password with SHA256
        passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

        byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

        string result = Convert.ToBase64String(bytesEncrypted);

        return result;
    }


    private string DecryptText(string input, string password)
    {
        // Get the bytes of the string
        byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

        byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

        string result = Encoding.UTF8.GetString(bytesDecrypted);

        return result;
    }

    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

}
}
