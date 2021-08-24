using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の波打つ動き
/// </summary>
public class EnemyRipplingMotion : EnemyController
{
    /// <summary>範囲</summary>
    [SerializeField] float m_range = 3;
    /// <summary>最大の高さ</summary>
    float m_maxHeight;
    /// <summary>最低の高さ</summary>
    float m_minHeight;
    /// <summary>回転の速さ</summary>
    [SerializeField] float m_rotateSpeed = 3;
    /// <summary>元の高さ</summary>
    float m_originalHeight;
    /// <summary> 前フレームの高さ</summary>
    float m_lastHeight;

    protected override void Start()
    {
        base.Start();
        m_originalHeight = transform.position.y;
        m_lastHeight = m_originalHeight;

        m_maxHeight = m_originalHeight + m_range / 2;
        m_minHeight = m_originalHeight - m_range / 2;
    }

    protected override void Move()
    {
        base.Move();

        m_rb.velocity = new Vector2(m_rb.velocity.x, m_rotateSpeed);

        if (m_lastHeight > m_maxHeight)
        {
            transform.position = new Vector2(transform.position.x, m_maxHeight - 0.1f);
            m_rotateSpeed *= -1;
        }
        else if (m_lastHeight < m_minHeight)
        {
            transform.position = new Vector2(transform.position.x, m_minHeight + 0.1f);
            m_rotateSpeed *= -1;
        }

        m_lastHeight = transform.position.y;
    }
}
