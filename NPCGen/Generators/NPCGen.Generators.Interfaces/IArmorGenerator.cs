using System;
using EquipmentGen.Common.Items;

namespace NPCGen.Generators.Interfaces
{
    public interface IArmorGenerator
    {
        Item GenerateAtLevel(Int32 level);
    }
}