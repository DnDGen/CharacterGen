using System;
using System.Collections.Generic;

namespace NPCGen.Common.Abilities.Skills
{
    public static class SkillConstants
    {
        public const String Appraise = "Appraise";
        public const String Balance = "Balance";
        public const String Bluff = "Bluff";
        public const String Climb = "Climb";
        public const String Concentration = "Concentration";
        public const String DecipherScript = "Decipher Script";
        public const String Diplomacy = "Diplomacy";
        public const String DisableDevice = "Disable Device";
        public const String Disguise = "Disguise";
        public const String EscapeArtist = "Escape Artist";
        public const String Forgery = "Forgery";
        public const String GatherInformation = "Gather Information";
        public const String HandleAnimal = "Handle Animal";
        public const String Heal = "Heal";
        public const String Hide = "Hide";
        public const String Intimidate = "Intimidate";
        public const String Jump = "Jump";
        public const String KnowledgeArcana = "Knowledge (Arcana)";
        public const String KnowledgeArchitectureAndEngineering = "Knowledge (Architecture and Engineering)";
        public const String KnowledgeDungeoneering = "Knowledge (Dungeoneering)";
        public const String KnowledgeGeography = "Knowledge (Geography)";
        public const String KnowledgeHistory = "Knowledge (History)";
        public const String KnowledgeLocal = "Knowledge (Local)";
        public const String KnowledgeNature = "Knowledge (Nature)";
        public const String KnowledgeNobilityAndRoyalty = "Knowledge (Nobility and Royalty)";
        public const String KnowledgeReligion = "Knowledge (Religion)";
        public const String KnowledgeThePlanes = "Knowledge (The Planes)";
        public const String Listen = "Listen";
        public const String MoveSilently = "Move Silently";
        public const String OpenLock = "Open Lock";
        public const String Perform = "Perform";
        public const String Ride = "Ride";
        public const String Search = "Search";
        public const String SenseMotive = "Sense Motive";
        public const String SleightOfHand = "Sleight of Hand";
        public const String Spellcraft = "Spellcraft";
        public const String Spot = "Spot";
        public const String Survival = "Survival";
        public const String Swim = "Swim";
        public const String Tumble = "Tumble";
        public const String UseMagicDevice = "Use Magic Device";
        public const String UseRope = "Use Rope";

        public static IEnumerable<String> GetSkills()
        {
            return new[]
            {
                Appraise,
                Balance,
                Bluff,
                Climb,
                Concentration,
                DecipherScript,
                Diplomacy,
                DisableDevice,
                Disguise,
                EscapeArtist,
                Forgery,
                GatherInformation,
                HandleAnimal,
                Heal,
                Hide,
                Intimidate,
                Jump,
                KnowledgeArcana,
                KnowledgeArchitectureAndEngineering,
                KnowledgeDungeoneering,
                KnowledgeGeography,
                KnowledgeHistory,
                KnowledgeLocal,
                KnowledgeNature,
                KnowledgeNobilityAndRoyalty,
                KnowledgeReligion,
                KnowledgeThePlanes,
                Listen,
                MoveSilently,
                OpenLock,
                Perform,
                Ride,
                Search,
                SenseMotive,
                SleightOfHand,
                Spellcraft,
                Spot,
                Survival,
                Swim,
                Tumble,
                UseMagicDevice,
                UseRope
            };
        }
    }
}