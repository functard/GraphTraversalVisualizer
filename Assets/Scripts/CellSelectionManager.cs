using UnityEngine;

public class CellSelectionManager : MonoBehaviour
{
    [SerializeField] private Sprite m_DefaultImage;
    [SerializeField] private Sprite m_StartCellImage;
    [SerializeField] private Sprite m_EndCellImage;
    [SerializeField] private Color m_WallCellColor = Color.black;
    [SerializeField] private Color m_GrassColor = Color.green;
    [SerializeField] private Color m_WaterColor = Color.blue;
    [SerializeField] private Color m_MuddColor = new Color(123, 0, 0);

    private enum CellSelection { DEFAULT, START, END, WALL, GRASS, MUDD, WATER }

    private CellSelection m_CellSelection;

    private Camera m_cam;

    private Vector2Int m_StartPos;
    private Vector2Int m_EndPos;

    [SerializeField] private GridManager m_GridManager;

    public void Init()
    {
        m_cam = Camera.main;
        m_CellSelection = CellSelection.START;

        //initialize starting cell as bottom left corner
        m_StartPos = new Vector2Int(0, 0);

        //initialize goal cell as top right corner
        m_EndPos = new Vector2Int(m_GridManager.TemplateGrid.Width - 1, m_GridManager.TemplateGrid.Height - 1);

        // set start and end cell images
        m_GridManager.Grid.GetNodeAtPosition(m_StartPos).SpriteRenderer.sprite = m_StartCellImage;
        m_GridManager.Grid.GetNodeAtPosition(m_EndPos).SpriteRenderer.sprite = m_EndCellImage;
    }

    private void Update()
    {
        if (AppManager.Instance.AppState != AppManager.AppStates.CELL_SELECTION)
            return;

        // right mouse click
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Vector3 mouseWorldPos = m_cam.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.RoundToInt(mouseWorldPos.x);
            int y = Mathf.RoundToInt(mouseWorldPos.y);

            // bounds check
            if (x < -0.5f || x > m_GridManager.Grid.Width - 0.5f ||
                y < -0.5f || y > m_GridManager.Grid.Height - 0.5f)
                return;

            // check if clicked on start or ending node position
            if (new Vector2(x, y) == m_StartPos || new Vector2(x, y) == m_EndPos)
                return;


            // reset weight
            m_GridManager.Grid.GetNodeAtPosition(x, y).Weigth = 0;
            //m_GridManager.Grid.GetNodeAtPosition(m_StartPos).SpriteRenderer.sprite = m_DefaultImage;

            switch (m_CellSelection)
            {
                case CellSelection.DEFAULT:
                    m_GridManager.Grid.GetNodeAtPosition(x, y).Walkable = true;
                    m_GridManager.Grid.GetNodeAtPosition(x, y).SpriteRenderer.color = Color.white;
                    m_GridManager.Grid.GetNodeAtPosition(x, y).Weigth = 0;

                    break;
                // start cell selection
                case CellSelection.START:
                    // reset old selection
                    m_GridManager.Grid.GetNodeAtPosition(m_StartPos).SpriteRenderer.sprite = m_DefaultImage;

                    // update new selection
                    m_StartPos.x = x;
                    m_StartPos.y = y;
                    m_GridManager.Grid.GetNodeAtPosition(m_StartPos).SpriteRenderer.sprite = m_StartCellImage;

                    // Make the cell walkable in case the wall was replaced by start cell
                    m_GridManager.Grid.GetNodeAtPosition(m_StartPos).Walkable = true;
                    break;

                // end cell selection 
                case CellSelection.END:
                    // reset old selection
                    m_GridManager.Grid.GetNodeAtPosition(m_EndPos).SpriteRenderer.sprite = m_DefaultImage;

                    // update new selection
                    m_EndPos.x = x;
                    m_EndPos.y = y;
                    m_GridManager.Grid.GetNodeAtPosition(m_EndPos).SpriteRenderer.sprite = m_EndCellImage;

                    // Make the cell walkable in case the wall was replaced by end cell
                    m_GridManager.Grid.GetNodeAtPosition(m_EndPos).Walkable = true;
                    break;

                // wall cell selection
                case CellSelection.WALL:
                    m_GridManager.Grid.GetNodeAtPosition(x, y).Walkable = false;
                    m_GridManager.Grid.GetNodeAtPosition(x, y).SpriteRenderer.color = m_WallCellColor;
                    break;
                case CellSelection.GRASS:
                    m_GridManager.Grid.GetNodeAtPosition(x, y).Walkable = true;
                    m_GridManager.Grid.GetNodeAtPosition(x, y).Weigth = 4;
                    m_GridManager.Grid.GetNodeAtPosition(x, y).SpriteRenderer.color = m_GrassColor;
                    break;
                case CellSelection.WATER:
                    m_GridManager.Grid.GetNodeAtPosition(x, y).Walkable = true;
                    m_GridManager.Grid.GetNodeAtPosition(x, y).Weigth = 9;
                    m_GridManager.Grid.GetNodeAtPosition(x, y).SpriteRenderer.color = m_WaterColor;
                    break;
                case CellSelection.MUDD:
                    m_GridManager.Grid.GetNodeAtPosition(x, y).Walkable = true;
                    m_GridManager.Grid.GetNodeAtPosition(x, y).SpriteRenderer.color = m_MuddColor;
                    m_GridManager.Grid.GetNodeAtPosition(x, y).Weigth = 14;
                    break;
                default:
                    break;
            }
        }
    }

    public Vector2Int GetStartCellPos()
    {
        return m_StartPos;
    }
    public Vector2Int GetEndCellPos()
    {
        return m_EndPos;
    }

    #region Button Functions
    public void OnClick_StartCellSelection()
    {
        m_CellSelection = CellSelection.START;
    }
    public void OnClick_EndCellSelection()
    {
        m_CellSelection = CellSelection.END;
    }
    public void OnClick_DefaultCellSelection()
    {
        m_CellSelection = CellSelection.DEFAULT;
    }
    public void OnClick_WallCellSelection()
    {
        m_CellSelection = CellSelection.WALL;
    }
    public void OnClick_WaterCellSelection()
    {
        m_CellSelection = CellSelection.WATER;
    }
    public void OnClick_GrassCellSelection()
    {
        m_CellSelection = CellSelection.GRASS;
    }

    public void OnClick_MuddCellSelection()
    {
        m_CellSelection = CellSelection.MUDD;
    }
    #endregion
}