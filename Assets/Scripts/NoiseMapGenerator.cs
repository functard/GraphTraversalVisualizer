﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMapGenerator : MonoBehaviour
{
    [SerializeField]
    private static float m_Scale = 14;

    public static void GenerateMap(CellGrid _grid)
    {
        float[,] noise = GenerateNoise(_grid.Width, _grid.Height, m_Scale);

        for (int w = 0; w < _grid.Width; w++)
        {
            for (int h = 0; h < _grid.Height; h++)
            {
                if (noise[w, h] < 0.3f)
                {
                    _grid.GetNodeAtPosition(w, h).SpriteRenderer.color = Color.white;
                    _grid.GetNodeAtPosition(w, h).Weigth = 0;
                }
                else if (noise[w, h] < 0.5f)
                {
                    _grid.GetNodeAtPosition(w, h).SpriteRenderer.color = Color.green;
                    _grid.GetNodeAtPosition(w, h).Weigth = 3;
                }
                else if (noise[w, h] < 0.75f)
                {
                    _grid.GetNodeAtPosition(w, h).SpriteRenderer.color = Color.blue;
                    _grid.GetNodeAtPosition(w, h).Weigth = 5;
                }
                else
                {
                    _grid.GetNodeAtPosition(w, h).SpriteRenderer.color = Color.yellow;
                    _grid.GetNodeAtPosition(w, h).Weigth = 5;
                }
            }
        }
    }

    private static float[,] GenerateNoise(int _width, int _heigth, float _scale)
    {
        if (_scale == 0)
        {
            Debug.Log("Scale value needs to be greater than 0");
            return null;
        }

        float seed = Random.Range(0, 1000);
        float[,] map = new float[_width, _heigth];

        for (int w = 0; w < _width; w++)
        {
            for (int h = 0; h < _heigth; h++)
            {
                map[w, h] = Mathf.PerlinNoise(w / _scale + seed, h / _scale + seed);
            }
        }
        return map;
    }
}