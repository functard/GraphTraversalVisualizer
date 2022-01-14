using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PngParser : MonoBehaviour
{
    public static CellGrid ParseToGrid(Texture2D _texture, Color _wallColor, Color _walkableColor)
    {
        CellGrid grid = new CellGrid(_texture.width, _texture.height, null);

        for (int w = 0; w < _texture.width; w++)
        {
            for (int h = 0; h < _texture.height; h++)
            {
                int r = Random.Range(0, 100);
                if (r < 10)
                    grid.GetNodeAtPosition(w, h).Walkable = false;

            }
        }

        Astar.FindPath(grid.GetNodeAtPosition(0, 0), grid.GetNodeAtPosition(grid.Width - 1, grid.Height - 1), EMovementSettings.Diagonal, grid, VisualizationSetting.EVisualizationType.INSTANT);

        return grid;
    }
}
