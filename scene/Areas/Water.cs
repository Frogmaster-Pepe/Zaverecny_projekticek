using Godot;
using System;

public partial class Water : Area2D
{
    public override void _Ready()
    {
        // BodyEntered se volá když CharacterBody2D vstoupí do Area2D
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;
        GD.Print($"Water ready - Layer: {CollisionLayer}, Mask: {CollisionMask}");
    }

    private void OnBodyEntered(Node body)
    {
        GD.Print($"Water: Body entered - {body.Name}");

        if (body is Projectile)
        {
            return;
        }

        if (body is Pl1 pl1)
        {
            GD.Print("Water: Pl1 vstoupil do vody");
            pl1.ApplyWaterSlow();
        }
        else if (body is Pl2 pl2)
        {
            GD.Print("Water: Pl2 vstoupil do vody");
            pl2.ApplyWaterSlow();
        }
    }

    private void OnBodyExited(Node body)
    {
        GD.Print($"Water: Body exited - {body.Name}");

        if (body is Projectile)
        {
            return;
        }

        if (body is Pl1 pl1)
        {
            GD.Print("Water: Pl1 opustil vodu");
            pl1.RemoveWaterSlow();
        }
        else if (body is Pl2 pl2)
        {
            GD.Print("Water: Pl2 opustil vodu");
            pl2.RemoveWaterSlow();
        }
    }
}