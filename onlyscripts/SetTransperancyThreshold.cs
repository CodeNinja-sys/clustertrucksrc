using System;
using UnityEngine;

// Token: 0x02000134 RID: 308
public class SetTransperancyThreshold : MonoBehaviour
{
	// Token: 0x060006AE RID: 1710 RVA: 0x0002CCF4 File Offset: 0x0002AEF4
	private void Start()
	{
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x0002CCF8 File Offset: 0x0002AEF8
	private void Update()
	{
		base.GetComponent<Renderer>().material.SetFloat("_TranThreashold", this.mYposThreashold);
	}

	// Token: 0x040004BB RID: 1211
	[SerializeField]
	private float mYposThreashold = 100f;
}
