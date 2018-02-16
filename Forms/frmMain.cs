using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SkypeCC.DAL;
using SkypeCC.Entities;
using SkypeCC.Helpers;
using Message = SkypeCC.Entities.Message;

namespace SkypeCC.Forms
{
    public partial class frmMain : Form
    {

#region Constants

        private const string ProfileDbFile = "main.db";

        private static readonly string _skypeFolderPath = Path.Combine(Environment.GetEnvironmentVariable("APPDATA") ?? "", "Skype");

#endregion

#region Properties

        private string SelectedProfile
        {
            get { return cbProfile.SelectedItem?.ToString(); }
        }

        private string SelectedProfileDbPath
        {
            get
            {
                if (cbProfile.SelectedItem == null) return null;

                string dbName = cbProfile.SelectedItem.ToString();
                string dataBaseFile = Path.Combine(_skypeFolderPath, dbName);
                return Path.Combine(dataBaseFile, ProfileDbFile);
            }
        }

        private User SelectedUser
        {
            get { return lbUsers.SelectedItem as User; }
        }

#endregion

#region Ctor

        public frmMain()
        {
            InitializeComponent();
            GetSkypeUserNames();
        }

#endregion

#region Methods

        private void GetSkypeUserNames()
        {
            foreach (string folder in Directory.GetDirectories(_skypeFolderPath))
            {
                string mainDbFile = Path.Combine(folder, ProfileDbFile);
                if (File.Exists(mainDbFile))
                {
                    cbProfile.Items.Add(Path.GetFileName(folder));
                }
            }

            if (cbProfile.Items.Count > 0)
            {
                cbProfile.SelectedIndex = 0;
            }
        }

        private void ReloadUsersList()
        {
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            try
            {
                lbUsers.DataSource = null;
                if (cbProfile.SelectedItem != null)
                {
                    string dbName = cbProfile.SelectedItem.ToString();
                    string dataBaseFile = Path.Combine(_skypeFolderPath, dbName);
                    dataBaseFile = Path.Combine(dataBaseFile, "main.db");
                    using (UserDAL dal = new UserDAL(dataBaseFile))
                    {
                        lbUsers.DataSource = dal.GetUsers();
                    }
                }
            }
            finally
            {
                Cursor = DefaultCursor;
            }
        }

        private void CbProfileSelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadUsersList();
        }

        private void RefreshChatList(Messages messages)
        {
            lvMessages.BeginUpdate();
            lvMessages.Items.Clear();
            try
            {
                if (messages == null || !messages.Any()) return;

                List<ListViewItem> items = new List<ListViewItem>(messages.Count);
                foreach (Message message in messages)
                {
                    ListViewItem item = new ListViewItem
                    {
                        Tag = message,
                        ImageIndex = message.FromMe ? 0 : 1,
                        UseItemStyleForSubItems = false
                    };
                    Color siBackColor = message.FromMe ? Color.FromArgb(199, 237, 252) : Color.FromArgb(240, 244, 248);
                    item.SubItems.Add(message.DateTime.ToString("dd/MM/yyyy HH:mm:ss")).BackColor = siBackColor;
                    item.SubItems.Add(message.Text ?? "<deleted>").BackColor = siBackColor;
                    items.Add(item);
                }

                lvMessages.Items.AddRange(items.ToArray());
            }
            finally
            {
                lvMessages.EndUpdate();
            }
        }

        private void ReloadChatList()
        {
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            try
            {
                Messages messages = null;
                using (MessagesDAL dal = new MessagesDAL(SelectedProfileDbPath))
                {
                    User user = SelectedUser;
                    if (user != null)
                    {
                        messages = dal.GetMessages(user);
                    }
                    RefreshChatList(messages);
                }
            }
            finally
            {
                Cursor = DefaultCursor;
            }
        }

        private void lbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadChatList();
        }

        private bool CheckSkypeActivityForCurrentProfile()
        {
            Process p = SkypeHelper.GetSkypeActiveProcess(SelectedProfile);
            if (p != null)
            {
                if (MessageBox.Show(string.Format(@"Skype is running for the user '{0}'. You must close Skype before continuing.
Do you want to close Skype automatically?

Click YES to close Skype, NO/CANCEL to break the operation", SelectedProfile), "SkypeCC", 
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return false;
                }
                return SkypeHelper.ShutdownSkypeProcess(p);
            }
            return true;
        }

        private void DeleteSelectedMessages()
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                ListViewItem[] selectedItems = lvMessages.Items.OfType<ListViewItem>().Where(i => i.Checked).ToArray();
                if (!selectedItems.Any()) return;

                if (!CheckSkypeActivityForCurrentProfile() ||
                    MessageBox.Show("Delete selected messages?", "SkypeCC", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                IEnumerable<Message> messages = selectedItems.Select(i => i.Tag).OfType<Message>();
                using (MessagesDAL dal = new MessagesDAL(SelectedProfileDbPath))
                {
                    dal.Delete(messages);
                }

                lvMessages.BeginUpdate();
                Array.ForEach(selectedItems, lvMessages.Items.Remove);
            }
            finally
            {
                lvMessages.EndUpdate();
                Cursor = DefaultCursor;
            }
        }

        private void btnMessagesDeleteSelected_Click(object sender, EventArgs e)
        {
            DeleteSelectedMessages();
        }

        private void ClearAllMessages()
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                User user = SelectedUser;
                if (user == null || lvMessages.Items.Count == 0) return;

                if (!CheckSkypeActivityForCurrentProfile() ||
                    MessageBox.Show("Delete all messages?", "SkypeCC", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                using (MessagesDAL dal = new MessagesDAL(SelectedProfileDbPath))
                {
                    dal.DeleteAll(user);
                }

                lvMessages.BeginUpdate();
                lvMessages.Items.Clear();
            }
            finally
            {
                lvMessages.EndUpdate();
                Cursor = DefaultCursor;
            }
        }

        private void btnMessagesClearAll_Click(object sender, EventArgs e)
        {
            ClearAllMessages();
        }

        private void EditSelectedMessage()
        {
            ListViewItem item = lvMessages.SelectedItems.OfType<ListViewItem>().FirstOrDefault();

            Message message = item?.Tag as Message;
            if (message == null) return;

            if (!CheckSkypeActivityForCurrentProfile())
            {
                return;
            }

            using (frmEditMessage form = new frmEditMessage())
            {
                if (form.Execute(message.Text))
                {
                    string newMessage = form.NewMessage;
                    using (MessagesDAL dal = new MessagesDAL(SelectedProfileDbPath))
                    {
                        dal.EditMessage(message.Id, newMessage);
                    }
                    item.SubItems[2].Text = newMessage;
                    message.SetText(newMessage);
                }
            }
        }

        private void btnMessageEdit_Click(object sender, EventArgs e)
        {
            EditSelectedMessage();
        }

        private void lvMessages_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewItem item = lvMessages.GetItemAt(e.X, e.Y), selectedItem;
            if (item == null) return;

            if (e.Clicks == 2)
            {
                item.Checked = !item.Checked;
                return;
            }

            if (ModifierKeys == Keys.Control)
            {
                item.Checked = !item.Checked;
                return;
            }

            if (ModifierKeys == Keys.Shift &&
                (selectedItem = lvMessages.SelectedItems.OfType<ListViewItem>().FirstOrDefault()) != null &&
                item != selectedItem)
            {
                bool setChecked = !selectedItem.Checked;
                selectedItem.Checked = setChecked;

                int idx = item.Index;
                int increment = selectedItem.Index > item.Index ? 1 : -1;
                while (item != selectedItem)
                {
                    item.Checked = setChecked;
                    item = lvMessages.Items[idx += increment];
                }
            }
        }

        private void lvMessages_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                case Keys.F3:
                {
                    EditSelectedMessage();
                    break;
                }
                case Keys.Delete:
                {
                    DeleteSelectedMessages();
                    break;
                }
            }
        }

        private void lvMessages_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = lvMessages.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                EditSelectedMessage();
            }
        }

#endregion

    }
}
