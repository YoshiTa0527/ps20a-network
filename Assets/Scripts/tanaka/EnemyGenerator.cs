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
    float m_intervalTimer = 0f;
    /// <summary>敵の生成を停止する時間（秒）</summary>
    [SerializeField] float m_restTime = 3f;
    float m_restTimeTimer = 0;
    /// <summary>敵を生成し続ける時間（秒）</summary>
    [SerializeField] float m_generationTime = 10f;
    float m_generationTimeTimer = 0;
    /// <summary>タイムアップになったか</summary>
    bool m_isTimeUp = false;

    void Update()
    {
        // マスタークライアント側でのみ敵を生成する
        //最大人数までプレイヤーが入室したら敵を生成する
        if (!PhotonNetwork.IsMasterClient ||
            PhotonNetwork.CurrentRoom.Players.Count < PhotonNetwork.CurrentRoom.MaxPlayers) 
            return;

        m_intervalTimer += Time.deltaTime;
        m_generationTimeTimer += Time.deltaTime;

        if (m_intervalTimer > m_interval && m_isTimeUp == false)
        {
            //設定した生成し続ける時間だけ敵を生成する
            if (m_generationTimeTimer <= m_generationTime)
            {
                m_intervalTimer = 0;
                PhotonNetwork.Instantiate(m_enemyResourceName, this.transform.position, Quaternion.identity);
            }
            else
            {
                m_restTimeTimer += Time.deltaTime;

                //設定した停止時間まで経過したら生成し始める
                if (m_restTimeTimer > m_restTime)
                {
                    m_generationTimeTimer = 0;
                    m_restTimeTimer = 0;
                }
            }
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
        m_isTimeUp = true;
    }
}
