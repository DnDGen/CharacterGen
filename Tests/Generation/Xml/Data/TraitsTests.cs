using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data
{
    [TestFixture]
    public class TraitsTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "Traits";
        }

        [Test]
        public void DistinctiveScarPercentile()
        {
            AssertContent("Distinctive scar", 1);
        }

        [Test]
        public void MissingToothPercentile()
        {
            AssertContent("Missing tooth", 2);
        }

        [Test]
        public void MissingFingerPercentile()
        {
            AssertContent("Missing finger", 3);
        }

        [Test]
        public void BadBreathPercentile()
        {
            AssertContent("Bad breath", 4);
        }

        [Test]
        public void StrongBodyOdorPercentile()
        {
            AssertContent("Strong body odor", 5);
        }

        [Test]
        public void PleasantSmellingPercentile()
        {
            AssertContent("Pleasant smelling (perfumed)", 6);
        }

        [Test]
        public void SweatyPercentile()
        {
            AssertContent("Sweaty", 7);
        }

        [Test]
        public void HandsShakePercentile()
        {
            AssertContent("Hands shake", 8);
        }

        [Test]
        public void UnusualEyeColorPercentile()
        {
            AssertContent("Unusual eye color", 9);
        }

        [Test]
        public void HackingCoughPercentile()
        {
            AssertContent("Hacking cough", 10);
        }

        [Test]
        public void SneezesAndSnifflesPercentile()
        {
            AssertContent("Sneezes and sniffles", 11);
        }

        [Test]
        public void ParticularlyLowVoicePercentile()
        {
            AssertContent("Particularly low voice", 12);
        }

        [Test]
        public void ParticularlyHighVoicePercentile()
        {
            AssertContent("Particularly high voice", 13);
        }

        [Test]
        public void SlursWordsPercentile()
        {
            AssertContent("Slurs words", 14);
        }

        [Test]
        public void LispsPercentile()
        {
            AssertContent("Lisps", 15);
        }

        [Test]
        public void StuttersPercentile()
        {
            AssertContent("Stutters", 16);
        }

        [Test]
        public void EnunciatesVeryClearlyPercentile()
        {
            AssertContent("Enunciates very clearly", 17);
        }

        [Test]
        public void SpeaksLoudlyPercentile()
        {
            AssertContent("Speaks loudly", 18);
        }

        [Test]
        public void WhispersPercentile()
        {
            AssertContent("Whispers", 19);
        }

        [Test]
        public void HardOfHearingPercentile()
        {
            AssertContent("Hard of hearing", 20);
        }

        [Test]
        public void TattooPercentile()
        {
            AssertContent("Tattoo", 21);
        }

        [Test]
        public void BirthmarkPercentile()
        {
            AssertContent("Birthmark", 22);
        }

        [Test]
        public void UnusualSkinColorPercentile()
        {
            AssertContent("Unusual skin color", 23);
        }

        [Test]
        public void BaldPercentile()
        {
            AssertContent("Bald", 24);
        }

        [Test]
        public void ParticularlyLongHairPercentile()
        {
            AssertContent("Particularly long hair", 25);
        }

        [Test]
        public void EmptyPercentile()
        {
            AssertEmpty(26);
        }

        [Test]
        public void UnusualHairColorPercentile()
        {
            AssertContent("Unusual hair color", 27);
        }

        [Test]
        public void WalksWithALimpPercentile()
        {
            AssertContent("Walks with a limp", 28);
        }

        [Test]
        public void DistinctiveJewelryPercentile()
        {
            AssertContent("Distinctive jewelry", 29);
        }

        [Test]
        public void WearsFlamboyantOrOutlandishClothesPercentile()
        {
            AssertContent("Wears flamboyant or outlandish clothes", 30);
        }

        [Test]
        public void UnderdressedPercentile()
        {
            AssertContent("Underdressed", 31);
        }

        [Test]
        public void OverdressedPercentile()
        {
            AssertContent("Overdressed", 32);
        }

        [Test]
        public void NervousEyeTwitchPercentile()
        {
            AssertContent("Nervous eye twitch", 33);
        }

        [Test]
        public void FiddlesAndFidgetsNervouslyPercentile()
        {
            AssertContent("Fiddles and fidgets nervously", 34);
        }

        [Test]
        public void WhistlesALotPercentile()
        {
            AssertContent("Whistles a lot", 35);
        }

        [Test]
        public void SingsALotPercentile()
        {
            AssertContent("Signs a lot", 36);
        }

        [Test]
        public void FlipsACoinPercentile()
        {
            AssertContent("Flips a coin", 37);
        }

        [Test]
        public void GoodPosturePercentile()
        {
            AssertContent("Good posture", 38);
        }

        [Test]
        public void StoopedBackPercentile()
        {
            AssertContent("Stooped back", 39);
        }

        [Test]
        public void TallPercentile()
        {
            AssertContent("Tall", 40);
        }

        [Test]
        public void ShortPercentile()
        {
            AssertContent("Short", 41);
        }

        [Test]
        public void ThinPercentile()
        {
            AssertContent("Thin", 42);
        }

        [Test]
        public void FatPercentile()
        {
            AssertContent("Fat", 43);
        }

        [Test]
        public void VisibleWoundsOrSoresPercentile()
        {
            AssertContent("Visible wounds or sores", 44);
        }

        [Test]
        public void SquintsPercentile()
        {
            AssertContent("Squints", 45);
        }

        [Test]
        public void StaresOffIntoDistancePercentile()
        {
            AssertContent("Stares off into distance", 46);
        }

        [Test]
        public void FrequentlyChewingSomethingPercentile()
        {
            AssertContent("Frequently chewing something", 47);
        }

        [Test]
        public void DirtyAndUnkemptPercentile()
        {
            AssertContent("Dirty and unkempt", 48);
        }

        [Test]
        public void CleanPercentile()
        {
            AssertContent("Clean", 49);
        }

        [Test]
        public void DistinctiveNosePercentile()
        {
            AssertContent("Distinctive nose", 50);
        }

        [Test]
        public void SelfishPercentile()
        {
            AssertContent("Selfish", 51);
        }

        [Test]
        public void ObsequiousPercentile()
        {
            AssertContent("Obsequious", 52);
        }

        [Test]
        public void DrowsyPercentile()
        {
            AssertContent("Drowsy", 53);
        }

        [Test]
        public void BookishPercentile()
        {
            AssertContent("Bookish", 54);
        }

        [Test]
        public void ObservantPercentile()
        {
            AssertContent("Observant", 55);
        }

        [Test]
        public void NotVeryObservantPercentile()
        {
            AssertContent("Not very observant", 56);
        }

        [Test]
        public void OverlyCriticalPercentile()
        {
            AssertContent("Overly critical", 57);
        }

        [Test]
        public void PassionateArtistOrArtLoverPercentile()
        {
            AssertContent("Passionate artist or art lover", 58);
        }

        [Test]
        public void PassionateHobbyistPercentile()
        {
            AssertContent("Passionate hobbyist (fishing, hunting, gaming, animals, etc.)", 59);
        }

        [Test]
        public void CollectorPercentile()
        {
            AssertContent("Collector (books, trophies, coins, weapons, etc.)", 60);
        }

        [Test]
        public void SkinflintPercentile()
        {
            AssertContent("Skinflint", 61);
        }

        [Test]
        public void SpendthriftPercentile()
        {
            AssertContent("Spendthrift", 62);
        }

        [Test]
        public void PessimistPercentile()
        {
            AssertContent("Pessimist", 63);
        }

        [Test]
        public void OptimistPercentile()
        {
            AssertContent("Optimist", 64);
        }

        [Test]
        public void DrunkardPercentile()
        {
            AssertContent("Drunkard", 65);
        }

        [Test]
        public void TeetotalerPercentile()
        {
            AssertContent("Teetotaler", 66);
        }

        [Test]
        public void WellManneredPercentile()
        {
            AssertContent("Well mannered", 67);
        }

        [Test]
        public void RudePercentile()
        {
            AssertContent("Rude", 68);
        }

        [Test]
        public void JumpyPercentile()
        {
            AssertContent("Jumpy", 69);
        }

        [Test]
        public void FoppishPercentile()
        {
            AssertContent("Foppish", 70);
        }

        [Test]
        public void OverbearingPercentile()
        {
            AssertContent("Overbearing", 71);
        }

        [Test]
        public void AloofPercentile()
        {
            AssertContent("Aloof", 72);
        }

        [Test]
        public void ProudPercentile()
        {
            AssertContent("Proud", 73);
        }

        [Test]
        public void IndividualistPercentile()
        {
            AssertContent("Individualist", 74);
        }

        [Test]
        public void ConformistPercentile()
        {
            AssertContent("Conformist", 75);
        }

        [Test]
        public void HotTemperedPercentile()
        {
            AssertContent("Hot tempered", 76);
        }

        [Test]
        public void EvenTemperedPercentile()
        {
            AssertContent("Even tempered", 77);
        }

        [Test]
        public void NeuroticPercentile()
        {
            AssertContent("Neurotic", 78);
        }

        [Test]
        public void JealousPercentile()
        {
            AssertContent("Jealous", 79);
        }

        [Test]
        public void BravePercentile()
        {
            AssertContent("Brave", 80);
        }

        [Test]
        public void CowardlyPercentile()
        {
            AssertContent("Cowardly", 81);
        }

        [Test]
        public void CarelessPercentile()
        {
            AssertContent("Careless", 82);
        }

        [Test]
        public void CuriousPercentile()
        {
            AssertContent("Curious", 83);
        }

        [Test]
        public void TruthfulPercentile()
        {
            AssertContent("Truthful", 84);
        }

        [Test]
        public void LiarPercentile()
        {
            AssertContent("Liar", 85);
        }

        [Test]
        public void LazyPercentile()
        {
            AssertContent("Lazy", 86);
        }

        [Test]
        public void EnergeticPercentile()
        {
            AssertContent("Energetic", 87);
        }

        [Test]
        public void ReverentOrPiousPercentile()
        {
            AssertContent("Reverent or pious", 88);
        }

        [Test]
        public void IrreverentOrIrreligiousPercentile()
        {
            AssertContent("Irreverent or irreligious", 89);
        }

        [Test]
        public void StrongOpinionsOnPoliticsOrMoralsPercentile()
        {
            AssertContent("Strong opinions on politics or morals", 90);
        }

        [Test]
        public void MoodyPercentile()
        {
            AssertContent("Moody", 91);
        }

        [Test]
        public void CruelPercentile()
        {
            AssertContent("Cruel", 92);
        }

        [Test]
        public void UsesFlowerySpeechOrLongWordsPercentile()
        {
            AssertContent("Uses flowery speech or long words", 93);
        }

        [Test]
        public void UsesTheSamePhraseOverAndOverPercentile()
        {
            AssertContent("Uses the same phrase over and over", 94);
        }

        [Test]
        public void SexistRacistOrOtherwisePrejudicedPercentile()
        {
            AssertContent("Sexist, racist, or otherwise prejudiced", 95);
        }

        [Test]
        public void FascinatedByMagicPercentile()
        {
            AssertContent("Fascinated by magic", 96);
        }

        [Test]
        public void DistrustfulOfMagicPercentile()
        {
            AssertContent("Distrustful of magic", 97);
        }

        [Test]
        public void PrefersMembersOfOneClassOverAllOthersPercentile()
        {
            AssertContent("Prefers members of one class over all others", 98);
        }

        [Test]
        public void JokesterPercentile()
        {
            AssertContent("Jokester", 99);
        }

        [Test]
        public void NoSenseOfHumorPercentile()
        {
            AssertContent("No sense of humor", 100);
        }
    }
}