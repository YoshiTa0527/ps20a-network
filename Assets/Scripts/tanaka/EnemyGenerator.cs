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
    [SerializeField]
    EnemySpawnData spwawnData;
    /// <summary>タイムアップになったか</summary>
    bool m_isTimeUp = false;
    /// <summary> ゲーム中かどうか　/// </summary>
    bool m_isGameStarted = false;

    float intervalTime;
    float timer;

    void Update()
    {
        // マスタークライアント側でのみ敵を生成する
        //最大人数までプレイヤーが入室したら敵を生成する
        if (!PhotonNetwork.IsMasterClient ||
                !m_isGameStarted) 
            return;

        timer += Time.deltaTime;

        if (timer > intervalTime && !m_isTimeUp)
        {
            //タイマーリセット
            timer = 0;
            //敵選定
            EnemySpwanPrefab prefab = spwawnData.Data[Random.Range(0, spwawnData.Data.Count)];
            //プレハブ生成
            Transform enemyTransform = Instantiate(prefab).transform;
            enemyTransform.position = this.transform.position;
            //インターバル設定
            intervalTime = prefab.Interval;
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
        if ((int)e.Code == 0)
        {
            m_isGameStarted = true;
        }
    }

    void EnemyInstanceStop()
    {
        m_isTimeUp = true;
    }
}
