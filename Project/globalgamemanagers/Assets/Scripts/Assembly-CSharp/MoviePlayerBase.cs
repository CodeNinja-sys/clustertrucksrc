using UnityEngine;

public class MoviePlayerBase : MonoBehaviour
{
	public enum ScreenMode
	{
		Crop = 0,
		Fill = 1,
		Stretch = 2,
		CustomRect = 3,
	}

	public Texture2D framebuffer;
	public AudioClip audiobuffer;
	public bool drawToScreen;
	public Material otherMaterial;
	public Material material;
	public string texturePropertyName;
	public ScreenMode screenMode;
	public int screenGuiDepth;
	public Rect customScreenRect;
	public bool play;
	public int maxSyncErrorFrames;
}
