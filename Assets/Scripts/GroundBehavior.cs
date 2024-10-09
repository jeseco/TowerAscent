using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBehavior : MonoBehaviour
{
    public void DestroyGround()
    {
        Destroy(gameObject);
    }
}
