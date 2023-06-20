using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x020000F9 RID: 249
	public abstract class SingletonMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00027B04 File Offset: 0x00025D04
		public static T Instance
		{
			get
			{
				return SingletonMonoBehavior<!0>.GetInstance();
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00027B0C File Offset: 0x00025D0C
		private static void CreateInstance()
		{
			GameObject gameObject = new GameObject();
			gameObject.name = typeof(!0).ToString();
			Debug.Log("Creating instance of singleton: " + gameObject.name);
			SingletonMonoBehavior<!0>.instance = gameObject.AddComponent<T>();
			SingletonMonoBehavior<!0>.hasInstance = true;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00027B5C File Offset: 0x00025D5C
		private static T GetInstance()
		{
			object obj = SingletonMonoBehavior<!0>.lockObject;
			T result;
			lock (obj)
			{
				if (SingletonMonoBehavior<!0>.hasInstance)
				{
					result = SingletonMonoBehavior<!0>.instance;
				}
				else
				{
					Type typeFromHandle = typeof(!0);
					T[] array = UnityEngine.Object.FindObjectsOfType<T>();
					if (array.Length > 0)
					{
						SingletonMonoBehavior<!0>.instance = array[0];
						SingletonMonoBehavior<!0>.hasInstance = true;
						if (array.Length > 1)
						{
							Debug.LogWarning("Multiple instances of singleton " + typeFromHandle + " found; destroying all but the first.");
							for (int i = 1; i < array.Length; i++)
							{
								UnityEngine.Object.DestroyImmediate(array[i].gameObject);
							}
						}
						result = SingletonMonoBehavior<!0>.instance;
					}
					else
					{
						SingletonPrefabAttribute singletonPrefabAttribute = Attribute.GetCustomAttribute(typeFromHandle, typeof(SingletonPrefabAttribute)) as SingletonPrefabAttribute;
						if (singletonPrefabAttribute == null)
						{
							SingletonMonoBehavior<!0>.CreateInstance();
						}
						else
						{
							string name = singletonPrefabAttribute.Name;
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>(name));
							if (gameObject == null)
							{
								Debug.LogError(string.Concat(new object[]
								{
									"Could not find prefab ",
									name,
									" for singleton of type ",
									typeFromHandle,
									"."
								}));
								SingletonMonoBehavior<!0>.CreateInstance();
							}
							else
							{
								gameObject.name = name;
								SingletonMonoBehavior<!0>.instance = gameObject.GetComponent<T>();
								if (SingletonMonoBehavior<!0>.instance == null)
								{
									Debug.LogWarning(string.Concat(new object[]
									{
										"There wasn't a component of type \"",
										typeFromHandle,
										"\" inside prefab \"",
										name,
										"\"; creating one now."
									}));
									SingletonMonoBehavior<!0>.instance = gameObject.AddComponent<T>();
									SingletonMonoBehavior<!0>.hasInstance = true;
								}
							}
						}
						result = SingletonMonoBehavior<!0>.instance;
					}
				}
			}
			return result;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00027D38 File Offset: 0x00025F38
		private static void EnforceSingleton()
		{
			object obj = SingletonMonoBehavior<!0>.lockObject;
			lock (obj)
			{
				if (SingletonMonoBehavior<!0>.hasInstance)
				{
					T[] array = UnityEngine.Object.FindObjectsOfType<T>();
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].GetInstanceID() != SingletonMonoBehavior<!0>.instance.GetInstanceID())
						{
							UnityEngine.Object.DestroyImmediate(array[i].gameObject);
						}
					}
				}
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00027DDC File Offset: 0x00025FDC
		protected bool SetupSingleton()
		{
			SingletonMonoBehavior<!0>.EnforceSingleton();
			int instanceID = base.GetInstanceID();
			T t = SingletonMonoBehavior<!0>.Instance;
			return instanceID == t.GetInstanceID();
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00027E0C File Offset: 0x0002600C
		private void OnDestroy()
		{
			SingletonMonoBehavior<!0>.hasInstance = false;
		}

		// Token: 0x040003F3 RID: 1011
		private static T instance;

		// Token: 0x040003F4 RID: 1012
		private static bool hasInstance;

		// Token: 0x040003F5 RID: 1013
		private static object lockObject = new object();
	}
}
