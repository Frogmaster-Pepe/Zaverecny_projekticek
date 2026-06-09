using Godot;
using System;

public partial class Pl2 : CharacterBody2D
{
    public const float BaseSpeed = 200.0f;
    public const float JumpForce = -400.0f;
    public const float Gravity = 800.0f;
    public const string ProjectileScenePath = "res://scene/Projectiles/Projectile.tscn";
    public const float ProjectileSpawnOffset = 50.0f;
    public const float WaterSlowSpeed = 100.0f;
    public const float Acceleration = 1500.0f;

    private Vector2 _lastDirection = Vector2.Right;
    private bool _isInWater = false;
    private Vector2 _prevVelocity = Vector2.Zero;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
        float direction = Input.GetAxis("ui_left", "ui_right");

        if (direction != 0)
        {
            _lastDirection = new Vector2(direction, 0);
        }

        float currentSpeed = _isInWater ? WaterSlowSpeed : BaseSpeed;

        if (direction != 0)
        {
            velocity.X = Mathf.MoveToward(velocity.X, direction * currentSpeed, Acceleration * (float)delta);
        }
        else
        {
            velocity.X = Mathf.MoveToward(velocity.X, 0, Acceleration * (float)delta);
        }
        if (Input.IsActionJustPressed("ui_up") && IsOnFloor())
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
        if (Input.IsActionJustPressed("mo_shoot"))
        {
            ShootProjectile();
        }

        Velocity = velocity;
        _prevVelocity = velocity;
        MoveAndSlide();
    }
    private void ShootProjectile()
    {
        var projectileScene = GD.Load<PackedScene>(ProjectileScenePath);
        if (projectileScene == null)
        {
            return;
        }
        var projectile = projectileScene.Instantiate<Projectile>();
        GetParent().AddChild(projectile);
        projectile.GlobalPosition = GlobalPosition + _lastDirection * ProjectileSpawnOffset;
        projectile.SetDirection(_lastDirection);
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