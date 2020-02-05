using System.Collections.Generic;

namespace SiriusScientific.Core.WCF
{
	public interface IBaseContract
	{
		List<ServiceInfo> GetServiceInfo();
	}
}
