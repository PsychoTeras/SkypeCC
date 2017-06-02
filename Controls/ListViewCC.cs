using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SkypeCC.Controls
{
    class ListViewCC : ListView
    {
        private bool _inColumnWidthChanging;

        public ListViewCC()
        {
            OwnerDraw = true;
            DoubleBuffered = true;
            ResizeColumns();
            DrawSubItem += ListViewCCDrawSubItem;
        }

        protected override void WndProc(ref Message m)
        {
            if (!DesignMode && m.Msg == 0x0030) //WM_SETFONT
            {
                Font f = new Font(Font.Name, 12);
                m.WParam = f.ToHfont();
            }
            base.WndProc(ref m);
        }

        private void ListViewCCDrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawBackground();
            using (StringFormat sf = new StringFormat())
            {
                sf.LineAlignment = StringAlignment.Center;
                Rectangle r = new Rectangle(e.Bounds.X + 6, e.Bounds.Y, e.Bounds.Width - 6, e.Bounds.Height);
                e.Graphics.DrawString(e.Header.Text, Font, Brushes.Black, r, sf);
            }
        }

        protected override void OnColumnWidthChanging(ColumnWidthChangingEventArgs e)
        {
            if (View != View.Details || _inColumnWidthChanging)
            {
                base.OnColumnWidthChanging(e);
                return;
            }

            if (e.ColumnIndex == Columns.Count - 1)
            {
                e.Cancel = true;
                e.NewWidth = Columns[e.ColumnIndex].Width;
                return;
            }

            int width = ClientRectangle.Width;
            if (Columns.Count > 1)
            {
                width -= Columns.Cast<ColumnHeader>().
                    Take(Columns.Count - 1).
                    Sum(c => c.Width);
            }

            _inColumnWidthChanging = true;
            Columns[Columns.Count - 1].Width = width;
            _inColumnWidthChanging = false;
        }

        public void ResizeColumns()
        {
            if (Columns.Count > 0 && View == View.Details)
            {
                OnColumnWidthChanging(new ColumnWidthChangingEventArgs(0, Columns[0].Width));
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ResizeColumns();
        }
    }
}
