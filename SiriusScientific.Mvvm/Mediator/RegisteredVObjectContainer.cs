using System;
using System.Collections.Generic;

namespace SiriusScientific.Mvvm.Mediator
{
	public sealed class RegisteredVObjectContainer<T, K> : Dictionary<T, List<K>>
	{
		private	void EnsureKey(T key)
		{
			if(!ContainsKey(key))
			{
				this[key] = new List<K>(1);
			}
			else
			{
				if (this[key] == null)
					this[key] = new List<K>(1);
			}
		}

		public void AddValue(T key, K newItem)
		{
			EnsureKey(key);

			this[key].Add(newItem);
		}

		public void AddValues(T key, IEnumerable<K> newItems)
		{
			 EnsureKey(key);

			this[key].AddRange(newItems);
		}

		public bool RemoveValue(T key, K value)
		{
			if ( !ContainsKey(key) )
				return false;

			this[key].Remove(value);

			if (this[key].Count == 0)
				this.Remove(key);

			return true;
		}

		public bool RemoveAllValue(T key, Predicate<K> match)
		{
			if ( !ContainsKey(key) )
				return false;

			this[key].RemoveAll(match);

			if (this[key].Count == 0)
				this.Remove(key);

			return true;
		}
	}
}
