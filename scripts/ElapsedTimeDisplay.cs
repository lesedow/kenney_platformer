using Godot;
using Godot.Collections;

public partial class ElapsedTimeDisplay : Control
{
    public enum Digit 
    { 
        MinutesTens, 
        Minutes, 
        SecondsTens, 
        Seconds, 
        CentisecondsTens, 
        Centiseconds 
    };

    [ExportGroup("Timer Data")]
    [Export] private SpeedrunTimer Timer;
    [Export] private Array<AtlasTexture> Textures;

    [ExportGroup("Region Data")]
    [Export] private float RegionSize;
    [Export] private float Separation;
    [Export] private Vector2 StartPosition;

    public void SetDigitTexture(AtlasTexture texture, int digit)
    {
        texture.Region = new Rect2(
            StartPosition.X + digit * (RegionSize + Separation), 
            StartPosition.Y, 
            RegionSize, 
            RegionSize
        );
    }

    public override void _Process(double delta)
    {
        int minuteFrame       =      Timer.Time.Minutes % 10;
        int minutesFrame      =      Timer.Time.Minutes / 10;
        int secondFrame       =      Timer.Time.Seconds % 10;
        int secondsFrame      =      Timer.Time.Seconds / 10;
        int centisecondFrame  = Timer.Time.Centiseconds % 10;
        int centisecondsFrame = Timer.Time.Centiseconds / 10;

        SetDigitTexture(Textures[(int)Digit.MinutesTens], minutesFrame);
        SetDigitTexture(Textures[(int)Digit.Minutes], minuteFrame);
        SetDigitTexture(Textures[(int)Digit.SecondsTens], secondsFrame);
        SetDigitTexture(Textures[(int)Digit.Seconds], secondFrame);
        SetDigitTexture(Textures[(int)Digit.CentisecondsTens], centisecondsFrame);
        SetDigitTexture(Textures[(int)Digit.Centiseconds], centisecondFrame);
    }
}
