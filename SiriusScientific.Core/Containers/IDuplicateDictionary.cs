using System.Collections;
using System.Collections.Generic;

namespace SiriusScientific.Core.Containers
{
	public interface IDuplicateDictionary<K, V> : ICollection<KeyValuePair<K, V>>, IEnumerable<KeyValuePair<K, V>>, IEnumerable
	{
		// Properties

		int KeyCount { get; }
		ICollection<K> Keys { get; }
		ICollection<List<V>> ValueLists { get; }
		IEnumerable<V> Values { get; }
		List<V> this[K key] { get; set; }

		// Methods

		void Add(K key, V value);
		void AddList(K key, List<V> valueList);
		bool ChangeValue(K key, V oldvalue, V newValue);
		bool Contains(K key, V value);
		bool ContainsKey(K key);
		int GetValueCount(K key);
		bool Remove(K key, V value);
		bool RemoveKey(K key);
		bool TryGetValueList(K key, out List<V> valueList);
	}
}