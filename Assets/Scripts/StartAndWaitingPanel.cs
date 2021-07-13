using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAndWaitingPanel : MonoBehaviour
{
    [SerializeField] GameObject m_startPanel;
    [SerializeField] float m_startPanelActiveTime;
    float m_timer;
    [SerializeField] GameObject m_waitingPanel;
    [SerializeField] LimitTimeController m_limitTimeController;

    void Update()
    {
        if (m_limitTimeController.IsGameStart)
        {
            m_startPanel.SetActive(true);
            m_waitingPanel.SetActive(false);
            m_timer += Time.deltaTime;
            if (m_timer >= m_startPanelActiveTime)
            {
                m_startPanel.SetActive(false);
            }
        }
    }
}
