using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001AF RID: 431
	public class RotationGizmo : Gizmo
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0003CC84 File Offset: 0x0003AE84
		public static float MinCameraLookRotationCircleRadiusScale
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0003CC8C File Offset: 0x0003AE8C
		public static float MinRotationSphereRadius
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0003CC94 File Offset: 0x0003AE94
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x0003CC9C File Offset: 0x0003AE9C
		public float RotationSphereRadius
		{
			get
			{
				return this._rotationSphereRadius;
			}
			set
			{
				this._rotationSphereRadius = Mathf.Max(RotationGizmo.MinRotationSphereRadius, value);
				if (Application.isPlaying)
				{
					this.CalculateRotationCirclePointsInGizmoLocalSpace();
					this.CreateRotationSphereMesh();
				}
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0003CCC8 File Offset: 0x0003AEC8
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x0003CCD0 File Offset: 0x0003AED0
		public Color RotationSphereColor
		{
			get
			{
				return this._rotationSphereColor;
			}
			set
			{
				this._rotationSphereColor = value;
				if (Application.isPlaying)
				{
					this.AssignRotationSphereColorToSphereMaterials();
				}
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0003CCEC File Offset: 0x0003AEEC
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x0003CCF4 File Offset: 0x0003AEF4
		public bool IsRotationSphereLit
		{
			get
			{
				return this._isRotationSphereLit;
			}
			set
			{
				this._isRotationSphereLit = value;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0003CD00 File Offset: 0x0003AF00
		// (set) Token: 0x060009BA RID: 2490 RVA: 0x0003CD08 File Offset: 0x0003AF08
		public bool ShowRotationGuide
		{
			get
			{
				return this._showRotationGuide;
			}
			set
			{
				this._showRotationGuide = value;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x0003CD14 File Offset: 0x0003AF14
		// (set) Token: 0x060009BC RID: 2492 RVA: 0x0003CD1C File Offset: 0x0003AF1C
		public Color RotationGuieLineColor
		{
			get
			{
				return this._rotationGuieLineColor;
			}
			set
			{
				this._rotationGuieLineColor = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x0003CD28 File Offset: 0x0003AF28
		// (set) Token: 0x060009BE RID: 2494 RVA: 0x0003CD30 File Offset: 0x0003AF30
		public Color RotationGuideDiscColor
		{
			get
			{
				return this._rotationGuideDiscColor;
			}
			set
			{
				this._rotationGuideDiscColor = value;
				if (Application.isPlaying)
				{
					this._rotationGuideDiscMaterial.SetColor("_Color", this._rotationGuideDiscColor);
				}
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x0003CD5C File Offset: 0x0003AF5C
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x0003CD64 File Offset: 0x0003AF64
		public bool ShowSphereBoundary
		{
			get
			{
				return this._showRotationSphereBoundary;
			}
			set
			{
				this._showRotationSphereBoundary = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x0003CD70 File Offset: 0x0003AF70
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x0003CD78 File Offset: 0x0003AF78
		public Color SphereBoundaryLineColor
		{
			get
			{
				return this._rotationSphereBoundaryLineColor;
			}
			set
			{
				this._rotationSphereBoundaryLineColor = value;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x0003CD84 File Offset: 0x0003AF84
		// (set) Token: 0x060009C4 RID: 2500 RVA: 0x0003CD8C File Offset: 0x0003AF8C
		public bool ShowCameraLookRotationCircle
		{
			get
			{
				return this._showCameraLookRotationCircle;
			}
			set
			{
				this._showCameraLookRotationCircle = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0003CD98 File Offset: 0x0003AF98
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x0003CDA0 File Offset: 0x0003AFA0
		public float CameraLookRotationCircleRadiusScale
		{
			get
			{
				return this._cameraLookRotationCircleRadiusScale;
			}
			set
			{
				this._cameraLookRotationCircleRadiusScale = Mathf.Max(RotationGizmo.MinCameraLookRotationCircleRadiusScale, value);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0003CDB4 File Offset: 0x0003AFB4
		// (set) Token: 0x060009C8 RID: 2504 RVA: 0x0003CDBC File Offset: 0x0003AFBC
		public Color CameraLookRotationCircleLineColor
		{
			get
			{
				return this._cameraLookRotationCircleLineColor;
			}
			set
			{
				this._cameraLookRotationCircleLineColor = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x0003CDC8 File Offset: 0x0003AFC8
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x0003CDD0 File Offset: 0x0003AFD0
		public Color CameraLookRotationCircleColorWhenSelected
		{
			get
			{
				return this._cameraLookRotationCircleColorWhenSelected;
			}
			set
			{
				this._cameraLookRotationCircleColorWhenSelected = value;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x0003CDDC File Offset: 0x0003AFDC
		public RotationGizmoSnapSettings SnapSettings
		{
			get
			{
				return this._snapSettings;
			}
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0003CDE4 File Offset: 0x0003AFE4
		public override bool IsReadyForObjectManipulation()
		{
			return this._selectedAxis != GizmoAxis.None || this._isRotationSphereSelected || this._isCameraLookRotationCircleSelected;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0003CE14 File Offset: 0x0003B014
		public override GizmoType GetGizmoType()
		{
			return GizmoType.Rotation;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0003CE18 File Offset: 0x0003B018
		protected override void Start()
		{
			base.Start();
			this.CreateRotationSphereMaterials();
			this.AssignRotationSphereColorToSphereMaterials();
			this.CreateRotationSphereMesh();
			this.CreateRotationGuideDiscMaterial();
			this.CalculateRotationCirclePointsInGizmoLocalSpace();
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0003CE4C File Offset: 0x0003B04C
		protected override void Update()
		{
			base.Update();
			this.DrawRotationSphere(this.GetRotationSphereWorldTransform());
			if (this._mouse.IsLeftMouseButtonDown)
			{
				return;
			}
			this._selectedAxis = GizmoAxis.None;
			this._isCameraLookRotationCircleSelected = false;
			this._isRotationSphereSelected = false;
			Ray ray = this._camera.ScreenPointToRay(Input.mousePosition);
			float num = float.MaxValue;
			float distanceFromCameraPositionToRotationSphereCenter = this.GetDistanceFromCameraPositionToRotationSphereCenter();
			float worldSpaceRotationCircleRadius = this.GetWorldSpaceRotationCircleRadius();
			Vector3[] gizmoLocalAxes = base.GetGizmoLocalAxes();
			for (int i = 0; i < 3; i++)
			{
				Vector3 vector;
				if (this.RayIntersectsRotationCircle(ray, gizmoLocalAxes[i], worldSpaceRotationCircleRadius, out vector))
				{
					float magnitude = (ray.origin - vector).magnitude;
					if (magnitude < num && this.IsPointOnRotationCircleVisible(vector, distanceFromCameraPositionToRotationSphereCenter))
					{
						num = magnitude;
						this._selectedAxis = (GizmoAxis)i;
					}
				}
			}
			if (this._showCameraLookRotationCircle)
			{
				Vector2 rotationSphereScreenSpaceCenter = this.GetRotationSphereScreenSpaceCenter();
				float num2 = this.EstimateRotationSphereScreenSpaceBoundaryCircleRadius(rotationSphereScreenSpaceCenter) * this._cameraLookRotationCircleRadiusScale;
				Vector2 a = Input.mousePosition;
				if (Mathf.Abs((a - rotationSphereScreenSpaceCenter).magnitude - num2) <= 5f)
				{
					this._selectedAxis = GizmoAxis.None;
					this._isCameraLookRotationCircleSelected = true;
					Vector2 a2 = a - rotationSphereScreenSpaceCenter;
					a2.Normalize();
					this._cameraLookRotationCirclePickPoint = rotationSphereScreenSpaceCenter + a2 * this.EstimateRotationSphereScreenSpaceBoundaryCircleRadius(rotationSphereScreenSpaceCenter) * this._cameraLookRotationCircleRadiusScale;
				}
			}
			float num3;
			if (this._selectedAxis == GizmoAxis.None && !this._isCameraLookRotationCircleSelected && ray.IntersectsSphere(this._gizmoTransform.position, this.GetWorldSpaceRotationSphereRadius(), out num3))
			{
				this._isRotationSphereSelected = true;
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0003D00C File Offset: 0x0003B20C
		protected override void OnLeftMouseButtonDown()
		{
			base.OnLeftMouseButtonDown();
			if (this._selectedAxis != GizmoAxis.None)
			{
				Ray ray = this._camera.ScreenPointToRay(Input.mousePosition);
				Vector3 vector;
				if (this._selectedAxis == GizmoAxis.X)
				{
					vector = this._gizmoTransform.right;
				}
				else if (this._selectedAxis == GizmoAxis.Y)
				{
					vector = this._gizmoTransform.up;
				}
				else
				{
					vector = this._gizmoTransform.forward;
				}
				Vector3 vector2;
				if (this.RayIntersectsRotationCircle(ray, vector, this.GetWorldSpaceRotationCircleRadius(), out vector2))
				{
					Plane plane = new Plane(vector, this._gizmoTransform.position);
					float distanceToPoint = plane.GetDistanceToPoint(vector2);
					vector2 -= vector * distanceToPoint;
					Vector3 a = vector2 - this._gizmoTransform.position;
					a.Normalize();
					this._rotationGuideLinePoints[0] = this._gizmoTransform.position + a * this.GetWorldSpaceRotationCircleRadius();
				}
			}
			else if (this._isCameraLookRotationCircleSelected)
			{
				this._rotationGuideLinePoints[0] = this._cameraLookRotationCirclePickPoint;
			}
			this._rotationGuideLinePoints[1] = this._rotationGuideLinePoints[0];
			this._accumulatedRotation = 0f;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0003D164 File Offset: 0x0003B364
		protected override void OnMouseMoved()
		{
			base.OnMouseMoved();
			if (this._mouse.IsLeftMouseButtonDown)
			{
				float num = 0.45f;
				if (this._selectedAxis != GizmoAxis.None)
				{
					Vector3 vector;
					if (this._selectedAxis == GizmoAxis.X)
					{
						vector = this._gizmoTransform.right;
					}
					else if (this._selectedAxis == GizmoAxis.Y)
					{
						vector = this._gizmoTransform.up;
					}
					else
					{
						vector = this._gizmoTransform.forward;
					}
					Vector3 rhs = this._rotationGuideLinePoints[0] - this._gizmoTransform.position;
					Vector3 b = Vector3.Cross(vector, rhs);
					b.Normalize();
					Vector3 vector2 = this._rotationGuideLinePoints[0];
					Vector3 vector3 = vector2 + b;
					vector2 = this._camera.WorldToScreenPoint(vector2);
					vector3 = this._camera.WorldToScreenPoint(vector3);
					Vector2 lhs = vector3 - vector2;
					lhs.Normalize();
					float num2 = Vector2.Dot(lhs, this._mouse.CursorOffsetSinceLastFrame);
					float num3 = num * num2;
					if (this._snapSettings.IsSnappingEnabled)
					{
						this._accumulatedRotation += num3;
						if (Mathf.Abs(this._accumulatedRotation) >= this._snapSettings.StepValueInDegrees)
						{
							float num4 = (float)((int)Mathf.Abs(this._accumulatedRotation / this._snapSettings.StepValueInDegrees));
							float num5 = this._snapSettings.StepValueInDegrees * num4 * Mathf.Sign(num3);
							Vector3 vector4 = this._rotationGuideLinePoints[1] - this._gizmoTransform.position;
							vector4 = Quaternion.AngleAxis(num5, vector) * vector4;
							vector4.Normalize();
							this._rotationGuideLinePoints[1] = this._gizmoTransform.position + vector4 * this.GetWorldSpaceRotationCircleRadius();
							this._gizmoTransform.Rotate(vector, num5, Space.World);
							this.RotateControlledObjects(vector, num5);
							if (this._accumulatedRotation > 0f)
							{
								this._accumulatedRotation -= this._snapSettings.StepValueInDegrees * num4;
							}
							else if (this._accumulatedRotation < 0f)
							{
								this._accumulatedRotation += this._snapSettings.StepValueInDegrees * num4;
							}
						}
					}
					else
					{
						Vector3 vector5 = this._rotationGuideLinePoints[1] - this._gizmoTransform.position;
						vector5 = Quaternion.AngleAxis(num3, vector) * vector5;
						vector5.Normalize();
						this._rotationGuideLinePoints[1] = this._gizmoTransform.position + vector5 * this.GetWorldSpaceRotationCircleRadius();
						this._gizmoTransform.Rotate(vector, num3, Space.World);
						this.RotateControlledObjects(vector, num3);
					}
				}
				else if (this._isCameraLookRotationCircleSelected)
				{
					Vector2 rotationSphereScreenSpaceCenter = this.GetRotationSphereScreenSpaceCenter();
					Vector2 vector6 = this._cameraLookRotationCirclePickPoint - rotationSphereScreenSpaceCenter;
					Vector2 lhs2 = new Vector2(-vector6.y, vector6.x);
					lhs2.Normalize();
					Vector2 cursorOffsetSinceLeftMouseButtonDown = this._mouse.CursorOffsetSinceLeftMouseButtonDown;
					cursorOffsetSinceLeftMouseButtonDown.Normalize();
					float num6 = Vector2.Dot(lhs2, cursorOffsetSinceLeftMouseButtonDown);
					float angle = this._mouse.CursorOffsetSinceLeftMouseButtonDown.magnitude * num * num6;
					Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
					Vector2 vector7 = vector6;
					vector7 = rotation * vector7;
					vector7.Normalize();
					Vector2 cursorOffsetSinceLastFrame = this._mouse.CursorOffsetSinceLastFrame;
					cursorOffsetSinceLastFrame.Normalize();
					float num7 = Vector2.Dot(lhs2, cursorOffsetSinceLastFrame);
					float num8 = this._mouse.CursorOffsetSinceLastFrame.magnitude * num7 * num;
					this._gizmoTransform.Rotate(this._cameraTransform.forward, num8, Space.World);
					this._rotationGuideLinePoints[1] = rotationSphereScreenSpaceCenter + vector7 * this.EstimateRotationSphereScreenSpaceBoundaryCircleRadius(rotationSphereScreenSpaceCenter) * this._cameraLookRotationCircleRadiusScale;
					this.RotateControlledObjects(this._cameraTransform.forward, num8);
				}
				else if (this._isRotationSphereSelected)
				{
					Vector2 lhs3 = new Vector2(1f, 0f);
					Vector2 lhs4 = new Vector2(0f, 1f);
					Vector2 cursorOffsetSinceLastFrame2 = this._mouse.CursorOffsetSinceLastFrame;
					cursorOffsetSinceLastFrame2.Normalize();
					float num9 = -Vector2.Dot(lhs3, cursorOffsetSinceLastFrame2);
					float num10 = Vector2.Dot(lhs4, cursorOffsetSinceLastFrame2);
					float num11 = this._mouse.CursorOffsetSinceLastFrame.magnitude * num10 * num;
					float num12 = this._mouse.CursorOffsetSinceLastFrame.magnitude * num9 * num;
					this._gizmoTransform.Rotate(this._cameraTransform.right, num11, Space.World);
					this._gizmoTransform.Rotate(this._cameraTransform.up, num12, Space.World);
					this.RotateControlledObjects(this._cameraTransform.right, num11);
					this.RotateControlledObjects(this._cameraTransform.up, num12);
				}
			}
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0003D684 File Offset: 0x0003B884
		protected override void OnRenderObject()
		{
			base.OnRenderObject();
			if (this._mouse.IsLeftMouseButtonDown)
			{
				if (this._selectedAxis != GizmoAxis.None && this._showRotationGuide)
				{
					GLPrimitives.Draw3DLine(this._gizmoTransform.position, this._rotationGuideLinePoints[0], this._rotationGuieLineColor, this._lineRenderingMaterial);
					GLPrimitives.Draw3DLine(this._gizmoTransform.position, this._rotationGuideLinePoints[1], this._rotationGuieLineColor, this._lineRenderingMaterial);
					Vector3 discPlaneNormal = this._gizmoTransform.right;
					if (this._selectedAxis == GizmoAxis.Y)
					{
						discPlaneNormal = this._gizmoTransform.up;
					}
					else if (this._selectedAxis == GizmoAxis.Z)
					{
						discPlaneNormal = this._gizmoTransform.forward;
					}
					GLPrimitives.Draw3DFilledDisc(this._gizmoTransform.position, this._rotationGuideLinePoints[0], this._rotationGuideLinePoints[1], discPlaneNormal, this._rotationGuideDiscColor, this._rotationGuideDiscMaterial);
				}
				else if (this._isCameraLookRotationCircleSelected && this._showRotationGuide)
				{
					Vector2 rotationSphereScreenSpaceCenter = this.GetRotationSphereScreenSpaceCenter();
					GLPrimitives.Draw2DLine(rotationSphereScreenSpaceCenter, this._rotationGuideLinePoints[0], this._rotationGuieLineColor, this._lineRenderingMaterial);
					GLPrimitives.Draw2DLine(rotationSphereScreenSpaceCenter, this._rotationGuideLinePoints[1], this._rotationGuieLineColor, this._lineRenderingMaterial);
					GLPrimitives.Draw2DFilledDisc(rotationSphereScreenSpaceCenter, this._rotationGuideLinePoints[0], this._rotationGuideLinePoints[1], this._rotationGuideDiscColor, this._rotationGuideDiscMaterial);
				}
			}
			this.DrawRotationCircle(GizmoAxis.X);
			this.DrawRotationCircle(GizmoAxis.Y);
			this.DrawRotationCircle(GizmoAxis.Z);
			if ((this._showRotationSphereBoundary || this._showCameraLookRotationCircle) && this.IsGizmoVisible())
			{
				Vector3 circleCenter = this.GetRotationSphereScreenSpaceCenter();
				Vector3[] rotationSphereScreenSpaceBoundaryPoints = this.GetRotationSphereScreenSpaceBoundaryPoints();
				if (this._showRotationSphereBoundary)
				{
					GLPrimitives.Draw2DCircleBorderLines(rotationSphereScreenSpaceBoundaryPoints, circleCenter, this._rotationSphereBoundaryLineColor, 1f, this._lineRenderingMaterial);
				}
				if (this._showCameraLookRotationCircle)
				{
					Color borderLineColor = (!this._isCameraLookRotationCircleSelected) ? this._cameraLookRotationCircleLineColor : this._cameraLookRotationCircleColorWhenSelected;
					GLPrimitives.Draw2DCircleBorderLines(rotationSphereScreenSpaceBoundaryPoints, circleCenter, borderLineColor, this._cameraLookRotationCircleRadiusScale, this._lineRenderingMaterial);
				}
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0003D8F4 File Offset: 0x0003BAF4
		private bool IsGizmoVisible()
		{
			return !this.IsGizmoInFrontOfCameraNearClipPlane() && !this.IsGizmoInFrontOfCameraFarClipPlane();
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0003D910 File Offset: 0x0003BB10
		private bool IsGizmoInFrontOfCameraNearClipPlane()
		{
			Vector3 inPt = this._gizmoTransform.position + this._cameraTransform.forward * this.GetWorldSpaceRotationSphereRadius();
			Plane plane = new Plane(-this._cameraTransform.forward, this._cameraTransform.position + this._cameraTransform.forward * this._camera.nearClipPlane);
			return plane.GetDistanceToPoint(inPt) > 0f;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0003D998 File Offset: 0x0003BB98
		private bool IsGizmoInFrontOfCameraFarClipPlane()
		{
			Vector3 inPt = this._gizmoTransform.position - this._cameraTransform.forward * this.GetWorldSpaceRotationSphereRadius();
			Plane plane = new Plane(this._cameraTransform.forward, this._cameraTransform.position + this._cameraTransform.forward * this._camera.farClipPlane);
			return plane.GetDistanceToPoint(inPt) > 0f;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0003DA18 File Offset: 0x0003BC18
		private Vector3[] GetRotationCirclePointInWorldSpace(GizmoAxis gizmoAxis)
		{
			Vector3[] array;
			if (gizmoAxis == GizmoAxis.X)
			{
				array = this._rotationCirclePointsForXAxisInLocalSpace;
			}
			else if (gizmoAxis == GizmoAxis.Y)
			{
				array = this._rotationCirclePointsForYAxisInLocalSpace;
			}
			else
			{
				array = this._rotationCirclePointsForZAxisInLocalSpace;
			}
			Vector3[] array2 = new Vector3[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = this._gizmoTransform.TransformPoint(array[i]);
			}
			return array2;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0003DA94 File Offset: 0x0003BC94
		private void DrawRotationCircle(GizmoAxis gizmoAxis)
		{
			float distanceFromCameraPositionToRotationSphereCenter = this.GetDistanceFromCameraPositionToRotationSphereCenter();
			Vector3[] rotationCirclePointInWorldSpace = this.GetRotationCirclePointInWorldSpace(gizmoAxis);
			Color[] array = new Color[rotationCirclePointInWorldSpace.Length];
			Color color = (this._selectedAxis != gizmoAxis) ? this._axesColors[(int)gizmoAxis] : this._selectedAxisColor;
			for (int i = 0; i < rotationCirclePointInWorldSpace.Length; i++)
			{
				Vector3 point = rotationCirclePointInWorldSpace[i];
				bool flag = this.IsPointOnRotationCircleVisible(point, distanceFromCameraPositionToRotationSphereCenter);
				array[i] = ((!flag) ? new Color(color.r, color.g, color.b, 0f) : color);
			}
			GLPrimitives.Draw3DLines(rotationCirclePointInWorldSpace, array, true, this._lineRenderingMaterial, true, array[0]);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0003DB68 File Offset: 0x0003BD68
		private Vector3[] GetRotationSphereScreenSpaceBoundaryPoints()
		{
			Vector3 vector = this.GetRotationSphereScreenSpaceCenter();
			float num = this.EstimateRotationSphereScreenSpaceBoundaryCircleRadius(vector);
			Vector3[] array = new Vector3[100];
			float num2 = 3.6363637f;
			for (int i = 0; i < 100; i++)
			{
				float f = num2 * (float)i * 0.017453292f;
				Vector3 vector2 = new Vector3(Mathf.Sin(f) * num, Mathf.Cos(f) * num, 0f);
				vector2 += vector;
				array[i] = vector2;
			}
			return array;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0003DBF8 File Offset: 0x0003BDF8
		private Vector2 GetRotationSphereScreenSpaceCenter()
		{
			return this._camera.WorldToScreenPoint(this._gizmoTransform.position);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0003DC18 File Offset: 0x0003BE18
		private float EstimateRotationSphereScreenSpaceBoundaryCircleRadius(Vector3 screenSpaceBoundaryCircleCenter)
		{
			Vector3 vector = this._gizmoTransform.position + this._cameraTransform.up * this.GetWorldSpaceRotationSphereRadius();
			vector = this._camera.WorldToScreenPoint(vector);
			vector.z = 0f;
			return (vector - screenSpaceBoundaryCircleCenter).magnitude;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0003DC74 File Offset: 0x0003BE74
		private float GetDistanceFromCameraPositionToRotationSphereCenter()
		{
			if (this._camera.orthographic)
			{
				Plane plane = new Plane(this._cameraTransform.forward, this._cameraTransform.position + this._cameraTransform.forward * this._camera.nearClipPlane);
				float distanceToPoint = plane.GetDistanceToPoint(this._gizmoTransform.position);
				return Mathf.Abs(distanceToPoint);
			}
			return (this._cameraTransform.position - this._gizmoTransform.position).magnitude;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0003DD0C File Offset: 0x0003BF0C
		private bool IsPointOnRotationCircleVisible(Vector3 point, float distanceFromCameraPositionToSphereCenter)
		{
			if (this._camera.orthographic)
			{
				Plane plane = new Plane(this._cameraTransform.forward, this._cameraTransform.position + this._cameraTransform.forward * this._camera.nearClipPlane);
				float distanceToPoint = plane.GetDistanceToPoint(point);
				return Mathf.Abs(distanceToPoint) <= distanceFromCameraPositionToSphereCenter;
			}
			return (point - this._cameraTransform.position).magnitude <= distanceFromCameraPositionToSphereCenter;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0003DD9C File Offset: 0x0003BF9C
		private void CreateRotationSphereMaterials()
		{
			Shader shader = Shader.Find("Gizmo Lit Mesh");
			Shader shader2 = Shader.Find("Gizmo Unlit Mesh");
			this._litRotationSphereMaterial = new Material(shader);
			this._litRotationSphereMaterial.SetColor("_Color", this._rotationSphereColor);
			this._unlitRotationSphereMaterial = new Material(shader2);
			this._unlitRotationSphereMaterial.SetColor("_Color", this._rotationSphereColor);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0003DE04 File Offset: 0x0003C004
		private void CreateRotationGuideDiscMaterial()
		{
			Shader shader = Shader.Find("Gizmo Unlit Mesh");
			this._rotationGuideDiscMaterial = new Material(shader);
			this._rotationGuideDiscMaterial.SetColor("_Color", this._rotationGuideDiscColor);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0003DE40 File Offset: 0x0003C040
		private void AssignRotationSphereColorToSphereMaterials()
		{
			if (this._litRotationSphereMaterial != null)
			{
				this._litRotationSphereMaterial.SetColor("_Color", this._rotationSphereColor);
			}
			if (this._unlitRotationSphereMaterial != null)
			{
				this._unlitRotationSphereMaterial.SetColor("_Color", this._rotationSphereColor);
			}
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0003DE9C File Offset: 0x0003C09C
		private Material GetRotationSphereMaterial()
		{
			return (!this._isRotationSphereLit) ? this._unlitRotationSphereMaterial : this._litRotationSphereMaterial;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0003DEBC File Offset: 0x0003C0BC
		private void CreateRotationSphereMesh()
		{
			if (this._rotationSphereMesh != null)
			{
				base.DestroyGizmoMesh(this._rotationSphereMesh);
			}
			this._rotationSphereMesh = ProceduralMeshGenerator.CreateSphereMesh(this._rotationSphereRadius, 50, 50);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0003DEFC File Offset: 0x0003C0FC
		private void CalculateRotationCirclePointsInGizmoLocalSpace()
		{
			this._rotationCirclePointsForXAxisInLocalSpace = this.CalculateRotationCirclePointsInInGizmoLocalSpace(GizmoAxis.X);
			this._rotationCirclePointsForYAxisInLocalSpace = this.CalculateRotationCirclePointsInInGizmoLocalSpace(GizmoAxis.Y);
			this._rotationCirclePointsForZAxisInLocalSpace = this.CalculateRotationCirclePointsInInGizmoLocalSpace(GizmoAxis.Z);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0003DF28 File Offset: 0x0003C128
		private Vector3[] CalculateRotationCirclePointsInInGizmoLocalSpace(GizmoAxis gizmoAxis)
		{
			float num = this._rotationSphereRadius * 1f;
			Matrix4x4 matrix4x = default(Matrix4x4);
			if (gizmoAxis == GizmoAxis.X)
			{
				matrix4x.SetTRS(Vector3.zero, Quaternion.Euler(0f, 0f, 90f), Vector3.one);
			}
			else if (gizmoAxis == GizmoAxis.Y)
			{
				matrix4x = Matrix4x4.identity;
			}
			else
			{
				matrix4x.SetTRS(Vector3.zero, Quaternion.Euler(90f, 0f, 0f), Vector3.one);
			}
			Vector3[] array = new Vector3[100];
			float num2 = 3.6363637f;
			for (int i = 0; i < 100; i++)
			{
				Vector3 vector = new Vector3(Mathf.Cos(0.017453292f * num2 * (float)i) * num, 0f, Mathf.Sin(0.017453292f * num2 * (float)i) * num);
				vector = matrix4x.MultiplyPoint(vector);
				array[i] = vector;
			}
			return array;
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0003E024 File Offset: 0x0003C224
		private float GetWorldSpaceRotationSphereRadius()
		{
			return this._rotationSphereRadius * base.CalculateGizmoScale();
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0003E034 File Offset: 0x0003C234
		private float GetWorldSpaceRotationCircleRadius()
		{
			return this.GetWorldSpaceRotationSphereRadius() * 1f;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0003E044 File Offset: 0x0003C244
		private float GetRotationCircleCylinderAxisLength()
		{
			if (this._camera.orthographic)
			{
				return 0.4f;
			}
			return 0.2f * base.CalculateGizmoScale();
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0003E074 File Offset: 0x0003C274
		private float GetRotationCircleIntersectionEpsilon()
		{
			if (this._camera.orthographic)
			{
				return 0.35f;
			}
			return 0.2f * base.CalculateGizmoScale();
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0003E0A4 File Offset: 0x0003C2A4
		private bool RayIntersectsRotationCircle(Ray ray, Vector3 circlePlaneNormal, float circleRadius, out Vector3 intersectionPoint)
		{
			intersectionPoint = Vector3.zero;
			float d;
			if (ray.Intersects3DCircle(this._gizmoTransform.position, circleRadius, circlePlaneNormal, true, this.GetRotationCircleIntersectionEpsilon(), out d))
			{
				intersectionPoint = ray.origin + ray.direction * d;
				return true;
			}
			float rotationCircleCylinderAxisLength = this.GetRotationCircleCylinderAxisLength();
			Vector3 cylinderAxisFirstPoint = this._gizmoTransform.position - circlePlaneNormal * rotationCircleCylinderAxisLength * 0.5f;
			Vector3 cylinderAxisSecondPoint = this._gizmoTransform.position + circlePlaneNormal * rotationCircleCylinderAxisLength * 0.5f;
			if (ray.IntersectsCylinder(cylinderAxisFirstPoint, cylinderAxisSecondPoint, circleRadius, out d))
			{
				intersectionPoint = ray.origin + ray.direction * d;
				return true;
			}
			return false;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0003E180 File Offset: 0x0003C380
		private void RotateControlledObjects(Vector3 rotationAxis, float angleInDegrees)
		{
			if (base.ControlledObjects != null)
			{
				rotationAxis.Normalize();
				List<GameObject> topParentsFromControlledObjects = base.GetTopParentsFromControlledObjects();
				if (topParentsFromControlledObjects.Count != 0)
				{
					if (this._transformPivotPoint == TransformPivotPoint.Center)
					{
						foreach (GameObject gameObject in topParentsFromControlledObjects)
						{
							if (gameObject != null)
							{
								gameObject.Rotate(rotationAxis, angleInDegrees, this._gizmoTransform.position);
							}
						}
					}
					else
					{
						foreach (GameObject gameObject2 in topParentsFromControlledObjects)
						{
							if (gameObject2 != null)
							{
								gameObject2.transform.Rotate(rotationAxis, angleInDegrees, Space.World);
							}
						}
					}
					this._objectsWereTransformedSinceLeftMouseButtonWasPressed = true;
				}
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0003E29C File Offset: 0x0003C49C
		private void DrawRotationSphere(Matrix4x4 worldTransform)
		{
			Graphics.DrawMesh(this._rotationSphereMesh, worldTransform, this.GetRotationSphereMaterial(), base.gameObject.layer);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0003E2C8 File Offset: 0x0003C4C8
		private Matrix4x4 GetRotationSphereWorldTransform()
		{
			return this._gizmoTransform.localToWorldMatrix;
		}

		// Token: 0x04000737 RID: 1847
		private const float _rotationCircleRadiusScale = 1f;

		// Token: 0x04000738 RID: 1848
		[SerializeField]
		private float _rotationSphereRadius = 3f;

		// Token: 0x04000739 RID: 1849
		[SerializeField]
		private Color _rotationSphereColor = new Color(0.3f, 0.3f, 0.3f, 0.12f);

		// Token: 0x0400073A RID: 1850
		[SerializeField]
		private bool _isRotationSphereLit = true;

		// Token: 0x0400073B RID: 1851
		[SerializeField]
		private bool _showRotationGuide = true;

		// Token: 0x0400073C RID: 1852
		[SerializeField]
		private Color _rotationGuieLineColor = new Color(0.8f, 0.8f, 0.8f, 0.8f);

		// Token: 0x0400073D RID: 1853
		[SerializeField]
		private Color _rotationGuideDiscColor = new Color(0.5f, 0.5f, 0.5f, 0.1f);

		// Token: 0x0400073E RID: 1854
		[SerializeField]
		private bool _showRotationSphereBoundary = true;

		// Token: 0x0400073F RID: 1855
		[SerializeField]
		private Color _rotationSphereBoundaryLineColor = Color.white;

		// Token: 0x04000740 RID: 1856
		[SerializeField]
		private bool _showCameraLookRotationCircle = true;

		// Token: 0x04000741 RID: 1857
		[SerializeField]
		private float _cameraLookRotationCircleRadiusScale = 1.15f;

		// Token: 0x04000742 RID: 1858
		[SerializeField]
		private Color _cameraLookRotationCircleLineColor = Color.white;

		// Token: 0x04000743 RID: 1859
		[SerializeField]
		private Color _cameraLookRotationCircleColorWhenSelected = Color.yellow;

		// Token: 0x04000744 RID: 1860
		[SerializeField]
		private RotationGizmoSnapSettings _snapSettings = new RotationGizmoSnapSettings();

		// Token: 0x04000745 RID: 1861
		private float _accumulatedRotation;

		// Token: 0x04000746 RID: 1862
		private Material _litRotationSphereMaterial;

		// Token: 0x04000747 RID: 1863
		private Material _unlitRotationSphereMaterial;

		// Token: 0x04000748 RID: 1864
		private Material _rotationGuideDiscMaterial;

		// Token: 0x04000749 RID: 1865
		private Mesh _rotationSphereMesh;

		// Token: 0x0400074A RID: 1866
		private Vector3[] _rotationCirclePointsForXAxisInLocalSpace;

		// Token: 0x0400074B RID: 1867
		private Vector3[] _rotationCirclePointsForYAxisInLocalSpace;

		// Token: 0x0400074C RID: 1868
		private Vector3[] _rotationCirclePointsForZAxisInLocalSpace;

		// Token: 0x0400074D RID: 1869
		private Vector3[] _rotationGuideLinePoints = new Vector3[2];

		// Token: 0x0400074E RID: 1870
		private bool _isCameraLookRotationCircleSelected;

		// Token: 0x0400074F RID: 1871
		private Vector2 _cameraLookRotationCirclePickPoint;

		// Token: 0x04000750 RID: 1872
		private bool _isRotationSphereSelected;
	}
}
