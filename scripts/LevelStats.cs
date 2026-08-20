using Godot;

public partial class LevelStats : Resource
{
    public int CollectedCoints { get; set; }
    public int TotalJumps { get; set; }
    public int TotalDeaths { get; set; }
    public TimeData Time { get; set; }
}
