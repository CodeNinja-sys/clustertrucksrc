using System;
using UnityEngine;

// Token: 0x020002C1 RID: 705
public class timeWatch : AbilityBaseClass
{
	// Token: 0x060010CC RID: 4300 RVA: 0x0006DF04 File Offset: 0x0006C104
	public override string getToolTip()
	{
		return this._toolTip;
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x0006DF0C File Offset: 0x0006C10C
	public override string getToolTipController()
	{
		return this._toolTipController;
	}

	// Token: 0x060010CE RID: 4302 RVA: 0x0006DF14 File Offset: 0x0006C114
	private void Start()
	{
		Time.timeScale = 1f;
	}

	// Token: 0x060010CF RID: 4303 RVA: 0x0006DF20 File Offset: 0x0006C120
	private void Update()
	{
		if (this.isGoing)
		{
			if (Time.timeScale > 0.5f)
			{
				info.playerControlledTime -= Time.unscaledDeltaTime * 2f;
			}
		}
		else if (Time.timeScale < 1f)
		{
			info.playerControlledTime += Time.unscaledDeltaTime * 2f;
		}
		else
		{
			info.playerControlledTime = 1f;
		}
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x0006DF98 File Offset: 0x0006C198
	public override void Stop()
	{
		this.isGoing = false;
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x0006DFA4 File Offset: 0x0006C1A4
	public override void Go()
	{
		this.isGoing = true;
	}

	// Token: 0x04000DF1 RID: 3569
	private bool isGoing;
}
