using UnityEngine;
using AmplifyColor;

public class AmplifyColorBase : MonoBehaviour
{
	public Quality QualityLevel;
	public float BlendAmount;
	public Texture LutTexture;
	public Texture LutBlendTexture;
	public Texture MaskTexture;
	public bool UseVolumes;
	public float ExitVolumeBlendTime;
	public Transform TriggerVolumeProxy;
	public LayerMask VolumeCollisionMask;
	public VolumeEffectFlags EffectFlags;
	[SerializeField]
	private string sharedInstanceID;
}
