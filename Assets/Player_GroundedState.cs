using UnityEngine;

public class Player_GroundedState : EntityState
{
    public Player_GroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocityY < 0) stateMachine.ChangeState(player.fallState);

        if (input.Player.Jump.WasCompletedThisFrame()) stateMachine.ChangeState(player.jumpState);
    }
}
