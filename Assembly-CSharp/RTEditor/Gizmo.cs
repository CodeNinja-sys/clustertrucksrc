using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001AA RID: 426
	public abstract class Gizmo : MonoBehaviour
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x0003C248 File Offset: 0x0003A448
		public static float MinGizmoBaseScale
		{
			get
			{
				return 0.01f;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0003C250 File Offset: 0x0003A450
		// (set) Token: 0x06000991 RID: 2449 RVA: 0x0003C258 File Offset: 0x0003A458
		public Color SelectedAxisColor
		{
			get
			{
				return this._selectedAxisColor;
			}
			set
			{
				this._selectedAxisColor = value;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x0003C264 File Offset: 0x0003A464
		// (set) Token: 0x06000993 RID: 2451 RVA: 0x0003C26C File Offset: 0x0003A46C
		public float GizmoBaseScale
		{
			get
			{
				return this._gizmoBaseScale;
			}
			set
			{
				this._gizmoBaseScale = Mathf.Max(Gizmo.MinGizmoBaseScale, value);
				this.AdjustGizmoScale();
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0003C288 File Offset: 0x0003A488
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x0003C290 File Offset: 0x0003A490
		public bool PreserveGizmoScreenSize
		{
			get
			{
				return this._preserveGizmoScreenSize;
			}
			set
			{
				this._preserveGizmoScreenSize = value;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x0003C29C File Offset: 0x0003A49C
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x0003C2A4 File Offset: 0x0003A4A4
		public TransformPivotPoint TransformPivotPoint
		{
			get
			{
				return this._transformPivotPoint;
			}
			set
			{
				this._transformPivotPoint = value;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0003C2B0 File Offset: 0x0003A4B0
		// (set) Token: 0x06000999 RID: 2457 RVA: 0x0003C2B8 File Offset: 0x0003A4B8
		public IEnumerable<GameObject> ControlledObjects { get; set; }

		// Token: 0x0600099A RID: 2458 RVA: 0x0003C2C4 File Offset: 0x0003A4C4
		public Color GetAxisColor(GizmoAxis axis)
		{
			if (axis == GizmoAxis.None)
			{
				return Color.black;
			}
			return this._axesColors[(int)axis];
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0003C2E4 File Offset: 0x0003A4E4
		public void SetAxisColor(GizmoAxis axis, Color color)
		{
			if (axis == GizmoAxis.None)
			{
				return;
			}
			this._axesColors[(int)axis] = color;
		}

		// Token: 0x0600099C RID: 2460
		public abstract bool IsReadyForObjectManipulation();

		// Token: 0x0600099D RID: 2461 RVA: 0x0003C300 File Offset: 0x0003A500
		public bool IsTransformingObjects()
		{
			return this.IsReadyForObjectManipulation() && this._mouse.IsLeftMouseButtonDown;
		}

		// Token: 0x0600099E RID: 2462
		public abstract GizmoType GetGizmoType();

		// Token: 0x0600099F RID: 2463 RVA: 0x0003C31C File Offset: 0x0003A51C
		protected virtual void Start()
		{
			this._camera = MonoSingletonBase<EditorCamera>.Instance.Camera;
			this._cameraTransform = this._camera.transform;
			this._gizmoTransform = base.transform;
			this._lineRenderingMaterial = new Material(Shader.Find("Gizmo Line Rendering"));
			this._lineRenderingMaterial.SetInt("_StencilRefValue", this._doNotUseStencil);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0003C384 File Offset: 0x0003A584
		protected virtual void Update()
		{
			this._mouse.UpdateInfoForCurrentFrame();
			if (this._mouse.WasLeftMouseButtonPressedInCurrentFrame)
			{
				this.OnLeftMouseButtonDown();
			}
			if (this._mouse.WasLeftMouseButtonReleasedInCurrentFrame)
			{
				this.OnLeftMouseButtonUp();
			}
			if (this._mouse.CursorOffsetSinceLastFrame.magnitude > 0f)
			{
				this.OnMouseMoved();
			}
			this.AdjustGizmoScale();
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0003C3F4 File Offset: 0x0003A5F4
		protected void AdjustGizmoScale()
		{
			if (this._camera != null && this._cameraTransform != null)
			{
				float num = this.CalculateGizmoScale();
				base.transform.localScale = new Vector3(num, num, num);
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0003C440 File Offset: 0x0003A640
		protected float CalculateGizmoScale()
		{
			if (!this._preserveGizmoScreenSize)
			{
				return this._gizmoBaseScale;
			}
			if (this._camera.orthographic)
			{
				float num = 0.02f;
				return this._gizmoBaseScale * this._camera.orthographicSize / (this._camera.pixelRect.height * num);
			}
			Vector3 vector = base.transform.position - this._cameraTransform.position;
			return this._gizmoBaseScale * vector.magnitude / (this._camera.pixelRect.height * 0.045f);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0003C4E4 File Offset: 0x0003A6E4
		protected int[] GetSortedGizmoAxesIndices()
		{
			Vector3[] gizmoLocalAxes = this.GetGizmoLocalAxes();
			int num = 0;
			float num2 = Vector3.Dot(gizmoLocalAxes[0], this._cameraTransform.forward);
			int[] array = new int[]
			{
				0,
				1,
				2
			};
			for (int i = 1; i < 3; i++)
			{
				float num3 = Vector3.Dot(gizmoLocalAxes[i], this._cameraTransform.forward);
				if (num3 >= 0f)
				{
					if (num3 > num2)
					{
						num2 = num3;
						num = i;
						if (Mathf.Abs(1f - num2) < 0.0001f)
						{
							break;
						}
					}
				}
			}
			array[0] = num;
			array[num] = 0;
			return array;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0003C5A0 File Offset: 0x0003A7A0
		protected void UpdateShaderStencilRefValuesForGizmoAxisLineDraw(int axisIndex, Vector3 startPoint, Vector3 endPoint, float gizmoScale)
		{
			Vector3 vector = endPoint - startPoint;
			float magnitude = vector.magnitude;
			vector.Normalize();
			Vector3 vector2 = startPoint + vector * 0.97f * magnitude;
			Vector3 direction = (!this._camera.orthographic) ? (this._cameraTransform.position - vector2) : (-this._cameraTransform.forward);
			Ray ray = new Ray(vector2, direction);
			Plane plane = new Plane(vector, endPoint);
			float num;
			if (plane.Raycast(ray, out num))
			{
				this._lineRenderingMaterial.SetInt("_StencilRefValue", this._axesStencilRefValues[axisIndex]);
			}
			else
			{
				this._lineRenderingMaterial.SetInt("_StencilRefValue", this._doNotUseStencil);
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0003C668 File Offset: 0x0003A868
		protected Vector3[] GetGizmoLocalAxes()
		{
			return new Vector3[]
			{
				this._gizmoTransform.right,
				this._gizmoTransform.up,
				this._gizmoTransform.forward
			};
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0003C6C0 File Offset: 0x0003A8C0
		protected Plane GetCoordinateSystemPlaneFromSelectedAxis()
		{
			switch (this._selectedAxis)
			{
			case GizmoAxis.X:
				return new Plane(this.GetAxisPlaneNormalMostAlignedWithCameraLook(GizmoAxis.X), this._gizmoTransform.position);
			case GizmoAxis.Y:
				return new Plane(this.GetAxisPlaneNormalMostAlignedWithCameraLook(GizmoAxis.Y), this._gizmoTransform.position);
			case GizmoAxis.Z:
				return new Plane(this.GetAxisPlaneNormalMostAlignedWithCameraLook(GizmoAxis.Z), this._gizmoTransform.position);
			default:
				return default(Plane);
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0003C73C File Offset: 0x0003A93C
		protected Vector3 GetAxisPlaneNormalMostAlignedWithCameraLook(GizmoAxis gizmoAxis)
		{
			float num = 0f;
			Vector3 result = this._gizmoTransform.forward;
			Vector3[] array;
			if (gizmoAxis == GizmoAxis.X)
			{
				array = new Vector3[]
				{
					this._gizmoTransform.forward,
					this._gizmoTransform.up
				};
			}
			else if (gizmoAxis == GizmoAxis.Y)
			{
				array = new Vector3[]
				{
					this._gizmoTransform.forward,
					this._gizmoTransform.right
				};
			}
			else
			{
				array = new Vector3[]
				{
					this._gizmoTransform.right,
					this._gizmoTransform.up
				};
			}
			for (int i = 0; i < 2; i++)
			{
				float num2 = Mathf.Abs(Vector3.Dot(this._cameraTransform.forward, array[i]));
				if (num2 > num)
				{
					num = num2;
					result = array[i];
				}
			}
			return result;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0003C860 File Offset: 0x0003AA60
		protected float[] GetMultiAxisExtensionSigns(bool adjustForBetterVisibility)
		{
			float num = Mathf.Sign(this.CalculateGizmoScale());
			if (adjustForBetterVisibility)
			{
				Vector3 forward = this._cameraTransform.forward;
				float num2 = Vector3.Dot(forward, this._gizmoTransform.right);
				float num3 = Vector3.Dot(forward, this._gizmoTransform.up);
				float num4 = Vector3.Dot(forward, this._gizmoTransform.forward);
				return new float[]
				{
					(num2 <= 0f) ? (1f * num) : (-1f * num),
					(num3 <= 0f) ? (1f * num) : (-1f * num),
					(num4 <= 0f) ? (1f * num) : (-1f * num)
				};
			}
			return new float[]
			{
				1f * num,
				1f * num,
				1f * num
			};
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0003C954 File Offset: 0x0003AB54
		protected void DestroyGizmoMesh(Mesh gizmoMesh)
		{
			if (gizmoMesh == null)
			{
				return;
			}
			if (Application.isEditor && Application.isPlaying)
			{
				UnityEngine.Object.DestroyImmediate(gizmoMesh);
			}
			else if (!Application.isEditor && Application.isPlaying)
			{
				UnityEngine.Object.Destroy(gizmoMesh);
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0003C9A8 File Offset: 0x0003ABA8
		protected List<GameObject> GetTopParentsFromControlledObjects()
		{
			if (this.ControlledObjects != null)
			{
				List<GameObject> list = new List<GameObject>();
				foreach (GameObject gameObject in this.ControlledObjects)
				{
					Transform transform = gameObject.transform;
					bool flag = false;
					foreach (GameObject gameObject2 in this.ControlledObjects)
					{
						if (!(gameObject2 == gameObject))
						{
							if (transform.IsChildOf(gameObject2.transform))
							{
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						list.Add(gameObject);
					}
				}
				return list;
			}
			return new List<GameObject>();
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0003CAB4 File Offset: 0x0003ACB4
		protected virtual void OnLeftMouseButtonDown()
		{
			this.TakeObjectTransformSnapshots(out this._preTransformObjectSnapshots);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0003CAC4 File Offset: 0x0003ACC4
		protected virtual void OnLeftMouseButtonUp()
		{
			if (this._objectsWereTransformedSinceLeftMouseButtonWasPressed)
			{
				this.TakeObjectTransformSnapshots(out this._postTransformObjectSnapshots);
				PostGizmoTransformedObjectsAction postGizmoTransformedObjectsAction = new PostGizmoTransformedObjectsAction(this._preTransformObjectSnapshots, this._postTransformObjectSnapshots, this);
				postGizmoTransformedObjectsAction.Execute();
				this._objectsWereTransformedSinceLeftMouseButtonWasPressed = false;
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0003CB08 File Offset: 0x0003AD08
		protected virtual void OnMouseMoved()
		{
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0003CB0C File Offset: 0x0003AD0C
		protected virtual void OnRenderObject()
		{
			this.AdjustGizmoScale();
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0003CB14 File Offset: 0x0003AD14
		private void TakeObjectTransformSnapshots(out List<ObjectTransformSnapshot> objectTransformSnapshots)
		{
			objectTransformSnapshots = null;
			List<GameObject> topParentsFromControlledObjects = this.GetTopParentsFromControlledObjects();
			if (topParentsFromControlledObjects.Count == 0)
			{
				return;
			}
			objectTransformSnapshots = new List<ObjectTransformSnapshot>(topParentsFromControlledObjects.Count);
			foreach (GameObject gameObject in topParentsFromControlledObjects)
			{
				ObjectTransformSnapshot objectTransformSnapshot = new ObjectTransformSnapshot();
				objectTransformSnapshot.TakeSnapshot(gameObject);
				objectTransformSnapshots.Add(objectTransformSnapshot);
			}
		}

		// Token: 0x04000712 RID: 1810
		protected Camera _camera;

		// Token: 0x04000713 RID: 1811
		[SerializeField]
		protected Color[] _axesColors = new Color[]
		{
			Color.red,
			Color.green,
			Color.blue
		};

		// Token: 0x04000714 RID: 1812
		[SerializeField]
		protected Color _selectedAxisColor = Color.yellow;

		// Token: 0x04000715 RID: 1813
		[SerializeField]
		protected float _gizmoBaseScale = 1f;

		// Token: 0x04000716 RID: 1814
		[SerializeField]
		protected bool _preserveGizmoScreenSize = true;

		// Token: 0x04000717 RID: 1815
		protected Transform _gizmoTransform;

		// Token: 0x04000718 RID: 1816
		protected Transform _cameraTransform;

		// Token: 0x04000719 RID: 1817
		protected Mouse _mouse = new Mouse();

		// Token: 0x0400071A RID: 1818
		protected Material _lineRenderingMaterial;

		// Token: 0x0400071B RID: 1819
		protected GizmoAxis _selectedAxis = GizmoAxis.None;

		// Token: 0x0400071C RID: 1820
		protected Vector3 _lastGizmoPickPoint;

		// Token: 0x0400071D RID: 1821
		protected int[] _axesStencilRefValues = new int[]
		{
			252,
			253,
			254
		};

		// Token: 0x0400071E RID: 1822
		protected int _doNotUseStencil = 255;

		// Token: 0x0400071F RID: 1823
		protected TransformPivotPoint _transformPivotPoint = TransformPivotPoint.Center;

		// Token: 0x04000720 RID: 1824
		protected List<ObjectTransformSnapshot> _preTransformObjectSnapshots;

		// Token: 0x04000721 RID: 1825
		protected List<ObjectTransformSnapshot> _postTransformObjectSnapshots;

		// Token: 0x04000722 RID: 1826
		protected bool _objectsWereTransformedSinceLeftMouseButtonWasPressed;
	}
}
