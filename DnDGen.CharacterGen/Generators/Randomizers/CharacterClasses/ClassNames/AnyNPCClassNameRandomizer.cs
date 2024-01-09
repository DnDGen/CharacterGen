﻿using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Randomizers.CharacterClasses;
using DnDGen.Infrastructure.Selectors.Collections;
using System.Collections.Generic;

namespace DnDGen.CharacterGen.Generators.Randomizers.CharacterClasses.ClassNames
{
    internal class AnyNPCClassNameRandomizer : IClassNameRandomizer
    {
        private readonly ICollectionSelector collectionsSelector;

        public AnyNPCClassNameRandomizer(ICollectionSelector collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        public string Randomize(Alignment alignment)
        {
            var npcs = GetAllPossibleResults(alignment);
            return collectionsSelector.SelectRandomFrom(npcs);
        }

        public IEnumerable<string> GetAllPossibleResults(Alignment alignment)
        {
            return collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.NPCs);
        }
    }
}
