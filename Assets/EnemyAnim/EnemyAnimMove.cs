using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class EnemyAnimMove : MonoBehaviourPunCallbacks, IOnEventCallback
{
    /// <summary>死んだか </summary>
    bool m_isDeath = false;
    PhotonView m_view = null;

    void Start()
    {
        m_view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!m_view || !m_view.IsMine) return;      // 自分が生成したものだけ処理する

        if (m_isDeath)
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
