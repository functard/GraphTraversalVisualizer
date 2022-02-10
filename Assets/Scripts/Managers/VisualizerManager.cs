using System.Collections.Generic;
using UnityEngine;

public class VisualizerManager : MonoBehaviour
{
    [SerializeField] private VisualizationSetting m_VisualizationSettings;

    [SerializeField] private GridManager m_GridManager;
    [SerializeField] private CellSelectionManager m_CellManager;
    [SerializeField] private UIManager m_UIManager;

    [SerializeField] private LineRenderer m_LineRenderer;

    private bool m_ShowArrows = false;
    private bool m_ShowCosts = false;

    //colors
    [SerializeField] private Color m_FrontierCellColor = Color.green;
    [SerializeField] private Color m_VisitedCellColor = Color.red;
    [SerializeField] private Color m_PathCellColor = Color.cyan;

    // ?
    private void Update()
    {
        if (!(AppManager.Instance.AppState == AppManager.EAppStates.RUNNING))
            return;

        ShowParentArrows(m_GridManager.Grid.ToList());
        switch (m_VisualizationSettings.AlgorithmType)
        {
            case VisualizationSetting.EAlgorihmType.BFS:
                PaintCells(BFS.FrontierCells, BFS.VisitedCells, BFS.PathCells);
                break;
            case VisualizationSetting.EAlgorihmType.DIJKSTRA:
                PaintCells(Dijkstra.FrontierCells.ToList(), Dijkstra.VisitedCells, Dijkstra.PathCells);
                //ShowDiagnsostics(Dijkstra.IterationCount, Dijkstra.PathCells.Count, Dijkstra.EllapsedTime);
                ShowDistanceCosts(Dijkstra.FrontierCells.ToList());
                ShowDistanceCosts(Dijkstra.VisitedCells);
                break;
            case VisualizationSetting.EAlgorihmType.A_STAR:
                PaintCells(Astar.OpenList.ToList(), Astar.ClosedList, Astar.PathCells);
                ShowDistanceCosts(Astar.OpenList.ToList());
                ShowDistanceCosts(Astar.ClosedList);
                break;
            case VisualizationSetting.EAlgorihmType.DFS:
                PaintCells(DFS.FrontierCells, DFS.VisitedCells, DFS.PathCells);
                ShowDistanceCosts(DFS.FrontierCells);
                ShowDistanceCosts(DFS.VisitedCells);
                break;
            case VisualizationSetting.EAlgorihmType.GREEDY_BEST:
                PaintCells(GreedyBestFirstSearch.FrontierCells.ToList(), GreedyBestFirstSearch.VisitedCells, GreedyBestFirstSearch.PathCells);
                ShowDistanceCosts(GreedyBestFirstSearch.FrontierCells.ToList());
                ShowDistanceCosts(GreedyBestFirstSearch.VisitedCells);
                break;
            default:
                break;
        }
    }

    public void StartPathFinder()
    {
        switch (m_VisualizationSettings.AlgorithmType)
        {
            case VisualizationSetting.EAlgorihmType.BFS:
                BFS.FindPath(m_GridManager.Grid.GetNodeAtPosition(m_CellManager.GetStartCellPos()),
                m_GridManager.Grid.GetNodeAtPosition(m_CellManager.GetEndCellPos()),
                m_VisualizationSettings.MovementType, m_GridManager.Grid, m_VisualizationSettings.VisualizationType);
                break;
            case VisualizationSetting.EAlgorihmType.DIJKSTRA:
                Dijkstra.FindPath(m_GridManager.Grid.GetNodeAtPosition(m_CellManager.GetStartCellPos()),
                m_GridManager.Grid.GetNodeAtPosition(m_CellManager.GetEndCellPos()),
                 m_VisualizationSettings.MovementType, m_GridManager.Grid, m_VisualizationSettings.VisualizationType,
                 m_VisualizationSettings.HeuristicType);
                break;
            case VisualizationSetting.EAlgorihmType.A_STAR:
                Astar.FindPath(m_GridManager.Grid.GetNodeAtPosition(m_CellManager.GetStartCellPos()),
                m_GridManager.Grid.GetNodeAtPosition(m_CellManager.GetEndCellPos()),
                m_VisualizationSettings.MovementType, m_GridManager.Grid, m_VisualizationSettings.VisualizationType,
                m_VisualizationSettings.HeuristicType);
                break;
            case VisualizationSetting.EAlgorihmType.DFS:
                DFS.FindPath(m_GridManager.Grid.GetNodeAtPosition(m_CellManager.GetStartCellPos()),
                m_GridManager.Grid.GetNodeAtPosition(m_CellManager.GetEndCellPos()),
                m_VisualizationSettings.MovementType, m_GridManager.Grid, m_VisualizationSettings.VisualizationType);
                break;
            case VisualizationSetting.EAlgorihmType.GREEDY_BEST:
                GreedyBestFirstSearch.FindPath(m_GridManager.Grid.GetNodeAtPosition(m_CellManager.GetStartCellPos()),
                m_GridManager.Grid.GetNodeAtPosition(m_CellManager.GetEndCellPos()),
                m_VisualizationSettings.MovementType, m_GridManager.Grid, m_VisualizationSettings.VisualizationType,
                m_VisualizationSettings.HeuristicType);
                break;
            default:
                break;
        }
    }

    private void ShowDiagnsostics(int _iterationCount, int _pathCount, float _ellapsedTime)
    {

        Debug.Log(_ellapsedTime);
        //Debug.Log("it : " + _iterationCount + " path : " + _pathCount + " time : " + _ellapsedTime);
    }

    public void ClearPathLines()
    {
        m_LineRenderer.positionCount = 0;
    }

    #region Private Functions

    private void PaintCells(IEnumerable<Cell> _frontierCells, IEnumerable<Cell> _visitedCells, List<Cell> _pathCells)
    {
        if (_frontierCells == null || _visitedCells == null)
            return;

        foreach (var cell in _frontierCells)
        {
            cell.SpriteRenderer.color = m_FrontierCellColor;
        }

        foreach (var cell in _visitedCells)
        {
            cell.SpriteRenderer.color = m_VisitedCellColor;
        }

        if (_pathCells == null)
            return;

        foreach (var cell in _pathCells)
        {
            cell.SpriteRenderer.color = m_PathCellColor;
        }
        DrawLineBetweenCells(_pathCells);


        m_GridManager.Grid.GetNodeAtPosition(m_CellManager.GetStartCellPos()).SpriteRenderer.color = Color.white;
        //m_GridManager.Grid.GetNodeAtPosition(m_CellManager.GetEndCellPos()).SpriteRenderer.color = Color.white;
    }

    private void ShowDistanceCosts(IEnumerable<Cell> _cells)
    {
        if (!m_ShowCosts)
            return;

        if (m_VisualizationSettings.AlgorithmType == VisualizationSetting.EAlgorihmType.A_STAR)
        {
            foreach (Cell cell in _cells)
            {
                cell.GCostText.text = cell.G.ToString();
                cell.HCostText.text = cell.H.ToString();
                cell.FCostText.text = cell.F.ToString();
            }
        }
        else if (m_VisualizationSettings.AlgorithmType == VisualizationSetting.EAlgorihmType.DIJKSTRA)
        {
            foreach (Cell cell in _cells)
            {
                cell.DistTraveledText.text = cell.DistTraveled.ToString();
            }
        }

        else if (m_VisualizationSettings.AlgorithmType == VisualizationSetting.EAlgorihmType.GREEDY_BEST)
        {
            foreach (Cell cell in _cells)
            {
                cell.DistTraveledText.text = cell.DistTraveled.ToString();
            }
        }
        // clear starting node distance cost texts 
        m_GridManager.Grid.GetNodeAtPosition(m_CellManager.GetStartCellPos()).GCostText.text = "";
        m_GridManager.Grid.GetNodeAtPosition(m_CellManager.GetStartCellPos()).HCostText.text = "";
        m_GridManager.Grid.GetNodeAtPosition(m_CellManager.GetStartCellPos()).FCostText.text = "";
        m_GridManager.Grid.GetNodeAtPosition(m_CellManager.GetStartCellPos()).DistTraveledText.text = "";
    }


    private void ClearDistanceCosts()
    {
        foreach (var cell in m_GridManager.Grid.ToList())
        {
            cell.GCostText.text = "";
            cell.HCostText.text = "";
            cell.FCostText.text = "";
            cell.DistTraveledText.text = "";
        }
    }

    private void ShowParentArrows(IEnumerable<Cell> _cells)
    {
        if (_cells == null || !m_ShowArrows)
            return;

        foreach (Cell cell in _cells)
        {
            if (cell.Parent == null)
                continue;

            // activate arrow game objects
            cell.ParentArrowPrefab.SetActive(true);

            // rotate the arrow towards parent
            cell.ParentArrowPrefab.transform.right = (new Vector3(cell.Parent.X, cell.Parent.Y) - new Vector3(cell.X, cell.Y)).normalized;
        }
        // disable starting node arrow
        m_GridManager.Grid.GetNodeAtPosition(m_CellManager.GetStartCellPos()).ParentArrowPrefab.SetActive(false);
    }

    private void ClearParentArrows()
    {
        foreach (var cell in m_GridManager.Grid.ToList())
        {
            cell.ParentArrowPrefab.SetActive(false);
        }
    }

    private void DrawLineBetweenCells(List<Cell> _path)
    {
        m_LineRenderer.positionCount = _path.Count;

        Vector3[] vertexPositions = new Vector3[_path.Count];

        int i = 0;
        foreach (Cell c in _path)
        {
            // -1 on z axis because order layers doesn't work on sprites
            vertexPositions[i] = new Vector3(c.X, c.Y, -1f);
            i++;
        }
        m_LineRenderer.SetPositions(vertexPositions);
    }
    #endregion


    #region Button Functions

    public void OnClick_GenerateNoiseMap()
    {
        OnClick_ClearGrid();
        NoiseMapGenerator.GenerateMap(m_GridManager.Grid);
    }
    public void OnClick_ClearGrid()
    {
        ClearParentArrows();
        ClearDistanceCosts();
        ClearPathLines();
        m_GridManager.Grid.Reset();
        m_CellManager.Init();
        AppManager.Instance.AppState = AppManager.EAppStates.CELL_SELECTION;
        m_UIManager.HandleCellSelectionStateButtons();
        Helper.ClearAlgorithms();
    }
    public void OnClick_ClearPath()
    {
        ClearParentArrows();
        ClearDistanceCosts();
        ClearPathLines();
        Helper.ClearAlgorithms();
        AppManager.Instance.AppState = AppManager.EAppStates.CELL_SELECTION;
        m_UIManager.HandleCellSelectionStateButtons();
        Helper.ClearAlgorithms();
    }

    public void OnClick_ShowArrows()
    {
        m_ShowArrows = !m_ShowArrows;

        if (m_ShowArrows == false)
            ClearParentArrows();
    }

    public void OnClick_ShowDistanceCosts()
    {
        m_ShowCosts = !m_ShowCosts;

        if (!m_ShowCosts)
            ClearDistanceCosts();
    }

    public void OnSliderChanged_AnimationSpeed(float _speed)
    {
        Helper.TimeStep = _speed;
    }
    #endregion
}
