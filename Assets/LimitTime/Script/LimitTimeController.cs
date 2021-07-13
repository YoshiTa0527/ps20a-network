using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class LimitTimeController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField] private float limitTime = 10;
    [SerializeField] Text limitTimeText = default;
    private bool isGameStart = false;
    public bool IsGameStart { private set { isGameStart = value; } get { return isGameStart; } }
    private bool isGameEnd = false;
    [SerializeField] private string messege = " ";

    void Update()
    {
        if (!PhotonNetwork.InRoom || !PhotonNetwork.IsMasterClient) return;
        if (isGameStart == true)
        {
            limitTime -= Time.deltaTime;
            if (limitTimeText) limitTimeText.text = limitTime.ToString("F1");
            if (limitTime < 0 && isGameEnd == false)
            {
                Raise();
                isGameEnd = true;
            }
        }
    }

    void IOnEventCallback.OnEvent(EventData e)
    {
        if ((int)e.Code == 0)
        {
            GameStart();
        }
    }

    public void GameStart()
    {
        isGameStart = true;
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
        PhotonNetwork.RaiseEvent(eventCode, messege, raiseEventOptions, sendOptions);
    }
}
