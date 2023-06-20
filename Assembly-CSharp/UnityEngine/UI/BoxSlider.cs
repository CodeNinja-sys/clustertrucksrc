using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200002D RID: 45
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("UI/BoxSlider", 35)]
	public class BoxSlider : Selectable, IDragHandler, IEventSystemHandler, IInitializePotentialDragHandler, ICanvasElement
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x0000765C File Offset: 0x0000585C
		protected BoxSlider()
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000769C File Offset: 0x0000589C
		public void LayoutComplete()
		{
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000076A0 File Offset: 0x000058A0
		public void GraphicUpdateComplete()
		{
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000076A4 File Offset: 0x000058A4
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x000076AC File Offset: 0x000058AC
		public RectTransform handleRect
		{
			get
			{
				return this.m_HandleRect;
			}
			set
			{
				if (BoxSlider.SetClass<RectTransform>(ref this.m_HandleRect, value))
				{
					this.UpdateCachedReferences();
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000076CC File Offset: 0x000058CC
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x000076D4 File Offset: 0x000058D4
		public float minValue
		{
			get
			{
				return this.m_MinValue;
			}
			set
			{
				if (BoxSlider.SetStruct<float>(ref this.m_MinValue, value))
				{
					this.Set(this.m_Value);
					this.SetY(this.m_ValueY);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00007708 File Offset: 0x00005908
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00007710 File Offset: 0x00005910
		public float maxValue
		{
			get
			{
				return this.m_MaxValue;
			}
			set
			{
				if (BoxSlider.SetStruct<float>(ref this.m_MaxValue, value))
				{
					this.Set(this.m_Value);
					this.SetY(this.m_ValueY);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00007744 File Offset: 0x00005944
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000774C File Offset: 0x0000594C
		public bool wholeNumbers
		{
			get
			{
				return this.m_WholeNumbers;
			}
			set
			{
				if (BoxSlider.SetStruct<bool>(ref this.m_WholeNumbers, value))
				{
					this.Set(this.m_Value);
					this.SetY(this.m_ValueY);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00007780 File Offset: 0x00005980
		// (set) Token: 0x060000FE RID: 254 RVA: 0x000077A0 File Offset: 0x000059A0
		public float value
		{
			get
			{
				if (this.wholeNumbers)
				{
					return Mathf.Round(this.m_Value);
				}
				return this.m_Value;
			}
			set
			{
				this.Set(value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000077AC File Offset: 0x000059AC
		// (set) Token: 0x06000100 RID: 256 RVA: 0x000077EC File Offset: 0x000059EC
		public float normalizedValue
		{
			get
			{
				if (Mathf.Approximately(this.minValue, this.maxValue))
				{
					return 0f;
				}
				return Mathf.InverseLerp(this.minValue, this.maxValue, this.value);
			}
			set
			{
				this.value = Mathf.Lerp(this.minValue, this.maxValue, value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00007814 File Offset: 0x00005A14
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00007834 File Offset: 0x00005A34
		public float valueY
		{
			get
			{
				if (this.wholeNumbers)
				{
					return Mathf.Round(this.m_ValueY);
				}
				return this.m_ValueY;
			}
			set
			{
				this.SetY(value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00007840 File Offset: 0x00005A40
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00007880 File Offset: 0x00005A80
		public float normalizedValueY
		{
			get
			{
				if (Mathf.Approximately(this.minValue, this.maxValue))
				{
					return 0f;
				}
				return Mathf.InverseLerp(this.minValue, this.maxValue, this.valueY);
			}
			set
			{
				this.valueY = Mathf.Lerp(this.minValue, this.maxValue, value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000078A8 File Offset: 0x00005AA8
		// (set) Token: 0x06000106 RID: 262 RVA: 0x000078B0 File Offset: 0x00005AB0
		public BoxSlider.BoxSliderEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				this.m_OnValueChanged = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000078BC File Offset: 0x00005ABC
		private float stepSize
		{
			get
			{
				return (!this.wholeNumbers) ? ((this.maxValue - this.minValue) * 0.1f) : 1f;
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000078F4 File Offset: 0x00005AF4
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000078F8 File Offset: 0x00005AF8
		public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00007954 File Offset: 0x00005B54
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00007978 File Offset: 0x00005B78
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateCachedReferences();
			this.Set(this.m_Value, false);
			this.SetY(this.m_ValueY, false);
			this.UpdateVisuals();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000079B4 File Offset: 0x00005BB4
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			base.OnDisable();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000079C8 File Offset: 0x00005BC8
		private void UpdateCachedReferences()
		{
			if (this.m_HandleRect)
			{
				this.m_HandleTransform = this.m_HandleRect.transform;
				if (this.m_HandleTransform.parent != null)
				{
					this.m_HandleContainerRect = this.m_HandleTransform.parent.GetComponent<RectTransform>();
				}
			}
			else
			{
				this.m_HandleContainerRect = null;
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00007A30 File Offset: 0x00005C30
		private void Set(float input)
		{
			this.Set(input, true);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00007A3C File Offset: 0x00005C3C
		private void Set(float input, bool sendCallback)
		{
			float num = Mathf.Clamp(input, this.minValue, this.maxValue);
			if (this.wholeNumbers)
			{
				num = Mathf.Round(num);
			}
			if (this.m_Value == num)
			{
				return;
			}
			this.m_Value = num;
			this.UpdateVisuals();
			if (sendCallback)
			{
				this.m_OnValueChanged.Invoke(num, this.valueY);
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00007AA0 File Offset: 0x00005CA0
		private void SetY(float input)
		{
			this.SetY(input, true);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00007AAC File Offset: 0x00005CAC
		private void SetY(float input, bool sendCallback)
		{
			float num = Mathf.Clamp(input, this.minValue, this.maxValue);
			if (this.wholeNumbers)
			{
				num = Mathf.Round(num);
			}
			if (this.m_ValueY == num)
			{
				return;
			}
			this.m_ValueY = num;
			this.UpdateVisuals();
			if (sendCallback)
			{
				this.m_OnValueChanged.Invoke(this.value, num);
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00007B10 File Offset: 0x00005D10
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			this.UpdateVisuals();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00007B20 File Offset: 0x00005D20
		private void UpdateVisuals()
		{
			this.m_Tracker.Clear();
			if (this.m_HandleContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_HandleRect, DrivenTransformProperties.Anchors);
				Vector2 zero = Vector2.zero;
				Vector2 one = Vector2.one;
				int index = 0;
				float value = this.normalizedValue;
				one[0] = value;
				zero[index] = value;
				int index2 = 1;
				value = this.normalizedValueY;
				one[1] = value;
				zero[index2] = value;
				this.m_HandleRect.anchorMin = zero;
				this.m_HandleRect.anchorMax = one;
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00007BB8 File Offset: 0x00005DB8
		private void UpdateDrag(PointerEventData eventData, Camera cam)
		{
			RectTransform handleContainerRect = this.m_HandleContainerRect;
			if (handleContainerRect != null && handleContainerRect.rect.size[0] > 0f)
			{
				Vector2 a;
				if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(handleContainerRect, eventData.position, cam, out a))
				{
					return;
				}
				a -= handleContainerRect.rect.position;
				float normalizedValue = Mathf.Clamp01((a - this.m_Offset)[0] / handleContainerRect.rect.size[0]);
				this.normalizedValue = normalizedValue;
				float normalizedValueY = Mathf.Clamp01((a - this.m_Offset)[1] / handleContainerRect.rect.size[1]);
				this.normalizedValueY = normalizedValueY;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00007CA0 File Offset: 0x00005EA0
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00007CD0 File Offset: 0x00005ED0
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			base.OnPointerDown(eventData);
			this.m_Offset = Vector2.zero;
			if (this.m_HandleContainerRect != null && RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.position, eventData.enterEventCamera))
			{
				Vector2 offset;
				if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.position, eventData.pressEventCamera, out offset))
				{
					this.m_Offset = offset;
				}
				this.m_Offset.y = -this.m_Offset.y;
			}
			else
			{
				this.UpdateDrag(eventData, eventData.pressEventCamera);
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00007D78 File Offset: 0x00005F78
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.UpdateDrag(eventData, eventData.pressEventCamera);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00007D94 File Offset: 0x00005F94
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00007DA0 File Offset: 0x00005FA0
		virtual Transform get_transform()
		{
			return base.transform;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007DA8 File Offset: 0x00005FA8
		virtual bool IsDestroyed()
		{
			return base.IsDestroyed();
		}

		// Token: 0x040000C5 RID: 197
		[SerializeField]
		private RectTransform m_HandleRect;

		// Token: 0x040000C6 RID: 198
		[Space(6f)]
		[SerializeField]
		private float m_MinValue;

		// Token: 0x040000C7 RID: 199
		[SerializeField]
		private float m_MaxValue = 1f;

		// Token: 0x040000C8 RID: 200
		[SerializeField]
		private bool m_WholeNumbers;

		// Token: 0x040000C9 RID: 201
		[SerializeField]
		private float m_Value = 1f;

		// Token: 0x040000CA RID: 202
		[SerializeField]
		private float m_ValueY = 1f;

		// Token: 0x040000CB RID: 203
		[Space(6f)]
		[SerializeField]
		private BoxSlider.BoxSliderEvent m_OnValueChanged = new BoxSlider.BoxSliderEvent();

		// Token: 0x040000CC RID: 204
		private Transform m_HandleTransform;

		// Token: 0x040000CD RID: 205
		private RectTransform m_HandleContainerRect;

		// Token: 0x040000CE RID: 206
		private Vector2 m_Offset = Vector2.zero;

		// Token: 0x040000CF RID: 207
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x0200002E RID: 46
		public enum Direction
		{
			// Token: 0x040000D1 RID: 209
			LeftToRight,
			// Token: 0x040000D2 RID: 210
			RightToLeft,
			// Token: 0x040000D3 RID: 211
			BottomToTop,
			// Token: 0x040000D4 RID: 212
			TopToBottom
		}

		// Token: 0x0200002F RID: 47
		[Serializable]
		public class BoxSliderEvent : UnityEvent<float, float>
		{
		}

		// Token: 0x02000030 RID: 48
		private enum Axis
		{
			// Token: 0x040000D6 RID: 214
			Horizontal,
			// Token: 0x040000D7 RID: 215
			Vertical
		}
	}
}
