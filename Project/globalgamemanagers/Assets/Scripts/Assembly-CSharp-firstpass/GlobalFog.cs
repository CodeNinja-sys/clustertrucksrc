using UnityEngine;

internal class GlobalFog : PostEffectsBase
{
	public bool distanceFog;
	public bool heightFog;
	public float height;
	public float heightDensity;
	public float startDistance;
	public Shader fogShader;
}
