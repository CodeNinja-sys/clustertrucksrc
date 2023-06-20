using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001B1 RID: 433
	public class ScaleGizmo : Gizmo
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0003E528 File Offset: 0x0003C728
		public static float MinAxisLength
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0003E530 File Offset: 0x0003C730
		public static float MinScaleBoxSize
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0003E538 File Offset: 0x0003C738
		public static float MinScreenSizeOfAllAxesSquare
		{
			get
			{
				return 2f;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0003E540 File Offset: 0x0003C740
		public static float MinMultiAxisTriangleSideLength
		{
			get
			{
				return 0.001f;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0003E548 File Offset: 0x0003C748
		public static float MinObjectsLocalAxesLength
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x0003E550 File Offset: 0x0003C750
		// (set) Token: 0x060009F9 RID: 2553 RVA: 0x0003E558 File Offset: 0x0003C758
		public float AxisLength
		{
			get
			{
				return this._axisLength;
			}
			set
			{
				this._axisLength = Mathf.Max(ScaleGizmo.MinAxisLength, value);
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0003E56C File Offset: 0x0003C76C
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x0003E574 File Offset: 0x0003C774
		public float ScaleBoxWidth
		{
			get
			{
				return this._scaleBoxWidth;
			}
			set
			{
				this._scaleBoxWidth = Mathf.Max(value, ScaleGizmo.MinScaleBoxSize);
				if (Application.isPlaying)
				{
					this.CreateScaleBoxMesh();
				}
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0003E598 File Offset: 0x0003C798
		// (set) Token: 0x060009FD RID: 2557 RVA: 0x0003E5A0 File Offset: 0x0003C7A0
		public float ScaleBoxHeight
		{
			get
			{
				return this._scaleBoxHeight;
			}
			set
			{
				this._scaleBoxHeight = Mathf.Max(value, ScaleGizmo.MinScaleBoxSize);
				if (Application.isPlaying)
				{
					this.CreateScaleBoxMesh();
				}
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0003E5C4 File Offset: 0x0003C7C4
		// (set) Token: 0x060009FF RID: 2559 RVA: 0x0003E5CC File Offset: 0x0003C7CC
		public float ScaleBoxDepth
		{
			get
			{
				return this._scaleBoxDepth;
			}
			set
			{
				this._scaleBoxDepth = Mathf.Max(value, ScaleGizmo.MinScaleBoxSize);
				if (Application.isPlaying)
				{
					this.CreateScaleBoxMesh();
				}
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0003E5F0 File Offset: 0x0003C7F0
		// (set) Token: 0x06000A01 RID: 2561 RVA: 0x0003E5F8 File Offset: 0x0003C7F8
		public bool AreScaleBoxesLit
		{
			get
			{
				return this._areScaleBoxesLit;
			}
			set
			{
				this._areScaleBoxesLit = value;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0003E604 File Offset: 0x0003C804
		// (set) Token: 0x06000A03 RID: 2563 RVA: 0x0003E60C File Offset: 0x0003C80C
		public bool ScaleAlongAllAxes
		{
			get
			{
				return this._scaleAlongAllAxes;
			}
			set
			{
				if (value && base.IsTransformingObjects())
				{
					return;
				}
				this._scaleAlongAllAxes = value;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0003E628 File Offset: 0x0003C828
		// (set) Token: 0x06000A05 RID: 2565 RVA: 0x0003E630 File Offset: 0x0003C830
		public float ScreenSizeOfAllAxesSquare
		{
			get
			{
				return this._screenSizeOfAllAxesSquare;
			}
			set
			{
				this._screenSizeOfAllAxesSquare = Mathf.Max(ScaleGizmo.MinScreenSizeOfAllAxesSquare, value);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0003E644 File Offset: 0x0003C844
		// (set) Token: 0x06000A07 RID: 2567 RVA: 0x0003E64C File Offset: 0x0003C84C
		public Color ColorOfAllAxesSquareLines
		{
			get
			{
				return this._colorOfAllAxesSquareLines;
			}
			set
			{
				this._colorOfAllAxesSquareLines = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0003E658 File Offset: 0x0003C858
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x0003E660 File Offset: 0x0003C860
		public Color ColorOfAllAxesSquareLinesWhenSelected
		{
			get
			{
				return this._colorOfAllAxesSquareLinesWhenSelected;
			}
			set
			{
				this._colorOfAllAxesSquareLinesWhenSelected = value;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0003E66C File Offset: 0x0003C86C
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x0003E674 File Offset: 0x0003C874
		public bool AdjustAllAxesScaleSquareWhileScalingObjects
		{
			get
			{
				return this._adjustAllAxesScaleSquareWhileScalingObjects;
			}
			set
			{
				this._adjustAllAxesScaleSquareWhileScalingObjects = value;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0003E680 File Offset: 0x0003C880
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x0003E688 File Offset: 0x0003C888
		public bool AdjustAxisLengthWhileScalingObjects
		{
			get
			{
				return this._adjustAxisLengthWhileScalingObjects;
			}
			set
			{
				this._adjustAxisLengthWhileScalingObjects = value;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0003E694 File Offset: 0x0003C894
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x0003E69C File Offset: 0x0003C89C
		public bool AdjustMultiAxisTrianglesWhileScalingObjects
		{
			get
			{
				return this._adjustMultiAxisTrianglesWhileScalingObjects;
			}
			set
			{
				this._adjustMultiAxisTrianglesWhileScalingObjects = value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0003E6A8 File Offset: 0x0003C8A8
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x0003E6B0 File Offset: 0x0003C8B0
		public Color SelectedMultiAxisTriangleColor
		{
			get
			{
				return this._selectedMultiAxisTriangleColor;
			}
			set
			{
				this._selectedMultiAxisTriangleColor = value;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0003E6BC File Offset: 0x0003C8BC
		// (set) Token: 0x06000A13 RID: 2579 RVA: 0x0003E6C4 File Offset: 0x0003C8C4
		public Color SelectedMultiAxisTriangleLineColor
		{
			get
			{
				return this._selectedMultiAxisTriangleLineColor;
			}
			set
			{
				this._selectedMultiAxisTriangleLineColor = value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0003E6D0 File Offset: 0x0003C8D0
		// (set) Token: 0x06000A15 RID: 2581 RVA: 0x0003E6D8 File Offset: 0x0003C8D8
		public float MultiAxisTriangleSideLength
		{
			get
			{
				return this._multiAxisTriangleSideLength;
			}
			set
			{
				this._multiAxisTriangleSideLength = Mathf.Max(ScaleGizmo.MinMultiAxisTriangleSideLength, value);
				if (Application.isPlaying)
				{
					this.CreateMultiAxisTriangleMesh();
				}
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0003E6FC File Offset: 0x0003C8FC
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x0003E704 File Offset: 0x0003C904
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

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0003E710 File Offset: 0x0003C910
		// (set) Token: 0x06000A19 RID: 2585 RVA: 0x0003E718 File Offset: 0x0003C918
		public bool DrawObjectsLocalAxesWhileScaling
		{
			get
			{
				return this._drawObjectsLocalAxesWhileScaling;
			}
			set
			{
				this._drawObjectsLocalAxesWhileScaling = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0003E724 File Offset: 0x0003C924
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x0003E72C File Offset: 0x0003C92C
		public float ObjectsLocalAxesLength
		{
			get
			{
				return this._objectsLocalAxesLength;
			}
			set
			{
				this._objectsLocalAxesLength = Mathf.Max(ScaleGizmo.MinObjectsLocalAxesLength, value);
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0003E740 File Offset: 0x0003C940
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x0003E748 File Offset: 0x0003C948
		public bool PreserveObjectLocalAxesScreenSize
		{
			get
			{
				return this._preserveObjectLocalAxesScreenSize;
			}
			set
			{
				this._preserveObjectLocalAxesScreenSize = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0003E754 File Offset: 0x0003C954
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x0003E75C File Offset: 0x0003C95C
		public bool AdjustObjectLocalAxesWhileScalingObjects
		{
			get
			{
				return this._adjustObjectLocalAxesWhileScalingObjects;
			}
			set
			{
				this._adjustObjectLocalAxesWhileScalingObjects = value;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0003E768 File Offset: 0x0003C968
		public ScaleGizmoSnapSettings SnapSettings
		{
			get
			{
				return this._snapSettings;
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0003E770 File Offset: 0x0003C970
		public override bool IsReadyForObjectManipulation()
		{
			return this._selectedAxis != GizmoAxis.None || this._selectedMultiAxisTriangle != MultiAxisTriangle.None || this._isAllAxesSquareSelected;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0003E794 File Offset: 0x0003C994
		public override GizmoType GetGizmoType()
		{
			return GizmoType.Scale;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0003E798 File Offset: 0x0003C998
		public Color GetMultiAxisTriangleColor(MultiAxisTriangle multiAxisTriangle)
		{
			if (multiAxisTriangle == MultiAxisTriangle.None)
			{
				return Color.black;
			}
			return this._multiAxisTriangleColors[(int)multiAxisTriangle];
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0003E7B8 File Offset: 0x0003C9B8
		public void SetMultiAxisTriangleColor(MultiAxisTriangle multiAxisTriangle, Color color)
		{
			if (multiAxisTriangle == MultiAxisTriangle.None)
			{
				return;
			}
			this._multiAxisTriangleColors[(int)multiAxisTriangle] = color;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0003E7D4 File Offset: 0x0003C9D4
		public Color GetMultiAxisTriangleLineColor(MultiAxisTriangle multiAxisTriangle)
		{
			if (multiAxisTriangle == MultiAxisTriangle.None)
			{
				return Color.black;
			}
			return this._multiAxisTriangleLineColors[(int)multiAxisTriangle];
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0003E7F4 File Offset: 0x0003C9F4
		public void SetMultiAxisTriangleLineColor(MultiAxisTriangle multiAxisTriangle, Color color)
		{
			if (multiAxisTriangle == MultiAxisTriangle.None)
			{
				return;
			}
			this._multiAxisTriangleLineColors[(int)multiAxisTriangle] = color;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0003E810 File Offset: 0x0003CA10
		protected override void Start()
		{
			base.Start();
			this.CreateScaleBoxMaterials();
			this.CreateScaleBoxMesh();
			this.CreateMultiAxisTriangleMaterials();
			this.CreateMultiAxisTriangleMesh();
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0003E83C File Offset: 0x0003CA3C
		protected override void Update()
		{
			base.Update();
			Matrix4x4[] scaleBoxesWorldTransforms = this.GetScaleBoxesWorldTransforms();
			this.DrawScaleBoxes(scaleBoxesWorldTransforms);
			Matrix4x4[] multiAxisTrianglesWorldTransforms = this.GetMultiAxisTrianglesWorldTransforms();
			if (!this._scaleAlongAllAxes)
			{
				this.DrawMultiAxisTriangles(multiAxisTrianglesWorldTransforms);
			}
			if (this._mouse.IsLeftMouseButtonDown)
			{
				return;
			}
			this._selectedAxis = GizmoAxis.None;
			this._selectedMultiAxisTriangle = MultiAxisTriangle.None;
			this._isAllAxesSquareSelected = false;
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
				if (!flag && ray.IntersectsBox(this._scaleBoxWidth, this._scaleBoxHeight, this._scaleBoxDepth, scaleBoxesWorldTransforms[i], out d))
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
			if (!this._scaleAlongAllAxes)
			{
				for (int j = 0; j < 3; j++)
				{
					Vector3[] multiAxisTriangleWorldSpacePoints = this.GetMultiAxisTriangleWorldSpacePoints(j);
					this.ReorderTriangleVertsForClockwiseWindingOrder(multiAxisTriangleWorldSpacePoints, multiAxisTrianglesWorldTransforms[j]);
					Plane plane = new Plane(multiAxisTriangleWorldSpacePoints[0], multiAxisTriangleWorldSpacePoints[1], multiAxisTriangleWorldSpacePoints[2]);
					float d;
					if (plane.Raycast(ray, out d))
					{
						Vector3 vector = ray.origin + ray.direction * d;
						if (vector.IsInsideTriangle(multiAxisTriangleWorldSpacePoints))
						{
							float magnitude3 = (vector - position).magnitude;
							if (magnitude3 < num)
							{
								num = magnitude3;
								this._selectedMultiAxisTriangle = (MultiAxisTriangle)j;
								this._selectedAxis = GizmoAxis.None;
							}
						}
					}
				}
			}
			if (this._scaleAlongAllAxes && this.IsMouseCursorInsideAllAxesScaleSquare())
			{
				this._isAllAxesSquareSelected = true;
				this._selectedAxis = GizmoAxis.None;
				this._selectedMultiAxisTriangle = MultiAxisTriangle.None;
			}
			for (int k = 0; k < 3; k++)
			{
				Material scaleBoxMaterial = this.GetScaleBoxMaterial(k);
				scaleBoxMaterial.SetColor("_Color", (this._selectedAxis != (GizmoAxis)k) ? this._axesColors[k] : this._selectedAxisColor);
			}
			for (int l = 0; l < 3; l++)
			{
				Material material = this._multiAxisTriangleMaterials[l];
				material.SetColor("_Color", (this._selectedMultiAxisTriangle != (MultiAxisTriangle)l) ? this._multiAxisTriangleColors[l] : this._selectedMultiAxisTriangleColor);
			}
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0003EBAC File Offset: 0x0003CDAC
		protected override void OnRenderObject()
		{
			base.OnRenderObject();
			this.DrawAxesLines();
			this.DrawMultiAxisTrianglesLines();
			this.DrawAllAxesSquareLines();
			this.DrawObjectsLocalAxesDuringScaleOperation();
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0003EBD8 File Offset: 0x0003CDD8
		protected override void OnLeftMouseButtonDown()
		{
			base.OnLeftMouseButtonDown();
			if (this._selectedAxis != GizmoAxis.None || this._selectedMultiAxisTriangle != MultiAxisTriangle.None)
			{
				Plane plane;
				if (this._selectedAxis != GizmoAxis.None)
				{
					plane = base.GetCoordinateSystemPlaneFromSelectedAxis();
				}
				else
				{
					plane = this.GetMultiAxisTrianglePlane(this._selectedMultiAxisTriangle);
				}
				Ray ray = this._camera.ScreenPointToRay(Input.mousePosition);
				float d;
				if (plane.Raycast(ray, out d))
				{
					this._lastGizmoPickPoint = ray.origin + ray.direction * d;
				}
			}
			if (this.IsReadyForObjectManipulation())
			{
				foreach (GameObject gameObject in base.ControlledObjects)
				{
					this._gameObjectLocalScaleSnapshot.Add(gameObject, gameObject.transform.localScale);
				}
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0003ECE0 File Offset: 0x0003CEE0
		protected override void OnLeftMouseButtonUp()
		{
			base.OnLeftMouseButtonUp();
			this._accumulatedScaleAxisDrag = 0f;
			this._accumulatedMultiAxisTriangleDrag = 0f;
			this._accumulatedAllAxesSquareDragInScreenUnits = 0f;
			this._accumulatedAllAxesSquareDragInWorldUnits = 0f;
			this._gameObjectLocalScaleSnapshot.Clear();
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0003ED20 File Offset: 0x0003CF20
		protected override void OnMouseMoved()
		{
			base.OnMouseMoved();
			if (this._mouse.IsLeftMouseButtonDown)
			{
				if (this._selectedAxis != GizmoAxis.None)
				{
					Vector3 rhs;
					if (this._selectedAxis == GizmoAxis.X)
					{
						rhs = this._gizmoTransform.right;
					}
					else if (this._selectedAxis == GizmoAxis.Y)
					{
						rhs = this._gizmoTransform.up;
					}
					else
					{
						rhs = this._gizmoTransform.forward;
					}
					Plane coordinateSystemPlaneFromSelectedAxis = base.GetCoordinateSystemPlaneFromSelectedAxis();
					Ray ray = this._camera.ScreenPointToRay(Input.mousePosition);
					float d;
					if (coordinateSystemPlaneFromSelectedAxis.Raycast(ray, out d))
					{
						Vector3 vector = ray.origin + ray.direction * d;
						Vector3 lhs = vector - this._lastGizmoPickPoint;
						float num = Vector3.Dot(lhs, rhs);
						this._accumulatedScaleAxisDrag += num;
						bool[] array = new bool[3];
						array[(int)this._selectedAxis] = true;
						this.ScaleControlledObjects(array);
						this._lastGizmoPickPoint = vector;
					}
				}
				else if (this._selectedMultiAxisTriangle != MultiAxisTriangle.None)
				{
					Plane multiAxisTrianglePlane = this.GetMultiAxisTrianglePlane(this._selectedMultiAxisTriangle);
					Ray ray2 = this._camera.ScreenPointToRay(Input.mousePosition);
					float d2;
					if (multiAxisTrianglePlane.Raycast(ray2, out d2))
					{
						Vector3 vector2 = ray2.origin + ray2.direction * d2;
						Vector3 rhs2 = vector2 - this._lastGizmoPickPoint;
						Vector3 multiAxisTriangleMedianVector = this.GetMultiAxisTriangleMedianVector(this._selectedMultiAxisTriangle);
						multiAxisTriangleMedianVector.Normalize();
						float num2 = Vector3.Dot(multiAxisTriangleMedianVector, rhs2);
						this._accumulatedMultiAxisTriangleDrag += num2;
						this.ScaleControlledObjects(this.GetScaleAxisBooleanFlagsForMultiAxisTriangle(this._selectedMultiAxisTriangle));
						this._lastGizmoPickPoint = vector2;
					}
				}
				else if (this._scaleAlongAllAxes && this._isAllAxesSquareSelected)
				{
					Vector2 a = Input.mousePosition;
					Vector2 b = this._camera.WorldToScreenPoint(this._gizmoTransform.position);
					Vector2 lhs2 = a - b;
					lhs2.Normalize();
					float num3 = Mathf.Sign(Vector2.Dot(lhs2, this._mouse.CursorOffsetSinceLastFrame));
					if (lhs2.y < 0f)
					{
						num3 *= -1f;
					}
					this._accumulatedAllAxesSquareDragInScreenUnits += this._mouse.CursorOffsetSinceLastFrame.magnitude * num3 * 0.45f;
					this.ScaleControlledObjects(new bool[]
					{
						true,
						true,
						true
					});
					Plane plane = new Plane(this._cameraTransform.forward, this._gizmoTransform.position);
					Ray ray3 = this._camera.ScreenPointToRay(Input.mousePosition);
					float d3;
					if (plane.Raycast(ray3, out d3))
					{
						Vector3 b2 = ray3.origin + ray3.direction * d3;
						this._accumulatedAllAxesSquareDragInWorldUnits = (this._gizmoTransform.position - b2).magnitude;
						this._accumulatedAllAxesSquareDragInWorldUnits *= Mathf.Sign(this._accumulatedAllAxesSquareDragInScreenUnits);
					}
				}
			}
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0003F038 File Offset: 0x0003D238
		private Vector2[] GetAllAxesScaleSquareScreenPoints()
		{
			float allAxesScaleSquareScaleFactorDuringScaleOperation = this.GetAllAxesScaleSquareScaleFactorDuringScaleOperation();
			Vector2 a = this._camera.WorldToScreenPoint(this._gizmoTransform.position);
			float d = this._screenSizeOfAllAxesSquare * 0.5f * allAxesScaleSquareScaleFactorDuringScaleOperation;
			return new Vector2[]
			{
				a - (Vector2.right - Vector2.up) * d,
				a + (Vector2.right + Vector2.up) * d,
				a + (Vector2.right - Vector2.up) * d,
				a - (Vector2.right + Vector2.up) * d
			};
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0003F11C File Offset: 0x0003D31C
		private bool IsMouseCursorInsideAllAxesScaleSquare()
		{
			Vector2 b = this._camera.WorldToScreenPoint(this._gizmoTransform.position);
			float num = this._screenSizeOfAllAxesSquare * 0.5f;
			Vector2 a = Input.mousePosition;
			Vector2 vector = a - b;
			return Mathf.Abs(vector.x) <= num && Mathf.Abs(vector.y) <= num;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0003F18C File Offset: 0x0003D38C
		private bool[] GetScaleAxisBooleanFlagsForMultiAxisTriangle(MultiAxisTriangle multiAxisTriangle)
		{
			switch (multiAxisTriangle)
			{
			case MultiAxisTriangle.XY:
			{
				bool[] array = new bool[3];
				array[0] = true;
				array[1] = true;
				return array;
			}
			case MultiAxisTriangle.XZ:
				return new bool[]
				{
					true,
					default(bool),
					true
				};
			case MultiAxisTriangle.YZ:
				return new bool[]
				{
					default(bool),
					true,
					true
				};
			default:
				return null;
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0003F1E0 File Offset: 0x0003D3E0
		private Vector3 GetMultiAxisTriangleMedianVector(MultiAxisTriangle multiAxisTriangle)
		{
			Vector3[] worldAxesMultipliedByMultiAxisExtensionSigns = this.GetWorldAxesMultipliedByMultiAxisExtensionSigns();
			float multiAxisTriangleSideLength = this.GetMultiAxisTriangleSideLength();
			Vector3 a = worldAxesMultipliedByMultiAxisExtensionSigns[0];
			Vector3 vector = worldAxesMultipliedByMultiAxisExtensionSigns[1];
			Vector3 vector2 = worldAxesMultipliedByMultiAxisExtensionSigns[2];
			switch (multiAxisTriangle)
			{
			case MultiAxisTriangle.XY:
			{
				Vector3 a2 = (a - vector) * multiAxisTriangleSideLength;
				float magnitude = a2.magnitude;
				a2.Normalize();
				return vector * multiAxisTriangleSideLength + a2 * magnitude * 0.5f;
			}
			case MultiAxisTriangle.XZ:
			{
				Vector3 a2 = (a - vector2) * multiAxisTriangleSideLength;
				float magnitude = a2.magnitude;
				a2.Normalize();
				return vector2 * multiAxisTriangleSideLength + a2 * magnitude * 0.5f;
			}
			case MultiAxisTriangle.YZ:
			{
				Vector3 a2 = (vector2 - vector) * multiAxisTriangleSideLength;
				float magnitude = a2.magnitude;
				a2.Normalize();
				return vector * multiAxisTriangleSideLength + a2 * magnitude * 0.5f;
			}
			default:
				return Vector3.zero;
			}
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0003F300 File Offset: 0x0003D500
		private float GetMultiAxisTriangleSideLength()
		{
			return base.CalculateGizmoScale() * this._multiAxisTriangleSideLength;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0003F310 File Offset: 0x0003D510
		private Plane GetMultiAxisTrianglePlane(MultiAxisTriangle multiAxisTriangle)
		{
			switch (multiAxisTriangle)
			{
			case MultiAxisTriangle.XY:
				return new Plane(this._gizmoTransform.forward, this._gizmoTransform.position);
			case MultiAxisTriangle.XZ:
				return new Plane(this._gizmoTransform.up, this._gizmoTransform.position);
			case MultiAxisTriangle.YZ:
				return new Plane(this._gizmoTransform.right, this._gizmoTransform.position);
			default:
				return default(Plane);
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0003F394 File Offset: 0x0003D594
		private void ReorderTriangleVertsForClockwiseWindingOrder(Vector3[] worldSpaceTriangleVerts, Matrix4x4 triangleWorldTransform)
		{
			Matrix4x4 inverse = triangleWorldTransform.inverse;
			Vector3[] array = worldSpaceTriangleVerts.Clone() as Vector3[];
			for (int i = 0; i < 3; i++)
			{
				array[i] = inverse.MultiplyPoint(worldSpaceTriangleVerts[i]);
			}
			Vector3 rhs = array[1] - array[0];
			Vector3 lhs = array[2] - array[0];
			if (Vector3.Cross(lhs, rhs).z < 0f)
			{
				Vector3 vector = worldSpaceTriangleVerts[1];
				worldSpaceTriangleVerts[1] = worldSpaceTriangleVerts[2];
				worldSpaceTriangleVerts[2] = vector;
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0003F474 File Offset: 0x0003D674
		private Vector3[] GetMultiAxisTriangleWorldSpacePoints(int multiAxisTriangleIndex)
		{
			Vector3[] worldAxesUsedToDrawMultiAxisTriangleLines = this.GetWorldAxesUsedToDrawMultiAxisTriangleLines();
			float d = base.CalculateGizmoScale();
			int num = multiAxisTriangleIndex * 2;
			return new Vector3[]
			{
				this._gizmoTransform.position,
				this._gizmoTransform.position + worldAxesUsedToDrawMultiAxisTriangleLines[num + 1] * this._multiAxisTriangleSideLength * d,
				this._gizmoTransform.position + worldAxesUsedToDrawMultiAxisTriangleLines[num] * this._multiAxisTriangleSideLength * d
			};
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0003F524 File Offset: 0x0003D724
		private void CreateScaleBoxMaterials()
		{
			Shader shader = Shader.Find("Gizmo Lit Mesh");
			Shader shader2 = Shader.Find("Gizmo Unlit Mesh");
			this._litScaleBoxMaterials = new Material[3];
			this._unlitScaleBoxMaterials = new Material[3];
			for (int i = 0; i < 3; i++)
			{
				this._litScaleBoxMaterials[i] = new Material(shader);
				this._litScaleBoxMaterials[i].SetInt("_StencilRefValue", this._axesStencilRefValues[i]);
				this._unlitScaleBoxMaterials[i] = new Material(shader2);
				this._unlitScaleBoxMaterials[i].SetInt("_StencilRefValue", this._axesStencilRefValues[i]);
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0003F5C4 File Offset: 0x0003D7C4
		private void CreateMultiAxisTriangleMaterials()
		{
			Shader shader = Shader.Find("Gizmo Unlit Mesh");
			for (int i = 0; i < 3; i++)
			{
				this._multiAxisTriangleMaterials[i] = new Material(shader);
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0003F5FC File Offset: 0x0003D7FC
		private void CreateScaleBoxMesh()
		{
			if (this._scaleBoxMesh != null)
			{
				base.DestroyGizmoMesh(this._scaleBoxMesh);
			}
			this._scaleBoxMesh = ProceduralMeshGenerator.CreateBoxMesh(this._scaleBoxWidth, this._scaleBoxHeight, this._scaleBoxDepth);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0003F644 File Offset: 0x0003D844
		private void CreateMultiAxisTriangleMesh()
		{
			if (this._multiAxisTriangleMesh != null)
			{
				base.DestroyGizmoMesh(this._multiAxisTriangleMesh);
			}
			this._multiAxisTriangleMesh = ProceduralMeshGenerator.CreateRightAngledTriangleMesh(this._multiAxisTriangleSideLength, this._multiAxisTriangleSideLength);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0003F688 File Offset: 0x0003D888
		private void DrawAxesLines()
		{
			int[] sortedGizmoAxesIndices = base.GetSortedGizmoAxesIndices();
			float gizmoScale = base.CalculateGizmoScale();
			Vector3[] gizmoLocalAxes = base.GetGizmoLocalAxes();
			Vector3 position = this._gizmoTransform.position;
			foreach (int num in sortedGizmoAxesIndices)
			{
				Vector3 vector = position + gizmoLocalAxes[num] * this.GetAxisLength(num, gizmoScale);
				base.UpdateShaderStencilRefValuesForGizmoAxisLineDraw(num, position, vector, gizmoScale);
				GLPrimitives.Draw3DLine(position, vector, (this._selectedAxis != (GizmoAxis)num) ? this._axesColors[num] : this._selectedAxisColor, this._lineRenderingMaterial);
			}
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0003F740 File Offset: 0x0003D940
		private float GetAxisLength(int axisIndex, float gizmoScale)
		{
			if (!this._adjustAxisLengthWhileScalingObjects || (axisIndex != (int)this._selectedAxis && !this.IsGizmoAxisSharedBySelectedMultiAxisTriangle(axisIndex) && !this._isAllAxesSquareSelected))
			{
				return this._axisLength * gizmoScale;
			}
			if (this._selectedAxis != GizmoAxis.None)
			{
				return this._axisLength * gizmoScale * this.GetAxisScaleFactorForAccumulatedDrag(this._selectedAxis);
			}
			if (this._selectedMultiAxisTriangle != MultiAxisTriangle.None)
			{
				return this._axisLength * gizmoScale * this.GetMultiAxisTriangleScaleFactorForAccumulatedDrag(this._selectedMultiAxisTriangle);
			}
			return this._axisLength * gizmoScale * this.GetAllAxesSquareScaleFactorForAccumulatedDrag();
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0003F7D8 File Offset: 0x0003D9D8
		private bool IsGizmoAxisSharedBySelectedMultiAxisTriangle(int axisIndex)
		{
			if (this._selectedMultiAxisTriangle == MultiAxisTriangle.None)
			{
				return false;
			}
			if (this._selectedMultiAxisTriangle == MultiAxisTriangle.XY)
			{
				return axisIndex == 0 || axisIndex == 1;
			}
			if (this._selectedMultiAxisTriangle == MultiAxisTriangle.XZ)
			{
				return axisIndex == 0 || axisIndex == 2;
			}
			return axisIndex == 1 || axisIndex == 2;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0003F834 File Offset: 0x0003DA34
		private void DrawScaleBoxes(Matrix4x4[] worldTransforms)
		{
			for (int i = 0; i < 3; i++)
			{
				Graphics.DrawMesh(this._scaleBoxMesh, worldTransforms[i], this.GetScaleBoxMaterial(i), base.gameObject.layer);
			}
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0003F87C File Offset: 0x0003DA7C
		private Material GetScaleBoxMaterial(int axisIndex)
		{
			return (!this._areScaleBoxesLit) ? this._unlitScaleBoxMaterials[axisIndex] : this._litScaleBoxMaterials[axisIndex];
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0003F8AC File Offset: 0x0003DAAC
		private Matrix4x4[] GetScaleBoxesWorldTransforms()
		{
			Matrix4x4[] array = new Matrix4x4[3];
			Vector3[] scaleBoxesGizmoLocalPositions = this.GetScaleBoxesGizmoLocalPositions(base.CalculateGizmoScale());
			Quaternion[] scaleBoxesGizmoLocalRotations = this.GetScaleBoxesGizmoLocalRotations();
			for (int i = 0; i < 3; i++)
			{
				Vector3 pos = this._gizmoTransform.position + this._gizmoTransform.rotation * scaleBoxesGizmoLocalPositions[i];
				Quaternion q = this._gizmoTransform.rotation * scaleBoxesGizmoLocalRotations[i];
				array[i] = default(Matrix4x4);
				array[i].SetTRS(pos, q, this._gizmoTransform.lossyScale);
			}
			return array;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0003F964 File Offset: 0x0003DB64
		private Quaternion[] GetScaleBoxesGizmoLocalRotations()
		{
			return new Quaternion[]
			{
				Quaternion.identity,
				Quaternion.Euler(0f, 0f, 90f),
				Quaternion.Euler(0f, 90f, 0f)
			};
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0003F9C8 File Offset: 0x0003DBC8
		private Vector3[] GetScaleBoxesGizmoLocalPositions(float gizmoScale)
		{
			float num = 0.5f * this._scaleBoxWidth * gizmoScale;
			return new Vector3[]
			{
				Vector3.right * (this.GetAxisLength(0, gizmoScale) + num),
				Vector3.up * (this.GetAxisLength(1, gizmoScale) + num),
				Vector3.forward * (this.GetAxisLength(2, gizmoScale) + num)
			};
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0003FA4C File Offset: 0x0003DC4C
		private void DrawMultiAxisTriangles(Matrix4x4[] worldTransforms)
		{
			for (int i = 0; i < 3; i++)
			{
				Graphics.DrawMesh(this._multiAxisTriangleMesh, worldTransforms[i], this._multiAxisTriangleMaterials[i], base.gameObject.layer);
			}
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0003FA98 File Offset: 0x0003DC98
		private Matrix4x4[] GetMultiAxisTrianglesWorldTransforms()
		{
			Matrix4x4[] array = new Matrix4x4[3];
			Vector3[] multiAxisTrianglesGizmoLocalScales = this.GetMultiAxisTrianglesGizmoLocalScales();
			Quaternion[] multiAxisTrianglesGizmoLocalRotations = this.GetMultiAxisTrianglesGizmoLocalRotations();
			for (int i = 0; i < 3; i++)
			{
				float multiAxisTriangleScaleFactorDuringScaleOperation = this.GetMultiAxisTriangleScaleFactorDuringScaleOperation((MultiAxisTriangle)i);
				Vector3 s = Vector3.Scale(this._gizmoTransform.lossyScale, multiAxisTrianglesGizmoLocalScales[i]) * multiAxisTriangleScaleFactorDuringScaleOperation;
				Quaternion q = this._gizmoTransform.rotation * multiAxisTrianglesGizmoLocalRotations[i];
				array[i] = default(Matrix4x4);
				array[i].SetTRS(this._gizmoTransform.position, q, s);
			}
			return array;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0003FB4C File Offset: 0x0003DD4C
		private float GetMultiAxisTriangleScaleFactorDuringScaleOperation(MultiAxisTriangle multiAxisTriangle)
		{
			if (!this._adjustMultiAxisTrianglesWhileScalingObjects || multiAxisTriangle != this._selectedMultiAxisTriangle)
			{
				return 1f;
			}
			return this.GetMultiAxisTriangleScaleFactorForAccumulatedDrag(multiAxisTriangle);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0003FB80 File Offset: 0x0003DD80
		private float GetAllAxesScaleSquareScaleFactorDuringScaleOperation()
		{
			if (!this._adjustAllAxesScaleSquareWhileScalingObjects)
			{
				return 1f;
			}
			return this.GetAllAxesSquareScaleFactorForAccumulatedDrag();
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0003FB9C File Offset: 0x0003DD9C
		private Quaternion[] GetMultiAxisTrianglesGizmoLocalRotations()
		{
			return new Quaternion[]
			{
				Quaternion.identity,
				Quaternion.Euler(90f, 0f, 0f),
				Quaternion.Euler(0f, -90f, 0f)
			};
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0003FC00 File Offset: 0x0003DE00
		private Vector3[] GetMultiAxisTrianglesGizmoLocalScales()
		{
			float[] multiAxisExtensionSigns = base.GetMultiAxisExtensionSigns(this._adjustMultiAxisForBetterVisibility);
			return new Vector3[]
			{
				new Vector3(1f * multiAxisExtensionSigns[0], 1f * multiAxisExtensionSigns[1], 1f),
				new Vector3(1f * multiAxisExtensionSigns[0], 1f * multiAxisExtensionSigns[2], 1f),
				new Vector3(1f * multiAxisExtensionSigns[2], 1f * multiAxisExtensionSigns[1], 1f)
			};
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0003FC98 File Offset: 0x0003DE98
		private void DrawMultiAxisTrianglesLines()
		{
			if (!this._scaleAlongAllAxes)
			{
				Vector3[] linePoints;
				Color[] lineColors;
				this.GetMultiAxisTrianglesLinePointsAndColors(out linePoints, out lineColors);
				GLPrimitives.Draw3DLines(linePoints, lineColors, false, this._lineRenderingMaterial, false, Color.black);
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0003FCD0 File Offset: 0x0003DED0
		private void GetMultiAxisTrianglesLinePointsAndColors(out Vector3[] triangleLinesPoints, out Color[] triangleLinesColors)
		{
			float num = base.CalculateGizmoScale();
			float d = (this._multiAxisTriangleSideLength + 0.001f) * num;
			triangleLinesPoints = new Vector3[18];
			triangleLinesColors = new Color[9];
			Vector3 position = this._gizmoTransform.position;
			Vector3[] worldAxesUsedToDrawMultiAxisTriangleLines = this.GetWorldAxesUsedToDrawMultiAxisTriangleLines();
			for (int i = 0; i < 3; i++)
			{
				Color color = (i != (int)this._selectedMultiAxisTriangle) ? this._multiAxisTriangleLineColors[i] : this._selectedMultiAxisTriangleLineColor;
				int num2 = i * 3;
				triangleLinesColors[num2] = color;
				triangleLinesColors[num2 + 1] = color;
				triangleLinesColors[num2 + 2] = color;
				int num3 = i * 2;
				float multiAxisTriangleScaleFactorDuringScaleOperation = this.GetMultiAxisTriangleScaleFactorDuringScaleOperation((MultiAxisTriangle)i);
				Vector3 vector = this._gizmoTransform.position + worldAxesUsedToDrawMultiAxisTriangleLines[num3] * d * multiAxisTriangleScaleFactorDuringScaleOperation;
				Vector3 vector2 = this._gizmoTransform.position + worldAxesUsedToDrawMultiAxisTriangleLines[num3 + 1] * d * multiAxisTriangleScaleFactorDuringScaleOperation;
				int num4 = i * 6;
				triangleLinesPoints[num4] = position;
				triangleLinesPoints[num4 + 1] = vector;
				triangleLinesPoints[num4 + 2] = vector;
				triangleLinesPoints[num4 + 3] = vector2;
				triangleLinesPoints[num4 + 4] = vector2;
				triangleLinesPoints[num4 + 5] = position;
			}
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0003FE6C File Offset: 0x0003E06C
		private void DrawAllAxesSquareLines()
		{
			if (this._scaleAlongAllAxes)
			{
				Color borderLineColor = (!this._isAllAxesSquareSelected) ? this._colorOfAllAxesSquareLines : this._colorOfAllAxesSquareLinesWhenSelected;
				GLPrimitives.Draw2DRectangleBorderLines(this.GetAllAxesScaleSquareScreenPoints(), borderLineColor, this._lineRenderingMaterial);
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0003FEB4 File Offset: 0x0003E0B4
		private void DrawObjectsLocalAxesDuringScaleOperation()
		{
			if (this._mouse.IsLeftMouseButtonDown && this._drawObjectsLocalAxesWhileScaling && this.IsReadyForObjectManipulation() && base.ControlledObjects != null)
			{
				List<GameObject> topParentsFromControlledObjects = base.GetTopParentsFromControlledObjects();
				Vector3[] linePoints;
				Color[] lineColors;
				this.GetObjectLocalAxesLinePointsAndColors(topParentsFromControlledObjects, out linePoints, out lineColors);
				GLPrimitives.Draw3DLines(linePoints, lineColors, false, this._lineRenderingMaterial, false, Color.black);
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0003FF18 File Offset: 0x0003E118
		private void GetObjectLocalAxesLinePointsAndColors(List<GameObject> gameObjects, out Vector3[] axesLinesPoints, out Color[] axesLinesColors)
		{
			float num = base.CalculateGizmoScale();
			float num2 = (!this._preserveObjectLocalAxesScreenSize) ? 1f : num;
			float d = this._objectsLocalAxesLength * num2;
			float d2 = 1f;
			float d3 = 1f;
			float d4 = 1f;
			if (this._adjustObjectLocalAxesWhileScalingObjects)
			{
				if (this._selectedAxis == GizmoAxis.X)
				{
					d2 = this.GetAxisScaleFactorForAccumulatedDrag(GizmoAxis.X);
				}
				else if (this._selectedMultiAxisTriangle == MultiAxisTriangle.XY || this._selectedMultiAxisTriangle == MultiAxisTriangle.XZ)
				{
					d2 = this.GetMultiAxisTriangleScaleFactorForAccumulatedDrag(this._selectedMultiAxisTriangle);
				}
				else if (this._isAllAxesSquareSelected)
				{
					d2 = this.GetAllAxesSquareScaleFactorForAccumulatedDrag();
				}
				if (this._selectedAxis == GizmoAxis.Y)
				{
					d3 = this.GetAxisScaleFactorForAccumulatedDrag(GizmoAxis.Y);
				}
				else if (this._selectedMultiAxisTriangle == MultiAxisTriangle.XY || this._selectedMultiAxisTriangle == MultiAxisTriangle.YZ)
				{
					d3 = this.GetMultiAxisTriangleScaleFactorForAccumulatedDrag(this._selectedMultiAxisTriangle);
				}
				else if (this._isAllAxesSquareSelected)
				{
					d3 = this.GetAllAxesSquareScaleFactorForAccumulatedDrag();
				}
				if (this._selectedAxis == GizmoAxis.Z)
				{
					d4 = this.GetAxisScaleFactorForAccumulatedDrag(GizmoAxis.Z);
				}
				else if (this._selectedMultiAxisTriangle == MultiAxisTriangle.XZ || this._selectedMultiAxisTriangle == MultiAxisTriangle.YZ)
				{
					d4 = this.GetMultiAxisTriangleScaleFactorForAccumulatedDrag(this._selectedMultiAxisTriangle);
				}
				else if (this._isAllAxesSquareSelected)
				{
					d4 = this.GetAllAxesSquareScaleFactorForAccumulatedDrag();
				}
			}
			int num3 = gameObjects.Count * 3;
			axesLinesPoints = new Vector3[num3 * 2];
			axesLinesColors = new Color[num3];
			for (int i = 0; i < gameObjects.Count; i++)
			{
				Transform transform = gameObjects[i].transform;
				Vector3 position = transform.position;
				Vector3 right = transform.right;
				Vector3 up = transform.up;
				Vector3 forward = transform.forward;
				int num4 = i * 3;
				axesLinesColors[num4] = this._axesColors[0];
				axesLinesColors[num4 + 1] = this._axesColors[1];
				axesLinesColors[num4 + 2] = this._axesColors[2];
				int num5 = i * 6;
				axesLinesPoints[num5] = position + right * d * d2;
				axesLinesPoints[num5 + 1] = position - right * d * d2;
				axesLinesPoints[num5 + 2] = position + up * d * d3;
				axesLinesPoints[num5 + 3] = position - up * d * d3;
				axesLinesPoints[num5 + 4] = position + forward * d * d4;
				axesLinesPoints[num5 + 5] = position - forward * d * d4;
			}
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00040228 File Offset: 0x0003E428
		private Vector3[] GetWorldAxesUsedToDrawMultiAxisTriangleLines()
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

		// Token: 0x06000A4D RID: 2637 RVA: 0x00040304 File Offset: 0x0003E504
		private Vector3[] GetWorldAxesMultipliedByMultiAxisExtensionSigns()
		{
			float[] multiAxisExtensionSigns = base.GetMultiAxisExtensionSigns(this._adjustMultiAxisForBetterVisibility);
			return new Vector3[]
			{
				this._gizmoTransform.right * multiAxisExtensionSigns[0],
				this._gizmoTransform.up * multiAxisExtensionSigns[1],
				this._gizmoTransform.forward * multiAxisExtensionSigns[2]
			};
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00040384 File Offset: 0x0003E584
		private void ScaleControlledObjects(bool[] axesFlags)
		{
			if (base.ControlledObjects != null)
			{
				float scaleFactorForGameObjectGlobalScaleSnapshot = this.GetScaleFactorForGameObjectGlobalScaleSnapshot();
				Vector3 one = Vector3.one;
				for (int i = 0; i < 3; i++)
				{
					if (axesFlags[i])
					{
						one[i] = scaleFactorForGameObjectGlobalScaleSnapshot;
					}
				}
				List<GameObject> topParentsFromControlledObjects = base.GetTopParentsFromControlledObjects();
				if (topParentsFromControlledObjects.Count != 0)
				{
					foreach (GameObject gameObject in topParentsFromControlledObjects)
					{
						if (gameObject != null)
						{
							Transform transform = gameObject.transform;
							Vector3 vector = this._gameObjectLocalScaleSnapshot[gameObject];
							vector = Vector3.Scale(vector, one);
							if (vector.x == 0f)
							{
								vector.x = 1E-05f;
							}
							if (vector.y == 0f)
							{
								vector.y = 1E-05f;
							}
							if (vector.z == 0f)
							{
								vector.z = 1E-05f;
							}
							Vector3 localScale = transform.localScale;
							if (localScale.x == 0f)
							{
								localScale.x = 1E-05f;
							}
							if (localScale.y == 0f)
							{
								localScale.y = 1E-05f;
							}
							if (localScale.z == 0f)
							{
								localScale.z = 1E-05f;
							}
							Vector3 vector2 = Vector3.Scale(vector, new Vector3(1f / localScale.x, 1f / localScale.y, 1f / localScale.z));
							if (this._transformPivotPoint == TransformPivotPoint.Center)
							{
								Vector3 rhs = transform.position - this._gizmoTransform.position;
								Vector3 right = transform.right;
								Vector3 up = transform.up;
								Vector3 forward = transform.forward;
								float num = Vector3.Dot(right, rhs);
								float num2 = Vector3.Dot(up, rhs);
								float num3 = Vector3.Dot(forward, rhs);
								if (axesFlags[0])
								{
									num *= vector2.x;
								}
								if (axesFlags[1])
								{
									num2 *= vector2.y;
								}
								if (axesFlags[2])
								{
									num3 *= vector2.z;
								}
								transform.localScale = vector;
								transform.position = this._gizmoTransform.position + right * num + up * num2 + forward * num3;
							}
							else
							{
								transform.localScale = vector;
							}
						}
					}
					this._objectsWereTransformedSinceLeftMouseButtonWasPressed = true;
				}
			}
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00040640 File Offset: 0x0003E840
		private float GetScaleFactorForGameObjectGlobalScaleSnapshot()
		{
			if (this._selectedAxis != GizmoAxis.None)
			{
				return this.GetAxisScaleFactorForAccumulatedDrag(this._selectedAxis);
			}
			if (this._selectedMultiAxisTriangle != MultiAxisTriangle.None)
			{
				return this.GetMultiAxisTriangleScaleFactorForAccumulatedDrag(this._selectedMultiAxisTriangle);
			}
			return this.GetAllAxesSquareScaleFactorForAccumulatedDrag();
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00040688 File Offset: 0x0003E888
		private float GetAxisScaleFactorForAccumulatedDrag(GizmoAxis gizmoAxis)
		{
			if (!this._snapSettings.IsSnappingEnabled)
			{
				float num = this._axisLength * base.CalculateGizmoScale();
				return (num + this._accumulatedScaleAxisDrag) / num;
			}
			if (Mathf.Abs(this._accumulatedScaleAxisDrag) >= this._snapSettings.StepValueInWorldUnits)
			{
				float num2 = (float)((int)Mathf.Abs(this._accumulatedScaleAxisDrag / this._snapSettings.StepValueInWorldUnits));
				float num3 = this._snapSettings.StepValueInWorldUnits * num2 * Mathf.Sign(this._accumulatedScaleAxisDrag);
				return 1f + num3;
			}
			return 1f;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0004071C File Offset: 0x0003E91C
		private float GetMultiAxisTriangleScaleFactorForAccumulatedDrag(MultiAxisTriangle multiAxisTriangle)
		{
			if (!this._snapSettings.IsSnappingEnabled)
			{
				float magnitude = this.GetMultiAxisTriangleMedianVector(this._selectedMultiAxisTriangle).magnitude;
				return (magnitude + this._accumulatedMultiAxisTriangleDrag) / magnitude;
			}
			if (Mathf.Abs(this._accumulatedMultiAxisTriangleDrag) >= this._snapSettings.StepValueInWorldUnits)
			{
				float num = (float)((int)Mathf.Abs(this._accumulatedMultiAxisTriangleDrag / this._snapSettings.StepValueInWorldUnits));
				float num2 = this._snapSettings.StepValueInWorldUnits * num * Mathf.Sign(this._accumulatedMultiAxisTriangleDrag);
				return 1f + num2;
			}
			return 1f;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x000407B8 File Offset: 0x0003E9B8
		private float GetAllAxesSquareScaleFactorForAccumulatedDrag()
		{
			if (!this._snapSettings.IsSnappingEnabled)
			{
				return (this._screenSizeOfAllAxesSquare + this._accumulatedAllAxesSquareDragInScreenUnits) / this._screenSizeOfAllAxesSquare;
			}
			if (Mathf.Abs(this._accumulatedAllAxesSquareDragInWorldUnits) >= this._snapSettings.StepValueInWorldUnits)
			{
				float num = (float)((int)Mathf.Abs(this._accumulatedAllAxesSquareDragInWorldUnits / this._snapSettings.StepValueInWorldUnits));
				float num2 = this._snapSettings.StepValueInWorldUnits * num * Mathf.Sign(this._accumulatedAllAxesSquareDragInWorldUnits);
				return 1f + num2;
			}
			return 1f;
		}

		// Token: 0x04000753 RID: 1875
		private const float _allAxesSquareDragUnitsPerScreenUnit = 0.45f;

		// Token: 0x04000754 RID: 1876
		[SerializeField]
		private float _axisLength = 5f;

		// Token: 0x04000755 RID: 1877
		[SerializeField]
		private float _scaleBoxWidth = 0.5f;

		// Token: 0x04000756 RID: 1878
		[SerializeField]
		private float _scaleBoxHeight = 0.5f;

		// Token: 0x04000757 RID: 1879
		[SerializeField]
		private float _scaleBoxDepth = 0.5f;

		// Token: 0x04000758 RID: 1880
		[SerializeField]
		private bool _scaleAlongAllAxes;

		// Token: 0x04000759 RID: 1881
		[SerializeField]
		private float _screenSizeOfAllAxesSquare = 25f;

		// Token: 0x0400075A RID: 1882
		[SerializeField]
		private Color _colorOfAllAxesSquareLines = Color.white;

		// Token: 0x0400075B RID: 1883
		[SerializeField]
		private Color _colorOfAllAxesSquareLinesWhenSelected = Color.yellow;

		// Token: 0x0400075C RID: 1884
		[SerializeField]
		private bool _adjustAllAxesScaleSquareWhileScalingObjects = true;

		// Token: 0x0400075D RID: 1885
		[SerializeField]
		private bool _areScaleBoxesLit = true;

		// Token: 0x0400075E RID: 1886
		private Mesh _scaleBoxMesh;

		// Token: 0x0400075F RID: 1887
		private Material[] _litScaleBoxMaterials;

		// Token: 0x04000760 RID: 1888
		private Material[] _unlitScaleBoxMaterials;

		// Token: 0x04000761 RID: 1889
		private Mesh _multiAxisTriangleMesh;

		// Token: 0x04000762 RID: 1890
		private Material[] _multiAxisTriangleMaterials = new Material[3];

		// Token: 0x04000763 RID: 1891
		[SerializeField]
		private bool _adjustAxisLengthWhileScalingObjects = true;

		// Token: 0x04000764 RID: 1892
		[SerializeField]
		private Color[] _multiAxisTriangleColors = new Color[]
		{
			new Color(0f, 0f, 1f, 0.2f),
			new Color(0f, 1f, 0f, 0.2f),
			new Color(1f, 0f, 0f, 0.2f)
		};

		// Token: 0x04000765 RID: 1893
		[SerializeField]
		private Color[] _multiAxisTriangleLineColors = new Color[]
		{
			new Color(0f, 0f, 1f, 1f),
			new Color(0f, 1f, 0f, 1f),
			new Color(1f, 0f, 0f, 1f)
		};

		// Token: 0x04000766 RID: 1894
		[SerializeField]
		private Color _selectedMultiAxisTriangleColor = new Color(1f, 1f, 0f, 0.2f);

		// Token: 0x04000767 RID: 1895
		[SerializeField]
		private Color _selectedMultiAxisTriangleLineColor = new Color(1f, 1f, 0f, 1f);

		// Token: 0x04000768 RID: 1896
		[SerializeField]
		private float _multiAxisTriangleSideLength = 1.3f;

		// Token: 0x04000769 RID: 1897
		[SerializeField]
		private bool _adjustMultiAxisForBetterVisibility = true;

		// Token: 0x0400076A RID: 1898
		private float _accumulatedScaleAxisDrag;

		// Token: 0x0400076B RID: 1899
		private float _accumulatedMultiAxisTriangleDrag;

		// Token: 0x0400076C RID: 1900
		private float _accumulatedAllAxesSquareDragInScreenUnits;

		// Token: 0x0400076D RID: 1901
		private float _accumulatedAllAxesSquareDragInWorldUnits;

		// Token: 0x0400076E RID: 1902
		[SerializeField]
		private bool _adjustMultiAxisTrianglesWhileScalingObjects = true;

		// Token: 0x0400076F RID: 1903
		[SerializeField]
		private bool _drawObjectsLocalAxesWhileScaling = true;

		// Token: 0x04000770 RID: 1904
		[SerializeField]
		private float _objectsLocalAxesLength = 1f;

		// Token: 0x04000771 RID: 1905
		[SerializeField]
		private bool _preserveObjectLocalAxesScreenSize = true;

		// Token: 0x04000772 RID: 1906
		[SerializeField]
		private bool _adjustObjectLocalAxesWhileScalingObjects;

		// Token: 0x04000773 RID: 1907
		[SerializeField]
		private ScaleGizmoSnapSettings _snapSettings = new ScaleGizmoSnapSettings();

		// Token: 0x04000774 RID: 1908
		private Dictionary<GameObject, Vector3> _gameObjectLocalScaleSnapshot = new Dictionary<GameObject, Vector3>();

		// Token: 0x04000775 RID: 1909
		private MultiAxisTriangle _selectedMultiAxisTriangle = MultiAxisTriangle.None;

		// Token: 0x04000776 RID: 1910
		private bool _isAllAxesSquareSelected;
	}
}
