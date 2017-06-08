using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Feats
{
    public class FeatCollections
    {
        public IEnumerable<Feat> Racial { get; set; }
        public IEnumerable<Feat> Class { get; set; }
        public IEnumerable<Feat> Additional { get; set; }

        public IEnumerable<Feat> All
        {
            get
            {
                var allFeats = Racial.Union(Class).Union(Additional);
                var clonedFeats = allFeats.Select(f => f.Clone()).ToArray(); //INFO: Have to execute immediately, or it re-clones the feats which breaks references
                var combinedFeats = CombineFeats(clonedFeats);

                return combinedFeats;
            }
        }

        public FeatCollections()
        {
            Racial = Enumerable.Empty<Feat>();
            Class = Enumerable.Empty<Feat>();
            Additional = Enumerable.Empty<Feat>();
        }

        private IEnumerable<Feat> CombineFeats(IEnumerable<Feat> allFeats)
        {
            var featsToCombine = allFeats.Where(f => CanCombine(f, allFeats)).ToArray();
            var featsToRemove = new List<Feat>();
            var combinedFeatNames = new List<string>();

            foreach (var featToCombine in featsToCombine)
            {
                if (combinedFeatNames.Contains(featToCombine.Name))
                    continue;

                var combinableFeats = featsToCombine.Where(f => CanCombine(featToCombine, featsToCombine));
                if (combinableFeats.Count() < 2)
                    continue;

                var otherFeats = Enumerable.Empty<Feat>();

                if (combinableFeats.Any(f => f.Frequency.TimePeriod == FeatConstants.Frequencies.Constant))
                {
                    var featToKeep = combinableFeats.First(f => f.Frequency.TimePeriod == FeatConstants.Frequencies.Constant);
                    otherFeats = combinableFeats.Except(new[] { featToKeep });
                }
                else if (combinableFeats.Any(f => f.Frequency.TimePeriod == FeatConstants.Frequencies.AtWill))
                {
                    var featToKeep = combinableFeats.First(f => f.Frequency.TimePeriod == FeatConstants.Frequencies.AtWill);
                    otherFeats = combinableFeats.Except(new[] { featToKeep });
                }
                else
                {
                    otherFeats = combinableFeats.Except(new[] { featToCombine });
                    featToCombine.Frequency.Quantity = combinableFeats.Sum(f => f.Frequency.Quantity);
                }

                featsToRemove.AddRange(otherFeats);
                combinedFeatNames.Add(featToCombine.Name);
            }

            var featsWithRemovableStrengths = allFeats.Where(f => CanRemovePower(f, allFeats));
            combinedFeatNames.Clear();

            foreach (var featToRemove in featsWithRemovableStrengths)
            {
                if (combinedFeatNames.Contains(featToRemove.Name))
                    continue;

                var removableFeats = featsWithRemovableStrengths.Where(f => f.Name == featToRemove.Name);

                var maxPower = removableFeats.Max(f => f.Power);
                var featToKeep = removableFeats.First(f => f.Power == maxPower);
                var otherFeats = removableFeats.Except(new[] { featToKeep });

                featsToRemove.AddRange(otherFeats);
                combinedFeatNames.Add(featToRemove.Name);
            }

            if (allFeats.Any(f => f.Foci.Contains(FeatConstants.Foci.All)))
            {
                var featsWithAllFocus = allFeats.Where(f => f.Foci.Contains(FeatConstants.Foci.All));
                var featNamesWithAllFocus = featsWithAllFocus.Where(f => !f.CanBeTakenMultipleTimes).Select(f => f.Name);

                var redundantFeats = allFeats.Where(f => featNamesWithAllFocus.Contains(f.Name) && f.Foci.Contains(FeatConstants.Foci.All) == false);
                featsToRemove.AddRange(redundantFeats);

                foreach (var feat in featsWithAllFocus)
                    feat.Foci = new[] { FeatConstants.Foci.All };
            }

            var remainingFeats = allFeats.Except(featsToRemove);
            var featsWithCombinableFoci = remainingFeats.Where(f => CanCombineFoci(f, remainingFeats));
            var combinedFeatNamesAndPowers = new Dictionary<string, List<int>>();

            foreach (var featToKeep in featsWithCombinableFoci)
            {
                if (combinedFeatNamesAndPowers.ContainsKey(featToKeep.Name) == false)
                    combinedFeatNamesAndPowers[featToKeep.Name] = new List<int>();

                if (combinedFeatNamesAndPowers[featToKeep.Name].Contains(featToKeep.Power))
                    continue;

                combinedFeatNamesAndPowers[featToKeep.Name].Add(featToKeep.Power);

                var removableFeats = featsWithCombinableFoci.Where(f => f.Name == featToKeep.Name && f.Power == featToKeep.Power);

                foreach (var featToRemove in removableFeats)
                {
                    var matchingFoci = featToKeep.Foci.Intersect(featToRemove.Foci);
                    if (matchingFoci.Any())
                        continue;

                    featToKeep.Foci = featToKeep.Foci.Union(featToRemove.Foci);
                    featsToRemove.Add(featToRemove);
                }
            }

            return allFeats.Except(featsToRemove);
        }

        private bool CanCombineFoci(Feat feat, IEnumerable<Feat> allFeats)
        {
            if (feat.Frequency.TimePeriod != string.Empty)
                return false;

            if (feat.Foci.Any() == false || feat.Foci.Contains(FeatConstants.Foci.All))
                return false;

            var count = allFeats.Count(f => f.Name == feat.Name
                                        && f.Foci.Any()
                                        && f.Foci.Contains(FeatConstants.Foci.All) == false
                                        && f.Power == feat.Power
                                        && f.Frequency.TimePeriod == string.Empty);

            return count > 1;
        }

        private bool CanRemovePower(Feat feat, IEnumerable<Feat> allFeats)
        {
            if (feat.Frequency.TimePeriod != string.Empty)
                return false;

            if (feat.CanBeTakenMultipleTimes)
                return false;

            var count = allFeats.Count(f => f.Name == feat.Name
                                        && f.Foci.Except(feat.Foci).Any() == false
                                        && f.Frequency.TimePeriod == string.Empty);

            return count > 1;
        }

        private bool CanCombine(Feat feat, IEnumerable<Feat> allFeats)
        {
            if (feat.Frequency.TimePeriod == string.Empty)
                return false;

            var count = allFeats.Count(f => f.Name == feat.Name
                                        && f.Foci.Except(feat.Foci).Any() == false
                                        && f.Power == feat.Power
                                        && FrequenciesCanCombine(f.Frequency, feat.Frequency));

            return count > 1;
        }

        private bool FrequenciesCanCombine(Frequency firstFrequency, Frequency secondFrequency)
        {
            return firstFrequency.TimePeriod == secondFrequency.TimePeriod
                || firstFrequency.TimePeriod == FeatConstants.Frequencies.AtWill
                || secondFrequency.TimePeriod == FeatConstants.Frequencies.AtWill
                || firstFrequency.TimePeriod == FeatConstants.Frequencies.Constant
                || secondFrequency.TimePeriod == FeatConstants.Frequencies.Constant;
        }
    }
}