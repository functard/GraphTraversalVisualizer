using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMovementSettings { Diagonal, NoDiagonal, DontCrossCorners }

public class VisualizationSetting : MonoBehaviour
{
    public enum EHeuristics { EUCLIDIAN, MANHATTAN, OCTILE }
    public enum EAlgorihmType { ASTAR, DIJKSTRA, BFS, DFS }
    public enum EVisualizationType { DELAYED,INSTANT,INPUT }

    public EHeuristics HeuristicType = EHeuristics.EUCLIDIAN;
    public EAlgorihmType AlgorithmType = EAlgorihmType.ASTAR;
    public EMovementSettings MovementType = EMovementSettings.Diagonal;
    public EVisualizationType VisualizationType = EVisualizationType.DELAYED;


    public void OnValueChange_SetMovementType(int _i)
    {
        MovementType = (EMovementSettings)_i;
    }
    public void OnValueChange_SetHeuristicType(int _i)
    {
        HeuristicType = (EHeuristics)_i;
    }
    public void OnValueChange_SetAlgorithmType(int _i)
    {
        AlgorithmType = (EAlgorihmType)_i;
        Debug.Log(AlgorithmType);
    }
    public void OnValueChange_SetVisualizationType(int _i)
    {
        VisualizationType = (EVisualizationType)_i;
    }
}
