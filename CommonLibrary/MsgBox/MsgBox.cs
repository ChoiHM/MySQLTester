using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace CommonLibrary
{
    public class MsgBox
    {
        public static DialogResult Show(string Message, MessageBoxButtons messageBoxButtons = MessageBoxButtons.OK, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, string caption = "Message", int closingTime = 0)
        {
            if (Message.Length > 10000)
            {
                Message = Message.Substring(0, 10000) + Environment.NewLine + ".......(생략)";
            }
            MsgForm frm = new MsgForm(Message, MessageBoxIcon.None, messageBoxButtons, defaultButton, caption, closingTime);
            return frm.ShowDialog();
        }

        public static DialogResult ShowInfo(string Message, MessageBoxButtons messageBoxButtons = MessageBoxButtons.OK, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, string caption = "Message", int closingTime = 0)
        {
            if (Message.Length > 10000)
            {
                Message = Message.Substring(0, 10000) + Environment.NewLine + ".......(생략)";
            }
            MsgForm frm = new MsgForm(Message, MessageBoxIcon.Information, messageBoxButtons, defaultButton, caption, closingTime);
            return frm.ShowDialog();
        }

        public static DialogResult ShowWarning(string Message, MessageBoxButtons messageBoxButtons = MessageBoxButtons.OK, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, string caption = "Message", int closingTime = 0)
        {
            if (Message.Length > 10000)
            {
                Message = Message.Substring(0, 10000) + Environment.NewLine + ".......(생략)";
            }
            MsgForm frm = new MsgForm(Message, MessageBoxIcon.Warning, messageBoxButtons, defaultButton, caption, closingTime);
            return frm.ShowDialog();
        }

        public static DialogResult ShowError(string Message, MessageBoxButtons messageBoxButtons = MessageBoxButtons.OK, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, string caption = "Message", int closingTime = 0)
        {
            if (Message.Length > 10000)
            {
                Message = Message.Substring(0, 10000) + Environment.NewLine + ".......(생략)";
            }
            MsgForm frm = new MsgForm(Message, MessageBoxIcon.Error, messageBoxButtons, defaultButton, caption, closingTime);
            return frm.ShowDialog();
        }
        public static DialogResult ShowQuestion(string Message, MessageBoxButtons messageBoxButtons = MessageBoxButtons.YesNo, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, string caption = "Message")
        {
            if (Message.Length > 10000)
            {
                Message = Message.Substring(0, 10000) + Environment.NewLine + ".......(생략)";
            }
            MsgForm frm = new MsgForm(Message, MessageBoxIcon.Question, messageBoxButtons, defaultButton, caption);
            return frm.ShowDialog();
        }

    }
}
