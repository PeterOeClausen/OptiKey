using JuliusSweetland.OptiKey.Observables.TriggerSources;
using JuliusSweetland.OptiKey.UI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuliusSweetland.OptiKey.Services
{
    public class InstanceGetter
    {
        public ITriggerSourceWithTimeToCompleteTrigger triggerSource { get; set; }

        private static InstanceGetter instance;

        public static InstanceGetter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InstanceGetter();
                }
                return instance;
            }
        }
    }
}
