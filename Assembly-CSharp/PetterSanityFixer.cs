using System;
using UnityEngine;

// Token: 0x02000175 RID: 373
public class PetterSanityFixer : MonoBehaviour
{
	// Token: 0x06000867 RID: 2151 RVA: 0x000379DC File Offset: 0x00035BDC
	private void Start()
	{
		if (!Application.isEditor)
		{
			return;
		}
		UnityEngine.Object.FindObjectOfType<IntroHandler>().GetComponent<Animator>().speed = 5f;
		UnityEngine.Object.FindObjectOfType<IntroHandler>().GetComponent<Animator>().speed = 500f;
		UnityEngine.Object.FindObjectOfType<IntroHandler>().GetComponent<Animator>().speed = 505f;
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x00037A30 File Offset: 0x00035C30
	private void Update()
	{
	}
}
