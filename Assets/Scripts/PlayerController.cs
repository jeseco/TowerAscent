using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_JumpForce;
    [SerializeField] private float m_JumpForceBoost;

    [Header("Jump Force boost delay")]
    [SerializeField] private float m_Delay;

    [Header("UI Reference")]
    [SerializeField] private Canvas m_UI;

    [Header("Sounds")]
    [SerializeField] private AudioClip m_JumpHop;
    [SerializeField] private AudioClip m_JumpLand;
    [SerializeField] private AudioClip m_Walk;

    private ScoreManager m_ScoreManager;

    private float m_TimeElapse; 

    private bool m_JumpBoostActivated;
    private float m_JumpForceMemory;

    private Rigidbody2D m_Rigidbody;

    private Animator m_Animator;

    private SpriteRenderer m_SpriteRenderer;

    private bool m_IsGrounded = true;

    private AudioSource m_AudioSource;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_JumpForceMemory = m_JumpForce;
        m_ScoreManager = m_UI.GetComponent<ScoreManager>(); 
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        Vector3 CalculatedVelocity = m_Rigidbody.velocity;
        CalculatedVelocity.x = HorizontalInput * m_Speed;
        m_Rigidbody.velocity = CalculatedVelocity;

        if (HorizontalInput > 0)
        {
            m_SpriteRenderer.flipX = false;
            m_Animator.SetBool("IsRunning", true);
        }
        else if (HorizontalInput < 0)
        {
            m_SpriteRenderer.flipX = true;
            m_Animator.SetBool("IsRunning", true);
        }
        else
        {
            m_Animator.SetBool("IsRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && m_IsGrounded)
        {
            m_IsGrounded = false;
            m_Animator.SetBool("IsJumping", true);
            m_Rigidbody.AddForce(Vector3.up * m_JumpForce, ForceMode2D.Impulse);
            m_AudioSource.clip = m_JumpHop;
            m_AudioSource.Play();
        }

        if (m_JumpBoostActivated)
        {
            m_JumpForce = m_JumpForceBoost;
            m_TimeElapse += Time.deltaTime;
            if(m_TimeElapse >= m_Delay)
            {
                m_JumpForce = m_JumpForceMemory;
                m_TimeElapse = 0;
                m_JumpBoostActivated = false;
            }

        }

        if (m_Rigidbody.velocity.y > Mathf.Epsilon)
        {
            gameObject.layer = 7;
        }
        else
        {
            gameObject.layer = 6;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GroundBehavior l_GroundBehavior = collision.gameObject.GetComponent<GroundBehavior>();
        PlatformBehavior l_PlatformBehavior = collision.gameObject.GetComponent<PlatformBehavior>();
        if (l_GroundBehavior != null || l_PlatformBehavior != null)
        {
            m_AudioSource.clip = m_JumpLand;
            m_AudioSource.Play();
            m_Animator.SetBool("IsJumping", false);
            m_IsGrounded = true;
        }
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("Score", m_ScoreManager.m_Score);
        if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
        }
        SceneManager.LoadScene("GameOver");
    }

    public void JumpBoost()
    {
        m_JumpBoostActivated = true;
    }
}
