using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public enum ECellType
{
    NORMAL,
    GRASS,
    MUDD,
    WATER
}

public class Cell : IComparable<Cell>
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Cell Parent;

    public bool Walkable;

    public int Weigth;

    public int Priority;

    // Astar fields
    public int G, H;
    public int F { get { return G + H; } set { F = value; } }

    //Dijkstra field
    public int DistTraveled;

    // Visuals
    public GameObject ParentArrowPrefab;
    public GameObject Prefab;
    public SpriteRenderer SpriteRenderer;

    public TextMeshPro FCostText;
    public TextMeshPro HCostText;
    public TextMeshPro GCostText;
    public TextMeshPro DistTraveledText;

    private Sprite m_DefaultSprite;
    private Vector2Int[] m_NeighborDirections = new Vector2Int[]
    {
        new Vector2Int( 1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int( 0, 1),
        new Vector2Int( 0,-1),
        new Vector2Int( 1, 1),
        new Vector2Int( 1,-1),
        new Vector2Int(-1, 1),
        new Vector2Int(-1,-1)
    };

    public Cell(int _x, int _y, GameObject _prefab)
    {
        X = _x;
        Y = _y;
        Walkable = true;
        Weigth = 0;

        InitVisualFields(_prefab);
        G = 0;
        H = 0;

        DistTraveled = -1;
    }
    public void Reset()
    {
        G = 0;
        H = 0;
        DistTraveled = -1;
        Weigth = 0;
        SpriteRenderer.color = Color.white;
        SpriteRenderer.sprite = m_DefaultSprite;
        Parent = null;
        Walkable = true;
    }
    private void InitVisualFields(GameObject _prefab)
    {
        if (_prefab == null)
            return;

        Prefab = UnityEngine.Object.Instantiate(_prefab, new Vector3(X, Y), Quaternion.identity);

        if (Prefab.transform.childCount >= 1)
        {
            ParentArrowPrefab = Prefab.transform.GetChild(0).gameObject;

            TextMeshPro[] costTexts = Prefab.GetComponentsInChildren<TextMeshPro>(true);
            GCostText = costTexts[0];
            HCostText = costTexts[1];
            FCostText = costTexts[2];
            DistTraveledText = costTexts[3];
        }
        SpriteRenderer = Prefab.GetComponent<SpriteRenderer>();
        m_DefaultSprite = SpriteRenderer.sprite;
#if UNITY_EDITOR
        _prefab.name = "X : " + X.ToString() + " " + "Y : " + Y.ToString();
#endif
    }

    public List<Cell> GetNeighbours(EMovementSettings _movementSettings, CellGrid _grid)
    {
        List<Cell> neighbours = new List<Cell>();

        switch (_movementSettings)
        {
            case EMovementSettings.NoDiagonal:
                for (int w = -1; w <= 1; w += 2)
                {
                    int checkX = w + X;

                    if (checkX >= 0 && checkX < _grid.Width && _grid.GetNodeAtPosition(checkX, Y).Walkable)
                        neighbours.Add(_grid.GetNodeAtPosition(checkX, Y));
                }

                for (int w = -1; w <= 1; w += 2)
                {
                    int checkY = w + Y;

                    if (checkY >= 0 && checkY < _grid.Height && _grid.GetNodeAtPosition(X, checkY).Walkable)
                        neighbours.Add(_grid.GetNodeAtPosition(X, checkY));
                }

                return neighbours;
            case EMovementSettings.Diagonal:

                //TODO: start with vertical/horizontal, otherwise weigthless algorithms looks off

                //for (int i = 0; i < 8; i++)
                //{
                //    int checkX = m_NeighborDirections[i].x + X;
                //    int checkY = m_NeighborDirections[i].y + Y;

                //    if (checkX >= 0 && checkX < _grid.Width &&
                //        checkY >= 0 && checkY < _grid.Height &&
                //        _grid.GetNodeAtPosition(checkX, checkY).Walkable)
                //    {
                //        neighbours.Add(_grid.GetNodeAtPosition(checkX, checkY));
                //    }
                //}
                //for (int w = -1; w <= 1; w += 2)
                //{
                //    int checkX = w + X;

                //    if (checkX >= 0 && checkX < _grid.Width && _grid.GetNodeAtPosition(checkX, Y).Walkable)
                //        neighbours.Add(_grid.GetNodeAtPosition(checkX, Y));
                //}

                //for (int w = -1; w <= 1; w += 2)
                //{
                //    int checkY = w + Y;

                //    if (checkY >= 0 && checkY < _grid.Height && _grid.GetNodeAtPosition(X, checkY).Walkable)
                //        neighbours.Add(_grid.GetNodeAtPosition(X, checkY));
                //}

                ////if (X - 1 > 0 && Y + 1 < _grid.Height)
                ////    neighbours.Add(_grid.GetNodeAtPosition(X - 1, Y + 1));

                ////if (X - 1 > 0 && Y - 1 > 0)
                ////    neighbours.Add(_grid.GetNodeAtPosition(X - 1, Y - 1));

                ////if (X + 1 < _grid.Width && Y + 1 < _grid.Height)
                ////    neighbours.Add(_grid.GetNodeAtPosition(X + 1, Y + 1));

                ////if (X + 1 > 0 && Y - 1 > 0)
                ////    neighbours.Add(_grid.GetNodeAtPosition(X + 1, Y - 1));

                for (int w = -1; w <= 1; w++)
                {
                    for (int h = -1; h <= 1; h++)
                    {
                        if (w == 0 && h == 0)
                            continue;

                        int checkX = X + w;
                        int checkY = Y + h;

                        if (checkX >= 0 && checkX < _grid.Width &&
                            checkY >= 0 && checkY < _grid.Height && _grid.GetNodeAtPosition(checkX, checkY).Walkable)
                        {
                            neighbours.Add(_grid.GetNodeAtPosition(checkX, checkY));
                        }
                    }
                }

                return neighbours;
            case EMovementSettings.DontCrossCorners:
                for (int w = -1; w <= 1; w++)
                {
                    for (int h = -1; h <= 1; h++)
                    {
                        if (w == 0 && h == 0)
                            continue;


                        int checkX = X + w;
                        int checkY = Y + h;

                        if (checkX >= 0 && checkX < _grid.Width && checkY >= 0 && checkY < _grid.Height && _grid.GetNodeAtPosition(checkX, checkY).Walkable)
                        {
                            if (Mathf.Abs(w) == 1 && Mathf.Abs(h) == 1)
                            {
                                if (!_grid.GetNodeAtPosition(X, checkY).Walkable && !_grid.GetNodeAtPosition(checkX, Y).Walkable)
                                {
                                    continue;
                                }
                            }
                            neighbours.Add(_grid.GetNodeAtPosition(checkX, checkY));
                        }
                    }
                }
                return neighbours;
            default:
                return null;
        }
    }

    public int CompareTo(Cell other)
    {
        // Astar ? TODO: use a nullable type and check for null
        if (DistTraveled == -1 && other.DistTraveled == -1)
        {
            Debug.Log("astar");
            if (F < other.F)
                return -1;
            else if (other.F < F)
                return 1;
            // if both f values are the same
            else
            {
                // compare the h values
                return H.CompareTo(other.H);
            }

        }
        Debug.Log("other");
        //Dijkstra
        if (Priority < other.Priority)
            return -1;
        else if (other.Priority < Priority)
            return 1;
        else
            return 0;
    }
}
