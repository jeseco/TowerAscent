using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float m_StartPosition, m_ImageLength;
    [SerializeField] GameObject m_Camera;
    [SerializeField] float m_ParallaxEffectSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_StartPosition = transform.position.y;
        m_ImageLength = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float l_distance = m_Camera.transform.position.y * m_ParallaxEffectSpeed;
        float l_Movement = m_Camera.transform.position.y * (1 - m_ParallaxEffectSpeed);

        transform.position = new Vector3(transform.position.x, m_StartPosition + l_distance, transform.position.z);
        
        if (l_Movement > m_StartPosition + m_ImageLength)
        {
            m_StartPosition += m_ImageLength;
        }
    }
}
