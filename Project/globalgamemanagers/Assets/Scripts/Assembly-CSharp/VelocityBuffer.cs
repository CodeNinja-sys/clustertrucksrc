using UnityEngine;

public class VelocityBuffer : EffectBase
{
	public enum NeighborMaxSupport
	{
		TileSize10 = 0,
		TileSize20 = 1,
		TileSize40 = 2,
	}

	public Shader velocityShader;
	public bool neighborMaxGen;
	public NeighborMaxSupport neighborMaxSupport;
}
