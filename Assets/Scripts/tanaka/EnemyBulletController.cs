using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyBulletController : MonoBehaviour
{
    /// <summary>弾が飛ぶ速さ</summary>
    [SerializeField] float m_speed;
    /// <summary>弾が飛ぶ方向</summary>
    [SerializeField] Vector2 m_direction = Vector2.up;
    /// <summary>弾の生存期間（秒）</summary>
    [SerializeField] float m_lifeTime = 1f;
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
            m_rb.velocity = m_speed * m_direction;
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

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //敵にぶつかったら弾を消す
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        if (m_view && m_view.IsMine)    // 自分が生成したものだけ処理する
    //        {
    //            PhotonNetwork.Destroy(this.gameObject);
    //        }
    //    }
    //}

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
