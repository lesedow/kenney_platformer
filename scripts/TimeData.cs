using Godot;

public partial class TimeData : Resource
{
    public int Minutes { get; set; }
    public int Seconds { get; set; }
    public int Centiseconds { get; set; }
    
    public string GetFormatedTime()
    {
        return $"{Minutes:00}:{Seconds:00}:{Centiseconds:00}";
    }
}
