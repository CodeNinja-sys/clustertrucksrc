using System;
using UnityEngine;

// Token: 0x02000176 RID: 374
public class PlayRandomSoundOnImpact : MonoBehaviour
{
	// Token: 0x0600086A RID: 2154 RVA: 0x00037A3C File Offset: 0x00035C3C
	private void Start()
	{
		this.au = base.GetComponent<AudioSource>();
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x00037A4C File Offset: 0x00035C4C
	private void OnCollisionEnter(Collision other)
	{
		this.au.PlayOneShot(this.clips[UnityEngine.Random.Range(0, this.clips.Length)]);
		if (this.screenShake > 0f)
		{
			Camera.main.GetComponent<cameraEffects>().SetShake(0.5f, Vector3.zero);
		}
	}

	// Token: 0x040006AF RID: 1711
	private AudioSource au;

	// Token: 0x040006B0 RID: 1712
	public AudioClip[] clips;

	// Token: 0x040006B1 RID: 1713
	public float screenShake;
}
