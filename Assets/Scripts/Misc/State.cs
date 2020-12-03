using UnityEngine;

public abstract class State
{
    protected Player character;
    protected StateMachine stateMachine;

    protected State(Player character, StateMachine stateMachine)
    {
        this.character = character;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
    }

    public virtual void Exit()
    {
    }
}