using UnityEngine;
// Photon 用の名前空間を参照する
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;


/// <summary>
/// 敵を生成するコンポーネント
/// </summary>
public class EnemyGenerator : MonoBehaviourPunCallbacks, IOnEventCallback
{
    /// <summary>敵として生成するプレハブの名前</summary>
    [SerializeField] string m_enemyResourceName = "PrefabResourceName";
    /// <summary>敵を生成する間隔（秒）</summary>
    [SerializeField] float m_interval = 1f;
    float m_timer = 0f;
    bool isTimeUp = false;

    void Update()
    {
        // マスタークライアント側でのみ敵を生成する
        if (!PhotonNetwork.InRoom || !PhotonNetwork.IsMasterClient) return;
        
        m_timer += Time.deltaTime;

        if (m_timer > m_interval && isTimeUp == false)
        {
            m_timer = 0;
            PhotonNetwork.Instantiate(m_enemyResourceName, this.transform.position, Quaternion.identity);
        }
        else
        {
            return;
        }
        
    }

    void IOnEventCallback.OnEvent(EventData e)
    {
        //Debug.Log($"{this.name} が {e.Code.ToString()} を受け取った");
        if ((int)e.Code == 1)
        {
            EnemyInstanceStop();
        }
    }

    void EnemyInstanceStop()
    {
        isTimeUp = true;
    }
}
