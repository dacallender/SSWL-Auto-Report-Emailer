namespace SiriusScientific.Core.Math
{
	public static class Normalize
	{
		public static double NormalizeX(double x, double actualMinX, double actualMaxX, double scaledMinX, double scaledMaxX)
		{
			return   (scaledMaxX - scaledMinX) /(actualMaxX - actualMinX) * (x - actualMinX) + actualMinX;
		}
	}
}
