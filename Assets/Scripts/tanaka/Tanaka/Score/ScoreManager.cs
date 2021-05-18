using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

/// <summary>
/// スコアを加算し、スコアテキストの表示を変える
/// </summary>
public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text m_text = null;
    int m_totalScore = 0;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient) m_text.color = Color.red;
        else m_text.color = Color.blue;
    }

    public void AddScore(int score)
    {
        m_totalScore += score;
        m_text.color = Color.red;
        m_text.color = Color.blue;
    }
}
