using System;
using UnityEngine;

// Token: 0x0200025C RID: 604
public class TonemapHandler : MonoBehaviour
{
	// Token: 0x06000EAB RID: 3755 RVA: 0x0005E978 File Offset: 0x0005CB78
	private void Update()
	{
		if (this.target != 0f)
		{
			this.tone.middleGrey = Mathf.Lerp(this.tone.middleGrey, this.target, Time.deltaTime * 2f);
		}
		else
		{
			this.tone.middleGrey = Mathf.Lerp(this.tone.middleGrey, this.ogTarget, Time.deltaTime * 2f);
		}
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x0005E9F4 File Offset: 0x0005CBF4
	public void SetTone(float f)
	{
		this.target = f;
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x0005EA00 File Offset: 0x0005CC00
	private void OnEnable()
	{
		if (this.dontLook)
		{
			return;
		}
		this.target = 0f;
		this.tone.middleGrey = ((!UnityEngine.Object.FindObjectOfType<SetSkybox>()) ? 1f : UnityEngine.Object.FindObjectOfType<SetSkybox>().toneMapBrightnes);
		this.ogTarget = ((!UnityEngine.Object.FindObjectOfType<SetSkybox>()) ? 1f : UnityEngine.Object.FindObjectOfType<SetSkybox>().toneMapBrightnes);
	}

	// Token: 0x04000B37 RID: 2871
	public Tonemapping tone;

	// Token: 0x04000B38 RID: 2872
	public float target;

	// Token: 0x04000B39 RID: 2873
	public float ogTarget;

	// Token: 0x04000B3A RID: 2874
	public bool dontLook;
}
