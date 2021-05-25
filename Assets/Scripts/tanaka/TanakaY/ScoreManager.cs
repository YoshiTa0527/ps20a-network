using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager current;
    [SerializeField] Text m_textA = null;
    [SerializeField] Text m_textB = null;
    [SerializeField] float m_refleshInterval = 0.1f;
    float m_refleshTimer;
    PhotonView m_view;

    private void Awake()
    {
        current = this;
        m_view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        m_refleshTimer += Time.deltaTime;
        if (m_refleshTimer > m_refleshInterval)
        {
            m_refleshTimer = 0;
            //m_view.RPC("RefleshScoreList", RpcTarget.All);
        }
    }

    [PunRPC]
    void RefleshScoreList()
    {


    }
    /// <summary>
    /// 二つのテキストを振り分ける
    /// </summary>
    public void SetScore(bool isOwner, int score)
    {
        if (isOwner)
        {
            m_textA.text = "PlayerA:" + score.ToString();
        }
        else
        {
            m_textB.text = "PlayerB:" + score.ToString();
        }
    }
}
