using System;
using UnityEngine;

// Token: 0x020001A6 RID: 422
public static class Matrix4x4Extensions
{
	// Token: 0x06000981 RID: 2433 RVA: 0x0003B858 File Offset: 0x00039A58
	public static Vector3 GetAxis(this Matrix4x4 matrix, int axisIndex)
	{
		Vector3 result = matrix.GetColumn(axisIndex);
		result.Normalize();
		return result;
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x0003B87C File Offset: 0x00039A7C
	public static Vector3 GetScaleTransform(this Matrix4x4 matrix)
	{
		return new Vector3(matrix.GetColumn(0).magnitude, matrix.GetColumn(1).magnitude, matrix.GetColumn(2).magnitude);
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x0003B8C0 File Offset: 0x00039AC0
	public static Vector3 GetTranslation(this Matrix4x4 matrix)
	{
		return matrix.GetColumn(3);
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x0003B8D0 File Offset: 0x00039AD0
	public static Matrix4x4 SetScaleToOneOnAllAxes(this Matrix4x4 matrix)
	{
		for (int i = 0; i < 3; i++)
		{
			Vector4 column = matrix.GetColumn(i);
			Vector3 vector = column;
			vector.Normalize();
			matrix.SetColumn(i, new Vector4(vector.x, vector.y, vector.z, column.w));
		}
		return matrix;
	}
}
