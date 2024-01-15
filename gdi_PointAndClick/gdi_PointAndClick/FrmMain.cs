using System.Collections.Generic; // benötigt für Listen

namespace gdi_PointAndClick
{
    public partial class FrmMain : Form
    {
        List<Rectangle> rectangles = new List<Rectangle>();

        public FrmMain()
        {
            InitializeComponent();
            ResizeRedraw = true;
        }

        private void FrmMain_Paint(object sender, PaintEventArgs e)
        {
            // Hilfsvarablen
            Graphics g = e.Graphics;
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;

            for (int i = 0; i < rectangles.Count; i++)
            {
                Brush b = new SolidBrush(GenerateUniqueColor(i));
                g.FillRectangle(b, rectangles[i]);
            }
        }
        private Color GenerateUniqueColor(int index)
        {
            int red = (index * 53) % 256;
            int green = (index * 71) % 256;
            int blue = (index * 97) % 256;

            return Color.FromArgb(red, green, blue);
        }

        private void FrmMain_MouseClick(object sender, MouseEventArgs e)
        {
            Random rnd = new Random();
            int breite = rnd.Next(20, 50);
            int höhe = rnd.Next(20, 50);

            int x = e.Location.X - breite / 2;
            int y = e.Location.Y - höhe / 2;

            Rectangle newRectangle = new Rectangle(x, y, breite, höhe);

            bool klickonrectangle = false;

            for (int i = 0; i < rectangles.Count; i++)
            {
                if (rectangles[i].Contains(e.Location.X, e.Location.Y))
                {
                    klickonrectangle = true;
                    rectangles.Remove(rectangles[i]);
                    Refresh();

                }
            }
            if (!klickonrectangle)
            {
                rectangles.Add(newRectangle);
                Refresh();
            }
        }


        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                rectangles.Clear();
                Refresh();
            }
        }
    }
}