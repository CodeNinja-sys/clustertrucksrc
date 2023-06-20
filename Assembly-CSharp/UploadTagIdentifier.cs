using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002E8 RID: 744
public class UploadTagIdentifier : MonoBehaviour
{
	// Token: 0x06001170 RID: 4464 RVA: 0x00070DF0 File Offset: 0x0006EFF0
	private void OnDisable()
	{
		base.GetComponent<Button>().image.color = Color.white;
	}
}
