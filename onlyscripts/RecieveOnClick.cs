using System;
using UnityEngine;

// Token: 0x02000211 RID: 529
public class RecieveOnClick : MonoBehaviour
{
	// Token: 0x06000C72 RID: 3186 RVA: 0x0004D4C4 File Offset: 0x0004B6C4
	public void Clicked()
	{
		if (base.gameObject.activeInHierarchy)
		{
			TutorialHandler.Instance.nextSlide();
		}
	}
}
