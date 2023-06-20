using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000075 RID: 117
	public class TouchSwipeControl : TouchControl
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x000115B0 File Offset: 0x0000F7B0
		public override void CreateControl()
		{
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000115B4 File Offset: 0x0000F7B4
		public override void DestroyControl()
		{
			if (this.currentTouch != null)
			{
				this.TouchEnded(this.currentTouch);
				this.currentTouch = null;
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000115D4 File Offset: 0x0000F7D4
		public override void ConfigureControl()
		{
			this.worldActiveArea = TouchManager.ConvertToWorld(this.activeArea, this.areaUnitType);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000115F0 File Offset: 0x0000F7F0
		public override void DrawGizmos()
		{
			Utility.DrawRectGizmo(this.worldActiveArea, Color.yellow);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00011604 File Offset: 0x0000F804
		private void Update()
		{
			if (this.dirty)
			{
				this.ConfigureControl();
				this.dirty = false;
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00011620 File Offset: 0x0000F820
		public override void SubmitControlState(ulong updateTick, float deltaTime)
		{
			Vector2 value = TouchControl.SnapTo(this.currentVector, this.snapAngles);
			base.SubmitAnalogValue(this.target, value, 0f, 1f, updateTick, deltaTime);
			base.SubmitButtonState(this.upTarget, this.fireButtonTarget && this.nextButtonTarget == this.upTarget, updateTick, deltaTime);
			base.SubmitButtonState(this.downTarget, this.fireButtonTarget && this.nextButtonTarget == this.downTarget, updateTick, deltaTime);
			base.SubmitButtonState(this.leftTarget, this.fireButtonTarget && this.nextButtonTarget == this.leftTarget, updateTick, deltaTime);
			base.SubmitButtonState(this.rightTarget, this.fireButtonTarget && this.nextButtonTarget == this.rightTarget, updateTick, deltaTime);
			base.SubmitButtonState(this.tapTarget, this.fireButtonTarget && this.nextButtonTarget == this.tapTarget, updateTick, deltaTime);
			if (this.fireButtonTarget && this.nextButtonTarget != TouchControl.ButtonTarget.None)
			{
				this.fireButtonTarget = !this.oneSwipePerTouch;
				this.lastButtonTarget = this.nextButtonTarget;
				this.nextButtonTarget = TouchControl.ButtonTarget.None;
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00011768 File Offset: 0x0000F968
		public override void CommitControlState(ulong updateTick, float deltaTime)
		{
			base.CommitAnalog(this.target);
			base.CommitButton(this.upTarget);
			base.CommitButton(this.downTarget);
			base.CommitButton(this.leftTarget);
			base.CommitButton(this.rightTarget);
			base.CommitButton(this.tapTarget);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000117C0 File Offset: 0x0000F9C0
		public override void TouchBegan(Touch touch)
		{
			if (this.currentTouch != null)
			{
				return;
			}
			this.beganPosition = TouchManager.ScreenToWorldPoint(touch.position);
			if (this.worldActiveArea.Contains(this.beganPosition))
			{
				this.lastPosition = this.beganPosition;
				this.currentTouch = touch;
				this.currentVector = Vector2.zero;
				this.fireButtonTarget = true;
				this.nextButtonTarget = TouchControl.ButtonTarget.None;
				this.lastButtonTarget = TouchControl.ButtonTarget.None;
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00011838 File Offset: 0x0000FA38
		public override void TouchMoved(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			Vector3 a = TouchManager.ScreenToWorldPoint(touch.position);
			Vector3 vector = a - this.lastPosition;
			if (vector.magnitude >= this.sensitivity)
			{
				this.lastPosition = a;
				this.currentVector = vector.normalized;
				if (this.fireButtonTarget)
				{
					TouchControl.ButtonTarget buttonTargetForVector = this.GetButtonTargetForVector(this.currentVector);
					if (buttonTargetForVector != this.lastButtonTarget)
					{
						this.nextButtonTarget = buttonTargetForVector;
					}
				}
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000118C4 File Offset: 0x0000FAC4
		public override void TouchEnded(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			this.currentTouch = null;
			this.currentVector = Vector2.zero;
			Vector3 b = TouchManager.ScreenToWorldPoint(touch.position);
			if ((this.beganPosition - b).magnitude < this.sensitivity)
			{
				this.fireButtonTarget = true;
				this.nextButtonTarget = this.tapTarget;
				this.lastButtonTarget = TouchControl.ButtonTarget.None;
				return;
			}
			this.fireButtonTarget = false;
			this.nextButtonTarget = TouchControl.ButtonTarget.None;
			this.lastButtonTarget = TouchControl.ButtonTarget.None;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00011950 File Offset: 0x0000FB50
		private TouchControl.ButtonTarget GetButtonTargetForVector(Vector2 vector)
		{
			Vector2 lhs = TouchControl.SnapTo(vector, TouchControl.SnapAngles.Four);
			if (lhs == Vector2.up)
			{
				return this.upTarget;
			}
			if (lhs == Vector2.right)
			{
				return this.rightTarget;
			}
			if (lhs == -Vector2.up)
			{
				return this.downTarget;
			}
			if (lhs == -Vector2.right)
			{
				return this.leftTarget;
			}
			return TouchControl.ButtonTarget.None;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x000119CC File Offset: 0x0000FBCC
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x000119D4 File Offset: 0x0000FBD4
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

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x000119F8 File Offset: 0x0000FBF8
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00011A00 File Offset: 0x0000FC00
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

		// Token: 0x04000303 RID: 771
		[SerializeField]
		[Header("Position")]
		private TouchUnitType areaUnitType;

		// Token: 0x04000304 RID: 772
		[SerializeField]
		private Rect activeArea = new Rect(25f, 25f, 50f, 50f);

		// Token: 0x04000305 RID: 773
		[Range(0f, 1f)]
		public float sensitivity = 0.1f;

		// Token: 0x04000306 RID: 774
		[Header("Analog Target")]
		public TouchControl.AnalogTarget target;

		// Token: 0x04000307 RID: 775
		public TouchControl.SnapAngles snapAngles;

		// Token: 0x04000308 RID: 776
		[Header("Button Targets")]
		public TouchControl.ButtonTarget upTarget;

		// Token: 0x04000309 RID: 777
		public TouchControl.ButtonTarget downTarget;

		// Token: 0x0400030A RID: 778
		public TouchControl.ButtonTarget leftTarget;

		// Token: 0x0400030B RID: 779
		public TouchControl.ButtonTarget rightTarget;

		// Token: 0x0400030C RID: 780
		public TouchControl.ButtonTarget tapTarget;

		// Token: 0x0400030D RID: 781
		public bool oneSwipePerTouch;

		// Token: 0x0400030E RID: 782
		private Rect worldActiveArea;

		// Token: 0x0400030F RID: 783
		private Vector3 currentVector;

		// Token: 0x04000310 RID: 784
		private Vector3 beganPosition;

		// Token: 0x04000311 RID: 785
		private Vector3 lastPosition;

		// Token: 0x04000312 RID: 786
		private Touch currentTouch;

		// Token: 0x04000313 RID: 787
		private bool fireButtonTarget;

		// Token: 0x04000314 RID: 788
		private TouchControl.ButtonTarget nextButtonTarget;

		// Token: 0x04000315 RID: 789
		private TouchControl.ButtonTarget lastButtonTarget;

		// Token: 0x04000316 RID: 790
		private bool dirty;
	}
}
