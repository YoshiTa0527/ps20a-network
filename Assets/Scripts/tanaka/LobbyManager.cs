﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    private string gameSceneName;
    [SerializeField]
    private InputField playerNameInput;
    [SerializeField]
    private InputField roomNameInput;

    /// <summary>
    /// プレイヤー名、ルーム名を設定してゲームシーンに移動する
    /// </summary>
    public void GO()
    {
        NetworkGameManager.m_playerName = playerNameInput.text;
        NetworkGameManager.m_roomName = roomNameInput.text;

        SceneLoader loder = new SceneLoader();

        loder.LoadScene(gameSceneName);
    }
}