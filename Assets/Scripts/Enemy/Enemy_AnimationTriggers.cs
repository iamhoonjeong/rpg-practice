using UnityEngine;

public class Enemy_AnimationTriggers : EntityAnimationTriggers
{
    private Enemy enemy;
    private Enemy_VFX enemyVfx;

    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponentInParent<Enemy>();
        enemyVfx = GetComponentInParent<Enemy_VFX>();
    }

    private void EnableCounterWindow()
    {
        enemyVfx.EnableAttackAlert(true);
        enemy.EnableCounterWindow(true);
    }

    private void DisableCounterWindow()
    {
        enemyVfx.EnableAttackAlert(false);
        enemy.EnableCounterWindow(false);
    }
}
