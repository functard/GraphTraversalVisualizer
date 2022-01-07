using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static float TimeStep = 0.25f;
    public static int GetDistance(Cell _a, Cell _b)
    {
        int dstX = Mathf.Abs(_a.X - _b.X);
        int dstY = Mathf.Abs(_a.Y - _b.Y);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);

        //int dx = Mathf.Abs(_a.X - _b.X);
        //int dy = Mathf.Abs(_a.Y - _b.Y);

        //int diagonal;
        //int straigth;
        //if (dy > dx)
        //{
        //    diagonal = dx * 14;
        //    straigth = (dy - dx) * 10;
        //}
        //else
        //{
        //    diagonal = dy * 14;
        //    straigth = (dx - dy) * 10;
        //}
        //return straigth + diagonal;
    }

    public static List<Cell> RetracePath(Cell _start, Cell _end)
    {
        List<Cell> path = new List<Cell>();
        Cell curr = _end;

        while (curr != _start)
        {
            path.Add(curr);
            curr = curr.Parent;
        }
        path.Add(_start);
        path.Reverse();
        return path;
    }

    public static void ClearAlgorithms()
    {
        BFS.Clear();
        Astar.Clear();
        Dijkstra.Clear();
    }
}
