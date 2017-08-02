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
        List<string> Phrases { get; set; }
        int PhraseNumber { get; set; }
    }
}
