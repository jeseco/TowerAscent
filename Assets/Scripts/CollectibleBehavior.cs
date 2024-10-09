using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        PlayerController l_PlayerController = collision.GetComponent<PlayerController>();
        if (l_PlayerController != null )
        {
            l_PlayerController.JumpBoost();
        }
    }
}
