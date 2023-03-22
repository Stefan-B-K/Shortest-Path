using SDA_0463_imd_MyProject.Properties;
using System;
using System.CodeDom;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace SDA_0463_imd_MyProject.Classes
{
    public struct Cities
    {
        static public List<Vertex> loadVertices(int panelWidth, int panelHeight)
        {
            string[] rows = CSV.Cities.Split(Environment.NewLine);

            List<Double> Xs = rows.Select(r => Double.Parse(r.Split(';')[2])).ToList();
            List<Double> Ys = rows.Select(r => Double.Parse(r.Split(';')[1])).ToList();
            Double div = Math.Max((Xs.Max() - Xs.Min()) / (panelWidth - 150), (Ys.Max() - Ys.Min()) / (panelHeight - 70));
            int minusX = (int)Math.Round(Xs.Min() / div) - (int)Math.Round( (panelWidth - (Xs.Max() - Xs.Min())/div )/2 );
            int minusY = (int)Math.Round(Ys.Min() / div) - (int)Math.Round((panelHeight - (Ys.Max() - Ys.Min()) / div) / 2) ;
            List<Vertex> values = rows
                                       .Select(v => Vertex.FromCSV(v, panelHeight, div, minusX, minusY))
                                       .ToList();
            return values;
        }

       
    }

}