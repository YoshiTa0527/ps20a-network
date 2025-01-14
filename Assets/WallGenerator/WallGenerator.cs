﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class WallGenerator : MonoBehaviourPunCallbacks, IOnEventCallback
{
    /// <summary>インターバルをランダム化するか</summary>
    [SerializeField] bool m_isRandomInterval = false;
    /// <summary>生成物のランダム化</summary>
    [SerializeField] bool m_isRandomGenerator = false;
    /// <summary>生成する壁のオブジェクト</summary>
    [SerializeField] string[] m_walls;
    /// <summary>生成される壁のインデックス</summary>
    int m_indexNum = 0;
    /// <summary>生成する間隔</summary>
    [SerializeField] float m_interval = 2f;
    /// <summary>タイマー</summary>
    float m_timer = 0f;
    /// <summary>生成フラグ</summary>
    bool m_generatorFlag = false;
    /// <summary>ランダムの最小値</summary>
    [SerializeField] float m_intervalMinRange = 2f;
    /// <summary>ランダムの最大値</summary>
    [SerializeField] float m_intervalMaxRange = 5f;

    void Start()
    {
        m_timer = m_interval;
    }

    void Update()
    {
        if (!PhotonNetwork.InRoom || !PhotonNetwork.IsMasterClient) return;

        m_timer += Time.deltaTime;

        if (!m_generatorFlag)
        {
            if (m_timer > m_interval)
            {
                m_timer = 0;
                
                PhotonNetwork.Instantiate(m_walls[m_indexNum], this.transform.position, Quaternion.identity);

                if (m_isRandomGenerator)
                {
                    m_indexNum = Random.Range(0, m_walls.Length);
                }
                else
                {
                    m_indexNum++;

                    if (m_indexNum >= m_walls.Length)
                    {
                        m_indexNum = 0;
                    }
                }
                
                if (m_isRandomInterval)
                {
                    m_interval = Random.Range(m_intervalMinRange, m_intervalMaxRange);
                }
            }
        }
    }

    void IOnEventCallback.OnEvent(EventData a)
    {
        if ((int)a.Code == 1)
        {
            Generato();
        }
    }

    void Generato()
    {
        m_generatorFlag = true;
    }
}
