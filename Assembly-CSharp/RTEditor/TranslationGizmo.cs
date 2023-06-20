using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001B6 RID: 438
	public class TranslationGizmo : Gizmo
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x00040B2C File Offset: 0x0003ED2C
		public static float MinAxisLength
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00040B34 File Offset: 0x0003ED34
		public static float MinArrowConeRadius
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x00040B3C File Offset: 0x0003ED3C
		public static float MinArrowConeLength
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00040B44 File Offset: 0x0003ED44
		public static float MinMultiAxisSquareSize
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x00040B4C File Offset: 0x0003ED4C
		public static float MinScreenSizeOfCameraAxesTranslationSquare
		{
			get
			{
				return 2f;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00040B54 File Offset: 0x0003ED54
		public static float MinScreenSizeOfVertexSnappingSquare
		{
			get
			{
				return 2f;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00040B5C File Offset: 0x0003ED5C
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x00040B64 File Offset: 0x0003ED64
		public float AxisLength
		{
			get
			{
				return this._axisLength;
			}
			set
			{
				this._axisLength = Mathf.Max(TranslationGizmo.MinAxisLength, value);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00040B78 File Offset: 0x0003ED78
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x00040B80 File Offset: 0x0003ED80
		public float ArrowConeRadius
		{
			get
			{
				return this._arrowConeRadius;
			}
			set
			{
				this._arrowConeRadius = Mathf.Max(TranslationGizmo.MinArrowConeRadius, value);
				if (Application.isPlaying)
				{
					this.CreateArrowConeMesh();
				}
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x00040BA4 File Offset: 0x0003EDA4
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x00040BAC File Offset: 0x0003EDAC
		public float ArrowConeLength
		{
			get
			{
				return this._arrowConeLength;
			}
			set
			{
				this._arrowConeLength = Mathf.Max(TranslationGizmo.MinArrowConeLength, value);
				if (Application.isPlaying)
				{
					this.CreateArrowConeMesh();
				}
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00040BD0 File Offset: 0x0003EDD0
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x00040BD8 File Offset: 0x0003EDD8
		public float MultiAxisSquareSize
		{
			get
			{
				return this._multiAxisSquareSize;
			}
			set
			{
				this._multiAxisSquareSize = Mathf.Max(TranslationGizmo.MinMultiAxisSquareSize, value);
				if (Application.isPlaying)
				{
					this.CreateMultiAxisSquareMesh();
				}
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00040BFC File Offset: 0x0003EDFC
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x00040C04 File Offset: 0x0003EE04
		public bool AdjustMultiAxisForBetterVisibility
		{
			get
			{
				return this._adjustMultiAxisForBetterVisibility;
			}
			set
			{
				this._adjustMultiAxisForBetterVisibility = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x00040C10 File Offset: 0x0003EE10
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x00040C18 File Offset: 0x0003EE18
		public Color SelectedMultiAxisSquareColor
		{
			get
			{
				return this._selectedMultiAxisSquareColor;
			}
			set
			{
				this._selectedMultiAxisSquareColor = value;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x00040C24 File Offset: 0x0003EE24
		// (set) Token: 0x06000A70 RID: 2672 RVA: 0x00040C2C File Offset: 0x0003EE2C
		public Color SelectedMultiAxisSquareLineColor
		{
			get
			{
				return this._selectedMultiAxisSquareLineColor;
			}
			set
			{
				this._selectedMultiAxisSquareLineColor = value;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x00040C38 File Offset: 0x0003EE38
		// (set) Token: 0x06000A72 RID: 2674 RVA: 0x00040C40 File Offset: 0x0003EE40
		public bool AreArrowConesLit
		{
			get
			{
				return this._areArrowConesLit;
			}
			set
			{
				this._areArrowConesLit = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00040C4C File Offset: 0x0003EE4C
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x00040C54 File Offset: 0x0003EE54
		public bool TranslateAlongCameraRightAndUpAxes
		{
			get
			{
				return this._translateAlongCameraRightAndUpAxes;
			}
			set
			{
				if (value && base.IsTransformingObjects())
				{
					return;
				}
				this._translateAlongCameraRightAndUpAxes = value;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00040C70 File Offset: 0x0003EE70
		// (set) Token: 0x06000A76 RID: 2678 RVA: 0x00040C78 File Offset: 0x0003EE78
		public float ScreenSizeOfCameraAxesTranslationSquare
		{
			get
			{
				return this._screenSizeOfCameraAxesTranslationSquare;
			}
			set
			{
				this._screenSizeOfCameraAxesTranslationSquare = Mathf.Max(TranslationGizmo.MinScreenSizeOfCameraAxesTranslationSquare, value);
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x00040C8C File Offset: 0x0003EE8C
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x00040C94 File Offset: 0x0003EE94
		public Color ColorOfCameraAxesTranslationSquareLines
		{
			get
			{
				return this._colorOfCameraAxesTranslationSquareLines;
			}
			set
			{
				this._colorOfCameraAxesTranslationSquareLines = value;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00040CA0 File Offset: 0x0003EEA0
		// (set) Token: 0x06000A7A RID: 2682 RVA: 0x00040CA8 File Offset: 0x0003EEA8
		public Color ColorOfCameraAxesTranslationSquareLinesWhenSelected
		{
			get
			{
				return this._colorOfCameraAxesTranslationSquareLinesWhenSelected;
			}
			set
			{
				this._colorOfCameraAxesTranslationSquareLinesWhenSelected = value;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x00040CB4 File Offset: 0x0003EEB4
		// (set) Token: 0x06000A7C RID: 2684 RVA: 0x00040CBC File Offset: 0x0003EEBC
		public float ScreenSizeOfVertexSnappingSquare
		{
			get
			{
				return this._screenSizeOfVertexSnappingSquare;
			}
			set
			{
				this._screenSizeOfVertexSnappingSquare = Mathf.Max(value, TranslationGizmo.MinScreenSizeOfVertexSnappingSquare);
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00040CD0 File Offset: 0x0003EED0
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x00040CD8 File Offset: 0x0003EED8
		public Color ColorOfVertexSnappingSquareLines
		{
			get
			{
				return this._colorOfVertexSnappingSquareLines;
			}
			set
			{
				this._colorOfVertexSnappingSquareLines = value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x00040CE4 File Offset: 0x0003EEE4
		// (set) Token: 0x06000A80 RID: 2688 RVA: 0x00040CEC File Offset: 0x0003EEEC
		public Color ColorOfVertexSnappingSquareLinesWhenSelected
		{
			get
			{
				return this._colorOfVertexSnappingSquareLinesWhenSelected;
			}
			set
			{
				this._colorOfVertexSnappingSquareLinesWhenSelected = value;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00040CF8 File Offset: 0x0003EEF8
		public TranslationGizmoSnapSettings SnapSettings
		{
			get
			{
				return this._snapSettings;
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00040D00 File Offset: 0x0003EF00
		public Color GetMultiAxisSquareColor(MultiAxisSquare multiAxisSquare)
		{
			if (multiAxisSquare == MultiAxisSquare.None)
			{
				return Color.black;
			}
			return this._multiAxisSquareColors[(int)multiAxisSquare];
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00040D20 File Offset: 0x0003EF20
		public void SetMultiAxisSquareColor(MultiAxisSquare multiAxisSquare, Color color)
		{
			if (multiAxisSquare == MultiAxisSquare.None)
			{
				return;
			}
			this._multiAxisSquareColors[(int)multiAxisSquare] = color;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00040D3C File Offset: 0x0003EF3C
		public Color GetMultiAxisSquareLineColor(MultiAxisSquare multiAxisSquare)
		{
			if (multiAxisSquare == MultiAxisSquare.None)
			{
				return Color.black;
			}
			return this._multiAxisSquareLineColors[(int)multiAxisSquare];
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00040D5C File Offset: 0x0003EF5C
		public void SetMultiAxisSquareLineColor(MultiAxisSquare multiAxisSquare, Color color)
		{
			if (multiAxisSquare == MultiAxisSquare.None)
			{
				return;
			}
			this._multiAxisSquareLineColors[(int)multiAxisSquare] = color;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00040D78 File Offset: 0x0003EF78
		public override bool IsReadyForObjectManipulation()
		{
			return this._selectedAxis != GizmoAxis.None || this._selectedMultiAxisSquare != MultiAxisSquare.None || this._isCameraAxesTranslationSquareSelected || this._snapSettings.IsVertexSnappingEnabled;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00040DAC File Offset: 0x0003EFAC
		public override GizmoType GetGizmoType()
		{
			return GizmoType.Translation;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00040DB0 File Offset: 0x0003EFB0
		protected override void Start()
		{
			base.Start();
			this.CreateArrowConeMesh();
			this.CreateMultiAxisSquareMesh();
			this.CreateArrowConeMaterials();
			this.CreateMultiAxisSquareMaterials();
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00040DDC File Offset: 0x0003EFDC
		protected override void Update()
		{
			base.Update();
			Matrix4x4[] arrowConesWorldTransforms = this.GetArrowConesWorldTransforms();
			this.DrawArrowCones(arrowConesWorldTransforms);
			if (!this._translateAlongCameraRightAndUpAxes && !this._snapSettings.IsVertexSnappingEnabled)
			{
				Matrix4x4[] multiAxisSquaresWorldTransforms = this.GetMultiAxisSquaresWorldTransforms();
				this.DrawMultiAxisSquares(multiAxisSquaresWorldTransforms);
			}
			if (!this._snapSettings.IsVertexSnappingEnabled)
			{
				if (this._mouse.IsLeftMouseButtonDown)
				{
					return;
				}
				this._selectedAxis = GizmoAxis.None;
				this._selectedMultiAxisSquare = MultiAxisSquare.None;
				this._isCameraAxesTranslationSquareSelected = false;
				float num = float.MaxValue;
				Ray ray = this._camera.ScreenPointToRay(Input.mousePosition);
				float num2 = base.CalculateGizmoScale();
				float cylinderRadius = 0.2f * num2;
				Vector3 position = this._cameraTransform.position;
				Vector3 position2 = this._gizmoTransform.position;
				Vector3[] gizmoLocalAxes = base.GetGizmoLocalAxes();
				Vector3 cylinderAxisFirstPoint = position2;
				for (int i = 0; i < 3; i++)
				{
					bool flag = false;
					Vector3 cylinderAxisSecondPoint = position2 + gizmoLocalAxes[i] * this._axisLength * num2;
					float d;
					if (ray.IntersectsCylinder(cylinderAxisFirstPoint, cylinderAxisSecondPoint, cylinderRadius, out d))
					{
						Vector3 a = ray.origin + ray.direction * d;
						float magnitude = (a - position).magnitude;
						if (magnitude < num)
						{
							num = magnitude;
							this._selectedAxis = (GizmoAxis)i;
							flag = true;
						}
					}
					if (!flag && ray.IntersectsCone(this._arrowConeRadius, this._arrowConeLength, arrowConesWorldTransforms[i], out d))
					{
						Vector3 a2 = ray.origin + ray.direction * d;
						float magnitude2 = (a2 - position).magnitude;
						if (magnitude2 < num)
						{
							num = magnitude2;
							this._selectedAxis = (GizmoAxis)i;
						}
					}
				}
				if (!this._translateAlongCameraRightAndUpAxes)
				{
					Vector3[] array = new Vector3[]
					{
						this._gizmoTransform.forward,
						this._gizmoTransform.up,
						this._gizmoTransform.right
					};
					float[] multiAxisExtensionSigns = base.GetMultiAxisExtensionSigns(this._adjustMultiAxisForBetterVisibility);
					float num3 = Mathf.Sign(num2);
					Vector3[] array2 = new Vector3[]
					{
						this._gizmoTransform.right * multiAxisExtensionSigns[0],
						this._gizmoTransform.up * multiAxisExtensionSigns[1],
						this._gizmoTransform.right * multiAxisExtensionSigns[0],
						this._gizmoTransform.forward * multiAxisExtensionSigns[2],
						this._gizmoTransform.up * multiAxisExtensionSigns[1],
						this._gizmoTransform.forward * multiAxisExtensionSigns[2]
					};
					for (int j = 0; j < 3; j++)
					{
						Plane plane = new Plane(array[j], this._gizmoTransform.position);
						float d;
						if (plane.Raycast(ray, out d))
						{
							Vector3 a3 = ray.origin + ray.direction * d;
							Vector3 lhs = a3 - this._gizmoTransform.position;
							float num4 = Vector3.Dot(lhs, array2[j * 2]) * num3;
							float num5 = Vector3.Dot(lhs, array2[j * 2 + 1]) * num3;
							if (num4 >= 0f && num4 <= this._multiAxisSquareSize * Mathf.Abs(num2) && num5 >= 0f && num5 <= this._multiAxisSquareSize * Mathf.Abs(num2))
							{
								float magnitude3 = (a3 - position).magnitude;
								if (magnitude3 < num)
								{
									num = magnitude3;
									this._selectedMultiAxisSquare = (MultiAxisSquare)j;
									this._selectedAxis = GizmoAxis.None;
								}
							}
						}
					}
				}
				if (this._translateAlongCameraRightAndUpAxes && this.IsMouseCursorInsideCameraAxesTranslationSquare())
				{
					this._isCameraAxesTranslationSquareSelected = true;
					this._selectedAxis = GizmoAxis.None;
					this._selectedMultiAxisSquare = MultiAxisSquare.None;
				}
			}
			else if (!this._mouse.IsLeftMouseButtonDown)
			{
				this._selectedAxis = GizmoAxis.None;
				this._selectedMultiAxisSquare = MultiAxisSquare.None;
				this._isCameraAxesTranslationSquareSelected = false;
				List<GameObject> objectsForClosestVertexSelection = this.GetObjectsForClosestVertexSelection(base.ControlledObjects);
				if (objectsForClosestVertexSelection.Count != 0)
				{
					this._gizmoTransform.position = this.GetWorldPositionClosestToMouseCursorForVertexSnapping(objectsForClosestVertexSelection, false);
				}
			}
			for (int k = 0; k < 3; k++)
			{
				Material arrowConeMaterial = this.GetArrowConeMaterial((GizmoAxis)k);
				arrowConeMaterial.SetColor("_Color", (this._selectedAxis != (GizmoAxis)k) ? this._axesColors[k] : this._selectedAxisColor);
			}
			for (int l = 0; l < 3; l++)
			{
				Material material = this._multiAxisSquareMaterials[l];
				material.SetColor("_Color", (this._selectedMultiAxisSquare != (MultiAxisSquare)l) ? this._multiAxisSquareColors[l] : this._selectedMultiAxisSquareColor);
			}
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0004133C File Offset: 0x0003F53C
		private List<GameObject> GetObjectsForClosestVertexSelection(IEnumerable<GameObject> gameObjects)
		{
			if (gameObjects == null)
			{
				return new List<GameObject>();
			}
			Vector2 vector = Input.mousePosition;
			float num = float.MaxValue;
			GameObject gameObject = null;
			List<GameObject> list = new List<GameObject>();
			foreach (GameObject gameObject2 in gameObjects)
			{
				Rect screenRectangle = gameObject2.GetScreenRectangle(this._camera);
				if (screenRectangle.Contains(vector, true))
				{
					list.Add(gameObject2);
				}
				else if (list.Count == 0)
				{
					Vector2 closestPointToPoint = screenRectangle.GetClosestPointToPoint(vector);
					float magnitude = (closestPointToPoint - vector).magnitude;
					if (magnitude < num)
					{
						num = magnitude;
						gameObject = gameObject2;
					}
				}
			}
			if (list.Count == 0 && gameObject == null)
			{
				return new List<GameObject>();
			}
			if (list.Count == 0)
			{
				list.Add(gameObject);
			}
			return list;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00041454 File Offset: 0x0003F654
		private Vector3 GetWorldPositionClosestToMouseCursorForVertexSnapping(List<GameObject> gameObjects, bool considerOnlyMeshObjects)
		{
			Vector2 vector = Input.mousePosition;
			List<GameObject> list = gameObjects.FindAll((GameObject item) => item.GetMesh() != null);
			if (list.Count != 0)
			{
				MeshVertexGroupMappings instance = SingletonBase<MeshVertexGroupMappings>.Instance;
				Dictionary<GameObject, List<MeshVertexGroup>> dictionary = new Dictionary<GameObject, List<MeshVertexGroup>>();
				foreach (GameObject gameObject in gameObjects)
				{
					Mesh mesh = gameObject.GetMesh();
					if (instance.ContainsMappingForMesh(mesh))
					{
						dictionary.Add(gameObject, instance.GetMeshVertexGroups(mesh));
					}
				}
				Vector3 result = Vector3.zero;
				float num = float.MaxValue;
				float num2 = float.MaxValue;
				MeshVertexGroup meshVertexGroup = null;
				GameObject gameObject2 = null;
				bool flag = false;
				foreach (KeyValuePair<GameObject, List<MeshVertexGroup>> keyValuePair in dictionary)
				{
					GameObject key = keyValuePair.Key;
					List<MeshVertexGroup> value = keyValuePair.Value;
					Matrix4x4 localToWorldMatrix = key.transform.localToWorldMatrix;
					foreach (MeshVertexGroup meshVertexGroup2 in value)
					{
						Bounds bounds = meshVertexGroup2.GroupAABB.Transform(localToWorldMatrix);
						Rect screenRectangle = bounds.GetScreenRectangle(this._camera);
						if (screenRectangle.Contains(vector, true))
						{
							flag = true;
							List<Vector3> modelSpaceVertices = meshVertexGroup2.ModelSpaceVertices;
							foreach (Vector3 v in modelSpaceVertices)
							{
								Vector3 vector2 = localToWorldMatrix.MultiplyPoint(v);
								Vector2 a = this._camera.WorldToScreenPoint(vector2);
								float magnitude = (a - vector).magnitude;
								if (magnitude < num)
								{
									num = magnitude;
									result = vector2;
								}
							}
						}
						if (!flag)
						{
							Vector2 closestPointToPoint = screenRectangle.GetClosestPointToPoint(vector);
							float magnitude2 = (closestPointToPoint - vector).magnitude;
							if (magnitude2 < num2)
							{
								num2 = magnitude2;
								meshVertexGroup = meshVertexGroup2;
								gameObject2 = key;
							}
						}
					}
				}
				if (!flag && meshVertexGroup != null)
				{
					num = float.MaxValue;
					Matrix4x4 localToWorldMatrix2 = gameObject2.transform.localToWorldMatrix;
					List<Vector3> modelSpaceVertices2 = meshVertexGroup.ModelSpaceVertices;
					foreach (Vector3 v2 in modelSpaceVertices2)
					{
						Vector3 vector3 = localToWorldMatrix2.MultiplyPoint(v2);
						Vector2 a2 = this._camera.WorldToScreenPoint(vector3);
						float magnitude3 = (a2 - vector).magnitude;
						if (magnitude3 < num)
						{
							num = magnitude3;
							result = vector3;
						}
					}
				}
				return result;
			}
			if (!considerOnlyMeshObjects)
			{
				Vector3 result2 = Vector3.zero;
				float num3 = float.MaxValue;
				foreach (GameObject gameObject3 in gameObjects)
				{
					Vector3 position = gameObject3.transform.position;
					Vector2 a3 = this._camera.WorldToScreenPoint(position);
					float magnitude4 = (a3 - vector).magnitude;
					if (magnitude4 < num3)
					{
						num3 = magnitude4;
						result2 = position;
					}
				}
				return result2;
			}
			return Vector3.zero;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00041868 File Offset: 0x0003FA68
		protected override void OnLeftMouseButtonDown()
		{
			base.OnLeftMouseButtonDown();
			if (this._mouse.IsLeftMouseButtonDown && (this._selectedAxis != GizmoAxis.None || this._selectedMultiAxisSquare != MultiAxisSquare.None || this._isCameraAxesTranslationSquareSelected))
			{
				Plane plane;
				if (this._selectedAxis != GizmoAxis.None)
				{
					plane = base.GetCoordinateSystemPlaneFromSelectedAxis();
				}
				else if (this._selectedMultiAxisSquare != MultiAxisSquare.None)
				{
					plane = this.GetPlaneFromSelectedMultiAxisSquare();
				}
				else
				{
					plane = this.GetCameraAxesTranslationSquarePlane();
				}
				Ray ray = this._camera.ScreenPointToRay(Input.mousePosition);
				float d;
				if (plane.Raycast(ray, out d))
				{
					this._lastGizmoPickPoint = ray.origin + ray.direction * d;
				}
			}
			this._accumulatedTranslation = Vector3.zero;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00041930 File Offset: 0x0003FB30
		protected override void OnMouseMoved()
		{
			base.OnMouseMoved();
			if (this._mouse.IsLeftMouseButtonDown)
			{
				if (this._snapSettings.IsVertexSnappingEnabled)
				{
					List<GameObject> list = this._camera.GetVisibleGameObjects();
					if (base.ControlledObjects != null)
					{
						HashSet<GameObject> controlledObjects = new HashSet<GameObject>(base.ControlledObjects);
						list = list.FindAll((GameObject item) => !controlledObjects.Contains(item));
					}
					List<GameObject> objectsForClosestVertexSelection = this.GetObjectsForClosestVertexSelection(list);
					if (objectsForClosestVertexSelection.Count != 0)
					{
						Vector3 worldPositionClosestToMouseCursorForVertexSnapping = this.GetWorldPositionClosestToMouseCursorForVertexSnapping(objectsForClosestVertexSelection, true);
						Vector3 vector = worldPositionClosestToMouseCursorForVertexSnapping - this._gizmoTransform.position;
						this._gizmoTransform.position += vector;
						this.TranslateControlledObjects(vector);
					}
				}
				else if (this._selectedAxis != GizmoAxis.None)
				{
					Vector3 vector2;
					if (this._selectedAxis == GizmoAxis.X)
					{
						vector2 = this._gizmoTransform.right;
					}
					else if (this._selectedAxis == GizmoAxis.Y)
					{
						vector2 = this._gizmoTransform.up;
					}
					else
					{
						vector2 = this._gizmoTransform.forward;
					}
					Plane coordinateSystemPlaneFromSelectedAxis = base.GetCoordinateSystemPlaneFromSelectedAxis();
					Ray ray = this._camera.ScreenPointToRay(Input.mousePosition);
					float d;
					if (coordinateSystemPlaneFromSelectedAxis.Raycast(ray, out d))
					{
						Vector3 vector3 = ray.origin + ray.direction * d;
						Vector3 lhs = vector3 - this._lastGizmoPickPoint;
						float num = Vector3.Dot(lhs, vector2);
						this._lastGizmoPickPoint = vector3;
						if (this._snapSettings.IsStepSnappingEnabled)
						{
							int selectedAxis = (int)this._selectedAxis;
							ref Vector3 ptr = ref this._accumulatedTranslation;
							int index2;
							int index = index2 = selectedAxis;
							float num2 = ptr[index2];
							this._accumulatedTranslation[index] = num2 + num;
							if (Mathf.Abs(this._accumulatedTranslation[selectedAxis]) >= this._snapSettings.StepValueInWorldUnits)
							{
								float num3 = (float)((int)Mathf.Abs(this._accumulatedTranslation[selectedAxis] / this._snapSettings.StepValueInWorldUnits));
								float d2 = this._snapSettings.StepValueInWorldUnits * num3 * Mathf.Sign(this._accumulatedTranslation[selectedAxis]);
								Vector3 vector4 = vector2 * d2;
								this._gizmoTransform.position += vector4;
								this.TranslateControlledObjects(vector4);
								if (this._accumulatedTranslation[selectedAxis] > 0f)
								{
									ref Vector3 ptr2 = ref this._accumulatedTranslation;
									int index3 = index2 = selectedAxis;
									num2 = ptr2[index2];
									this._accumulatedTranslation[index3] = num2 - this._snapSettings.StepValueInWorldUnits * num3;
								}
								else if (this._accumulatedTranslation[selectedAxis] < 0f)
								{
									ref Vector3 ptr3 = ref this._accumulatedTranslation;
									int index4 = index2 = selectedAxis;
									num2 = ptr3[index2];
									this._accumulatedTranslation[index4] = num2 + this._snapSettings.StepValueInWorldUnits * num3;
								}
							}
						}
						else
						{
							Vector3 vector5 = vector2 * num;
							this._gizmoTransform.position += vector5;
							this.TranslateControlledObjects(vector5);
						}
					}
				}
				else if (this._selectedMultiAxisSquare != MultiAxisSquare.None)
				{
					float[] multiAxisExtensionSigns = base.GetMultiAxisExtensionSigns(this._adjustMultiAxisForBetterVisibility);
					Vector3 vector6;
					Vector3 vector7;
					int num4;
					int num5;
					if (this._selectedMultiAxisSquare == MultiAxisSquare.XY)
					{
						vector6 = this._gizmoTransform.right * multiAxisExtensionSigns[0];
						vector7 = this._gizmoTransform.up * multiAxisExtensionSigns[1];
						num4 = 0;
						num5 = 1;
					}
					else if (this._selectedMultiAxisSquare == MultiAxisSquare.XZ)
					{
						vector6 = this._gizmoTransform.right * multiAxisExtensionSigns[0];
						vector7 = this._gizmoTransform.forward * multiAxisExtensionSigns[2];
						num4 = 0;
						num5 = 2;
					}
					else
					{
						vector6 = this._gizmoTransform.up * multiAxisExtensionSigns[1];
						vector7 = this._gizmoTransform.forward * multiAxisExtensionSigns[2];
						num4 = 1;
						num5 = 2;
					}
					Plane planeFromSelectedMultiAxisSquare = this.GetPlaneFromSelectedMultiAxisSquare();
					Ray ray2 = this._camera.ScreenPointToRay(Input.mousePosition);
					float d3;
					if (planeFromSelectedMultiAxisSquare.Raycast(ray2, out d3))
					{
						Vector3 vector8 = ray2.origin + ray2.direction * d3;
						Vector3 lhs2 = vector8 - this._lastGizmoPickPoint;
						float num6 = Vector3.Dot(lhs2, vector6);
						float num7 = Vector3.Dot(lhs2, vector7);
						this._lastGizmoPickPoint = vector8;
						if (this._snapSettings.IsStepSnappingEnabled)
						{
							ref Vector3 ptr4 = ref this._accumulatedTranslation;
							int index2;
							int index5 = index2 = num4;
							float num2 = ptr4[index2];
							this._accumulatedTranslation[index5] = num2 + num6;
							ref Vector3 ptr5 = ref this._accumulatedTranslation;
							int index6 = index2 = num5;
							num2 = ptr5[index2];
							this._accumulatedTranslation[index6] = num2 + num7;
							Vector3 vector9 = Vector3.zero;
							if (Mathf.Abs(this._accumulatedTranslation[num4]) >= this._snapSettings.StepValueInWorldUnits)
							{
								float num8 = (float)((int)Mathf.Abs(this._accumulatedTranslation[num4] / this._snapSettings.StepValueInWorldUnits));
								float d4 = this._snapSettings.StepValueInWorldUnits * num8 * Mathf.Sign(this._accumulatedTranslation[num4]);
								vector9 += vector6 * d4;
								if (this._accumulatedTranslation[num4] > 0f)
								{
									ref Vector3 ptr6 = ref this._accumulatedTranslation;
									int index7 = index2 = num4;
									num2 = ptr6[index2];
									this._accumulatedTranslation[index7] = num2 - this._snapSettings.StepValueInWorldUnits * num8;
								}
								else if (this._accumulatedTranslation[num4] < 0f)
								{
									ref Vector3 ptr7 = ref this._accumulatedTranslation;
									int index8 = index2 = num4;
									num2 = ptr7[index2];
									this._accumulatedTranslation[index8] = num2 + this._snapSettings.StepValueInWorldUnits * num8;
								}
							}
							if (Mathf.Abs(this._accumulatedTranslation[num5]) >= this._snapSettings.StepValueInWorldUnits)
							{
								float num9 = (float)((int)Mathf.Abs(this._accumulatedTranslation[num5] / this._snapSettings.StepValueInWorldUnits));
								float d5 = this._snapSettings.StepValueInWorldUnits * num9 * Mathf.Sign(this._accumulatedTranslation[num5]);
								vector9 += vector7 * d5;
								if (this._accumulatedTranslation[num5] > 0f)
								{
									ref Vector3 ptr8 = ref this._accumulatedTranslation;
									int index9 = index2 = num5;
									num2 = ptr8[index2];
									this._accumulatedTranslation[index9] = num2 - this._snapSettings.StepValueInWorldUnits * num9;
								}
								else if (this._accumulatedTranslation[num5] < 0f)
								{
									ref Vector3 ptr9 = ref this._accumulatedTranslation;
									int index10 = index2 = num5;
									num2 = ptr9[index2];
									this._accumulatedTranslation[index10] = num2 + this._snapSettings.StepValueInWorldUnits * num9;
								}
							}
							this._gizmoTransform.position += vector9;
							this.TranslateControlledObjects(vector9);
						}
						else
						{
							Vector3 vector10 = num6 * vector6 + num7 * vector7;
							this._gizmoTransform.position += vector10;
							this.TranslateControlledObjects(vector10);
						}
					}
				}
				else if (this._translateAlongCameraRightAndUpAxes && this._isCameraAxesTranslationSquareSelected)
				{
					Plane cameraAxesTranslationSquarePlane = this.GetCameraAxesTranslationSquarePlane();
					Ray ray3 = this._camera.ScreenPointToRay(Input.mousePosition);
					float d6;
					if (cameraAxesTranslationSquarePlane.Raycast(ray3, out d6))
					{
						Vector3 vector11 = ray3.origin + ray3.direction * d6;
						Vector3 lhs3 = vector11 - this._lastGizmoPickPoint;
						float num10 = Vector3.Dot(lhs3, this._cameraTransform.right);
						float num11 = Vector3.Dot(lhs3, this._cameraTransform.up);
						this._lastGizmoPickPoint = vector11;
						if (this._snapSettings.IsStepSnappingEnabled)
						{
							ref Vector3 ptr10 = ref this._accumulatedTranslation;
							int index2;
							int index11 = index2 = 0;
							float num2 = ptr10[index2];
							this._accumulatedTranslation[index11] = num2 + num10;
							ref Vector3 ptr11 = ref this._accumulatedTranslation;
							int index12 = index2 = 1;
							num2 = ptr11[index2];
							this._accumulatedTranslation[index12] = num2 + num11;
							Vector3 vector12 = Vector3.zero;
							if (Mathf.Abs(this._accumulatedTranslation[0]) >= this._snapSettings.StepValueInWorldUnits)
							{
								float num12 = (float)((int)Mathf.Abs(this._accumulatedTranslation[0] / this._snapSettings.StepValueInWorldUnits));
								float d7 = this._snapSettings.StepValueInWorldUnits * num12 * Mathf.Sign(this._accumulatedTranslation[0]);
								vector12 += this._cameraTransform.right * d7;
								if (this._accumulatedTranslation[0] > 0f)
								{
									ref Vector3 ptr12 = ref this._accumulatedTranslation;
									int index13 = index2 = 0;
									num2 = ptr12[index2];
									this._accumulatedTranslation[index13] = num2 - this._snapSettings.StepValueInWorldUnits * num12;
								}
								else if (this._accumulatedTranslation[0] < 0f)
								{
									ref Vector3 ptr13 = ref this._accumulatedTranslation;
									int index14 = index2 = 0;
									num2 = ptr13[index2];
									this._accumulatedTranslation[index14] = num2 + this._snapSettings.StepValueInWorldUnits * num12;
								}
							}
							if (Mathf.Abs(this._accumulatedTranslation[1]) >= this._snapSettings.StepValueInWorldUnits)
							{
								float num13 = (float)((int)Mathf.Abs(this._accumulatedTranslation[1] / this._snapSettings.StepValueInWorldUnits));
								float d8 = this._snapSettings.StepValueInWorldUnits * num13 * Mathf.Sign(this._accumulatedTranslation[1]);
								vector12 += this._cameraTransform.up * d8;
								if (this._accumulatedTranslation[1] > 0f)
								{
									ref Vector3 ptr14 = ref this._accumulatedTranslation;
									int index15 = index2 = 1;
									num2 = ptr14[index2];
									this._accumulatedTranslation[index15] = num2 - this._snapSettings.StepValueInWorldUnits * num13;
								}
								else if (this._accumulatedTranslation[1] < 0f)
								{
									ref Vector3 ptr15 = ref this._accumulatedTranslation;
									int index16 = index2 = 1;
									num2 = ptr15[index2];
									this._accumulatedTranslation[index16] = num2 + this._snapSettings.StepValueInWorldUnits * num13;
								}
							}
							this._gizmoTransform.position += vector12;
							this.TranslateControlledObjects(vector12);
						}
						else
						{
							Vector3 vector13 = this._cameraTransform.right * num10 + this._cameraTransform.up * num11;
							this._gizmoTransform.position += vector13;
							this.TranslateControlledObjects(vector13);
						}
					}
				}
			}
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x000423E0 File Offset: 0x000405E0
		protected override void OnRenderObject()
		{
			base.OnRenderObject();
			float num = base.CalculateGizmoScale();
			int[] sortedGizmoAxesIndices = base.GetSortedGizmoAxesIndices();
			Vector3[] gizmoLocalAxes = base.GetGizmoLocalAxes();
			Vector3 position = this._gizmoTransform.position;
			foreach (int num2 in sortedGizmoAxesIndices)
			{
				Color lineColor = (this._selectedAxis != (GizmoAxis)num2) ? this._axesColors[num2] : this._selectedAxisColor;
				Vector3 vector = position + gizmoLocalAxes[num2] * this._axisLength * num;
				base.UpdateShaderStencilRefValuesForGizmoAxisLineDraw(num2, position, vector, num);
				GLPrimitives.Draw3DLine(position, vector, lineColor, this._lineRenderingMaterial);
			}
			if (!this._translateAlongCameraRightAndUpAxes && !this._snapSettings.IsVertexSnappingEnabled)
			{
				Vector3[] linePoints;
				Color[] lineColors;
				this.GetMultiAxisSquaresLinePointsAndColors(num, out linePoints, out lineColors);
				GLPrimitives.Draw3DLines(linePoints, lineColors, false, this._lineRenderingMaterial, false, Color.black);
			}
			if (this._translateAlongCameraRightAndUpAxes)
			{
				Color borderLineColor = (!this._isCameraAxesTranslationSquareSelected) ? this._colorOfCameraAxesTranslationSquareLines : this._colorOfCameraAxesTranslationSquareLinesWhenSelected;
				GLPrimitives.Draw2DRectangleBorderLines(this.GetCameraAxesTranslationSquareScreenPoints(), borderLineColor, this._lineRenderingMaterial);
			}
			if (this._snapSettings.IsVertexSnappingEnabled)
			{
				Color borderLineColor2 = (!this._mouse.IsLeftMouseButtonDown) ? this._colorOfVertexSnappingSquareLines : this._colorOfVertexSnappingSquareLinesWhenSelected;
				GLPrimitives.Draw2DRectangleBorderLines(this.GetVertexSnappingSquareScreenPoints(), borderLineColor2, this._lineRenderingMaterial);
			}
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00042564 File Offset: 0x00040764
		private void GetMultiAxisSquaresLinePointsAndColors(float gizmoScale, out Vector3[] squareLinesPoints, out Color[] squareLinesColors)
		{
			float d = (this._multiAxisSquareSize + 0.001f) * gizmoScale;
			squareLinesPoints = new Vector3[24];
			squareLinesColors = new Color[12];
			Vector3[] worldAxesUsedToDrawMultiAxisSquareLines = this.GetWorldAxesUsedToDrawMultiAxisSquareLines();
			for (int i = 0; i < 3; i++)
			{
				Color color = (this._selectedMultiAxisSquare != (MultiAxisSquare)i) ? this._multiAxisSquareLineColors[i] : this._selectedMultiAxisSquareLineColor;
				int num = i * 4;
				squareLinesColors[num] = color;
				squareLinesColors[num + 1] = color;
				squareLinesColors[num + 2] = color;
				squareLinesColors[num + 3] = color;
				int num2 = i * 2;
				Vector3 position = this._gizmoTransform.position;
				Vector3 vector = position + worldAxesUsedToDrawMultiAxisSquareLines[num2 + 1] * d;
				Vector3 vector2 = vector + worldAxesUsedToDrawMultiAxisSquareLines[num2] * d;
				Vector3 vector3 = position + worldAxesUsedToDrawMultiAxisSquareLines[num2] * d;
				int num3 = i * 8;
				squareLinesPoints[num3] = position;
				squareLinesPoints[num3 + 1] = vector;
				squareLinesPoints[num3 + 2] = vector;
				squareLinesPoints[num3 + 3] = vector2;
				squareLinesPoints[num3 + 4] = vector2;
				squareLinesPoints[num3 + 5] = vector3;
				squareLinesPoints[num3 + 6] = vector3;
				squareLinesPoints[num3 + 7] = position;
			}
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00042718 File Offset: 0x00040918
		private Vector2[] GetCameraAxesTranslationSquareScreenPoints()
		{
			Vector2 a = this._camera.WorldToScreenPoint(this._gizmoTransform.position);
			float d = this._screenSizeOfCameraAxesTranslationSquare * 0.5f;
			return new Vector2[]
			{
				a - (Vector2.right - Vector2.up) * d,
				a + (Vector2.right + Vector2.up) * d,
				a + (Vector2.right - Vector2.up) * d,
				a - (Vector2.right + Vector2.up) * d
			};
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x000427F0 File Offset: 0x000409F0
		private Vector2[] GetVertexSnappingSquareScreenPoints()
		{
			Vector2 a = this._camera.WorldToScreenPoint(this._gizmoTransform.position);
			float d = this._screenSizeOfVertexSnappingSquare * 0.5f;
			return new Vector2[]
			{
				a - (Vector2.right - Vector2.up) * d,
				a + (Vector2.right + Vector2.up) * d,
				a + (Vector2.right - Vector2.up) * d,
				a - (Vector2.right + Vector2.up) * d
			};
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x000428C8 File Offset: 0x00040AC8
		private bool IsMouseCursorInsideCameraAxesTranslationSquare()
		{
			Vector2 b = this._camera.WorldToScreenPoint(this._gizmoTransform.position);
			float num = this._screenSizeOfCameraAxesTranslationSquare * 0.5f;
			Vector2 a = Input.mousePosition;
			Vector2 vector = a - b;
			return Mathf.Abs(vector.x) <= num && Mathf.Abs(vector.y) <= num;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00042938 File Offset: 0x00040B38
		private void DrawMultiAxisSquares(Matrix4x4[] worldTransformMatrices)
		{
			for (int i = 0; i < 3; i++)
			{
				Graphics.DrawMesh(this._multiAxisSquareMesh, worldTransformMatrices[i], this._multiAxisSquareMaterials[i], base.gameObject.layer);
			}
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00042984 File Offset: 0x00040B84
		private Matrix4x4[] GetMultiAxisSquaresWorldTransforms()
		{
			Matrix4x4[] array = new Matrix4x4[3];
			Vector3[] multiAxisSquaresGizmoLocalPositions = this.GetMultiAxisSquaresGizmoLocalPositions();
			Quaternion[] multiAxisSquaresGizmoLocalRotations = this.GetMultiAxisSquaresGizmoLocalRotations();
			float d = base.CalculateGizmoScale();
			for (int i = 0; i < 3; i++)
			{
				Vector3 pos = this._gizmoTransform.position + this._gizmoTransform.rotation * multiAxisSquaresGizmoLocalPositions[i] * d;
				Quaternion q = this._gizmoTransform.rotation * multiAxisSquaresGizmoLocalRotations[i];
				array[i] = default(Matrix4x4);
				array[i].SetTRS(pos, q, this._gizmoTransform.lossyScale);
			}
			return array;
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00042A4C File Offset: 0x00040C4C
		private Quaternion[] GetMultiAxisSquaresGizmoLocalRotations()
		{
			return new Quaternion[]
			{
				Quaternion.identity,
				Quaternion.Euler(90f, 0f, 0f),
				Quaternion.Euler(0f, 90f, 0f)
			};
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00042AB0 File Offset: 0x00040CB0
		private Vector3[] GetMultiAxisSquaresGizmoLocalPositions()
		{
			float d = this._multiAxisSquareSize * 0.5f;
			float[] multiAxisExtensionSigns = base.GetMultiAxisExtensionSigns(this._adjustMultiAxisForBetterVisibility);
			return new Vector3[]
			{
				(Vector3.right * multiAxisExtensionSigns[0] + Vector3.up * multiAxisExtensionSigns[1]) * d,
				(Vector3.right * multiAxisExtensionSigns[0] + Vector3.forward * multiAxisExtensionSigns[2]) * d,
				(Vector3.up * multiAxisExtensionSigns[1] + Vector3.forward * multiAxisExtensionSigns[2]) * d
			};
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00042B70 File Offset: 0x00040D70
		private Vector3[] GetWorldAxesUsedToDrawMultiAxisSquareLines()
		{
			float[] multiAxisExtensionSigns = base.GetMultiAxisExtensionSigns(this._adjustMultiAxisForBetterVisibility);
			return new Vector3[]
			{
				this._gizmoTransform.right * multiAxisExtensionSigns[0],
				this._gizmoTransform.up * multiAxisExtensionSigns[1],
				this._gizmoTransform.right * multiAxisExtensionSigns[0],
				this._gizmoTransform.forward * multiAxisExtensionSigns[2],
				this._gizmoTransform.up * multiAxisExtensionSigns[1],
				this._gizmoTransform.forward * multiAxisExtensionSigns[2]
			};
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00042C4C File Offset: 0x00040E4C
		private void CreateArrowConeMaterials()
		{
			Shader shader = Shader.Find("Gizmo Lit Mesh");
			Shader shader2 = Shader.Find("Gizmo Unlit Mesh");
			for (int i = 0; i < 3; i++)
			{
				this._litArrowConeMaterials[i] = new Material(shader);
				this._litArrowConeMaterials[i].SetInt("_StencilRefValue", this._axesStencilRefValues[i]);
				this._unlitArrowConeMaterials[i] = new Material(shader2);
				this._unlitArrowConeMaterials[i].SetInt("_StencilRefValue", this._axesStencilRefValues[i]);
			}
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00042CD4 File Offset: 0x00040ED4
		private Material GetArrowConeMaterial(GizmoAxis gizmoAxis)
		{
			if (this._areArrowConesLit)
			{
				return this._litArrowConeMaterials[(int)gizmoAxis];
			}
			return this._unlitArrowConeMaterials[(int)gizmoAxis];
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00042CF4 File Offset: 0x00040EF4
		private void CreateMultiAxisSquareMaterials()
		{
			Shader shader = Shader.Find("Gizmo Unlit Mesh");
			for (int i = 0; i < 3; i++)
			{
				this._multiAxisSquareMaterials[i] = new Material(shader);
			}
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00042D2C File Offset: 0x00040F2C
		private void CreateMultiAxisSquareMesh()
		{
			if (this._multiAxisSquareMesh != null)
			{
				base.DestroyGizmoMesh(this._multiAxisSquareMesh);
			}
			this._multiAxisSquareMesh = ProceduralMeshGenerator.CreatePlaneMesh(this._multiAxisSquareSize, this._multiAxisSquareSize);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00042D70 File Offset: 0x00040F70
		private void CreateArrowConeMesh()
		{
			if (this._arrowConeMesh != null)
			{
				base.DestroyGizmoMesh(this._arrowConeMesh);
			}
			this._arrowConeMesh = ProceduralMeshGenerator.CreateConeMesh(this._arrowConeRadius, this._arrowConeLength, 20, 20, 5);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00042DB8 File Offset: 0x00040FB8
		private Plane GetPlaneFromSelectedMultiAxisSquare()
		{
			switch (this._selectedMultiAxisSquare)
			{
			case MultiAxisSquare.XY:
				return new Plane(this._gizmoTransform.forward, this._gizmoTransform.position);
			case MultiAxisSquare.XZ:
				return new Plane(this._gizmoTransform.up, this._gizmoTransform.position);
			case MultiAxisSquare.YZ:
				return new Plane(this._gizmoTransform.right, this._gizmoTransform.position);
			default:
				return default(Plane);
			}
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00042E40 File Offset: 0x00041040
		private Plane GetCameraAxesTranslationSquarePlane()
		{
			return new Plane(this._cameraTransform.forward, this._gizmoTransform.position);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00042E60 File Offset: 0x00041060
		private void TranslateControlledObjects(Vector3 translationVector)
		{
			if (base.ControlledObjects != null)
			{
				List<GameObject> topParentsFromControlledObjects = base.GetTopParentsFromControlledObjects();
				if (topParentsFromControlledObjects.Count != 0)
				{
					foreach (GameObject gameObject in topParentsFromControlledObjects)
					{
						if (gameObject != null)
						{
							gameObject.transform.position += translationVector;
						}
					}
					this._objectsWereTransformedSinceLeftMouseButtonWasPressed = true;
				}
			}
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00042F04 File Offset: 0x00041104
		private void DrawArrowCones(Matrix4x4[] worldTransformMatrices)
		{
			for (int i = 0; i < 3; i++)
			{
				Graphics.DrawMesh(this._arrowConeMesh, worldTransformMatrices[i], this.GetArrowConeMaterial((GizmoAxis)i), base.gameObject.layer);
			}
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00042F4C File Offset: 0x0004114C
		private Matrix4x4[] GetArrowConesWorldTransforms()
		{
			Matrix4x4[] array = new Matrix4x4[3];
			Vector3[] arrowConesGizmoLocalPositions = this.GetArrowConesGizmoLocalPositions();
			Quaternion[] arrowConesGizmoLocalRotations = this.GetArrowConesGizmoLocalRotations();
			float d = base.CalculateGizmoScale();
			for (int i = 0; i < 3; i++)
			{
				Vector3 pos = this._gizmoTransform.position + this._gizmoTransform.rotation * arrowConesGizmoLocalPositions[i] * d;
				Quaternion q = this._gizmoTransform.rotation * arrowConesGizmoLocalRotations[i];
				array[i] = default(Matrix4x4);
				array[i].SetTRS(pos, q, this._gizmoTransform.lossyScale);
			}
			return array;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00043014 File Offset: 0x00041214
		private Vector3[] GetArrowConesGizmoLocalPositions()
		{
			return new Vector3[]
			{
				Vector3.right * this._axisLength,
				Vector3.up * this._axisLength,
				Vector3.forward * this._axisLength
			};
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0004307C File Offset: 0x0004127C
		private Quaternion[] GetArrowConesGizmoLocalRotations()
		{
			return new Quaternion[]
			{
				Quaternion.Euler(0f, 0f, -90f),
				Quaternion.identity,
				Quaternion.Euler(90f, 0f, 0f)
			};
		}

		// Token: 0x04000783 RID: 1923
		[SerializeField]
		private float _axisLength = 5f;

		// Token: 0x04000784 RID: 1924
		[SerializeField]
		private float _arrowConeRadius = 0.4f;

		// Token: 0x04000785 RID: 1925
		[SerializeField]
		private float _arrowConeLength = 1.19f;

		// Token: 0x04000786 RID: 1926
		[SerializeField]
		private float _multiAxisSquareSize = 1f;

		// Token: 0x04000787 RID: 1927
		[SerializeField]
		private bool _adjustMultiAxisForBetterVisibility = true;

		// Token: 0x04000788 RID: 1928
		[SerializeField]
		private Color[] _multiAxisSquareColors = new Color[]
		{
			new Color(0f, 0f, 1f, 0.2f),
			new Color(0f, 1f, 0f, 0.2f),
			new Color(1f, 0f, 0f, 0.2f)
		};

		// Token: 0x04000789 RID: 1929
		[SerializeField]
		private Color[] _multiAxisSquareLineColors = new Color[]
		{
			new Color(0f, 0f, 1f, 1f),
			new Color(0f, 1f, 0f, 1f),
			new Color(1f, 0f, 0f, 1f)
		};

		// Token: 0x0400078A RID: 1930
		[SerializeField]
		private Color _selectedMultiAxisSquareColor = new Color(1f, 1f, 0f, 0.2f);

		// Token: 0x0400078B RID: 1931
		[SerializeField]
		private Color _selectedMultiAxisSquareLineColor = new Color(1f, 1f, 0f, 1f);

		// Token: 0x0400078C RID: 1932
		[SerializeField]
		private bool _areArrowConesLit = true;

		// Token: 0x0400078D RID: 1933
		[SerializeField]
		private bool _translateAlongCameraRightAndUpAxes;

		// Token: 0x0400078E RID: 1934
		[SerializeField]
		private float _screenSizeOfCameraAxesTranslationSquare = 25f;

		// Token: 0x0400078F RID: 1935
		[SerializeField]
		private Color _colorOfCameraAxesTranslationSquareLines = Color.white;

		// Token: 0x04000790 RID: 1936
		[SerializeField]
		private Color _colorOfCameraAxesTranslationSquareLinesWhenSelected = Color.yellow;

		// Token: 0x04000791 RID: 1937
		[SerializeField]
		private float _screenSizeOfVertexSnappingSquare = 25f;

		// Token: 0x04000792 RID: 1938
		[SerializeField]
		private Color _colorOfVertexSnappingSquareLines = Color.white;

		// Token: 0x04000793 RID: 1939
		[SerializeField]
		private Color _colorOfVertexSnappingSquareLinesWhenSelected = Color.yellow;

		// Token: 0x04000794 RID: 1940
		private TranslationGizmoSnapSettings _snapSettings = new TranslationGizmoSnapSettings();

		// Token: 0x04000795 RID: 1941
		private Vector3 _accumulatedTranslation;

		// Token: 0x04000796 RID: 1942
		private Material[] _litArrowConeMaterials = new Material[3];

		// Token: 0x04000797 RID: 1943
		private Material[] _unlitArrowConeMaterials = new Material[3];

		// Token: 0x04000798 RID: 1944
		private Material[] _multiAxisSquareMaterials = new Material[3];

		// Token: 0x04000799 RID: 1945
		private Mesh _arrowConeMesh;

		// Token: 0x0400079A RID: 1946
		private Mesh _multiAxisSquareMesh;

		// Token: 0x0400079B RID: 1947
		private MultiAxisSquare _selectedMultiAxisSquare = MultiAxisSquare.None;

		// Token: 0x0400079C RID: 1948
		private bool _isCameraAxesTranslationSquareSelected;
	}
}
