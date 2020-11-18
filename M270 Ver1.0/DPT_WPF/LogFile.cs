using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPT_WPF
{
    public class LogFile
    {
        public string Time { get; set; }
        public string Machine { get; set; }
        public string Work { get; set; }
        public string Image
        {
            get;
            set;
        }
    }

    public class WeekFile
    {
        public string WeekImage
        {
            get;
            set;
        }
        public string WeekStartTime { get; set; }
        public string WeekEndTime { get; set; }
        public string WeekWorkTime { get; set; }
        public string WeekFileName { get; set; }
        public string WeekImageetc
        {
            get;
            set;
        }
    }

    public class FolderList
    {
        public string FileName { get; set; }
        public string StartTime { get; set; }
        public string CameraFileCount { get; set; }
        public string SaveImage { get; set; }
        public string DeleteImageP { get; set; }
    }
}
