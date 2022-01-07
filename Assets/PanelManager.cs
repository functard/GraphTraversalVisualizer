using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager ActiveManager;

    private RectTransform m_PanelTransform;
    private CanvasGroup m_CanvasGroup;

    [SerializeField] private float m_UndockedHeigth;

    private float m_DockedHeigth;
    private bool m_Docked = true;



    private void Start()
    {
        m_CanvasGroup = GetComponent<CanvasGroup>();
        m_PanelTransform = GetComponent<RectTransform>();
        m_DockedHeigth = m_PanelTransform.rect.height;
    }
    public void Dock()
    {
        ActiveManager = this;

        if (!m_Docked)
        {
            m_PanelTransform.sizeDelta = new Vector2(m_PanelTransform.sizeDelta.x, m_DockedHeigth);
            m_CanvasGroup.alpha = 0;
        }
        else
        {
            m_PanelTransform.sizeDelta = new Vector2(m_PanelTransform.sizeDelta.x, m_UndockedHeigth);
            m_CanvasGroup.alpha = 1;
        }
        m_Docked = !m_Docked;
    }
}
