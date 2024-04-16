using Godot;
using System;

public partial class Main : Node2D
{
    [Export]
    public PackedScene EnemyScene { get; set; }
    private Timer mob_timer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        mob_timer  = GetNode<Timer>("MobTimer");
        mob_timer.Start();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void OnMobTimerTimeout()
    {
        // Note: Normally it is best to use explicit types rather than the `var`
        // keyword. However, var is acceptable to use here because the types are
        // obviously Mob and PathFollow2D, since they appear later on the line.

        // Create a new instance of the Mob scene.
        //Mob mob = MobScene.Instantiate<Mob>();

        Enemy enemy = EnemyScene.Instantiate<Enemy>();
        PathFollow2D mobSpawnLocation = GetNode<PathFollow2D>("Player/MobSpawnPath/MobSpawnLocation");

        mobSpawnLocation.ProgressRatio = GD.Randf();
        enemy.Position = mobSpawnLocation.Position;
        GD.Print("Spawning at position " + mobSpawnLocation.Position);
        AddChild(enemy);
    }

    public void OnDifficultyTimerTimeout()
    {
        if (mob_timer.WaitTime > 0)
        {
            GD.Print("The horde strengthens!");
            mob_timer.WaitTime--;
        }
    }
}