using System;
using NPCGen.Core.Characters.Generation.Randomizers.Alignments;
using NPCGen.Core.Characters.Generation.Randomizers.CharacterClass;
using NPCGen.Core.Characters.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Characters.Generation.Randomizers.Races.Metaraces;

namespace NPCGen.Core.Characters.Generation.Verifiers
{
    public class RandomizerVerifier
    {
        public Boolean Verify(IAlignmentRandomizer alignmentRandomizer, IClassRandomizer classRandomizer,
            IBaseRaceRandomizer raceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            if (alignmentRandomizer is ChaoticAlignment)
            {
                throw new NotImplementedException();
            }
            else if (alignmentRandomizer is EvilAlignment)
            {
                throw new NotImplementedException();
            }
            else if (alignmentRandomizer is GoodAlignment)
            {
                throw new NotImplementedException();
            }
            else if (alignmentRandomizer is LawfulAlignment)
            {
                throw new NotImplementedException();
            }
            else if (alignmentRandomizer is NeutralAlignment)
            {
                throw new NotImplementedException();
            }
            else if (alignmentRandomizer is NonChaoticAlignment)
            {
                throw new NotImplementedException();
            }
            else if (alignmentRandomizer is NonEvilAlignment)
            {
                throw new NotImplementedException();
            }
            else if (alignmentRandomizer is NonGoodAlignment)
            {
                throw new NotImplementedException();
            }
            else if (alignmentRandomizer is NonLawfulAlignment)
            {
                throw new NotImplementedException();
            }
            else if (alignmentRandomizer is NonNeutralAlignment)
            {
                throw new NotImplementedException();
            }

            if (classRandomizer is HealerClass)
            {
                throw new NotImplementedException();
            }
            else if (classRandomizer is MageClass)
            {
                throw new NotImplementedException();
            }
            else if (classRandomizer is NonSpellcasterClass)
            {
                throw new NotImplementedException();
            }
            else if (classRandomizer is SpellcasterClass)
            {
                throw new NotImplementedException();
            }
            else if (classRandomizer is StealthClass)
            {
                throw new NotImplementedException();
            }
            else if (classRandomizer is WarriorClass)
            {
                throw new NotImplementedException();
            }

            return true;
        }
    }
}