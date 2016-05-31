using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Selectors.Collections
{
    internal class LanguageCollectionsSelector : ILanguageCollectionsSelector
    {
        private ICollectionsSelector innerSelector;

        public LanguageCollectionsSelector(ICollectionsSelector innerSelector)
        {
            this.innerSelector = innerSelector;
        }

        public IEnumerable<string> SelectAutomaticLanguagesFor(Race race, string className)
        {
            var baseRaceLanguages = innerSelector.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, race.BaseRace);
            var metaraceLanguages = innerSelector.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, race.Metarace);
            var classLanguages = innerSelector.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, className);

            return baseRaceLanguages.Union(metaraceLanguages).Union(classLanguages);
        }

        public IEnumerable<string> SelectBonusLanguagesFor(string baseRace, string className)
        {
            var baseRaceLanguages = innerSelector.SelectFrom(TableNameConstants.Set.Collection.BonusLanguages, baseRace);
            var classLanguages = innerSelector.SelectFrom(TableNameConstants.Set.Collection.BonusLanguages, className);

            return baseRaceLanguages.Union(classLanguages);
        }
    }
}