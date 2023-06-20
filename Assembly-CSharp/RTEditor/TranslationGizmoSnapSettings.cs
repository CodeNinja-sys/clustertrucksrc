using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001B7 RID: 439
	[Serializable]
	public class TranslationGizmoSnapSettings
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x00043104 File Offset: 0x00041304
		public static float MinStepValue
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0004310C File Offset: 0x0004130C
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x00043114 File Offset: 0x00041314
		public bool IsStepSnappingEnabled
		{
			get
			{
				return this._isStepSnappingEnabled;
			}
			set
			{
				this._isStepSnappingEnabled = value;
				if (this._isStepSnappingEnabled)
				{
					this._isVertexSnappingEnabled = false;
				}
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00043130 File Offset: 0x00041330
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x00043138 File Offset: 0x00041338
		public bool IsVertexSnappingEnabled
		{
			get
			{
				return this._isVertexSnappingEnabled;
			}
			set
			{
				this._isVertexSnappingEnabled = value;
				if (this._isVertexSnappingEnabled)
				{
					this._isStepSnappingEnabled = false;
				}
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x00043154 File Offset: 0x00041354
		// (set) Token: 0x06000AAC RID: 2732 RVA: 0x0004315C File Offset: 0x0004135C
		public float StepValueInWorldUnits
		{
			get
			{
				return this._stepValueInWorldUnits;
			}
			set
			{
				this._stepValueInWorldUnits = Mathf.Max(TranslationGizmoSnapSettings.MinStepValue, value);
			}
		}

		// Token: 0x0400079E RID: 1950
		private bool _isStepSnappingEnabled;

		// Token: 0x0400079F RID: 1951
		private bool _isVertexSnappingEnabled;

		// Token: 0x040007A0 RID: 1952
		private float _stepValueInWorldUnits = 1f;
	}
}
