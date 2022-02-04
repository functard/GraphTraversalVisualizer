using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests
{
    public class BFSTests : MonoBehaviour
    {
        [Test]
        public void BFSDiagonalTests()
        {
            CellGrid grid = new CellGrid(3, 3, null);
            BFS.FindPath(grid.GetNodeAtPosition(0, 0), grid.GetNodeAtPosition(0, 2), EMovementSettings.DIAGONAL, grid,VisualizationSetting.EVisualizationType.INSTANT);

            List<Cell> expected = new List<Cell>() { grid.GetNodeAtPosition(0, 0), grid.GetNodeAtPosition(0, 1), grid.GetNodeAtPosition(0, 2) };
            Assert.AreEqual(expected, BFS.PathCells);
            Assert.AreEqual(3, BFS.PathCells.Count);

            grid = new CellGrid(13, 9, null);
            BFS.FindPath(grid.GetNodeAtPosition(0, 0), grid.GetNodeAtPosition(12, 8), EMovementSettings.DIAGONAL, grid, VisualizationSetting.EVisualizationType.INSTANT);
            Assert.AreEqual(13, BFS.PathCells.Count);
        }
    }
}
