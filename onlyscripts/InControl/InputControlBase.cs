using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000061 RID: 97
	public abstract class InputControlBase : IInputControl
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000D944 File Offset: 0x0000BB44
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000D94C File Offset: 0x0000BB4C
		public ulong UpdateTick { get; protected set; }

		// Token: 0x06000272 RID: 626 RVA: 0x0000D958 File Offset: 0x0000BB58
		private void PrepareForUpdate(ulong updateTick)
		{
			if (updateTick < this.pendingTick)
			{
				throw new InvalidOperationException("Cannot be updated with an earlier tick.");
			}
			if (this.pendingCommit && updateTick != this.pendingTick)
			{
				throw new InvalidOperationException("Cannot be updated for a new tick until pending tick is committed.");
			}
			if (updateTick > this.pendingTick)
			{
				this.lastState = this.thisState;
				this.nextState.Reset();
				this.pendingTick = updateTick;
				this.pendingCommit = true;
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000D9D0 File Offset: 0x0000BBD0
		public bool UpdateWithState(bool state, ulong updateTick, float deltaTime)
		{
			this.PrepareForUpdate(updateTick);
			this.nextState.Set(state || this.nextState.State);
			return state;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000D9FC File Offset: 0x0000BBFC
		public bool UpdateWithValue(float value, ulong updateTick, float deltaTime)
		{
			this.PrepareForUpdate(updateTick);
			if (Utility.Abs(value) > Utility.Abs(this.nextState.RawValue))
			{
				this.nextState.RawValue = value;
				if (!this.Raw)
				{
					value = Utility.ApplyDeadZone(value, this.lowerDeadZone, this.upperDeadZone);
				}
				this.nextState.Set(value, this.stateThreshold);
				return true;
			}
			return false;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000DA6C File Offset: 0x0000BC6C
		internal bool UpdateWithRawValue(float value, ulong updateTick, float deltaTime)
		{
			this.PrepareForUpdate(updateTick);
			if (Utility.Abs(value) > Utility.Abs(this.nextState.RawValue))
			{
				this.nextState.RawValue = value;
				this.nextState.Set(value, this.stateThreshold);
				return true;
			}
			return false;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000DABC File Offset: 0x0000BCBC
		internal void SetValue(float value, ulong updateTick)
		{
			if (updateTick > this.pendingTick)
			{
				this.lastState = this.thisState;
				this.nextState.Reset();
				this.pendingTick = updateTick;
				this.pendingCommit = true;
			}
			this.nextState.RawValue = value;
			this.nextState.Set(value, this.StateThreshold);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000DB18 File Offset: 0x0000BD18
		public void ClearInputState()
		{
			this.lastState.Reset();
			this.thisState.Reset();
			this.nextState.Reset();
			this.wasRepeated = false;
			this.clearInputState = true;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000DB4C File Offset: 0x0000BD4C
		public void Commit()
		{
			this.pendingCommit = false;
			this.thisState = this.nextState;
			if (this.clearInputState)
			{
				this.lastState = this.nextState;
				this.UpdateTick = this.pendingTick;
				this.clearInputState = false;
				return;
			}
			bool state = this.lastState.State;
			bool state2 = this.thisState.State;
			this.wasRepeated = false;
			if (state && !state2)
			{
				this.nextRepeatTime = 0f;
			}
			else if (state2)
			{
				if (state != state2)
				{
					this.nextRepeatTime = Time.realtimeSinceStartup + this.FirstRepeatDelay;
				}
				else if (Time.realtimeSinceStartup >= this.nextRepeatTime)
				{
					this.wasRepeated = true;
					this.nextRepeatTime = Time.realtimeSinceStartup + this.RepeatDelay;
				}
			}
			if (this.thisState != this.lastState)
			{
				this.UpdateTick = this.pendingTick;
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000DC44 File Offset: 0x0000BE44
		public void CommitWithState(bool state, ulong updateTick, float deltaTime)
		{
			this.UpdateWithState(state, updateTick, deltaTime);
			this.Commit();
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000DC58 File Offset: 0x0000BE58
		public void CommitWithValue(float value, ulong updateTick, float deltaTime)
		{
			this.UpdateWithValue(value, updateTick, deltaTime);
			this.Commit();
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		public bool State
		{
			get
			{
				return this.Enabled && this.thisState.State;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000DC88 File Offset: 0x0000BE88
		public bool LastState
		{
			get
			{
				return this.Enabled && this.lastState.State;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000DCA4 File Offset: 0x0000BEA4
		public float Value
		{
			get
			{
				return (!this.Enabled) ? 0f : this.thisState.Value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000DCD4 File Offset: 0x0000BED4
		public float LastValue
		{
			get
			{
				return (!this.Enabled) ? 0f : this.lastState.Value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000DD04 File Offset: 0x0000BF04
		public float RawValue
		{
			get
			{
				return (!this.Enabled) ? 0f : this.thisState.RawValue;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000DD34 File Offset: 0x0000BF34
		internal float NextRawValue
		{
			get
			{
				return (!this.Enabled) ? 0f : this.nextState.RawValue;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000DD64 File Offset: 0x0000BF64
		public bool HasChanged
		{
			get
			{
				return this.Enabled && this.thisState != this.lastState;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000DD88 File Offset: 0x0000BF88
		public bool IsPressed
		{
			get
			{
				return this.Enabled && this.thisState.State;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000DDA4 File Offset: 0x0000BFA4
		public bool WasPressed
		{
			get
			{
				return this.Enabled && this.thisState && !this.lastState;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
		public bool WasReleased
		{
			get
			{
				return this.Enabled && !this.thisState && this.lastState;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000DE0C File Offset: 0x0000C00C
		public bool WasRepeated
		{
			get
			{
				return this.Enabled && this.wasRepeated;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000DE24 File Offset: 0x0000C024
		// (set) Token: 0x06000287 RID: 647 RVA: 0x0000DE2C File Offset: 0x0000C02C
		public float Sensitivity
		{
			get
			{
				return this.sensitivity;
			}
			set
			{
				this.sensitivity = Mathf.Clamp01(value);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000DE3C File Offset: 0x0000C03C
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0000DE44 File Offset: 0x0000C044
		public float LowerDeadZone
		{
			get
			{
				return this.lowerDeadZone;
			}
			set
			{
				this.lowerDeadZone = Mathf.Clamp01(value);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000DE54 File Offset: 0x0000C054
		// (set) Token: 0x0600028B RID: 651 RVA: 0x0000DE5C File Offset: 0x0000C05C
		public float UpperDeadZone
		{
			get
			{
				return this.upperDeadZone;
			}
			set
			{
				this.upperDeadZone = Mathf.Clamp01(value);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000DE6C File Offset: 0x0000C06C
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0000DE74 File Offset: 0x0000C074
		public float StateThreshold
		{
			get
			{
				return this.stateThreshold;
			}
			set
			{
				this.stateThreshold = Mathf.Clamp01(value);
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000DE84 File Offset: 0x0000C084
		public static implicit operator bool(InputControlBase instance)
		{
			return instance.State;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000DE8C File Offset: 0x0000C08C
		public static implicit operator float(InputControlBase instance)
		{
			return instance.Value;
		}

		// Token: 0x040001F0 RID: 496
		private float sensitivity = 1f;

		// Token: 0x040001F1 RID: 497
		private float lowerDeadZone;

		// Token: 0x040001F2 RID: 498
		private float upperDeadZone = 1f;

		// Token: 0x040001F3 RID: 499
		private float stateThreshold;

		// Token: 0x040001F4 RID: 500
		public float FirstRepeatDelay = 0.8f;

		// Token: 0x040001F5 RID: 501
		public float RepeatDelay = 0.1f;

		// Token: 0x040001F6 RID: 502
		public bool Raw;

		// Token: 0x040001F7 RID: 503
		internal bool Enabled = true;

		// Token: 0x040001F8 RID: 504
		private ulong pendingTick;

		// Token: 0x040001F9 RID: 505
		private bool pendingCommit;

		// Token: 0x040001FA RID: 506
		private float nextRepeatTime;

		// Token: 0x040001FB RID: 507
		private float lastPressedTime;

		// Token: 0x040001FC RID: 508
		private bool wasRepeated;

		// Token: 0x040001FD RID: 509
		private bool clearInputState;

		// Token: 0x040001FE RID: 510
		private InputControlState lastState;

		// Token: 0x040001FF RID: 511
		private InputControlState nextState;

		// Token: 0x04000200 RID: 512
		private InputControlState thisState;
	}
}
