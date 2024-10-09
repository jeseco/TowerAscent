using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody;
    private bool m_IsFalling;

    [SerializeField] private float m_FallingDelay;
    private float m_TimeElapse;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    public void DestroyPlatform()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (m_IsFalling)
        {
            m_TimeElapse += Time.deltaTime;
            if (m_TimeElapse > m_FallingDelay)
            {
                m_Rigidbody.constraints = RigidbodyConstraints2D.None;
                m_Rigidbody.gravityScale = 3f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController l_PlayerController = collision.gameObject.GetComponent<PlayerController>();
        PlatformBehavior l_PlatformBehavior = collision.gameObject.GetComponent<PlatformBehavior>();
        if (l_PlayerController != null || l_PlatformBehavior != null)
        {
            m_IsFalling = true;
        }
    }
}
