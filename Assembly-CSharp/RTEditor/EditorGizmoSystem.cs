using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x02000184 RID: 388
	public class EditorGizmoSystem : MonoSingletonBase<EditorGizmoSystem>, IMessageListener
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x00038240 File Offset: 0x00036440
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x00038248 File Offset: 0x00036448
		public TranslationGizmo TranslationGizmo
		{
			get
			{
				return this._translationGizmo;
			}
			set
			{
				if (value == null)
				{
					return;
				}
				this._translationGizmo = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00038260 File Offset: 0x00036460
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x00038268 File Offset: 0x00036468
		public RotationGizmo RotationGizmo
		{
			get
			{
				return this._rotationGizmo;
			}
			set
			{
				if (value == null)
				{
					return;
				}
				this._rotationGizmo = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00038280 File Offset: 0x00036480
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x00038288 File Offset: 0x00036488
		public ScaleGizmo ScaleGizmo
		{
			get
			{
				return this._scaleGizmo;
			}
			set
			{
				if (value == null)
				{
					return;
				}
				this._scaleGizmo = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x000382A0 File Offset: 0x000364A0
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x000382A8 File Offset: 0x000364A8
		public int GizmoLayer
		{
			get
			{
				return this._gizmoLayer;
			}
			set
			{
				this._gizmoLayer = Mathf.Max(0, Mathf.Min(31, value));
				this.AssignGizmosToGizmoLayer();
				this.AdjustDirectionalLightCullingMask();
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x000382D8 File Offset: 0x000364D8
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x000382E8 File Offset: 0x000364E8
		public string GizmoLayerName
		{
			get
			{
				return LayerMask.LayerToName(this._gizmoLayer);
			}
			set
			{
				this._gizmoLayer = LayerMask.NameToLayer(value);
				this.AssignGizmosToGizmoLayer();
				this.AdjustDirectionalLightCullingMask();
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00038304 File Offset: 0x00036504
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x0003830C File Offset: 0x0003650C
		public TransformSpace TransformSpace
		{
			get
			{
				return this._transformSpace;
			}
			set
			{
				this.ChangeTransformSpace(value);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x00038318 File Offset: 0x00036518
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x00038320 File Offset: 0x00036520
		public GizmoType ActiveGizmoType
		{
			get
			{
				return this._activeGizmoType;
			}
			set
			{
				this.ChangeActiveGizmo(value);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0003832C File Offset: 0x0003652C
		public Gizmo ActiveGizmo
		{
			get
			{
				return this._activeGizmo;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00038334 File Offset: 0x00036534
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x0003833C File Offset: 0x0003653C
		public TransformPivotPoint TransformPivotPoint
		{
			get
			{
				return this._transformPivotPoint;
			}
			set
			{
				this.ChangeTransformPivotPoint(value);
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00038348 File Offset: 0x00036548
		public bool AreGizmosTurnedOff
		{
			get
			{
				return this._areGizmosTurnedOff;
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00038350 File Offset: 0x00036550
		public bool IsActiveGizmoReadyForObjectManipulation()
		{
			return !(this._activeGizmo == null) && this._activeGizmo.gameObject.activeSelf && this._activeGizmo.IsReadyForObjectManipulation();
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00038390 File Offset: 0x00036590
		public void TurnOffGizmos()
		{
			this._areGizmosTurnedOff = true;
			this.DeactivateAllGizmoObjects();
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x000383A0 File Offset: 0x000365A0
		private void Start()
		{
			this.ValidatePropertiesForRuntime();
			this.AssignGizmosToGizmoLayer();
			this.CreateGizmoDirectionalLight();
			this.AdjustDirectionalLightCullingMask();
			this.DeactivateAllGizmoObjects();
			this.ConnectObjectSelectionToGizmos();
			this.ActiveGizmoType = this._activeGizmoType;
			this.TransformPivotPoint = this._transformPivotPoint;
			MessageListenerDatabase instance = SingletonBase<MessageListenerDatabase>.Instance;
			instance.RegisterListenerForMessage(MessageType.ObjectSelectionChanged, this);
			instance.RegisterListenerForMessage(MessageType.GizmoTransformOperationWasUndone, this);
			instance.RegisterListenerForMessage(MessageType.GizmoTransformOperationWasRedone, this);
			instance.RegisterListenerForMessage(MessageType.VertexSnappingDisabled, this);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00038410 File Offset: 0x00036610
		private void ValidatePropertiesForRuntime()
		{
			bool flag = true;
			if (this._translationGizmo == null)
			{
				Debug.LogError("EditorGizmoSystem.Start: Missing translation gizmo. Please assign a game object with the 'TranslationGizmo' script attached to it.");
				flag = false;
			}
			if (this._rotationGizmo == null)
			{
				Debug.LogError("EditorGizmoSystem.Start: Missing rotation gizmo. Please assign a game object with the 'RotationGizmo' script attached to it.");
				flag = false;
			}
			if (this._scaleGizmo == null)
			{
				Debug.LogError("EditorGizmoSystem.Start: Missing scale gizmo. Please assign a game object with the 'ScaleGizmo' script attached to it.");
				flag = false;
			}
			if (!flag)
			{
				ApplicationHelper.Quit();
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00038484 File Offset: 0x00036684
		private void ConnectObjectSelectionToGizmos()
		{
			EditorObjectSelection instance = MonoSingletonBase<EditorObjectSelection>.Instance;
			instance.ConnectObjectSelectionToGizmo(this._translationGizmo);
			instance.ConnectObjectSelectionToGizmo(this._rotationGizmo);
			instance.ConnectObjectSelectionToGizmo(this._scaleGizmo);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x000384BC File Offset: 0x000366BC
		private void DeactivateAllGizmoObjects()
		{
			this._translationGizmo.gameObject.SetActive(false);
			this._rotationGizmo.gameObject.SetActive(false);
			this._scaleGizmo.gameObject.SetActive(false);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x000384FC File Offset: 0x000366FC
		private void AssignGizmosToGizmoLayer()
		{
			if (this._translationGizmo != null)
			{
				this._translationGizmo.gameObject.SetLayerForEntireHierarchy(this._gizmoLayer);
			}
			if (this._rotationGizmo != null)
			{
				this._rotationGizmo.gameObject.SetLayerForEntireHierarchy(this._gizmoLayer);
			}
			if (this._scaleGizmo != null)
			{
				this._scaleGizmo.gameObject.SetLayerForEntireHierarchy(this._gizmoLayer);
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00038580 File Offset: 0x00036780
		private void CreateGizmoDirectionalLight()
		{
			if (this._gizmoDirectionalLight == null)
			{
				this._gizmoDirectionalLight = new GameObject();
				this._gizmoDirectionalLight.name = "Gizmo Directional Light";
				Light light = this._gizmoDirectionalLight.AddComponent<Light>();
				light.type = LightType.Directional;
				light.shadows = LightShadows.None;
				light.intensity = 1.5f;
				Transform transform = this._gizmoDirectionalLight.transform;
				transform.parent = MonoSingletonBase<EditorCamera>.Instance.transform;
				transform.position = MonoSingletonBase<EditorCamera>.Instance.transform.position;
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00038610 File Offset: 0x00036810
		private void AdjustDirectionalLightCullingMask()
		{
			if (this._gizmoDirectionalLight != null)
			{
				this._gizmoDirectionalLight.GetComponent<Light>().cullingMask = 1 << this._gizmoLayer;
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0003864C File Offset: 0x0003684C
		private void Update()
		{
			if (this._gizmoDirectionalLight == null)
			{
				this.CreateGizmoDirectionalLight();
				this.AdjustDirectionalLightCullingMask();
			}
			this.UpdateDirectionalLight();
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00038674 File Offset: 0x00036874
		private void ChangeActiveGizmo(GizmoType gizmoType)
		{
			this._areGizmosTurnedOff = false;
			Gizmo activeGizmo = this._activeGizmo;
			this._activeGizmoType = gizmoType;
			this._activeGizmo = this.GetGizmoByType(gizmoType);
			if (activeGizmo != null)
			{
				activeGizmo.gameObject.SetActive(false);
				this._activeGizmo.transform.position = activeGizmo.transform.position;
				this.UpdateActiveGizmoRotation();
			}
			if (MonoSingletonBase<EditorObjectSelection>.Instance.NumberOfSelectedObjects != 0)
			{
				this._activeGizmo.gameObject.SetActive(true);
			}
			else
			{
				this._activeGizmo.gameObject.SetActive(false);
			}
			this._translationGizmo.SnapSettings.IsVertexSnappingEnabled = false;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00038724 File Offset: 0x00036924
		private void ChangeTransformSpace(TransformSpace transformSpace)
		{
			this._transformSpace = transformSpace;
			this.UpdateActiveGizmoRotation();
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00038734 File Offset: 0x00036934
		private void ChangeTransformPivotPoint(TransformPivotPoint transformPivotPoint)
		{
			this._transformPivotPoint = transformPivotPoint;
			this._translationGizmo.TransformPivotPoint = this._transformPivotPoint;
			this._rotationGizmo.TransformPivotPoint = this._transformPivotPoint;
			this._scaleGizmo.TransformPivotPoint = this._transformPivotPoint;
			this.EstablishActiveGizmoPosition();
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00038784 File Offset: 0x00036984
		private Gizmo GetGizmoByType(GizmoType gizmoType)
		{
			if (gizmoType == GizmoType.Translation)
			{
				return this._translationGizmo;
			}
			if (gizmoType == GizmoType.Rotation)
			{
				return this._rotationGizmo;
			}
			return this._scaleGizmo;
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x000387A8 File Offset: 0x000369A8
		private void EstablishActiveGizmoPosition()
		{
			EditorObjectSelection instance = MonoSingletonBase<EditorObjectSelection>.Instance;
			if (this._activeGizmo.gameObject.activeSelf && !this._activeGizmo.IsTransformingObjects())
			{
				if (this._transformPivotPoint == TransformPivotPoint.MeshPivot && instance.LastSelectedGameObject != null)
				{
					this._activeGizmo.transform.position = instance.LastSelectedGameObject.transform.position;
				}
				else
				{
					this._activeGizmo.transform.position = instance.GetSelectionWorldCenter();
				}
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00038838 File Offset: 0x00036A38
		private void UpdateActiveGizmoRotation()
		{
			EditorObjectSelection instance = MonoSingletonBase<EditorObjectSelection>.Instance;
			if ((this._transformSpace == TransformSpace.Global && this._activeGizmoType != GizmoType.Scale) || instance.LastSelectedGameObject == null)
			{
				this._activeGizmo.transform.rotation = Quaternion.identity;
			}
			else
			{
				this._activeGizmo.transform.rotation = instance.LastSelectedGameObject.transform.rotation;
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x000388B0 File Offset: 0x00036AB0
		private void UpdateDirectionalLight()
		{
			if (this._gizmoDirectionalLight != null)
			{
				this._gizmoDirectionalLight.RemoveAllColliders();
				this._gizmoDirectionalLight.transform.rotation = MonoSingletonBase<EditorCamera>.Instance.transform.rotation;
			}
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x000388F8 File Offset: 0x00036AF8
		public void RespondToMessage(Message message)
		{
			MessageType type = message.Type;
			switch (type)
			{
			case MessageType.GizmoTransformOperationWasUndone:
				this.RespondToMessage(message as GizmoTransformOperationWasUndoneMessage);
				break;
			case MessageType.GizmoTransformOperationWasRedone:
				this.RespondToMessage(message as GizmoTransformOperationWasRedoneMessage);
				break;
			case MessageType.ObjectSelectionChanged:
				this.RespondToMessage(message as ObjectSelectionChangedMessage);
				break;
			default:
				if (type == MessageType.VertexSnappingDisabled)
				{
					this.RespondToMessage(message as VertexSnappingDisabledMessage);
				}
				break;
			}
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00038974 File Offset: 0x00036B74
		private void RespondToMessage(ObjectSelectionChangedMessage message)
		{
			EditorObjectSelection instance = MonoSingletonBase<EditorObjectSelection>.Instance;
			if (instance.NumberOfSelectedObjects == 0)
			{
				this._activeGizmo.gameObject.SetActive(false);
			}
			else if (!this._areGizmosTurnedOff)
			{
				if (instance.NumberOfSelectedObjects != 0 && !this._activeGizmo.gameObject.activeSelf)
				{
					this._activeGizmo.gameObject.SetActive(true);
				}
				this.EstablishActiveGizmoPosition();
				this.UpdateActiveGizmoRotation();
			}
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x000389F0 File Offset: 0x00036BF0
		private void RespondToMessage(GizmoTransformOperationWasUndoneMessage message)
		{
			this.EstablishActiveGizmoPosition();
			this.UpdateActiveGizmoRotation();
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00038A00 File Offset: 0x00036C00
		private void RespondToMessage(GizmoTransformOperationWasRedoneMessage message)
		{
			this.EstablishActiveGizmoPosition();
			this.UpdateActiveGizmoRotation();
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00038A10 File Offset: 0x00036C10
		private void RespondToMessage(VertexSnappingDisabledMessage message)
		{
			this.EstablishActiveGizmoPosition();
		}

		// Token: 0x040006C3 RID: 1731
		[SerializeField]
		private TranslationGizmo _translationGizmo;

		// Token: 0x040006C4 RID: 1732
		[SerializeField]
		private RotationGizmo _rotationGizmo;

		// Token: 0x040006C5 RID: 1733
		[SerializeField]
		private ScaleGizmo _scaleGizmo;

		// Token: 0x040006C6 RID: 1734
		[SerializeField]
		private GameObject _gizmoDirectionalLight;

		// Token: 0x040006C7 RID: 1735
		[SerializeField]
		private int _gizmoLayer;

		// Token: 0x040006C8 RID: 1736
		private Gizmo _activeGizmo;

		// Token: 0x040006C9 RID: 1737
		[SerializeField]
		private TransformSpace _transformSpace = TransformSpace.Global;

		// Token: 0x040006CA RID: 1738
		[SerializeField]
		private GizmoType _activeGizmoType;

		// Token: 0x040006CB RID: 1739
		[SerializeField]
		private TransformPivotPoint _transformPivotPoint = TransformPivotPoint.Center;

		// Token: 0x040006CC RID: 1740
		private bool _areGizmosTurnedOff;
	}
}
