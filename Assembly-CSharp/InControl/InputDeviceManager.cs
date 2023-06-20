using System;
using System.Collections.Generic;

namespace InControl
{
	// Token: 0x0200006F RID: 111
	public abstract class InputDeviceManager
	{
		// Token: 0x0600032C RID: 812
		public abstract void Update(ulong updateTick, float deltaTime);

		// Token: 0x0600032D RID: 813 RVA: 0x0000F7F8 File Offset: 0x0000D9F8
		public virtual void Destroy()
		{
		}

		// Token: 0x040002A2 RID: 674
		protected List<InputDevice> devices = new List<InputDevice>();
	}
}
