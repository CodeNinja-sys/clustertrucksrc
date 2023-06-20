using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000301 RID: 769
public class setTextColor : MonoBehaviour
{
	// Token: 0x0600121A RID: 4634 RVA: 0x000737F4 File Offset: 0x000719F4
	private void Start()
	{
	}

	// Token: 0x0600121B RID: 4635 RVA: 0x000737F8 File Offset: 0x000719F8
	private void Update()
	{
		if (this.t.text == this.i.ToString())
		{
			this.t.color = this.ifColor;
		}
		else
		{
			this.t.color = this.elseColor;
		}
	}

	// Token: 0x04000F3F RID: 3903
	public Text t;

	// Token: 0x04000F40 RID: 3904
	public int i;

	// Token: 0x04000F41 RID: 3905
	public Color ifColor;

	// Token: 0x04000F42 RID: 3906
	public Color elseColor;
}
