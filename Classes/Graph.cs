using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using SDA_0463_imd_MyProject.Classes;
using SDA_0463_imd_MyProject.Properties;

namespace SDA_0463_imd_MyProject
{
    public class Graph
    {
        private int verticesCount;
        private Vertex?[] vertices;
        private float[,] adjMatrix;                       // Матрица на съседството
        private int numVerts;                             // Текущ брой върхове (index за добавяне)

        private const float infinity =(float)Int32.MaxValue;    
        DistanceFromFirstVertex[]? shortPath;
        int currentVertex;
        float startToCurrent;
        List<Edge>? pathEdges;
        List<Edge> pathToFrom = new();
        float lengthFromTo = 0;
        public List<((int, int), (int, int))> AllEgdes = new();


        public Graph(List<Vertex> cities)
        {
            int verticesCount = cities.Count;
            this.verticesCount = verticesCount;
            vertices = new Vertex[verticesCount];
            adjMatrix = new float[verticesCount, verticesCount];
            numVerts = 0;

            shortPath = new DistanceFromFirstVertex[verticesCount];
            for (int j = 0; j <= verticesCount - 1; j++)
                for (int k = 0; k <= verticesCount - 1; k++)
                    adjMatrix[j, k] = infinity;
            pathEdges = new();

            foreach (Vertex vertex in cities) AddVertex(vertex);
            LoadEdges();
        }


        public int Count { get { return numVerts; } }

        public List<((int, int), (int, int))> Path
        {
            get
            {
                List<((int, int), (int, int))> path = new();
                for (int i = pathToFrom.Count - 1; i >= 0; i--)
                {
                    path.Add((
                    (vertices[pathToFrom![i].vertexIndex1]!.x, vertices[pathToFrom![i].vertexIndex1]!.y),
                    (vertices[pathToFrom![i].vertexIndex2]!.x, vertices[pathToFrom![i].vertexIndex2]!.y)
                    ));
                }

                return path;
            }
        }

        public float PathLength { get { return lengthFromTo; } }

        public void LoadEdges()
        {
            string[] rows = CSV.Distances.Split(Environment.NewLine);
            foreach (string row in rows)
            {
                string[] values = row.Split(';');
                AddEdge(GetVertexForLabel(values[0]),
                        GetVertexForLabel(values[1]),
                        float.Parse(values[2]));
            }
        }

        private int GetVertexForLabel(string label)
        {
            return Array.IndexOf(vertices, Array.Find(vertices, v => v!.label == label));
        }

        public bool AddVertex(Vertex vertex)
        {

            if (numVerts < verticesCount)
            {
                vertices[numVerts] = vertex;
                numVerts++;
                return true;
            }
            MessageBox.Show("ERR0R ***** \nПрепълване на размерa на графа");
            return false;
        }

        public void AddEdge(int startVert, int endVert, float weight = 1)
        {
            if (startVert < verticesCount && endVert < verticesCount)
            {
                adjMatrix[startVert, endVert] = weight;
                adjMatrix[endVert, startVert] = weight;
                AllEgdes!.Add((
                    (vertices[startVert]!.x, vertices[startVert]!.y),
                    (vertices[endVert]!.x, vertices[endVert]!.y)
                    ));
            }
            else
                MessageBox.Show("ERR0R ***** \nНе може да се съзаде ребро");
        }

        public float generatePath(int startVertex, int endVertex)
        {
            PathsToAllVertices(startVertex);
            FindPathFromTo(startVertex, endVertex);
            return lengthFromTo;
        }

        private void PathsToAllVertices(int startVertex)             //  Dijkstra Algorithm     
        {
            bool tempHasEdges = false;
            for (int i = 0; i < numVerts; i++)
            {
                if (adjMatrix[startVertex, i] != infinity)
                {
                    tempHasEdges = true;
                    break;
                }
            }
            if (!tempHasEdges)
            {
                MessageBox.Show("Dijkstra Min Path: \nКрай! Няма пътища от/до началния град.");
                shortPath![0] = new DistanceFromFirstVertex(startVertex, -1);
                return;
            }

            vertices[startVertex]!.visited = true;

            for (int j = 0; j < numVerts; j++)
            {
                float tempDist = adjMatrix[startVertex, j];
                shortPath![j] = new DistanceFromFirstVertex(startVertex, tempDist);
            }

            for (int k = 0; k < numVerts - 1; k++)
            {
                int indexMin = GetMin();
                currentVertex = indexMin;
                startToCurrent = shortPath![indexMin].distance;
                vertices[currentVertex]!.visited = true;
                CalculateShortPath();
            }
        }

        private int GetMin()
        {
            float minDist = infinity;
            int indexMin = 0;

            for (int j = 1; j < numVerts; j++)
            {
                if (!vertices[j]!.visited && shortPath![j].distance < minDist)
                {
                    minDist = shortPath[j].distance;
                    indexMin = j;
                }
            }
            return indexMin;
        }

        private void CalculateShortPath()
        {
            int idVerts = 0;
            while (idVerts < numVerts)
            {
                // Пропускат се посетените върхове
                if (vertices[idVerts]!.visited) idVerts++;
                else
                {
                    float currentToFringe = adjMatrix[currentVertex, idVerts];

                    // Пропускат се върхове, до които няма път
                    if (currentToFringe == infinity)
                    {
                        idVerts++;
                        continue;
                    }

                    float startToFringe = startToCurrent + currentToFringe;
                    float sPathDist = shortPath![idVerts].distance;
                    if (startToFringe < sPathDist)
                    {
                        shortPath[idVerts].parentVertex = currentVertex;
                        shortPath[idVerts].distance = startToFringe;
                    }
                    idVerts++;
                }
            }
        }

        public void FindPathFromTo(int startVertex, int endVertex)
        {
            if (shortPath![startVertex].distance == -1) return;

            for (int i = 0; i < numVerts; i++)
            {
                if (i == startVertex) continue;
                Edge e = new(shortPath![i].parentVertex, i);
                pathEdges!.Add(e);
            }
            pathEdges = pathEdges!.OrderBy(e => e.vertexIndex1).ToList();
            
            bool finished = false;
            while (!finished)
                finished = filterPath(startVertex, endVertex);
            lengthFromTo = shortPath![endVertex].distance;
        }


        private bool filterPath(int startVertex, int endVertex)
        {
            List<(int, Edge)> secondIndices = new();
            
            foreach (Edge edge in pathEdges!)
            {
                if (edge.vertexIndex1 == startVertex)
                {
                    if (edge.vertexIndex2 == endVertex)
                    {
                        pathToFrom.Add(edge);
                        return true;
                    }
                    secondIndices.Add((edge.vertexIndex2, edge));
                } 
            }
            if (secondIndices.Count == 0) return false;
            foreach ((int, Edge) secondIndex in secondIndices)
            {
                if (filterPath(secondIndex.Item1, endVertex))
                {
                    pathToFrom.Add(secondIndex.Item2);
                    return true;
                }
            }
            return false;
        }

        public void CleartPaths()
        {
            for (int i = 0; i < numVerts; i++) vertices[i]!.visited = false;
            currentVertex = -1;
            shortPath = new DistanceFromFirstVertex[verticesCount];
            pathEdges!.Clear();
            pathToFrom.Clear();
        }

    }

}

