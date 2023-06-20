using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x02000190 RID: 400
	public class EditorShortuctKeys : MonoSingletonBase<EditorShortuctKeys>
	{
		// Token: 0x0600090F RID: 2319 RVA: 0x000398DC File Offset: 0x00037ADC
		private void Update()
		{
			this.HandleEditorGizmoSystemKeys();
			this.HandleEditorUndoRedoSystemKeys();
			this.HandleEditorObjectSelectionKeys();
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000398F0 File Offset: 0x00037AF0
		private void HandleEditorGizmoSystemKeys()
		{
			EditorGizmoSystem instance = MonoSingletonBase<EditorGizmoSystem>.Instance;
			if (!Input.GetKeyDown(KeyCode.Q))
			{
				if (!Input.GetKeyDown(KeyCode.W))
				{
					if (!Input.GetKeyDown(KeyCode.R))
					{
						if (!Input.GetKeyDown(KeyCode.G))
						{
							if (!Input.GetKeyDown(KeyCode.L))
							{
								if (!Input.GetKeyDown(KeyCode.Q))
								{
									if (Input.GetKeyDown(KeyCode.P))
									{
										TransformPivotPoint newPivotPoint = (instance.TransformPivotPoint != TransformPivotPoint.Center) ? TransformPivotPoint.Center : TransformPivotPoint.MeshPivot;
										TransformPivotPointChangeAction transformPivotPointChangeAction = new TransformPivotPointChangeAction(instance.TransformPivotPoint, newPivotPoint);
										transformPivotPointChangeAction.Execute();
									}
								}
							}
						}
					}
				}
			}
			GizmoType activeGizmoType = instance.ActiveGizmoType;
			if (activeGizmoType == GizmoType.Translation)
			{
				TranslationGizmo translationGizmo = instance.TranslationGizmo;
				bool flag = InputHelper.IsAnyShiftKeyPressed();
				if (flag)
				{
					translationGizmo.TranslateAlongCameraRightAndUpAxes = true;
				}
				else
				{
					translationGizmo.TranslateAlongCameraRightAndUpAxes = false;
				}
				bool flag2 = InputHelper.IsAnyCtrlOrCommandKeyPressed();
				if (flag2)
				{
					translationGizmo.SnapSettings.IsStepSnappingEnabled = true;
				}
				else
				{
					translationGizmo.SnapSettings.IsStepSnappingEnabled = false;
				}
				if (Input.GetKeyDown(KeyCode.V))
				{
					translationGizmo.SnapSettings.IsVertexSnappingEnabled = true;
					VertexSnappingEnabledMessage.SendToInterestedListeners();
				}
				if (Input.GetKeyUp(KeyCode.V))
				{
					translationGizmo.SnapSettings.IsVertexSnappingEnabled = false;
					VertexSnappingDisabledMessage.SendToInterestedListeners();
				}
			}
			else if (activeGizmoType == GizmoType.Rotation)
			{
				RotationGizmo rotationGizmo = instance.RotationGizmo;
				bool flag3 = InputHelper.IsAnyCtrlOrCommandKeyPressed();
				if (flag3)
				{
					rotationGizmo.SnapSettings.IsSnappingEnabled = true;
				}
				else
				{
					rotationGizmo.SnapSettings.IsSnappingEnabled = false;
				}
			}
			else if (activeGizmoType == GizmoType.Scale)
			{
				ScaleGizmo scaleGizmo = instance.ScaleGizmo;
				bool flag4 = InputHelper.IsAnyShiftKeyPressed();
				if (flag4)
				{
					scaleGizmo.ScaleAlongAllAxes = true;
				}
				else
				{
					scaleGizmo.ScaleAlongAllAxes = false;
				}
				bool flag5 = InputHelper.IsAnyCtrlOrCommandKeyPressed();
				if (flag5)
				{
					scaleGizmo.SnapSettings.IsSnappingEnabled = true;
				}
				else
				{
					scaleGizmo.SnapSettings.IsSnappingEnabled = false;
				}
			}
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00039AE4 File Offset: 0x00037CE4
		private void HandleEditorUndoRedoSystemKeys()
		{
			EditorUndoRedoSystem instance = MonoSingletonBase<EditorUndoRedoSystem>.Instance;
			if (!Application.isEditor)
			{
				if (Input.GetKeyDown(KeyCode.Z) && InputHelper.IsAnyCtrlOrCommandKeyPressed())
				{
					instance.Undo();
				}
				else if (Input.GetKeyDown(KeyCode.Y) && InputHelper.IsAnyCtrlOrCommandKeyPressed())
				{
					instance.Redo();
				}
			}
			else if (Input.GetKeyDown(KeyCode.Z) && InputHelper.IsAnyCtrlOrCommandKeyPressed() && InputHelper.IsAnyShiftKeyPressed())
			{
				instance.Undo();
			}
			else if (Input.GetKeyDown(KeyCode.Y) && InputHelper.IsAnyCtrlOrCommandKeyPressed() && InputHelper.IsAnyShiftKeyPressed())
			{
				instance.Redo();
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00039B94 File Offset: 0x00037D94
		private void HandleEditorObjectSelectionKeys()
		{
			EditorObjectSelection instance = MonoSingletonBase<EditorObjectSelection>.Instance;
			if (InputHelper.IsAnyCtrlOrCommandKeyPressed())
			{
				instance.AddObjectsToSelectionEnabled = true;
			}
			else
			{
				instance.AddObjectsToSelectionEnabled = false;
			}
			if (InputHelper.IsAnyShiftKeyPressed())
			{
				instance.MultiObjectDeselectEnabled = true;
			}
			else
			{
				instance.MultiObjectDeselectEnabled = false;
			}
		}
	}
}
