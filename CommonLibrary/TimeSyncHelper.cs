using System;
using System.Runtime.InteropServices;

namespace CommonLibrary
{
    public class TimeSyncHelper
    {
        [DllImport("kernel32.dll")]
        public static extern bool SetLocalTime(ref SYSTEMTIME time);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public short Year;
            public short Month;
            public short DayOfWeek;
            public short Day;
            public short Hour;
            public short Minute;
            public short Second;
            public short Milliseconds;
        }

        public static void SyncTime(DateTime dateTime)
        {
            SYSTEMTIME st = new SYSTEMTIME();
            st.Year = (short)dateTime.Year;
            st.Month = (short)dateTime.Month;
            st.DayOfWeek = (short)dateTime.DayOfWeek;
            st.Day = (short)dateTime.Day;
            st.Hour = (short)dateTime.Hour;
            st.Minute = (short)dateTime.Minute;
            st.Second = (short)dateTime.Second;
            SetLocalTime(ref st);
        }
    }
}
