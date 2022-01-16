using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GreedyBestFirstSearch
{
    public static PriorityQueue<Cell> FrontierCells { get; private set; }
    public static List<Cell> VisitedCells { get; private set; }
    public static List<Cell> PathCells { get; private set; }

    public static void FindPath(Cell _start, Cell _end, EMovementSettings _movementSettings, CellGrid _grid, VisualizationSetting.EVisualizationType _type)
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
        FrontierCells = new PriorityQueue<Cell>();
        VisitedCells = new List<Cell>();
        PathCells = new List<Cell>();

        FrontierCells.Enqueue(_start);

        while (FrontierCells.Count() > 0)
        {
            Cell curr = FrontierCells.Dequeue();
            VisitedCells.Add(curr);

            // found path
            if (curr == _end)
            {
                PathCells = Helper.RetracePath(_start, _end);
                return;
            }

            foreach (Cell neighbour in curr.GetNeighbours(_movementSettings, _grid))
            {
                if (!VisitedCells.Contains(neighbour))
                {
                    int dist = Helper.GetDistance(curr, neighbour);
                    int newCost = dist + neighbour.DistTraveled + curr.Weigth;
                    neighbour.DistTraveled = newCost;
                    neighbour.Parent = curr;

                    neighbour.Priority = Helper.GetDistance(neighbour, _end);

                    FrontierCells.Enqueue(neighbour);
                }
            }

        }
    }
    private static IEnumerator FindPathWithDelay(Cell _start, Cell _end, EMovementSettings _movementSettings, CellGrid _grid)
    {
        FrontierCells = new PriorityQueue<Cell>();
        VisitedCells = new List<Cell>();
        PathCells = new List<Cell>();

        FrontierCells.Enqueue(_start);

        while (FrontierCells.Count() > 0)
        {
            Cell curr = FrontierCells.Dequeue();
            VisitedCells.Add(curr);

            // found path
            if (curr == _end)
            {
                PathCells = Helper.RetracePath(_start, _end);
                break;
            }

            foreach (Cell neighbour in curr.GetNeighbours(_movementSettings, _grid))
            {
                if (!VisitedCells.Contains(neighbour))
                {
                    int dist = Helper.GetDistance(curr, neighbour);
                    int newCost = dist + neighbour.DistTraveled + curr.Weigth;
                    neighbour.DistTraveled = newCost;
                    neighbour.Parent = curr;

                    neighbour.Priority = Helper.GetDistance(neighbour, _end);

                    FrontierCells.Enqueue(neighbour);
                }
            }
            yield return new WaitForSeconds(Helper.TimeStep);
        }
    }
    private static IEnumerator FindPathWithInput(Cell _start, Cell _end, EMovementSettings _movementSettings, CellGrid _grid)
    {
        FrontierCells = new PriorityQueue<Cell>();
        VisitedCells = new List<Cell>();
        PathCells = new List<Cell>();

        FrontierCells.Enqueue(_start);

        while (FrontierCells.Count() > 0)
        {
            Cell curr = FrontierCells.Dequeue();
            VisitedCells.Add(curr);

            // found path
            if (curr == _end)
            {
                PathCells = Helper.RetracePath(_start, _end);
                break;
            }

            foreach (Cell neighbour in curr.GetNeighbours(_movementSettings, _grid))
            {
                if (!VisitedCells.Contains(neighbour))
                {
                    int dist = Helper.GetDistance(curr, neighbour);
                    int newCost = dist + neighbour.DistTraveled + curr.Weigth;
                    neighbour.DistTraveled = newCost;
                    neighbour.Parent = curr;

                    neighbour.Priority = Helper.GetDistance(neighbour, _end);

                    FrontierCells.Enqueue(neighbour);
                }
            }
            while (!Input.GetKey(KeyCode.Space))
                yield return null;

            yield return new WaitForSeconds(Helper.TimeStep);
        }
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
