using UnityEngine;
// Photon 用の名前空間を参照する
using Photon.Pun;

/// <summary>
/// シューティングゲームの自機を制御するコンポーネント
/// </summary>
public class SpaceShipController : MonoBehaviour
{
    /// <summary>動く速さ</summary>
    [SerializeField] float m_moveSpeed = 10f;
    /// <summary>弾を発射する場所</summary>
    [SerializeField] Transform m_muzzle = null;
    /// <summary>弾のプレハブ名</summary>
    [SerializeField] string m_bulletResourceName = "PrefabResourceName";
    Rigidbody2D m_rb = null;
    PhotonView m_view = null;
    SpriteRenderer m_spriteRenderer = null;

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
    /// 押す力
    /// </summary>
    [SerializeField]
    float pushPower;
    /// <summary>
    /// ダッシュ中かどうか
    /// </summary>
    bool isDashing;

    /// <summary>
    /// 操作不能状態かどうか
    /// </summary>
    bool cantMove;

    #region 無敵時の処理に関するメンバ変数群
    /// <summary>
    /// 無敵時の機体の色
    /// </summary>
    [SerializeField] private Color m_invincibleColor = new Color(255, 255, 255, 100);
    /// <summary>
    /// 通常時の機体の色
    /// </summary>
    private Color m_defaltColor;
    /// <summary>
    /// 無敵時間
    /// </summary>
    [SerializeField] private int m_invincibleTime = 3;
    /// <summary>
    /// 経過時間
    /// </summary>
    private float m_elapsedTime = 0;
    /// <summary>
    /// 無敵状態か [true: 無敵 / false:通常]
    /// </summary>
    private bool m_isInvincible = false;
    /// <summary>
    /// 無敵状態か [true: 無敵 / false:通常]
    /// </summary>
    public bool IsInvincible { get { return m_isInvincible; } }
    /// <summary>
    /// 移動以外の行動が不能な状態か
    /// [true: 不能状態である / false: 不能状態でない]
    /// </summary>
    private bool m_cantAction = false;
    #endregion

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_view = GetComponent<PhotonView>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_defaltColor = m_spriteRenderer.material.color;
    }

    void Update()
    {
        if (!m_view || !m_view.IsMine) return;      // 自分が生成したものだけ処理する

        //無敵状態だったら
        if (m_isInvincible)
        {
            OffInvincibleByTimeCourse();
        }

        //ダッシュ状態でないときのみ入力を受け付ける
        if (!cantMove)
        {
            Move();
            if (!m_cantAction)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Fire();
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Dash();
                }
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
        //サウンド
        SoundManager.Instance?.PlaySE(0);
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
        //サウンド
        SoundManager.Instance?.PlaySE(1);
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

    /// <summary>
    /// 時間経過で無敵状態を解除する
    /// </summary>
    private void OffInvincibleByTimeCourse()
    {
        //時間をカウントする
        m_elapsedTime += Time.deltaTime;

        //設定された無敵状態の時間経過したら無敵状態を抜ける
        if (m_elapsedTime >= m_invincibleTime)
        {
            m_isInvincible = false;
            m_cantAction = false;
            m_elapsedTime = 0;

            //機体を通常時の色に戻す
            m_spriteRenderer.material.color = m_defaltColor;
            Debug.Log("無敵状態から抜けました");

            m_view.RPC("SyncInvincibleOff", RpcTarget.Others);
        }
    }

    /// <summary>
    /// 無敵状態が解除されたことを同期する
    /// </summary>
    [PunRPC]
    void SyncInvincibleOff()
    {
        m_isInvincible = false;
        m_spriteRenderer.material.color = m_defaltColor;
    }

    /// <summary>
    /// 無敵状態にする
    /// </summary>
    private void OnInvivcible()
    {
        m_cantAction = true;
        m_isInvincible = true;

        //機体を無敵時の色にする
        m_spriteRenderer.material.color = m_invincibleColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!m_view || !m_view.IsMine) return;      // 自分が生成したものだけ処理する

        if (isDashing)
        {
            SpaceShipController other = collision.gameObject.GetComponent<SpaceShipController>();
            if (other)
            {
                other.Pushed(m_rb.velocity * pushPower);
                Vector2 reflect = collision.contacts[0].normal;
                reflect *= -2 * Vector2.Dot(reflect, m_rb.velocity);
                m_rb.velocity += reflect;
                Debug.Log("プッシュ！！");
                //サウンド
                SoundManager.Instance?.PlaySE(3);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_isInvincible) return;

        //敵、敵の弾にぶつかったら無敵になる
        if (collision.gameObject.CompareTag("EnemyBullet") ||
            collision.gameObject.CompareTag("Enemy"))
        {
            OnInvivcible();
            //サウンド
            SoundManager.Instance?.PlaySE(3);
        }
    }
}