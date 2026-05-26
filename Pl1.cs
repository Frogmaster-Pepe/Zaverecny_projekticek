using Godot;
using System;

public partial class Pl1 : CharacterBody2D
{
	  public const float Speed = 400.0f;
	public override void _PhysicsProcess(double delta) 
	{
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector("mo_left", "mo_right", "mo_up", "mo_down").Normalized();
   
		if (direction != Vector2.Zero) 
		{
			velocity = direction * Speed;
			LookAt(Position + direction);
		} 
		else {
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}
		Velocity = velocity;
		MoveAndSlide();
	}
}

	
