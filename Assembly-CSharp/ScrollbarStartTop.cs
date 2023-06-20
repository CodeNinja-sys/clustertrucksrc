using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002C8 RID: 712
public class ScrollbarStartTop : MonoBehaviour
{
	// Token: 0x060010ED RID: 4333 RVA: 0x0006E458 File Offset: 0x0006C658
	private void OnEnable()
	{
		base.StartCoroutine(this.Start());
	}

	// Token: 0x060010EE RID: 4334 RVA: 0x0006E468 File Offset: 0x0006C668
	public IEnumerator Start()
	{
		yield return null;
		this._bar = base.GetComponentInChildren<Scrollbar>();
		this._bar.value = 1f;
		yield break;
	}

	// Token: 0x04000E0C RID: 3596
	private Scrollbar _bar;
}
