using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace MapEditor
{
    public class Map
    {
        public int Height;
        public int Width;

        private Cell[][] Cells;

        public Cell this[int y, int x]
        {
            get { return Cells[y][x]; }
        }

        public Map(Stream stream, MainWindow sender)
        {
            try
            {
                byte[] buff = new byte[stream.Length];
                stream.Read(buff, 0, (int)stream.Length);

                string[] text = Encoding.UTF8.GetString(buff).Split(';');

                Height = int.Parse(text[0]);
                Width = int.Parse(text[1]);

                Cells = new Cell[Height][];
                for (int i = 0; i < Height; i++)
                {
                    sender.Dispatcher.Invoke(sender.updProgress, 100.0 / Height * i );

                    Cells[i] = new Cell[Width];
                    for (int j = 0; j < Width; j++)
                    {
                        Cells[i][j] = new Cell(text[i * Width + j + 2]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot create map. Error: " + ex.Message);
            }
        }

        public Map(int height, int width, Landscape ls)
        {
            Height = height;
            Width = width;

            Cells = new Cell[height][];

            for (int i = 0; i < height; i++)
            {
                Cells[i] = new Cell[width];
                for (int j = 0; j < width; j++)
                {
                    Cells[i][j] = new Cell();
                    Cells[i][j].EnglishMessage = "";
                    Cells[i][j].RussianMessage = "";
                    Cells[i][j].IsPassable = true;
                    Cells[i][j].IsTaskable = false;
                    Cells[i][j].IsTeleport = false;
                    Cells[i][j].LandscapeId = ls;
                }
            }
        }

        public void Save(string filename)
        {
            var file = File.Create(filename);

            byte[] buff = Encoding.UTF8.GetBytes(Height.ToString() + ";" + Width.ToString() + ";");

            file.Write(buff, 0, buff.Length);

            foreach (var i in Cells)
            {
                foreach (var j in i)
                {
                    buff = Encoding.UTF8.GetBytes(j.ToString() + ";");
                    file.Write(buff, 0, buff.Length);
                }
            }
        }
    }
}