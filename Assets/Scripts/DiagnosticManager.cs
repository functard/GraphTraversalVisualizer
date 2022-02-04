using UnityEngine;
using TMPro;

public class DiagnosticManager : MonoBehaviour
{
    private static int m_IterationCount;
    private static float m_EllapsedTime;

    [SerializeField]
    private TextMeshPro m_Text;

    public static void Start()
    {
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
        Debug.Log("iteration : " + m_IterationCount + "Time : " + m_EllapsedTime);
    }
}
