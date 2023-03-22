using System;

namespace SDA_0463_imd_MyProject.Classes
{
    public class Edge
    {
        public int vertexIndex1;
        public int vertexIndex2;
        public Edge(int vertexIndex1, int vertexIndex2)
        {
            this.vertexIndex1 = vertexIndex1;
            this.vertexIndex2 = vertexIndex2;
        }
    }
}
