using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000256 RID: 598
public class SetSkybox : MonoBehaviour
{
	// Token: 0x06000E93 RID: 3731 RVA: 0x0005E470 File Offset: 0x0005C670
	private void Start()
	{
		this.OnEnable();
	}

	// Token: 0x06000E94 RID: 3732 RVA: 0x0005E478 File Offset: 0x0005C678
	private void OnEnable()
	{
		try
		{
			if (this.sendToHandler)
			{
				foreach (TonemapHandler tonemapHandler in UnityEngine.Object.FindObjectsOfType<TonemapHandler>())
				{
					tonemapHandler.tone.middleGrey = this.toneMapBrightnes;
					tonemapHandler.target = 0f;
					tonemapHandler.ogTarget = this.toneMapBrightnes;
				}
			}
		}
		catch
		{
		}
		RenderSettings.skybox = this.skybox;
		RenderSettings.fogColor = this.fogColor;
		RenderSettings.fogDensity = this.fogValue;
		if (this.useSkyBoxAmbience)
		{
			RenderSettings.ambientMode = AmbientMode.Skybox;
		}
		else
		{
			RenderSettings.ambientMode = AmbientMode.Trilight;
			RenderSettings.ambientSkyColor = this.skyColor;
			RenderSettings.ambientEquatorColor = this.equatorColor;
			RenderSettings.ambientGroundColor = this.groundColor;
		}
	}

	// Token: 0x04000B1B RID: 2843
	public Material skybox;

	// Token: 0x04000B1C RID: 2844
	public float toneMapBrightnes = 1.15f;

	// Token: 0x04000B1D RID: 2845
	public Color fogColor = Color.white;

	// Token: 0x04000B1E RID: 2846
	public Color skyColor = Color.white;

	// Token: 0x04000B1F RID: 2847
	public Color equatorColor = Color.white;

	// Token: 0x04000B20 RID: 2848
	public Color groundColor = Color.white;

	// Token: 0x04000B21 RID: 2849
	public float fogValue = 0.0025f;

	// Token: 0x04000B22 RID: 2850
	public bool useSkyBoxAmbience;

	// Token: 0x04000B23 RID: 2851
	public bool sendToHandler;
}
