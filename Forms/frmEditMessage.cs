using System.Drawing;
using System.Windows.Forms;

namespace SkypeCC.Forms
{
    public partial class frmEditMessage : Form
    {

#region Static methods

        private static Size _formSize;

#endregion

#region Properties

        public string NewMessage => tbMessage.Text.TrimEnd();

#endregion

#region Ctor

        public frmEditMessage()
        {
            InitializeComponent();
        }

#endregion

#region Class methods

        public bool Execute(string oldMessage)
        {
            tbMessage.Text = oldMessage;
            tbMessage.SelectionStart = tbMessage.Text.Length;
            if (_formSize != default(Size))
            {
                Size = _formSize;
            }
            return ShowDialog() == DialogResult.OK;
        }

        private void frmEditMessage_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                {
                    Close();
                    break;
                }
                case Keys.Enter:
                {
                    if (ModifierKeys != Keys.Control)
                    {
                        DialogResult = DialogResult.OK;
                    }
                    break;
                }
            }
        }

        private void frmEditMessage_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formSize = Size;
        }

#endregion

    }
}
