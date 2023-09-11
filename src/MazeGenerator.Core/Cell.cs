using System.Drawing;

namespace MazeGenerator.Core

{
    public class Cell
    {
        public enum Paths
        {
            Up,
            Down,
            Right,
            Left,
            None
        }

     

        public Paths Path;

        public Cell(Point location, Point position)
        {
            Position = position;
            Location = location; 
            IsVisited = false;
            PreviousCell = null;
            Path = Paths.None;
        }

        public bool[] CellWalls { get; set; } = {true, true, true, true};

       
        public Point Location { get; set; }

        
        public Point Position { get; set; }
        public bool IsVisited { get; set; }
        public Cell PreviousCell { get; set; }

        public void Draw(Graphics g, Pen pen, Size size)
        {
            
            if (CellWalls[0])
            {
                g.DrawLine(pen, Location, new Point(Location.X, Location.Y + size.Height));
            }

            if (CellWalls[2])
            {
                g.DrawLine(pen, new Point(Location.X + size.Width, Location.Y),
                    new Point(Location.X + size.Width, Location.Y + size.Height));
            }

            if (CellWalls[1])
            {
                g.DrawLine(pen, Location, new Point(Location.X + size.Width, Location.Y));
            }

            if (CellWalls[3])
            {
                g.DrawLine(pen, new Point(Location.X, Location.Y + size.Height),
                    new Point(Location.X + size.Width, Location.Y + size.Height));
            }
        }
    }
}