using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000219 RID: 537
public class RoadHandler : MonoBehaviour
{
	// Token: 0x17000267 RID: 615
	// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0004DBFC File Offset: 0x0004BDFC
	public bool isInitalized
	{
		get
		{
			return base.GetComponent<Image>().enabled;
		}
	}

	// Token: 0x06000C90 RID: 3216 RVA: 0x0004DC0C File Offset: 0x0004BE0C
	public static RoadHandler Instance()
	{
		if (!RoadHandler._RoadHandlerScript)
		{
			RoadHandler._RoadHandlerScript = (UnityEngine.Object.FindObjectOfType(typeof(RoadHandler)) as RoadHandler);
			if (!RoadHandler._RoadHandlerScript)
			{
				Debug.LogError("There needs to be one active RoadHandler script on a GameObject in your scene.");
			}
		}
		return RoadHandler._RoadHandlerScript;
	}

	// Token: 0x06000C91 RID: 3217 RVA: 0x0004DC60 File Offset: 0x0004BE60
	private void Start()
	{
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x0004DC64 File Offset: 0x0004BE64
	private void Update()
	{
	}

	// Token: 0x06000C93 RID: 3219 RVA: 0x0004DC68 File Offset: 0x0004BE68
	public void Initialize(UnityEngine.Object[] objs, GameObject roadHandle)
	{
		this.clearGrid();
		this._currentRoadHandle = roadHandle;
		this._currentRoadHandle.GetComponent<Renderer>().material.color = Color.blue;
		this.Activate();
		foreach (UnityEngine.Object @object in objs)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this._cellPrefab.gameObject);
			gameObject.GetComponentInChildren<Text>().text = @object.name;
			gameObject.GetComponent<objectHolder>().setObject(@object);
			gameObject.transform.SetParent(this._grid, false);
		}
	}

	// Token: 0x06000C94 RID: 3220 RVA: 0x0004DCFC File Offset: 0x0004BEFC
	private void Activate()
	{
		base.GetComponent<Image>().enabled = true;
		this._grid.gameObject.SetActive(true);
		this._cancelButton.SetActive(true);
	}

	// Token: 0x06000C95 RID: 3221 RVA: 0x0004DD34 File Offset: 0x0004BF34
	private void Deactivate()
	{
		base.GetComponent<Image>().enabled = false;
		this._grid.gameObject.SetActive(false);
		this._cancelButton.SetActive(false);
	}

	// Token: 0x06000C96 RID: 3222 RVA: 0x0004DD6C File Offset: 0x0004BF6C
	public void clearGrid()
	{
		if (this._currentRoadHandle != null)
		{
			this._currentRoadHandle.GetComponent<Renderer>().material.color = Color.red;
		}
		this._currentRoadHandle = null;
		this.Deactivate();
		foreach (objectHolder objectHolder in this._grid.GetComponentsInChildren<objectHolder>())
		{
			UnityEngine.Object.Destroy(objectHolder.gameObject);
		}
	}

	// Token: 0x06000C97 RID: 3223 RVA: 0x0004DDE0 File Offset: 0x0004BFE0
	public void OnSubmit(BaseEventData p)
	{
		PointerEventData pointerEventData = p as PointerEventData;
		if (p.selectedObject.tag == "buildBlock")
		{
			levelEditorManager.Instance().currentMaterial = null;
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(p.selectedObject.GetComponent<objectHolder>().getObject());
			Debug.Log("Position: " + this._currentRoadHandle.transform.position);
			int num = int.Parse(this._currentRoadHandle.name);
			Debug.Log("Current Handle: " + this._currentRoadHandle.name + " Wanted: " + (-num + 3).ToString());
			Vector3 vector = Vector3.zero;
			GameObject gameObject2 = null;
			foreach (RoadHandle roadHandle in gameObject.GetComponentsInChildren<RoadHandle>())
			{
				if (roadHandle.gameObject.name == (-num + 3).ToString())
				{
					vector = roadHandle.transform.localPosition;
					gameObject2 = roadHandle.gameObject;
				}
			}
			if (vector == Vector3.zero)
			{
				throw new Exception("Couldnt Find Correct ROadhandle!");
			}
			Vector3 b = (!(gameObject.GetComponentInChildren<useThisObject>() == null)) ? gameObject.GetComponentInChildren<useThisObject>().transform.localPosition : Vector3.zero;
			Vector3 vector2 = this._currentRoadHandle.transform.position - (vector + b);
			Debug.Log(string.Concat(new object[]
			{
				"Total Padding: ",
				vector.z + b.z,
				"  Padding: ",
				b.z
			}));
			float num2 = this._currentRoadHandle.GetComponentInParent<useThisObject>().transform.localPosition.y - gameObject.GetComponentInChildren<useThisObject>().transform.localPosition.y + this._currentRoadHandle.GetComponentInParent<editor_first>().transform.position.y;
			Debug.Log("Correct Ypos: " + num2);
			Vector3 vector3 = new Vector3(vector2.x, num2, vector2.z);
			Debug.Log("FINAL POS NO ROT: " + vector3 + "  Pivot: ", this._currentRoadHandle.transform);
			Vector3 vector4 = vector3 - this._currentRoadHandle.transform.position;
			vector4 = Quaternion.Euler(0f, this._currentRoadHandle.GetComponentInParent<useThisObject>().transform.parent.rotation.eulerAngles.y, 0f) * vector4;
			Vector3 vector5 = vector4 + this._currentRoadHandle.transform.position;
			Debug.Log("FINAL NEW POINT: " + vector5);
			gameObject.transform.position = vector5;
			gameObject.transform.rotation = this._currentRoadHandle.GetComponentInParent<useThisObject>().transform.parent.rotation;
			Debug.Log("FINAL POS: " + gameObject.transform.position);
			this._currentRoadHandle.GetComponent<RoadHandle>().makeConnection(gameObject2.transform);
			gameObject2.GetComponent<RoadHandle>().makeConnection(this._currentRoadHandle.transform);
			levelEditorManager.blockType type = levelEditorManager.blockType.broadsblock;
			levelEditorManager.Instance().setLayerForObject(gameObject.transform, 0);
			levelEditorManager.Instance().addBlockToMap(gameObject, type, true);
			this.clearGrid();
		}
	}

	// Token: 0x0400090F RID: 2319
	public GameObject _cancelButton;

	// Token: 0x04000910 RID: 2320
	public Transform _grid;

	// Token: 0x04000911 RID: 2321
	public Transform _cellPrefab;

	// Token: 0x04000912 RID: 2322
	private GameObject _currentRoadHandle;

	// Token: 0x04000913 RID: 2323
	private static RoadHandler _RoadHandlerScript;
}
