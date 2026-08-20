using Godot;

public partial class Player : CharacterBody2D
{
    [Export] private Sprite2D visual;
    [Export] private GpuParticles2D jumpParticles;

    [ExportGroup("Jump Settings")]
    [Export] private float maxFallSpeed;    
    [Export] private float jumpSpeed;
    [Export(PropertyHint.Range, "0.0, 1.0, 0.1")] private float jumpCutAmount;

    [ExportGroup("Squash Animation")]
    [Export] private Vector2 squashDownValues;
    [Export] private Vector2 squashUpValues;
    [Export] private float squashAnimationDuration;
    [Export] private Tween.TransitionType transitionType;

    [ExportGroup("Movement Settings")]
    [Export] private float maxSpeed;
    [Export] private float acceleration;
    [Export] private float deceleration;

    private Tween jumpTween;
    private Vector2 velocity;

    private void ApplyGravity(float deltaTime)
    {
        velocity.Y = (!IsOnFloor())
            ? velocity.Y + GetGravity().Y * deltaTime
            : 0.0f;

        velocity.Y = Mathf.Min(maxFallSpeed, velocity.Y);
    }

    private void HandleJump(float deltaTime)
    {
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            PlayJumpWindUp();

            jumpParticles.Restart();
            jumpParticles.Emitting = true;

            velocity.Y = jumpSpeed;
        }

        if ((Input.IsActionJustReleased("jump") 
            && velocity.Y < 0.0f) || 
            IsOnCeiling())
        {
            velocity.Y *= jumpCutAmount;
        }
        
    }

    private void PlayJumpWindUp()
    {
        // When the player jump stretch the height until it reaches the peak of the jump
        // or the player cuts the jump earlier

        jumpTween = CreateTween();
        
        jumpTween.TweenProperty(visual, "scale", squashDownValues, squashAnimationDuration)
            .SetEase(Tween.EaseType.Out)
            .SetTrans(transitionType);

        jumpTween.TweenProperty(visual, "scale", squashUpValues, squashAnimationDuration)
            .SetEase(Tween.EaseType.In)
            .SetTrans(transitionType);

        jumpTween.TweenProperty(visual, "scale", new Vector2(1.0f, 1.0f), squashAnimationDuration)
            .SetEase(Tween.EaseType.Out)
            .SetTrans(transitionType);
    }

    private void MovePlayer(float deltaTime)
    {
        float direction = Input.GetAxis("move_left", "move_right");

        if (direction != 0.0f)
            visual.FlipH = direction > 0.0f;

        if (direction != 0.0f){
            velocity.X = Mathf.MoveToward(velocity.X, maxSpeed * direction, acceleration * deltaTime);
        } else {
            velocity.X = Mathf.MoveToward(velocity.X, 0.0f, deceleration * deltaTime);
        }

    }

    public void StopMovement() => velocity.X = 0.0f;  

    public override void _PhysicsProcess(double delta)
    {
        float deltaTime = (float)delta;
        
        MovePlayer(deltaTime);
        ApplyGravity(deltaTime);
        HandleJump(deltaTime);

        Velocity = velocity;
        MoveAndSlide();
    }
}
