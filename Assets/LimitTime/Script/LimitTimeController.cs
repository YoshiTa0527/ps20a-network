using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;


public class LimitTimeController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField] private float limitTime = 10;
    private bool isGameStart = false;
    [SerializeField] private string messege = " ";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStart == true && PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            limitTime -= Time.deltaTime;
            if (limitTime < 0)
            {
                Raise();
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
        byte eventCode = 1;
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All
        };
        SendOptions sendOptions = new SendOptions();
        PhotonNetwork.RaiseEvent(eventCode, messege, raiseEventOptions, sendOptions);
    }
}
