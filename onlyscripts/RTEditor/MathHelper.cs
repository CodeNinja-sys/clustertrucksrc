using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001BF RID: 447
	public static class MathHelper
	{
		// Token: 0x06000ACD RID: 2765 RVA: 0x00044CBC File Offset: 0x00042EBC
		public static float SafeAcos(float cosine)
		{
			cosine = Mathf.Max(-1f, Mathf.Min(1f, cosine));
			return Mathf.Acos(cosine);
		}
	}
}
