using System;

namespace InControl
{
	// Token: 0x02000063 RID: 99
	public interface InputControlSource
	{
		// Token: 0x06000294 RID: 660
		float GetValue(InputDevice inputDevice);

		// Token: 0x06000295 RID: 661
		bool GetState(InputDevice inputDevice);
	}
}
