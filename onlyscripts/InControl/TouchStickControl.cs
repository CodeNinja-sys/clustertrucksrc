using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000074 RID: 116
	public class TouchStickControl : TouchControl
	{
		// Token: 0x060003AA RID: 938 RVA: 0x00010DC4 File Offset: 0x0000EFC4
		public override void CreateControl()
		{
			this.ring.Create("Ring", base.transform, 1000);
			this.knob.Create("Knob", base.transform, 1001);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00010E08 File Offset: 0x0000F008
		public override void DestroyControl()
		{
			this.ring.Delete();
			this.knob.Delete();
			if (this.currentTouch != null)
			{
				this.TouchEnded(this.currentTouch);
				this.currentTouch = null;
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00010E4C File Offset: 0x0000F04C
		public override void ConfigureControl()
		{
			this.resetPosition = base.OffsetToWorldPosition(this.anchor, this.offset, this.offsetUnitType, true);
			base.transform.position = this.resetPosition;
			this.ring.Update(true);
			this.knob.Update(true);
			this.worldActiveArea = TouchManager.ConvertToWorld(this.activeArea, this.areaUnitType);
			this.worldKnobRange = TouchManager.ConvertToWorld(this.knobRange, this.knob.SizeUnitType);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00010ED4 File Offset: 0x0000F0D4
		public override void DrawGizmos()
		{
			this.ring.DrawGizmos(this.RingPosition, Color.yellow);
			this.knob.DrawGizmos(this.KnobPosition, Color.yellow);
			Utility.DrawCircleGizmo(this.RingPosition, this.worldKnobRange, Color.red);
			Utility.DrawRectGizmo(this.worldActiveArea, Color.green);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00010F38 File Offset: 0x0000F138
		private void Update()
		{
			if (this.dirty)
			{
				this.ConfigureControl();
				this.dirty = false;
			}
			else
			{
				this.ring.Update();
				this.knob.Update();
			}
			if (this.IsNotActive)
			{
				if (this.resetWhenDone && this.KnobPosition != this.resetPosition)
				{
					Vector3 b = this.KnobPosition - this.RingPosition;
					this.RingPosition = Vector3.MoveTowards(this.RingPosition, this.resetPosition, this.ringResetSpeed * Time.deltaTime);
					this.KnobPosition = this.RingPosition + b;
				}
				if (this.KnobPosition != this.RingPosition)
				{
					this.KnobPosition = Vector3.MoveTowards(this.KnobPosition, this.RingPosition, this.knobResetSpeed * Time.deltaTime);
				}
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00011024 File Offset: 0x0000F224
		public override void SubmitControlState(ulong updateTick, float deltaTime)
		{
			base.SubmitAnalogValue(this.target, this.snappedValue, this.lowerDeadZone, this.upperDeadZone, updateTick, deltaTime);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00011058 File Offset: 0x0000F258
		public override void CommitControlState(ulong updateTick, float deltaTime)
		{
			base.CommitAnalog(this.target);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00011068 File Offset: 0x0000F268
		public override void TouchBegan(Touch touch)
		{
			if (this.IsActive)
			{
				return;
			}
			this.beganPosition = TouchManager.ScreenToWorldPoint(touch.position);
			bool flag = this.worldActiveArea.Contains(this.beganPosition);
			bool flag2 = this.ring.Contains(this.beganPosition);
			if (this.snapToInitialTouch && (flag || flag2))
			{
				this.RingPosition = this.beganPosition;
				this.KnobPosition = this.beganPosition;
				this.currentTouch = touch;
			}
			else if (flag2)
			{
				this.KnobPosition = this.beganPosition;
				this.beganPosition = this.RingPosition;
				this.currentTouch = touch;
			}
			if (this.IsActive)
			{
				this.TouchMoved(touch);
				this.ring.State = true;
				this.knob.State = true;
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00011148 File Offset: 0x0000F348
		public override void TouchMoved(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			this.movedPosition = TouchManager.ScreenToWorldPoint(touch.position);
			Vector3 vector = this.movedPosition - this.beganPosition;
			Vector3 normalized = vector.normalized;
			float magnitude = vector.magnitude;
			if (this.allowDragging)
			{
				float num = magnitude - this.worldKnobRange;
				if (num < 0f)
				{
					num = 0f;
				}
				this.beganPosition += num * normalized;
				this.RingPosition = this.beganPosition;
			}
			this.movedPosition = this.beganPosition + Mathf.Clamp(magnitude, 0f, this.worldKnobRange) * normalized;
			this.value = (this.movedPosition - this.beganPosition) / this.worldKnobRange;
			this.value.x = this.inputCurve.Evaluate(Utility.Abs(this.value.x)) * Mathf.Sign(this.value.x);
			this.value.y = this.inputCurve.Evaluate(Utility.Abs(this.value.y)) * Mathf.Sign(this.value.y);
			if (this.snapAngles == TouchControl.SnapAngles.None)
			{
				this.snappedValue = this.value;
				this.KnobPosition = this.movedPosition;
			}
			else
			{
				this.snappedValue = TouchControl.SnapTo(this.value, this.snapAngles);
				this.KnobPosition = this.beganPosition + this.snappedValue * this.worldKnobRange;
			}
			this.RingPosition = this.beganPosition;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00011310 File Offset: 0x0000F510
		public override void TouchEnded(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			this.value = Vector3.zero;
			this.snappedValue = Vector3.zero;
			float magnitude = (this.resetPosition - this.RingPosition).magnitude;
			this.ringResetSpeed = ((!Utility.IsZero(this.resetDuration)) ? (magnitude / this.resetDuration) : magnitude);
			float magnitude2 = (this.RingPosition - this.KnobPosition).magnitude;
			this.knobResetSpeed = ((!Utility.IsZero(this.resetDuration)) ? (magnitude2 / this.resetDuration) : this.knobRange);
			this.currentTouch = null;
			this.ring.State = false;
			this.knob.State = false;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x000113E0 File Offset: 0x0000F5E0
		public bool IsActive
		{
			get
			{
				return this.currentTouch != null;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x000113F0 File Offset: 0x0000F5F0
		public bool IsNotActive
		{
			get
			{
				return this.currentTouch == null;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x000113FC File Offset: 0x0000F5FC
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x00011434 File Offset: 0x0000F634
		public Vector3 RingPosition
		{
			get
			{
				return (!this.ring.Ready) ? base.transform.position : this.ring.Position;
			}
			set
			{
				if (this.ring.Ready)
				{
					this.ring.Position = value;
				}
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00011454 File Offset: 0x0000F654
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0001148C File Offset: 0x0000F68C
		public Vector3 KnobPosition
		{
			get
			{
				return (!this.knob.Ready) ? base.transform.position : this.knob.Position;
			}
			set
			{
				if (this.knob.Ready)
				{
					this.knob.Position = value;
				}
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003BA RID: 954 RVA: 0x000114AC File Offset: 0x0000F6AC
		// (set) Token: 0x060003BB RID: 955 RVA: 0x000114B4 File Offset: 0x0000F6B4
		public TouchControlAnchor Anchor
		{
			get
			{
				return this.anchor;
			}
			set
			{
				if (this.anchor != value)
				{
					this.anchor = value;
					this.dirty = true;
				}
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003BC RID: 956 RVA: 0x000114D0 File Offset: 0x0000F6D0
		// (set) Token: 0x060003BD RID: 957 RVA: 0x000114D8 File Offset: 0x0000F6D8
		public Vector2 Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				if (this.offset != value)
				{
					this.offset = value;
					this.dirty = true;
				}
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003BE RID: 958 RVA: 0x000114FC File Offset: 0x0000F6FC
		// (set) Token: 0x060003BF RID: 959 RVA: 0x00011504 File Offset: 0x0000F704
		public TouchUnitType OffsetUnitType
		{
			get
			{
				return this.offsetUnitType;
			}
			set
			{
				if (this.offsetUnitType != value)
				{
					this.offsetUnitType = value;
					this.dirty = true;
				}
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00011520 File Offset: 0x0000F720
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x00011528 File Offset: 0x0000F728
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

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0001154C File Offset: 0x0000F74C
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x00011554 File Offset: 0x0000F754
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

		// Token: 0x040002E7 RID: 743
		[Header("Position")]
		[SerializeField]
		private TouchControlAnchor anchor = TouchControlAnchor.BottomLeft;

		// Token: 0x040002E8 RID: 744
		[SerializeField]
		private TouchUnitType offsetUnitType;

		// Token: 0x040002E9 RID: 745
		[SerializeField]
		private Vector2 offset = new Vector2(20f, 20f);

		// Token: 0x040002EA RID: 746
		[SerializeField]
		private TouchUnitType areaUnitType;

		// Token: 0x040002EB RID: 747
		[SerializeField]
		private Rect activeArea = new Rect(0f, 0f, 50f, 100f);

		// Token: 0x040002EC RID: 748
		[Header("Options")]
		public TouchControl.AnalogTarget target = TouchControl.AnalogTarget.LeftStick;

		// Token: 0x040002ED RID: 749
		public TouchControl.SnapAngles snapAngles;

		// Token: 0x040002EE RID: 750
		[Range(0f, 1f)]
		public float lowerDeadZone = 0.1f;

		// Token: 0x040002EF RID: 751
		[Range(0f, 1f)]
		public float upperDeadZone = 0.9f;

		// Token: 0x040002F0 RID: 752
		public AnimationCurve inputCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

		// Token: 0x040002F1 RID: 753
		public bool allowDragging;

		// Token: 0x040002F2 RID: 754
		public bool snapToInitialTouch = true;

		// Token: 0x040002F3 RID: 755
		public bool resetWhenDone = true;

		// Token: 0x040002F4 RID: 756
		public float resetDuration = 0.1f;

		// Token: 0x040002F5 RID: 757
		[Header("Sprites")]
		public TouchSprite ring = new TouchSprite(20f);

		// Token: 0x040002F6 RID: 758
		public TouchSprite knob = new TouchSprite(10f);

		// Token: 0x040002F7 RID: 759
		public float knobRange = 7.5f;

		// Token: 0x040002F8 RID: 760
		private Vector3 resetPosition;

		// Token: 0x040002F9 RID: 761
		private Vector3 beganPosition;

		// Token: 0x040002FA RID: 762
		private Vector3 movedPosition;

		// Token: 0x040002FB RID: 763
		private float ringResetSpeed;

		// Token: 0x040002FC RID: 764
		private float knobResetSpeed;

		// Token: 0x040002FD RID: 765
		private Rect worldActiveArea;

		// Token: 0x040002FE RID: 766
		private float worldKnobRange;

		// Token: 0x040002FF RID: 767
		private Vector3 value;

		// Token: 0x04000300 RID: 768
		private Vector3 snappedValue;

		// Token: 0x04000301 RID: 769
		private Touch currentTouch;

		// Token: 0x04000302 RID: 770
		private bool dirty;
	}
}
