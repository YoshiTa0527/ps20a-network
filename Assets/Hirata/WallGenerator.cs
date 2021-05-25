using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WallGenerator : MonoBehaviour
{
    /// <summary>
    /// 生成する壁のオブジェクト
    /// </summary>
    [SerializeField] string m_wall = "wall";

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
            PhotonNetwork.Instantiate(m_wall, this.transform.position, Quaternion.identity);
        }
    }
}
