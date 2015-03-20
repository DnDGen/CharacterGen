using System;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills
{
    [TestFixture]
    public class SkillGroupsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.SkillGroups; }
        }

        [TestCase(TableNameConstants.Set.Collection.Groups.Knowledge,
            SkillConstants.KnowledgeArcana,
            SkillConstants.KnowledgeArchitectureAndEngineering,
            SkillConstants.KnowledgeDungeoneering,
            SkillConstants.KnowledgeGeography,
            SkillConstants.KnowledgeHistory,
            SkillConstants.KnowledgeLocal,
            SkillConstants.KnowledgeNature,
            SkillConstants.KnowledgeNobilityAndRoyalty,
            SkillConstants.KnowledgeReligion,
            SkillConstants.KnowledgeThePlanes)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}