using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class WallGenerator : MonoBehaviourPunCallbacks, IOnEventCallback
{
    /// <summary>
    /// 生成する壁のオブジェクト
    /// </summary>
    [SerializeField] string[] m_walls;

    /// <summary>
    /// 生成される壁のインデックス
    /// </summary>
    int m_indexNum = 0;

    /// <summary>
    /// 生成する間隔
    /// </summary>
    [SerializeField] float m_interval = 2f;

    /// <summary>
    /// タイマー
    /// </summary>
    float m_timer = 0f;

    /// <summary>
    /// 生成フラグ
    /// </summary>
    bool m_generatorFlag = false;

    void Update()
    {
        if (!PhotonNetwork.InRoom || !PhotonNetwork.IsMasterClient) return;

        m_timer += Time.deltaTime;

        if (!m_generatorFlag)
        {
            if (m_timer > m_interval)
            {
                m_timer = 0;
                if (m_indexNum >= m_walls.Length)
                {
                    m_indexNum = 0;
                }

                PhotonNetwork.Instantiate(m_walls[m_indexNum], this.transform.position, Quaternion.identity);

                m_indexNum++;
            }
        }
    }

    void IOnEventCallback.OnEvent(EventData a)
    {
        if ((int)a.Code == 1)
        {
            Generato();
        }
    }

    void Generato()
    {
        m_generatorFlag = true;
    }
}
