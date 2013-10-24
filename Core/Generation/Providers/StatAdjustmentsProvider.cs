using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;

namespace NPCGen.Core.Generation.Providers
{
    public class StatAdjustmentsProvider : IStatAdjustmentsProvider
    {
        private IStatAdjustmentXmlParser statAdjustmentXmlParser;

        public StatAdjustmentsProvider(IStatAdjustmentXmlParser statAdjustmentXmlParser)
        {
            this.statAdjustmentXmlParser = statAdjustmentXmlParser;
        }

        public Dictionary<String, Int32> GetAdjustments(Race race)
        {
            var adjustments = new Dictionary<String, Int32>();

            var strengthAdjustments = statAdjustmentXmlParser.Parse("StrengthStatAdjustments.xml");
            var constitutionAdjustments = statAdjustmentXmlParser.Parse("ConstitutionStatAdjustments.xml");
            var dexterityAdjustments = statAdjustmentXmlParser.Parse("DexterityStatAdjustments.xml");
            var intelligenceAdjustments = statAdjustmentXmlParser.Parse("IntelligenceStatAdjustments.xml");
            var wisdomAdjustments = statAdjustmentXmlParser.Parse("WisdomStatAdjustments.xml");
            var charismaAdjustments = statAdjustmentXmlParser.Parse("CharismaStatAdjustments.xml");

            adjustments.Add(StatConstants.Strength, strengthAdjustments[race.BaseRace] + strengthAdjustments[race.Metarace]);
            adjustments.Add(StatConstants.Constitution, constitutionAdjustments[race.BaseRace] + constitutionAdjustments[race.Metarace]);
            adjustments.Add(StatConstants.Dexterity, dexterityAdjustments[race.BaseRace] + dexterityAdjustments[race.Metarace]);
            adjustments.Add(StatConstants.Intelligence, intelligenceAdjustments[race.BaseRace] + intelligenceAdjustments[race.Metarace]);
            adjustments.Add(StatConstants.Wisdom, wisdomAdjustments[race.BaseRace] + wisdomAdjustments[race.Metarace]);
            adjustments.Add(StatConstants.Charisma, charismaAdjustments[race.BaseRace] + charismaAdjustments[race.Metarace]);

            return adjustments;
        }
    }
}