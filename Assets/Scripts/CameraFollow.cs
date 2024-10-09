using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_Player;
    [SerializeField] private float m_Offset;

    private Rigidbody2D m_PlayerBody;
    private Rigidbody2D m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerBody = m_Player.GetComponent<Rigidbody2D>();        
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PlayerBody.velocity.y > 0 && (m_Player.transform.position.y - transform.position.y) > m_Offset)
        {
            Vector2 CalculatedVelocity = m_Rigidbody.velocity;
            CalculatedVelocity.y = m_PlayerBody.velocity.y;
            m_Rigidbody.velocity = CalculatedVelocity;
        }
        else
        {
            m_Rigidbody.velocity = Vector2.zero;
        }

    }
}
