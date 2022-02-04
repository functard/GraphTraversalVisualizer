using System.Collections.Generic;
using NUnit.Framework;

namespace Tests
{
    public class AstarTests
    {
        [Test]
        public void AstarTest()
        {
            CellGrid grid = new CellGrid(3, 3, null);
            Astar.FindPath(grid.GetNodeAtPosition(0, 0), grid.GetNodeAtPosition(0, 2), EMovementSettings.DIAGONAL, grid,
                           VisualizationSetting.EVisualizationType.INSTANT, VisualizationSetting.EHeuristics.EUCLIDIAN);

            List<Cell> expected = new List<Cell>() { grid.GetNodeAtPosition(0, 0), grid.GetNodeAtPosition(0, 1), grid.GetNodeAtPosition(0, 2) };
            Assert.AreEqual(expected, Astar.PathCells);
            Assert.AreEqual(3, Astar.PathCells.Count);

            grid = new CellGrid(13, 9, null);
            Astar.FindPath(grid.GetNodeAtPosition(0, 0), grid.GetNodeAtPosition(12, 8), EMovementSettings.DIAGONAL, grid,
                            VisualizationSetting.EVisualizationType.INSTANT, VisualizationSetting.EHeuristics.EUCLIDIAN);

            Assert.AreEqual(13, Astar.PathCells.Count);
        }
    }
}