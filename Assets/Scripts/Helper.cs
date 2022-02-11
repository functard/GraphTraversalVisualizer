using System.Collections.Generic;

public static class Helper
{
    public static float TimeStep = 0.25f;

    public static List<Cell> RetracePath(Cell _start, Cell _end)
    {
        DiagnosticManager.Stop();

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
        DFS.Clear();
        BFS.Clear();
        Astar.Clear();
        Dijkstra.Clear();
        GreedyBestFirstSearch.Clear();
    }
}
