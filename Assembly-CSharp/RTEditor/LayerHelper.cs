using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001BE RID: 446
	public static class LayerHelper
	{
		// Token: 0x06000ACC RID: 2764 RVA: 0x00044C7C File Offset: 0x00042E7C
		public static List<string> GetAllLayerNames()
		{
			List<string> list = new List<string>();
			for (int i = 0; i < 31; i++)
			{
				string text = LayerMask.LayerToName(i);
				if (!string.IsNullOrEmpty(text))
				{
					list.Add(text);
				}
			}
			return list;
		}
	}
}
