using System;
using UnityEngine;

// Token: 0x0200017C RID: 380
public class RandomClip : MonoBehaviour
{
	// Token: 0x0600087B RID: 2171 RVA: 0x00037D6C File Offset: 0x00035F6C
	private void Start()
	{
		this.au.pitch = (this.au.pitch *= UnityEngine.Random.Range(0.9f, 1.1f));
		this.au.PlayOneShot(this.clips[UnityEngine.Random.Range(0, this.clips.Length)], UnityEngine.Random.Range(0.9f, 1.1f));
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x00037DD8 File Offset: 0x00035FD8
	private void Update()
	{
	}

	// Token: 0x040006B9 RID: 1721
	public AudioSource au;

	// Token: 0x040006BA RID: 1722
	public AudioClip[] clips;
}
