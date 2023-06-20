using System;

namespace InControl
{
	// Token: 0x020000F8 RID: 248
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class SingletonPrefabAttribute : Attribute
	{
		// Token: 0x0600054D RID: 1357 RVA: 0x00027AE0 File Offset: 0x00025CE0
		public SingletonPrefabAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x040003F2 RID: 1010
		public readonly string Name;
	}
}
