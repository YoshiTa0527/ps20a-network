using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))]
public class WallController : MonoBehaviour
{
    PhotonView m_view = null;
    Rigidbody2D m_rb = null;
    /// <summary>移動スピード</summary>
    [SerializeField] float m_moveSpeed = 5f;
    /// <summary>動く方向</summary>
    [SerializeField] Vector2 m_moveDirection = Vector2.left;
    /// <summary>生存期間（秒）</summary>
    [SerializeField] float m_lifeTime = 10f;
    /// <summary>タイマー</summary>
    float m_timer = 0f;

    void Start()
    {
        m_view = GetComponent<PhotonView>();
        m_rb = GetComponent<Rigidbody2D>();

        if (m_view && m_view.IsMine)
        {
            m_rb.velocity = m_moveDirection.normalized * m_moveSpeed;
        }
    }

    void Update()
    {
        if (!m_view || !m_view.IsMine) return;      // 自分が生成したものだけ処理する

        m_timer += Time.deltaTime;

        if (m_timer > m_lifeTime)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
