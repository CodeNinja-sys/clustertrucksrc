using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000078 RID: 120
	public abstract class TouchControl : MonoBehaviour
	{
		// Token: 0x060003E7 RID: 999
		public abstract void CreateControl();

		// Token: 0x060003E8 RID: 1000
		public abstract void DestroyControl();

		// Token: 0x060003E9 RID: 1001
		public abstract void ConfigureControl();

		// Token: 0x060003EA RID: 1002
		public abstract void SubmitControlState(ulong updateTick, float deltaTime);

		// Token: 0x060003EB RID: 1003
		public abstract void CommitControlState(ulong updateTick, float deltaTime);

		// Token: 0x060003EC RID: 1004
		public abstract void TouchBegan(Touch touch);

		// Token: 0x060003ED RID: 1005
		public abstract void TouchMoved(Touch touch);

		// Token: 0x060003EE RID: 1006
		public abstract void TouchEnded(Touch touch);

		// Token: 0x060003EF RID: 1007
		public abstract void DrawGizmos();

		// Token: 0x060003F0 RID: 1008 RVA: 0x00011E54 File Offset: 0x00010054
		private void OnEnable()
		{
			TouchManager.OnSetup += this.Setup;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00011E68 File Offset: 0x00010068
		private void OnDisable()
		{
			this.DestroyControl();
			Resources.UnloadUnusedAssets();
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00011E78 File Offset: 0x00010078
		private void Setup()
		{
			if (!base.enabled)
			{
				return;
			}
			this.CreateControl();
			this.ConfigureControl();
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00011E94 File Offset: 0x00010094
		protected Vector3 OffsetToWorldPosition(TouchControlAnchor anchor, Vector2 offset, TouchUnitType offsetUnitType, bool lockAspectRatio)
		{
			Vector3 b;
			if (offsetUnitType == TouchUnitType.Pixels)
			{
				b = TouchUtility.RoundVector(offset) * TouchManager.PixelToWorld;
			}
			else if (lockAspectRatio)
			{
				b = offset * TouchManager.PercentToWorld;
			}
			else
			{
				b = Vector3.Scale(offset, TouchManager.ViewSize);
			}
			return TouchManager.ViewToWorldPoint(TouchUtility.AnchorToViewPoint(anchor)) + b;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00011F04 File Offset: 0x00010104
		protected void SubmitButtonState(TouchControl.ButtonTarget target, bool state, ulong updateTick, float deltaTime)
		{
			if (TouchManager.Device == null || target == TouchControl.ButtonTarget.None)
			{
				return;
			}
			InputControl control = TouchManager.Device.GetControl((InputControlType)target);
			if (control != null)
			{
				control.UpdateWithState(state, updateTick, deltaTime);
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00011F40 File Offset: 0x00010140
		protected void CommitButton(TouchControl.ButtonTarget target)
		{
			if (TouchManager.Device == null || target == TouchControl.ButtonTarget.None)
			{
				return;
			}
			InputControl control = TouchManager.Device.GetControl((InputControlType)target);
			if (control != null)
			{
				control.Commit();
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00011F78 File Offset: 0x00010178
		protected void SubmitAnalogValue(TouchControl.AnalogTarget target, Vector2 value, float lowerDeadZone, float upperDeadZone, ulong updateTick, float deltaTime)
		{
			if (TouchManager.Device == null)
			{
				return;
			}
			Vector2 value2 = Utility.ApplyCircularDeadZone(value, lowerDeadZone, upperDeadZone);
			if (target == TouchControl.AnalogTarget.LeftStick || target == TouchControl.AnalogTarget.Both)
			{
				TouchManager.Device.UpdateLeftStickWithValue(value2, updateTick, deltaTime);
			}
			if (target == TouchControl.AnalogTarget.RightStick || target == TouchControl.AnalogTarget.Both)
			{
				TouchManager.Device.UpdateRightStickWithValue(value2, updateTick, deltaTime);
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00011FD4 File Offset: 0x000101D4
		protected void CommitAnalog(TouchControl.AnalogTarget target)
		{
			if (TouchManager.Device == null)
			{
				return;
			}
			if (target == TouchControl.AnalogTarget.LeftStick || target == TouchControl.AnalogTarget.Both)
			{
				TouchManager.Device.CommitLeftStick();
			}
			if (target == TouchControl.AnalogTarget.RightStick || target == TouchControl.AnalogTarget.Both)
			{
				TouchManager.Device.CommitRightStick();
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00012014 File Offset: 0x00010214
		protected void SubmitRawAnalogValue(TouchControl.AnalogTarget target, Vector2 rawValue, ulong updateTick, float deltaTime)
		{
			if (TouchManager.Device == null)
			{
				return;
			}
			if (target == TouchControl.AnalogTarget.LeftStick || target == TouchControl.AnalogTarget.Both)
			{
				TouchManager.Device.UpdateLeftStickWithRawValue(rawValue, updateTick, deltaTime);
			}
			if (target == TouchControl.AnalogTarget.RightStick || target == TouchControl.AnalogTarget.Both)
			{
				TouchManager.Device.UpdateRightStickWithRawValue(rawValue, updateTick, deltaTime);
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00012064 File Offset: 0x00010264
		protected static Vector2 SnapTo(Vector2 vector, TouchControl.SnapAngles snapAngles)
		{
			if (snapAngles == TouchControl.SnapAngles.None)
			{
				return vector;
			}
			float snapAngle = 360f / (float)snapAngles;
			return TouchControl.SnapTo(vector, snapAngle);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001208C File Offset: 0x0001028C
		protected static Vector2 SnapTo(Vector2 vector, float snapAngle)
		{
			float num = Vector2.Angle(vector, Vector2.up);
			if (num < snapAngle / 2f)
			{
				return Vector2.up * vector.magnitude;
			}
			if (num > 180f - snapAngle / 2f)
			{
				return -Vector2.up * vector.magnitude;
			}
			float num2 = Mathf.Round(num / snapAngle);
			float angle = num2 * snapAngle - num;
			Vector3 axis = Vector3.Cross(Vector2.up, vector);
			Quaternion rotation = Quaternion.AngleAxis(angle, axis);
			return rotation * vector;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00012130 File Offset: 0x00010330
		private void OnDrawGizmosSelected()
		{
			if (!base.enabled)
			{
				return;
			}
			if (TouchManager.ControlsShowGizmos != TouchManager.GizmoShowOption.WhenSelected)
			{
				return;
			}
			if (Utility.GameObjectIsCulledOnCurrentCamera(base.gameObject))
			{
				return;
			}
			if (!Application.isPlaying)
			{
				this.ConfigureControl();
			}
			this.DrawGizmos();
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001217C File Offset: 0x0001037C
		private void OnDrawGizmos()
		{
			if (!base.enabled)
			{
				return;
			}
			if (TouchManager.ControlsShowGizmos == TouchManager.GizmoShowOption.UnlessPlaying)
			{
				if (Application.isPlaying)
				{
					return;
				}
			}
			else if (TouchManager.ControlsShowGizmos != TouchManager.GizmoShowOption.Always)
			{
				return;
			}
			if (Utility.GameObjectIsCulledOnCurrentCamera(base.gameObject))
			{
				return;
			}
			if (!Application.isPlaying)
			{
				this.ConfigureControl();
			}
			this.DrawGizmos();
		}

		// Token: 0x02000079 RID: 121
		public enum ButtonTarget
		{
			// Token: 0x04000329 RID: 809
			None,
			// Token: 0x0400032A RID: 810
			Action1 = 15,
			// Token: 0x0400032B RID: 811
			Action2,
			// Token: 0x0400032C RID: 812
			Action3,
			// Token: 0x0400032D RID: 813
			Action4,
			// Token: 0x0400032E RID: 814
			LeftTrigger,
			// Token: 0x0400032F RID: 815
			RightTrigger,
			// Token: 0x04000330 RID: 816
			LeftBumper,
			// Token: 0x04000331 RID: 817
			RightBumper,
			// Token: 0x04000332 RID: 818
			DPadDown = 12,
			// Token: 0x04000333 RID: 819
			DPadLeft,
			// Token: 0x04000334 RID: 820
			DPadRight,
			// Token: 0x04000335 RID: 821
			DPadUp = 11,
			// Token: 0x04000336 RID: 822
			Menu = 30,
			// Token: 0x04000337 RID: 823
			Button0 = 62,
			// Token: 0x04000338 RID: 824
			Button1,
			// Token: 0x04000339 RID: 825
			Button2,
			// Token: 0x0400033A RID: 826
			Button3,
			// Token: 0x0400033B RID: 827
			Button4,
			// Token: 0x0400033C RID: 828
			Button5,
			// Token: 0x0400033D RID: 829
			Button6,
			// Token: 0x0400033E RID: 830
			Button7,
			// Token: 0x0400033F RID: 831
			Button8,
			// Token: 0x04000340 RID: 832
			Button9,
			// Token: 0x04000341 RID: 833
			Button10,
			// Token: 0x04000342 RID: 834
			Button11,
			// Token: 0x04000343 RID: 835
			Button12,
			// Token: 0x04000344 RID: 836
			Button13,
			// Token: 0x04000345 RID: 837
			Button14,
			// Token: 0x04000346 RID: 838
			Button15,
			// Token: 0x04000347 RID: 839
			Button16,
			// Token: 0x04000348 RID: 840
			Button17,
			// Token: 0x04000349 RID: 841
			Button18,
			// Token: 0x0400034A RID: 842
			Button19
		}

		// Token: 0x0200007A RID: 122
		public enum AnalogTarget
		{
			// Token: 0x0400034C RID: 844
			None,
			// Token: 0x0400034D RID: 845
			LeftStick,
			// Token: 0x0400034E RID: 846
			RightStick,
			// Token: 0x0400034F RID: 847
			Both
		}

		// Token: 0x0200007B RID: 123
		public enum SnapAngles
		{
			// Token: 0x04000351 RID: 849
			None,
			// Token: 0x04000352 RID: 850
			Four = 4,
			// Token: 0x04000353 RID: 851
			Eight = 8,
			// Token: 0x04000354 RID: 852
			Sixteen = 16
		}
	}
}
