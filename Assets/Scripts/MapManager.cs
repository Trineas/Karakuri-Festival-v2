using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Transform player;

    public GameObject floor1, floor2, floor3, floor4, floor5;

    void Update()
    {
        if (player.position.y <= 1f)
        {
            floor1.SetActive(true);
            floor2.SetActive(false);
            floor3.SetActive(false);
            floor4.SetActive(false);
            floor5.SetActive(false);
        }

        if (player.position.y == 5.52f)
        {
            floor1.SetActive(false);
            floor2.SetActive(true);
            floor3.SetActive(false);
            floor4.SetActive(false);
            floor5.SetActive(false);
        }

        if (player.position.y == 11.52f)
        {
            floor1.SetActive(false);
            floor2.SetActive(false);
            floor3.SetActive(true);
            floor4.SetActive(false);
            floor5.SetActive(false);
        }

        if (player.position.y == 17.52f)
        {
            floor1.SetActive(false);
            floor2.SetActive(false);
            floor3.SetActive(false);
            floor4.SetActive(true);
            floor5.SetActive(false);
        }

        if (player.position.y == 23.52f)
        {
            floor1.SetActive(false);
            floor2.SetActive(false);
            floor3.SetActive(false);
            floor4.SetActive(false);
            floor5.SetActive(true);
        }
    }
}
