using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiriusScientific.Mvvm.DialogBase
{
	public interface IDialogPassParameter
	{
		IDialogParameter DialogParams { get; set; }
	}
}
