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

    private enum CellSelection { Default, Start, End, Wall, Grass, Mudd, Water }

    private CellSelection m_CellSelection;

    private Camera m_cam;

    private Vector2Int m_StartPos;
    private Vector2Int m_EndPos;

    [SerializeField] private GridManager m_GridManager;

    public void Init()
    {
        m_cam = Camera.main;
        m_CellSelection = CellSelection.Start;

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
                case CellSelection.Default:
                    m_GridManager.Grid.GetNodeAtPosition(x, y).Walkable = true;
                    m_GridManager.Grid.GetNodeAtPosition(x, y).SpriteRenderer.color = Color.white;
                    m_GridManager.Grid.GetNodeAtPosition(x, y).Weigth = 0;

                    break;
                // start cell selection
                case CellSelection.Start:
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
                case CellSelection.End:
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
                case CellSelection.Wall:

                    m_GridManager.Grid.GetNodeAtPosition(x, y).Walkable = false;
                    m_GridManager.Grid.GetNodeAtPosition(x, y).SpriteRenderer.color = m_WallCellColor;
                    break;

                case CellSelection.Grass:
                    m_GridManager.Grid.GetNodeAtPosition(x, y).Weigth = 3;
                    m_GridManager.Grid.GetNodeAtPosition(x, y).SpriteRenderer.color = m_GrassColor;
                    break;
                case CellSelection.Water:
                    Debug.Log("?");
                    m_GridManager.Grid.GetNodeAtPosition(x, y).Weigth = 5;
                    m_GridManager.Grid.GetNodeAtPosition(x, y).SpriteRenderer.color = m_WaterColor;
                    break;
                case CellSelection.Mudd:
                    Debug.Log("mydd");
                    m_GridManager.Grid.GetNodeAtPosition(x, y).SpriteRenderer.color = m_MuddColor;
                    m_GridManager.Grid.GetNodeAtPosition(x, y).Weigth = 7;
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
        m_CellSelection = CellSelection.Start;
    }
    public void OnClick_EndCellSelection()
    {
        m_CellSelection = CellSelection.End;
    }
    public void OnClick_DefaultCellSelection()
    {
        m_CellSelection = CellSelection.Default;
    }
    public void OnClick_WallCellSelection()
    {
        m_CellSelection = CellSelection.Wall;
    }
    public void OnClick_WaterCellSelection()
    {
        m_CellSelection = CellSelection.Water;
    }
    public void OnClick_GrassCellSelection()
    {
        m_CellSelection = CellSelection.Grass;
    }

    public void OnClick_MuddCellSelection()
    {
        m_CellSelection = CellSelection.Mudd;
        Debug.Log(m_MuddColor);
    }
    #endregion
}