using UnityEngine;
// Photon 用の名前空間を参照する
using Photon.Pun;

/// <summary>
/// シューティングゲームの自機を制御するコンポーネント
/// </summary>
public class KojiShipController : MonoBehaviour
{
    /// <summary>動く速さ</summary>
    [SerializeField] float m_moveSpeed = 10f;
    /// <summary>弾を発射する場所</summary>
    [SerializeField] Transform m_muzzle = null;
    /// <summary>弾のプレハブ名</summary>
    [SerializeField] string m_bulletResourceName = "PrefabResourceName";
    Rigidbody2D m_rb = null;
    PhotonView m_view = null;


    /// <summary>
    /// ダッシュ力
    /// </summary>
    [SerializeField]
    float dashPower;
    /// <summary>
    /// ダッシュ状態を解除するスピード
    /// </summary>
    [SerializeField]
    float dashEndSpeed;
    /// <summary>
    /// ダッシュ中かどうか
    /// </summary>
    bool isDashing;


    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!m_view || !m_view.IsMine) return;      // 自分が生成したものだけ処理する

        if (!isDashing)
        {
            Move();
            if (Input.GetButtonDown("Fire1"))
            {
                Fire();
            }
        }
        else
        {
            if (m_rb.velocity.magnitude <= dashEndSpeed)
            {
                isDashing = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
        }
    }

    /// <summary>
    /// 弾を発射する
    /// </summary>
    void Fire()
    {
        PhotonNetwork.Instantiate(m_bulletResourceName, m_muzzle.position, m_muzzle.rotation);
    }

    /// <summary>
    /// 上下左右にキャラクターを動かす
    /// </summary>
    void Move()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        Vector2 dir = new Vector2(h, v).normalized;
        m_rb.velocity = dir * m_moveSpeed;
    }

    /// <summary>
    /// 入力方向にダッシュする
    /// </summary>
    void Dash()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        Vector2 dir = new Vector2(h, v).normalized;
        m_rb.AddForce(dir * dashPower, ForceMode2D.Impulse);
        isDashing = true;
    }
}
