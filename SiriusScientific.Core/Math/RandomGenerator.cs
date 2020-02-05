using System;

namespace SiriusScientific.Core.Math
{
	public class RandomGenerator
	{
		public static double RandomDouble(double minRange, double maxRange, int seed)
		{
			//System.DateTime.Now.GetHashCode()
			return minRange + new Random(seed).NextDouble() * ( maxRange - minRange );
		}

		public static int RandomInteger(int minRange, int maxRange, int seed)
		{
			return new Random(seed).Next(minRange, maxRange);
		}
	}
}
