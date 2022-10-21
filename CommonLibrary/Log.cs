using System;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace CommonLibrary
{
    public class Log
    {
        protected string _fileName = "";
        private object locker = new Object();

        public BindingList<LogItem> source = new BindingList<LogItem>();

        /// <param name="fileName">파일명칭</param>
        public Log(string fileName = "")
        {
            _fileName = fileName;
        }

        private static Log _instance;
        public static Log Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Log();

                return _instance;
            }
        }

        public string LogPath
        {
            get
            {
                return System.Windows.Forms.Application.StartupPath + "\\Log\\"
                    + DateTime.Now.ToString("yyyy") + "\\Log" + DateTime.Now.ToString("yyyyMMdd").ToString() + ((_fileName.Trim() == "") ? "" : "_" + _fileName) + ".log";
            }
        }

        public void Write(string message, bool isError = false, bool addSource = true)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] {message}");
                WirteToFile(sb);
                if (addSource) addToSource(message, isError);
            }
            catch
            {
                return;
            }
        }

        public void Write(Exception Ex, bool addSource = true)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("-------------------------------------------------------------------");
                sb.AppendLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "] Exception");
                sb.AppendLine("[Exception Message]");
                sb.AppendLine(Ex.Message.ToString());
                if (Ex.StackTrace != null)
                {
                    sb.AppendLine("[Trace]");
                    sb.AppendLine(Ex.StackTrace.ToString());
                }
                sb.AppendLine("-------------------------------------------------------------------");

                WirteToFile(sb);
                if (addSource) addToSource("[ERROR] " + Ex.Message, true);
            }
            catch
            {
                return;
            }
        }

        private void WirteToFile(StringBuilder LogData)
        {
            try
            {
                lock (locker)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(LogPath));

                    using (StreamWriter sw = new StreamWriter(LogPath, true, Encoding.UTF8))
                    {
                        sw.Write(LogData);
                        sw.Flush();
                    }
                }
            }
            catch
            {
            }
        }

        private void addToSource(string pMsg, bool isError = false)
        {
            try
            {
                source.Insert(0, (new LogItem() { dt = DateTime.Now.ToLongTimeString(), msg = pMsg, isError = isError }));
                if (source.Count > 100)
                {
                    source.RemoveAt(source.Count -1);
                }
            }
            catch
            {
                return;
            }
        }
    }

    public class LogItem
    {
        public bool isError { get; set; }
        public string dt { get; set; }
        public string msg { get; set; }
    }
}