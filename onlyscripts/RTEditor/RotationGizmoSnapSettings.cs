using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001B0 RID: 432
	[Serializable]
	public class RotationGizmoSnapSettings
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0003E2EC File Offset: 0x0003C4EC
		public static float MinStepValue
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		// (set) Token: 0x060009EF RID: 2543 RVA: 0x0003E2FC File Offset: 0x0003C4FC
		public bool IsSnappingEnabled
		{
			get
			{
				return this._isSnappingEnabled;
			}
			set
			{
				this._isSnappingEnabled = value;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0003E308 File Offset: 0x0003C508
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x0003E310 File Offset: 0x0003C510
		public float StepValueInDegrees
		{
			get
			{
				return this._stepValueInDegrees;
			}
			set
			{
				this._stepValueInDegrees = Mathf.Max(RotationGizmoSnapSettings.MinStepValue, value);
			}
		}

		// Token: 0x04000751 RID: 1873
		[SerializeField]
		private bool _isSnappingEnabled;

		// Token: 0x04000752 RID: 1874
		[SerializeField]
		private float _stepValueInDegrees = 15f;
	}
}
