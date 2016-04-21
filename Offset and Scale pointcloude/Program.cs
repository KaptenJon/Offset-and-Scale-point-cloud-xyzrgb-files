using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offset_and_Scale_pointcloude
{
    class Program
    {
        class Rows
        {
            public double X, Y, Z, R, G, B;

        }

        static void Main(string[] args)
        {
            if (args.Length < 1 && string.IsNullOrEmpty(args[0]))
            {
                return;
            }

            double scale = 1;
            if (args.Length > 1)
                scale = double.Parse(args[1]);
            var rows = new List<Rows>();
            using (var f = File.OpenText(args[0]))
            {
                for (int i = 0; !f.EndOfStream; i++)
                {
                    var row = f.ReadLine()?.Split(' ');
                    rows.Add(new Rows()
                    {
                        X = double.Parse(row[0]),
                        Y = double.Parse(row[1]),
                        Z = double.Parse(row[2]),
                        R = double.Parse(row[3]),
                        G = double.Parse(row[4]),
                        B = double.Parse(row[5]),
                    });
                }
            }
            var minx = rows.Min(t => t.X);
            var miny = rows.Min(t => t.Y);
            var minz = rows.Min(t => t.Z);
            
            File.Delete(args[0]);
            using (var f = File.CreateText(args[0]))
            {
                for (int index = 0; index < rows.Count; index++)
                {
                    //Offset
                    rows[index].X -= minx;
                    rows[index].Y -= miny;
                    rows[index].Z -= minz;
                    //Scale
                    rows[index].X *= scale;
                    rows[index].Y *= scale;
                    rows[index].Z *= scale;
                    f.WriteLine(rows[index].X + " " + rows[index].Y + " " + rows[index].Z + " " + rows[index].R + " " +
                                rows[index].G + " " + rows[index].B);
                }


            }
        }
    }
}
