using System.Windows.Forms;

namespace SiriusScientific.Mvvm.DialogBase
{
	public interface IDialogResult
	{
		DialogResult ReturnState { get; set; }
		object ReturnValue { get; set; }
	}
}
