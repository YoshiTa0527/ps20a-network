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
    [SerializeField] GameObject m_result = default;
    [SerializeField] Transform m_canvas;
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
    }

    void IOnEventCallback.OnEvent(EventData e)
    {
        if ((int)e.Code == 1)
        {
            GameObject go = Instantiate(m_result, m_canvas);
            go.GetComponent<Result>().SetResult(m_playerAScore, m_playerBScore);
        }
    }

}
