using UnityEngine;

public class IdleState : State
{
    private float horizontalInput;
    private float verticalInput;
    private bool FirePressedInput;
    private bool FireReleaseInput;

    public IdleState(Player character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        horizontalInput = verticalInput = 0.0f;
    }

    public override void HandleInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        FirePressedInput = Input.GetButtonDown("Fire1");
        FireReleaseInput = Input.GetButtonUp("Fire1");
    }

    public override void PhysicsUpdate()
    {
        character.Move(horizontalInput, verticalInput);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        character.ProcessHit(other);
    }

    public override void LogicUpdate()
    {
        if (FirePressedInput)
        {
            character.Shoot();
        }
        if (FireReleaseInput)
        {
            character.StopShoot();
        }
    }
}