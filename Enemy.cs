using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
    [Export]
    public int Speed { get; set; } = 350; // How fast the player will move (pixels/sec).
    private Player player;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        player = GetNode<Player>("../Player");

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        var velocity = Vector2.Zero; // The player's movement vector.

        //GD.Print(player.Position);
        velocity = (player.Position - Position).Normalized();
        Position += (velocity * Speed) * (float)delta;
    }

    private void OnBodyEntered(Node2D body)
    {
        GD.Print(body.Name);
        // check if the body that entered is a attack hitbox
        if (player.basic_attacking)
        {
            Hide(); // Player disappears after being hit.
            //EmitSignal(SignalName.Hit);
            // Must be deferred as we can't change physics properties on a physics callback.
            //GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
        }
        else
        {
            GD.Print("Player Hit!");
        }
    }
}
