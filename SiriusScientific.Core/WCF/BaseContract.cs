// ***********************************************************************
// Assembly         : SiriusScientific.Core
// Author           : David
// Created          : 03-17-2017
//
// Last Modified By : David
// Last Modified On : 03-19-2017
// ***********************************************************************
// <copyright file="BaseContract.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel.Configuration;

/// <summary>
/// The WCF namespace.
/// </summary>
namespace SiriusScientific.Core.WCF
{
	/// <summary>
	/// Class BaseContract.
	/// </summary>
	public class BaseContract : IBaseContract
	{
		/// <summary>
		/// The _service information collection
		/// </summary>
		private List<ServiceInfo> _serviceInfoCollection;

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseContract"/> class.
		/// </summary>
		public BaseContract()
		{
			_serviceInfoCollection = new List<ServiceInfo>();
		}

		/// <summary>
		/// Gets the service information by enumerating all services in the app.config.
		/// </summary>
		/// <returns>List&lt;ServiceInfo&gt;.</returns>
		public List<ServiceInfo> GetServiceInfo()
		{
			_serviceInfoCollection.Clear();

			//TODO: Move string to Resources
			ServicesSection servicesSection = ConfigurationManager.GetSection("system.serviceModel/services") as ServicesSection;

			if (servicesSection != null)
			{
				foreach (ServiceElement service in servicesSection.Services)
				{
					foreach (BaseAddressElement baseAddressElement in service.Host.BaseAddresses)
					{
						foreach (ServiceEndpointElement endpoint in service.Endpoints)
						{
							Type.GetType(service.Name);
							_serviceInfoCollection.Add(new ServiceInfo()
							{
								EndpointAddress = baseAddressElement.BaseAddress,
								ImplementName = service.Name,
								Binding = endpoint.Binding,
								MetadataAddress = endpoint.Address,
								ContractName = endpoint.Contract
							});
						}
					}
				}
			}

			return _serviceInfoCollection;
		}

		public static string ValidateUrl( string url )
		{
			if (url.EndsWith("/")) return url;

			return url + "/";
		}
	}
}
