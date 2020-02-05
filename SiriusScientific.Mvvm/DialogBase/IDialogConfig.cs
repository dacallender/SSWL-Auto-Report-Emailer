using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiriusScientific.Mvvm.DialogBase
{
	public interface IDialogConfig
	{
		bool IsOK { get; set; }
		bool IsCancel { get; set; }
		DialogKind DialogKind { get; set; }

		bool OverrideEnabled { get; set; }
	}
}
