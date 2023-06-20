using System;
using UnityEngine;

// Token: 0x0200010F RID: 271
[Serializable]
public struct SerializableQuaternion
{
	// Token: 0x060005D8 RID: 1496 RVA: 0x0002A724 File Offset: 0x00028924
	public SerializableQuaternion(float rX, float rY, float rZ, float rW)
	{
		this.x = rX;
		this.y = rY;
		this.z = rZ;
		this.w = rW;
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x0002A744 File Offset: 0x00028944
	public override string ToString()
	{
		return string.Format("[{0}, {1}, {2}, {3}]", new object[]
		{
			this.x,
			this.y,
			this.z,
			this.w
		});
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x0002A79C File Offset: 0x0002899C
	public static implicit operator Quaternion(SerializableQuaternion rValue)
	{
		return new Quaternion(rValue.x, rValue.y, rValue.z, rValue.w);
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x0002A7C0 File Offset: 0x000289C0
	public static implicit operator SerializableQuaternion(Quaternion rValue)
	{
		return new SerializableQuaternion(rValue.x, rValue.y, rValue.z, rValue.w);
	}

	// Token: 0x04000448 RID: 1096
	public float x;

	// Token: 0x04000449 RID: 1097
	public float y;

	// Token: 0x0400044A RID: 1098
	public float z;

	// Token: 0x0400044B RID: 1099
	public float w;
}
