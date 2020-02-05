using Newtonsoft.Json;

namespace SiriusScientific.Core.Containers
{
	public class NodeParams : INodeParams
	{
		[JsonIgnore]
		public string StatusMessage { get; set; }

		[JsonIgnore]
		public bool ErrorFlag { get; set; }
	}
}
