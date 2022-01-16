using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testt : MonoBehaviour
{
    [SerializeField] private GridManager gms;
    [SerializeField] private Texture2D tex;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Cell c = gms.Grid.GetNodeAtPosition(0, 0);
            foreach (var item in c.GetNeighbours(EMovementSettings.Diagonal,gms.Grid))
            {
                item.SpriteRenderer.color = Color.cyan;
            }
        }

    }

}
