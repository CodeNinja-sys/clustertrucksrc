using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x02000180 RID: 384
	public class EditorCamera : MonoSingletonBase<EditorCamera>
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x00037F18 File Offset: 0x00036118
		public static float MinZoomSpeed
		{
			get
			{
				return 0.01f;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x00037F20 File Offset: 0x00036120
		public static float MinPanSpeed
		{
			get
			{
				return 0.01f;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x00037F28 File Offset: 0x00036128
		public static float MinRotationSpeedInDegrees
		{
			get
			{
				return 0.01f;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x00037F30 File Offset: 0x00036130
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x00037F38 File Offset: 0x00036138
		public float OrthographicZoomSpeed
		{
			get
			{
				return this._orthographicZoomSpeed;
			}
			set
			{
				this._orthographicZoomSpeed = Mathf.Max(value, EditorCamera.MinZoomSpeed);
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00037F4C File Offset: 0x0003614C
		// (set) Token: 0x0600088D RID: 2189 RVA: 0x00037F54 File Offset: 0x00036154
		public float PerspectiveZoomSpeed
		{
			get
			{
				return this._perspectiveZoomSpeed;
			}
			set
			{
				this._perspectiveZoomSpeed = Mathf.Max(value, EditorCamera.MinZoomSpeed);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x00037F68 File Offset: 0x00036168
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x00037F70 File Offset: 0x00036170
		public float PanSpeed
		{
			get
			{
				return this._panSpeed;
			}
			set
			{
				this._panSpeed = Mathf.Max(value, EditorCamera.MinPanSpeed);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x00037F84 File Offset: 0x00036184
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x00037F8C File Offset: 0x0003618C
		public float RotationSpeedInDegrees
		{
			get
			{
				return this._rotationSpeedInDegrees;
			}
			set
			{
				this._rotationSpeedInDegrees = Mathf.Max(value, EditorCamera.MinRotationSpeedInDegrees);
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x00037FA0 File Offset: 0x000361A0
		public Camera Camera
		{
			get
			{
				return levelEditorManager.Instance().MainCamera;
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00037FAC File Offset: 0x000361AC
		public List<GameObject> GetVisibleGameObjects()
		{
			return this._camera.GetVisibleGameObjects();
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00037FBC File Offset: 0x000361BC
		private void Awake()
		{
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00037FC0 File Offset: 0x000361C0
		private void Start()
		{
			this._camera = levelEditorManager.Instance().MainCamera;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00037FD4 File Offset: 0x000361D4
		private void Update()
		{
			if (this._applicationJustGainedFocus)
			{
				this._applicationJustGainedFocus = false;
				this._mouse.ResetCursorPositionInPreviousFrame();
			}
			this._mouse.UpdateInfoForCurrentFrame();
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0003800C File Offset: 0x0003620C
		private void ApplyCameraZoomBasedOnUserInput()
		{
			float num = (!this._camera.orthographic) ? this._perspectiveZoomSpeed : this._orthographicZoomSpeed;
			EditorCameraZoom.ZoomCamera(this._camera, Input.GetAxis("Mouse ScrollWheel") * num);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00038054 File Offset: 0x00036254
		private void PanCameraBasedOnUserInput()
		{
			if (this._mouse.IsMiddleMouseButtonDown)
			{
				float num = Time.deltaTime * this._panSpeed;
				EditorCameraPan.PanCamera(this._camera, -this._mouse.CursorOffsetSinceLastFrame.x * num, -this._mouse.CursorOffsetSinceLastFrame.y * num);
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000380B8 File Offset: 0x000362B8
		private void RotateCameraBasedOnUserInput()
		{
			if (this._mouse.IsRightMouseButtonDown)
			{
				float num = this._rotationSpeedInDegrees * Time.deltaTime;
				EditorCameraRotation.RotateCamera(this._camera, -this._mouse.CursorOffsetSinceLastFrame.y * num, this._mouse.CursorOffsetSinceLastFrame.x * num);
			}
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00038118 File Offset: 0x00036318
		private void OnApplicationFocus(bool focusStatus)
		{
			if (focusStatus)
			{
				this._applicationJustGainedFocus = true;
			}
		}

		// Token: 0x040006BC RID: 1724
		[SerializeField]
		private float _orthographicZoomSpeed = 3f;

		// Token: 0x040006BD RID: 1725
		[SerializeField]
		private float _perspectiveZoomSpeed = 20f;

		// Token: 0x040006BE RID: 1726
		[SerializeField]
		private float _panSpeed = 3f;

		// Token: 0x040006BF RID: 1727
		[SerializeField]
		private float _rotationSpeedInDegrees = 8.8f;

		// Token: 0x040006C0 RID: 1728
		private Camera _camera;

		// Token: 0x040006C1 RID: 1729
		private Mouse _mouse = new Mouse();

		// Token: 0x040006C2 RID: 1730
		private bool _applicationJustGainedFocus;
	}
}
