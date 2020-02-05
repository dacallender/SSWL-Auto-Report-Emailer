using System.Runtime.Serialization;

namespace SiriusScientific.Core.WCF
{
	[DataContract]
	public enum BindingType
	{
		[EnumMember]
		BasicHttpBinding,

		[EnumMember]
		WsHttpBinding,

		[EnumMember]
		WsDualHttpBinding,

		[EnumMember]
		NetTcpBinding,

		[EnumMember]
		NetNamedPipeBinding,

		[EnumMember]
		NetMsmqBinding,

		[EnumMember]
		WsFederationHttpBinding,

		[EnumMember]
		MsmqIntegrationBinding
	}
}
