using System;
using UnityEngine;

// Token: 0x0200001E RID: 30
public class FinalBossClips : MonoBehaviour
{
	// Token: 0x0600009E RID: 158 RVA: 0x00005DC8 File Offset: 0x00003FC8
	private void Awake()
	{
		this.mAudioSourceIntro.Play();
		this.mAudioSourceLoop.PlayDelayed(this.mAudioSourceIntro.clip.length);
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		Debug.LogError("FinalBosClipAwake");
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00005E10 File Offset: 0x00004010
	private void Update()
	{
	}

	// Token: 0x04000090 RID: 144
	[SerializeField]
	private AudioSource mAudioSourceIntro;

	// Token: 0x04000091 RID: 145
	[SerializeField]
	private AudioSource mAudioSourceLoop;
}
