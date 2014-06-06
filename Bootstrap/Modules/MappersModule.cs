using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using NPCGen.Mappers;
using NPCGen.Mappers.Interfaces;

namespace NPCGen.Bootstrap.Modules
{
    public class MappersModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAdjustmentXmlParser>().To<AdjustmentXmlParser>();
            Bind<ILanguagesXmlParser>().To<LanguagesXmlParser>();
            Bind<IPercentileXmlParser>().To<PercentileXmlParser>();
            Bind<IStatPriorityXmlParser>().To<StatPriorityXmlParser>();
        }
    }
}