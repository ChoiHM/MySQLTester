using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace CommonLibrary
{
    public partial class MsgForm : Form
    {
        string _Text = "";
        public string Message
        {
            get { return _Text; }
            set { _Text = value; txtRich.Text = value; }
        }

        protected MessageBoxIcon _messageBoxIcon { get; set; } = MessageBoxIcon.None;
        protected MessageBoxButtons _messageBoxButtons { get; set; } = MessageBoxButtons.OK;
        protected MessageBoxDefaultButton _defaultButton { get; set; } = MessageBoxDefaultButton.Button1;
        protected string _caption { get; set; } = "";
        /// <summary>
        /// 0~6000 sec
        /// </summary>
        protected int _closingTime { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="messageBoxIcon"></param>
        /// <param name="messageBoxButtons"></param>
        /// <param name="defaultButton"></param>
        /// <param name="caption"></param>
        /// <param name="closingTime">자동으로 닫히기까지의 시간(Sec). 표시되는 버튼이 OK 하나일 때만 작동한다.</param>
        public MsgForm(string text, MessageBoxIcon messageBoxIcon, MessageBoxButtons messageBoxButtons, MessageBoxDefaultButton defaultButton, string caption = "Message", int closingTime = 0)
        {
            InitializeComponent();
            Message = text;
            _messageBoxIcon = messageBoxIcon;
            _messageBoxButtons = messageBoxButtons;
            _defaultButton = defaultButton;
            _caption = caption;
            _closingTime = closingTime;
            if (_closingTime < 0) { _closingTime = 0; }
            else if (_closingTime > 6000) { _closingTime = 6000; }

        }

        private void MsgForm_Load(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;

            this.Text = _caption;
            txtRich.Text = _Text;

            switch (_messageBoxIcon)
            {
                case MessageBoxIcon.None:
                    break;
                case MessageBoxIcon.Warning:
                    pic.Image = imageList1.Images[0];
                    break;
                case MessageBoxIcon.Error:
                    pic.Image = imageList1.Images[1];
                    break;
                case MessageBoxIcon.Information:
                    pic.Image = imageList1.Images[2];
                    break;
                case MessageBoxIcon.Question:
                    pic.Image = imageList1.Images[3];
                    break;
                default:
                    break;
            }

            switch (_messageBoxButtons)
            {
                case MessageBoxButtons.OK:
                    button1.Visible = false;
                    button2.Visible = false;
                    button3.Text = "확인";
                    button3.Focus();
                    break;
                case MessageBoxButtons.OKCancel:
                    button1.Visible = false;
                    button2.Text = "확인";
                    button3.Text = "취소";
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    button1.Text = "중단";
                    button2.Text = "재시도";
                    button3.Text = "무시";
                    break;
                case MessageBoxButtons.YesNoCancel:
                    button1.Text = "예";
                    button2.Text = "아니오";
                    button3.Text = "취소";
                    break;
                case MessageBoxButtons.YesNo:
                    button1.Visible = false;
                    button2.Text = "예";
                    button3.Text = "아니오";
                    break;
                case MessageBoxButtons.RetryCancel:
                    button1.Visible = false;
                    button2.Text = "재시도";
                    button3.Text = "취소";
                    break;
                default:
                    button1.Visible = false;
                    button2.Visible = false;
                    button3.Text = "취소";
                    break;
            }

            if (_messageBoxButtons != MessageBoxButtons.YesNoCancel
                && _messageBoxButtons != MessageBoxButtons.AbortRetryIgnore
                && TextRenderer.MeasureText(_Text, txtRich.Font).Width < 170
                )
            {
                this.Width -= 140;
                txtRich.Width -= 140;
                txtRich.Top += 20;
            }

            switch (_defaultButton)
            {
                case MessageBoxDefaultButton.Button1:
                    this.AcceptButton = button1;
                    break;
                case MessageBoxDefaultButton.Button2:
                    this.AcceptButton = button2;
                    break;
                case MessageBoxDefaultButton.Button3:
                    this.AcceptButton = button3;
                    break;
                default:
                    break;
            }

            if (_messageBoxButtons == MessageBoxButtons.OK && _closingTime > 0)
            {
                System.Timers.Timer tmr = new System.Timers.Timer();
                tmr.Interval = 1000 * _closingTime;
                tmr.Elapsed += Tmr_Elapsed;
                tmr.Start();
            }
        }

        private void Tmr_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            switch (_messageBoxButtons)
            {
                case MessageBoxButtons.OK:
                    this.DialogResult = DialogResult.OK;
                    break;
                default:
                    this.DialogResult = DialogResult.None;
                    break;
            }

            this.InvokeIfRequired(() => this.Close());
        }

        private void txtRich_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtRich_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            // 세로 사이즈 조정
            try
            {
                this.SuspendLayout();
                ((RichTextBox)sender).Height = e.NewRectangle.Height + 7;
                if (txtRich.Height >= 400)
                {
                    txtRich.Height = 400;
                    this.Height = txtRich.Height + 120;
                    txtRich.ContentsResized -= txtRich_ContentsResized;
                    txtRich.ScrollBars = RichTextBoxScrollBars.Vertical;
                    txtRich.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.MaximumSize = new System.Drawing.Size(1920, 1080);
                    this.MinimumSize = new Size(450, 200);
                }
                else if (txtRich.Height < 400 && txtRich.Height > 50)
                {
                    this.Height = txtRich.Height + 120;
                }
            }
            catch
            {
                return;
            }
            finally
            {
                this.ResumeLayout();
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            switch (btn.Text.ToLower())
            {
                case "ok":
                case "확인":
                    DialogResult = DialogResult.OK;
                    break;
                case "cancel":
                case "취소":
                    DialogResult = DialogResult.Cancel;
                    break;
                case "yes":
                case "예":
                    DialogResult = DialogResult.Yes;
                    break;
                case "no":
                case "아니오":
                    DialogResult = DialogResult.No;
                    break;
                case "ignore":
                case "무시":
                    DialogResult = DialogResult.Ignore;
                    break;
                case "abort":
                case "중단":
                    DialogResult = DialogResult.Abort;
                    break;
                case "retry":
                case "재시도":
                    DialogResult = DialogResult.Retry;
                    break;
                default:
                    break;
            }
            this.Close();
        }

        private void MsgForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Escape)
            {
                return;
            }
            if (_messageBoxButtons != MessageBoxButtons.YesNoCancel && _messageBoxButtons != MessageBoxButtons.AbortRetryIgnore)
            {
                this.Close();
            }
        }
        private void MsgForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                pic.Image?.Dispose();
                pic.Image = null;
            }
            catch
            {
            }
        }

    }
}
