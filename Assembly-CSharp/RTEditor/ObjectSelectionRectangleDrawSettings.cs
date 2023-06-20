using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x0200018D RID: 397
	[Serializable]
	public class ObjectSelectionRectangleDrawSettings
	{
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x00039784 File Offset: 0x00037984
		// (set) Token: 0x060008FD RID: 2301 RVA: 0x0003978C File Offset: 0x0003798C
		public Color BorderLineColor
		{
			get
			{
				return this._borderLineColor;
			}
			set
			{
				this._borderLineColor = value;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00039798 File Offset: 0x00037998
		// (set) Token: 0x060008FF RID: 2303 RVA: 0x000397A0 File Offset: 0x000379A0
		public Color FillColor
		{
			get
			{
				return this._fillColor;
			}
			set
			{
				this._fillColor = value;
			}
		}

		// Token: 0x040006E2 RID: 1762
		[SerializeField]
		private Color _borderLineColor = Color.green;

		// Token: 0x040006E3 RID: 1763
		[SerializeField]
		private Color _fillColor = new Color(0f, 1f, 0f, 0.1f);
	}
}
