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
    [SerializeField] Sprite m_mySprite = default;

    private void Start()
    {
        m_photonView = GetComponent<PhotonView>();

        if (m_photonView.IsMine)
            GetComponent<SpriteRenderer>().sprite = m_mySprite;
    }
}
