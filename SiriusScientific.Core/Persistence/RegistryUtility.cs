using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace SiriusScientific.Core.Persistence
{
	public class RegistryUtility : IRegistryUtility,  IDisposable
	{
		public RegistryHive RegistryHive { get; set; }
		public RegistryView RegistryView { get; set; }

		private RegistryKey _registryKey;

		public RegistryUtility()
		{ }

		public RegistryUtility(RegistryHive registryHive, RegistryView registryView)
		{
			RegistryHive = registryHive;
			RegistryView = registryView;

			_registryKey = RegistryKey.OpenBaseKey(registryHive, registryView);
		}

		public void Open()
		{
			_registryKey = RegistryKey.OpenBaseKey(RegistryHive, RegistryView);
		}

		public void Open(RegistryHive registryHive, RegistryView registryView)
		{
			_registryKey = RegistryKey.OpenBaseKey(registryHive, registryView);
		}

		public void Close()
		{
			_registryKey.Close();
		}

		public string[] ListSubkeys(string path)
		{
			string regPath = path;

			string[] subKeys = null;

			RegistryKey subKey = _registryKey.OpenSubKey(regPath);

			if (subKey != null)
			{
				subKeys = subKey.GetSubKeyNames();

				subKey.Close();
			}

			return subKeys;
		}

		public void EnumerateSubkeys(string path, List<string> keyList)
		{
			RegistryKey subKey = _registryKey.OpenSubKey(path);

			foreach(string sub in subKey.GetSubKeyNames())
			{
				keyList.Add(sub);

				string newPath = path + "\\" + sub;

				EnumerateSubkeys(newPath, keyList); // By recalling itselfit makes sure it get all the subkey names
			}

			subKey.Close();
		}

		public T GetValue<T>(string path, string key)
		{
			object value = null;

			RegistryKey subKey = _registryKey.OpenSubKey(path);

			if (subKey != null)
			{
				value = subKey.GetValue(key);

				subKey.Close();
			}

			return (T)value;
		}

		public void SetValue<T>(string path, string key, T value)
		{
			RegistryKey subKey = _registryKey.OpenSubKey(path, true);

			if (subKey != null)
			{
				subKey.SetValue(key, value);

				subKey.Close();
			}
		}

		public void Dispose()
		{
			_registryKey.Close();
		}
	}
}
