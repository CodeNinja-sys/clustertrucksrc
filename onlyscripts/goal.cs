using System;
using UnityEngine;

// Token: 0x0200027F RID: 639
public class goal : MonoBehaviour
{
	// Token: 0x06000F5E RID: 3934 RVA: 0x00063F64 File Offset: 0x00062164
	private void Awake()
	{
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x00063F68 File Offset: 0x00062168
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			UnityEngine.Object.FindObjectOfType<GameManager>().WinLevel();
		}
	}
}
