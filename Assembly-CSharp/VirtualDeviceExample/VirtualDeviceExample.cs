using System;
using InControl;
using UnityEngine;

namespace VirtualDeviceExample
{
	// Token: 0x02000043 RID: 67
	public class VirtualDeviceExample : MonoBehaviour
	{
		// Token: 0x0600017D RID: 381 RVA: 0x00009B90 File Offset: 0x00007D90
		private void OnEnable()
		{
			this.virtualDevice = new VirtualDevice();
			InputManager.OnSetup += delegate()
			{
				InputManager.AttachDevice(this.virtualDevice);
			};
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00009BB0 File Offset: 0x00007DB0
		private void OnDisable()
		{
			InputManager.DetachDevice(this.virtualDevice);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00009BC0 File Offset: 0x00007DC0
		private void Update()
		{
			InputDevice activeDevice = InputManager.ActiveDevice;
			base.transform.rotation = Quaternion.AngleAxis(activeDevice.LeftStick.Angle, Vector3.back);
			Color color = Color.white;
			if (activeDevice.Action1.IsPressed)
			{
				color = Color.green;
			}
			if (activeDevice.Action2.IsPressed)
			{
				color = Color.red;
			}
			if (activeDevice.Action3.IsPressed)
			{
				color = Color.blue;
			}
			if (activeDevice.Action4.IsPressed)
			{
				color = Color.yellow;
			}
			base.GetComponent<Renderer>().material.color = color;
		}

		// Token: 0x04000109 RID: 265
		private VirtualDevice virtualDevice;
	}
}
