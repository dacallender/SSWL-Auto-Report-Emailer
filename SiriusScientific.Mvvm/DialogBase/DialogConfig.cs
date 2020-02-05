using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiriusScientific.Mvvm.DialogBase
{
	public class DialogConfig : IDialogConfig
	{
		public bool IsOK { get; set; }
		public bool IsCancel { get; set; }
		public DialogKind DialogKind { get; set; }
		public bool OverrideEnabled { get; set; }
	}
}
