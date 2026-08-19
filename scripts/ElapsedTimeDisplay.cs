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

    [ExportGroup("Timer Textures")]
    [Export] private Array<AtlasTexture> textures;

    [ExportGroup("Region Data")]
    [Export] private float regionSize;
    [Export] private float separation;
    [Export] private Vector2 startPosition;

    public void SetDigitTexture(AtlasTexture texture, int digit)
    {
        texture.Region = new Rect2(
            startPosition.X + digit * (regionSize + separation), 
            startPosition.Y, 
            regionSize, 
            regionSize
        );
    }

    public override void _Process(double delta)
    {
        int minuteFrame = ElapsedTime.ElapsedMinutes % 10;
        int minutesFrame = ElapsedTime.ElapsedMinutes / 10;
        int secondFrame = ElapsedTime.ElapsedSeconds % 10;
        int secondsFrame = ElapsedTime.ElapsedSeconds / 10;
        int centisecondFrame = ElapsedTime.ElapsedCentiseconds % 10;
        int centisecondsFrame = ElapsedTime.ElapsedCentiseconds / 10;

        SetDigitTexture(textures[(int)Digit.MinutesTens], minutesFrame);
        SetDigitTexture(textures[(int)Digit.Minutes], minuteFrame);
        SetDigitTexture(textures[(int)Digit.SecondsTens], secondsFrame);
        SetDigitTexture(textures[(int)Digit.Seconds], secondFrame);
        SetDigitTexture(textures[(int)Digit.CentisecondsTens], centisecondsFrame);
        SetDigitTexture(textures[(int)Digit.Centiseconds], centisecondFrame);
    }
}
