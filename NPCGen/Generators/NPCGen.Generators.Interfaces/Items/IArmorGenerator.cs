using System;
using EquipmentGen.Common.Items;

namespace NPCGen.Generators.Interfaces.Items
{
    public interface IArmorGenerator
    {
        Item GenerateAtLevel(Int32 level);
    }
}