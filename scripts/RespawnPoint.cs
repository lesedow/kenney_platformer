#if TOOLS
using Godot;

[Tool]
public partial class RespawnPoint : Marker2D
{
    private float _pointRadius;
    private Color _pointColor;
    private Label _displayName;
    [Export] private float PointRadius 
    {
        set 
        { 
            _pointRadius = value;
            QueueRedraw();
        }

        get => _pointRadius;
    }

    [Export] private Color PointColor
    { 
        set
        {
            _pointColor = value;
            QueueRedraw();
        }

        get => _pointColor;
    }

    public override void _EnterTree()
    {
        if (!Engine.IsEditorHint()) return;

        if (_displayName == null)
        {
            _displayName = new Label();
            AddChild(_displayName);
        }

        Renamed += UpdateDisplayName;

        UpdateDisplayName();
    }

    public void UpdateDisplayName()
    {
        _displayName.Text = Name;
        _displayName.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.Center);
    }

    public override void _ExitTree()
    {
        if (!Engine.IsEditorHint()) return;

        if (!IsConnected(SignalName.Renamed, Callable.From(UpdateDisplayName)))
            return;
        
        Renamed -= UpdateDisplayName;
    }

    public override void _Draw()
    {
        if (Engine.IsEditorHint())
            DrawCircle(position: Vector2.Zero, radius: PointRadius, color: PointColor);
    }
}
#endif