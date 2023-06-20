using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x02000185 RID: 389
	[Serializable]
	public class EditorObjectSelection : MonoSingletonBase<EditorObjectSelection>
	{
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00038A58 File Offset: 0x00036C58
		public ObjectSelectionBoxDrawSettings ObjectSelectionBoxDrawSettings
		{
			get
			{
				return this._objectSelectionBoxDrawSettings;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00038A60 File Offset: 0x00036C60
		public ObjectSelectionRectangleDrawSettings ObjectSelectionRectangleDrawSettings
		{
			get
			{
				return this._objectSelectionRectangle.DrawSettings;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00038A70 File Offset: 0x00036C70
		public GameObject LastSelectedGameObject
		{
			get
			{
				return this._lastSelectedGameObject;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x00038A78 File Offset: 0x00036C78
		public HashSet<GameObject> SelectedGameObjects
		{
			get
			{
				return new HashSet<GameObject>(this._selectedObjects);
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00038A88 File Offset: 0x00036C88
		public int NumberOfSelectedObjects
		{
			get
			{
				return this._selectedObjects.Count;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00038A98 File Offset: 0x00036C98
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x00038AA0 File Offset: 0x00036CA0
		public bool AddObjectsToSelectionEnabled
		{
			get
			{
				return this._addObjectsToSelectionEnabled;
			}
			set
			{
				this._addObjectsToSelectionEnabled = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00038AAC File Offset: 0x00036CAC
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x00038AB4 File Offset: 0x00036CB4
		public bool MultiObjectDeselectEnabled
		{
			get
			{
				return this._multiObjectDeselectEnabled;
			}
			set
			{
				this._multiObjectDeselectEnabled = value;
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00038AC0 File Offset: 0x00036CC0
		public bool IsGameObjectSelected(GameObject gameObject)
		{
			return this._selectedObjects.Contains(gameObject);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00038AD0 File Offset: 0x00036CD0
		public void ApplySnapshot(ObjectSelectionSnapshot objectSelectionSnapshot)
		{
			this._selectedObjects = new HashSet<GameObject>(objectSelectionSnapshot.SelectedGameObjects);
			this._lastSelectedGameObject = objectSelectionSnapshot.LastSelectedGameObject;
			EditorGizmoSystem instance = MonoSingletonBase<EditorGizmoSystem>.Instance;
			this.ConnectObjectSelectionToGizmo(instance.TranslationGizmo);
			this.ConnectObjectSelectionToGizmo(instance.RotationGizmo);
			this.ConnectObjectSelectionToGizmo(instance.ScaleGizmo);
			ObjectSelectionChangedMessage.SendToInterestedListeners();
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00038B2C File Offset: 0x00036D2C
		public Vector3 GetSelectionWorldCenter()
		{
			this.RecalculateSelectionCenter();
			return this._selectionWorldCenter;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00038B3C File Offset: 0x00036D3C
		public void ConnectObjectSelectionToGizmo(Gizmo gizmo)
		{
			gizmo.ControlledObjects = this._selectedObjects;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00038B4C File Offset: 0x00036D4C
		public void removeFromSelection(GameObject g)
		{
			if (this._selectedObjects.Contains(g))
			{
				this._selectedObjects.Remove(g);
			}
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00038B6C File Offset: 0x00036D6C
		public void addObjectToSelection(GameObject g)
		{
			if (this._selectedObjects.Contains(g))
			{
				Debug.LogError("Already Contains: " + g.name, g);
				return;
			}
			ObjectSelectionSnapshot objectSelectionSnapshot = new ObjectSelectionSnapshot();
			objectSelectionSnapshot.TakeSnapshot();
			this._selectedObjects.Add(g);
			ObjectSelectionSnapshot objectSelectionSnapshot2 = new ObjectSelectionSnapshot();
			objectSelectionSnapshot2.TakeSnapshot();
			PostObjectSelectionChangedAction postObjectSelectionChangedAction = new PostObjectSelectionChangedAction(objectSelectionSnapshot, objectSelectionSnapshot2);
			postObjectSelectionChangedAction.Execute();
			ActiveGizmoTypeChangeAction activeGizmoTypeChangeAction = new ActiveGizmoTypeChangeAction(MonoSingletonBase<EditorGizmoSystem>.Instance.ActiveGizmoType, GizmoType.Translation);
			activeGizmoTypeChangeAction.Execute();
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00038BEC File Offset: 0x00036DEC
		public void replaceSelection(List<GameObject> newSelection)
		{
			this._selectedObjects = new HashSet<GameObject>(newSelection);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00038BFC File Offset: 0x00036DFC
		public void clearSelection()
		{
			this._selectedObjects.Clear();
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00038C0C File Offset: 0x00036E0C
		private void Start()
		{
			this._lineRenderingMaterial = new Material(Shader.Find("Selection Line Rendering"));
			this._objectSelectionRectangle.BorderLineMaterial = this._lineRenderingMaterial;
			this._objectSelectionRectangle.FillMaterial = new Material(Shader.Find("2D Geometry Rendering"));
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00038C5C File Offset: 0x00036E5C
		private void Update()
		{
			if (!levelEditorManager.Instance().CanUseGizmos)
			{
				return;
			}
			this._mouse.UpdateInfoForCurrentFrame();
			if (this._mouse.WasLeftMouseButtonPressedInCurrentFrame)
			{
				this.OnLeftMouseButtonDown();
			}
			if (this._mouse.WasMouseMovedSinceLastFrame)
			{
				this.OnMouseMoved();
			}
			if (this._mouse.WasLeftMouseButtonReleasedInCurrentFrame)
			{
				this.OnLeftMouseButtonUp();
			}
			this._selectedObjects.RemoveWhere((GameObject item) => item == null);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00038CF0 File Offset: 0x00036EF0
		private void OnLeftMouseButtonDown()
		{
			RaycastHit raycastHit;
			Physics.Raycast(levelEditorManager.Instance().MainCamera.ScreenPointToRay(Input.mousePosition), out raycastHit, 500f, levelEditorManager.Instance().onlyUIMask);
			if (raycastHit.collider && raycastHit.collider)
			{
				Debug.Log("Hit UI!", raycastHit.collider);
				return;
			}
			this._preMultiObjectSelectionChangeSnapshot = new ObjectSelectionSnapshot();
			this._preMultiObjectSelectionChangeSnapshot.TakeSnapshot();
			if (!MonoSingletonBase<EditorGizmoSystem>.Instance.IsActiveGizmoReadyForObjectManipulation())
			{
				this.CastRayForSingleObjectSelection();
			}
			this._objectSelectionRectangle.SetEnclosingRectBottomRightPoint(Input.mousePosition);
			this._objectSelectionRectangle.SetEnclosingRectTopLeftPoint(Input.mousePosition);
			if (!MonoSingletonBase<EditorGizmoSystem>.Instance.IsActiveGizmoReadyForObjectManipulation())
			{
				this._objectSelectionRectangle.IsVisible = true;
			}
			else
			{
				this._objectSelectionRectangle.IsVisible = false;
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00038DE4 File Offset: 0x00036FE4
		private void OnLeftMouseButtonUp()
		{
			this._objectSelectionRectangle.IsVisible = false;
			if (this._multiSelectionWasPerformedSinceLeftMouseButtonWasPressed)
			{
				this._postMultiObjectSelectionChangeSnapshot = new ObjectSelectionSnapshot();
				this._postMultiObjectSelectionChangeSnapshot.TakeSnapshot();
				PostObjectSelectionChangedAction postObjectSelectionChangedAction = new PostObjectSelectionChangedAction(this._preMultiObjectSelectionChangeSnapshot, this._postMultiObjectSelectionChangeSnapshot);
				postObjectSelectionChangedAction.Execute();
				this._preMultiObjectSelectionChangeSnapshot = null;
				this._postMultiObjectSelectionChangeSnapshot = null;
				this._multiSelectionWasPerformedSinceLeftMouseButtonWasPressed = false;
			}
			levelEditorManager.Instance().updateMapForGizmos();
			levelEditorManager.Instance().objectSelectionChanged(false);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00038E60 File Offset: 0x00037060
		private void OnMouseMoved()
		{
			if (!this._objectSelectionRectangle.IsVisible)
			{
				return;
			}
			if (this._mouse.IsLeftMouseButtonDown)
			{
				this._objectSelectionRectangle.SetEnclosingRectTopLeftPoint(Input.mousePosition);
				if (!MonoSingletonBase<EditorGizmoSystem>.Instance.IsActiveGizmoReadyForObjectManipulation())
				{
					this.HandleObjectSelectionWithActiveSelectionShape();
				}
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00038EB8 File Offset: 0x000370B8
		private void HandleObjectSelectionWithActiveSelectionShape()
		{
			List<GameObject> visibleGameObjects = MonoSingletonBase<EditorCamera>.Instance.GetVisibleGameObjects();
			List<GameObject> intersectingGameObjects = this._objectSelectionRectangle.GetIntersectingGameObjects(visibleGameObjects, MonoSingletonBase<EditorCamera>.Instance.Camera);
			if (!this._addObjectsToSelectionEnabled && !this._multiObjectDeselectEnabled)
			{
				this._multiSelectionWasPerformedSinceLeftMouseButtonWasPressed = (this._selectedObjects.Count != 0);
				this._selectedObjects.Clear();
			}
			if (!this._multiObjectDeselectEnabled && intersectingGameObjects.Count != 0)
			{
				this._multiSelectionWasPerformedSinceLeftMouseButtonWasPressed = true;
				foreach (GameObject found in intersectingGameObjects)
				{
					GameObject gameObject = levelEditorManager.Instance().isGameObjectValidForGizmoSelection(found);
					if (gameObject)
					{
						this._selectedObjects.Add(gameObject);
						this._lastSelectedGameObject = gameObject;
					}
				}
			}
			else if (intersectingGameObjects.Count != 0)
			{
				this._multiSelectionWasPerformedSinceLeftMouseButtonWasPressed = true;
				foreach (GameObject item in intersectingGameObjects)
				{
					this._selectedObjects.Remove(item);
				}
				this._lastSelectedGameObject = this.RetrieveAGameObjectFromObjectSelectionCollection();
				levelEditorManager.Instance().objectSelectionChanged(false);
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00039040 File Offset: 0x00037240
		private void CastRayForSingleObjectSelection()
		{
			if (this._multiObjectDeselectEnabled)
			{
				return;
			}
			Camera camera = MonoSingletonBase<EditorCamera>.Instance.Camera;
			RaycastHit raycastHit;
			if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out raycastHit))
			{
				this.OnGameObjectWasPickedByMouseCursor(raycastHit.collider.gameObject);
			}
			else
			{
				ObjectSelectionSnapshot objectSelectionSnapshot = null;
				if (!this._addObjectsToSelectionEnabled)
				{
					if (this._selectedObjects.Count != 0)
					{
						objectSelectionSnapshot = new ObjectSelectionSnapshot();
						objectSelectionSnapshot.TakeSnapshot();
					}
					this._selectedObjects.Clear();
				}
				if (objectSelectionSnapshot != null)
				{
					ObjectSelectionSnapshot objectSelectionSnapshot2 = new ObjectSelectionSnapshot();
					objectSelectionSnapshot2.TakeSnapshot();
					PostObjectSelectionChangedAction postObjectSelectionChangedAction = new PostObjectSelectionChangedAction(objectSelectionSnapshot, objectSelectionSnapshot2);
					postObjectSelectionChangedAction.Execute();
				}
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x000390E8 File Offset: 0x000372E8
		private void OnGameObjectWasPickedByMouseCursor(GameObject pickedGameObject)
		{
			ObjectSelectionSnapshot objectSelectionSnapshot;
			if (this._addObjectsToSelectionEnabled)
			{
				if (this._selectedObjects.Contains(pickedGameObject))
				{
					objectSelectionSnapshot = new ObjectSelectionSnapshot();
					objectSelectionSnapshot.TakeSnapshot();
					this._selectedObjects.Remove(pickedGameObject);
					this._lastSelectedGameObject = this.RetrieveAGameObjectFromObjectSelectionCollection();
					levelEditorManager.Instance().objectSelectionChanged(false);
				}
				else
				{
					objectSelectionSnapshot = new ObjectSelectionSnapshot();
					objectSelectionSnapshot.TakeSnapshot();
					GameObject gameObject = levelEditorManager.Instance().isGameObjectValidForGizmoSelection(pickedGameObject);
					if (gameObject)
					{
						this._selectedObjects.Add(gameObject);
						this._lastSelectedGameObject = gameObject;
					}
					levelEditorManager.Instance().objectSelectionChanged(false);
				}
			}
			else
			{
				objectSelectionSnapshot = new ObjectSelectionSnapshot();
				objectSelectionSnapshot.TakeSnapshot();
				this._selectedObjects.Clear();
				GameObject gameObject2 = levelEditorManager.Instance().isGameObjectValidForGizmoSelection(pickedGameObject);
				if (gameObject2)
				{
					Debug.Log("TOGIZMO: " + gameObject2.name);
					this._selectedObjects.Add(gameObject2);
					if (gameObject2.GetComponent<BlockInfo>().Category == "Roads/")
					{
						Debug.Log("CLIKED ON A ROAD!");
						levelEditorManager.Instance().setTargetedGameobject(gameObject2, "Adding a road!");
					}
					this._lastSelectedGameObject = gameObject2;
				}
				levelEditorManager.Instance().objectSelectionChanged(false);
			}
			if (objectSelectionSnapshot != null)
			{
				ObjectSelectionSnapshot objectSelectionSnapshot2 = new ObjectSelectionSnapshot();
				objectSelectionSnapshot2.TakeSnapshot();
				PostObjectSelectionChangedAction postObjectSelectionChangedAction = new PostObjectSelectionChangedAction(objectSelectionSnapshot, objectSelectionSnapshot2);
				postObjectSelectionChangedAction.Execute();
			}
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00039248 File Offset: 0x00037448
		private void OnRenderObject()
		{
			if (levelEditorManager.Instance().IsPlayTesting)
			{
				return;
			}
			ObjectSelectionBoxDrawer objectSelectionBoxDrawer = ObjectSelectionBoxDrawerFactory.CreateObjectSelectionBoxDrawer(this._objectSelectionBoxDrawSettings.SelectionBoxStyle);
			objectSelectionBoxDrawer.DrawObjectSelectionBoxes(this._selectedObjects, this._objectSelectionBoxDrawSettings, this._lineRenderingMaterial);
			this._objectSelectionRectangle.Draw();
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0003929C File Offset: 0x0003749C
		private void RecalculateSelectionCenter()
		{
			if (this._selectedObjects.Count == 0)
			{
				this._selectionWorldCenter = Vector3.zero;
			}
			else
			{
				Vector3 a = Vector3.zero;
				foreach (GameObject gameObject in this._selectedObjects)
				{
					a += gameObject.GetWorldCenter();
				}
				this._selectionWorldCenter = a / (float)this._selectedObjects.Count;
			}
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00039348 File Offset: 0x00037548
		private GameObject RetrieveAGameObjectFromObjectSelectionCollection()
		{
			using (HashSet<GameObject>.Enumerator enumerator = this._selectedObjects.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return enumerator.Current;
				}
			}
			return null;
		}

		// Token: 0x040006CD RID: 1741
		[SerializeField]
		private ObjectSelectionBoxDrawSettings _objectSelectionBoxDrawSettings = new ObjectSelectionBoxDrawSettings();

		// Token: 0x040006CE RID: 1742
		private HashSet<GameObject> _selectedObjects = new HashSet<GameObject>();

		// Token: 0x040006CF RID: 1743
		[SerializeField]
		private ObjectSelectionRectangle _objectSelectionRectangle = new ObjectSelectionRectangle();

		// Token: 0x040006D0 RID: 1744
		private Material _lineRenderingMaterial;

		// Token: 0x040006D1 RID: 1745
		private GameObject _lastSelectedGameObject;

		// Token: 0x040006D2 RID: 1746
		private Vector3 _selectionWorldCenter;

		// Token: 0x040006D3 RID: 1747
		private bool _addObjectsToSelectionEnabled;

		// Token: 0x040006D4 RID: 1748
		private bool _multiObjectDeselectEnabled;

		// Token: 0x040006D5 RID: 1749
		private ObjectSelectionSnapshot _preMultiObjectSelectionChangeSnapshot;

		// Token: 0x040006D6 RID: 1750
		private ObjectSelectionSnapshot _postMultiObjectSelectionChangeSnapshot;

		// Token: 0x040006D7 RID: 1751
		private bool _multiSelectionWasPerformedSinceLeftMouseButtonWasPressed;

		// Token: 0x040006D8 RID: 1752
		private Mouse _mouse = new Mouse();
	}
}
