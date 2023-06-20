using System;
using UnityEngine;

// Token: 0x02000253 RID: 595
public class PlayAudioSource : MonoBehaviour
{
	// Token: 0x06000E8B RID: 3723 RVA: 0x0005E2C8 File Offset: 0x0005C4C8
	public void Play()
	{
		this.mAudioSource.Play();
	}

	// Token: 0x04000B16 RID: 2838
	[SerializeField]
	private AudioSource mAudioSource;
}
