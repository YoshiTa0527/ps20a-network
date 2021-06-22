using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyShootBullet : MonoBehaviour
{
    /// <summary>弾のPrefab</summary>
    [SerializeField] string m_bulletResourceName = "PrefabResourceName";
    /// <summary>弾を撃つ間隔</summary>
    [SerializeField] float m_interval = 1f;
    float m_timer = 0f;

    void Update()
    {
        // マスタークライアント側でのみ敵を生成する
        if (!PhotonNetwork.InRoom || !PhotonNetwork.IsMasterClient) return;

        m_timer += Time.deltaTime;

        if (m_timer > m_interval)
        {
            m_timer = 0;
            PhotonNetwork.Instantiate(m_bulletResourceName, this.transform.position, Quaternion.identity);
        }
    }
}
