using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x0200018E RID: 398
	public abstract class ObjectSelectionShape
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x000397B4 File Offset: 0x000379B4
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x000397BC File Offset: 0x000379BC
		public bool IsVisible
		{
			get
			{
				return this._isVisible;
			}
			set
			{
				this._isVisible = value;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (set) Token: 0x06000903 RID: 2307 RVA: 0x000397C8 File Offset: 0x000379C8
		public Material BorderLineMaterial
		{
			set
			{
				this._borderLineMaterial = value;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x000397D4 File Offset: 0x000379D4
		public Material FillMaterial
		{
			set
			{
				this._fillMaterial = value;
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x000397E0 File Offset: 0x000379E0
		public void SetEnclosingRectTopLeftPoint(Vector2 topLeftPoint)
		{
			this._enclosingRectangle.xMin = topLeftPoint.x;
			this._enclosingRectangle.yMax = topLeftPoint.y;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00039814 File Offset: 0x00037A14
		public void SetEnclosingRectBottomRightPoint(Vector2 bottomRightPoint)
		{
			this._enclosingRectangle.xMax = bottomRightPoint.x;
			this._enclosingRectangle.yMin = bottomRightPoint.y;
		}

		// Token: 0x06000907 RID: 2311
		public abstract void Draw();

		// Token: 0x06000908 RID: 2312
		public abstract List<GameObject> GetIntersectingGameObjects(List<GameObject> gameObjects, Camera camera);

		// Token: 0x06000909 RID: 2313 RVA: 0x00039848 File Offset: 0x00037A48
		protected bool IsEnclosingRectangleBigEnoughForSelection()
		{
			return Mathf.Abs(this._enclosingRectangle.width) > 2f && Mathf.Abs(this._enclosingRectangle.height) > 2f;
		}

		// Token: 0x040006E4 RID: 1764
		protected bool _isVisible;

		// Token: 0x040006E5 RID: 1765
		protected Rect _enclosingRectangle;

		// Token: 0x040006E6 RID: 1766
		protected Material _borderLineMaterial;

		// Token: 0x040006E7 RID: 1767
		protected Material _fillMaterial;
	}
}
