using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HitToPlayer : MonoBehaviour
{
    [SerializeField] Color m_color;

    Rigidbody2D m_rb = null;
    PhotonView m_view = null;

    [SerializeField] private int m_invincibleTime = 3;
    private float m_time = 0;

    private bool m_isInvincibleState = false;
    /// <summary>
    /// 無敵状態か
    /// </summary>
    public bool IsInvincibleState { get { return m_isInvincibleState; } private set { } }

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        MakeInvincibleState();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!m_view || !m_view.IsMine) return;

        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            m_isInvincibleState = true;
            Debug.Log("無敵状態に入りました");
        }
    }

    void MakeInvincibleState()
    {
        if (m_isInvincibleState)
        {
            m_time += Time.deltaTime;
            if (m_time >= m_invincibleTime)
            {
                m_isInvincibleState = false;
                Debug.Log("無敵状態から抜けました");
                m_time = 0;
            }
        }
    }
}
