using System;
using UnityEngine;

// Token: 0x02000110 RID: 272
[Serializable]
public struct SerializableVector3
{
	// Token: 0x060005DC RID: 1500 RVA: 0x0002A7E4 File Offset: 0x000289E4
	public SerializableVector3(float rX, float rY, float rZ)
	{
		this.x = rX;
		this.y = rY;
		this.z = rZ;
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x0002A7FC File Offset: 0x000289FC
	public override string ToString()
	{
		return string.Format("[{0}, {1}, {2}]", this.x, this.y, this.z);
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x0002A82C File Offset: 0x00028A2C
	public static implicit operator Vector3(SerializableVector3 rValue)
	{
		return new Vector3(rValue.x, rValue.y, rValue.z);
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x0002A848 File Offset: 0x00028A48
	public static implicit operator SerializableVector3(Vector3 rValue)
	{
		return new SerializableVector3(rValue.x, rValue.y, rValue.z);
	}

	// Token: 0x0400044C RID: 1100
	public float x;

	// Token: 0x0400044D RID: 1101
	public float y;

	// Token: 0x0400044E RID: 1102
	public float z;
}
