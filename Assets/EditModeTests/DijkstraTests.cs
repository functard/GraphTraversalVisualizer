using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests
{
    public class DijkstraTests 
    {
        [Test]
        public void DijkstraTest()
        {
            CellGrid grid = new CellGrid(3, 3, null);
            Dijkstra.FindPath(grid.GetNodeAtPosition(0, 0), grid.GetNodeAtPosition(0, 2), EMovementSettings.DIAGONAL, grid,
                           VisualizationSetting.EVisualizationType.INSTANT, VisualizationSetting.EHeuristics.EUCLIDIAN);

            List<Cell> expected = new List<Cell>() { grid.GetNodeAtPosition(0, 0), grid.GetNodeAtPosition(0, 1), grid.GetNodeAtPosition(0, 2) };
            Assert.AreEqual(expected, Dijkstra.PathCells);
            Assert.AreEqual(3, Dijkstra.PathCells.Count);

            grid = new CellGrid(13, 9, null);
            Dijkstra.FindPath(grid.GetNodeAtPosition(0, 0), grid.GetNodeAtPosition(12, 8), EMovementSettings.DIAGONAL, grid,
                            VisualizationSetting.EVisualizationType.INSTANT, VisualizationSetting.EHeuristics.EUCLIDIAN);

            Assert.AreEqual(13, Dijkstra.PathCells.Count);
        }
    }
}
