using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Astar
{
    public static PriorityQueue<Cell> OpenList;
    public static List<Cell> ClosedList;
    public static List<Cell> PathCells;

    public static void FindPath(Cell _start, Cell _end, EMovementSettings _movementSettings, CellGrid _grid,
                                VisualizationSetting.EVisualizationType _type, VisualizationSetting.EHeuristics _heuristic)
    {
        switch (_type)
        {
            case VisualizationSetting.EVisualizationType.DELAYED:
                CoroutineController.Start(FindPathWithDelay(_start, _end, _movementSettings, _grid, _heuristic));
                break;
            case VisualizationSetting.EVisualizationType.INSTANT:
                FindPathInstant(_start, _end, _movementSettings, _grid, _heuristic);
                break;
            case VisualizationSetting.EVisualizationType.INPUT:
                CoroutineController.Start(FindPathWithInput(_start, _end, _movementSettings, _grid, _heuristic));
                break;
            default:
                break;
        }
    }
    private static void FindPathInstant(Cell _start, Cell _end, EMovementSettings _movementSettings, CellGrid _grid,
                                                                        VisualizationSetting.EHeuristics _heuristic)
    {
        DiagnosticManager.Start();
        OpenList = new PriorityQueue<Cell>();
        ClosedList = new List<Cell>();

        _start.G = 0;
        _start.H = Helper.GetDistance(_start, _end);

        OpenList.Enqueue(_start);

        while (OpenList.Count() > 0)
        {
            DiagnosticManager.Record();

            Cell curr = OpenList.Dequeue();
            ClosedList.Add(curr);

            // found path
            if (curr == _end)
            {
                PathCells = Helper.RetracePath(_start, _end);
                return;
            }

            foreach (Cell neighbour in curr.GetNeighbours(_movementSettings, _grid))
            {
                if (!ClosedList.Contains(neighbour))
                {
                    int newCostToNeighbour = curr.G + Helper.GetDistance(curr, neighbour) + neighbour.Weigth;
                    if (newCostToNeighbour < neighbour.G || !OpenList.Contains(neighbour))
                    {
                        neighbour.G = newCostToNeighbour;
                        neighbour.H = Heuristics.Heuristic(neighbour, _end, _heuristic);
                        neighbour.Parent = curr;
                        neighbour.Priority = neighbour.F;

                        if (!OpenList.Contains(neighbour))
                        {
                            OpenList.Enqueue(neighbour);
                        }
                    }
                }
            }
        }
        DiagnosticManager.Stop();
    }
    private static IEnumerator FindPathWithDelay(Cell _start, Cell _end, EMovementSettings _movementSettings, CellGrid _grid,
                                                                                VisualizationSetting.EHeuristics _heuristic)
    {
        DiagnosticManager.Start();

        OpenList = new PriorityQueue<Cell>();
        ClosedList = new List<Cell>();

        OpenList.Enqueue(_start);

        while (OpenList.Count() > 0)
        {
            DiagnosticManager.Record();

            Cell curr = OpenList.Dequeue();
            ClosedList.Add(curr);

            // found path
            if (curr == _end)
            {
                PathCells = Helper.RetracePath(_start, _end);
                yield break;
            }

            foreach (Cell neighbour in curr.GetNeighbours(_movementSettings, _grid))
            {
                if (!ClosedList.Contains(neighbour))
                {
                    int newCostToNeighbour = curr.G + Helper.GetDistance(curr, neighbour) + neighbour.Weigth;
                    if (newCostToNeighbour < neighbour.G || !OpenList.Contains(neighbour))
                    {
                        neighbour.G = newCostToNeighbour;
                        neighbour.H = Heuristics.Heuristic(neighbour, _end, _heuristic);
                        neighbour.Parent = curr;
                        neighbour.Priority = neighbour.F;

                        if (!OpenList.Contains(neighbour))
                        {
                            OpenList.Enqueue(neighbour);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(Helper.TimeStep);
        }
        DiagnosticManager.Stop();
    }
    private static IEnumerator FindPathWithInput(Cell _start, Cell _end, EMovementSettings _movementSettings, CellGrid _grid, 
                                                                                VisualizationSetting.EHeuristics _heuristic)
    {
        DiagnosticManager.Start();

        OpenList = new PriorityQueue<Cell>();
        ClosedList = new List<Cell>();

        OpenList.Enqueue(_start);

        while (OpenList.Count() > 0)
        {
            DiagnosticManager.Record();
            Cell curr = OpenList.Dequeue();
            ClosedList.Add(curr);

            // found path
            if (curr == _end)
            {
                PathCells = Helper.RetracePath(_start, _end);
                yield break;
            }

            foreach (Cell neighbour in curr.GetNeighbours(_movementSettings, _grid))
            {
                if (!ClosedList.Contains(neighbour))
                {
                    int tentativeScore = curr.G + Helper.GetDistance(curr, neighbour) + neighbour.Weigth;
                    if (tentativeScore < neighbour.G || !OpenList.Contains(neighbour))
                    {
                        neighbour.G = tentativeScore;
                        neighbour.H = Heuristics.Heuristic(neighbour, _end, _heuristic);
                        neighbour.Parent = curr;

                        if (!OpenList.Contains(neighbour))
                        {
                            OpenList.Enqueue(neighbour);
                        }
                    }
                }
            }
            while (!Input.GetKey(KeyCode.Space))
                yield return null;

            yield return new WaitForSeconds(0.2f);
        }
        DiagnosticManager.Stop();
    }

    public static void Clear()
    {
        if (OpenList != null)
            OpenList.Clear();
        if (ClosedList != null)
            ClosedList.Clear();
        if (PathCells != null)
            PathCells.Clear();
    }
}
