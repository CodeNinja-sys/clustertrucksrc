using System;
using UnityEngine;

// Token: 0x02000291 RID: 657
public class mainCanvas : MonoBehaviour
{
	// Token: 0x06000FC2 RID: 4034 RVA: 0x000663B4 File Offset: 0x000645B4
	public static mainCanvas Instance()
	{
		if (!mainCanvas._mainCanvas)
		{
			mainCanvas._mainCanvas = (UnityEngine.Object.FindObjectOfType(typeof(mainCanvas)) as mainCanvas);
			if (!mainCanvas._mainCanvas)
			{
				Debug.LogError("There needs to be one active mainCanvas script on a GameObject in your scene.");
			}
		}
		return mainCanvas._mainCanvas;
	}

	// Token: 0x06000FC3 RID: 4035 RVA: 0x00066408 File Offset: 0x00064608
	public void disableCanvas()
	{
		this._activeGameObjects = base.GetComponentsInChildren<Transform>(false);
		foreach (Transform transform in this._activeGameObjects)
		{
			if (!(transform.gameObject == this._displayText) && !(transform == base.transform) && !(transform.GetComponent<Camera>() != null))
			{
				transform.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06000FC4 RID: 4036 RVA: 0x0006648C File Offset: 0x0006468C
	public void enableCanvas()
	{
		foreach (Transform transform in this._activeGameObjects)
		{
			transform.gameObject.SetActive(true);
		}
	}

	// Token: 0x04000C93 RID: 3219
	private static mainCanvas _mainCanvas;

	// Token: 0x04000C94 RID: 3220
	private Transform[] _activeGameObjects;

	// Token: 0x04000C95 RID: 3221
	public GameObject _displayText;
}
