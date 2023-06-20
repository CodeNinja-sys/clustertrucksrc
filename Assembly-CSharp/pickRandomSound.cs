using System;
using UnityEngine;

// Token: 0x020002A3 RID: 675
public class pickRandomSound : MonoBehaviour
{
	// Token: 0x06001018 RID: 4120 RVA: 0x00067E08 File Offset: 0x00066008
	private void Start()
	{
		base.gameObject.GetComponent<AudioSource>().PlayOneShot(this.clips[UnityEngine.Random.Range(0, this.clips.Length)]);
	}

	// Token: 0x04000CF4 RID: 3316
	public AudioClip[] clips;
}
