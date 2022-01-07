using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GridTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void GridResizeTests()
        {
            CellGrid grid = new CellGrid(3, 3, null);
            Assert.AreEqual(3 * 3,grid.Length());

            grid.UpdateGrid(10, 10);
            Assert.AreEqual(10 * 10,grid.Length());
            
            grid.UpdateGrid(10, 10);
            Assert.AreEqual(10 * 10,grid.Length());
        }

        [Test]
        public void GetCellPosAtTests()
        {
            CellGrid grid = new CellGrid(10, 10, null);

            Cell c = grid.GetNodeAtPosition(0, 0);
            Vector2 pos = new Vector2(c.X, c.Y);
            Assert.AreEqual(Vector2.zero, pos);

            c = grid.GetNodeAtPosition(0, 1);
            pos = new Vector2(c.X, c.Y);
            Assert.AreEqual(new Vector2(0,1), pos);

            c = grid.GetNodeAtPosition(09, 09);
            pos = new Vector2(c.X, c.Y);
            Assert.AreEqual(new Vector2(09, 09), pos);
        }
    }
}
