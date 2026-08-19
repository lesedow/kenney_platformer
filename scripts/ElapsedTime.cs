using Godot;
public partial class ElapsedTime : Node
{
    public int elapesedMinutes { get; private set; }
    public int elapsedSeconds { get; private set; }
    public int elapsedCentiSeconds { get; private set; }

    private float acumulatedDelta = 0.0f;

    private void RecalculateElapsedTime()
    {
        elapsedSeconds = (int)(acumulatedDelta % 60);
    }

    public override void _Process(double delta)
    {
        acumulatedDelta += (float)delta;
        RecalculateElapsedTime();
    }
}
