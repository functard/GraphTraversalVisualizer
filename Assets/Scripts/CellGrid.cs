using System.Collections.Generic;
using UnityEngine;

public class CellGrid
{
    private int m_Width;
    public int Width { get { return m_Width; } }

    private int m_Height;
    public int Height { get { return m_Height; } }

    // Grid is stored in a list for dynamicality
    private List<Cell> m_Grid = new List<Cell>();

    private GameObject m_Prefab;

    public CellGrid(int _rows, int _cols, GameObject _prefab)
    {
        m_Width = _rows;
        m_Height = _cols;
        m_Prefab = _prefab;

        for (int h = 0; h < m_Height; h++)
        {
            for (int w = 0; w < m_Width; w++)
            {
                m_Grid.Add(new Cell(w, h, m_Prefab));
            }
        }
    }

    public void Reset()
    {
        foreach (Cell cell in m_Grid)
        {
            cell.Reset();
        }
    }

    public void Destroy()
    {
        foreach (Cell cell in m_Grid)
        {
            GameObject.Destroy(cell.Prefab);
        }
        m_Grid.Clear();
    }

    public void UpdateGrid(int _width, int _heigth)
    {
        // if size decreased
        if (_width < m_Width)
        {
            // ToArray is used to prevent modifying the collection in the loop
            foreach (Cell c in m_Grid.ToArray())
            {
                if (c.X >= _width || c.Y >= _heigth)
                {
                    Object.Destroy(c.Prefab);
                    m_Grid.Remove(c);
                }
            }
        }
        // if size increasead
        else
        {
            for (int h = 0; h < _heigth; h++)
            {
                for (int w = m_Width; w < _width; w++)
                {
                    m_Grid.Add(new Cell(w, h, m_Prefab));
                }

                // top side

                // -------------
                // -------------
                // # # # # # # #
                // # # # # # # #
                // # # # # # # #
                // # # # # # # #
                // # # # # # # #
                if (h >= m_Height)
                {
                    for (int i = 0; i < m_Width; i++)
                    {
                        m_Grid.Add(new Cell(i, h, m_Prefab));
                    }
                }
            }
        }
        m_Width = _width;
        m_Height = _heigth;
    }
  
    public Cell GetNodeAtPosition(int _x, int _y)
    {
        return m_Grid[_y * m_Width + _x];
    }

    public Cell GetNodeAtPosition(Vector2Int _index)
    {
        return m_Grid[_index.y * m_Width + _index.x];
    }

    public int Length()
    {
        return m_Grid.Count;
    }

    public List<Cell> ToList()
    {
        return m_Grid;
    }
}
