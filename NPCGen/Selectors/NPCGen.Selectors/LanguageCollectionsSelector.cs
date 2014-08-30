﻿using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Races;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Selectors
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
            var baseRaceLanguages = innerSelector.SelectFrom("AutomaticLanguages", race.BaseRace);
            var metaraceLanguages = innerSelector.SelectFrom("AutomaticLanguages", race.Metarace);
            var classLanguages = innerSelector.SelectFrom("AutomaticLanguages", className);

            return baseRaceLanguages.Union(metaraceLanguages).Union(classLanguages);
        }

        public IEnumerable<String> SelectBonusLanguagesFor(String baseRace, String className)
        {
            var baseRaceLanguages = innerSelector.SelectFrom("BonusLanguages", baseRace);
            var classLanguages = innerSelector.SelectFrom("BonusLanguages", className);

            return baseRaceLanguages.Union(classLanguages);
        }
    }
}