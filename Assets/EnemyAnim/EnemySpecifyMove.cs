using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class EnemySpecifyMove : MonoBehaviourPunCallbacks, IOnEventCallback
{
    /// <summary>動く速さ</summary>
    [SerializeField] float m_moveSpeed = 1f;
    /// <summary>動く方向</summary>
    [SerializeField] Vector2 m_moveDirection = Vector2.left;
    /// <summary>次に動く方向</summary>
    [SerializeField] Vector2 m_nextMoveDirection = Vector2.up;
    /// <summary>次の方向に動くまでの時間</summary>
    [SerializeField] float m_nextMoveTime = 3f;
    /// <summary>生存期間（秒）</summary>
    [SerializeField] float m_lifeTime = 10f;
    /// <summary>経過時間 </summary>
    float m_timer = 0f;
    /// <summary>死んだか </summary>
    bool m_isDeath = false;
    /// <summary>次の移動になっているか</summary>
    bool m_nextMove = false;

    protected Rigidbody2D m_rb = null;
    PhotonView m_view = null;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!m_view || !m_view.IsMine) return;      // 自分が生成したものだけ処理する
        
        m_timer += Time.deltaTime;

        if (m_timer > m_nextMoveTime)
        {
            m_nextMove = true;
        }

        if (!m_nextMove)
        {
            Move(m_moveDirection);
        }
        else
        {
            Move(m_nextMoveDirection);
        }

        if (m_timer > m_lifeTime || m_isDeath)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_view && m_view.IsMine)      // 自分が生成したものだけ処理する
        {
            if (collision.CompareTag("Bullet") ||
                collision.CompareTag("Player"))
            {
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

    void Move(Vector2 direction)
    {
        m_rb.velocity = direction.normalized * m_moveSpeed;
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
