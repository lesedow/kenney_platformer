using Godot;

public partial class Coin : Area2D
{
    [ExportGroup("Animation")]
    [Export] private float Distance;
    [Export] private float Duration;
    [Export] private Tween.TransitionType Transition;
    [Export] private Tween.EaseType Ease;
    private AnimatedSprite2D Visual;

    public override void _Ready()
    {
        Visual = GetNode<AnimatedSprite2D>("Visual");

        if (Visual == null)
        {
            GD.PrintErr($"{Name} doesn't have a visual attached!");
            return;
        }

        Tween tween = CreateTween();
        tween.SetLoops();
        tween.TweenProperty(Visual, "position:y", Distance, Duration)
              .SetEase(Ease)
              .SetTrans(Transition);
        tween.TweenProperty(Visual, "position:y", -Distance, Duration)
             .SetEase(Ease) 
             .SetTrans(Transition);
    }
}
