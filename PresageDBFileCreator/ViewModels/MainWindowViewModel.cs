using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace PresageDBFileCreator.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string inputFilePath;
        public string InputFilePath {
            get { return inputFilePath; }
            set {
                SetProperty(ref inputFilePath, value);
                if (!inputFilePath.Equals(null)) IsAbleToConvertFile = true;
                if (!inputFilePath.Equals(null)) {
                    StringBuilder sb = new StringBuilder();
                    foreach(string s in File.ReadAllLines(inputFilePath, Encoding.UTF8))
                    {
                        sb.AppendLine(s);
                    }
                    FileContent = sb.ToString();
                }
            }
        }

        private string fileContent;
        public string FileContent
        {
            get { return fileContent; }
            set
            {
                SetProperty(ref fileContent, value);
            }
        }

        private bool isAbleToConvertFile;
        public bool IsAbleToConvertFile
        {
            get { return isAbleToConvertFile; }
            set
            {
                SetProperty(ref isAbleToConvertFile, value);
            }
        }

        public MainWindowViewModel()
        {

        }
    }
}
