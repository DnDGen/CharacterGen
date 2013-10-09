namespace NPCGen.Core.Generation.Verifiers.Interfaces
{
    public class VerifierCollection
    {
        public IRandomizerVerifier RandomizerVerifier { get; set; }
        public IAlignmentVerifier AlignmentVerifier { get; set; }
        public IBaseRaceVerifier BaseRaceVerifier { get; set; }
        public IClassNameVerifier ClassNameVerifier { get; set; }
        public IMetaraceVerifier MetaraceVerifier { get; set; }
    }
}