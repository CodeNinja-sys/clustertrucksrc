using System;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200010B RID: 267
	public class TestInputManager : MonoBehaviour
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x0002994C File Offset: 0x00027B4C
		private void OnEnable()
		{
			this.isPaused = false;
			Time.timeScale = 1f;
			Logger.OnLogMessage += delegate(LogMessage logMessage)
			{
				this.logMessages.Add(logMessage);
			};
			InputManager.OnDeviceAttached += delegate(InputDevice inputDevice)
			{
				Debug.Log("Attached: " + inputDevice.Name);
			};
			InputManager.OnDeviceDetached += delegate(InputDevice inputDevice)
			{
				Debug.Log("Detached: " + inputDevice.Name);
			};
			InputManager.OnActiveDeviceChanged += delegate(InputDevice inputDevice)
			{
				Debug.Log("Active device changed to: " + inputDevice.Name);
			};
			InputManager.OnUpdate += this.HandleInputUpdate;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000299F4 File Offset: 0x00027BF4
		private void HandleInputUpdate(ulong updateTick, float deltaTime)
		{
			this.CheckForPauseButton();
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000299FC File Offset: 0x00027BFC
		private void Start()
		{
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00029A00 File Offset: 0x00027C00
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00029A10 File Offset: 0x00027C10
		private void CheckForPauseButton()
		{
			if (Input.GetKeyDown(KeyCode.P) || InputManager.MenuWasPressed)
			{
				Time.timeScale = ((!this.isPaused) ? 0f : 1f);
				this.isPaused = !this.isPaused;
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00029A64 File Offset: 0x00027C64
		private void SetColor(Color color)
		{
			this.style.normal.textColor = color;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00029A78 File Offset: 0x00027C78
		private void OnGUI()
		{
			int num = 300;
			int num2 = 10;
			int num3 = 10;
			int num4 = 15;
			GUI.skin.font = this.font;
			this.SetColor(Color.white);
			string text = "Devices:";
			text = text + " (Platform: " + InputManager.Platform + ")";
			text = text + " " + InputManager.ActiveDevice.Direction.Vector;
			if (this.isPaused)
			{
				this.SetColor(Color.red);
				text = "+++ PAUSED +++";
			}
			GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text, this.style);
			this.SetColor(Color.white);
			foreach (InputDevice inputDevice in InputManager.Devices)
			{
				bool flag = InputManager.ActiveDevice == inputDevice;
				Color color = (!flag) ? Color.white : Color.yellow;
				num3 = 35;
				this.SetColor(color);
				GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), inputDevice.Name, this.style);
				num3 += num4;
				if (inputDevice.IsUnknown)
				{
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), inputDevice.Meta, this.style);
					num3 += num4;
				}
				GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), "SortOrder: " + inputDevice.SortOrder, this.style);
				num3 += num4;
				GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), "LastChangeTick: " + inputDevice.LastChangeTick, this.style);
				num3 += num4;
				foreach (InputControl inputControl in inputDevice.Controls)
				{
					if (inputControl != null)
					{
						string arg;
						if (inputDevice.IsKnown)
						{
							arg = string.Format("{0} ({1})", inputControl.Target, inputControl.Handle);
						}
						else
						{
							arg = inputControl.Handle;
						}
						this.SetColor((!inputControl.State) ? color : Color.green);
						string text2 = string.Format("{0} {1}", arg, (!inputControl.State) ? string.Empty : ("= " + inputControl.Value));
						GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text2, this.style);
						num3 += num4;
					}
				}
				num3 += num4;
				color = ((!flag) ? Color.white : new Color(1f, 0.7f, 0.2f));
				if (inputDevice.IsKnown)
				{
					OneAxisInputControl oneAxisInputControl = inputDevice.LeftStickX;
					this.SetColor((!oneAxisInputControl.State) ? color : Color.green);
					string text3 = string.Format("{0} {1}", "Left Stick X", (!oneAxisInputControl.State) ? string.Empty : ("= " + oneAxisInputControl.Value));
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text3, this.style);
					num3 += num4;
					oneAxisInputControl = inputDevice.LeftStickY;
					this.SetColor((!oneAxisInputControl.State) ? color : Color.green);
					text3 = string.Format("{0} {1}", "Left Stick Y", (!oneAxisInputControl.State) ? string.Empty : ("= " + oneAxisInputControl.Value));
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text3, this.style);
					num3 += num4;
					this.SetColor((!inputDevice.LeftStick.State) ? color : Color.green);
					text3 = string.Format("{0} {1}", "Left Stick A", (!inputDevice.LeftStick.State) ? string.Empty : ("= " + inputDevice.LeftStick.Angle));
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text3, this.style);
					num3 += num4;
					oneAxisInputControl = inputDevice.RightStickX;
					this.SetColor((!oneAxisInputControl.State) ? color : Color.green);
					text3 = string.Format("{0} {1}", "Right Stick X", (!oneAxisInputControl.State) ? string.Empty : ("= " + oneAxisInputControl.Value));
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text3, this.style);
					num3 += num4;
					oneAxisInputControl = inputDevice.RightStickY;
					this.SetColor((!oneAxisInputControl.State) ? color : Color.green);
					text3 = string.Format("{0} {1}", "Right Stick Y", (!oneAxisInputControl.State) ? string.Empty : ("= " + oneAxisInputControl.Value));
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text3, this.style);
					num3 += num4;
					this.SetColor((!inputDevice.RightStick.State) ? color : Color.green);
					text3 = string.Format("{0} {1}", "Right Stick A", (!inputDevice.RightStick.State) ? string.Empty : ("= " + inputDevice.RightStick.Angle));
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text3, this.style);
					num3 += num4;
					oneAxisInputControl = inputDevice.DPadX;
					this.SetColor((!oneAxisInputControl.State) ? color : Color.green);
					text3 = string.Format("{0} {1}", "DPad X", (!oneAxisInputControl.State) ? string.Empty : ("= " + oneAxisInputControl.Value));
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text3, this.style);
					num3 += num4;
					oneAxisInputControl = inputDevice.DPadY;
					this.SetColor((!oneAxisInputControl.State) ? color : Color.green);
					text3 = string.Format("{0} {1}", "DPad Y", (!oneAxisInputControl.State) ? string.Empty : ("= " + oneAxisInputControl.Value));
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text3, this.style);
					num3 += num4;
				}
				this.SetColor(Color.cyan);
				InputControl anyButton = inputDevice.AnyButton;
				if (anyButton)
				{
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), "AnyButton = " + anyButton.Handle, this.style);
				}
				num2 += 200;
			}
			Color[] array = new Color[]
			{
				Color.gray,
				Color.yellow,
				Color.white
			};
			this.SetColor(Color.white);
			num2 = 10;
			num3 = Screen.height - (10 + num4);
			for (int j = this.logMessages.Count - 1; j >= 0; j--)
			{
				LogMessage logMessage = this.logMessages[j];
				this.SetColor(array[(int)logMessage.type]);
				foreach (string text4 in logMessage.text.Split(new char[]
				{
					'\n'
				}))
				{
					GUI.Label(new Rect((float)num2, (float)num3, (float)Screen.width, (float)(num3 + 10)), text4, this.style);
					num3 -= num4;
				}
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0002A334 File Offset: 0x00028534
		private void DrawUnityInputDebugger()
		{
			int num = 300;
			int num2 = Screen.width / 2;
			int num3 = 10;
			int num4 = 20;
			this.SetColor(Color.white);
			string[] joystickNames = Input.GetJoystickNames();
			int num5 = joystickNames.Length;
			for (int i = 0; i < num5; i++)
			{
				string text = joystickNames[i];
				int num6 = i + 1;
				GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), string.Concat(new object[]
				{
					"Joystick ",
					num6,
					": \"",
					text,
					"\""
				}), this.style);
				num3 += num4;
				string text2 = "Buttons: ";
				for (int j = 0; j < 20; j++)
				{
					string name = string.Concat(new object[]
					{
						"joystick ",
						num6,
						" button ",
						j
					});
					bool key = Input.GetKey(name);
					if (key)
					{
						string text3 = text2;
						text2 = string.Concat(new object[]
						{
							text3,
							"B",
							j,
							"  "
						});
					}
				}
				GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text2, this.style);
				num3 += num4;
				string text4 = "Analogs: ";
				for (int k = 0; k < 20; k++)
				{
					string axisName = string.Concat(new object[]
					{
						"joystick ",
						num6,
						" analog ",
						k
					});
					float axisRaw = Input.GetAxisRaw(axisName);
					if (Utility.AbsoluteIsOverThreshold(axisRaw, 0.2f))
					{
						string text3 = text4;
						text4 = string.Concat(new object[]
						{
							text3,
							"A",
							k,
							": ",
							axisRaw.ToString("0.00"),
							"  "
						});
					}
				}
				GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text4, this.style);
				num3 += num4;
				num3 += 25;
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0002A570 File Offset: 0x00028770
		private void OnDrawGizmos()
		{
			InputDevice activeDevice = InputManager.ActiveDevice;
			Vector2 a = new Vector2(activeDevice.LeftStickX, activeDevice.LeftStickY);
			Gizmos.color = Color.blue;
			Vector2 vector = new Vector2(-3f, -1f);
			Vector2 v = vector + a * 2f;
			Gizmos.DrawSphere(vector, 0.1f);
			Gizmos.DrawLine(vector, v);
			Gizmos.DrawSphere(v, 1f);
			Gizmos.color = Color.red;
			Vector2 vector2 = new Vector2(3f, -1f);
			Vector2 v2 = vector2 + activeDevice.RightStick.Vector * 2f;
			Gizmos.DrawSphere(vector2, 0.1f);
			Gizmos.DrawLine(vector2, v2);
			Gizmos.DrawSphere(v2, 1f);
		}

		// Token: 0x04000440 RID: 1088
		public Font font;

		// Token: 0x04000441 RID: 1089
		private GUIStyle style = new GUIStyle();

		// Token: 0x04000442 RID: 1090
		private List<LogMessage> logMessages = new List<LogMessage>();

		// Token: 0x04000443 RID: 1091
		private bool isPaused;
	}
}
