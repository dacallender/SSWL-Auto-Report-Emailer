using System;
using System.Collections.Generic;
using SiriusScientific.Core.Containers;

namespace SiriusScientific.Core.Parser
{
	public abstract class CommandParameterParser
	{
		protected Dictionary<string, Func<string, INodeParams, bool>> ParameterDictionary { get; set; }

		public bool? ParseParameter(string parameterString, ref INodeParams parameterDetails)
		{
			string[] parameterArray = parameterString.Split(':');

			ParameterDictionary.TryGetValue(parameterArray[0], out Func<string, INodeParams, bool> function);

			return function?.Invoke(parameterArray[1], parameterDetails);
		}

		public INodeParams ParseParameters<T>(string parameterString) where T : new()
		{
			string[] parameterArray = parameterString.Split(' ');

			INodeParams parameters = new T() as INodeParams;

			foreach (string argument in parameterArray)
			{
				if (ParseParameter(argument, ref parameters) == false)
				{
					return null;
				}
			}
			return parameters;
		}
		protected abstract void InitializeParameters();
	}
}
