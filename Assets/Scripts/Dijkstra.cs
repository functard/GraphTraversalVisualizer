using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra
{
    public static PriorityQueue<Cell> FrontierCells { get; private set; }
    public static List<Cell> VisitedCells { get; private set; }

    public static List<Cell> PathCells { get; private set; }
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
    public static void FindPathInstant(Cell _start, Cell _end, EMovementSettings _movementSettings, CellGrid _grid,
                                                                        VisualizationSetting.EHeuristics _heuristic)
    {
        DiagnosticManager.Start();
        FrontierCells = new PriorityQueue<Cell>();
        VisitedCells = new List<Cell>();

        FrontierCells.Enqueue(_start);
        _start.DistTraveled = 0;

        while (FrontierCells.Count() > 0)
        {
            DiagnosticManager.Record();
            Cell curr = FrontierCells.Dequeue();

            // add to explored list
            VisitedCells.Add(curr);

            if (curr == _end)
            {
                PathCells = Helper.RetracePath(_start, _end);
                return;
            }

            // for every neighbour
            foreach (Cell neighbour in curr.GetNeighbours(_movementSettings, _grid))
            {
                // if not in any of the lists
                if (!VisitedCells.Contains(neighbour))
                {
                    int newDistTraveled = Heuristics.Heuristic(curr, neighbour, _heuristic) + curr.DistTraveled + neighbour.Weigth;

                    // if Distance not updated or new distance is better
                    if (neighbour.DistTraveled == -1 || newDistTraveled < neighbour.DistTraveled)
                    {
                        neighbour.DistTraveled = newDistTraveled;
                        // set parent node
                        neighbour.Parent = curr;

                        neighbour.Priority = neighbour.DistTraveled;
                        FrontierCells.Enqueue(neighbour);

                    }
                }
            }
        }
        DiagnosticManager.Stop();
    }

    public static IEnumerator FindPathWithDelay(Cell _start, Cell _end, EMovementSettings _movementSettings, CellGrid _grid,
                                                                                VisualizationSetting.EHeuristics _heuristic)
    {
        DiagnosticManager.Start();
        FrontierCells = new PriorityQueue<Cell>();
        VisitedCells = new List<Cell>();
        PathCells = new List<Cell>();

        FrontierCells.Enqueue(_start);
        _start.DistTraveled = 0;
        while (FrontierCells.Count() > 0)
        {
            DiagnosticManager.Record();
            Cell curr = FrontierCells.Dequeue();

            // add to explored list
            VisitedCells.Add(curr);

            if (curr == _end)
            {
                PathCells = Helper.RetracePath(_start, _end);
                yield break;
            }

            // for every neighbour
            foreach (Cell neighbour in curr.GetNeighbours(_movementSettings, _grid))
            {
                // if not in any of the lists
                if (!VisitedCells.Contains(neighbour))
                {
                    int newDistTraveled = Heuristics.Heuristic(curr, neighbour, _heuristic) + curr.DistTraveled + neighbour.Weigth;
                    // if Distance not updated or new distance is better
                    if (neighbour.DistTraveled == -1 || newDistTraveled < neighbour.DistTraveled)
                    {
                        neighbour.DistTraveled = newDistTraveled;
                        // set parent node
                        neighbour.Parent = curr;

                        neighbour.Priority = neighbour.DistTraveled;
                        FrontierCells.Enqueue(neighbour);

                    }
                }
            }
            yield return new WaitForSeconds(Helper.TimeStep);
        }
        DiagnosticManager.Stop();
    }

    public static IEnumerator FindPathWithInput(Cell _start, Cell _end, EMovementSettings _movementSettings, CellGrid _grid,
                                                                                VisualizationSetting.EHeuristics _heuristic)
    {
        DiagnosticManager.Start();
        FrontierCells = new PriorityQueue<Cell>();
        VisitedCells = new List<Cell>();

        FrontierCells.Enqueue(_start);
        _start.DistTraveled = 0;
        while (FrontierCells.Count() > 0)
        {
            DiagnosticManager.Record();
            Cell curr = FrontierCells.Dequeue();

            // add to explored list
            VisitedCells.Add(curr);

            if (curr == _end)
            {
                PathCells = Helper.RetracePath(_start, _end);
                yield break;
            }

            // for every neighbour
            foreach (Cell neighbour in curr.GetNeighbours(_movementSettings, _grid))
            {
                // if not in any of the lists
                if (!VisitedCells.Contains(neighbour))
                {
                    int newDistTraveled = Heuristics.Heuristic(curr, neighbour, _heuristic) + curr.DistTraveled + neighbour.Weigth;

                    // if Distance not updated or new distance is better
                    if (neighbour.DistTraveled == -1 || newDistTraveled < neighbour.DistTraveled)
                    {
                        neighbour.DistTraveled = newDistTraveled;
                        // set parent node
                        neighbour.Parent = curr;

                        neighbour.Priority = neighbour.DistTraveled;
                        FrontierCells.Enqueue(neighbour);

                    }
                }
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

