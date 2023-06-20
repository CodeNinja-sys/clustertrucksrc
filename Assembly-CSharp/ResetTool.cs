using System;
using UnityEngine;

// Token: 0x02000212 RID: 530
public class ResetTool : MonoBehaviour
{
	// Token: 0x06000C74 RID: 3188 RVA: 0x0004D4E8 File Offset: 0x0004B6E8
	private void Awake()
	{
		switch (int.Parse(base.name))
		{
		case 0:
			this._tool = new ResetTool.addTool();
			break;
		case 1:
			this._tool = new ResetTool.moveTool();
			break;
		case 2:
			this._tool = new ResetTool.rotationTool();
			break;
		}
	}

	// Token: 0x06000C75 RID: 3189 RVA: 0x0004D548 File Offset: 0x0004B748
	public void Reset()
	{
		this._tool.reset();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000902 RID: 2306
	private ResetTool.ToolUI _tool;

	// Token: 0x02000213 RID: 531
	private class ToolUI
	{
		// Token: 0x06000C77 RID: 3191 RVA: 0x0004D56C File Offset: 0x0004B76C
		public virtual void reset()
		{
			Debug.Log("Base Tool");
		}

		// Token: 0x04000903 RID: 2307
		protected int _id;
	}

	// Token: 0x02000214 RID: 532
	private class addTool : ResetTool.ToolUI
	{
		// Token: 0x06000C79 RID: 3193 RVA: 0x0004D580 File Offset: 0x0004B780
		public override void reset()
		{
			Debug.Log("addToolReset");
		}
	}

	// Token: 0x02000215 RID: 533
	private class moveTool : ResetTool.ToolUI
	{
		// Token: 0x06000C7B RID: 3195 RVA: 0x0004D594 File Offset: 0x0004B794
		public override void reset()
		{
			Debug.Log("moveToolReset");
		}
	}

	// Token: 0x02000216 RID: 534
	private class rotationTool : ResetTool.ToolUI
	{
		// Token: 0x06000C7D RID: 3197 RVA: 0x0004D5A8 File Offset: 0x0004B7A8
		public override void reset()
		{
			Debug.Log("rotationToolReset");
		}
	}
}
