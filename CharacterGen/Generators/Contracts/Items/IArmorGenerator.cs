using System;
using TreasureGen.Common.Items;

namespace CharacterGen.Generators.Items
{
    public interface IArmorGenerator
    {
        Item GenerateAtLevel(Int32 level);
    }
}