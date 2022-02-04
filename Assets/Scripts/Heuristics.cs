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
        return dx + dy;
    }

    private static int Octile(Cell _a, Cell _b)
    {
        int dstX = Mathf.Abs(_a.X - _b.X);
        int dstY = Mathf.Abs(_a.Y - _b.Y);
        if (dstX < dstY)
            return 10 * dstX + 4 * dstY;
        else
            return 10 * dstY + 4 * dstX;
    }
}
