using System;
using UnityEngine;

// Token: 0x0200028D RID: 653
public class lineRendererIndex : MonoBehaviour
{
	// Token: 0x06000FAA RID: 4010 RVA: 0x00065B04 File Offset: 0x00063D04
	private void Start()
	{
	}

	// Token: 0x06000FAB RID: 4011 RVA: 0x00065B08 File Offset: 0x00063D08
	private void Update()
	{
	}

	// Token: 0x06000FAC RID: 4012 RVA: 0x00065B0C File Offset: 0x00063D0C
	public void setType(int type)
	{
		this.m_type = type;
		Debug.Log("Index set to: " + this.m_type);
	}

	// Token: 0x06000FAD RID: 4013 RVA: 0x00065B30 File Offset: 0x00063D30
	public int getType()
	{
		if (this.m_type < 0)
		{
			throw new Exception("Index not set!");
		}
		return this.m_type;
	}

	// Token: 0x04000C89 RID: 3209
	private int m_type;
}
