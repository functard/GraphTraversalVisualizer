using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Tests
{
    public class PriorityQueueTests
    {
        [Test]
        public void QueueNodeOrderTest()
        {
            PriorityQueue<Cell> queue = new PriorityQueue<Cell>();

            Cell a = new Cell(0, 0, null)
            {
                Priority = 25,
                DistTraveled = 25
            };
            queue.Enqueue(a);

            Cell b = new Cell(0, 0, null)
            {
                Priority = 12,
                DistTraveled = 12

            };
            queue.Enqueue(b);


            Cell c = new Cell(0, 0, null)
            {
                Priority = 0,
                DistTraveled = 0
            };
            queue.Enqueue(c);


            Cell d = new Cell(0, 0, null)
            {
                Priority = 1,
                DistTraveled = 1
            };
            queue.Enqueue(d);

            Cell e = new Cell(0, 0, null)
            {
                Priority = 2,
                DistTraveled = 2
            };
            queue.Enqueue(e);

            Cell f = new Cell(0, 0, null)
            {
                Priority = -5,
                DistTraveled = -5
            };
            queue.Enqueue(f);

            Assert.AreEqual(f, queue.Dequeue());
            Assert.AreEqual(c, queue.Dequeue());
            Assert.AreEqual(d, queue.Dequeue());
            Assert.AreEqual(e, queue.Dequeue());
            Assert.AreEqual(b, queue.Dequeue());
            Assert.AreEqual(a, queue.Dequeue());
            Assert.AreEqual(queue.Count(), 0);
        }

        [Test]
        public void QueueIntOrderTest()
        {
            List<int> inputs = new List<int>() { 37, 6, 18, 19, 3, 5, 0, 1, 6, 42 };
            PriorityQueue<int> queue = new PriorityQueue<int>();

            foreach (var item in inputs)
            {
                queue.Enqueue(item);
            }
                                             //{ 37, 6, 18, 19, 3, 5, 0, 1, 6, 42 };
            var expectedList = new List<int>() { 0, 1, 3, 5, 6, 6, 18, 19, 37, 42 };

            int i = 0;
            while (queue.Count() > 0)
            {
                Assert.AreEqual(queue.Dequeue(), expectedList[i]);
                i++;
            }
            Assert.AreEqual(0, queue.Count());
        }
    }
}
