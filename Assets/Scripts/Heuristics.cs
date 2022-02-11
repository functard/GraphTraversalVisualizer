using System.Collections;
using UnityEngine;
using static VisualizationSetting;

public static class Heuristics
{
    public static int Heuristic(Cell _a, Cell _b, EHeuristics _type)
    {
        switch (_type)
        {
            case EHeuristics.EUCLIDIAN:
                return Euclidian(_a, _b);
            case EHeuristics.MANHATTAN:
                return Manhattan(_a, _b);
            case EHeuristics.OCTILE:
                return Octile(_a, _b);
            default:
                return -1;
        }
    }

    //
    // values are multiplied by 14 (diagonal) and 10 (vertical/horizontal) in order to get rid of the decimal places
    //

    private static int Euclidian(Cell _a, Cell _b)
    {
        // min of dx,dy gives the needed diagonal movements, rest is horizontal/vertical(e.g cost of 1)
        int dx = Mathf.Abs(_a.X - _b.X);
        int dy = Mathf.Abs(_a.Y - _b.Y);

        //if min needed diagonal is dy
        if (dx > dy)
            return 14 * dy + 10 * Mathf.Abs(dx - dy);

        //if min needed diagonal is dx
        return 14 * dx + 10 * Mathf.Abs(dy - dx);
    }

    private static int Manhattan(Cell _a, Cell _b)
    {
        int dx = Mathf.Abs(_a.X - _b.X);
        int dy = Mathf.Abs(_a.Y - _b.Y);
        return (dx + dy) * 10;
    }

    private static int Octile(Cell _a, Cell _b)
    {
        // 1.4 * min(dx,dy) + dx - dy
        int dx = Mathf.Abs(_a.X - _b.X);
        int dy = Mathf.Abs(_a.Y - _b.Y);

        return 14 * Mathf.Min(dx, dy) + 10 * Mathf.Abs(dx - dy);
    }
}
