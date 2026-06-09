using Godot;
using System;

public partial class Pl1 : CharacterBody2D
{

    public const float BaseSpeed = 200.0f;
    public const float JumpForce = -400.0f;
    public const float Gravity = 800.0f;
    public const float DashDistance = 80.0f;
    public const float DashDuration = 0.15f;
    public const float DashCooldown = 1.5f;
    public const float WaterSlowSpeed = 100.0f;
    public const float Acceleration = 1500.0f;

    private Vector2 _lastDirection = Vector2.Right;
    private bool _isDashing = false;
    private float _dashTimer = 0.0f;
    private float _dashCooldownTimer = 0.0f;
    private bool _isInWater = false;
    private Vector2 _prevVelocity = Vector2.Zero;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
        float direction = Input.GetAxis("mo_left", "mo_right");

        if (direction != 0)
        {
            _lastDirection = new Vector2(direction, 0);
        }

        float currentSpeed = _isInWater ? WaterSlowSpeed : BaseSpeed;

        if (_dashCooldownTimer > 0.0f)
        {
            _dashCooldownTimer -= (float)delta;
        }
        if (Input.IsActionJustPressed("mo_dash") && !_isDashing && _dashCooldownTimer <= 0.0f)
        {
            StartDash();
        }

        if (_isDashing)
        {
            _dashTimer -= (float)delta;
            if (_dashTimer <= 0.0f)
            {
                _isDashing = false;
                _dashCooldownTimer = DashCooldown;
            }
            else
            {
                velocity.X = _lastDirection.X * (DashDistance / DashDuration);
                velocity.Y = 0;
            }
        }
        else
        {

            if (direction != 0)
            {
                velocity.X = Mathf.MoveToward(velocity.X, direction * currentSpeed, Acceleration * (float)delta);
            }
            else
            {
                velocity.X = Mathf.MoveToward(velocity.X, 0, Acceleration * (float)delta);
            }


            if (Input.IsActionJustPressed("mo_up") && IsOnFloor())
            {
                velocity.Y = JumpForce;
            }


            if (!IsOnFloor())
            {
                velocity.Y += Gravity * (float)delta;
            }
            else
            {

                if (velocity.Y > 0)
                {
                    velocity.Y = 0;
                }
            }
        }

        Velocity = velocity;
        _prevVelocity = velocity;
        MoveAndSlide();
    }
    private void StartDash()
    {
        _isDashing = true;
        _dashTimer = DashDuration;
    }
    public void ApplyWaterSlow()
    {
        _isInWater = true;
    }
    public void RemoveWaterSlow()
    {
        _isInWater = false;
    }
}