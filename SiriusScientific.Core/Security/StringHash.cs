using System;
using System.Numerics;
using System.Text;

namespace SiriusScientific.Core.Security
{
	public static class StringHash
	{
		public static string ToHash(string targetString, int multiplier)
		{			
			return Convert.ToBase64String((new BigInteger(Encoding.UTF8.GetBytes(targetString)) * multiplier).ToByteArray());
		}

		public static string ToString( string hashString, int multiplier )
		{
			return Encoding.UTF8.GetString((new BigInteger(Convert.FromBase64String(hashString)) / multiplier).ToByteArray());
		}
	}
}
