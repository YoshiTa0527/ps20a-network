using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveUIButton : MonoBehaviour
{
    [SerializeField] GameObject m_UI = null;
    public void OnClick()
    {
        m_UI.SetActive(!m_UI.activeSelf);
    }
}
