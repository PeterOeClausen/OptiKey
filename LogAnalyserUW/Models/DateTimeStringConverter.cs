using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalyserUW.Models
{
    public class DateTimeStringConverter
    {
        public static DateTime StringToDateTime(string str)
        {
            string[] values = str.Split('-');
            int[] intValues = new int[7];
            for(int i = 0; i<values.Length; i++)
            {
                intValues[i] = int.Parse(values[i]);
            }

            return new DateTime(intValues[0], intValues[1], intValues[2], intValues[3], intValues[4], intValues[5], intValues[6]);
        }
    }
}
