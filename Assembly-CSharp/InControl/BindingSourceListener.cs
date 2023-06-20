using System;

namespace InControl
{
	// Token: 0x02000049 RID: 73
	public interface BindingSourceListener
	{
		// Token: 0x0600019C RID: 412
		void Reset();

		// Token: 0x0600019D RID: 413
		BindingSource Listen(BindingListenOptions listenOptions, InputDevice device);
	}
}
