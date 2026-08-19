using Godot;
public partial class ElapsedTime : Node
{
    public static int ElapsedMinutes { get; private set; }
    public static int ElapsedSeconds { get; private set; }
    public static int ElapsedCentiseconds { get; private set; }
    private static float acumulatedDelta = 0.0f;

    public static ElapsedTime Instance { get; set; }

    private ElapsedTime() { }
    public override void _Ready()
    {
        if (Instance != null)
        {
            Instance = this;
        }
    }

    private void RecalculateElapsedTime()
    {
        ElapsedMinutes = (int)(acumulatedDelta / 60) % 60;
        ElapsedSeconds = (int)(acumulatedDelta % 60);
        ElapsedCentiseconds = (int)((acumulatedDelta * 100) % 100);
    }

    public override void _Process(double delta)
    {
        acumulatedDelta += (float)delta;
        RecalculateElapsedTime();
    }
}
