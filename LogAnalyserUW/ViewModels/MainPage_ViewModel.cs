using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalyserUW.ViewModels
{
    public class MainPage_ViewModel
    {
        public string AllLogsPath { get; set; }

        public MainPage_ViewModel()
        {
            this.AllLogsPath = "No path has been set yet";
        }
    }
}
