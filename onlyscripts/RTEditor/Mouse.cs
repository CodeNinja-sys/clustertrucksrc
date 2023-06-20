using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001C0 RID: 448
	public class Mouse
	{
		// Token: 0x06000ACE RID: 2766 RVA: 0x00044CDC File Offset: 0x00042EDC
		public Mouse()
		{
			this._cursorPositionInPreviousFrame = Vector2.zero;
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00044CF0 File Offset: 0x00042EF0
		public bool IsLeftMouseButtonDown
		{
			get
			{
				return this._isLeftMouseButtonDown;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00044CF8 File Offset: 0x00042EF8
		public bool IsRightMouseButtonDown
		{
			get
			{
				return this._isRightMouseButtonDown;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00044D00 File Offset: 0x00042F00
		public bool IsMiddleMouseButtonDown
		{
			get
			{
				return this._isMiddleMouseButtonDown;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00044D08 File Offset: 0x00042F08
		public bool WasLeftMouseButtonPressedInCurrentFrame
		{
			get
			{
				return this._wasLeftMouseButtonPressedInCurrentFrame;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x00044D10 File Offset: 0x00042F10
		public bool WasRightMouseButtonPressedInCurrentFrame
		{
			get
			{
				return this._wasRightMouseButtonPressedInCurrentFrame;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x00044D18 File Offset: 0x00042F18
		public bool WasMiddleMouseButtonPressedInCurrentFrame
		{
			get
			{
				return this._wasMiddleMouseButtonPressedInCurrentFrame;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x00044D20 File Offset: 0x00042F20
		public bool WasLeftMouseButtonReleasedInCurrentFrame
		{
			get
			{
				return this._wasLeftMouseButtonReleasedInCurrentFrame;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x00044D28 File Offset: 0x00042F28
		public bool WasRightMouseButtonReleasedInCurrentFrame
		{
			get
			{
				return this._wasRightMouseButtonReleasedInCurrentFrame;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00044D30 File Offset: 0x00042F30
		public bool WasMiddleMouseButtonReleasedInCurrentFrame
		{
			get
			{
				return this._wasMiddleMouseButtonReleasedInCurrentFrame;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00044D38 File Offset: 0x00042F38
		public Vector2 CursorPositionInPreviousFrame
		{
			get
			{
				return this._cursorPositionInPreviousFrame;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x00044D40 File Offset: 0x00042F40
		public Vector2 CursorOffsetSinceLastFrame
		{
			get
			{
				return this._cursorOffsetSinceLastFrame;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00044D48 File Offset: 0x00042F48
		public bool WasMouseMovedSinceLastFrame
		{
			get
			{
				return this._cursorOffsetSinceLastFrame.magnitude != 0f;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x00044D60 File Offset: 0x00042F60
		public Vector2 CursorOffsetSinceLeftMouseButtonDown
		{
			get
			{
				return this._cursorOffsetSinceLeftMouseButtonDown;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00044D68 File Offset: 0x00042F68
		public Vector2 CursorOffsetSinceRightMouseButtonDown
		{
			get
			{
				return this._cursorOffsetSinceRightMouseButtonDown;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x00044D70 File Offset: 0x00042F70
		public Vector2 CursorOffsetSinceMiddleMouseButtonDown
		{
			get
			{
				return this._cursorOffsetSinceMiddleMouseButtonDown;
			}
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00044D78 File Offset: 0x00042F78
		public void UpdateInfoForCurrentFrame()
		{
			this.UpdateMouseButtonStatesForCurrentFrame();
			this.UpdateCursorPositionAndOffsetInfoForCurrentFrame();
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00044D88 File Offset: 0x00042F88
		public void ResetCursorPositionInPreviousFrame()
		{
			this._cursorPositionInPreviousFrame = Input.mousePosition;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00044D9C File Offset: 0x00042F9C
		private void UpdateMouseButtonStatesForCurrentFrame()
		{
			this._wasLeftMouseButtonPressedInCurrentFrame = InputHelper.WasLeftMouseButtonPressedInCurrentFrame();
			this._wasRightMouseButtonPressedInCurrentFrame = InputHelper.WasRightMouseButtonPressedInCurrentFrame();
			this._wasMiddleMouseButtonPressedInCurrentFrame = InputHelper.WasMiddleMouseButtonPressedInCurrentFrame();
			this._wasLeftMouseButtonReleasedInCurrentFrame = InputHelper.WasLeftMouseButtonReleasedInCurrentFrame();
			this._wasRightMouseButtonReleasedInCurrentFrame = InputHelper.WasRightMouseButtonReleasedInCurrentFrame();
			this._wasMiddleMouseButtonReleasedInCurrentFrame = InputHelper.WasMiddleMouseButtonReleasedInCurrentFrame();
			if (this._wasLeftMouseButtonPressedInCurrentFrame)
			{
				this._isLeftMouseButtonDown = true;
			}
			if (this._wasRightMouseButtonPressedInCurrentFrame)
			{
				this._isRightMouseButtonDown = true;
			}
			if (this._wasMiddleMouseButtonPressedInCurrentFrame)
			{
				this._isMiddleMouseButtonDown = true;
			}
			if (this._wasLeftMouseButtonReleasedInCurrentFrame)
			{
				this._isLeftMouseButtonDown = false;
			}
			if (this._wasRightMouseButtonReleasedInCurrentFrame)
			{
				this._isRightMouseButtonDown = false;
			}
			if (this._wasMiddleMouseButtonReleasedInCurrentFrame)
			{
				this._isMiddleMouseButtonDown = false;
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00044E58 File Offset: 0x00043058
		private void UpdateCursorPositionAndOffsetInfoForCurrentFrame()
		{
			Vector2 vector = Input.mousePosition;
			this._cursorOffsetSinceLastFrame = vector - this._cursorPositionInPreviousFrame;
			if (this._isLeftMouseButtonDown)
			{
				this._cursorOffsetSinceLeftMouseButtonDown += this._cursorOffsetSinceLastFrame;
			}
			if (this._isRightMouseButtonDown)
			{
				this._cursorOffsetSinceRightMouseButtonDown += this._cursorOffsetSinceLastFrame;
			}
			if (this._isMiddleMouseButtonDown)
			{
				this._cursorOffsetSinceMiddleMouseButtonDown += this._cursorOffsetSinceLastFrame;
			}
			if (this._wasLeftMouseButtonReleasedInCurrentFrame)
			{
				this._cursorOffsetSinceLeftMouseButtonDown = Vector2.zero;
			}
			if (this._wasRightMouseButtonReleasedInCurrentFrame)
			{
				this._cursorOffsetSinceRightMouseButtonDown = Vector2.zero;
			}
			if (this._wasMiddleMouseButtonReleasedInCurrentFrame)
			{
				this._cursorOffsetSinceMiddleMouseButtonDown = Vector2.zero;
			}
			this._cursorPositionInPreviousFrame = vector;
		}

		// Token: 0x040007A4 RID: 1956
		private bool _isLeftMouseButtonDown;

		// Token: 0x040007A5 RID: 1957
		private bool _isRightMouseButtonDown;

		// Token: 0x040007A6 RID: 1958
		private bool _isMiddleMouseButtonDown;

		// Token: 0x040007A7 RID: 1959
		private bool _wasLeftMouseButtonPressedInCurrentFrame;

		// Token: 0x040007A8 RID: 1960
		private bool _wasRightMouseButtonPressedInCurrentFrame;

		// Token: 0x040007A9 RID: 1961
		private bool _wasMiddleMouseButtonPressedInCurrentFrame;

		// Token: 0x040007AA RID: 1962
		private bool _wasLeftMouseButtonReleasedInCurrentFrame;

		// Token: 0x040007AB RID: 1963
		private bool _wasRightMouseButtonReleasedInCurrentFrame;

		// Token: 0x040007AC RID: 1964
		private bool _wasMiddleMouseButtonReleasedInCurrentFrame;

		// Token: 0x040007AD RID: 1965
		private Vector2 _cursorPositionInPreviousFrame;

		// Token: 0x040007AE RID: 1966
		private Vector2 _cursorOffsetSinceLastFrame;

		// Token: 0x040007AF RID: 1967
		private Vector2 _cursorOffsetSinceLeftMouseButtonDown;

		// Token: 0x040007B0 RID: 1968
		private Vector2 _cursorOffsetSinceRightMouseButtonDown;

		// Token: 0x040007B1 RID: 1969
		private Vector2 _cursorOffsetSinceMiddleMouseButtonDown;
	}
}
