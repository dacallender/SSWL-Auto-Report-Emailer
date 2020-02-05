using System.Collections.Concurrent;
using System.Linq;

namespace SiriusScientific.Core.Containers
{
	public class ReverseConcurrentDictionary<T1, T2> : ConcurrentDictionary<T1, T2>
	{
		public void TryGetKey(T2 value, out T1 key)
		{
			key = this.FirstOrDefault(x => Equals(x.Value, value)).Key;
		}
	}
}
