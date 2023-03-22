using System;
using SDA_0463_imd_MyProject;

namespace SDA_0463_imd_MyProject.Classes
{
    public class Vertex
    {
        public bool visited;
        public string label;
        public int x;
        public int y;
        public TownSize size;

        public Vertex(string label, int x, int y, TownSize size)
        {
            this.label = label;
            visited = false;
            this.x = x;
            this.y = y;
            this.size = size;
        }

        public Vertex(string label)
        {
            this.label = label;
            visited = false;
            x = 0;
            y = 0;
            size = TownSize.Small;
        }

        public static Vertex FromCSV(
            string csvLine,
            int panelHeight,
            Double div,
            int minusX,
            int minusY)
        {
            string[] values = csvLine.Split(';');
            TownSize townsize;
            try
            {
                Enum.TryParse(values[3], out townsize);
            } catch
            {
                townsize = TownSize.Small;
            }
            int x = (int)Math.Round(Double.Parse(values[2])/div) - minusX;
            int y = panelHeight - (int)Math.Round(Double.Parse(values[1]) / div) + minusY;
            return new Vertex(values[0], x, y, townsize);
        }

    }

    public enum TownSize
    {
        Small, Big
    }

}

