using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000085 RID: 133
	public class UnityGyroAxisSource : InputControlSource
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x00013A90 File Offset: 0x00011C90
		public UnityGyroAxisSource()
		{
			UnityGyroAxisSource.Calibrate();
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00013AA0 File Offset: 0x00011CA0
		public UnityGyroAxisSource(UnityGyroAxisSource.GyroAxis axis)
		{
			this.Axis = (int)axis;
			UnityGyroAxisSource.Calibrate();
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00013AB4 File Offset: 0x00011CB4
		public float GetValue(InputDevice inputDevice)
		{
			return UnityGyroAxisSource.GetAxis()[this.Axis];
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00013AD4 File Offset: 0x00011CD4
		public bool GetState(InputDevice inputDevice)
		{
			return Utility.IsNotZero(this.GetValue(inputDevice));
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00013AE4 File Offset: 0x00011CE4
		private static Quaternion GetAttitude()
		{
			return Quaternion.Inverse(UnityGyroAxisSource.zeroAttitude) * Input.gyro.attitude;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00013B00 File Offset: 0x00011D00
		private static Vector3 GetAxis()
		{
			Vector3 vector = UnityGyroAxisSource.GetAttitude() * Vector3.forward;
			float x = UnityGyroAxisSource.ApplyDeadZone(Mathf.Clamp(vector.x, -1f, 1f));
			float y = UnityGyroAxisSource.ApplyDeadZone(Mathf.Clamp(vector.y, -1f, 1f));
			return new Vector3(x, y);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00013B5C File Offset: 0x00011D5C
		private static float ApplyDeadZone(float value)
		{
			return Mathf.InverseLerp(0.05f, 1f, Utility.Abs(value)) * Mathf.Sign(value);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00013B7C File Offset: 0x00011D7C
		public static void Calibrate()
		{
			UnityGyroAxisSource.zeroAttitude = Input.gyro.attitude;
		}

		// Token: 0x04000393 RID: 915
		private static Quaternion zeroAttitude;

		// Token: 0x04000394 RID: 916
		public int Axis;

		// Token: 0x02000086 RID: 134
		public enum GyroAxis
		{
			// Token: 0x04000396 RID: 918
			X,
			// Token: 0x04000397 RID: 919
			Y
		}
	}
}
