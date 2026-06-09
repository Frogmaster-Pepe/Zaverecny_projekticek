using Godot;
using System;

public partial class Projectile : Area2D
{
    public const float Speed = 400.0f;
    private Vector2 _direction = Vector2.Right;
    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += _direction * Speed * (float)delta;
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.Normalized();
        if (_direction.X != 0)
        {
            Scale = new Vector2(_direction.X > 0 ? 1 : -1, 1);
        }
    }
    private void OnBodyEntered(Node body)
    {
        QueueFree();
    }

    private void OnAreaEntered(Area2D area)
    {
        QueueFree();
    }
}