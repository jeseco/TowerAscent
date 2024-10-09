using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text m_ScoreText;
    [SerializeField] TMP_Text m_HighScoreText;
    // Start is called before the first frame update
    void Start()
    {
        m_ScoreText.text = PlayerPrefs.GetInt("Score").ToString();
        m_HighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

}
