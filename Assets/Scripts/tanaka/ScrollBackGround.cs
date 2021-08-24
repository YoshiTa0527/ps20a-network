using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackGround : MonoBehaviour
{
    [SerializeField] GameObject m_startPosition;
    [SerializeField] GameObject m_endPosition;
    [SerializeField] GameObject m_otherEndPosition;
    Vector3 m_startPos;

    [SerializeField] float m_spd = 5f;
    private void Start()
    {
        m_startPos = m_startPosition.transform.position;
    }
    private void Update()
    {
        if (Vector3.Distance(m_startPosition.transform.position, m_endPosition.transform.position) == 0)
        {
            this.transform.position = m_otherEndPosition.transform.position;
        }

        this.transform.position += Vector3.left / m_spd;

    }
}
