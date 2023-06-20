using System;
using RTEditor;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200021C RID: 540
public class ToolPanelRef : MonoBehaviour
{
	// Token: 0x06000CA1 RID: 3233 RVA: 0x0004E328 File Offset: 0x0004C528
	public static ToolPanelRef Instance()
	{
		if (!ToolPanelRef._ToolPanelRef)
		{
			ToolPanelRef._ToolPanelRef = (UnityEngine.Object.FindObjectOfType(typeof(ToolPanelRef)) as ToolPanelRef);
			if (!ToolPanelRef._ToolPanelRef)
			{
				Debug.LogError("There needs to be one active ToolPanelRef script on a GameObject in your scene.");
			}
		}
		return ToolPanelRef._ToolPanelRef;
	}

	// Token: 0x06000CA2 RID: 3234 RVA: 0x0004E37C File Offset: 0x0004C57C
	private void Awake()
	{
		foreach (MonoBehaviour monoBehaviour in this.InitList)
		{
		}
	}

	// Token: 0x06000CA3 RID: 3235 RVA: 0x0004E3A8 File Offset: 0x0004C5A8
	public void changedTool(int _tool)
	{
		Debug.Log("New Tool TO Change: " + _tool);
		switch (_tool)
		{
		case 1:
		{
			ActiveGizmoTypeChangeAction activeGizmoTypeChangeAction = new ActiveGizmoTypeChangeAction(MonoSingletonBase<EditorGizmoSystem>.Instance.ActiveGizmoType, GizmoType.Translation);
			activeGizmoTypeChangeAction.Execute();
			goto IL_A9;
		}
		case 2:
		{
			ActiveGizmoTypeChangeAction activeGizmoTypeChangeAction2 = new ActiveGizmoTypeChangeAction(MonoSingletonBase<EditorGizmoSystem>.Instance.ActiveGizmoType, GizmoType.Rotation);
			activeGizmoTypeChangeAction2.Execute();
			goto IL_A9;
		}
		case 5:
		{
			ActiveGizmoTypeChangeAction activeGizmoTypeChangeAction3 = new ActiveGizmoTypeChangeAction(MonoSingletonBase<EditorGizmoSystem>.Instance.ActiveGizmoType, GizmoType.Scale);
			activeGizmoTypeChangeAction3.Execute();
			goto IL_A9;
		}
		}
		GizmosTurnOffAction gizmosTurnOffAction = new GizmosTurnOffAction(MonoSingletonBase<EditorGizmoSystem>.Instance.ActiveGizmoType);
		gizmosTurnOffAction.Execute();
		IL_A9:
		if (this.clearCurrentTool())
		{
			Debug.Log(((levelEditorManager.Tool)_tool).ToString());
			base.transform.FindChild(_tool.ToString()).gameObject.SetActive(true);
			if (base.transform.parent.parent.FindChild("_Buttons").FindChild(((levelEditorManager.Tool)_tool).ToString()))
			{
				this._icon.position = new Vector3(base.transform.parent.parent.FindChild("_Buttons").FindChild(((levelEditorManager.Tool)_tool).ToString()).transform.position.x, this._icon.transform.position.y, 0f);
			}
		}
		if (_tool == 7)
		{
			TutorialHandler.Instance.SpecialEvents[14].Clicked();
			levelEditorManager.Instance().objectSelectionChanged(false);
		}
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x0004E55C File Offset: 0x0004C75C
	private bool clearCurrentTool()
	{
		foreach (Transform transform in base.gameObject.GetComponentsInChildren<Transform>())
		{
			if (transform != base.transform && transform.parent == base.transform && transform.tag != "kill")
			{
				transform.gameObject.SetActive(false);
			}
		}
		return true;
	}

	// Token: 0x06000CA5 RID: 3237 RVA: 0x0004E5D8 File Offset: 0x0004C7D8
	public void ShowIcon(bool show)
	{
		this._icon.GetComponent<Image>().enabled = show;
	}

	// Token: 0x0400091B RID: 2331
	public MonoBehaviour[] InitList;

	// Token: 0x0400091C RID: 2332
	public Transform _icon;

	// Token: 0x0400091D RID: 2333
	private static ToolPanelRef _ToolPanelRef;
}
