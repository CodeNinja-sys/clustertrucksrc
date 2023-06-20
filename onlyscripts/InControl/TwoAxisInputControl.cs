using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000069 RID: 105
	public class TwoAxisInputControl : IInputControl
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x0000E360 File Offset: 0x0000C560
		public TwoAxisInputControl()
		{
			this.Left = new OneAxisInputControl();
			this.Right = new OneAxisInputControl();
			this.Up = new OneAxisInputControl();
			this.Down = new OneAxisInputControl();
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000E3AC File Offset: 0x0000C5AC
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0000E3B4 File Offset: 0x0000C5B4
		public float X { get; protected set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000E3C0 File Offset: 0x0000C5C0
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0000E3C8 File Offset: 0x0000C5C8
		public float Y { get; protected set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000E3D4 File Offset: 0x0000C5D4
		// (set) Token: 0x060002AD RID: 685 RVA: 0x0000E3DC File Offset: 0x0000C5DC
		public OneAxisInputControl Left { get; protected set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000E3E8 File Offset: 0x0000C5E8
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0000E3F0 File Offset: 0x0000C5F0
		public OneAxisInputControl Right { get; protected set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000E3FC File Offset: 0x0000C5FC
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000E404 File Offset: 0x0000C604
		public OneAxisInputControl Up { get; protected set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000E410 File Offset: 0x0000C610
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000E418 File Offset: 0x0000C618
		public OneAxisInputControl Down { get; protected set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000E424 File Offset: 0x0000C624
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000E42C File Offset: 0x0000C62C
		public ulong UpdateTick { get; protected set; }

		// Token: 0x060002B6 RID: 694 RVA: 0x0000E438 File Offset: 0x0000C638
		public void ClearInputState()
		{
			this.Left.ClearInputState();
			this.Right.ClearInputState();
			this.Up.ClearInputState();
			this.Down.ClearInputState();
			this.lastState = false;
			this.lastValue = Vector2.zero;
			this.thisState = false;
			this.thisValue = Vector2.zero;
			this.clearInputState = true;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000E49C File Offset: 0x0000C69C
		public void Filter(TwoAxisInputControl twoAxisInputControl, float deltaTime)
		{
			this.UpdateWithAxes(twoAxisInputControl.X, twoAxisInputControl.Y, InputManager.CurrentTick, deltaTime);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000E4C4 File Offset: 0x0000C6C4
		internal void UpdateWithAxes(float x, float y, ulong updateTick, float deltaTime)
		{
			this.lastState = this.thisState;
			this.lastValue = this.thisValue;
			this.thisValue = ((!this.Raw) ? Utility.ApplyCircularDeadZone(x, y, this.LowerDeadZone, this.UpperDeadZone) : new Vector2(x, y));
			this.X = this.thisValue.x;
			this.Y = this.thisValue.y;
			this.Left.CommitWithValue(Mathf.Max(0f, -this.X), updateTick, deltaTime);
			this.Right.CommitWithValue(Mathf.Max(0f, this.X), updateTick, deltaTime);
			if (InputManager.InvertYAxis)
			{
				this.Up.CommitWithValue(Mathf.Max(0f, -this.Y), updateTick, deltaTime);
				this.Down.CommitWithValue(Mathf.Max(0f, this.Y), updateTick, deltaTime);
			}
			else
			{
				this.Up.CommitWithValue(Mathf.Max(0f, this.Y), updateTick, deltaTime);
				this.Down.CommitWithValue(Mathf.Max(0f, -this.Y), updateTick, deltaTime);
			}
			this.thisState = (this.Up.State || this.Down.State || this.Left.State || this.Right.State);
			if (this.clearInputState)
			{
				this.lastState = this.thisState;
				this.lastValue = this.thisValue;
				this.clearInputState = false;
				this.HasChanged = false;
				return;
			}
			if (this.thisValue != this.lastValue)
			{
				this.UpdateTick = updateTick;
				this.HasChanged = true;
			}
			else
			{
				this.HasChanged = false;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000E6EC File Offset: 0x0000C8EC
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000E6A8 File Offset: 0x0000C8A8
		public float StateThreshold
		{
			get
			{
				return this.stateThreshold;
			}
			set
			{
				this.stateThreshold = value;
				this.Left.StateThreshold = value;
				this.Right.StateThreshold = value;
				this.Up.StateThreshold = value;
				this.Down.StateThreshold = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000E6F4 File Offset: 0x0000C8F4
		public bool State
		{
			get
			{
				return this.thisState;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000E6FC File Offset: 0x0000C8FC
		public bool LastState
		{
			get
			{
				return this.lastState;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000E704 File Offset: 0x0000C904
		public Vector2 Value
		{
			get
			{
				return this.thisValue;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000E70C File Offset: 0x0000C90C
		public Vector2 LastValue
		{
			get
			{
				return this.lastValue;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000E714 File Offset: 0x0000C914
		public Vector2 Vector
		{
			get
			{
				return this.thisValue;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000E71C File Offset: 0x0000C91C
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000E724 File Offset: 0x0000C924
		public bool HasChanged { get; protected set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000E730 File Offset: 0x0000C930
		public bool IsPressed
		{
			get
			{
				return this.thisState;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000E738 File Offset: 0x0000C938
		public bool WasPressed
		{
			get
			{
				return this.thisState && !this.lastState;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000E754 File Offset: 0x0000C954
		public bool WasReleased
		{
			get
			{
				return !this.thisState && this.lastState;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000E76C File Offset: 0x0000C96C
		public float Angle
		{
			get
			{
				return Utility.VectorToAngle(this.thisValue);
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000E77C File Offset: 0x0000C97C
		public static implicit operator bool(TwoAxisInputControl instance)
		{
			return instance.thisState;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000E784 File Offset: 0x0000C984
		public static implicit operator Vector2(TwoAxisInputControl instance)
		{
			return instance.thisValue;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000E78C File Offset: 0x0000C98C
		public static implicit operator Vector3(TwoAxisInputControl instance)
		{
			return instance.thisValue;
		}

		// Token: 0x04000278 RID: 632
		public float LowerDeadZone;

		// Token: 0x04000279 RID: 633
		public float UpperDeadZone = 1f;

		// Token: 0x0400027A RID: 634
		public bool Raw;

		// Token: 0x0400027B RID: 635
		private bool thisState;

		// Token: 0x0400027C RID: 636
		private bool lastState;

		// Token: 0x0400027D RID: 637
		private Vector2 thisValue;

		// Token: 0x0400027E RID: 638
		private Vector2 lastValue;

		// Token: 0x0400027F RID: 639
		private bool clearInputState;

		// Token: 0x04000280 RID: 640
		private float stateThreshold;
	}
}
