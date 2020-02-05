using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace SiriusScientific.Core.Containers
{
	[Serializable]
	[ComVisible(false)]
	public sealed class DuplicateDictionary<TK, TV> : IEnumerable<KeyValuePair<TK, TV>>, ICollection<KeyValuePair<TK, TV>>, IDuplicateDictionary<TK, TV>, IEnumerable, ICollection, ISerializable, IDeserializationCallback
	{
		// Fields

		private Dictionary<TK, List<TV>> _dict;

		// Properties

		public IEqualityComparer<TK> Comparer
		{
			get { return _dict.Comparer; }
		}

		public int Count
		{
			get
			{
				int count = 0;
				foreach (List<TV> list in _dict.Values)
				{
					count += list.Count;
				}
				return count;
			}
		}

		public int KeyCount
		{

			get { return _dict.Keys.Count; }

		}

		public ICollection<TK> Keys
		{
			get { return _dict.Keys; }
		}

		public ICollection<List<TV>> ValueLists
		{
			get { return _dict.Values; }
		}

		public IEnumerable<TV> Values
		{
			get
			{
				foreach (TK key in _dict.Keys)
				{
					foreach (TV value in _dict[key])
					{
						yield return value;
					}
				}
			}
		}

		public List<TV> this[TK key]
		{
			get { return _dict[key]; }
			set { _dict[key] = new List<TV>(value); }
		}


		public TV this[TK key, int index]
		{
			get
			{
				List<TV> list = _dict[key];
				if (index < 0 || index >= list.Count)
				{
					throw new ArgumentException("Index out of range for key");
				}
				return list[index];
			}

			set
			{
				if (_dict.ContainsKey(key))
				{
					List<TV> list = _dict[key];

					if (index < 0 || index > list.Count)
					{
						throw new ArgumentException("Index out of range for key");
					}
					else if (index == list.Count)
					{
						list.Add(value);
					}
					else
					{
						list[index] = value;
					}
				}
				else if (index == 0)
				{
					List<TV> list = new List<TV>();
					list.Add(value);
					_dict.Add(key, list);
				}
				else
				{
					throw new ArgumentException("Index out of range for key");
				}
			}
		}

		// Constructors

		public DuplicateDictionary()
		{
			_dict = new Dictionary<TK, List<TV>>();
		}

		public DuplicateDictionary(IDictionary<TK, TV> dictionary)
		{
			_dict = new Dictionary<TK, List<TV>>();
			foreach (TK key in dictionary.Keys)
			{
				List<TV> list = new List<TV>();
				list.Add(dictionary[key]);
				_dict.Add(key, list);
			}
		}

		public DuplicateDictionary(IDuplicateDictionary<TK, TV> duplicateDictionary)
		{
			_dict = new Dictionary<TK, List<TV>>();
			foreach (TK key in duplicateDictionary.Keys)
			{
				_dict.Add(key, new List<TV>(duplicateDictionary[key]));
			}
		}

		public DuplicateDictionary(IEqualityComparer<TK> comparer)
		{
			_dict = new Dictionary<TK, List<TV>>(comparer);
		}

		public DuplicateDictionary(int capacity)
		{
			_dict = new Dictionary<TK, List<TV>>(capacity);
		}

		public DuplicateDictionary(IDictionary<TK, TV> dictionary, IEqualityComparer<TK> comparer)
		{
			_dict = new Dictionary<TK, List<TV>>(comparer);
			foreach (TK key in dictionary.Keys)
			{
				List<TV> list = new List<TV>();
				list.Add(dictionary[key]);
				_dict.Add(key, list);
			}
		}

		public DuplicateDictionary(IDuplicateDictionary<TK, TV> duplicateDictionary, IEqualityComparer<TK> comparer)
		{
			_dict = new Dictionary<TK, List<TV>>(comparer);
			foreach (TK key in duplicateDictionary.Keys)
			{
				_dict.Add(key, new List<TV>(duplicateDictionary[key]));
			}
		}

		public DuplicateDictionary(int capacity, IEqualityComparer<TK> comparer)
		{
			_dict = new Dictionary<TK, List<TV>>(capacity, comparer);
		}

		private DuplicateDictionary(SerializationInfo info, StreamingContext context)
		{
			if (info == null) return;
			_dict = (Dictionary<TK, List<TV>>)info.GetValue("InternalDictionary", typeof(Dictionary<TK, List<TV>>));
		}

		// Methods

		public void Add(TK key, TV value)
		{
			if (_dict.ContainsKey(key))
			{
				List<TV> list = _dict[key];
				list.Add(value);
			}
			else
			{
				List<TV> list = new List<TV>();
				list.Add(value);
				_dict.Add(key, list);
			}
		}

		public void Add(KeyValuePair<TK, TV> keyValuePair)
		{
			this.Add(keyValuePair.Key, keyValuePair.Value);
		}

		public void AddList(TK key, List<TV> valueList)
		{
			if (_dict.ContainsKey(key))
			{
				List<TV> list = _dict[key];
				foreach (TV val in valueList) list.Add(val);
			}
			else
			{
				_dict.Add(key, new List<TV>(valueList));
			}
		}

		public void AddRange(IEnumerable<KeyValuePair<TK, TV>> keyValuePairs)
		{
			foreach (KeyValuePair<TK, TV> kvp in keyValuePairs) this.Add(kvp.Key, kvp.Value);
		}

		public bool ChangeValue(TK key, TV oldValue, TV newValue)
		{
			if (_dict.ContainsKey(key))
			{
				List<TV> list = _dict[key];

				for (int i = 0; i < list.Count; i++)
				{
					if (Object.Equals(list[i], oldValue))
					{
						list[i] = newValue;
						return true;
					}
				}
			}
			return false;
		}

		public bool ChangeValueAt(TK key, int index, TV newValue)
		{
			if (_dict.ContainsKey(key))
			{
				List<TV> list = _dict[key];

				if (index < 0 || index >= list.Count)
				{
					return false;
				}
				else
				{
					list[index] = newValue;
					return true;
				}
			}
			return false;
		}

		public void Clear()
		{
			_dict.Clear();
		}

		public bool Contains(TK key, TV value)
		{
			if (_dict.ContainsKey(key))
			{
				List<TV> list = _dict[key];
				foreach (TV val in list)
				{
					if (Object.Equals(val, value)) return true;
				}
			}
			return false;
		}

		public bool Contains(KeyValuePair<TK, TV> keyValuePair)
		{
			return this.Contains(keyValuePair.Key, keyValuePair.Value);
		}

		public bool ContainsKey(TK key)
		{
			return _dict.ContainsKey(key);
		}

		public bool ContainsValue(TV value)
		{
			TK firstKey;
			return ContainsValue(value, out firstKey);
		}

		public bool ContainsValue(TV value, out TK firstKey)
		{
			foreach (TK key in _dict.Keys)
			{
				foreach (TV val in _dict[key])
				{
					if (Object.Equals(val, value))
					{
						firstKey = key;
						return true;
					}
				}
			}
			firstKey = default(TK);
			return false;
		}

		public void CopyTo(KeyValuePair<TK, TV>[] array, int index)
		{
			if (array == null) throw new ArgumentNullException();
			if (index < 0) throw new ArgumentOutOfRangeException();
			if (array.Length < index + this.Count) throw new ArgumentException();
			int i = index;
			foreach (KeyValuePair<TK, TV> kvp in this)
			{
				array[i++] = kvp;
			}
		}

		public IEnumerable<KeyValuePair<TK, int>> FindKeyIndexPairs(TV value)
		{
			foreach (TK key in _dict.Keys)
			{
				List<TV> list = _dict[key];
				for (int i = 0; i < list.Count; i++)
				{
					if (Object.Equals(list[i], value))
					{
						yield return new KeyValuePair<TK, int>(key, i);
					}
				}
			}
		}

		public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
		{
			foreach (TK key in _dict.Keys)
			{
				List<TV> list = _dict[key];
				foreach (TV value in list)
				{
					yield return new KeyValuePair<TK, TV>(key, value);
				}
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null) return;
			info.AddValue("InternalDictionary", _dict);
		}

		public int GetValueCount(TK key)
		{
			if (_dict.ContainsKey(key)) return _dict[key].Count;
			return 0;
		}

		public int IndexOfValue(TK key, TV value)
		{
			if (_dict.ContainsKey(key))
			{
				List<TV> list = _dict[key];

				for (int i = 0; i < list.Count; i++)
				{
					if (Object.Equals(list[i], value)) return i;
				}
			}
			return -1;
		}

		public void OnDeserialization(object sender)
		{
			// nothing to do
		}

		public bool Remove(TK key, TV value)
		{
			int count = this.GetValueCount(key);
			if (count == 0) return false;
			for (int i = 0; i < count; i++)
			{
				TV val = _dict[key][i];
				if (Object.Equals(val, value))
				{
					if (count == 1)
					{
						_dict.Remove(key);
					}
					else
					{
						_dict[key].RemoveAt(i);
					}
					return true;
				}
			}
			return false;
		}

		public bool Remove(KeyValuePair<TK, TV> keyValuePair)
		{
			return this.Remove(keyValuePair.Key, keyValuePair.Value);
		}

		public bool RemoveAt(TK key, int index)
		{
			int count = this.GetValueCount(key);
			if (count == 0 || index < 0 || index >= count) return false;
			if (count == 1)
			{
				_dict.Remove(key);
			}
			else
			{
				List<TV> list = _dict[key];
				list.RemoveAt(index);
			}
			return true;
		}

		public bool RemoveKey(TK key)
		{
			int count = this.GetValueCount(key);
			if (count > 0)
			{
				_dict.Remove(key);
				return true;
			}
			return false;
		}

		public bool TryGetValueList(TK key, out List<TV> valueList)
		{
			return _dict.TryGetValue(key, out valueList);
		}

		public bool TryGetValueAt(TK key, int index, out TV value)
		{
			if (_dict.ContainsKey(key) && index >= 0 && index < _dict[key].Count)
			{
				value = _dict[key][index];
				return true;
			}
			else
			{
				value = default(TV);
				return false;
			}
		}

		// Explicit Property Implementations

		bool ICollection<KeyValuePair<TK, TV>>.IsReadOnly
		{
			get { return false; }
		}

		bool ICollection.IsSynchronized
		{
			get { return false; }
		}

		object ICollection.SyncRoot
		{
			get { return ((ICollection)_dict).SyncRoot; }
		}

		// Explicit Method Implementations

		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyTo((KeyValuePair<TK, TV>[])array, index);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
