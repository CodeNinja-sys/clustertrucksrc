using System;
using InControl;
using UnityEngine;

namespace TouchExample
{
	// Token: 0x02000041 RID: 65
	public class CubeController : MonoBehaviour
	{
		// Token: 0x06000174 RID: 372 RVA: 0x00009870 File Offset: 0x00007A70
		private void Start()
		{
			this.cachedRenderer = base.GetComponent<Renderer>();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00009880 File Offset: 0x00007A80
		private void Update()
		{
			InputDevice activeDevice = InputManager.ActiveDevice;
			if (activeDevice != InputDevice.Null && activeDevice != TouchManager.Device)
			{
				TouchManager.ControlsEnabled = false;
			}
			this.cachedRenderer.material.color = this.GetColorFromActionButtons(activeDevice);
			base.transform.Rotate(Vector3.down, 500f * Time.deltaTime * activeDevice.Direction.X, Space.World);
			base.transform.Rotate(Vector3.right, 500f * Time.deltaTime * activeDevice.Direction.Y, Space.World);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00009918 File Offset: 0x00007B18
		private Color GetColorFromActionButtons(InputDevice inputDevice)
		{
			if (inputDevice.Action1)
			{
				return Color.green;
			}
			if (inputDevice.Action2)
			{
				return Color.red;
			}
			if (inputDevice.Action3)
			{
				return Color.blue;
			}
			if (inputDevice.Action4)
			{
				return Color.yellow;
			}
			return Color.white;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00009984 File Offset: 0x00007B84
		private void OnGUI()
		{
			float num = 10f;
			int touchCount = TouchManager.TouchCount;
			for (int i = 0; i < touchCount; i++)
			{
				InControl.Touch touch = TouchManager.GetTouch(i);
				GUI.Label(new Rect(10f, num, 500f, num + 15f), string.Concat(new object[]
				{
					string.Empty,
					i,
					": fingerId = ",
					touch.fingerId,
					", phase = ",
					touch.phase.ToString(),
					", position = ",
					touch.position
				}));
				num += 20f;
			}
		}

		// Token: 0x04000108 RID: 264
		private Renderer cachedRenderer;
	}
}
