using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200017B RID: 379
public class RaceGhostButtonTag : MonoBehaviour
{
	// Token: 0x06000879 RID: 2169 RVA: 0x00037D54 File Offset: 0x00035F54
	private void OnEnable()
	{
		base.GetComponent<Button>().interactable = false;
	}
}
