using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WallController : MonoBehaviour
{
    /// <summary>動く速さ</summary>
    [SerializeField] float m_moveSpeed = 1f;
    /// <summary>生存期間（秒）</summary>
    [SerializeField] float m_lifeTime = 10f;
    PhotonView m_view = null;
    float m_timer = 0f;

    void Start()
    {
        m_view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!m_view || !m_view.IsMine) return;      // 自分が生成したものだけ処理する

        if (m_view && m_view.IsMine)      // 自分が生成したものだけ処理する
        {
            transform.Translate(m_moveSpeed, 0, 0);
        }

        m_timer += Time.deltaTime;

        if (m_timer > m_lifeTime)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
