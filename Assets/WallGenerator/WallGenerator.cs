using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WallGenerator : MonoBehaviour
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

    void Update()
    {
        if (!PhotonNetwork.InRoom || !PhotonNetwork.IsMasterClient) return;

        m_timer += Time.deltaTime;

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
