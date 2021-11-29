using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public class PlayerColorController : MonoBehaviour
{
    PhotonView m_photonView = default;

    /// <summary>プレイヤー2のSprite </summary>
    [SerializeField] Sprite m_player2Sprite = default;

    private void Start()
    {
        m_photonView = GetComponent<PhotonView>();
        m_photonView.RPC("ChangeColor", RpcTarget.All);
    }

    [PunRPC]
    void ChangeColor()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            GetComponent<SpriteRenderer>().sprite = m_player2Sprite;
        }
        else
        {
            PlayerColorController[] playerColorControllers = FindObjectsOfType<PlayerColorController>();
            for (int i = 0; i < playerColorControllers.Length; i++)
            {
                if (playerColorControllers[i].gameObject != gameObject)
                {
                    playerColorControllers[i].gameObject.GetComponent<SpriteRenderer>().sprite = m_player2Sprite;
                }
            }
        }
    }
}
