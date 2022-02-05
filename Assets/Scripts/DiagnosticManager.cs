using UnityEngine;
using TMPro;

public class DiagnosticManager : MonoBehaviour
{
    private static DiagnosticManager m_Instance;

    private static int m_IterationCount;
    private static float m_EllapsedTime;

    [SerializeField]
    private TextMeshProUGUI m_IterationsText;

    [SerializeField]
    private TextMeshProUGUI m_EllapsedTimeText;

    [SerializeField]
    private TextMeshProUGUI m_PathCountText;

    private void Awake()
    {
        if (m_Instance != null && m_Instance != this)
            Destroy(this.gameObject);
        else
            m_Instance = this;
    }

    public static void Start()
    {
        m_Instance.ResetDiagnosticTexts();
        m_IterationCount = 0;
        m_EllapsedTime = Time.realtimeSinceStartup;
    }
    public static void Record()
    {
        m_IterationCount++;
    }
    public static void Stop()
    {
        m_EllapsedTime = Time.realtimeSinceStartup - m_EllapsedTime;
        m_Instance.DisplayDiagnosticsText();
        Debug.Log("iteration : " + m_IterationCount + "Time : " + m_EllapsedTime);
    }
    private void DisplayDiagnosticsText()
    {
        m_IterationsText.text = "Iterations : " + m_IterationCount.ToString(); 
        m_EllapsedTimeText.text ="Ellapsed Time :" + m_EllapsedTime.ToString();
    }

    private void ResetDiagnosticTexts()
    {
        m_IterationsText.text = "Iterations : ";
        m_EllapsedTimeText.text = "Ellapsed Time :";
    }
}