using System;

namespace SDA_0463_imd_MyProject.Classes
{
    public class DistanceFromFirstVertex
    {
        public float distance;
        public int parentVertex;
        public DistanceFromFirstVertex(int parentVertex, float distance)
        {
            this.distance = distance;
            this.parentVertex = parentVertex;
        }
    }

}