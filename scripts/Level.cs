using Godot;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public partial class Level : Node2D
{
    [Export] private Node2D respawnPointsParent;
    [Export] private Player player;
    private List<RespawnPoint> respawnPoints;
    public RespawnPoint CurrentRespawnPoint { get; private set; }

    public override void _Ready()
    {
        Debug.Assert(respawnPointsParent != null,
            "Respawn points parent is not set!"
        );

        Debug.Assert(player != null,
            "Player reference not set"
        );

        respawnPoints = respawnPointsParent
            .GetChildren()
            .OfType<RespawnPoint>()
            .OrderBy(point => Mathf.Abs(point.Position.X - player.Position.X))
            .ToList();

        CurrentRespawnPoint = respawnPoints.FirstOrDefault();
    }

    public override void _Process(double delta)
    {

    }
}
