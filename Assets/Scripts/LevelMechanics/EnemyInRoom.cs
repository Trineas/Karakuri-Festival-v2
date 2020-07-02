using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInRoom : MonoBehaviour
{
    private void OnDestroy()
    {
        EnemyDoorController.instance.enemyCounter--;
    }
}
