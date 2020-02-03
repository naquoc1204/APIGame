/**
 * Project: Lib.API 
 * FileName: Util.cs 
 * EF Version: 6.1.0 - FR: 4.5
 * Description: Short description.
 * Last update: 2020-2-3
 * Author: NGUYỄN ANH QUỐC (ASUS)
 * Email: naquoc1204@gmail.com
 * Phone: 093.123.6699
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Lib.API
{
    /// <summary>
    /// Các hàm tiện ích kiểm tra & xử lý dữ liệu
    /// </summary>
    public class Util
    {


        #region "CÁC HÀM CHECK KIỂU DỮ LIỆU"

        /// <summary>
        /// Kiểm tra dữ liệu rỗng
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>true/false</returns>
        public static bool IsEmpty(object value)
        {
            return value == null ? true : IsEmpty(value.ToString());
        }

        /// <summary>
        /// Kiểm tra dữ liệu rỗng
        /// </summary>
        /// <param name="value">String</param>
        /// <returns>true/false</returns>
        public static bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Kiểm tra kiểu number
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>true/false</returns>
        public static bool IsNumber(object value)
        {
            double num;
            return double.TryParse((value ?? string.Empty).ToString(), out num);
        }

        /// <summary>
        /// Kiểm tra kiểu ngày
        /// </summary>
        /// <param name="date">String</param>
        /// <returns>true/false</returns>
        public static bool IsDate(string date)
        {
            DateTime result = new DateTime();
            return DateTime.TryParse(date, out result);
        }

        /// <summary>
        /// Kiểm tra Email
        /// </summary>
        /// <param name="date">String</param>
        /// <returns>true/false</returns>
        public static bool IsEmail(string value)
        {
            Regex regex = new Regex(@"^\w+([-+.]\w+)*@(\w+([-.]\w+)*\.)+([a-zA-Z]+)+$", RegexOptions.IgnoreCase);
            return regex.Match(value).Success;
        }

        public static string GetUserEmail(string value)
        {
            MailAddress address = new MailAddress(value);
            return address.User;
        }

        /// <summary>
        /// Kiểm tra URL, chỉ http
        /// </summary>
        /// <param name="date">String</param>
        /// <returns>true/false</returns>
        public static bool IsUrl(string value)
        {
            Regex regex = new Regex(@"(http://)?([\w-]+\.)*[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.IgnoreCase);
            return regex.Match(value).Success;
        }

        #endregion


        #region "CÁC HÀM GET/CONVERT DỮ LIỆU"

        /// <summary>
        /// Convert sang String
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>String</returns>
        public static string GetString(object obj)
        {
            try
            {
                return IsEmpty(obj) ? string.Empty : obj.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Convert sang Int32
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Int32</returns>
        public static int GetInt(object obj)
        {
            return (IsEmpty(obj) || !IsNumber(obj)) ? 0 : Convert.ToInt32(obj);
        }

        /// <summary>
        /// COnvert sang Double
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Double</returns>
        public static double GetDouble(object obj)
        {
            return (IsEmpty(obj) || !IsNumber(obj)) ? 0 : Convert.ToDouble(obj);
        }

        /// <summary>
        /// Convert sang Number (Double)
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Double</returns>
        public static double GetNumber(object obj)
        {
            return (IsEmpty(obj) || !IsNumber(obj)) ? 0 : Convert.ToDouble(obj);
        }

        /// <summary>
        /// Convert sang Decimal 
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Decimal</returns>
        public static decimal GetDecimal(object obj)
        {
            return (IsEmpty(obj) || !IsNumber(obj)) ? 0 : Convert.ToDecimal(obj);
        }

        /// <summary>
        /// Convert sang FLoat
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>FLoat</returns>
        public static float getFloat(object obj)
        {
            return (IsEmpty(obj) || !IsNumber(obj)) ? 0f : float.Parse(GetString(obj));
        }

        #endregion


        #region "CÁC HÀM XỬ LÝ CHUỖI"
        
        /// <summary>
        /// Lấy chiều dài chuỗi
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Int</returns>
        public static int GetLength(object obj)
        {
            return GetString(obj).Length;
        }

        /// <summary>
        /// Chuyển chuổi thành mảng
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="separator">Char ký tự</param>
        /// <returns>Array</returns>
        public static string[] GetArray(object obj, char separator)
        {
            return GetString(obj).Split(separator);
        }

        /// <summary>
        /// Đảo chuỗi
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Chuổi, có ngăn cách bởi dấu *</returns>
        public static string Reverse(object obj)
        {
            var array = GetString(obj).ToCharArray();
            Array.Reverse(array);
            return string.Join("*", array);
        }

        /// <summary>
        /// Cắt chuỗi bên trái
        /// </summary>
        /// <param name="value">Object/String</param>
        /// <param name="length">Chiều dài cần lấy</param>
        /// <returns>String</returns>
        public static string Left(object value, int length)
        {
            if (GetLength(value) < length)
                return GetString(value);
            else
                return GetString(value).Substring(0, length);
        }

        /// <summary>
        /// Cắt chuỗi bên phải
        /// </summary>
        /// <param name="value">Object/String</param>
        /// <param name="length">Chiều dài cần lấy</param>
        /// <returns>String</returns>
        public static string Right(object value, int length)
        {
            if (GetLength(value) < length)
                return GetString(value);
            else
                return GetString(value).Substring(GetLength(value) - length, length);
        }

        

        /// <summary>
        /// Tạo chuỗi Random
        /// </summary>
        /// <param name="lenght">Chiều dài chuỗi</param>
        /// <returns>String</returns>
        public static string CreateCodeSecurity(int lenght)
        {
            string code = "abcdefghijkmnpqrstuvxyzwABCDEFGHIJKLMNPQRSTUVXYZW1234567890";
            Random rd = new Random();
            string value = "";
            for (int i = 0; i < lenght; i++)
            {
                value += code.Substring(rd.Next(0, code.Length - 2), 1);
            }
            return value;
        }

        /// <summary>
        /// Remove các thẻ Html khỏi nội dung
        /// </summary>
        /// <param name="html">Nội dung</param>
        /// <param name="allowHarmlessTags">true/false</param>
        /// <returns>String</returns>
        public static string StripHtml(string html, bool allowHarmlessTags)
        {
            if (html == null || html == string.Empty)
                return string.Empty;
            if (allowHarmlessTags)
                return Regex.Replace(html, "", string.Empty);
            return Regex.Replace(html, "<[^>]*>", string.Empty);
        }

        /// <summary>
        /// Lấy ký tự trong nội dung
        /// </summary>
        /// <param name="value">Nội dung</param>
        /// <param name="lenght">Số ký tự</param>
        /// <returns>String</returns>
        public static string WordLimit(string value, int lenght)
        {
            value = StripHtml(value, false);
            if (IsEmpty(value))
                return string.Empty;
            else
            {
                string[] tmp = value.Split(' ');

                if (tmp.Length > lenght)
                {
                    value = "";
                    for (int i = 0; i <= lenght; i++)
                    {
                        if (tmp[i] != " " || !IsEmpty(tmp[i]))
                            value += tmp[i] + " ";
                    }
                    return value.Remove(value.Length - 1, 1) + "...";
                }
                else
                {
                    return value;
                }
            }
        }


        /// <summary>
        /// Chữ hoa đầu câu
        /// </summary>
        /// <param name="value">String</param>
        /// <returns>String</returns>
        public static string UpFirstText(string value)
        {
            if (IsEmpty(value)) return "";
            else
            {
                if (GetLength(value) == 1)
                {
                    return value.ToUpper();
                }
                else
                {
                    return value.Substring(0, 1).ToUpper() + value.Substring(1, GetLength(value) - 1);
                }
            }
        }

        /// <summary>
        /// Xóa các ký tự đặc biệt
        /// Chỉ lấy các ký tự A-Z và 0-9
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9 ]+", "", RegexOptions.Compiled);
        }


        /// <summary>
        /// Chuyển ký tự có dầu thành không dấu
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RejectMarksforString(string text)
        {
            string[] pattern = new string[7];

            pattern[0] = "a|(á|ả|à|ạ|ã|ă|ắ|ẳ|ằ|ặ|ẵ|â|ấ|ẩ|ầ|ậ|ẫ)";
            pattern[1] = "o|(ó|ỏ|ò|ọ|õ|ô|ố|ổ|ồ|ộ|ỗ|ơ|ớ|ở|ờ|ợ|ỡ)";
            pattern[2] = "e|(é|è|ẻ|ẹ|ẽ|ê|ế|ề|ể|ệ|ễ)";
            pattern[3] = "u|(ú|ù|ủ|ụ|ũ|ư|ứ|ừ|ử|ự|ữ)";
            pattern[4] = "i|(í|ì|ỉ|ị|ĩ)";
            pattern[5] = "y|(ý|ỳ|ỷ|ỵ|ỹ)";
            pattern[6] = "d|đ";
            for (int i = 0; i < pattern.Length; i++)
            {
                // kí tự sẽ thay thế
                char replaceChar = pattern[i][0];
                MatchCollection matchs = Regex.Matches(text, pattern[i]);
                foreach (Match m in matchs)
                {
                    text = text.Replace(m.Value[0], replaceChar);
                }
            }
            text = text.Trim().ToLower();
            return text;
        }


        /// <summary>
        /// Lấy danh sách url hình trong nội dung
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Array</returns>
        public static List<string> GetImageURLList(string value)
        {
            string pattern = @"http://.[\S]+(.jpg |.png |.jpeg |" +
            ".bmp |.gif |.jpg$|.png$|.jpeg$|.bmp$|.gif$)";
            Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);
            List<string> listImageURl = new List<string>();
            MatchCollection mc;
            mc = reg.Matches(value);
            if (mc.Count > 0)
            {
                int count = 0;
                int pos = value.IndexOf(mc[count].Value);
                while (pos != -1)
                {
                    listImageURl.Add(mc[count].Value);
                    value = mc[count].Value;
                    count++;
                    if (count > mc.Count)
                        pos = -1;
                    else
                        pos = value.IndexOf(mc[count].Value);
                }
                listImageURl.Add(value);
            }
            return listImageURl;
        }


        /// <summary>
        /// Lấy nội dung từ HTML
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>String</returns>
        public static string GetHttpFromURL(string url)
        {
            string strResult = string.Empty;
            try
            {
                System.Net.WebClient MyWebClient = new System.Net.WebClient();
                MyWebClient.Credentials = System.Net.CredentialCache.DefaultCredentials;
                MyWebClient.Encoding = System.Text.Encoding.UTF8;
                strResult = MyWebClient.DownloadString(url);
            }
            catch (Exception)
            {
                strResult = "Tải trang thất bại";
            }
            return strResult;
        }


        #endregion

        #region "DATE"

        public static string formatDate(DateTime date, string format)
        {
            return date.ToString(format);
        }

        public static string formatDate(DateTime? date, string format)
        {
            if (IsEmpty(date))
            {
                return string.Empty;
            }
            else
            {
                return date.Value.ToString(format);
            }
        }


        public static DateTime? dateToSQL(string dateString)
        {
            try
            {
                var arrayDate = dateString.Split('/').Select(i => int.Parse(i)).ToArray<int>();
                return new DateTime(arrayDate[2], arrayDate[1], arrayDate[0], 0, 0, 0);
            }
            catch
            {
                return null;
            }
        }

        public static DateTime dateToSQL(string dateString, string timeString)
        {
            try
            {
                var aDate = GetArray(dateString, '/');
                var aTime = GetArray(timeString, ':');
                var date = new DateTime(GetInt(aDate[2]), GetInt(aDate[1]), GetInt(aDate[0]), GetInt(aTime[0]), GetInt(aTime[1]), 0);
                return date;
            }
            catch
            {
                return new DateTime();
            }
        }

       

        public static string dateFull(DateTime? date, string lang)
        {
            if (IsEmpty(date))
            {
                return string.Empty;
            }
            else
            {
                if (lang.Equals("vn"))
                {
                    return formatDate(date, "dddd, dd/MM/yyyy");
                }
                else
                {
                    return formatDate(date, "dddd, dd/MM/yyyy");
                }

            }
        }
        #endregion
    }
}
