using CharacterGen.Common.Races;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Selectors.Domain
{
    public class LanguageCollectionsSelector : ILanguageCollectionsSelector
    {
        private ICollectionsSelector innerSelector;

        public LanguageCollectionsSelector(ICollectionsSelector innerSelector)
        {
            this.innerSelector = innerSelector;
        }

        public IEnumerable<String> SelectAutomaticLanguagesFor(Race race, String className)
        {
            var baseRaceLanguages = innerSelector.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, race.BaseRace);
            var metaraceLanguages = innerSelector.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, race.Metarace);
            var classLanguages = innerSelector.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, className);

            return baseRaceLanguages.Union(metaraceLanguages).Union(classLanguages);
        }

        public IEnumerable<String> SelectBonusLanguagesFor(String baseRace, String className)
        {
            var baseRaceLanguages = innerSelector.SelectFrom(TableNameConstants.Set.Collection.BonusLanguages, baseRace);
            var classLanguages = innerSelector.SelectFrom(TableNameConstants.Set.Collection.BonusLanguages, className);

            return baseRaceLanguages.Union(classLanguages);
        }
    }
}