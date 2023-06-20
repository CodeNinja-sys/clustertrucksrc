using System;
using UnityEngine;

// Token: 0x0200023A RID: 570
public class makeMapLayer : MonoBehaviour
{
	// Token: 0x06000DB4 RID: 3508 RVA: 0x00058E68 File Offset: 0x00057068
	private void Start()
	{
	}

	// Token: 0x06000DB5 RID: 3509 RVA: 0x00058E6C File Offset: 0x0005706C
	private void Update()
	{
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x00058E70 File Offset: 0x00057070
	public void fixLayer()
	{
		foreach (Transform transform in base.GetComponentsInChildren<Transform>())
		{
			transform.gameObject.layer = 8;
		}
	}

	// Token: 0x04000A4E RID: 2638
	private const int MAP_LAYER = 8;
}
