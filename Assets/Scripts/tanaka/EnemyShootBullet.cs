using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyShootBullet : MonoBehaviour
{
    /// <summary>弾のPrefab</summary>
    [SerializeField] protected string m_bulletResourceName = "PrefabResourceName";
    /// <summary>弾を撃つ間隔</summary>
    [SerializeField] protected float m_interval = 1f;
    /// <summary>一度に撃つ弾の数</summary>
    [SerializeField] protected int m_countAtOnce;
    /// <summary>一度に撃つときの間隔</summary>
    [SerializeField] protected float m_intervalAtOnce;
    /// <summary> 銃身(これのup方向に弾を飛ばす) </summary>
    [SerializeField] protected Transform m_barrel;

    float m_timer = 0f;
    

    void Update()
    {
        // マスタークライアント側でのみ敵を生成する
        if (!PhotonNetwork.InRoom || !PhotonNetwork.IsMasterClient) return;

        m_timer += Time.deltaTime;

        if (m_timer > m_interval)
        {
            m_timer = 0;
            StartCoroutine(ShootBullets());
        }
    }

    private IEnumerator ShootBullets()
    {
        for (int i = 0; i < m_countAtOnce; i++)
        {
            PhotonNetwork.Instantiate(m_bulletResourceName, this.transform.position, m_barrel ? m_barrel.rotation : Quaternion.identity);
            yield return new WaitForSeconds(m_intervalAtOnce);
        }
    }
}
