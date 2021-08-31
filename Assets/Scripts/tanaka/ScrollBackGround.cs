using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackGround : MonoBehaviour
{
    /// <summary>初期位置</summary>
    float m_startPos;
    [SerializeField] SpriteRenderer m_backGround;
    SpriteRenderer m_backGroundClone;
    /// <summary>スクロールするスピード</summary>
    [SerializeField] float m_spd = 5f;
    private void Start()
    {
        m_startPos = m_backGround.transform.position.x;
        m_backGroundClone = Instantiate(m_backGround);
        m_backGroundClone.transform.Translate(m_backGround.bounds.size.x, m_backGround.transform.position.y, 0f);
    }
    private void Update()
    {
        this.transform.Translate(Vector2.left * m_spd * Time.deltaTime);
        m_backGroundClone.transform.transform.Translate(Vector2.left * m_spd * Time.deltaTime);

        if (m_backGround.transform.position.x < m_startPos - m_backGround.bounds.size.x)
        {
            m_backGround.transform.Translate(m_backGround.bounds.size.x * 2, 0, 0);
        }

        if (m_backGroundClone.transform.position.x < m_startPos - m_backGroundClone.bounds.size.x)
        {
            m_backGroundClone.transform.Translate(m_backGroundClone.bounds.size.x * 2, 0, 0);
        }
    }
}
