using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private float _jumpTime = 0.1f;
    private float _timer = 0;

    public PlayerJumpState(Transform player)
    {
    }

    public override void Start(PlayerStateManager manager)
    {
        manager.anim.Play("Jump");
        manager.rg.AddForce(Vector2.up * manager.jumpForce, ForceMode2D.Impulse);
        _timer = 0;
    }

    public override void FixUpdate(PlayerStateManager manager)
    {
        manager.rg.velocity = new Vector2(manager.direction.x * manager.speed, manager.rg.velocity.y);
    }

    public override void Update(PlayerStateManager manager)
    {
        _timer += Time.deltaTime;

        manager.render.flipX = manager.direction.x < 0 || (manager.direction.x <= 0 && manager.render.flipX);

        if (_timer < _jumpTime) return;

        if (!manager.grounded && manager.frameInput.JumpDown)
        {
            manager.SwitchState(manager.DoubleJump);
        }

        if (manager.grounded && manager.frameInput.Move == Vector2.zero)
        {
            manager.SwitchState(manager.IdleState);
        }

        if (manager.grounded && manager.frameInput.Move != Vector2.zero)
        {
            manager.SwitchState(manager.RunState);
        }
    }

    public override void OnCollisionEnter(PlayerStateManager manager, Collision2D other) { }
}