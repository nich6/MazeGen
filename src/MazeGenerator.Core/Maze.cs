using System.Collections.Generic;
using System.Drawing;

namespace MazeGenerator.Core
{
    public class Maze
    {
        public List<Cell> FoundPath = new List<Cell>();
        public Pen LocationPen = new Pen(Brushes.Green, 7);
        public Pen MazePen = new Pen(Brushes.Black, 3);

        public Maze(int width, int height, int cellSize)
        {
            X = cellSize;
            Width = width / cellSize;
            Height = height / cellSize;
            MazeArray = new Cell[Width, Height];
            Initialize();
            Begin = MazeArray[0, 0];
            End = MazeArray[Width - 1, Height - 1];
        }

        public int Width { get; set; }

        public int Height { get; }

        public Cell[,] MazeArray { get; }

        public Cell Begin { get; set; } 
        public Cell End { get; set; } 
        public int X { get; }

        public int Y => X;
        public bool Working { get; set; }
        public bool Solving { get; set; }
        public Point CurrentGenerateCell { get; set; }
        public Point CurrentSolvePosition { get; set; }

        private void Initialize()
        {
            for (var i = 0; i < Height; i++)
            for (var j = 0; j < Width; j++)
            {
                MazeArray[j, i] = new Cell(new Point(j * X, i * Y), new Point(j, i));
            }
        }

        private static void drawVerticalWall(Graphics g, ref Point location, int height, Pen pen)
        {
            g.DrawLine(pen,
                location,
                new Point(location.X, location.Y + height));
        }

        public void Draw(Graphics g)
        {
            g.Clear(Color.DarkGray);

            if (Width == 0)
            {
                return;
            }

            g.FillRectangle(Brushes.Green, new Rectangle(Begin.Location, new Size(X, Y)));

            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    if (MazeArray[i, j].IsVisited)
                    {
                        g.FillRectangle(Brushes.LightGray, new Rectangle(MazeArray[i, j].Location, new Size(X, Y)));
                    }

                    
                    if (Working && CurrentGenerateCell.X == j && CurrentGenerateCell.Y == i)
                    {
                        var target = new Point(j * X, i * Y);
                        drawVerticalWall(g, ref target, Y, LocationPen);
                    }

                    if (Solving && CurrentSolvePosition.X == j && CurrentSolvePosition.Y == i)
                    {
                        g.FillRectangle(Brushes.Green, new Rectangle(MazeArray[i, j].Location, new Size(X, Y)));
                    }

                    MazeArray[i, j].Draw(g, MazePen, new Size(X, Y));

                    if (MazeArray[i, j].Path != Cell.Paths.None)
                    {
                        switch (MazeArray[i, j].Path)
                        {
                            case Cell.Paths.Up:
                                g.DrawLine(LocationPen,
                                    new Point(MazeArray[i, j].Location.X + X / 2, MazeArray[i, j].Location.Y + Y / 2),
                                    new Point(MazeArray[i - 1, j].Location.X + X / 2, MazeArray[i - 1, j].Location.Y + Y / 2));
                                break;
                            case Cell.Paths.Down:
                                g.DrawLine(LocationPen,
                                    new Point(MazeArray[i, j].Location.X + X / 2, MazeArray[i, j].Location.Y + Y / 2),
                                    new Point(MazeArray[i + 1, j].Location.X + X / 2, MazeArray[i + 1, j].Location.Y + Y / 2));
                                break;
                            case Cell.Paths.Right:
                                g.DrawLine(LocationPen,
                                    new Point(MazeArray[i, j].Location.X + X / 2, MazeArray[i, j].Location.Y + Y / 2),
                                    new Point(MazeArray[i, j + 1].Location.X + X / 2, MazeArray[i, j + 1].Location.Y + Y / 2));
                                break;
                            default:
                                g.DrawLine(LocationPen,
                                    new Point(MazeArray[i, j].Location.X + X / 2, MazeArray[i, j].Location.Y + Y / 2),
                                    new Point(MazeArray[i, j - 1].Location.X + X / 2, MazeArray[i, j - 1].Location.Y + Y / 2));
                                break;
                        }
                    }
                }
            }
        }

        public void DrawPath(Graphics g)
        {

            g.FillRectangle(Brushes.Green, new Rectangle(Begin.Location, new Size(X, Y)));

            for (var i = 1; i < FoundPath.Count; i++)
       
            {
                g.DrawLine(LocationPen,
                    new Point(FoundPath[i].Location.X + X / 2, FoundPath[i].Location.Y + Y / 2),
                    new Point(FoundPath[i - 1].Location.X + X / 2, FoundPath[i - 1].Location.Y + Y / 2));
            }
        }
    }
}