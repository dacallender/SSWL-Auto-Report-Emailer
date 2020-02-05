using System;
using System.Collections.Generic;
using System.Linq;

namespace SiriusScientific.Core.Parser
{
    public abstract class CommandLineParser
    {
		protected Dictionary<string,Func<string,bool>> CommandDictionary{get;set;}

		public bool? ParseCommands(string commandLine)
		{
			string[] commandArray = commandLine.Split(' ');

			string[] parameterArray = new List<string>(commandArray).GetRange(1, commandArray.Count() - 1).ToArray();

			CommandDictionary.TryGetValue(commandArray[0].ToLowerInvariant(), out Func<string, bool> function);

			if (function == null)
			{
				function = Error;

				function(commandLine);

				return true;
			}

			return function?.Invoke(string.Join(" ", parameterArray));
        }

		internal static bool Error(string commandLine)
		{
#if DEBUG
			Console.WriteLine($"Error in command line: {commandLine}");
#endif
			return true;
		}

        protected abstract void InitializeCommands();
    }
}