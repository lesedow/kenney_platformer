using Godot;

public partial class KillZone : Area2D
{
    [Export] private Level level;
    public override void _Ready()
    {
        BodyEntered += KillZone_BodyEntered;
    }

    private async void KillZone_BodyEntered(Node2D body)
    {
        Tween tween = CreateTween();
        tween.SetProcessMode(Tween.TweenProcessMode.Physics);

        Player player = (Player)body;

        player.SetPhysicsProcess(false);
        player.StopMovement();

        tween.Parallel()
             .TweenProperty(body, "position", level.CurrentRespawnPoint.Position, .3f)
             .SetEase(Tween.EaseType.InOut)
             .SetTrans(Tween.TransitionType.Cubic);

        tween.Parallel()
             .TweenProperty(body, "modulate:a", .5f, .3f)
             .SetEase(Tween.EaseType.Out)
             .SetTrans(Tween.TransitionType.Cubic);

        tween.TweenProperty(body, "modulate:a", 1.0f, .3f)
             .SetEase(Tween.EaseType.In)
             .SetTrans(Tween.TransitionType.Cubic);     

        await ToSignal(tween, Tween.SignalName.Finished);

        body.SetPhysicsProcess(true);
    }
}
