
using System;
using NPCGen.Core.Generation.Providers.Interfaces;
namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    public abstract class EvilMetarace : BaseMetarace
    {
        public EvilMetarace(IPercentileResultProvider provider) : base(provider) { }

        protected override Boolean MetaraceIsAllowed(String metarace)
        {
            throw new NotImplementedException();
        }
    }
}