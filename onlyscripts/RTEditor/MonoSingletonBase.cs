using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001DA RID: 474
	public class MonoSingletonBase<T> : MonoBehaviour where T : MonoBehaviour
	{
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0004651C File Offset: 0x0004471C
		public static T Instance
		{
			get
			{
				if (MonoSingletonBase<!0>._instance == null)
				{
					object singletonLock = MonoSingletonBase<!0>._singletonLock;
					lock (singletonLock)
					{
						T[] array = UnityEngine.Object.FindObjectsOfType(typeof(!0)) as !0[];
						if (array.Length > 1)
						{
							if (Application.isEditor)
							{
								Debug.LogWarning("MonoSingleton<T>.Instance: Only 1 singleton instance can exist in the scene. Null will be returned.");
							}
							return (!0)((object)null);
						}
						if (array.Length == 0)
						{
							GameObject gameObject = new GameObject();
							MonoSingletonBase<!0>._instance = gameObject.AddComponent<T>();
							gameObject.name = "(singleton) " + typeof(!0).ToString();
							UnityEngine.Object.DontDestroyOnLoad(gameObject);
						}
						else
						{
							MonoSingletonBase<!0>._instance = array[0];
						}
					}
				}
				return MonoSingletonBase<!0>._instance;
			}
		}

		// Token: 0x040007E2 RID: 2018
		private static object _singletonLock = new object();

		// Token: 0x040007E3 RID: 2019
		private static T _instance;
	}
}
