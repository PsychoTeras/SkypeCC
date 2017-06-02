using System.Drawing;
using System.Windows.Forms;

namespace SkypeCC.Controls
{
    class ListBoxCC : ListBox
    {
        public ListBoxCC()
        {
            DoubleBuffered = true;
            DrawMode = DrawMode.OwnerDrawVariable;
        }

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            e.ItemHeight = 20;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index == -1 || e.Index == Items.Count) return;

            e.DrawBackground();
            Brush b = (e.State | DrawItemState.Selected) == e.State ? Brushes.White : Brushes.Black;
            using (StringFormat sf = new StringFormat())
            {
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(GetItemText(Items[e.Index]), e.Font, b, e.Bounds, sf);
            }
            e.DrawFocusRectangle();
        }
    }
}
