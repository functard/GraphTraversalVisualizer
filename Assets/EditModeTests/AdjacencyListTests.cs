using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Tests
{
    public class AdjacencyListTests
    {
        [Test]
        public void DiagonalEdgesCountTests()
        {
            // bottom left
            CellGrid grid = new CellGrid(20, 20, null);
            List<Cell> neighbors = grid.GetNodeAtPosition(new Vector2Int(0, 0)).GetNeighbours(EMovementSettings.Diagonal, grid);
            Assert.AreEqual(3, neighbors.Count);

            // top right
            neighbors = grid.GetNodeAtPosition(new Vector2Int(19, 19)).GetNeighbours(EMovementSettings.Diagonal, grid);
            Assert.AreEqual(3, neighbors.Count);

            // top left
            neighbors = grid.GetNodeAtPosition(new Vector2Int(0, 19)).GetNeighbours(EMovementSettings.Diagonal, grid);
            Assert.AreEqual(3, neighbors.Count);

            // bottom right
            neighbors = grid.GetNodeAtPosition(new Vector2Int(19, 0)).GetNeighbours(EMovementSettings.Diagonal, grid);
            Assert.AreEqual(3, neighbors.Count);
        }

        [Test]
        public void NoDiagonalEdgesCountTests()
        {
            // bottom left
            CellGrid grid = new CellGrid(20, 20, null);
            List<Cell> neighbors = grid.GetNodeAtPosition(new Vector2Int(0, 0)).GetNeighbours(EMovementSettings.NoDiagonal, grid);
            Assert.AreEqual(2, neighbors.Count);

            // top right
            neighbors = grid.GetNodeAtPosition(new Vector2Int(19, 19)).GetNeighbours(EMovementSettings.NoDiagonal, grid);
            Assert.AreEqual(2, neighbors.Count);

            // top left
            neighbors = grid.GetNodeAtPosition(new Vector2Int(0, 19)).GetNeighbours(EMovementSettings.NoDiagonal, grid);
            Assert.AreEqual(2, neighbors.Count);

            // bottom right
            neighbors = grid.GetNodeAtPosition(new Vector2Int(19, 0)).GetNeighbours(EMovementSettings.NoDiagonal, grid);
            Assert.AreEqual(2, neighbors.Count);
        }

        [Test]
        public void DontCrossCornersEdgesCountTests()
        {
            // bottom left
            CellGrid grid = new CellGrid(20, 20, null);
            List<Cell> neighbors = grid.GetNodeAtPosition(new Vector2Int(0, 0)).GetNeighbours(EMovementSettings.DontCrossCorners, grid);
            Assert.AreEqual(3, neighbors.Count);

            // top right
            neighbors = grid.GetNodeAtPosition(new Vector2Int(19, 19)).GetNeighbours(EMovementSettings.DontCrossCorners, grid);
            Assert.AreEqual(3, neighbors.Count);

            // top left
            neighbors = grid.GetNodeAtPosition(new Vector2Int(0, 19)).GetNeighbours(EMovementSettings.DontCrossCorners, grid);
            Assert.AreEqual(3, neighbors.Count);

            // bottom right
            neighbors = grid.GetNodeAtPosition(new Vector2Int(19, 0)).GetNeighbours(EMovementSettings.DontCrossCorners, grid);
            Assert.AreEqual(3, neighbors.Count);
        }

        [Test]
        public void DiagonalObstacleTests()
        {
            CellGrid grid = new CellGrid(20, 20, null);
            grid.GetNodeAtPosition(new Vector2Int(1, 1)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(0, 1)).Walkable = false;

            List<Cell> neighbors = grid.GetNodeAtPosition(new Vector2Int(0, 0)).GetNeighbours(EMovementSettings.Diagonal, grid);
            List<Cell> expected = new List<Cell>();
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(1, 0)));
            Assert.AreEqual(expected, neighbors);

            foreach (var item in grid.ToList())
            {
                item.Walkable = true;
            }

            // All neighbors blocked
            grid.GetNodeAtPosition(new Vector2Int(4, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(5, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(4, 5)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 5)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(4, 4)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(5, 4)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 4)).Walkable = false;

            neighbors = grid.GetNodeAtPosition(new Vector2Int(5, 5)).GetNeighbours(EMovementSettings.Diagonal, grid);
            Assert.AreEqual(0, neighbors.Count);

            foreach (var item in grid.ToList())
            {
                item.Walkable = true;
            }
            expected.Clear();


            // Diagonals blocked
            grid.GetNodeAtPosition(new Vector2Int(4, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 4)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(4, 4)).Walkable = false;


            neighbors = grid.GetNodeAtPosition(new Vector2Int(5, 5)).GetNeighbours(EMovementSettings.Diagonal, grid);
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(5, 6)));
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(5, 4)));
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(6, 5)));
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(4, 5)));

            Assert.AreEqual(4, neighbors.Count);
            CollectionAssert.AreEquivalent(expected, neighbors);

            foreach (var item in grid.ToList())
            {
                item.Walkable = true;
            }
            expected.Clear();

            // Vertical/HorizontalBlocked
            grid.GetNodeAtPosition(new Vector2Int(5, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(5, 4)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 5)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(4, 5)).Walkable = false;


            neighbors = grid.GetNodeAtPosition(new Vector2Int(5, 5)).GetNeighbours(EMovementSettings.Diagonal, grid);
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(4, 6)));
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(6, 6)));
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(6, 4)));
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(4, 4)));
            Assert.AreEqual(4, neighbors.Count);
            CollectionAssert.AreEquivalent(expected, neighbors);

            foreach (var item in grid.ToList())
            {
                item.Walkable = true;
            }
            expected.Clear();

            // One diagonal allowed
            grid.GetNodeAtPosition(new Vector2Int(4, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(5, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(4, 5)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 5)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(4, 4)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(5, 4)).Walkable = false;

            expected.Add(grid.GetNodeAtPosition(new Vector2Int(6, 4)));

            neighbors = grid.GetNodeAtPosition(new Vector2Int(5, 5)).GetNeighbours(EMovementSettings.Diagonal, grid);
            Assert.AreEqual(expected, neighbors);
        }

        [Test]
        public void NoDiagonalObstacleTests()
        {
            CellGrid grid = new CellGrid(20, 20, null);

            grid.GetNodeAtPosition(new Vector2Int(1, 1)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(0, 1)).Walkable = false;

            List<Cell> neighbors = grid.GetNodeAtPosition(new Vector2Int(0, 0)).GetNeighbours(EMovementSettings.NoDiagonal, grid);
            List<Cell> expected = new List<Cell>();
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(1, 0)));
            Assert.AreEqual(expected, neighbors);

            foreach (var item in grid.ToList())
            {
                item.Walkable = true;
            }

            // All neighbors blocked
            grid.GetNodeAtPosition(new Vector2Int(4, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(5, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(4, 5)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 5)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(4, 4)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(5, 4)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 4)).Walkable = false;

            neighbors = grid.GetNodeAtPosition(new Vector2Int(5, 5)).GetNeighbours(EMovementSettings.NoDiagonal, grid);
            Assert.AreEqual(0, neighbors.Count);

            foreach (var item in grid.ToList())
            {
                item.Walkable = true;
            }
            expected.Clear();

            // Diagonals blocked
            grid.GetNodeAtPosition(new Vector2Int(4, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 4)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(4, 4)).Walkable = false;


            neighbors = grid.GetNodeAtPosition(new Vector2Int(5, 5)).GetNeighbours(EMovementSettings.NoDiagonal, grid);
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(5, 6)));
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(5, 4)));
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(6, 5)));
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(4, 5)));

            CollectionAssert.AreEquivalent(expected, neighbors);

            foreach (var item in grid.ToList())
            {
                item.Walkable = true;
            }
            expected.Clear();

            // Vertical/Horizontal Blocked
            grid.GetNodeAtPosition(new Vector2Int(5, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(5, 4)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 5)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(4, 5)).Walkable = false;


            neighbors = grid.GetNodeAtPosition(new Vector2Int(5, 5)).GetNeighbours(EMovementSettings.NoDiagonal, grid);
            Assert.AreEqual(0, neighbors.Count);
        }

        [Test]
        public void DontCrossCornersObstacleTests()
        {
            CellGrid grid = new CellGrid(20, 20, null);

            grid.GetNodeAtPosition(new Vector2Int(1, 1)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(0, 1)).Walkable = false;

            List<Cell> neighbors = grid.GetNodeAtPosition(new Vector2Int(0, 0)).GetNeighbours(EMovementSettings.DontCrossCorners, grid);
            List<Cell> expected = new List<Cell>();
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(1, 0)));
            Assert.AreEqual(expected, neighbors);

            foreach (var item in grid.ToList())
            {
                item.Walkable = true;
            }

            // All neighbors blocked
            grid.GetNodeAtPosition(new Vector2Int(4, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(5, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(4, 5)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 5)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(4, 4)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(5, 4)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 4)).Walkable = false;

            neighbors = grid.GetNodeAtPosition(new Vector2Int(5, 5)).GetNeighbours(EMovementSettings.DontCrossCorners, grid);
            Assert.AreEqual(0, neighbors.Count);

            foreach (var item in grid.ToList())
            {
                item.Walkable = true;
            }
            expected.Clear();

            // Diagonals blocked
            grid.GetNodeAtPosition(new Vector2Int(4, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 4)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(4, 4)).Walkable = false;


            neighbors = grid.GetNodeAtPosition(new Vector2Int(5, 5)).GetNeighbours(EMovementSettings.DontCrossCorners, grid);
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(5, 6)));
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(5, 4)));
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(6, 5)));
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(4, 5)));

            CollectionAssert.AreEquivalent(expected, neighbors);

            foreach (var item in grid.ToList())
            {
                item.Walkable = true;
            }
            expected.Clear();

            // Vertical/Horizontal Blocked
            grid.GetNodeAtPosition(new Vector2Int(5, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(5, 4)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 5)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(4, 5)).Walkable = false;

            neighbors = grid.GetNodeAtPosition(new Vector2Int(5, 5)).GetNeighbours(EMovementSettings.Diagonal, grid);
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(4, 6)));
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(6, 6)));
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(6, 4)));
            expected.Add(grid.GetNodeAtPosition(new Vector2Int(4, 4)));
            Assert.AreEqual(4, neighbors.Count);
            CollectionAssert.AreEquivalent(expected, neighbors);

            foreach (var item in grid.ToList())
            {
                item.Walkable = true;
            }
            expected.Clear();

            // One diagonal allowed
            grid.GetNodeAtPosition(new Vector2Int(4, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(5, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 6)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(4, 5)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(6, 5)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(4, 4)).Walkable = false;
            grid.GetNodeAtPosition(new Vector2Int(5, 4)).Walkable = false;

            expected.Add(grid.GetNodeAtPosition(new Vector2Int(6, 4)));

            neighbors = grid.GetNodeAtPosition(new Vector2Int(5, 5)).GetNeighbours(EMovementSettings.DontCrossCorners, grid);
            Assert.AreEqual(0, neighbors.Count);
        }
    }
}
