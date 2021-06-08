using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyScore : MonoBehaviour
{
    [SerializeField] int m_score = 5;
    PhotonView m_view;

    private void Start()
    {
        m_view = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Bullet")
        {
            Debug.Log("hitOnTrigger");
            PhotonView bulletView = collision.gameObject.GetComponent<PhotonView>();
            if (bulletView)
                ScoreManager.current.AddScore(bulletView.Owner.IsMasterClient, m_score);
        }

    }

}
