using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalyserUW.Models
{
    public class TimeAndWords
    {
        public DateTime Time { get; set; }
        public string[] Words { get; set; }

        public TimeAndWords(DateTime time, string[] words)
        {
            this.Time = time;
            this.Words = words;
        }
    }
}
