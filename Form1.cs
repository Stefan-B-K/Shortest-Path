using SDA_0463_imd_MyProject.Properties;
using System.Linq;
using System.Drawing;
using System.Xml;
using SDA_0463_imd_MyProject.Classes;

namespace SDA_0463_imd_MyProject
{
    public partial class Form1 : Form
    {
        Graph g;
        List<Vertex> cities;

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Определяне на най-късия път с кола между два града - посредством неориентиран претеглен граф и алгоритъма на Dijkstra";
            cities = Cities.loadVertices(panelMap.Width, panelMap.Height);
            g = new Graph(cities);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] labels = cities.Select(e => e.label).ToArray();
            cbStartVertex.Items.Clear();
            cbStartVertex.Items.AddRange(labels);
            cbStartVertex.Text = "  Изберете град за начало";
            cbEndVertex.Items.Clear();
            cbEndVertex.Items.AddRange(labels);
            cbEndVertex.Text = "  Изберете град за край";
            labelOutput.Text = "";

            panelMap.Paint += DrawGraph!;
            panelMap.Invalidate();
        }


        private void buttonDijkstra_Click(object sender, EventArgs e)
        {
            int selectedStart = cbStartVertex.SelectedIndex;
            int selectedEnd = cbEndVertex.SelectedIndex;
            if (selectedStart < 0 || selectedEnd < 0)
            {
                MessageBox.Show("Изберете начален и краен връх.");
                return;
            }
            if (selectedStart == selectedEnd)
            {
                MessageBox.Show("Началото и краят съвпадат!");
                return;
            }

            float pathLength = g.generatePath(selectedStart, selectedEnd);
            labelOutput.Text = g.Count == 0 
                    ? "" 
                    :  "Общо дължина на пътя: " + pathLength.ToString("0.#") + " km"; ;
  
            panelMap.Paint += DrawPath!;
            panelMap.Invalidate();
        }

        private void DrawGraph(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(SystemColors.Window);
            Pen pen = new Pen(Color.LightGray);
            foreach (((int, int), (int, int)) edge in g.AllEgdes)
                e.Graphics.DrawLine(pen, edge.Item1.Item1, edge.Item1.Item2, edge.Item2.Item1, edge.Item2.Item2);

            foreach (Vertex vertex in cities)
            {
                DrawTown(vertex, e);
                LabelTown(vertex, panelMap);
            }
        }


        private void DrawTown(Vertex vertex, PaintEventArgs e)
        {
            int radius1 = vertex.size == TownSize.Big ? 5 : 3;
            int radius2 = vertex.size == TownSize.Big ? 8 : 5;
            Color color = Color.Blue;

            Brush brush = new SolidBrush(color);
            Pen pen = new(color);
            int x1 = vertex.x - radius1;
            int y1 = vertex.y - radius1;
            int width1 = 2 * radius1;
            int height1 = 2 * radius1;
            int x2 = vertex.x - radius2;
            int y2 = vertex.y - radius2;
            int width2 = 2 * radius2;
            int height2 = 2 * radius2;
            e.Graphics.FillEllipse(brush, x1, y1, width1, height1);
            e.Graphics.DrawEllipse(pen, x2, y2, width2, height2);
        }

        private void LabelTown(Vertex vertex, Panel panelMap)
        {
            Label label = new Label();
            label.Text = vertex.label;
            int offsetY = vertex.size == TownSize.Small ? 7 : 9;
            List<string> labelsAbove = new() { "Горна Оряховица", "Димитровград", "Пловдив", "Пазарджик", "Севлиево", "Велинград", "Попово" };
            if (labelsAbove.Contains(vertex.label)) offsetY -= 4 * offsetY;
            label.Location = new Point(vertex.x - 15, vertex.y + offsetY);
            label.AutoSize = true;
            panelMap.Controls.Add(label);
        }

        private void DrawPath(object sender, PaintEventArgs e)
        {
            Pen pen = new(Color.Red, 2);
            foreach (((int, int), (int, int)) edge in g.Path)
                e.Graphics.DrawLine(pen, edge.Item1.Item1, edge.Item1.Item2, edge.Item2.Item1, edge.Item2.Item2);
            g.CleartPaths();
        }
    }
}