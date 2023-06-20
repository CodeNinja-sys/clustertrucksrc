using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InControl
{
	// Token: 0x02000070 RID: 112
	[AddComponentMenu("Event/InControl Input Module")]
	public class InControlInputModule : StandaloneInputModule
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0000F7FC File Offset: 0x0000D9FC
		protected InControlInputModule()
		{
			this.direction = new TwoAxisInputControl();
			this.direction.StateThreshold = this.analogMoveThreshold;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000F86C File Offset: 0x0000DA6C
		// (set) Token: 0x06000330 RID: 816 RVA: 0x0000F874 File Offset: 0x0000DA74
		public PlayerAction SubmitAction { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000F880 File Offset: 0x0000DA80
		// (set) Token: 0x06000332 RID: 818 RVA: 0x0000F888 File Offset: 0x0000DA88
		public PlayerAction CancelAction { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000F894 File Offset: 0x0000DA94
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000F89C File Offset: 0x0000DA9C
		public PlayerTwoAxisAction MoveAction { get; set; }

		// Token: 0x06000335 RID: 821 RVA: 0x0000F8A8 File Offset: 0x0000DAA8
		public override void UpdateModule()
		{
			this.lastMousePosition = this.thisMousePosition;
			this.thisMousePosition = Input.mousePosition;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000F8C4 File Offset: 0x0000DAC4
		public override bool IsModuleSupported()
		{
			return this.allowMobileDevice || Input.mousePresent;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000F8DC File Offset: 0x0000DADC
		public override bool ShouldActivateModule()
		{
			if (!base.enabled || !base.gameObject.activeInHierarchy)
			{
				return false;
			}
			this.UpdateInputState();
			bool flag = false;
			flag |= this.SubmitWasPressed;
			flag |= this.CancelWasPressed;
			flag |= this.VectorWasPressed;
			if (this.allowMouseInput)
			{
				flag |= this.MouseHasMoved;
				flag |= this.MouseButtonIsPressed;
			}
			return flag;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000F948 File Offset: 0x0000DB48
		public override void ActivateModule()
		{
			base.ActivateModule();
			this.thisMousePosition = Input.mousePosition;
			this.lastMousePosition = Input.mousePosition;
			GameObject gameObject = base.eventSystem.currentSelectedGameObject;
			if (gameObject == null)
			{
				gameObject = base.eventSystem.firstSelectedGameObject;
			}
			base.eventSystem.SetSelectedGameObject(gameObject, this.GetBaseEventData());
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000F9A8 File Offset: 0x0000DBA8
		public override void Process()
		{
			bool flag = base.SendUpdateEventToSelectedObject();
			if (base.eventSystem.sendNavigationEvents)
			{
				if (!flag)
				{
					flag = this.SendVectorEventToSelectedObject();
				}
				if (!flag)
				{
					this.SendButtonEventToSelectedObject();
				}
			}
			if (this.allowMouseInput)
			{
				base.ProcessMouseEvent();
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
		private bool SendButtonEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			BaseEventData baseEventData = this.GetBaseEventData();
			if (this.SubmitWasPressed)
			{
				ExecuteEvents.Execute<ISubmitHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.submitHandler);
			}
			else if (this.SubmitWasReleased)
			{
			}
			if (this.CancelWasPressed)
			{
				ExecuteEvents.Execute<ICancelHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.cancelHandler);
			}
			return baseEventData.used;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000FA80 File Offset: 0x0000DC80
		private bool SendVectorEventToSelectedObject()
		{
			if (!this.VectorWasPressed)
			{
				return false;
			}
			AxisEventData axisEventData = this.GetAxisEventData(this.thisVectorState.x, this.thisVectorState.y, 0.5f);
			if (axisEventData.moveDir != MoveDirection.None)
			{
				if (base.eventSystem.currentSelectedGameObject == null)
				{
					base.eventSystem.SetSelectedGameObject(base.eventSystem.firstSelectedGameObject, this.GetBaseEventData());
				}
				else
				{
					ExecuteEvents.Execute<IMoveHandler>(base.eventSystem.currentSelectedGameObject, axisEventData, ExecuteEvents.moveHandler);
				}
				this.SetVectorRepeatTimer();
			}
			return axisEventData.used;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000FB24 File Offset: 0x0000DD24
		protected override void ProcessMove(PointerEventData pointerEvent)
		{
			GameObject pointerEnter = pointerEvent.pointerEnter;
			base.ProcessMove(pointerEvent);
			if (this.focusOnMouseHover && pointerEnter != pointerEvent.pointerEnter)
			{
				GameObject eventHandler = ExecuteEvents.GetEventHandler<ISelectHandler>(pointerEvent.pointerEnter);
				base.eventSystem.SetSelectedGameObject(eventHandler, pointerEvent);
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000FB74 File Offset: 0x0000DD74
		private void Update()
		{
			this.direction.Filter(this.Device.Direction, Time.deltaTime);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000FB9C File Offset: 0x0000DD9C
		private void UpdateInputState()
		{
			this.lastVectorState = this.thisVectorState;
			this.thisVectorState = Vector2.zero;
			TwoAxisInputControl twoAxisInputControl = this.MoveAction ?? this.direction;
			if (Utility.AbsoluteIsOverThreshold(twoAxisInputControl.X, this.analogMoveThreshold))
			{
				this.thisVectorState.x = Mathf.Sign(twoAxisInputControl.X);
			}
			if (Utility.AbsoluteIsOverThreshold(twoAxisInputControl.Y, this.analogMoveThreshold))
			{
				this.thisVectorState.y = Mathf.Sign(twoAxisInputControl.Y);
			}
			if (this.VectorIsReleased)
			{
				this.nextMoveRepeatTime = 0f;
			}
			if (this.VectorIsPressed)
			{
				if (this.lastVectorState == Vector2.zero)
				{
					if (Time.realtimeSinceStartup > this.lastVectorPressedTime + 0.1f)
					{
						this.nextMoveRepeatTime = Time.realtimeSinceStartup + this.moveRepeatFirstDuration;
					}
					else
					{
						this.nextMoveRepeatTime = Time.realtimeSinceStartup + this.moveRepeatDelayDuration;
					}
				}
				this.lastVectorPressedTime = Time.realtimeSinceStartup;
			}
			this.lastSubmitState = this.thisSubmitState;
			this.thisSubmitState = ((this.SubmitAction != null) ? this.SubmitAction.IsPressed : this.SubmitButton.IsPressed);
			this.lastCancelState = this.thisCancelState;
			this.thisCancelState = ((this.CancelAction != null) ? this.CancelAction.IsPressed : this.CancelButton.IsPressed);
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000FD28 File Offset: 0x0000DF28
		// (set) Token: 0x0600033F RID: 831 RVA: 0x0000FD1C File Offset: 0x0000DF1C
		private InputDevice Device
		{
			get
			{
				return this.inputDevice ?? InputManager.ActiveDevice;
			}
			set
			{
				this.inputDevice = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000FD3C File Offset: 0x0000DF3C
		private InputControl SubmitButton
		{
			get
			{
				return this.Device.GetControl((InputControlType)this.submitButton);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000FD50 File Offset: 0x0000DF50
		private InputControl CancelButton
		{
			get
			{
				return this.Device.GetControl((InputControlType)this.cancelButton);
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000FD64 File Offset: 0x0000DF64
		private void SetVectorRepeatTimer()
		{
			this.nextMoveRepeatTime = Mathf.Max(this.nextMoveRepeatTime, Time.realtimeSinceStartup + this.moveRepeatDelayDuration);
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000FD84 File Offset: 0x0000DF84
		private bool VectorIsPressed
		{
			get
			{
				return this.thisVectorState != Vector2.zero;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000FD98 File Offset: 0x0000DF98
		private bool VectorIsReleased
		{
			get
			{
				return this.thisVectorState == Vector2.zero;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000FDAC File Offset: 0x0000DFAC
		private bool VectorHasChanged
		{
			get
			{
				return this.thisVectorState != this.lastVectorState;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000FDC0 File Offset: 0x0000DFC0
		private bool VectorWasPressed
		{
			get
			{
				return (this.VectorIsPressed && Time.realtimeSinceStartup > this.nextMoveRepeatTime) || (this.VectorIsPressed && this.lastVectorState == Vector2.zero);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000FE08 File Offset: 0x0000E008
		private bool SubmitWasPressed
		{
			get
			{
				return this.thisSubmitState && this.thisSubmitState != this.lastSubmitState;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000FE2C File Offset: 0x0000E02C
		private bool SubmitWasReleased
		{
			get
			{
				return !this.thisSubmitState && this.thisSubmitState != this.lastSubmitState;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000FE50 File Offset: 0x0000E050
		private bool CancelWasPressed
		{
			get
			{
				return this.thisCancelState && this.thisCancelState != this.lastCancelState;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000FE74 File Offset: 0x0000E074
		private bool MouseHasMoved
		{
			get
			{
				return (this.thisMousePosition - this.lastMousePosition).sqrMagnitude > 0f;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000FEA4 File Offset: 0x0000E0A4
		private bool MouseButtonIsPressed
		{
			get
			{
				return Input.GetMouseButtonDown(0);
			}
		}

		// Token: 0x040002A3 RID: 675
		public new InControlInputModule.Button submitButton = InControlInputModule.Button.Action1;

		// Token: 0x040002A4 RID: 676
		public new InControlInputModule.Button cancelButton = InControlInputModule.Button.Action2;

		// Token: 0x040002A5 RID: 677
		[Range(0.1f, 0.9f)]
		public float analogMoveThreshold = 0.5f;

		// Token: 0x040002A6 RID: 678
		public float moveRepeatFirstDuration = 0.8f;

		// Token: 0x040002A7 RID: 679
		public float moveRepeatDelayDuration = 0.1f;

		// Token: 0x040002A8 RID: 680
		public bool allowMobileDevice = true;

		// Token: 0x040002A9 RID: 681
		public bool allowMouseInput = true;

		// Token: 0x040002AA RID: 682
		public bool focusOnMouseHover;

		// Token: 0x040002AB RID: 683
		private InputDevice inputDevice;

		// Token: 0x040002AC RID: 684
		private Vector3 thisMousePosition;

		// Token: 0x040002AD RID: 685
		private Vector3 lastMousePosition;

		// Token: 0x040002AE RID: 686
		private Vector2 thisVectorState;

		// Token: 0x040002AF RID: 687
		private Vector2 lastVectorState;

		// Token: 0x040002B0 RID: 688
		private bool thisSubmitState;

		// Token: 0x040002B1 RID: 689
		private bool lastSubmitState;

		// Token: 0x040002B2 RID: 690
		private bool thisCancelState;

		// Token: 0x040002B3 RID: 691
		private bool lastCancelState;

		// Token: 0x040002B4 RID: 692
		private float nextMoveRepeatTime;

		// Token: 0x040002B5 RID: 693
		private float lastVectorPressedTime;

		// Token: 0x040002B6 RID: 694
		private TwoAxisInputControl direction;

		// Token: 0x02000071 RID: 113
		public enum Button
		{
			// Token: 0x040002BB RID: 699
			Action1 = 15,
			// Token: 0x040002BC RID: 700
			Action2,
			// Token: 0x040002BD RID: 701
			Action3,
			// Token: 0x040002BE RID: 702
			Action4
		}
	}
}
