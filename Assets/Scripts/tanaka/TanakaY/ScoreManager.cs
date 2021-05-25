using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text[] m_texts = null;
    [SerializeField] float m_refleshInterval = 0.1f;
    float m_refleshTimer;
    public GameObject[] m_players;

    private void Update()
    {
        m_refleshTimer += Time.deltaTime;
        if (m_refleshTimer > m_refleshInterval)
        {
            m_refleshTimer = 0;
            RefleshScoreList();
        }
    }
    void RefleshScoreList()
    {


    }
}
