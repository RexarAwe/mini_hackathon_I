using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
    public int Speed { get; set; } = 400; // How fast the player will move (pixels/sec).

    public Vector2 ScreenSize; // Size of the game window.

    private AnimatedSprite2D basic_attack;
    public bool basic_attacking = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
        GetNode<Timer>("BasicAttackTimer").Start();
        basic_attack = GetNode<AnimatedSprite2D>("BasicAttack");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        var velocity = Vector2.Zero; // The player's movement vector.

        if (Input.IsActionPressed("move_right"))
        {
            velocity.X += 1;
        }

        if (Input.IsActionPressed("move_left"))
        {
            velocity.X -= 1;
        }

        if (Input.IsActionPressed("move_down"))
        {
            velocity.Y += 1;
        }

        if (Input.IsActionPressed("move_up"))
        {
            velocity.Y -= 1;
        }

        Position += (velocity * Speed) * (float)delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
            y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
        );

        //if (Input.IsActionJustReleased("left_mouse_click"))
        //if (Input.IsActionJustPressed("left_mouse_click"))
        //{
        //    GD.Print("swish!");
        //}
    }

    private void OnBasicAttackTimerTimeout()
    {
        // create hit box around the player character

        // generate sprite in the direction where the mouse is 
        
        if (basic_attack.IsPlaying())
        {
            basic_attack.Stop();
            basic_attack.Hide();

            basic_attacking = false;
        }
        else
        {
            basic_attack.Show();
            basic_attack.Play();

            basic_attacking = true;
        }
        
    }

    //private void OnBodyEntered(Node2D body)
    //{
    //    Hide(); // Player disappears after being hit.
    //    EmitSignal(SignalName.Hit);
    //    // Must be deferred as we can't change physics properties on a physics callback.
    //    GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
    //}
}
