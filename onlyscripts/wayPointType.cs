using System;
using UnityEngine;

// Token: 0x020002C4 RID: 708
public class wayPointType : MonoBehaviour
{
	// Token: 0x060010DA RID: 4314 RVA: 0x0006E184 File Offset: 0x0006C384
	private void Start()
	{
	}

	// Token: 0x060010DB RID: 4315 RVA: 0x0006E188 File Offset: 0x0006C388
	private void Update()
	{
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x0006E18C File Offset: 0x0006C38C
	public void setType(int type, bool play)
	{
		if (type == 0)
		{
			this.m_type = 1;
		}
		else
		{
			this.m_type = type;
		}
		if (play)
		{
			return;
		}
		levelEditorManager.Instance().getCurrentMap.getTileAt(base.transform).waypointType = this.m_type;
		base.GetComponent<Renderer>().material.color = levelEditorManager.Instance().colors[this.m_type - 1];
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x0006E208 File Offset: 0x0006C408
	public int getType()
	{
		if (this.m_type == -1)
		{
			Debug.LogError("Type not set! Making default");
			this.m_type = -2;
		}
		return this.m_type;
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x0006E23C File Offset: 0x0006C43C
	public void sendInfo(getModifierOptions.Parameter[] _params)
	{
		this.m_type = int.Parse(_params[0].getValue());
	}

	// Token: 0x04000E04 RID: 3588
	[SerializeField]
	private int m_type = -1;
}
