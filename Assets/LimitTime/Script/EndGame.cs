using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public class EndGame : MonoBehaviourPunCallbacks, IOnEventCallback
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IOnEventCallback.OnEvent(EventData e)
    {
        if ((int)e.Code == 1)
        {
            GameSet();
        }
    }

    private void GameSet()
    {
        Debug.Log("TimeOver");

    }
}
