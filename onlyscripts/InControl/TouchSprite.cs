using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000080 RID: 128
	[Serializable]
	public class TouchSprite
	{
		// Token: 0x06000431 RID: 1073 RVA: 0x00012ED0 File Offset: 0x000110D0
		public TouchSprite()
		{
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00012F40 File Offset: 0x00011140
		public TouchSprite(float size)
		{
			this.size = Vector2.one * size;
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00012FC0 File Offset: 0x000111C0
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x00012FC8 File Offset: 0x000111C8
		public bool Dirty { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00012FD4 File Offset: 0x000111D4
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x00012FDC File Offset: 0x000111DC
		public bool Ready { get; set; }

		// Token: 0x06000437 RID: 1079 RVA: 0x00012FE8 File Offset: 0x000111E8
		public void Create(string gameObjectName, Transform parentTransform, int sortingOrder)
		{
			this.spriteGameObject = this.CreateSpriteGameObject(gameObjectName, parentTransform);
			this.spriteRenderer = this.CreateSpriteRenderer(this.spriteGameObject, this.idleSprite, sortingOrder);
			this.spriteRenderer.color = this.idleColor;
			this.Ready = true;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00013034 File Offset: 0x00011234
		public void Delete()
		{
			this.Ready = false;
			UnityEngine.Object.Destroy(this.spriteGameObject);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00013048 File Offset: 0x00011248
		public void Update()
		{
			this.Update(false);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00013054 File Offset: 0x00011254
		public void Update(bool forceUpdate)
		{
			if (this.Dirty || forceUpdate)
			{
				if (this.spriteRenderer != null)
				{
					this.spriteRenderer.sprite = ((!this.State) ? this.idleSprite : this.busySprite);
				}
				if (this.sizeUnitType == TouchUnitType.Pixels)
				{
					Vector2 a = TouchUtility.RoundVector(this.size);
					this.ScaleSpriteInPixels(this.spriteGameObject, this.spriteRenderer, a);
					this.worldSize = a * TouchManager.PixelToWorld;
				}
				else
				{
					this.ScaleSpriteInPercent(this.spriteGameObject, this.spriteRenderer, this.size);
					if (this.lockAspectRatio)
					{
						this.worldSize = this.size * TouchManager.PercentToWorld;
					}
					else
					{
						this.worldSize = Vector2.Scale(this.size, TouchManager.ViewSize);
					}
				}
				this.Dirty = false;
			}
			if (this.spriteRenderer != null)
			{
				Color color = (!this.State) ? this.idleColor : this.busyColor;
				if (this.spriteRenderer.color != color)
				{
					this.spriteRenderer.color = Utility.MoveColorTowards(this.spriteRenderer.color, color, 5f * Time.deltaTime);
				}
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000131B8 File Offset: 0x000113B8
		private GameObject CreateSpriteGameObject(string name, Transform parentTransform)
		{
			return new GameObject(name)
			{
				transform = 
				{
					parent = parentTransform,
					localPosition = Vector3.zero,
					localScale = Vector3.one
				},
				layer = parentTransform.gameObject.layer
			};
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001320C File Offset: 0x0001140C
		private SpriteRenderer CreateSpriteRenderer(GameObject spriteGameObject, Sprite sprite, int sortingOrder)
		{
			SpriteRenderer spriteRenderer = spriteGameObject.AddComponent<SpriteRenderer>();
			spriteRenderer.sprite = sprite;
			spriteRenderer.sortingOrder = sortingOrder;
			spriteRenderer.sharedMaterial = new Material(Shader.Find("Sprites/Default"));
			spriteRenderer.sharedMaterial.SetFloat("PixelSnap", 1f);
			return spriteRenderer;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001325C File Offset: 0x0001145C
		private void ScaleSpriteInPixels(GameObject spriteGameObject, SpriteRenderer spriteRenderer, Vector2 size)
		{
			if (spriteGameObject == null || spriteRenderer == null || spriteRenderer.sprite == null)
			{
				return;
			}
			float num = spriteRenderer.sprite.rect.width / spriteRenderer.sprite.bounds.size.x;
			float num2 = TouchManager.PixelToWorld * num;
			float x = num2 * size.x / spriteRenderer.sprite.rect.width;
			float y = num2 * size.y / spriteRenderer.sprite.rect.height;
			spriteGameObject.transform.localScale = new Vector3(x, y);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00013320 File Offset: 0x00011520
		private void ScaleSpriteInPercent(GameObject spriteGameObject, SpriteRenderer spriteRenderer, Vector2 size)
		{
			if (spriteGameObject == null || spriteRenderer == null || spriteRenderer.sprite == null)
			{
				return;
			}
			if (this.lockAspectRatio)
			{
				float num = Mathf.Min(TouchManager.ViewSize.x, TouchManager.ViewSize.y);
				float x = num * size.x / spriteRenderer.sprite.bounds.size.x;
				float y = num * size.y / spriteRenderer.sprite.bounds.size.y;
				spriteGameObject.transform.localScale = new Vector3(x, y);
			}
			else
			{
				float x2 = TouchManager.ViewSize.x * size.x / spriteRenderer.sprite.bounds.size.x;
				float y2 = TouchManager.ViewSize.y * size.y / spriteRenderer.sprite.bounds.size.y;
				spriteGameObject.transform.localScale = new Vector3(x2, y2);
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001346C File Offset: 0x0001166C
		public bool Contains(Vector2 testWorldPoint)
		{
			if (this.shape == TouchSpriteShape.Oval)
			{
				float num = (testWorldPoint.x - this.Position.x) / this.worldSize.x;
				float num2 = (testWorldPoint.y - this.Position.y) / this.worldSize.y;
				return num * num + num2 * num2 < 0.25f;
			}
			float num3 = Utility.Abs(testWorldPoint.x - this.Position.x) * 2f;
			float num4 = Utility.Abs(testWorldPoint.y - this.Position.y) * 2f;
			return num3 <= this.worldSize.x && num4 <= this.worldSize.y;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00013548 File Offset: 0x00011748
		public bool Contains(Touch touch)
		{
			return this.Contains(TouchManager.ScreenToWorldPoint(touch.position));
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00013560 File Offset: 0x00011760
		public void DrawGizmos(Vector3 position, Color color)
		{
			if (this.shape == TouchSpriteShape.Oval)
			{
				Utility.DrawOvalGizmo(position, this.WorldSize, color);
			}
			else
			{
				Utility.DrawRectGizmo(position, this.WorldSize, color);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x000135A4 File Offset: 0x000117A4
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x000135AC File Offset: 0x000117AC
		public bool State
		{
			get
			{
				return this.state;
			}
			set
			{
				if (this.state != value)
				{
					this.state = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x000135C8 File Offset: 0x000117C8
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x000135D0 File Offset: 0x000117D0
		public Sprite BusySprite
		{
			get
			{
				return this.busySprite;
			}
			set
			{
				if (this.busySprite != value)
				{
					this.busySprite = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x000135F4 File Offset: 0x000117F4
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x000135FC File Offset: 0x000117FC
		public Sprite IdleSprite
		{
			get
			{
				return this.idleSprite;
			}
			set
			{
				if (this.idleSprite != value)
				{
					this.idleSprite = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x170000E2 RID: 226
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x00013620 File Offset: 0x00011820
		public Sprite Sprite
		{
			set
			{
				if (this.idleSprite != value)
				{
					this.idleSprite = value;
					this.Dirty = true;
				}
				if (this.busySprite != value)
				{
					this.busySprite = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0001366C File Offset: 0x0001186C
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x00013674 File Offset: 0x00011874
		public Color BusyColor
		{
			get
			{
				return this.busyColor;
			}
			set
			{
				if (this.busyColor != value)
				{
					this.busyColor = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00013698 File Offset: 0x00011898
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x000136A0 File Offset: 0x000118A0
		public Color IdleColor
		{
			get
			{
				return this.idleColor;
			}
			set
			{
				if (this.idleColor != value)
				{
					this.idleColor = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x000136C4 File Offset: 0x000118C4
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x000136CC File Offset: 0x000118CC
		public TouchSpriteShape Shape
		{
			get
			{
				return this.shape;
			}
			set
			{
				if (this.shape != value)
				{
					this.shape = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x000136E8 File Offset: 0x000118E8
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x000136F0 File Offset: 0x000118F0
		public TouchUnitType SizeUnitType
		{
			get
			{
				return this.sizeUnitType;
			}
			set
			{
				if (this.sizeUnitType != value)
				{
					this.sizeUnitType = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0001370C File Offset: 0x0001190C
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x00013714 File Offset: 0x00011914
		public Vector2 Size
		{
			get
			{
				return this.size;
			}
			set
			{
				if (this.size != value)
				{
					this.size = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00013738 File Offset: 0x00011938
		public Vector2 WorldSize
		{
			get
			{
				return this.worldSize;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00013740 File Offset: 0x00011940
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x00013778 File Offset: 0x00011978
		public Vector3 Position
		{
			get
			{
				return (!this.spriteGameObject) ? Vector3.zero : this.spriteGameObject.transform.position;
			}
			set
			{
				if (this.spriteGameObject)
				{
					this.spriteGameObject.transform.position = value;
				}
			}
		}

		// Token: 0x0400037D RID: 893
		[SerializeField]
		private Sprite idleSprite;

		// Token: 0x0400037E RID: 894
		[SerializeField]
		private Sprite busySprite;

		// Token: 0x0400037F RID: 895
		[SerializeField]
		private Color idleColor = new Color(1f, 1f, 1f, 0.5f);

		// Token: 0x04000380 RID: 896
		[SerializeField]
		private Color busyColor = new Color(1f, 1f, 1f, 1f);

		// Token: 0x04000381 RID: 897
		[SerializeField]
		private TouchSpriteShape shape;

		// Token: 0x04000382 RID: 898
		[SerializeField]
		private TouchUnitType sizeUnitType;

		// Token: 0x04000383 RID: 899
		[SerializeField]
		private Vector2 size = new Vector2(10f, 10f);

		// Token: 0x04000384 RID: 900
		[SerializeField]
		private bool lockAspectRatio = true;

		// Token: 0x04000385 RID: 901
		[SerializeField]
		[HideInInspector]
		private Vector2 worldSize;

		// Token: 0x04000386 RID: 902
		private Transform spriteParentTransform;

		// Token: 0x04000387 RID: 903
		private GameObject spriteGameObject;

		// Token: 0x04000388 RID: 904
		private SpriteRenderer spriteRenderer;

		// Token: 0x04000389 RID: 905
		private bool state;
	}
}
