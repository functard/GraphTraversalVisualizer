using UnityEngine;

public class AppManager : MonoBehaviour
{
    //Managers
    [SerializeField] private GridManager m_GridManager;
    [SerializeField] private UIManager m_UIManager;
    [SerializeField] private CamManager m_CamManager;
    [SerializeField] private CellSelectionManager m_CellSelectionManager;
    [SerializeField] private VisualizerManager m_VisualizerManager;

    public enum AppStates { SIZE_SELECTION, CELL_SELECTION, RUNNING }

    [HideInInspector]
    public AppStates AppState;

    public static AppManager Instance { get { return m_Instance; } }
    private static AppManager m_Instance;

    private void Awake()
    {
        if (m_Instance != null && m_Instance != this)
            Destroy(this.gameObject);
        else
            m_Instance = this;
    }

    public void UpdateAppState(AppStates _state)
    {
        // if same state
        if (AppState == _state)
            return;

        switch (_state)
        {
            case AppStates.SIZE_SELECTION:
                m_UIManager.HandleResizeStateButtons();
                m_CamManager.Init();
                m_VisualizerManager.ClearPathLines();
                Helper.ClearAlgorithms();
                break;
            case AppStates.CELL_SELECTION:
                m_UIManager.HandleCellSelectionStateButtons();
                m_GridManager.InitGrid();
                m_CellSelectionManager.Init();
                break;
            case AppStates.RUNNING:
                m_UIManager.HandleRunningStateButtons();
                m_VisualizerManager.StartPathFinder();
                break;
            //case AppStates.FINISHED:
            //    m_UIManager.HandleFinishedStateButtons();
            //    break;
            default:
                break;
        }
        AppState = _state;
    }
}
