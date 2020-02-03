/**
 * Project: Lib.API 
 * FileName: Encryption.cs 
 * EF Version: 6.1.0 - FR: 4.5
 * Description: Short description.
 * Last update: 2020-2-3
 * Author: NGUYỄN ANH QUỐC (ASUS)
 * Email: naquoc1204@gmail.com
 * Phone: 093.123.6699
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lib.API
{
    /// <summary>
    /// Mã hóa dữ liệu
    /// </summary>
    public class Encryption
    {
        /// <summary>
        /// biến private
        /// </summary>
        private static string _cEncode = "naquoc1204@gmail.com";
        private static string _cEncodeMD5 = "aksiy2HSAlnsa";
        private static int[][] _nArrayValue = new int[7][];
        private static int _nNumMax = 0;

        #region "MÃ HÓA BASE 64"


        /// <summary>
        /// mã hóa sang base 64 kèm prefix
        /// </summary>
        /// <param name="value">Chuỗi cần mã hóa</param>
        /// <returns></returns>
        public static string EncryptBase64(string value)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            string Password = _cEncode;// "thetai.nguyen88@gmail.com";
            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(value);
            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
            //Creates a symmetric encryptor object. 
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream();
            //Defines a stream that links data streams to cryptographic transformations
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(PlainText, 0, PlainText.Length);
            //Writes the final state and clears the buffer
            cryptoStream.FlushFinalBlock();
            byte[] CipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string EncryptedData = Convert.ToBase64String(CipherBytes);
            return EncryptedData;
        }


        /// <summary>
        /// Giãi mã base 64 kèm prefix
        /// </summary>
        /// <param name="value">Chuỗi cần mã hóa</param>
        /// <returns>Chuỗi đã giải mã</returns>
        public static string DecryptBase64(string value)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            string Password = _cEncode;// "";
            string DecryptedData;
            try
            {
                byte[] EncryptedData = Convert.FromBase64String(value);

                byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
                //Making of the key for decryption
                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
                //Creates a symmetric Rijndael decryptor object.
                ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

                MemoryStream memoryStream = new MemoryStream(EncryptedData);
                //Defines the cryptographics stream for decryption.THe stream contains decrpted data
                CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

                byte[] PlainText = new byte[EncryptedData.Length];
                int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
                memoryStream.Close();
                cryptoStream.Close();
                //Converting to string
                DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
            }
            catch
            {
                DecryptedData = value;
            }
            return DecryptedData;
        }

        #endregion

        #region "MÃ HÓA MD5"

        /// <summary>
        /// mã hóa md5
        /// </summary>
        /// <param name="strInput">chuỗi cần mã hóa</param>
        /// <returns></returns>
        public static string MD5(string strInput)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(strInput);
            bs = x.ComputeHash(bs);
            StringBuilder s = new StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string md5String = s.ToString();
            return md5String;
        }

        /// <summary>
        /// Mã hóa md5 kèm theo prefix
        /// </summary>
        /// <param name="strInput">Chuỗi cần mã hóa</param>
        /// <returns></returns>
        public static string MD5Password(string strInput)
        {
            string md5String = EncryptBase64(strInput);
            md5String.Insert(2, _cEncodeMD5);
            md5String = MD5(md5String);
            return md5String;
        }

        #endregion

        #region "MÃ HÓA NUMBER"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Số cần mã hóa</param>
        /// <returns>Số đã mã hóa</returns>
        public static double EncodeNumber(object value)
        {
            LoadArray();
            double dValue = Util.GetDouble(value);
            string str = Util.GetString(dValue);
            if (!Util.IsEmpty(dValue) && (dValue > 0.0))
            {
                int @int = Util.GetInt(str.Substring(0, 1));
                str = str.Remove(0, 1);
                str = str.Remove((Util.GetLength(str) - 1) / 2, 1);
                for (int i = 0; i <= (Util.GetLength(str) - 1); i++)
                {
                    for (int j = 0; j <= 9; j++)
                    {
                        string str2 = str.Substring(i, 1);
                        string str3 = _nArrayValue[@int][j].ToString();
                        if (str2 == str3)
                        {
                            str = str.Remove(i, 1).Insert(i, Util.GetString(_nArrayValue[0][j]));
                            j = 10;
                        }
                    }
                }
                return Util.GetNumber(str);
            }
            return 0.0;
        }

        /// <summary>
        /// Giải mã number
        /// </summary>
        /// <param name="value">Số được mã hóa</param>
        /// <returns>Số đã giải mã</returns>
        public static double DecodeNumber(double value)
        {
            LoadArray();
            string str = Util.GetString(value);
            if (!Util.IsEmpty(value) && (value > 0.0))
            {
                int @int = Util.GetInt(str.Substring(0, 1));
                str = str.Remove(0, 1);
                str = str.Remove((Util.GetLength(str) - 1) / 2, 1);
                for (int i = 0; i <= (Util.GetLength(str) - 1); i++)
                {
                    for (int j = 0; j <= 9; j++)
                    {
                        string str2 = str.Substring(i, 1);
                        string str3 = _nArrayValue[@int][j].ToString();
                        if (str2 == str3)
                        {
                            str = str.Remove(i, 1).Insert(i, Util.GetString(_nArrayValue[0][j]));
                            j = 10;
                        }
                    }
                }
                return Util.GetNumber(str);
            }
            return 0.0;
        }

        /// <summary>
        /// Khởi tạo mãng số
        /// </summary>
        private static void LoadArray()
        {
            _nArrayValue[0] = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            _nArrayValue[1] = new int[] { 9, 3, 4, 7, 1, 2, 0, 5, 6, 8 };
            _nArrayValue[2] = new int[] { 4, 5, 3, 8, 2, 7, 9, 6, 1, 0 };
            _nArrayValue[3] = new int[] { 0, 6, 3, 1, 7, 9, 8, 4, 5, 2 };
            _nArrayValue[4] = new int[] { 0, 4, 2, 6, 8, 3, 7, 1, 5, 9 };
            _nArrayValue[5] = new int[] { 0, 3, 6, 9, 2, 4, 6, 8, 5, 7 };
            _nArrayValue[6] = new int[] { 3, 7, 1, 0, 2, 9, 8, 4, 5, 6 };
            _nNumMax = _nArrayValue.Count<int[]>();
        }

        #endregion



        #region "MÃ HÓA KÝ TỰ THÀNH SỐ"

        /// <summary>
        /// Mã hóa ký tự sang số 
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static string EncodeOTN(string strInput)
        {
            try
            {
                string output = string.Empty;
                string value = strInput;
                int lenght = value.Length;
                for (int i = 0; i <= lenght - 1; i++)
                {
                    int val = System.Convert.ToInt32(value[i] + 15);
                    output += string.Format("{0}{1}", val.ToString().Length, val);
                }
                return output;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        ///  Giải mã từ số sang ký tự
        /// </summary>
        /// <param name="strOutput"></param>
        /// <returns></returns>
        public static string DecodeOTN(string strOutput)
        {
            string output = string.Empty;
            string value = strOutput;
            int lenght = value.Length;
            if (strOutput != string.Empty || strOutput != null)
            {
                for (int i = 0; i <= lenght - 1; i++)
                {
                    string len = (value[i]).ToString();
                    output += char.ConvertFromUtf32(Convert.ToInt32(value.Substring((i + 1), Convert.ToInt32(len))) - 15);
                    i += Convert.ToInt32(len);
                }
            }
            return output;
        }

        #endregion
    }
}
