using System;
using UnityEngine;

// Token: 0x02000111 RID: 273
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	// Token: 0x17000128 RID: 296
	// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0002A880 File Offset: 0x00028A80
	public static T Instance
	{
		get
		{
			if (Singleton<!0>.applicationIsQuitting)
			{
				Debug.LogWarning("[Singleton] Instance '" + typeof(!0) + "' already destroyed on application quit. Won't create again - returning null.");
				return (!0)((object)null);
			}
			object @lock = Singleton<!0>._lock;
			T instance;
			lock (@lock)
			{
				if (Singleton<!0>._instance == null)
				{
					Singleton<!0>._instance = (!0)((object)UnityEngine.Object.FindObjectOfType(typeof(!0)));
					if (UnityEngine.Object.FindObjectsOfType(typeof(!0)).Length > 1)
					{
						Debug.LogError("[Singleton] Something went really wrong  - there should never be more than 1 singleton! Reopening the scene might fix it.");
						return Singleton<!0>._instance;
					}
					if (Singleton<!0>._instance == null)
					{
						GameObject gameObject = new GameObject();
						Singleton<!0>._instance = gameObject.AddComponent<T>();
						gameObject.name = "(singleton) " + typeof(!0).ToString();
						UnityEngine.Object.DontDestroyOnLoad(gameObject);
						Debug.Log(string.Concat(new object[]
						{
							"[Singleton] An instance of ",
							typeof(!0),
							" is needed in the scene, so '",
							gameObject,
							"' was created with DontDestroyOnLoad."
						}));
					}
					else
					{
						Debug.Log("[Singleton] Using instance already created: " + Singleton<!0>._instance.gameObject.name);
					}
				}
				instance = Singleton<!0>._instance;
			}
			return instance;
		}
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x0002AA04 File Offset: 0x00028C04
	public void OnDestroy()
	{
		Singleton<!0>.applicationIsQuitting = true;
	}

	// Token: 0x0400044F RID: 1103
	private static T _instance;

	// Token: 0x04000450 RID: 1104
	private static object _lock = new object();

	// Token: 0x04000451 RID: 1105
	private static bool applicationIsQuitting = false;
}
