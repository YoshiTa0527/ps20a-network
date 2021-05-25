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

    /// <summary>
    /// 操作不能状態かどうか
    /// </summary>
    bool cantMove;


    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!m_view || !m_view.IsMine) return;      // 自分が生成したものだけ処理する


        //ダッシュ状態でないときのみ入力を受け付ける
        if (!cantMove)
        {
            Move();
            if (Input.GetButtonDown("Fire1"))
            {
                Fire();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Dash();
            }
        }
        else
        {
            if (m_rb.velocity.magnitude <= dashEndSpeed)
            {
                isDashing = false;
                cantMove = false;
            }
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
        if (dir.magnitude <= 0.1)
        {
            return;
        }
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
        cantMove = true;
    }

    /// <summary>
    /// 他プレイヤーから押されたときに呼ばれる関数
    /// </summary>
    /// <param name="power">押される方向と力</param>
    public void Pushed(Vector2 power)
    {
        m_view.RPC("SyncPushed", RpcTarget.Others, power);
        m_rb.AddForce(power, ForceMode2D.Impulse);
    }

    /// <summary>
    /// "押された"という情報を同期する。
    /// </summary>
    /// <param name="power">押される方向と力</param>
    [PunRPC]
    void SyncPushed(Vector2 power)
    {
        m_rb.AddForce(power, ForceMode2D.Impulse);
        cantMove = true;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!m_view || !m_view.IsMine) return;      // 自分が生成したものだけ処理する


        Debug.Log("はいりました");
        if (isDashing)
        {
            KojiShipController other = collision.gameObject.GetComponent<KojiShipController>();
            if (other)
            {
                other.Pushed(m_rb.velocity * 5);
                Debug.Log("プッシュ！！");
            }
        }
    }
}
