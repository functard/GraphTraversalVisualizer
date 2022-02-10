using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Buttons
    [SerializeField] private GameObject m_ConfirmButton;
    [SerializeField] private GameObject m_RestartButton;
    [SerializeField] private GameObject m_ResetPathButton;
    [SerializeField] private GameObject m_VisualizeButton;
    [SerializeField] private GameObject m_ResizeSlider;
    [SerializeField] private GameObject m_SettingsPanel;
    [SerializeField] private GameObject[] m_CellSelectionButtons;
    [SerializeField] private GameObject m_ShowDistanceCostButton;
    [SerializeField] private GameObject m_ShowParentButton;
    [SerializeField] private GameObject m_GenerateNoiseGridButton;
    [SerializeField] private GameObject m_DiagnosticsPanel;
    [SerializeField] private Slider m_AnimationSpeedSlider;

    /// <summary>
    /// Activates or deactivates all of the buttons nessecary for the RESIZING state
    /// </summary>
    public void HandleResizeStateButtons()
    {
        ActivateResizeButtons();
        DeactivateCellSelectionButtons();
        m_VisualizeButton.SetActive(false);
        m_SettingsPanel.SetActive(false);
        m_ResetPathButton.SetActive(false);
        m_RestartButton.SetActive(false);
        m_ShowParentButton.SetActive(false);
        m_ShowDistanceCostButton.SetActive(false);
        m_AnimationSpeedSlider.gameObject.SetActive(false);
        m_GenerateNoiseGridButton.SetActive(false);
        m_DiagnosticsPanel.SetActive(false);
    }

    /// <summary>
    /// Activates or deactivates all of the buttons nessecary for the CELL SELECTION state
    /// </summary>
    public void HandleCellSelectionStateButtons()
    {
        DeactivateResizeButtons();
        ActivateCellSelectionButtons();
        m_VisualizeButton.SetActive(true);
        m_SettingsPanel.SetActive(true);
        m_ResetPathButton.SetActive(true);
        m_RestartButton.SetActive(true);
        m_ShowParentButton.SetActive(false);
        m_ShowDistanceCostButton.SetActive(false);
        m_AnimationSpeedSlider.gameObject.SetActive(false);
        m_GenerateNoiseGridButton.SetActive(true);
        m_DiagnosticsPanel.SetActive(false);
    }

    /// <summary>
    /// Activates or deactivates all of the buttons nessecary for the RUNNING state
    /// </summary>
    public void HandleRunningStateButtons()
    {
        DeactivateCellSelectionButtons();
        m_SettingsPanel.SetActive(false);
        m_ShowParentButton.SetActive(true);
        m_ShowDistanceCostButton.SetActive(true);
        m_VisualizeButton.SetActive(false);
        m_AnimationSpeedSlider.gameObject.SetActive(true);
        m_GenerateNoiseGridButton.SetActive(false);
        m_DiagnosticsPanel.SetActive(true);
    }
    #region Button Functions

    public void OnClick_Visualize()
    {
        AppManager.Instance.UpdateAppState(AppManager.EAppStates.RUNNING);
    }
    public void OnClick_ResizeConfirm()
    {
        AppManager.Instance.UpdateAppState(AppManager.EAppStates.CELL_SELECTION);
    }

    public void OnClick_Restart()
    {
        AppManager.Instance.UpdateAppState(AppManager.EAppStates.SIZE_SELECTION);
    }
    #endregion

    private void ActivateResizeButtons()
    {
        m_ConfirmButton.SetActive(true);
        m_ResizeSlider.SetActive(true);
    }

    private void DeactivateResizeButtons()
    {
        m_ConfirmButton.SetActive(false);
        m_ResizeSlider.SetActive(false);
    }

    private void ActivateCellSelectionButtons()
    {
        foreach (GameObject button in m_CellSelectionButtons)
        {
            button.SetActive(true);
        }
    }

    private void DeactivateCellSelectionButtons()
    {
        foreach (GameObject button in m_CellSelectionButtons)
        {
            button.SetActive(false);
        }
    }
}
