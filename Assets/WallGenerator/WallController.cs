using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WallController : MonoBehaviour
{
    /// <summary>x軸に動く速さ</summary>
    [SerializeField] float m_moveSpeedX = 1f;

    /// <summary>y軸に動く速さ</summary>
    [SerializeField] float m_moveSpeedY = 1f;

    /// <summary>生存期間（秒）</summary>
    [SerializeField] float m_lifeTime = 10f;

    PhotonView m_view = null;

    /// <summary>
    /// タイマー
    /// </summary>
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
            transform.Translate(m_moveSpeedX, m_moveSpeedY, 0);
        }

        m_timer += Time.deltaTime;

        if (m_timer > m_lifeTime)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
