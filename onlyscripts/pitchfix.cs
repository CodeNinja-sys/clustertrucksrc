using System;
using UnityEngine;

// Token: 0x020002A4 RID: 676
public class pitchfix : MonoBehaviour
{
	// Token: 0x0600101A RID: 4122 RVA: 0x00067E50 File Offset: 0x00066050
	private void Start()
	{
		this.lowPass = base.gameObject.AddComponent<AudioLowPassFilter>();
		if (!this.lowPass)
		{
			this.lowPass = base.GetComponent<AudioLowPassFilter>();
		}
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x00067E8C File Offset: 0x0006608C
	private void LateUpdate()
	{
		this.lowPass.cutoffFrequency = Mathf.Lerp(this.lowPass.cutoffFrequency, 22000f * Mathf.Pow(Time.timeScale, 1f), Time.unscaledDeltaTime * 10f);
	}

	// Token: 0x04000CF5 RID: 3317
	private float normalPitch;

	// Token: 0x04000CF6 RID: 3318
	private AudioSource au;

	// Token: 0x04000CF7 RID: 3319
	private float startPitch;

	// Token: 0x04000CF8 RID: 3320
	public float lowCap = 0.5f;

	// Token: 0x04000CF9 RID: 3321
	private AudioLowPassFilter lowPass;
}
