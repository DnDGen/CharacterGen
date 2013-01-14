using NPCGen.Roll;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPCGen.Equipment
{
    class Treasure
    {
        public static String Money(Int32 level)
        {
            var cash = Coin(level);
            var gem = Goods(level);

            if (String.IsNullOrEmpty(cash) || String.IsNullOrEmpty(gem))
                return cash + gem;
            return String.Format("{0}, {1}", cash, gem);
            
        }

        private static String Coin(Int32 level)
        {
            var roll = Dice.Percentile();

            switch (level)
            {
                case 1:
                    if (roll < 15)
                        return String.Empty;
                    if (roll < 30)
                        return String.Format("{0} cp", Dice.d6() * 1000);
                    if (roll < 53)
                        return String.Format("{0} sp", Dice.d8() * 100);
                    if (roll < 96)
                        return String.Format("{0} gp", Dice.Roll(2, 8, 0) * 10);
                    return String.Format("{0} pp", Dice.d4() * 10);
                case 2:
                    if (roll < 14)
                        return String.Empty;
                    if (roll < 24)
                        return String.Format("{0} cp", Dice.d10() * 1000);
                    if (roll < 44)
                        return String.Format("{0} sp", Dice.Roll(2, 10, 0) * 100);
                    if (roll < 96)
                        return String.Format("{0} gp", Dice.Roll(4, 10, 0) * 10);
                    return String.Format("{0} pp", Dice.Roll(2, 8, 0) * 10);
                case 3:
                    if (roll < 12)
                        return String.Empty;
                    if (roll < 22)
                        return String.Format("{0} cp", Dice.Roll(2, 10, 0) * 1000);
                    if (roll < 42)
                        return String.Format("{0} sp", Dice.Roll(4, 8, 0) * 100);
                    if (roll < 96)
                        return String.Format("{0} gp", Dice.d4() * 100);
                    return String.Format("{0} pp", Dice.d10() * 10);
                case 4:
                    if (roll < 12)
                        return String.Empty;
                    if (roll < 22)
                        return String.Format("{0} cp", Dice.Roll(3, 10, 0) * 1000);
                    if (roll < 42)
                        return String.Format("{0} sp", Dice.Roll(4, 12, 0) * 1000);
                    if (roll < 96)
                        return String.Format("{0} gp", Dice.d6() * 100);
                    return String.Format("{0} pp", Dice.d8() * 10);
                case 5:
                    if (roll < 11)
                        return String.Empty;
                    if (roll < 20)
                        return String.Format("{0} cp", Dice.d4() * 10000);
                    if (roll < 39)
                        return String.Format("{0} sp", Dice.d6() * 1000);
                    if (roll < 96)
                        return String.Format("{0} gp", Dice.d8() * 100);
                    return String.Format("{0} pp", Dice.d10() * 10);
                case 6:
                    if (roll < 11)
                        return String.Empty;
                    if (roll < 19)
                        return String.Format("{0} cp", Dice.d6() * 10000);
                    if (roll < 38)
                        return String.Format("{0} sp", Dice.d8() * 1000);
                    if (roll < 96)
                        return String.Format("{0} gp", Dice.d10() * 100);
                    return String.Format("{0} pp", Dice.d12() * 10);
                case 7:
                    if (roll < 12)
                        return String.Empty;
                    if (roll < 19)
                        return String.Format("{0} cp", Dice.d10() * 10000);
                    if (roll < 36)
                        return String.Format("{0} sp", Dice.d12() * 1000);
                    if (roll < 94)
                        return String.Format("{0} gp", Dice.Roll(2, 6, 0) * 100);
                    return String.Format("{0} pp", Dice.Roll(3, 4, 0) * 10);
                case 8:
                    if (roll < 11)
                        return String.Empty;
                    if (roll < 16)
                        return String.Format("{0} cp", Dice.d12() * 10000);
                    if (roll < 30)
                        return String.Format("{0} sp", Dice.Roll(2, 6, 0) * 1000);
                    if (roll < 88)
                        return String.Format("{0} gp", Dice.Roll(2, 8, 0) * 100);
                    return String.Format("{0} pp", Dice.Roll(3, 6, 0) * 10);
                case 9:
                    if (roll < 11)
                        return String.Empty;
                    if (roll < 16)
                        return String.Format("{0} cp", Dice.Roll(2, 6, 0) * 10000);
                    if (roll < 30)
                        return String.Format("{0} sp", Dice.Roll(2, 8, 0) * 1000);
                    if (roll < 86)
                        return String.Format("{0} gp", Dice.Roll(5, 4, 0) * 100);
                    return String.Format("{0} pp", Dice.Roll(2, 12, 0) * 10);
                case 10:
                    if (roll < 11)
                        return String.Empty;
                    if (roll < 25)
                        return String.Format("{0} sp", Dice.Roll(2, 10, 0) * 1000);
                    if (roll < 80)
                        return String.Format("{0} gp", Dice.Roll(6, 4, 0) * 100);
                    return String.Format("{0} pp", Dice.Roll(5, 6, 0) * 10);
                case 11:
                    if (roll < 9)
                        return String.Empty;
                    if (roll < 15)
                        return String.Format("{0} sp", Dice.Roll(3, 10, 0) * 1000);
                    if (roll < 76)
                        return String.Format("{0} gp", Dice.Roll(4, 8, 0) * 1000);
                    return String.Format("{0} pp", Dice.Roll(4, 10, 0) * 10);
                case 12:
                    if (roll < 9)
                        return String.Empty;
                    if (roll < 15)
                        return String.Format("{0} sp", Dice.Roll(3, 12, 0) * 1000);
                    if (roll < 76)
                        return String.Format("{0} gp", Dice.d4() * 1000);
                    return String.Format("{0} pp", Dice.d4() * 100);
                case 13:
                    if (roll < 9)
                        return String.Empty;
                    if (roll < 76)
                        return String.Format("{0} gp", Dice.d4() * 1000);
                    return String.Format("{0} pp", Dice.d10() * 100);
                case 14:
                    if (roll < 9)
                        return String.Empty;
                    if (roll < 76)
                        return String.Format("{0} gp", Dice.d6() * 1000);
                    return String.Format("{0} pp", Dice.d12() * 100);
                case 15:
                    if (roll < 4)
                        return String.Empty;
                    if (roll < 75)
                        return String.Format("{0} gp", Dice.d8() * 1000);
                    return String.Format("{0} pp", Dice.Roll(3, 4, 0) * 100);
                case 16:
                    if (roll < 4)
                        return String.Empty;
                    if (roll < 75)
                        return String.Format("{0} gp", Dice.d12() * 1000);
                    return String.Format("{0} pp", Dice.Roll(3, 4, 0) * 100);
                case 17:
                    if (roll < 4)
                        return String.Empty;
                    if (roll < 69)
                        return String.Format("{0} gp", Dice.Roll(3, 4, 0) * 1000);
                    return String.Format("{0} pp", Dice.Roll(2, 10, 0) * 100);
                case 18:
                    if (roll < 3)
                        return String.Empty;
                    if (roll < 66)
                        return String.Format("{0} gp", Dice.Roll(3, 6, 0) * 1000);
                    return String.Format("{0} pp", Dice.Roll(5, 4, 0) * 100);
                case 19:
                    if (roll < 3)
                        return String.Empty;
                    if (roll < 66)
                        return String.Format("{0} gp", Dice.Roll(3, 8, 0) * 1000);
                    return String.Format("{0} pp", Dice.Roll(3, 10, 0) * 100);
                default:
                    if (roll < 3)
                        return String.Empty;
                    if (roll < 66)
                        return String.Format("{0} gp", Dice.Roll(4, 8, 0) * 1000);
                    return String.Format("{0} pp", Dice.Roll(4, 10, 0) * 100);
            }
        }

        private static String Goods(Int32 level)
        {
            var Roll = Dice.Percentile();

            switch (level)
            {
                case 1:
                    if (Roll < 91)
                        return String.Empty;
                    if (Roll < 96)
                        return Gems(1);
                    return ArtObject(1);
                case 2:
                    if (Roll < 82)
                        return String.Empty;
                    else if (Roll < 96)
                        return Gems(Dice.Roll(1, 3, 0));
                    else
                        return ArtObject(1);
                case 3:
                    if (Roll < 78)
                        return String.Empty;
                    else if (Roll < 96)
                        return Gems(Dice.Roll(1, 3, 0));
                    else
                        return ArtObject(1);
                case 4:
                    if (Roll < 71)
                        return String.Empty;
                    else if (Roll < 96)
                        return Gems(Dice.d4());
                    else
                        return ArtObject(1);
                case 5:
                    if (Roll < 61)
                        return String.Empty;
                    else if (Roll < 96)
                        return Gems(Dice.d4());
                    else
                        return ArtObject(1);
                case 6:
                    if (Roll < 57)
                        return String.Empty;
                    else if (Roll < 93)
                        return Gems(Dice.d4());
                    else
                        return ArtObject(1);
                case 7:
                    if (Roll < 49)
                        return String.Empty;
                    else if (Roll < 89)
                        return Gems(Dice.d4());
                    else
                        return ArtObject(1);
                case 8:
                    if (Roll < 46)
                        return String.Empty;
                    else if (Roll < 86)
                        return Gems(Dice.d4());
                    else
                        return ArtObject(1);
                case 9:
                    if (Roll < 41)
                        return String.Empty;
                    else if (Roll < 81)
                        return Gems(Dice.d4());
                    else
                        return ArtObject(1);
                case 10:
                    if (Roll < 36)
                        return String.Empty;
                    else if (Roll < 80)
                        return Gems(Dice.d4());
                    else
                        return ArtObject(1);
                case 11:
                    if (Roll < 25)
                        return String.Empty;
                    else if (Roll < 75)
                        return Gems(Dice.d4());
                    else
                        return ArtObject(1);
                case 12:
                    if (Roll < 18)
                        return String.Empty;
                    else if (Roll < 71)
                        return Gems(Dice.d4());
                    else
                        return ArtObject(1);
                case 13:
                    if (Roll < 12)
                        return String.Empty;
                    else if (Roll < 67)
                        return Gems(Dice.d4());
                    else
                        return ArtObject(1);
                case 14:
                    if (Roll < 12)
                        return String.Empty;
                    else if (Roll < 67)
                        return Gems(Dice.d4());
                    else
                        return ArtObject(1);
                case 15:
                    if (Roll < 10)
                        return String.Empty;
                    else if (Roll < 66)
                        return Gems(Dice.d4());
                    else
                        return ArtObject(1);
                case 16:
                    if (Roll < 8)
                        return String.Empty;
                    else if (Roll < 65)
                        return Gems(Dice.d4());
                    else
                        return ArtObject(1);
                case 17:
                    if (Roll < 5)
                        return String.Empty;
                    else if (Roll < 64)
                        return Gems(Dice.d4());
                    else
                        return ArtObject(1);
                case 18:
                    if (Roll < 5)
                        return String.Empty;
                    else if (Roll < 55)
                        return Gems(Dice.d4());
                    else
                        return ArtObject(1);
                case 19:
                    if (Roll < 4)
                        return String.Empty;
                    else if (Roll < 51)
                        return Gems(Dice.d4());
                    else
                        return ArtObject(1);
                default:
                    if (Roll < 3)
                        return String.Empty;
                    else if (Roll < 39)
                        return Gems(Dice.d4());
                    else
                        return ArtObject(1);
            }
        }

        public static String Gems(Int32 quantity)
        {
            var output = String.Empty;
            String gem;

            do
            {
                switch (Dice.Percentile())
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                    case 24:
                    case 25:
                        var price = Dice.Roll(4, 4, 0);
                        switch (Dice.d12())
                        {
                            case 1: gem = "a banded agate"; break;
                            case 2: gem = "an eye agate"; break;
                            case 3: gem = "a moss agate"; break;
                            case 4: gem = "an azurite"; break;
                            case 5: gem = "a blue quartz"; break;
                            case 6: gem = "a hematite"; break;
                            case 7: gem = "a lapis lazuli"; break;
                            case 8: gem = "a malachite"; break;
                            case 9: gem = "an obsidian"; break;
                            case 10: gem = "a rhodochrosite"; break;
                            case 11: gem = "a tiger eye turquoise"; break;
                            default: gem = "an irregular freshwater pearl"; break;
                        }
                        output += String.Format("{0} ({1} gp), ", gem, price);
                        break;
                    case 26:
                    case 27:
                    case 28:
                    case 29:
                    case 30:
                    case 31:
                    case 32:
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                    case 37:
                    case 38:
                    case 39:
                    case 40:
                    case 41:
                    case 42:
                    case 43:
                    case 44:
                    case 45:
                    case 46:
                    case 47:
                    case 48:
                    case 49:
                    case 50:
                        price = Dice.Roll(2, 4, 0) * 10;
                        switch (Dice.Roll(1, 17, 0))
                        {
                            case 1: gem = "a bloodstone"; break;
                            case 2: gem = "a carnelian"; break;
                            case 3: gem = "a chalcedony"; break;
                            case 4: gem = "a chrysoprase"; break;
                            case 5: gem = "a citrine"; break;
                            case 6: gem = "an iolite"; break;
                            case 7: gem = "a jasper"; break;
                            case 8: gem = "a moonstone"; break;
                            case 9: gem = "an onyx"; break;
                            case 10: gem = "a peridot"; break;
                            case 11: gem = "a clear quartz rock crystal"; break;
                            case 12: gem = "a sard"; break;
                            case 13: gem = "a sardonyx"; break;
                            case 14: gem = "a rose quartz"; break;
                            case 15: gem = "a smoky quartz"; break;
                            case 16: gem = "a star quartz"; break;
                            default: gem = "a zircon"; break;
                        }
                        output += String.Format("{0} ({1} gp), ", gem, price);
                        break;
                    case 51:
                    case 52:
                    case 53:
                    case 54:
                    case 55:
                    case 56:
                    case 57:
                    case 58:
                    case 59:
                    case 60:
                    case 61:
                    case 62:
                    case 63:
                    case 64:
                    case 65:
                    case 66:
                    case 67:
                    case 68:
                    case 69:
                    case 70:
                        price = Dice.Roll(4, 4, 0) * 10;
                        switch (Dice.Roll(1, 16, 0))
                        {
                            case 1: gem = "an amber"; break;
                            case 2: gem = "an amethyst"; break;
                            case 3: gem = "a chrysoberyl"; break;
                            case 4: gem = "a coral"; break;
                            case 5: gem = "a red garnet"; break;
                            case 6: gem = "a brown-green garnet"; break;
                            case 7: gem = "a jade"; break;
                            case 8: gem = "a jet"; break;
                            case 9: gem = "a white pearl"; break;
                            case 10: gem = "a golden pearl"; break;
                            case 11: gem = "a pink pearl"; break;
                            case 12: gem = "a silver pearl"; break;
                            case 13: gem = "a red spinel"; break;
                            case 14: gem = "a red-brown spinel"; break;
                            case 15: gem = "a deep green spinel"; break;
                            default: gem = "a tourmaline"; break;
                        }
                        output += String.Format("{0} ({1} gp), ", gem, price);
                        break;
                    case 91:
                    case 92:
                    case 93:
                    case 94:
                    case 95:
                    case 96:
                    case 97:
                    case 98:
                    case 99:
                        price = Dice.Roll(4, 4, 0) * 100;
                        switch (Dice.d10())
                        {
                            case 1: gem = "an emerald"; break;
                            case 2: gem = "a white opal"; break;
                            case 3: gem = "a black opal"; break;
                            case 4: gem = "a fire opal"; break;
                            case 5: gem = "a blue sapphire"; break;
                            case 6: gem = "a fiery yellow corundum"; break;
                            case 7: gem = "a rich purple corundum"; break;
                            case 8: gem = "a blue star sapphire"; break;
                            case 9: gem = "a black star sapphire"; break;
                            default: gem = "a star ruby"; break;
                        }
                        output += String.Format("{0} ({1} gp), ", gem, price);
                        break;
                    case 100:
                        price = Dice.Roll(2, 4, 0) * 1000;
                        switch (Dice.Roll(1, 7, 0))
                        {
                            case 1: gem = "a clearest bright green emerald"; break;
                            case 2: gem = "a blue-white diamond"; break;
                            case 3: gem = "a canary diamond"; break;
                            case 4: gem = "a pink diamond"; break;
                            case 5: gem = "a brown diamond"; break;
                            case 6: gem = "a blue diamond"; break;
                            default: gem = "a jacinth"; break;
                        }
                        output += String.Format("{0} ({1} gp), ", gem, price);
                        break;
                    default:
                        price = Dice.Roll(2, 4, 0) * 100;
                        switch (Dice.d6())
                        {
                            case 1: gem = "an alexandrite"; break;
                            case 2: gem = "an aquamarine"; break;
                            case 3: gem = "a violet garnet"; break;
                            case 4: gem = "a black pearl"; break;
                            case 5: gem = "a deep blue spinel"; break;
                            default: gem = "a golden yellow topaz"; break;
                        }
                        output += String.Format("{0} ({1} gp), ", gem, price);
                        break;
                }
            } while (quantity-- > 0);

            return output;
        }

        private static String ArtObject(Int32 Quantity)
        {
            var output = String.Empty;

            while (Quantity-- > 0)
                output += ArtObject();

            return output;
        }

        private static String ArtObject()
        {
            String art;

            switch (Dice.Percentile())
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    var price = Dice.d10() * 10;
                    switch (Dice.d4())
                    {
                        case 1: art = "a silver ewer"; break;
                        case 2: art = "a carved bone statuette"; break;
                        case 3: art = "an ivory statuette"; break;
                        default: art = "a finely-wrought small gold bracelet"; break;
                    }
                    return String.Format("{0} ({1} gp), ", art, price);
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                    price = Dice.d6() * 100;
                    if (Dice.Roll(1, 2, 0) == 1)
                        art = "a large well-done wool tapestry";
                    else
                        art = "a brass mug with jade inlays";
                    return String.Format("{0} ({1} gp), ", art, price);
                case 41:
                case 42:
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48:
                case 49:
                case 50:
                    price = Dice.d10() * 100;
                    if (Dice.Roll(1, 2, 0) == 1)
                        art = "a silver comb with moonstones";
                    else
                        art = "a silver-plated steel longsword with a jet jewel in the hilt";
                    return String.Format("{0} ({1} gp), ", art, price);
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                case 60:
                    price = Dice.Roll(2, 6, 0) * 100;
                    if (Dice.Roll(1, 2, 0) == 1)
                        art = "a carved harp of exotic wood and with ivory inlay and zircon gems";
                    else
                        art = "a solid gold idol (10 lb.)";
                    return String.Format("{0} ({1} gp), ", art, price);
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 67:
                case 68:
                case 69:
                case 70:
                    price = Dice.Roll(3, 6, 0) * 100;
                    switch (Dice.Roll(1, 3, 0))
                    {
                        case 1: art = "a gold dragon comb with red garnet eye"; break;
                        case 2: art = "a gold and topaz bottle stopper cork"; break;
                        default: art = "a ceremonial electrum dagger with a star ruby in the pommel"; break;
                    }
                    return String.Format("{0} ({1} gp), ", art, price);
                case 71:
                case 72:
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                case 80:
                    price = Dice.Roll(4, 6, 0) * 100;
                    switch (Dice.Roll(1, 3, 0))
                    {
                        case 1: art = "an eyepatch with a mock eye of sapphire and moonstone"; break;
                        case 2: art = "a fire opal pendant on a fine gold chain"; break;
                        default: art = "an old masterpiece painting"; break;
                    }
                    return String.Format("{0} ({1} gp), ", art, price);
                case 81:
                case 82:
                case 83:
                case 84:
                case 85:
                    price = Dice.Roll(5, 6, 0) * 100;
                    if (Dice.Roll(1, 2, 0) == 1)
                        art = "an embroidered silk and velvet mantle with numerous moonstones";
                    else
                        art = "a sapphire pendant on a gold chain";
                    return String.Format("{0} ({1} gp), ", art, price);
                case 86:
                case 87:
                case 88:
                case 89:
                case 90:
                    price = Dice.d4() * 1000;
                    switch (Dice.Roll(1, 3, 0))
                    {
                        case 1: art = "an embroidered and bejeweled glove"; break;
                        case 2: art = "a jeweled anklet"; break;
                        default: art = "a gold music box"; break;
                    }
                    return String.Format("{0} ({1} gp), ", art, price);
                case 91:
                case 92:
                case 93:
                case 94:
                case 95:
                    price = Dice.d6() * 1000;
                    if (Dice.Roll(1, 2, 0) == 1)
                        art = "a golden circlet with four aquamarines";
                    else
                        art = "a necklace of small pink pearls";
                    return String.Format("{0} ({1} gp), ", art, price);
                case 96:
                case 97:
                case 98:
                case 99:
                    price = Dice.Roll(2, 4, 0) * 1000;
                    if (Dice.Roll(1, 2, 0) == 1)
                        art = "a jeweled gold crown";
                    else
                        art = "a jeweled electrum ring";
                    return String.Format("{0} ({1} gp), ", art, price);
                case 100:
                    price = Dice.Roll(2, 6, 0) * 1000;
                    if (Dice.Roll(1, 2, 0) == 1)
                        art = "a gold and ruby ring";
                    else
                        art = "a gold cup set with emeralds";
                    return String.Format("{0} ({1} gp), ", art, price);
                default:
                    price = Dice.Roll(3, 6, 0) * 10;
                    switch (Dice.Roll(1, 3, 0))
                    {
                        case 1: art = "cloth of gold vestments"; break;
                        case 2: art = "a black velvet mask with numerous citrines"; break;
                        default: art = "a silver chalice with lapis lazuli gems"; break;
                    }
                    return String.Format("{0} ({1} gp), ", art, price);
            }
        }

        public static String MundaneItems()
        {
            var output = String.Empty;

            if (Dice.Percentile() > 80)
                output += MundaneCombatItems();
            if (Dice.Percentile() > 80)
                output += "\n" + Clothing();
            if (Dice.Percentile() > 80)
                output += "\n" + Food();
            if (Dice.Percentile() > 80)
                output += "\n" + MiscellaneousEquipment();
            if (Dice.Percentile() > 90)
                output += "\n" + Animals();

            return output;

        }

        private static String MundaneCombatItems()
        {
            switch (Dice.Roll(1, 29, 0))
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5: return String.Format("{0} flasks of alchemist's fire", Dice.d4());
                case 6:
                case 7:
                case 8:
                case 9:
                case 10: return String.Format("{0} flasks of acid", Dice.Roll(2, 4, 0));
                case 11:
                case 12: return String.Format("{0} smokesticks", Dice.d4());
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18: return String.Format("{0} flasks of holy water", Dice.d4());
                case 19:
                case 20: return String.Format("{0} thunderstones", Dice.d4());
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27: return String.Format("{0} vials of antitoxin", Dice.d4());
                case 28:
                case 29: return String.Format("{0} tanglefoot bags", Dice.d4());
                default: return "[ERROR: Mundane combat item out of range. Treasure.765]";
            }
        }

        private static String Clothing()
        {
            var roll = Dice.d4();
            var output = String.Empty;
            var Waist = false;
            var Head = false; 
            var Feet = false; 
            var Chest = false; 
            var Body = false; 
            var Legs = false;
            var Hands = false;
            var Cloak = false;

            do
            {
                switch (Dice.Percentile() / 2)
                {
                    case 0:
                    case 1:
                        if (!Waist)
                        {
                            Waist = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "belt";
                        } break;
                    case 2:
                    case 3:
                        if (!Feet)
                        {
                            Feet = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "soft boots";
                        } break;
                    case 4:
                        if (!Feet)
                        {
                            Feet = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "riding boots";
                        } break;
                    case 5:
                    case 6:
                        if (!Legs)
                        {
                            Legs = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "breeches";
                        } break;
                    case 7:
                        if (!Head)
                        {
                            Head = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "hat";
                        } break;
                    case 8:
                        if (!Head)
                        {
                            Head = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "cap";
                        } break;
                    case 9:
                    case 10:
                        if (!Cloak)
                        {
                            Cloak = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "good cloth cloak";
                        } break;
                    case 11:
                        if (!Cloak)
                        {
                            Cloak = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "fine fur cloak";
                        } break;
                    case 12:
                    case 13:
                        if (!Waist)
                        {
                            Waist = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "girdle";
                        } break;
                    case 14:
                    case 15:
                        if (!Hands)
                        {
                            Hands = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "gloves";
                        } break;
                    case 16:
                    case 17:
                        if (!Body)
                        {
                            Body = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "common gown";
                        } break;
                    case 18:
                    case 19:
                        if (!Legs)
                        {
                            Legs = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "hose";
                        } break;
                    case 20:
                    case 21:
                        if (!output.Contains("weapon case"))
                        {
                            if (output != String.Empty)
                                output += ", ";
                            output += "weapon case (sheath, bow case, etc.)";
                        } break;
                    case 22:
                    case 23:
                        if (!Hands)
                        {
                            Hands = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "mittens";
                        } break;
                    case 24:
                    case 25:
                        if (!output.Contains("pin"))
                        {
                            if (output != String.Empty)
                                output += ", ";
                            output += "pin";
                        } break;
                    case 26:
                    case 27:
                        if (!output.Contains("brooch"))
                        {
                            if (output != String.Empty)
                                output += ", ";
                            output += "plain brooch";
                        } break;
                    case 28:
                    case 29:
                        if (!Body)
                        {
                            Body = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "common robe";
                        } break;
                    case 30:
                        if (!Body)
                        {
                            Body = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "embroidered robe";
                        } break;
                    case 31:
                    case 32:
                        if (!Feet)
                        {
                            Feet = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "sandals";
                        } break;
                    case 33:
                    case 34:
                        if (!output.Contains("sash"))
                        {
                            if (output != String.Empty)
                                output += ", ";
                            output += "sash";
                        } break;
                    case 35:
                    case 36:
                        if (!Feet)
                        {
                            Feet = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "shoes";
                        } break;
                    case 37:
                    case 38:
                        if (!Chest)
                        {
                            Chest = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "silk jacket";
                        } break;
                    case 39:
                    case 40:
                        if (!Chest)
                        {
                            Chest = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "surcoat";
                        } break;
                    case 41:
                    case 42:
                        if (!Chest)
                        {
                            Chest = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "tabard";
                        } break;
                    case 43:
                    case 44:
                        if (!Body)
                        {
                            Body = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "coarse toga";
                        } break;
                    case 45:
                    case 46:
                        if (!Chest)
                        {
                            Chest = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "tunic";
                        } break;
                    case 47:
                    case 48:
                        if (!Chest)
                        {
                            Chest = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "vest";
                        } break;
                    case 49:
                    case 50:
                        if (!Cloak)
                        {
                            Cloak = true;
                            if (output != String.Empty)
                                output += ", ";
                            output += "long coat";
                        } break;
                    default: return "[ERROR: Clothing out of range.  Treasure.793]";
                }
            } while (roll-- > 0);

            return output;
        }

        private static String Food()
        {
            var roll = Dice.d4();
            var output = String.Empty;

            do
            {
                switch (Dice.Percentile() / 4)
                {
                    case 0:
                    case 1:
                        if (output != String.Empty)
                            output += ", ";
                        output += "ale";
                        break;
                    case 2:
                        if (output != String.Empty)
                            output += ", ";
                        output += "bread";
                        break;
                    case 3:
                        if (output != String.Empty)
                            output += ", ";
                        output += "cheese";
                        break;
                    case 4:
                        if (output != String.Empty)
                            output += ", ";
                        output += "common wine";
                        break;
                    case 5:
                        if (output != String.Empty)
                            output += ", ";
                        output += "eggs";
                        break;
                    case 6:
                        if (output != String.Empty)
                            output += ", ";
                        output += "vegetables";
                        break;
                    case 7:
                        if (output != String.Empty)
                            output += ", ";
                        output += "honey";
                        break;
                    case 8:
                        if (output != String.Empty)
                            output += ", ";
                        output += "meat";
                        break;
                    case 9:
                        if (output != String.Empty)
                            output += ", ";
                        output += "beer";
                        break;
                    case 10:
                        if (output != String.Empty)
                            output += ", ";
                        output += "soup";
                        break;
                    case 11:
                        if (output != String.Empty)
                            output += ", ";
                        output += "pickled fish";
                        break;
                    case 12:
                        if (output != String.Empty)
                            output += ", ";
                        output += "butter";
                        break;
                    case 13:
                        if (output != String.Empty)
                            output += ", ";
                        output += "coarse sugar";
                        break;
                    case 14:
                        if (output != String.Empty)
                            output += ", ";
                        output += "firewood";
                        break;
                    case 15:
                        if (output != String.Empty)
                            output += ", ";
                        output += "figs";
                        break;
                    case 16:
                        if (output != String.Empty)
                            output += ", ";
                        output += "herbs";
                        break;
                    case 17:
                        if (output != String.Empty)
                            output += ", ";
                        output += "nuts";
                        break;
                    case 18:
                        if (output != String.Empty)
                            output += ", ";
                        output += "raisins";
                        break;
                    case 19:
                        if (output != String.Empty)
                            output += ", ";
                        output += "rice";
                        break;
                    case 20:
                        if (output != String.Empty)
                            output += ", ";
                        output += "salt";
                        break;
                    case 21:
                        if (output != String.Empty)
                            output += ", ";
                        output += "salted herring";
                        break;
                    case 22:
                        if (output != String.Empty)
                            output += ", ";
                        output += "exotic spices (saffron, clove, etc.)";
                        break;
                    case 23:
                        if (output != String.Empty)
                            output += ", ";
                        output += "uncommon spices (cinnamon, etc.)";
                        break;
                    case 24:
                        if (output != String.Empty)
                            output += ", ";
                        output += "rare spices (pepper, ginger, etc.)";
                        break;
                    case 25:
                        if (output != String.Empty)
                            output += ", ";
                        output += "cider";
                        break;
                    default: return "[ERROR: Food out of range.  Treasure.1049]";
                }
            } while (roll-- > 0);

            return output;
        }

        private static String MiscellaneousEquipment()
        {
            var roll = Dice.d4();
            var output = String.Empty;

            do
            {
                switch (Dice.Percentile() / 2)
                {
                    case 0:
                        if (output != String.Empty)
                            output += ", ";
                        output += "large basket";
                        break;
                    case 1:
                    case 2:
                        if (output != String.Empty)
                            output += ", ";
                        output += "small basket";
                        break;
                    case 3:
                        if (output != String.Empty)
                            output += ", ";
                        output += "bell";
                        break;
                    case 4:
                    case 5:
                        if (output != String.Empty)
                            output += ", ";
                        output += "small belt pouch";
                        break;
                    case 6:
                        if (output != String.Empty)
                            output += ", ";
                        output += "large belt pouch";
                        break;
                    case 7:
                        if (output != String.Empty)
                            output += ", ";
                        output += "writing ink (1 vial)";
                        break;
                    case 8:
                        if (output != String.Empty)
                            output += ", ";
                        output += "bucket";
                        break;
                    case 9:
                    case 10:
                        if (output != String.Empty)
                            output += ", ";
                        output += "light chain (1')";
                        break;
                    case 11:
                        if (output != String.Empty)
                            output += ", ";
                        output += "heavy chain (1')";
                        break;
                    case 12:
                        if (output != String.Empty)
                            output += ", ";
                        output += "common cloth (4 sq ft)";
                        break;
                    case 13:
                        if (output != String.Empty)
                            output += ", ";
                        output += "fine cloth (4 sq ft)";
                        break;
                    case 14:
                        if (output != String.Empty)
                            output += ", ";
                        output += "rich cloth (4 sq ft)";
                        break;
                    case 15:
                        if (output != String.Empty)
                            output += ", ";
                        output += "candle";
                        break;
                    case 16:
                        if (output != String.Empty)
                            output += ", ";
                        output += "chalk";
                        break;
                    case 17:
                        if (output != String.Empty)
                            output += ", ";
                        output += "crampons";
                        break;
                    case 18:
                        if (output != String.Empty)
                            output += ", ";
                        output += "fishhook";
                        break;
                    case 19:
                        if (output != String.Empty)
                            output += ", ";
                        output += "fishing net (10 sq ft)";
                        break;
                    case 20:
                        if (output != String.Empty)
                            output += ", ";
                        output += "glass bottle";
                        break;
                    case 21:
                        if (output != String.Empty)
                            output += ", ";
                        output += "grappling hook";
                        break;
                    case 22:
                        if (output != String.Empty)
                            output += ", ";
                        output += "hourglass";
                        break;
                    case 23:
                        if (output != String.Empty)
                            output += ", ";
                        output += "beacon lantern";
                        break;
                    case 24:
                        if (output != String.Empty)
                            output += ", ";
                        output += "bullseye lantern";
                        break;
                    case 25:
                        if (output != String.Empty)
                            output += ", ";
                        output += "hooded lantern";
                        break;
                    case 26:
                        if (output != String.Empty)
                            output += ", ";
                        output += "good lock";
                        break;
                    case 27:
                        if (output != String.Empty)
                            output += ", ";
                        output += "poor lock";
                        break;
                    case 28:
                        if (output != String.Empty)
                            output += ", ";
                        output += "magnifying glass";
                        break;
                    case 29:
                        if (output != String.Empty)
                            output += ", ";
                        output += "map/scroll case";
                        break;
                    case 30:
                        if (output != String.Empty)
                            output += ", ";
                        output += "small metal mirror";
                        break;
                    case 31:
                        if (output != String.Empty)
                            output += ", ";
                        output += "musical instrument";
                        break;
                    case 32:
                        if (output != String.Empty)
                            output += ", ";
                        output += "greek fire oil (1 flask)";
                        break;
                    case 33:
                        if (output != String.Empty)
                            output += ", ";
                        output += "lamp oil (1 flask)";
                        break;
                    case 34:
                        if (output != String.Empty)
                            output += ", ";
                        output += "paper (1 sheet)";
                        break;
                    case 35:
                        if (output != String.Empty)
                            output += ", ";
                        output += "papyrus (1 sheet)";
                        break;
                    case 36:
                        if (output != String.Empty)
                            output += ", ";
                        output += "parchment (1 sheet)";
                        break;
                    case 37:
                        if (output != String.Empty)
                            output += ", ";
                        output += "perfume (1 vial)";
                        break;
                    case 38:
                        if (output != String.Empty)
                            output += ", ";
                        output += "piton";
                        break;
                    case 39:
                        if (output != String.Empty)
                            output += ", ";
                        output += "silk rope (50')";
                        break;
                    case 40:
                        if (output != String.Empty)
                            output += ", ";
                        output += "large sack";
                        break;
                    case 41:
                        if (output != String.Empty)
                            output += ", ";
                        output += "winter blanket";
                        break;
                    case 42:
                        if (output != String.Empty)
                            output += ", ";
                        output += "small sack";
                        break;
                    case 43:
                        if (output != String.Empty)
                            output += ", ";
                        output += "sealing/candle wax (1 lb)";
                        break;
                    case 44:
                        if (output != String.Empty)
                            output += ", ";
                        output += "sewing needle";
                        break;
                    case 45:
                        if (output != String.Empty)
                            output += ", ";
                        output += "signal whistle";
                        break;
                    case 46:
                        if (output != String.Empty)
                            output += ", ";
                        output += "signet ring or personal seal";
                        break;
                    case 47:
                        if (output != String.Empty)
                            output += ", ";
                        output += "bar of soap";
                        break;
                    case 48:
                        if (output != String.Empty)
                            output += ", ";
                        output += "spyglass";
                        break;
                    case 49:
                        if (output != String.Empty)
                            output += ", ";
                        output += "small tent";
                        break;
                    case 50:
                        if (output != String.Empty)
                            output += ", ";
                        output += "water clock";
                        break;
                    default: return "[ERROR: Miscellaneous Equipment out of range.  Treasure.1192]";
                }
            } while (roll-- > 0);

            return output;
        }

        private static String Animals()
        {
            switch (Dice.Percentile())
            {
                case 1:
                case 2: return "a boar";
                case 3: return "a bull";
                case 4: return "a calf";
                case 5:
                case 6: return "a camel";
                case 7:
                case 8:
                case 9: return "a capon";
                case 10:
                case 11:
                case 12: return "a cat";
                case 13:
                case 14:
                case 15: return "a chicken";
                case 16: return "a cow";
                case 17:
                case 18:
                case 19: return "a hunting dog";
                case 20:
                case 21:
                case 22: return "a guard dog";
                case 23:
                case 24:
                case 25: return "a war dog";
                case 26:
                case 27: return "a donkey";
                case 28:
                case 29: return "a mule";
                case 30:
                case 31: return "an ass";
                case 32: return "a labor elephant";
                case 33: return "a war elephant";
                case 34:
                case 35:
                case 36: return "a trained falcon";
                case 37:
                case 38:
                case 39: return "a goat";
                case 40:
                case 41:
                case 42: return "a goose";
                case 43:
                case 44:
                case 45: return "a guinea hen";
                case 46:
                case 47: return "a draft horse";
                case 48:
                case 49: return "a heavy war horse";
                case 50:
                case 51: return "a light war horse";
                case 52:
                case 53: return "a medium war horse";
                case 54:
                case 55: return "a riding horse";
                case 56: return "a hunting cat (jaguar, etc.)";
                case 57: return "an ox";
                case 58:
                case 59:
                case 60: return "a partridge";
                case 61:
                case 62:
                case 63: return "a peacock";
                case 64:
                case 65:
                case 66: return "a pig";
                case 67:
                case 68:
                case 69: return "a pigeon";
                case 70:
                case 71:
                case 72: return "a homing pigeon";
                case 73:
                case 74:
                case 75: return "a pony";
                case 76:
                case 77:
                case 78: return "a ram";
                case 79:
                case 80:
                case 81: return "a sheep";
                case 82:
                case 83:
                case 84: return "a songbird";
                case 85:
                case 86:
                case 87: return "a swan";
                case 88:
                case 89:
                case 90: return "a duck";
                case 91:
                case 92:
                case 93: return "a toad";
                case 94:
                case 95:
                case 96: return "a frog";
                case 97:
                case 98:
                case 99: return "a mouse";
                case 100: return Animals() + " and " + Animals();
                default: return "[ERROR: Animals out of range.  Treasure.1448]";
            }
        }
    }
}