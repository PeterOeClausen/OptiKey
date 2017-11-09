using JuliusSweetland.OptiKey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuliusSweetland.OptiKey.Observables.TriggerSources
{
    public interface ITriggerSourceWithTimeToCompleteTrigger : ITriggerSource
    {
        TimeSpan defaultTimeToCompleteTrigger { get; set; }
        IDictionary<KeyValue, TimeSpan> timeToCompleteTriggerByKey { get; set; }
    }
}
