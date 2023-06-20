using System;
using UnityEngine;

// Token: 0x02000300 RID: 768
public class setRandoScale : MonoBehaviour
{
	// Token: 0x06001217 RID: 4631 RVA: 0x000737B4 File Offset: 0x000719B4
	private void Start()
	{
		base.transform.localScale *= UnityEngine.Random.Range(this.min, this.max);
	}

	// Token: 0x06001218 RID: 4632 RVA: 0x000737E8 File Offset: 0x000719E8
	private void Update()
	{
	}

	// Token: 0x04000F3D RID: 3901
	public float min = 0.5f;

	// Token: 0x04000F3E RID: 3902
	public float max = 1.5f;
}
