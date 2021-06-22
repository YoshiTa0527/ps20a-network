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
    [SerializeField] float m_moveSpeed = 1f;
    /// <summary>動く方向</summary>
    [SerializeField] Vector2 m_moveDirection = Vector2.left;
    /// <summary>生存期間（秒）</summary>
    [SerializeField] float m_lifeTime = 10f;
    Rigidbody2D m_rb = null;
    PhotonView m_view = null;
    float m_timer = 0f;
    bool isDeath = false;
    [SerializeField] private string messege = " ";

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_view = GetComponent<PhotonView>();

        if (m_view && m_view.IsMine)      // 自分が生成したものだけ処理する
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

        if (isDeath)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_view && m_view.IsMine)      // 自分が生成したものだけ処理する
        {
            if (collision.tag == "Bullet" ||
                collision.tag == "Player")
            {
                if (!PhotonNetwork.InRoom || !PhotonNetwork.IsMasterClient)
                {
                    Raise();
                }
                else
                {
                    isDeath = true;
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


    void EnemyDestroy()
    {
        isDeath = true;
    }
    void Raise()
    {
        //Debug.Log("ゲーム終了");
        byte eventCode = 2;
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.MasterClient
        };
        SendOptions sendOptions = new SendOptions();
        PhotonNetwork.RaiseEvent(eventCode, messege, raiseEventOptions, sendOptions);
    }
}
