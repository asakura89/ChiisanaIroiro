using System;

namespace Plugin.Common {
    internal static class RyandomNumberGenerator {
        // https://en.wikipedia.org/wiki/Feigenbaum_constants
        internal const Int32 Feigenbaum = 46692;

        internal static Int32 TurnToRyandom(this Int32 lowerBound, Int32 upperBound) {
            Int32 seed = Guid.NewGuid().GetHashCode() % Feigenbaum;
            var rnd = new Random(seed);
            return rnd.Next(lowerBound, upperBound);
        }

        internal static Int32 TurnToRyandom(this Int32 upperBound) => 0.TurnToRyandom(upperBound);
    }
}
