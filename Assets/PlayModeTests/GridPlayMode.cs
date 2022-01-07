using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GridPlayMode
    {
    
        [UnityTest]
        public IEnumerator GridPlayModeWithEnumeratorPasses()
        {
            GameObject go = new GameObject();
            CellGrid grid = new CellGrid(10, 10, null);
            grid.UpdateGrid(5, 5);
            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(25, grid.Length());
        }
    }
}
