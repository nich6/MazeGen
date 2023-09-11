using System.Drawing;

namespace MazeGenerator.Core
{
 
    public class GenerateMaze
    {
        private readonly Maze _maze;

        public GenerateMaze(Maze maze)
        {
            _maze = maze;
        }

        public void Generate(int value)
        {
            
            _maze.MazePen.Dispose();
            _maze.MazePen = _maze.X < 5 ? new Pen(Brushes.Black, 1) : new Pen(Brushes.Black, 3);
            var algo = new SearchAlgorithm(_maze);
            algo.DepthFirstSearch();
        }
    }
}