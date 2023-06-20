using System;
using UnityEngine;

// Token: 0x020001E2 RID: 482
public class BlockInfo : MonoBehaviour
{
	// Token: 0x1700023C RID: 572
	// (get) Token: 0x06000B5E RID: 2910 RVA: 0x00046DA4 File Offset: 0x00044FA4
	// (set) Token: 0x06000B5F RID: 2911 RVA: 0x00046DAC File Offset: 0x00044FAC
	public string Category
	{
		get
		{
			return this._category;
		}
		set
		{
			this.SetBlockCategory(value);
		}
	}

	// Token: 0x1700023D RID: 573
	// (get) Token: 0x06000B60 RID: 2912 RVA: 0x00046DB8 File Offset: 0x00044FB8
	public int Layer
	{
		get
		{
			return this._layer;
		}
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x00046DC0 File Offset: 0x00044FC0
	private void SetBlockCategory(string category)
	{
		this._category = category;
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x00046DCC File Offset: 0x00044FCC
	private void Awake()
	{
		if (this.overrideCategory != "HELLO")
		{
			this.SetBlockCategory(this.overrideCategory);
		}
	}

	// Token: 0x04000811 RID: 2065
	public string overrideCategory = "HELLO";

	// Token: 0x04000812 RID: 2066
	private string _category = string.Empty;

	// Token: 0x04000813 RID: 2067
	private int _layer = 1;
}
