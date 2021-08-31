using UnityEngine;

/// <summary>
/// アニメーションでの動き
/// </summary>
public class EnemyAnimMove : EnemyController
{
    enum MoveVariation
    {
        CurveMove,
        CurveMoveDown,
        FigureEight,
    }
    /// <summary>動きのアニメーションの設定</summary>
    [SerializeField] MoveVariation m_variation = MoveVariation.CurveMove;
    Animator m_anim;

    protected override void Start()
    {
        base.Start();
        m_anim = GetComponent<Animator>();
        string animName = m_variation.ToString();
        m_anim.Play(animName);
    }
}
