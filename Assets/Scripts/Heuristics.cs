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
    private static int Euclidian(Cell _a, Cell _b)
    {
        int dstX = Mathf.Abs(_a.X - _b.X);
        int dstY = Mathf.Abs(_a.Y - _b.Y);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
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
