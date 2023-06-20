using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001B2 RID: 434
	[Serializable]
	public class ScaleGizmoSnapSettings
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0004085C File Offset: 0x0003EA5C
		public static float MinStepValue
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x00040864 File Offset: 0x0003EA64
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x0004086C File Offset: 0x0003EA6C
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

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x00040878 File Offset: 0x0003EA78
		// (set) Token: 0x06000A58 RID: 2648 RVA: 0x00040880 File Offset: 0x0003EA80
		public float StepValueInWorldUnits
		{
			get
			{
				return this._stepValueInWorldUnits;
			}
			set
			{
				this._stepValueInWorldUnits = Mathf.Max(ScaleGizmoSnapSettings.MinStepValue, value);
			}
		}

		// Token: 0x04000777 RID: 1911
		[SerializeField]
		private bool _isSnappingEnabled;

		// Token: 0x04000778 RID: 1912
		[SerializeField]
		private float _stepValueInWorldUnits = 1f;
	}
}
