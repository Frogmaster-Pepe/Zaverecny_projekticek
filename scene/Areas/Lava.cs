using Godot;
using System;

public partial class Lava : Area2D
{
    public override void _Ready()
    {
        // BodyEntered se volá když CharacterBody2D vstoupí do Area2D
        BodyEntered += OnBodyEntered;
        GD.Print($"Lava ready - Layer: {CollisionLayer}, Mask: {CollisionMask}");
    }

    private void OnBodyEntered(Node body)
    {
        GD.Print($"Lava: Body entered - {body.Name}");

        if (body is Projectile)
        {
            return;
        }

        if (body is Pl1)
        {
            GD.Print("Pl1 se dotkl lávy!");
            GetTree().ReloadCurrentScene();
        }
        else if (body is Pl2)
        {
            GD.Print("Pl2 se dotkl lávy!");
            GetTree().ReloadCurrentScene();
        }
    }
}