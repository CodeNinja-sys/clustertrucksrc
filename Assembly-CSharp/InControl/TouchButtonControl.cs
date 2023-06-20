using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000073 RID: 115
	public class TouchButtonControl : TouchControl
	{
		// Token: 0x06000395 RID: 917 RVA: 0x00010A04 File Offset: 0x0000EC04
		public override void CreateControl()
		{
			this.button.Create("Button", base.transform, 1000);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00010A24 File Offset: 0x0000EC24
		public override void DestroyControl()
		{
			this.button.Delete();
			if (this.currentTouch != null)
			{
				this.TouchEnded(this.currentTouch);
				this.currentTouch = null;
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00010A50 File Offset: 0x0000EC50
		public override void ConfigureControl()
		{
			base.transform.position = base.OffsetToWorldPosition(this.anchor, this.offset, this.offsetUnitType, this.lockAspectRatio);
			this.button.Update(true);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00010A94 File Offset: 0x0000EC94
		public override void DrawGizmos()
		{
			this.button.DrawGizmos(this.ButtonPosition, Color.yellow);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00010AAC File Offset: 0x0000ECAC
		private void Update()
		{
			if (this.dirty)
			{
				this.ConfigureControl();
				this.dirty = false;
			}
			else
			{
				this.button.Update();
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00010AE4 File Offset: 0x0000ECE4
		public override void SubmitControlState(ulong updateTick, float deltaTime)
		{
			if (this.currentTouch == null && this.allowSlideToggle)
			{
				this.ButtonState = false;
				int touchCount = TouchManager.TouchCount;
				for (int i = 0; i < touchCount; i++)
				{
					this.ButtonState = (this.ButtonState || this.button.Contains(TouchManager.GetTouch(i)));
				}
			}
			base.SubmitButtonState(this.target, this.ButtonState, updateTick, deltaTime);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00010B60 File Offset: 0x0000ED60
		public override void CommitControlState(ulong updateTick, float deltaTime)
		{
			base.CommitButton(this.target);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00010B70 File Offset: 0x0000ED70
		public override void TouchBegan(Touch touch)
		{
			if (this.currentTouch != null)
			{
				return;
			}
			if (this.button.Contains(touch))
			{
				this.ButtonState = true;
				this.currentTouch = touch;
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00010BA0 File Offset: 0x0000EDA0
		public override void TouchMoved(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			if (this.toggleOnLeave && !this.button.Contains(touch))
			{
				this.ButtonState = false;
				this.currentTouch = null;
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00010BDC File Offset: 0x0000EDDC
		public override void TouchEnded(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			this.ButtonState = false;
			this.currentTouch = null;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00010BFC File Offset: 0x0000EDFC
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x00010C04 File Offset: 0x0000EE04
		private bool ButtonState
		{
			get
			{
				return this.buttonState;
			}
			set
			{
				if (this.buttonState != value)
				{
					this.buttonState = value;
					this.button.State = value;
				}
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00010C28 File Offset: 0x0000EE28
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x00010C60 File Offset: 0x0000EE60
		public Vector3 ButtonPosition
		{
			get
			{
				return (!this.button.Ready) ? base.transform.position : this.button.Position;
			}
			set
			{
				if (this.button.Ready)
				{
					this.button.Position = value;
				}
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00010C80 File Offset: 0x0000EE80
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x00010C88 File Offset: 0x0000EE88
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

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00010CA4 File Offset: 0x0000EEA4
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x00010CAC File Offset: 0x0000EEAC
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

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00010CD0 File Offset: 0x0000EED0
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x00010CD8 File Offset: 0x0000EED8
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

		// Token: 0x040002DC RID: 732
		[SerializeField]
		[Header("Position")]
		private TouchControlAnchor anchor = TouchControlAnchor.BottomRight;

		// Token: 0x040002DD RID: 733
		[SerializeField]
		private TouchUnitType offsetUnitType;

		// Token: 0x040002DE RID: 734
		[SerializeField]
		private Vector2 offset = new Vector2(-10f, 10f);

		// Token: 0x040002DF RID: 735
		[SerializeField]
		private bool lockAspectRatio = true;

		// Token: 0x040002E0 RID: 736
		[Header("Options")]
		public TouchControl.ButtonTarget target = TouchControl.ButtonTarget.Action1;

		// Token: 0x040002E1 RID: 737
		public bool allowSlideToggle = true;

		// Token: 0x040002E2 RID: 738
		public bool toggleOnLeave;

		// Token: 0x040002E3 RID: 739
		[Header("Sprites")]
		public TouchSprite button = new TouchSprite(15f);

		// Token: 0x040002E4 RID: 740
		private bool buttonState;

		// Token: 0x040002E5 RID: 741
		private Touch currentTouch;

		// Token: 0x040002E6 RID: 742
		private bool dirty;
	}
}
