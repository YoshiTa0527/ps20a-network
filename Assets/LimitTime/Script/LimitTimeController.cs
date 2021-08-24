using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

/// <summary>
/// 制限時間を管理する
/// </summary>
public class LimitTimeController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    /// <summary>
    /// 制限時間
    /// </summary>
    [SerializeField] private float m_limitTime = 10;
    /// <summary>
    /// タイマーを表示するテキスト
    /// </summary>
    [SerializeField] Text m_limitTimeText = default;
    /// <summary>
    /// ゲームが開始したか
    /// </summary>
    [SerializeField] private bool m_isGameStart = false;
    /// <summary>
    /// スタートしたか
    /// </summary>
    public bool IsGameStart { private set { m_isGameStart = value; } get { return m_isGameStart; } }
    /// <summary>
    /// 終了したか
    /// </summary>
    private bool m_isGameEnd = false;

    void Update()
    {
        if (!PhotonNetwork.InRoom) return;
        if (m_isGameStart == true)
        {
            m_limitTime -= Time.deltaTime;

            //秒数
            int s = (int)Mathf.Floor(m_limitTime);
            string sText = string.Format("{0:D2}", s);
            //ミリ秒
            int m = (int)((m_limitTime - s) * 60);
            string mText = string.Format("{0:D2}", m);

            if (m_limitTime <= 0 && m_isGameEnd == false)
            {
                Raise();
                m_isGameEnd = true;
                m_isGameStart = false;
                sText = "00";
                mText = "00";
            }

            m_limitTimeText.text = sText + ":" + mText;
        }
    }

    void IOnEventCallback.OnEvent(EventData e)
    {
        if ((int)e.Code == 0)
        {
            GameStart();
        }
    }

    /// <summary>
    /// ゲームを開始する
    /// </summary>
    public void GameStart()
    {
        m_isGameStart = true;
        Debug.Log("ゲーム開始");
    }

    void Raise()
    {
        Debug.Log("ゲーム終了");
        byte eventCode = 1;
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All
        };
        SendOptions sendOptions = new SendOptions();
        string messege = null;
        PhotonNetwork.RaiseEvent(eventCode, messege, raiseEventOptions, sendOptions);
    }
}
