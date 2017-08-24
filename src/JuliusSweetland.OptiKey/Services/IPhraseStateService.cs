using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuliusSweetland.OptiKey.Services
{
    public interface IPhraseStateService : INotifyPropertyChanged
    {
        int PhrasesShown { get; set; }
        List<string> Phrases { get; set; }
        int PhraseNumber { get; set; }
        Random Random { get; set; }
    }
}
