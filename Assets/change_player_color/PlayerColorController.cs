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
        if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            m_photonView = GetComponent<PhotonView>();
            m_photonView.RPC("ChangeColor", RpcTarget.All, m_photonView.ViewID);
        }
    }

    [PunRPC]
    void ChangeColor(int id)
    {
        if (m_photonView.ViewID == id)
            GetComponent<SpriteRenderer>().sprite = m_player2Sprite;
    }
}
