using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x02000187 RID: 391
	[Serializable]
	public class ObjectSelectionBoxDrawSettings
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x000394D0 File Offset: 0x000376D0
		public static float MinSelectionBoxCornerLineLength
		{
			get
			{
				return 0.01f;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x000394D8 File Offset: 0x000376D8
		public static float MinSelectionBoxScaleFactor
		{
			get
			{
				return 0.01f;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x000394E0 File Offset: 0x000376E0
		// (set) Token: 0x060008EB RID: 2283 RVA: 0x000394E8 File Offset: 0x000376E8
		public ObjectSelectionBoxStyle SelectionBoxStyle
		{
			get
			{
				return this._selectionBoxStyle;
			}
			set
			{
				this._selectionBoxStyle = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x000394F4 File Offset: 0x000376F4
		// (set) Token: 0x060008ED RID: 2285 RVA: 0x000394FC File Offset: 0x000376FC
		public float SelectionBoxCornerLineLength
		{
			get
			{
				return this._selectionBoxCornerLineLength;
			}
			set
			{
				this._selectionBoxCornerLineLength = Mathf.Max(ObjectSelectionBoxDrawSettings.MinSelectionBoxCornerLineLength, value);
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x00039510 File Offset: 0x00037710
		// (set) Token: 0x060008EF RID: 2287 RVA: 0x00039518 File Offset: 0x00037718
		public Color SelectionBoxLineColor
		{
			get
			{
				return this._selectionBoxLineColor;
			}
			set
			{
				this._selectionBoxLineColor = value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x00039524 File Offset: 0x00037724
		// (set) Token: 0x060008F1 RID: 2289 RVA: 0x0003952C File Offset: 0x0003772C
		public float SelectionBoxScaleFactor
		{
			get
			{
				return this._selectionBoxScaleFactor;
			}
			set
			{
				this._selectionBoxScaleFactor = Mathf.Max(ObjectSelectionBoxDrawSettings.MinSelectionBoxScaleFactor, value);
			}
		}

		// Token: 0x040006DA RID: 1754
		[SerializeField]
		private ObjectSelectionBoxStyle _selectionBoxStyle;

		// Token: 0x040006DB RID: 1755
		[SerializeField]
		private float _selectionBoxCornerLineLength = 0.4f;

		// Token: 0x040006DC RID: 1756
		[SerializeField]
		private Color _selectionBoxLineColor = new Color(1f, 1f, 1f, 0.53f);

		// Token: 0x040006DD RID: 1757
		[SerializeField]
		private float _selectionBoxScaleFactor = 1.009f;
	}
}
