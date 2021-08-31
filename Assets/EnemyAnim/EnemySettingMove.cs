using UnityEngine;

/// <summary>
/// 指定された時間が経過したら次の方向へ移動する動き
/// </summary>
public class EnemySettingMove : EnemyController
{
    /// <summary>次に動く方向</summary>
    [SerializeField] Vector2 m_nextMoveDirection = Vector2.up;
    /// <summary>次の方向に動くまでの時間</summary>
    [SerializeField] float m_nextMoveTime = 3f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        
        if (m_timer > m_nextMoveTime)
        {
            Move(m_nextMoveDirection);
        }
    }
}
