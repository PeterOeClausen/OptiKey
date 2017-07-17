using LogAnalyserUW.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using System.Threading.Tasks;
using LogAnalyserUW.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LogAnalyserUW
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.ViewModel = new MainPage_ViewModel();
        }

        public MainPage_ViewModel ViewModel { get; set; }

        private async void BrowseButtonClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("BrowseButtonClick CHANGED2!");
            var picker = new Windows.Storage.Pickers.FileOpenPicker(); //Later to be changed to folder picker
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            picker.FileTypeFilter.Add(".csv");
            //picker.FileTypeFilter.Add(".jpeg");
            //picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                Debug.WriteLine("User has picked: " + file.Name);

                if(file.Name.StartsWith("ScratchPadLog"))
                {
                    ReadScratchPadLog(file);
                }
            }
            else
            {
                Debug.WriteLine("User cancelled.");
            }
        }

        private void ReadFile(Windows.Storage.StorageFile file)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            //Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("sample.txt");
            //string[] lines = File.ReadAllLines(file.Path);
            Debug.WriteLine("Done");
        }

        private async void ReadScratchPadLog(Windows.Storage.StorageFile file)
        {
            IList<string> lines = await Windows.Storage.FileIO.ReadLinesAsync(file);
            List <TimeAndWords> timeAndWords = new List<TimeAndWords>();
            foreach (string line in lines.Skip(2))
            {
                Debug.WriteLine(line);
                string[] values = line.Split(',');
                string time = values[0];
                string[] words = values[1].ToString().Split(' ');
                timeAndWords.Add(new TimeAndWords(DateTimeStringConverter.StringToDateTime(time), words));
            }

            DateTime startTime = timeAndWords.First().Time;
            DateTime endTime = timeAndWords.Last().Time;


            //double wpm = 
            //Overvej at lave det baseret på KeySelectionLog.
            //Overvej også at lave cpm (Characters per minute).
        }
    }
}
