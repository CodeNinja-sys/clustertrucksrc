using System;
using AmplifyColor;
using UnityEngine;

// Token: 0x0200000A RID: 10
[AddComponentMenu("")]
public class AmplifyColorVolumeBase : MonoBehaviour
{
	// Token: 0x06000033 RID: 51 RVA: 0x00003B80 File Offset: 0x00001D80
	private void OnDrawGizmos()
	{
		if (this.ShowInSceneView)
		{
			BoxCollider component = base.GetComponent<BoxCollider>();
			if (component != null)
			{
				Gizmos.color = Color.green;
				Gizmos.DrawIcon(base.transform.position, "lut-volume.png", true);
				Gizmos.matrix = base.transform.localToWorldMatrix;
				Gizmos.DrawWireCube(component.center, component.size);
			}
		}
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00003BEC File Offset: 0x00001DEC
	private void OnDrawGizmosSelected()
	{
		BoxCollider component = base.GetComponent<BoxCollider>();
		if (component != null)
		{
			Color green = Color.green;
			green.a = 0.2f;
			Gizmos.color = green;
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Gizmos.DrawCube(component.center, component.size);
		}
	}

	// Token: 0x0400004E RID: 78
	public Texture2D LutTexture;

	// Token: 0x0400004F RID: 79
	public float EnterBlendTime = 1f;

	// Token: 0x04000050 RID: 80
	public int Priority;

	// Token: 0x04000051 RID: 81
	public bool ShowInSceneView = true;

	// Token: 0x04000052 RID: 82
	[HideInInspector]
	public VolumeEffectContainer EffectContainer = new VolumeEffectContainer();
}
