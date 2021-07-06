using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] Text m_winerText = default;
    [SerializeField] Text m_loserText = default;

    public void SetResult(int playerAScore, int playerBScore)
    {
        Debug.Log("Result:Call");
        this.gameObject.SetActive(true);
        if (playerAScore > playerBScore)
        {
            m_winerText.text = $"Winer!!:PlayerA\nScore:{playerAScore}";
            m_loserText.text = $"Loser:PlayerB\nScore:{playerBScore}";
        }
        else if (playerAScore < playerBScore)
        {
            m_winerText.text = $"Winer!!:PlayerB\nScore:{playerBScore}";
            m_loserText.text = $"Loser:PlayerA\nScore:{playerAScore}";
        }
    }
}
