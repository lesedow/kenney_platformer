using Godot;

[GlobalClass]
public partial class SpeedrunTimer : Node
{
    public TimeData Time { get; private set; }
    private static float AcumulatedDelta { get; set; }

    public override void _Ready()
    {
        Time = new TimeData();
    }

    private void RecalculateElapsedTime()
    {
        Time.Minutes = (int)(AcumulatedDelta / 60) % 60;
        Time.Seconds = (int)(AcumulatedDelta % 60);
        Time.Centiseconds = (int)((AcumulatedDelta * 100) % 100);
    }

    public override void _Process(double delta)
    {
        AcumulatedDelta += (float)delta;
        RecalculateElapsedTime();
    }
}
