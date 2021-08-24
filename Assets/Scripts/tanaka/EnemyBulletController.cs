using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyBulletController : MonoBehaviour
{
    /// <summary>弾が飛ぶ速さ</summary>
    [SerializeField] float m_speed;
    /// <summary>弾の生存期間（秒）</summary>
    [SerializeField] float m_lifeTime = 1f;
    [SerializeField] string bulletmove;
    PhotonView m_view = null;
    Rigidbody2D m_rb = null;
    float m_timer;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_view = GetComponent<PhotonView>();
        if (m_view && m_view.IsMine)    // 自分が生成したものだけ処理する
        {
            // 弾に初速を与える
            m_rb.velocity = m_speed * transform.up;
        }
    }

    void Update()
    {
        if (!m_view || !m_view.IsMine) return;  // 自分が生成したものだけ処理する

        // 一定時間で弾を消す
        m_timer += Time.deltaTime;

        if (m_timer > m_lifeTime)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // プレイヤーにぶつかったら弾を消す
        if (collision.gameObject.CompareTag("Player"))
        {
            if (m_view && m_view.IsMine)    // 自分が生成したものだけ処理する
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }
}
