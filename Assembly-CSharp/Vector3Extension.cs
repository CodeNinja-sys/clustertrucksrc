using System;
using UnityEngine;

// Token: 0x02000307 RID: 775
public static class Vector3Extension
{
	// Token: 0x0600122F RID: 4655 RVA: 0x00073FB0 File Offset: 0x000721B0
	public static Vector3 WithX(this Vector3 v, float x)
	{
		return new Vector3(x, v.y, v.z);
	}

	// Token: 0x06001230 RID: 4656 RVA: 0x00073FC8 File Offset: 0x000721C8
	public static Vector3 WithY(this Vector3 v, float y)
	{
		return new Vector3(v.x, y, v.z);
	}

	// Token: 0x06001231 RID: 4657 RVA: 0x00073FE0 File Offset: 0x000721E0
	public static Vector3 WithZ(this Vector3 v, float z)
	{
		return new Vector3(v.x, v.y, z);
	}
}
