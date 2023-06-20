using System;
using UnityEngine;

// Token: 0x02000177 RID: 375
public class PlaySoundOnPlayerTouch : MonoBehaviour
{
	// Token: 0x0600086D RID: 2157 RVA: 0x00037AAC File Offset: 0x00035CAC
	private void Start()
	{
		this.au = base.GetComponent<AudioSource>();
		if (this.au == null)
		{
			base.transform.parent.GetComponent<AudioSource>();
		}
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x00037AE8 File Offset: 0x00035CE8
	private void OnCollisionEnter(Collision other)
	{
		if (other.transform.tag == "Player")
		{
			base.enabled = false;
			this.au.PlayOneShot(this.clips[UnityEngine.Random.Range(0, this.clips.Length)]);
		}
	}

	// Token: 0x040006B2 RID: 1714
	private AudioSource au;

	// Token: 0x040006B3 RID: 1715
	public AudioClip[] clips;
}
