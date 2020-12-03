using UnityEngine;

public class InvulnerableState : IdleState
{
    public InvulnerableState(Player character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        character.BecomeInvulnerable();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        return;
    }
}