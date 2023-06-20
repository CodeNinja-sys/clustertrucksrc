using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001BB RID: 443
	public static class ApplicationHelper
	{
		// Token: 0x06000AB7 RID: 2743 RVA: 0x000435A0 File Offset: 0x000417A0
		public static void Quit()
		{
			if (Application.isEditor)
			{
				Debug.Break();
			}
			else
			{
				Application.Quit();
			}
		}
	}
}
