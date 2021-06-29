using UnityEngine;
// Photon 用の名前空間を参照する
using Photon.Pun;

/// <summary>
/// 敵を制御するコンポーネント
/// 設定した方向に進み、設定した時間が経ったら破棄される
/// </summary>
public class EnemyController1 : MonoBehaviour
{
    /// <summary>動く速さ</summary>
    [SerializeField] float m_moveSpeed = 1f;
    /// <summary>動く方向</summary>
    [SerializeField] Vector2 m_moveDirection = Vector2.left;
    /// <summary>生存期間（秒）</summary>
    [SerializeField] float m_lifeTime = 10f;
    Rigidbody2D m_rb = null;
    PhotonView m_view = null;
    float m_timer = 0f;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!m_view || !m_view.IsMine) return;

        Move();

        m_timer += Time.deltaTime;

        if (m_timer > m_lifeTime)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    void Move()
    {
        m_rb.velocity = m_moveDirection.normalized * m_moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_view && m_view.IsMine)
        {
            if (collision.tag == "Player" ||
                collision.tag == "Bullet")
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }
}
