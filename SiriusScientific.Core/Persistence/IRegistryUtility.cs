using System.Collections.Generic;
using Microsoft.Win32;

namespace SiriusScientific.Core.Persistence
{
	public interface IRegistryUtility
	{
		RegistryHive RegistryHive { get; set; }

		RegistryView RegistryView { get; set; }

		void Open();

		void Close();

		void Open(RegistryHive registryHive, RegistryView registryView);

		string[] ListSubkeys(string path);

		void EnumerateSubkeys(string path, List<string> keyList);

		T GetValue<T>(string path, string key);

		void SetValue<T>(string path, string key, T value);
	}
}