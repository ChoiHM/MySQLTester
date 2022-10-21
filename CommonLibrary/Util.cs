namespace CommonLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Serialization;


    public static partial class Util
    {
        public static bool CheckDupliation()
        {
            String thisprocessname = Process.GetCurrentProcess().ProcessName;

            if (Process.GetProcesses().Count(p => p.ProcessName == thisprocessname) > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        public static void DoubleBuffered(this DataGridView ctrl, bool setting = true)
        {
            Type dgvType = ctrl.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(ctrl, setting, null);
        }

        public static void DoubleBuffered(this TableLayoutPanel ctrl, bool setting = true)
        {
            Type dgvType = ctrl.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(ctrl, setting, null);
        }

        public static string Substring2(this string o, int startIndex, int Length, string defaultString = "")
        {
            try
            {
                if (o.Length >= startIndex + Length)
                {
                    return o.Substring(startIndex, Length);
                }
                else
                {
                    return defaultString;
                }
            }
            catch
            {
                return "";
            }
        }

        public static string Substring3(this string o, int startIndex, int Length)
        {
            try
            {
                if (o.Length >= startIndex + Length)
                {
                    return o.Substring(startIndex, Length);
                }
                else
                {
                    return o.Substring(startIndex, o.Length);
                }
            }
            catch
            {
                return "";
            }
        }

        public static string ToStringIfNull(this object o, string defaultString = "")
        {
            try
            {
                if (o == null || o == DBNull.Value)
                {
                    return defaultString;
                }
                else
                {
                    return o.ToString();
                }
            }
            catch
            {
                return "";
            }
        }

        public static DataTable ToDataTable<T>(this IList<T> data, string TableName)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            table.TableName = TableName;
            return table;
        }

        public static bool AlreadyRunning()
        {
            String thisprocessname = Process.GetCurrentProcess().ProcessName;
            if (Process.GetProcesses().Count(p => p.ProcessName == thisprocessname) > 1)
                return true;
            else
                return false;
        }

        public static void InvokeIfRequired(this ISynchronizeInvoke obj, MethodInvoker action)
        {
            try
            {
                if (obj.InvokeRequired)
                {
                    var args = new object[0];
                    obj.Invoke(action, args);
                }
                else
                {
                    action();
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public static T[] RemoveAt<T>(this T[] source, int index)
        {
            T[] dest = new T[source.Length - 1];
            if (index > 0)
                Array.Copy(source, 0, dest, 0, index);

            if (index < source.Length - 1)
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

            return dest;
        }

        #region 로컬 시간 변경

        public struct SystemTime
        {
            public ushort Year;
            public ushort Month;
            public ushort DayOfWeek;
            public ushort Day;
            public ushort Hour;
            public ushort Minute;
            public ushort Second;
            public ushort Millisecond;
        };

        [DllImport("kernel32.dll", EntryPoint = "SetSystemTime", SetLastError = true)]
        public static extern bool Win32SetSystemTime(ref SystemTime sysTime);

        public static void SetLocalSystemTime(DateTime dt)
        {
            SystemTime LOCALTIME = new SystemTime();
            LOCALTIME.Year = (ushort)dt.Year;
            LOCALTIME.Month = (ushort)dt.Month;
            LOCALTIME.DayOfWeek = (ushort)dt.DayOfWeek;
            LOCALTIME.Day = (ushort)dt.Day;
            LOCALTIME.Hour = (ushort)dt.Hour;
            LOCALTIME.Minute = (ushort)dt.Minute;
            LOCALTIME.Second = (ushort)dt.Second;
            // Call the unmanaged function that sets the new date and time instantly
            Win32SetSystemTime(ref LOCALTIME);
        }

        #endregion

        #region 날짜 관련
        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
            {
                yield return day;
            }
        }

        public static IEnumerable<DateTime> EachWeek(DateTime from, DateTime thru, bool isAssending = true)
        {
            if (isAssending == true)
            {
                for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(7))
                {
                    yield return ThisWeekStart(day);
                }
            }
            else
            {
                for (var day = thru.Date; day.Date >= from.Date; day = day.AddDays(-7))
                {
                    yield return ThisWeekStart(day);
                }
            }
        }

        public static IEnumerable<DateTime> EachMonth(DateTime from, DateTime thru, bool isAssending = true)
        {
            if (isAssending == true)
            {
                for (var day = from.Date; day.Date <= thru.Date; day = day.AddMonths(1))
                {
                    yield return new DateTime(day.Year, day.Month, 1);
                }
            }
            else
            {
                for (var day = thru.Date; day.Date >= from.Date; day = day.AddMonths(-1))
                {
                    yield return new DateTime(day.Year, day.Month, 1);
                }
            }
        }

        public struct DateRange
        {
            public DateTime Start { get; set; }

            public DateTime End { get; set; }
        }

        public static DateTime ThisYearStart(DateTime date)
        {
            DateRange range = new DateRange();

            range.Start = new DateTime(date.Year, 1, 1);
            range.End = range.Start.AddYears(1).AddSeconds(-1);

            return range.Start;
        }

        public static DateTime ThisYearEnd(DateTime date)
        {
            DateRange range = new DateRange();

            range.Start = new DateTime(date.Year, 1, 1);
            range.End = range.Start.AddYears(1).AddSeconds(-1);

            return range.End;
        }

        public static DateTime ThisMonthStart(DateTime date)
        {
            DateTime ret = new DateTime(date.Year, date.Month, 1);
            return ret;
        }

        public static DateTime ThisMonthEnd(DateTime date)
        {
            DateTime ret = new DateTime(date.Year, date.Month, 1);
            ret = ThisMonthStart(ret).AddMonths(1).AddSeconds(-1);
            return ret;
        }

        public static DateTime ThisWeekStart(DateTime date)
        {
            DateTime ret = new DateTime(date.Year, date.Month, date.Day);
            return ret.AddDays(-(int)date.DayOfWeek);
        }

        public static DateTime ThisWeekEnd(DateTime date)
        {
            DateTime ret = new DateTime(date.Year, date.Month, date.Day);
            ret = ThisWeekStart(ret).AddDays(7).AddSeconds(-1);
            return ret;
        }

        public static DateTime LastWeekStart(DateTime date)
        {
            DateTime ret = new DateTime(date.Year, date.Month, date.Day);
            return ThisWeekStart(ret.AddDays(-(int)date.DayOfWeek - 3));
        }

        public static DateTime LastWeekEnd(DateTime date)
        {
            DateTime ret = new DateTime(date.Year, date.Month, date.Day);
            ret = ThisWeekEnd(ThisWeekStart(ret).AddDays(-3).AddSeconds(-1));
            return ret;
        }

        public static DateRange LastYear(DateTime date)
        {
            DateRange range = new DateRange();

            range.Start = new DateTime(date.Year - 1, 1, 1);
            range.End = range.Start.AddYears(1).AddSeconds(-1);

            return range;
        }

        public static DateRange LastMonth(DateTime date)
        {
            DateRange range = new DateRange();

            range.Start = (new DateTime(date.Year, date.Month, 1)).AddMonths(-1);
            range.End = range.Start.AddMonths(1).AddSeconds(-1);

            return range;
        }

        public static DateTime ToDate(this string date)
        {
            try
            {
                if (date.Contains("-"))
                {
                    return DateTime.Parse(date);
                }
                else
                {
                    return new DateTime(date.Substring(0, 4).ToInt(), date.Substring(4, 2).ToInt(), date.Substring(6, 2).ToInt());
                }
            }
            catch
            {
                return DateTime.Now;
            }
        }


        public static int GetWeekOfYear(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static int getWeekDiff(DateTime d1, DateTime d2, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            var diff = d2.Subtract(d1);

            var weeks = diff.Days / 7;

            var remainingDays = diff.Days % 7;
            var cal = CultureInfo.InvariantCulture.Calendar;
            var d1WeekNo = cal.GetWeekOfYear(d1, CalendarWeekRule.FirstFullWeek, startOfWeek);
            var d1PlusRemainingWeekNo = cal.GetWeekOfYear(d1.AddDays(remainingDays), CalendarWeekRule.FirstFullWeek, startOfWeek);

            if (d1WeekNo != d1PlusRemainingWeekNo)
                weeks++;

            return weeks;
        }

        public static DateTime FirstDateOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstMonday = jan1.AddDays(daysOffset);
            int firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(jan1, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

            if (firstWeek <= 1)
            {
                weekOfYear -= 1;
            }

            return firstMonday.AddDays(weekOfYear * 7 + 1);
        }

        public static DateTime FirstDateOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime lastDateOfMonth(DateTime date)
        {
            return FirstDateOfMonth(date).AddMonths(1).AddDays(-1);
        }
        #endregion

        #region 문자열/수 관련
        public static bool IsEmpty(this string text)
        {
            if (string.IsNullOrWhiteSpace(text)) { return true; }
            else { return false; }
        }

        // 빈 문자열일 경우 지정된 Default값을 리턴
        public static string DefaultIfNull(this string text, string strDefaultValue)
        {
            if (string.IsNullOrWhiteSpace(text)) { return strDefaultValue; }
            else { return text; }
        }

        public static string dbnullToString<T>(this T x)
        {
            try
            {
                if (x == null || x.GetType() == DBNull.Value.GetType())
                {
                    return "";
                }
                else if (x.GetType() != typeof(T))
                {
                    return x.ToString();
                }
                else
                {
                    T t = x;
                    return t.ToString();
                    //return (T)x.ToString();
                    //return (string)x;
                }
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 첫글자만 대문자로 만들기
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(this string input)
        {
            if (input.IsEmpty())
            {
                return "";
            }
            else
            {
                return input.First().ToString().ToUpper() + input.Substring(1).ToLower();
            }
        }

        public static double dbnullToDouble(this object x)
        {
            try
            {
                if (x == DBNull.Value)
                {
                    return 0;
                }
                else
                {
                    return (double)x;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static string encodeSpecialChar(this string x)
        {
            string ret;
            ret = x.Replace("]", "]]");
            return ret;
        }

        public static string decodeSpecialChar(this string x)
        {
            string ret;
            ret = x.Replace("]]", "]");
            return ret;
        }

        public static byte ToHexString(this string nor)
        {
            return Convert.ToByte(nor, 16);
        }

        public static int HexToDec(this int hex)
        {
            return Convert.ToInt32(hex.ToString(), 16);
        }
        public static int HexToDec(this byte hex)
        {
            return Convert.ToInt32(hex.ToString(), 16);
        }

        public static bool IsInt(this string s)
        {
            int ret;
            return int.TryParse(s, out ret);
        }

        public static int ToInt(this string s, int defaultValue = 0)
        {
            try
            {
                int ret;
                double dblRet;
                if (int.TryParse(s, out ret))
                {
                    return int.Parse(s);
                }
                else if (double.TryParse(s, out dblRet))
                {
                    return Convert.ToInt32(double.Parse(s));
                }
                else
                {
                    return defaultValue;
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        public static double ToDouble(this string s, double defaultValue = 0, int fractionalPosition = 5)
        {
            double ret;
            if (double.TryParse(s, out ret))
            {
                return Math.Round(double.Parse(s), fractionalPosition);
            }
            else
            {
                return defaultValue;
            }
        }

        public static bool HasValue(this double value)
        {
            return !Double.IsNaN(value) && !Double.IsInfinity(value);
        }

        public static Byte ToByte(this string s, Byte defaultValue = 0)
        {
            Byte ret;
            if (Byte.TryParse(s, out ret))
            {
                return Byte.Parse(s);
            }
            else
            {
                return defaultValue;
            }
        }

        // String을 바이트 배열로 변환
        public static byte[] StringToByte(this string text, bool isLittleIndian = false)
        {
            byte[] StrByte = Encoding.UTF8.GetBytes(text);
            //if (isLittleIndian == true)
            //Array.Reverse(StrByte);
            return StrByte;
        }

        // 바이트 배열을 String으로 변환
        public static string ByteToString(this byte[] bytes, Encoding encType)
        {
            if (encType == null) { encType = Encoding.Default; }

            string str = encType.GetString(bytes);
            return str;
        }

        public static string ToHex(this string strValue)
        {
            return int.Parse(strValue).ToString("x").ToUpper();
        }

        public static byte[] HexToBytes(this string num)
        {
            if (num.Length % 2 != 0)
            {
                num = num.PadLeft(num.Length + 1, '0');
            }

            byte[] xBytes = new byte[num.Length / 2];

            for (int i = 0; i < xBytes.Length; i++)
            {
                xBytes[i] = Convert.ToByte(num.Substring(i * 2, 2), 16);
            }
            Array.Reverse(xBytes);
            return xBytes;

            //short number = Convert.ToInt16(num);
            //byte[] bytes = BitConverter.GetBytes(number);
            ////if (BitConverter.IsLittleEndian == false)
            ////    Array.Reverse(bytes);
            //return bytes;
        }

        public static byte[] ShortToLittleIndian(this short intValue)
        {
            byte[] intBytes = BitConverter.GetBytes(intValue);
            if (BitConverter.IsLittleEndian == false)
                Array.Reverse(intBytes);
            return intBytes;
        }

        public static byte calCheckSum(this List<byte> pkList)
        {
            Byte _CheckSumByte = 0x00;
            foreach (byte item in pkList)
            {
                _CheckSumByte ^= item;
            }
            return _CheckSumByte;
        }

        /// <summary>
        /// 소수점 찍기
        /// </summary>
        /// <param name="s"></param>
        /// <param name="Position"></param>
        /// <returns></returns>
        public static string WithPoint(this string s, int Position)
        {
            string ret = s;
            int totalWidth = 10;

            try
            {
                if (double.Parse(ret) < 0)
                {
                    ret = ret.Substring(1);
                    ret = ret.PadLeft(totalWidth, '0');
                    if (ret.Length > totalWidth) { totalWidth = ret.Length; }
                    ret = ret.Insert(totalWidth - Position, ".");
                    ret = ret.TrimStart('0');
                    ret = "-" + ret;
                }
                else
                {
                    ret = ret.PadLeft(totalWidth, '0');
                    if (ret.Length > totalWidth) { totalWidth = ret.Length; }
                    ret = ret.Insert(totalWidth - Position, ".");
                    ret = ret.TrimStart('0');
                }

                if (Math.Abs(double.Parse(ret)) < 1)
                {
                    ret = ret.Replace(".", "0.");
                }

                if (ret.EndsWith("."))
                {
                    ret = ret.Substring(0, ret.Length - 1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }

        /// <summary>
        /// 표준편차
        /// </summary>
        /// <param name="valueList"></param>
        /// <returns></returns>
        public static double StandardDeviation(List<double> valueList)
        {
            double M = 0.0;
            double S = 0.0;
            int k = 1;
            foreach (double value in valueList)
            {
                double tmpM = M;
                M += (value - tmpM) / k;
                S += (value - tmpM) * (value - M);
                k++;
            }
            return Math.Sqrt(S / (k - 2));
        }

        #endregion

        #region 메세지박스
        //public static DialogResult msgError(string msg)
        //{
        //    return MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        //}

        //public static DialogResult msgInform(string msg)
        //{
        //    return MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        //}

        //public static DialogResult msgWarning(string msg)
        //{
        //    return MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        //}

        //public static DialogResult msgOkCancel(string msg)
        //{
        //    return MessageBox.Show(msg, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        //}

        //public static DialogResult msgYesNo(string msg)
        //{
        //    return MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        //}

        #endregion

        #region XML 관련
        public static string SerializeObjectToXml<T>(this T obj)
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

            xmlSerializer.Serialize(xmlTextWriter, obj);
            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;

            string xmlString = ByteArrayToStringUtf8(memoryStream.ToArray());

            xmlTextWriter.Close();
            memoryStream.Close();
            memoryStream.Dispose();

            return xmlString;
        }

        public static T DeserializeXmlToObject<T>(this string xml)
        {
            using (MemoryStream memoryStream = new MemoryStream(StringToByteArrayUtf8(xml)))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

                using (StreamReader xmlStreamReader = new StreamReader(memoryStream, Encoding.UTF8))
                {
                    return (T)xmlSerializer.Deserialize(xmlStreamReader);
                }
            }
        }

        #endregion

        /// <summary>
        /// 디렉토리가 비었는지 확인
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        public static string ByteArrayToStringUtf8(this byte[] value)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            return encoding.GetString(value);
        }

        public static byte[] StringToByteArrayUtf8(this string value)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            return encoding.GetBytes(value);
        }

        public static byte[] imageToByteArray(Image imgToResize, Size size)
        {
            try
            {
                Image img = null;
                img = new Bitmap(imgToResize, size);

                using (var ms = new MemoryStream())
                {
                    img.Save(ms, imgToResize.RawFormat);
                    return ms.ToArray();
                }
            }
            catch
            {
                return null;
            }
        }

        public static string GetBase64String(string path)
        {
            using (Image image = Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    //string base64String = Convert.ToBase64String(Encoding.Convert(Encoding.Default, Encoding.UTF8, imageBytes, 0, imageBytes.Length));
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

        /// <summary>
        /// 수신된 Byte array를 파일로 저장한다
        /// </summary>
        /// <param name="fileArray">파일 bytes</param>
        /// <param name="fileName">파일명</param>
        /// <returns>저장 완료된 파일 경로</returns>
        public static bool SaveFile(byte[] fileArray, string folderPath, string fileName)
        {
            try
            {
                string fullPath = folderPath + "\\" + fileName;
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (var writer = new BinaryWriter(stream))
                    {
                        var bytesLeft = fileArray.Length; // assuming array is an array of bytes
                        var bytesWritten = 0;
                        while (bytesLeft > 0)
                        {
                            var chunkSize = Math.Min(1024 * 1000, bytesLeft);
                            writer.Write(fileArray, bytesWritten, chunkSize);
                            bytesWritten += chunkSize;
                            bytesLeft -= chunkSize;

                            //double current = 1;
                            //double total = 1;

                            //int percentage = (int)Math.Round((double)(100 * (bytesWritten / 10000)) / (fileArray.Length / 10000));

                            //string strTest = (current * 100 / total).ToString();
                            //bgw.ReportProgress(percentage);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }

}