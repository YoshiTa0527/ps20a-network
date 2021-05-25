using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

/// <summary>
/// プレイヤーのスコアを記録する
/// </summary>
public class PlayerScore : MonoBehaviour
{
    int m_totalScore;
    [SerializeField] Text m_scoreText = null;
    float m_syncTimer;
    float m_syncInterval = 0.1f;
    PhotonView m_view;

    private void Start()
    {
        m_totalScore = 0;

        m_view = GetComponent<PhotonView>();
        if (m_view.IsMine)
        {
            m_scoreText.color = Color.blue;
            m_scoreText.rectTransform.position = new Vector2(50, 50);
        }
        else
        {
            m_scoreText.color = Color.red;
            m_scoreText.rectTransform.position = new Vector2(150, 150);
        }

    }

    private void Update()
    {
        if (!m_view.IsMine) return;
        m_syncTimer += Time.deltaTime;
        if (m_syncTimer > m_syncInterval)
        {
            Debug.Log("更新");
            m_syncTimer = 0;
            m_view.RPC("SyncScore", RpcTarget.All);
        }
    }

    /// <summary>
    /// 加点する。弾が呼び出す
    /// </summary>
    /// <param name="isMaster"></param>
    /// <param name="score"></param>
    public void AddScore(int id, int score)
    {
        m_totalScore += score;
    }

    [PunRPC]
    void SyncScore()
    {
        Debug.Log("Called");
       
    }

    void RefreshScoreText()
    {

    }

}
