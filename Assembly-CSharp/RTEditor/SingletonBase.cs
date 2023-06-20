using System;

namespace RTEditor
{
	// Token: 0x020001DB RID: 475
	public abstract class SingletonBase<T> where T : class, new()
	{
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0004661C File Offset: 0x0004481C
		public static T Instance
		{
			get
			{
				return SingletonBase<!0>._instance;
			}
		}

		// Token: 0x040007E4 RID: 2020
		private static T _instance = Activator.CreateInstance<T>();
	}
}
