using System;

namespace SiriusScientific.Mvvm.IoC
{
	public class TypeNotRegisteredException : Exception
	{
		public TypeNotRegisteredException( string message )
			: base(message)
		{
		}
	}
}
