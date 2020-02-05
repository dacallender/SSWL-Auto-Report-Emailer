using System;
using System.Runtime.Serialization;

namespace SiriusScientific.Core.WCF
{
	[DataContract]
	public class ServiceInfo
	{
		[DataMember]
		public bool IsActive { get; set; }

		[DataMember]
		public string Location { get; set; }

		[DataMember]
		public string ImplementName { get; set; }

		[DataMember]
		public string ContractName { get; set;}

		[DataMember]
		public string ServiceId { get; set; }


		[DataMember]
		public Uri MetadataAddress { get;set; }

		[DataMember]
		public string EndpointAddress { get; set; }

		[DataMember]
		public string Binding { get; set; }
	}
}