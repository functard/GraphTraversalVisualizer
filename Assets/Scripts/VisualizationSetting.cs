using UnityEngine;

public enum EMovementSettings { DIAGONAL, NO_DIAGONAL, DONT_CROSS_CORNERS }

public class VisualizationSetting : MonoBehaviour
{
    public enum EHeuristics { EUCLIDIAN, MANHATTAN, OCTILE }
    public enum EAlgorihmType { A_STAR, DIJKSTRA, BFS, DFS, GREEDY_BEST }
    public enum EVisualizationType { DELAYED,INSTANT,INPUT }

    public EHeuristics HeuristicType = EHeuristics.EUCLIDIAN;
    public EAlgorihmType AlgorithmType = EAlgorihmType.A_STAR;
    public EMovementSettings MovementType = EMovementSettings.DIAGONAL;
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
    }
    public void OnValueChange_SetVisualizationType(int _i)
    {
        VisualizationType = (EVisualizationType)_i;
    }
}
