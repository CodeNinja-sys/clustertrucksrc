using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000076 RID: 118
	public class TouchTrackControl : TouchControl
	{
		// Token: 0x060003D5 RID: 981 RVA: 0x00011A58 File Offset: 0x0000FC58
		public override void CreateControl()
		{
			this.ConfigureControl();
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00011A60 File Offset: 0x0000FC60
		public override void DestroyControl()
		{
			if (this.currentTouch != null)
			{
				this.TouchEnded(this.currentTouch);
				this.currentTouch = null;
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00011A80 File Offset: 0x0000FC80
		public override void ConfigureControl()
		{
			this.worldActiveArea = TouchManager.ConvertToWorld(this.activeArea, this.areaUnitType);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00011A9C File Offset: 0x0000FC9C
		public override void DrawGizmos()
		{
			Utility.DrawRectGizmo(this.worldActiveArea, Color.yellow);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00011AB0 File Offset: 0x0000FCB0
		private void Update()
		{
			if (this.dirty)
			{
				this.ConfigureControl();
				this.dirty = false;
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00011ACC File Offset: 0x0000FCCC
		public override void SubmitControlState(ulong updateTick, float deltaTime)
		{
			Vector3 a = this.thisPosition - this.lastPosition;
			base.SubmitRawAnalogValue(this.target, a * this.scale, updateTick, deltaTime);
			this.lastPosition = this.thisPosition;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00011B18 File Offset: 0x0000FD18
		public override void CommitControlState(ulong updateTick, float deltaTime)
		{
			base.CommitAnalog(this.target);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00011B28 File Offset: 0x0000FD28
		public override void TouchBegan(Touch touch)
		{
			if (this.currentTouch != null)
			{
				return;
			}
			Vector3 point = TouchManager.ScreenToWorldPoint(touch.position);
			if (this.worldActiveArea.Contains(point))
			{
				this.thisPosition = TouchManager.ScreenToViewPoint(touch.position * 100f);
				this.lastPosition = this.thisPosition;
				this.currentTouch = touch;
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00011B8C File Offset: 0x0000FD8C
		public override void TouchMoved(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			this.thisPosition = TouchManager.ScreenToViewPoint(touch.position * 100f);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00011BC4 File Offset: 0x0000FDC4
		public override void TouchEnded(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			this.thisPosition = Vector3.zero;
			this.lastPosition = Vector3.zero;
			this.currentTouch = null;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00011BFC File Offset: 0x0000FDFC
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x00011C04 File Offset: 0x0000FE04
		public Rect ActiveArea
		{
			get
			{
				return this.activeArea;
			}
			set
			{
				if (this.activeArea != value)
				{
					this.activeArea = value;
					this.dirty = true;
				}
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00011C28 File Offset: 0x0000FE28
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x00011C30 File Offset: 0x0000FE30
		public TouchUnitType AreaUnitType
		{
			get
			{
				return this.areaUnitType;
			}
			set
			{
				if (this.areaUnitType != value)
				{
					this.areaUnitType = value;
					this.dirty = true;
				}
			}
		}

		// Token: 0x04000317 RID: 791
		[Header("Dimensions")]
		[SerializeField]
		private TouchUnitType areaUnitType;

		// Token: 0x04000318 RID: 792
		[SerializeField]
		private Rect activeArea = new Rect(25f, 25f, 50f, 50f);

		// Token: 0x04000319 RID: 793
		[Header("Analog Target")]
		public TouchControl.AnalogTarget target = TouchControl.AnalogTarget.LeftStick;

		// Token: 0x0400031A RID: 794
		public float scale = 1f;

		// Token: 0x0400031B RID: 795
		private Rect worldActiveArea;

		// Token: 0x0400031C RID: 796
		private Vector3 lastPosition;

		// Token: 0x0400031D RID: 797
		private Vector3 thisPosition;

		// Token: 0x0400031E RID: 798
		private Touch currentTouch;

		// Token: 0x0400031F RID: 799
		private bool dirty;
	}
}
