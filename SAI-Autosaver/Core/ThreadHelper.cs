using System;
using System.Windows.Forms;

namespace SAI_Autosaver.Core
{
    static class ThreadHelper
    {
        delegate void SetTextCallback(Form f, Control ctrl, string text);

        public static void SetText(Form form, Control ctrl, string text)
        {
            if (ctrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                form.Invoke(d, new object[] { form, ctrl, text });
            }
            else
            {
                ctrl.Text = text;
            }
        }

        public static void Invoke<T>(T ctrl, Action<T> action) where T : Control
        {
            ctrl.Invoke(new Action(() => action(ctrl)));
        }
    }
}
