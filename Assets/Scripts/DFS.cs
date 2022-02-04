using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DFS
{
    public static Stack<Cell> FrontierCells { get; private set; }
    public static List<Cell> VisitedCells { get; private set; }
    public static List<Cell> PathCells { get; private set; }

    public static void FindPath(Cell _start, Cell _end, EMovementSettings _movementSettings, CellGrid _grid,
                                                              VisualizationSetting.EVisualizationType _type)
    {
        switch (_type)
        {
            case VisualizationSetting.EVisualizationType.DELAYED:
                CoroutineController.Start(FindPathWithDelay(_start, _end, _movementSettings, _grid));
                break;
            case VisualizationSetting.EVisualizationType.INSTANT:
                FindPathInstant(_start, _end, _movementSettings, _grid);
                break;
            case VisualizationSetting.EVisualizationType.INPUT:
                CoroutineController.Start(FindPathWithInput(_start, _end, _movementSettings, _grid));
                break;
            default:
                break;
        }
    }

    private static void FindPathInstant(Cell _start, Cell _end, EMovementSettings _movementSettings, CellGrid _grid)
    {
        DiagnosticManager.Start();

        FrontierCells = new Stack<Cell>();
        VisitedCells = new List<Cell>();
        PathCells = new List<Cell>();

        FrontierCells.Push(_start);
        VisitedCells.Add(_start);
        while (FrontierCells.Count > 0)
        {
            DiagnosticManager.Record();

            Cell curr = FrontierCells.Pop();
            if (!VisitedCells.Contains(curr))
                VisitedCells.Add(curr);


            // for every neighbour
            foreach (Cell neighbour in curr.GetNeighbours(_movementSettings, _grid))
            {
                // if not in visited list
                if (!VisitedCells.Contains(neighbour)/* && !FrontierCells.Contains(neighbour)*/)
                {
                    // set parent node
                    neighbour.Parent = curr;

                    // add to frontier list
                    FrontierCells.Push(neighbour);
                }
            }
            // path found
            if (FrontierCells.Contains(_end))
            {
                PathCells = Helper.RetracePath(_start, _end);
                break;
            }
        }
        DiagnosticManager.Stop();

    }
    private static IEnumerator FindPathWithDelay(Cell _start, Cell _end, EMovementSettings _movementSettings, CellGrid _grid)
    {
        DiagnosticManager.Start();

        FrontierCells = new Stack<Cell>();
        VisitedCells = new List<Cell>();
        PathCells = new List<Cell>();

        FrontierCells.Push(_start);
        VisitedCells.Add(_start);
        while (FrontierCells.Count > 0)
        {
            DiagnosticManager.Record();

            Cell curr = FrontierCells.Pop();
            if (!VisitedCells.Contains(curr))
                VisitedCells.Add(curr);


            // for every neighbour
            foreach (Cell neighbour in curr.GetNeighbours(_movementSettings, _grid))
            {
                // if not in visited list
                if (!VisitedCells.Contains(neighbour)/* && !FrontierCells.Contains(neighbour)*/)
                {
                    // set parent node
                    neighbour.Parent = curr;

                    // add to frontier list
                    FrontierCells.Push(neighbour);
                }
            }
            // path found
            if (FrontierCells.Contains(_end))
            {
                PathCells = Helper.RetracePath(_start, _end);
                yield break;
            }
            yield return new WaitForSeconds(Helper.TimeStep);
        }
        DiagnosticManager.Stop();
    }

    private static IEnumerator FindPathWithInput(Cell _start, Cell _end, EMovementSettings _movementSettings, CellGrid _grid)
    {
        DiagnosticManager.Start();

        FrontierCells = new Stack<Cell>();
        VisitedCells = new List<Cell>();
        PathCells = new List<Cell>();

        FrontierCells.Push(_start);
        VisitedCells.Add(_start);
        while (FrontierCells.Count > 0)
        {
            DiagnosticManager.Record();

            Cell curr = FrontierCells.Pop();
            if (!VisitedCells.Contains(curr))
                VisitedCells.Add(curr);


            // for every neighbour
            foreach (Cell neighbour in curr.GetNeighbours(_movementSettings, _grid))
            {
                // if not in visited list
                if (!VisitedCells.Contains(neighbour)/* && !FrontierCells.Contains(neighbour)*/)
                {
                    // set parent node
                    neighbour.Parent = curr;

                    // add to frontier list
                    FrontierCells.Push(neighbour);
                }
            }
            // path found
            if (FrontierCells.Contains(_end))
            {
                PathCells = Helper.RetracePath(_start, _end);
                Debug.Log("done");
                yield break;
            }
            while (!Input.GetKeyDown(KeyCode.Space))
                yield return null;

            yield return new WaitForSeconds(0.1f);
        }
        DiagnosticManager.Stop();
    }

    public static void Clear()
    {
        if (FrontierCells != null)
            FrontierCells.Clear();
        if (VisitedCells != null)
            VisitedCells.Clear();
        if (PathCells != null)
            PathCells.Clear();
    }

}
