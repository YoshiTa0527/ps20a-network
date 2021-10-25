using UnityEngine;
// Photon 用の名前空間を参照する
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;

/// <summary>
/// 敵を制御するコンポーネント
/// 設定した方向に進み、設定した時間が経ったら破棄される
/// </summary>
public class EnemyController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    /// <summary>動く速さ</summary>
    [SerializeField] protected float m_moveSpeed = 1f;
    /// <summary>動く方向</summary>
    [SerializeField] protected Vector2 m_moveDirection = Vector2.left;
    /// <summary>生存期間（秒）</summary>
    [SerializeField] protected float m_lifeTime = 10f;
    /// <summary>経過時間 </summary>
    protected float m_timer = 0f;
    /// <summary>死んだか </summary>
    bool m_isDeath = false;

    protected Rigidbody2D m_rb = null;
    PhotonView m_view = null;

    protected virtual void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_view = GetComponent<PhotonView>();
    }

    protected virtual void Update()
    {
        if (!m_view || !m_view.IsMine) return;      // 自分が生成したものだけ処理する

        Move();
        m_timer += Time.deltaTime;

        if (m_timer > m_lifeTime || m_isDeath)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 移動する
    /// </summary>
    protected virtual void Move()
    {
        m_rb.velocity = m_moveDirection.normalized * m_moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_view && m_view.IsMine)      // 自分が生成したものだけ処理する
        {
            if (collision.CompareTag("Bullet") ||
                collision.CompareTag("Player"))
            {
                //プレイヤーが無敵状態だったら無視する
                SpaceShipController spaceShipController = collision.GetComponent<SpaceShipController>();
                if (spaceShipController != null && spaceShipController.IsInvincible)
                    return;

                if (!m_isDeath)
                {
                    if (!PhotonNetwork.InRoom || !PhotonNetwork.IsMasterClient)
                    {
                        Raise();
                        Debug.Log("弾が当たった");
                        return;
                    }
                    m_isDeath = true;
                    Debug.Log("弾が当たった");
                }
            }
        }
    }

    void IOnEventCallback.OnEvent(EventData e)
    {
        if ((int)e.Code == 2)
        {
            EnemyDestroy();
        }
    }

    /// <summary>
    /// 敵を消す
    /// </summary>
    void EnemyDestroy()
    {
        m_isDeath = true;
    }

    /// <summary>
    /// イベントを起こす
    /// </summary>
    void Raise()
    {
        byte eventCode = 2;
        string messege = " ";
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.MasterClient
        };
        SendOptions sendOptions = new SendOptions();
        PhotonNetwork.RaiseEvent(eventCode, messege, raiseEventOptions, sendOptions);
    }
}
