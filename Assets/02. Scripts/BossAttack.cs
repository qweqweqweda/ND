using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PLAYER"))
        {
            print("¡¢√À");
            GameManager.instance.map.moveSpeed = 0f;

            GameManager.instance.isGameOver = true;
        }
    }
}
