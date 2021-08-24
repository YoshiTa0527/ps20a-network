using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackGround : MonoBehaviour
{
    [SerializeField] GameObject m_startPosition;
    [SerializeField] GameObject m_endPosition;
    [SerializeField] GameObject m_otherEndPosition;
    Vector3 m_startPos;
    Vector3 m_camelaRectMin;
    [SerializeField] SpriteRenderer m_backGround;
    SpriteRenderer m_backGroundClone;
    [SerializeField] float m_spd = 5f;
    private void Start()
    {
        m_backGroundClone = Instantiate(m_backGround);
        m_backGroundClone.transform.Translate(0f, m_backGround.bounds.size.y, 0f);
    }
    private void Update()
    {

        this.transform.Translate(Vector2.left * m_spd * Time.deltaTime);
        if (transform.position.x < (m_camelaRectMin.x - Camera.main.transform.position.x) * 2)
        {
            transform.position = new Vector2((Camera.main.transform.position.x - m_camelaRectMin.x) * 2, transform.position.y);
        }

    }
}
