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
            Bind<IAdjustmentMapper>().To<AdjustmentXmlMapper>();
            Bind<ILanguagesMapper>().To<LanguagesXmlMapper>();
            Bind<IPercentileMapper>().To<PercentileXmlMapper>();
            Bind<IStatPriorityMapper>().To<StatPriorityXmlMapper>();
        }
    }
}