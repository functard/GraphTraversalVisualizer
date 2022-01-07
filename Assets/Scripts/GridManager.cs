using UnityEngine;

public class GridManager : MonoBehaviour
{
    // A template grid is used because resizing the grid with canvas objects is very slow
    private CellGrid m_TemplateGrid;
    public CellGrid TemplateGrid { get { return m_TemplateGrid; } }

    private CellGrid m_Grid;
    public CellGrid Grid { get { return m_Grid; } }

    [SerializeField] private GameObject m_TemplatePrefab;
    [SerializeField] private GameObject m_GridPrefab;

    /// <summary>
    /// Initilizes template grid for resizing.
    /// </summary>
    /// <param name="_width">number of row cells</param>
    /// <param name="_heigth">number of column cells</param>
    public void InitTemplateGrid(int _width, int _heigth)
    {
        if (m_Grid != null)
            m_Grid.Destroy();

        m_TemplateGrid = new CellGrid(_width, _heigth, m_TemplatePrefab);
    }

    /// <summary>
    /// Initilizes the actual grid with UI components.
    /// </summary>
    public void InitGrid()
    {
        if (m_Grid != null)
            m_Grid.Reset();

        m_TemplateGrid.Destroy();
        m_Grid = new CellGrid(m_TemplateGrid.Width, m_TemplateGrid.Height, m_GridPrefab);
    }

    public void OnClick_GenerateNoiseMap()
    {
        NoiseMapGenerator.GenerateMap(m_Grid.Width, m_Grid.Height, m_Grid);
    }
}