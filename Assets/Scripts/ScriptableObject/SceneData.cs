using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneType
{
    Lobby,
    Game,
}

[CreateAssetMenu(fileName = "SceneData", menuName = "ScriptableObjects/CreateSceneData")]
public class SceneData : ScriptableObject
{
    [SerializeField]
    string lobbyName;
    [SerializeField]
    string gameName;

    /// <summary>
    /// シーンに対応した名前を返す
    /// </summary>
    /// <param name="type">シーン</param>
    /// <returns>シーン名</returns>
    public string GetSceneName(SceneType type)
    {
        switch (type)
        {
            case SceneType.Lobby:
                return lobbyName;
            case SceneType.Game:
                return gameName;
            default:
                return null;
        }
    }
    
}
