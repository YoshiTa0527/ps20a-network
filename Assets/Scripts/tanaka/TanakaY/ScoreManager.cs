using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text m_text = null;
    

    private void Start()
    {

    }

    public void SetColor(int actorNumber)
    {

        if (actorNumber == 1) m_text.color = Color.blue;
        else m_text.color = Color.blue; ;
    }
}
