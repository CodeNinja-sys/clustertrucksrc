using System;

// Token: 0x02000032 RID: 50
public struct HsvColor
{
	// Token: 0x0600011F RID: 287 RVA: 0x00008050 File Offset: 0x00006250
	public HsvColor(double h, double s, double v)
	{
		this.H = h;
		this.S = s;
		this.V = v;
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000120 RID: 288 RVA: 0x00008068 File Offset: 0x00006268
	// (set) Token: 0x06000121 RID: 289 RVA: 0x00008078 File Offset: 0x00006278
	public float normalizedH
	{
		get
		{
			return (float)this.H / 360f;
		}
		set
		{
			this.H = (double)value * 360.0;
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x06000122 RID: 290 RVA: 0x0000808C File Offset: 0x0000628C
	// (set) Token: 0x06000123 RID: 291 RVA: 0x00008098 File Offset: 0x00006298
	public float normalizedS
	{
		get
		{
			return (float)this.S;
		}
		set
		{
			this.S = (double)value;
		}
	}

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x06000124 RID: 292 RVA: 0x000080A4 File Offset: 0x000062A4
	// (set) Token: 0x06000125 RID: 293 RVA: 0x000080B0 File Offset: 0x000062B0
	public float normalizedV
	{
		get
		{
			return (float)this.V;
		}
		set
		{
			this.V = (double)value;
		}
	}

	// Token: 0x06000126 RID: 294 RVA: 0x000080BC File Offset: 0x000062BC
	public override string ToString()
	{
		return string.Concat(new string[]
		{
			"{",
			this.H.ToString("f2"),
			",",
			this.S.ToString("f2"),
			",",
			this.V.ToString("f2"),
			"}"
		});
	}

	// Token: 0x040000D8 RID: 216
	public double H;

	// Token: 0x040000D9 RID: 217
	public double S;

	// Token: 0x040000DA RID: 218
	public double V;
}
