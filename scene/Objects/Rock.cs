using Godot;
using System;

public partial class Rock : RigidBody2D
{
	public const float PushForce = 300.0f;
	public const float Friction = 0.9f;

	private bool _isInLava = false;

	public override void _Ready()
	{
		GravityScale = 1.0f;
		LockRotation = true;
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	public override void _PhysicsProcess(double delta)
	{
		// Pokud je v lávě, není ovlivněn gravitací
		if (_isInLava)
		{
			GravityScale = 0.0f;
			// Aplikuj friction
			LinearVelocity *= Friction;
		}
		else
		{
			GravityScale = 1.0f;
		}
	}

	private void OnBodyEntered(Node body)
	{
		// Detekuj vstup do lávy
		if (body is Lava)
		{
			_isInLava = true;
			GD.Print("Rock vstoupil do lávy!");
		}

		// Dej se tlačit hráči (horizontálně)
		if (body is Pl1 pl1)
		{
			PushRock(pl1);
		}
		else if (body is Pl2 pl2)
		{
			PushRock(pl2);
		}
	}

	private void OnBodyExited(Node body)
	{
		// Detekuj výstup z lávy
		if (body is Lava)
		{
			_isInLava = false;
			GD.Print("Rock opustil lávu!");
		}
	}

	private void PushRock(Node2D player)
	{
		// Tlač rock pouze horizontálně podle směru hráče
		float direction = player.GlobalPosition.X < GlobalPosition.X ? 1 : -1;
		ApplyCentralForce(new Vector2(direction * PushForce, 0));
		GD.Print($"Rock je tlačen hráčem: {player.Name}");
	}
}