using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testt : MonoBehaviour
{
    [SerializeField] private GridManager gms;
    [SerializeField] private Texture2D tex;
    float timer;
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    foreach (var item in gms.Grid.ToList())
        //    {
        //        //Debug.Log("F cost : " + item.F);
        //        Debug.Log("Disttraveled : " + item.DistTraveled);
        //    }
        //}
        timer += Time.deltaTime;
        Debug.Log(timer);
        Debug.Log(Time.realtimeSinceStartup);

    }

}
