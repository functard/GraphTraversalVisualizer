using System.Collections;
using UnityEngine;
public class CoroutineController : MonoBehaviour
{
    private static CoroutineController m_Manager;

    void Start()
    {
        m_Manager = GetComponent<CoroutineController>();
    }

    public static void Start(IEnumerator routine)
    {
        StopAll();
        m_Manager.StartCoroutine(routine);
    }

    public static void StopAll()
    {
        m_Manager.StopAllCoroutines();
    }

}