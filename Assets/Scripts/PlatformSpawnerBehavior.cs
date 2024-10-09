using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnerBehavior : MonoBehaviour
{
    [SerializeField] private GameObject m_PlatformPrefabs;
    [SerializeField] private GameObject m_WallPrefabs;
    [SerializeField] private GameObject m_CollectiblePrefabs;
    [SerializeField] private Transform m_Player;

    [Header("Offset with the player")]
    [SerializeField] private float m_Offset;

    [SerializeField] private float m_SpawnHeight;

    [Header("Walls")]
    [SerializeField] private Vector2 m_WallSpawnWidth;
    [SerializeField] private float m_WallY;
    [SerializeField] private float m_WallSpawnHeight;

    private Rigidbody2D m_PlayerBody;
    private Rigidbody2D m_Rigidbody;

    private Vector2 m_RecordedPosition;

    void Start()
    {
        m_PlayerBody = m_Player.GetComponent<Rigidbody2D>(); 
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_RecordedPosition = transform.position;
    }

    void Update()
    {
        if (m_PlayerBody.velocity.y > 0 && (transform.position.y - m_PlayerBody.transform.position.y) < m_Offset)
        {
            Vector2 CalculatedVelocity = m_Rigidbody.velocity;
            CalculatedVelocity.y = m_PlayerBody.velocity.y;
            m_Rigidbody.velocity = CalculatedVelocity;
        }
        else
        {
            m_Rigidbody.velocity = Vector2.zero;
        }

        if ((transform.position.y - m_RecordedPosition.y) >= m_SpawnHeight)
        {
            Instantiate(m_PlatformPrefabs, new Vector3(Random.Range(-7, 7), transform.position.y, 0), Quaternion.identity);
            Instantiate(m_CollectiblePrefabs, new Vector3(Random.Range(-9, 9), transform.position.y, 0), Quaternion.identity);
            m_RecordedPosition = transform.position;
        }
        if (transform.position.y >= m_WallY)
        {
            Instantiate(m_WallPrefabs, new Vector3(m_WallSpawnWidth.x, m_WallY, 0), Quaternion.identity);
            Instantiate(m_WallPrefabs, new Vector3(m_WallSpawnWidth.y, m_WallY, 0), Quaternion.identity);
            m_WallY += m_WallSpawnHeight;
        }
    }
}
