using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public GameObject mapCamera;

    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(player.position.x, 0f, player.position.z);
        transform.position = newPosition + offset;

        if (player.position.y <= 1f)
        {
            mapCamera.transform.position = new Vector3(200f, 13f, 22.5f);
        }

        if (player.position.y == 5.52f)
        {
            mapCamera.transform.position = new Vector3(356.5f, 13f, 22.5f);
        }

        if (player.position.y == 11.52f)
        {
            mapCamera.transform.position = new Vector3(513f, 13f, 22.5f);
        }

        if (player.position.y == 17.52f)
        {
            mapCamera.transform.position = new Vector3(667.5f, 13f, 22.5f);
        }

        if (player.position.y >= 23.52f)
        {
            mapCamera.transform.position = new Vector3(822.8f, 13f, 22.5f);
        }
    }
}
