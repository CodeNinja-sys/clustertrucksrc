using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000077 RID: 119
	public class Touch
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x00011C4C File Offset: 0x0000FE4C
		internal Touch(int fingerId)
		{
			this.fingerId = fingerId;
			this.phase = TouchPhase.Ended;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00011C64 File Offset: 0x0000FE64
		internal void SetWithTouchData(Touch touch, ulong updateTick, float deltaTime)
		{
			this.phase = touch.phase;
			this.tapCount = touch.tapCount;
			Vector2 a = touch.position;
			if (a.x < 0f)
			{
				a.x = (float)Screen.width + a.x;
			}
			if (this.phase == TouchPhase.Began)
			{
				this.deltaPosition = Vector2.zero;
				this.lastPosition = a;
				this.position = a;
			}
			else
			{
				if (this.phase == TouchPhase.Stationary)
				{
					this.phase = TouchPhase.Moved;
				}
				this.deltaPosition = a - this.lastPosition;
				this.lastPosition = this.position;
				this.position = a;
			}
			this.deltaTime = deltaTime;
			this.updateTick = updateTick;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00011D28 File Offset: 0x0000FF28
		internal bool SetWithMouseData(ulong updateTick, float deltaTime)
		{
			if (Input.touchCount > 0)
			{
				return false;
			}
			Vector2 a = new Vector2(Mathf.Round(Input.mousePosition.x), Mathf.Round(Input.mousePosition.y));
			if (Input.GetMouseButtonDown(0))
			{
				this.phase = TouchPhase.Began;
				this.tapCount = 1;
				this.deltaPosition = Vector2.zero;
				this.lastPosition = a;
				this.position = a;
				this.deltaTime = deltaTime;
				this.updateTick = updateTick;
				return true;
			}
			if (Input.GetMouseButtonUp(0))
			{
				this.phase = TouchPhase.Ended;
				this.tapCount = 1;
				this.deltaPosition = a - this.lastPosition;
				this.lastPosition = this.position;
				this.position = a;
				this.deltaTime = deltaTime;
				this.updateTick = updateTick;
				return true;
			}
			if (Input.GetMouseButton(0))
			{
				this.phase = TouchPhase.Moved;
				this.tapCount = 1;
				this.deltaPosition = a - this.lastPosition;
				this.lastPosition = this.position;
				this.position = a;
				this.deltaTime = deltaTime;
				this.updateTick = updateTick;
				return true;
			}
			return false;
		}

		// Token: 0x04000320 RID: 800
		public int fingerId;

		// Token: 0x04000321 RID: 801
		public TouchPhase phase;

		// Token: 0x04000322 RID: 802
		public int tapCount;

		// Token: 0x04000323 RID: 803
		public Vector2 position;

		// Token: 0x04000324 RID: 804
		public Vector2 deltaPosition;

		// Token: 0x04000325 RID: 805
		public Vector2 lastPosition;

		// Token: 0x04000326 RID: 806
		public float deltaTime;

		// Token: 0x04000327 RID: 807
		public ulong updateTick;
	}
}
