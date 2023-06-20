using System;
using UnityEngine;

// Token: 0x0200017E RID: 382
public class RemoveTrucks : MonoBehaviour
{
	// Token: 0x06000881 RID: 2177 RVA: 0x00037DF4 File Offset: 0x00035FF4
	private void Start()
	{
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x00037DF8 File Offset: 0x00035FF8
	private void Update()
	{
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x00037DFC File Offset: 0x00035FFC
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "car")
		{
			other.transform.root.GetComponent<car>().RemoveTruck();
		}
	}
}
