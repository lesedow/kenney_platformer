using Godot;
using Godot.Collections;

public partial class ElapsedTimeDisplay : Control
{
    [Export] private TextureRect minutes;
    [Export] private TextureRect minute;
    [Export] private TextureRect seconds;
    [Export] private TextureRect second;
    [Export] private TextureRect centiseconds;
    [Export] private TextureRect centisecond;

    public override void _Process(double delta)
    {
    }

}
