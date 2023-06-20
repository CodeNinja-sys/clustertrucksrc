using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200007D RID: 125
	[ExecuteInEditMode]
	public class TouchManager : SingletonMonoBehavior<TouchManager>
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x000121E4 File Offset: 0x000103E4
		protected TouchManager()
		{
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060003FE RID: 1022 RVA: 0x00012204 File Offset: 0x00010404
		// (remove) Token: 0x060003FF RID: 1023 RVA: 0x0001221C File Offset: 0x0001041C
		public static event Action OnSetup;

		// Token: 0x06000400 RID: 1024 RVA: 0x00012234 File Offset: 0x00010434
		private void OnEnable()
		{
			if (!base.SetupSingleton())
			{
				return;
			}
			this.touchControls = base.GetComponentsInChildren<TouchControl>(true);
			if (Application.isPlaying)
			{
				InputManager.OnSetup += this.Setup;
				InputManager.OnUpdateDevices += this.UpdateDevice;
				InputManager.OnCommitDevices += this.CommitDevice;
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00012298 File Offset: 0x00010498
		private void OnDisable()
		{
			if (Application.isPlaying)
			{
				InputManager.OnSetup -= this.Setup;
				InputManager.OnUpdateDevices -= this.UpdateDevice;
				InputManager.OnCommitDevices -= this.CommitDevice;
			}
			this.Reset();
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000122E8 File Offset: 0x000104E8
		private void Setup()
		{
			this.UpdateScreenSize(new Vector2((float)Screen.width, (float)Screen.height));
			this.CreateDevice();
			this.CreateTouches();
			if (TouchManager.OnSetup != null)
			{
				TouchManager.OnSetup();
				TouchManager.OnSetup = null;
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00012334 File Offset: 0x00010534
		private void Reset()
		{
			this.device = null;
			this.mouseTouch = null;
			this.cachedTouches = null;
			this.activeTouches = null;
			this.readOnlyActiveTouches = null;
			this.touchControls = null;
			TouchManager.OnSetup = null;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00012374 File Offset: 0x00010574
		private void Start()
		{
			base.StartCoroutine(this.Ready());
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00012384 File Offset: 0x00010584
		private IEnumerator Ready()
		{
			yield return new WaitForEndOfFrame();
			this.isReady = true;
			this.UpdateScreenSize(new Vector2((float)Screen.width, (float)Screen.height));
			yield return null;
			yield break;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000123A0 File Offset: 0x000105A0
		private void Update()
		{
			if (!this.isReady)
			{
				return;
			}
			Vector2 vector = new Vector2((float)Screen.width, (float)Screen.height);
			if (this.screenSize != vector)
			{
				this.UpdateScreenSize(vector);
			}
			if (TouchManager.OnSetup != null)
			{
				TouchManager.OnSetup();
				TouchManager.OnSetup = null;
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00012400 File Offset: 0x00010600
		private void CreateDevice()
		{
			this.device = new InputDevice("TouchDevice");
			this.device.RawSticks = true;
			this.device.AddControl(InputControlType.LeftStickLeft, "LeftStickLeft");
			this.device.AddControl(InputControlType.LeftStickRight, "LeftStickRight");
			this.device.AddControl(InputControlType.LeftStickUp, "LeftStickUp");
			this.device.AddControl(InputControlType.LeftStickDown, "LeftStickDown");
			this.device.AddControl(InputControlType.RightStickLeft, "RightStickLeft");
			this.device.AddControl(InputControlType.RightStickRight, "RightStickRight");
			this.device.AddControl(InputControlType.RightStickUp, "RightStickUp");
			this.device.AddControl(InputControlType.RightStickDown, "RightStickDown");
			this.device.AddControl(InputControlType.LeftTrigger, "LeftTrigger");
			this.device.AddControl(InputControlType.RightTrigger, "RightTrigger");
			this.device.AddControl(InputControlType.DPadUp, "DPadUp");
			this.device.AddControl(InputControlType.DPadDown, "DPadDown");
			this.device.AddControl(InputControlType.DPadLeft, "DPadLeft");
			this.device.AddControl(InputControlType.DPadRight, "DPadRight");
			this.device.AddControl(InputControlType.Action1, "Action1");
			this.device.AddControl(InputControlType.Action2, "Action2");
			this.device.AddControl(InputControlType.Action3, "Action3");
			this.device.AddControl(InputControlType.Action4, "Action4");
			this.device.AddControl(InputControlType.LeftBumper, "LeftBumper");
			this.device.AddControl(InputControlType.RightBumper, "RightBumper");
			this.device.AddControl(InputControlType.Menu, "Menu");
			for (InputControlType inputControlType = InputControlType.Button0; inputControlType <= InputControlType.Button19; inputControlType++)
			{
				this.device.AddControl(inputControlType, inputControlType.ToString());
			}
			InputManager.AttachDevice(this.device);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000125E8 File Offset: 0x000107E8
		private void UpdateDevice(ulong updateTick, float deltaTime)
		{
			this.UpdateTouches(updateTick, deltaTime);
			this.SubmitControlStates(updateTick, deltaTime);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000125FC File Offset: 0x000107FC
		private void CommitDevice(ulong updateTick, float deltaTime)
		{
			this.CommitControlStates(updateTick, deltaTime);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00012608 File Offset: 0x00010808
		private void SubmitControlStates(ulong updateTick, float deltaTime)
		{
			int num = this.touchControls.Length;
			for (int i = 0; i < num; i++)
			{
				TouchControl touchControl = this.touchControls[i];
				if (touchControl.enabled && touchControl.gameObject.activeInHierarchy)
				{
					touchControl.SubmitControlState(updateTick, deltaTime);
				}
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001265C File Offset: 0x0001085C
		private void CommitControlStates(ulong updateTick, float deltaTime)
		{
			int num = this.touchControls.Length;
			for (int i = 0; i < num; i++)
			{
				TouchControl touchControl = this.touchControls[i];
				if (touchControl.enabled && touchControl.gameObject.activeInHierarchy)
				{
					touchControl.CommitControlState(updateTick, deltaTime);
				}
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000126B0 File Offset: 0x000108B0
		private void UpdateScreenSize(Vector2 currentScreenSize)
		{
			this.screenSize = currentScreenSize;
			this.halfScreenSize = this.screenSize / 2f;
			this.viewSize = this.ConvertViewToWorldPoint(Vector2.one) * 0.02f;
			this.percentToWorld = Mathf.Min(this.viewSize.x, this.viewSize.y);
			this.halfPercentToWorld = this.percentToWorld / 2f;
			if (this.touchCamera != null)
			{
				this.halfPixelToWorld = this.touchCamera.orthographicSize / this.screenSize.y;
				this.pixelToWorld = this.halfPixelToWorld * 2f;
			}
			if (this.touchControls != null)
			{
				int num = this.touchControls.Length;
				for (int i = 0; i < num; i++)
				{
					this.touchControls[i].ConfigureControl();
				}
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001279C File Offset: 0x0001099C
		private void CreateTouches()
		{
			this.cachedTouches = new Touch[16];
			for (int i = 0; i < 16; i++)
			{
				this.cachedTouches[i] = new Touch(i);
			}
			this.mouseTouch = this.cachedTouches[15];
			this.activeTouches = new List<Touch>(16);
			this.readOnlyActiveTouches = new ReadOnlyCollection<Touch>(this.activeTouches);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00012804 File Offset: 0x00010A04
		private void UpdateTouches(ulong updateTick, float deltaTime)
		{
			this.activeTouches.Clear();
			if (this.mouseTouch.SetWithMouseData(updateTick, deltaTime))
			{
				this.activeTouches.Add(this.mouseTouch);
			}
			for (int i = 0; i < Input.touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				Touch touch2 = this.cachedTouches[touch.fingerId];
				touch2.SetWithTouchData(touch, updateTick, deltaTime);
				this.activeTouches.Add(touch2);
			}
			for (int j = 0; j < 16; j++)
			{
				Touch touch3 = this.cachedTouches[j];
				if (touch3.phase != TouchPhase.Ended && touch3.updateTick != updateTick)
				{
					touch3.phase = TouchPhase.Ended;
					this.activeTouches.Add(touch3);
				}
			}
			this.InvokeTouchEvents();
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000128D4 File Offset: 0x00010AD4
		private void SendTouchBegan(Touch touch)
		{
			int num = this.touchControls.Length;
			for (int i = 0; i < num; i++)
			{
				TouchControl touchControl = this.touchControls[i];
				if (touchControl.enabled && touchControl.gameObject.activeInHierarchy)
				{
					touchControl.TouchBegan(touch);
				}
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00012928 File Offset: 0x00010B28
		private void SendTouchMoved(Touch touch)
		{
			int num = this.touchControls.Length;
			for (int i = 0; i < num; i++)
			{
				TouchControl touchControl = this.touchControls[i];
				if (touchControl.enabled && touchControl.gameObject.activeInHierarchy)
				{
					touchControl.TouchMoved(touch);
				}
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001297C File Offset: 0x00010B7C
		private void SendTouchEnded(Touch touch)
		{
			int num = this.touchControls.Length;
			for (int i = 0; i < num; i++)
			{
				TouchControl touchControl = this.touchControls[i];
				if (touchControl.enabled && touchControl.gameObject.activeInHierarchy)
				{
					touchControl.TouchEnded(touch);
				}
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000129D0 File Offset: 0x00010BD0
		private void InvokeTouchEvents()
		{
			int count = this.activeTouches.Count;
			if (this.enableControlsOnTouch && count > 0 && !this.controlsEnabled)
			{
				TouchManager.Device.RequestActivation();
				this.controlsEnabled = true;
			}
			for (int i = 0; i < count; i++)
			{
				Touch touch = this.activeTouches[i];
				switch (touch.phase)
				{
				case TouchPhase.Began:
					this.SendTouchBegan(touch);
					break;
				case TouchPhase.Moved:
					this.SendTouchMoved(touch);
					break;
				case TouchPhase.Ended:
					this.SendTouchEnded(touch);
					break;
				case TouchPhase.Canceled:
					this.SendTouchEnded(touch);
					break;
				}
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00012A8C File Offset: 0x00010C8C
		private bool TouchCameraIsValid()
		{
			return !(this.touchCamera == null) && !Utility.IsZero(this.touchCamera.orthographicSize) && (!Utility.IsZero(this.touchCamera.rect.width) || !Utility.IsZero(this.touchCamera.rect.height)) && (!Utility.IsZero(this.touchCamera.pixelRect.width) || !Utility.IsZero(this.touchCamera.pixelRect.height));
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00012B3C File Offset: 0x00010D3C
		private Vector3 ConvertScreenToWorldPoint(Vector2 point)
		{
			if (this.TouchCameraIsValid())
			{
				return this.touchCamera.ScreenToWorldPoint(new Vector3(point.x, point.y, -this.touchCamera.transform.position.z));
			}
			return Vector3.zero;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00012B94 File Offset: 0x00010D94
		private Vector3 ConvertViewToWorldPoint(Vector2 point)
		{
			if (this.TouchCameraIsValid())
			{
				return this.touchCamera.ViewportToWorldPoint(new Vector3(point.x, point.y, -this.touchCamera.transform.position.z));
			}
			return Vector3.zero;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00012BEC File Offset: 0x00010DEC
		private Vector3 ConvertScreenToViewPoint(Vector2 point)
		{
			if (this.TouchCameraIsValid())
			{
				return this.touchCamera.ScreenToViewportPoint(new Vector3(point.x, point.y, -this.touchCamera.transform.position.z));
			}
			return Vector3.zero;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00012C44 File Offset: 0x00010E44
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x00012C4C File Offset: 0x00010E4C
		public bool controlsEnabled
		{
			get
			{
				return this._controlsEnabled;
			}
			set
			{
				if (this._controlsEnabled != value)
				{
					int num = this.touchControls.Length;
					for (int i = 0; i < num; i++)
					{
						this.touchControls[i].enabled = value;
					}
					this._controlsEnabled = value;
				}
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00012C98 File Offset: 0x00010E98
		public static ReadOnlyCollection<Touch> Touches
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.readOnlyActiveTouches;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00012CA4 File Offset: 0x00010EA4
		public static int TouchCount
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.activeTouches.Count;
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00012CB8 File Offset: 0x00010EB8
		public static Touch GetTouch(int touchIndex)
		{
			return SingletonMonoBehavior<TouchManager>.Instance.activeTouches[touchIndex];
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00012CCC File Offset: 0x00010ECC
		public static Touch GetTouchByFingerId(int fingerId)
		{
			return SingletonMonoBehavior<TouchManager>.Instance.cachedTouches[fingerId];
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00012CDC File Offset: 0x00010EDC
		public static Vector3 ScreenToWorldPoint(Vector2 point)
		{
			return SingletonMonoBehavior<TouchManager>.Instance.ConvertScreenToWorldPoint(point);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00012CEC File Offset: 0x00010EEC
		public static Vector3 ViewToWorldPoint(Vector2 point)
		{
			return SingletonMonoBehavior<TouchManager>.Instance.ConvertViewToWorldPoint(point);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00012CFC File Offset: 0x00010EFC
		public static Vector3 ScreenToViewPoint(Vector2 point)
		{
			return SingletonMonoBehavior<TouchManager>.Instance.ConvertScreenToViewPoint(point);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00012D0C File Offset: 0x00010F0C
		public static float ConvertToWorld(float value, TouchUnitType unitType)
		{
			return value * ((unitType != TouchUnitType.Pixels) ? TouchManager.PercentToWorld : TouchManager.PixelToWorld);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00012D28 File Offset: 0x00010F28
		public static Rect PercentToWorldRect(Rect rect)
		{
			return new Rect((rect.xMin - 50f) * TouchManager.ViewSize.x, (rect.yMin - 50f) * TouchManager.ViewSize.y, rect.width * TouchManager.ViewSize.x, rect.height * TouchManager.ViewSize.y);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00012D9C File Offset: 0x00010F9C
		public static Rect PixelToWorldRect(Rect rect)
		{
			return new Rect(Mathf.Round(rect.xMin - TouchManager.HalfScreenSize.x) * TouchManager.PixelToWorld, Mathf.Round(rect.yMin - TouchManager.HalfScreenSize.y) * TouchManager.PixelToWorld, Mathf.Round(rect.width) * TouchManager.PixelToWorld, Mathf.Round(rect.height) * TouchManager.PixelToWorld);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00012E14 File Offset: 0x00011014
		public static Rect ConvertToWorld(Rect rect, TouchUnitType unitType)
		{
			return (unitType != TouchUnitType.Pixels) ? TouchManager.PercentToWorldRect(rect) : TouchManager.PixelToWorldRect(rect);
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00012E30 File Offset: 0x00011030
		public static Camera Camera
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.touchCamera;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00012E3C File Offset: 0x0001103C
		public static InputDevice Device
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.device;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00012E48 File Offset: 0x00011048
		public static Vector3 ViewSize
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.viewSize;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00012E54 File Offset: 0x00011054
		public static float PercentToWorld
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.percentToWorld;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00012E60 File Offset: 0x00011060
		public static float HalfPercentToWorld
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.halfPercentToWorld;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00012E6C File Offset: 0x0001106C
		public static float PixelToWorld
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.pixelToWorld;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00012E78 File Offset: 0x00011078
		public static float HalfPixelToWorld
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.halfPixelToWorld;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00012E84 File Offset: 0x00011084
		public static Vector2 ScreenSize
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.screenSize;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00012E90 File Offset: 0x00011090
		public static Vector2 HalfScreenSize
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.halfScreenSize;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00012E9C File Offset: 0x0001109C
		public static TouchManager.GizmoShowOption ControlsShowGizmos
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.controlsShowGizmos;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00012EA8 File Offset: 0x000110A8
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x00012EB4 File Offset: 0x000110B4
		public static bool ControlsEnabled
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.controlsEnabled;
			}
			set
			{
				SingletonMonoBehavior<TouchManager>.Instance.controlsEnabled = value;
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00012EC4 File Offset: 0x000110C4
		public static implicit operator bool(TouchManager instance)
		{
			return instance != null;
		}

		// Token: 0x0400035F RID: 863
		private const int MaxTouches = 16;

		// Token: 0x04000360 RID: 864
		[Space(10f)]
		public Camera touchCamera;

		// Token: 0x04000361 RID: 865
		public TouchManager.GizmoShowOption controlsShowGizmos = TouchManager.GizmoShowOption.Always;

		// Token: 0x04000362 RID: 866
		[HideInInspector]
		public bool enableControlsOnTouch;

		// Token: 0x04000363 RID: 867
		[HideInInspector]
		[SerializeField]
		private bool _controlsEnabled = true;

		// Token: 0x04000364 RID: 868
		[HideInInspector]
		public int controlsLayer = 5;

		// Token: 0x04000365 RID: 869
		private InputDevice device;

		// Token: 0x04000366 RID: 870
		private Vector3 viewSize;

		// Token: 0x04000367 RID: 871
		private Vector2 screenSize;

		// Token: 0x04000368 RID: 872
		private Vector2 halfScreenSize;

		// Token: 0x04000369 RID: 873
		private float percentToWorld;

		// Token: 0x0400036A RID: 874
		private float halfPercentToWorld;

		// Token: 0x0400036B RID: 875
		private float pixelToWorld;

		// Token: 0x0400036C RID: 876
		private float halfPixelToWorld;

		// Token: 0x0400036D RID: 877
		private TouchControl[] touchControls;

		// Token: 0x0400036E RID: 878
		private Touch[] cachedTouches;

		// Token: 0x0400036F RID: 879
		private List<Touch> activeTouches;

		// Token: 0x04000370 RID: 880
		private ReadOnlyCollection<Touch> readOnlyActiveTouches;

		// Token: 0x04000371 RID: 881
		private Vector2 lastMousePosition;

		// Token: 0x04000372 RID: 882
		private bool isReady;

		// Token: 0x04000373 RID: 883
		private Touch mouseTouch;

		// Token: 0x0200007E RID: 126
		public enum GizmoShowOption
		{
			// Token: 0x04000376 RID: 886
			Never,
			// Token: 0x04000377 RID: 887
			WhenSelected,
			// Token: 0x04000378 RID: 888
			UnlessPlaying,
			// Token: 0x04000379 RID: 889
			Always
		}
	}
}
