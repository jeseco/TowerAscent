using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text m_ScoreText;
    [SerializeField] private GameObject m_Player;

    public int m_Score; // Accessed in PlayerController

    // Update is called once per frame
    void Update()
    {
        m_Score = (int) m_Player.transform.position.y; 
        m_ScoreText.text = m_Score.ToString() + " POINTS";
    }

}
