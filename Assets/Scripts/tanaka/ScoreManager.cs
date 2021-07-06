using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Realtime;

/// <summary>
/// クライアントそれぞれがスコアマネージャーを持つ
/// スコアマネージャーはプレイヤーが入室したときにスコアテキストをそれぞれに与え、同期する
/// </summary>
public class ScoreManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public static ScoreManager current;
    [SerializeField] Text m_textA = null;
    [SerializeField] Text m_textB = null;
    [SerializeField] float m_refleshInterval = 0.1f;
    [SerializeField] Result m_result = default;
    int m_playerAScore;
    int m_playerBScore;
    float m_refleshTimer;
    PhotonView m_view;

    private void Awake()
    {
        current = this;
        m_view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!m_view.IsMine) return;
        m_refleshTimer += Time.deltaTime;
        if (m_refleshTimer > m_refleshInterval)
        {
            m_refleshTimer = 0;
            object[] parameters = new object[] { m_playerAScore, m_playerBScore };
            m_view.RPC("RefleshScoreList", RpcTarget.All, parameters);
        }
    }

    [PunRPC]
    void RefleshScoreList(int aScore, int bScore)
    {
        Debug.Log("isCalled");
        m_playerAScore = aScore;
        m_playerBScore = bScore;
        m_textA.text = "PlayerA:" + m_playerAScore.ToString();
        m_textB.text = "PlayerB:" + m_playerBScore.ToString();
    }

    public void AddScore(bool isOwner, int score)
    {
        if (isOwner)
        {
            m_playerAScore += score;
        }
        else
        {
            m_playerBScore += score;
        }
        Debug.Log($"PlayerA Score:{m_playerAScore}PlayerB Score:{m_playerBScore}");
    }

    void IOnEventCallback.OnEvent(EventData e)
    {
        if ((int)e.Code == 1)
        {
            Debug.Log("ScoreManager:OnEvent1");
            //m_result.gameObject.SetActive(true);
            m_result.SetResult(m_playerAScore, m_playerBScore);
            Debug.Log("ScoreManager:OnEvent2");
        }
    }

    public void TestFunc(int a)
    {
        m_result.SetResult(a, a + 10);
    }
}
