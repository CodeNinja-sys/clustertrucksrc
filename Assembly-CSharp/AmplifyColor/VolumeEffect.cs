using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace AmplifyColor
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	public class VolumeEffect
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00004290 File Offset: 0x00002490
		public VolumeEffect(AmplifyColorBase effect)
		{
			this.gameObject = effect;
			this.components = new List<VolumeEffectComponent>();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000042AC File Offset: 0x000024AC
		public static VolumeEffect BlendValuesToVolumeEffect(VolumeEffectFlags flags, VolumeEffect volume1, VolumeEffect volume2, float blend)
		{
			VolumeEffect volumeEffect = new VolumeEffect(volume1.gameObject);
			VolumeEffectComponentFlags compFlags;
			foreach (VolumeEffectComponentFlags compFlags2 in flags.components)
			{
				compFlags = compFlags2;
				if (compFlags.blendFlag)
				{
					VolumeEffectComponent volumeEffectComponent = volume1.components.Find((VolumeEffectComponent s) => s.componentName == compFlags.componentName);
					VolumeEffectComponent volumeEffectComponent2 = volume2.components.Find((VolumeEffectComponent s) => s.componentName == compFlags.componentName);
					if (volumeEffectComponent != null && volumeEffectComponent2 != null)
					{
						VolumeEffectComponent volumeEffectComponent3 = new VolumeEffectComponent(volumeEffectComponent.componentName);
						VolumeEffectFieldFlags fieldFlags;
						foreach (VolumeEffectFieldFlags fieldFlags2 in compFlags.componentFields)
						{
							fieldFlags = fieldFlags2;
							if (fieldFlags.blendFlag)
							{
								VolumeEffectField volumeEffectField = volumeEffectComponent.fields.Find((VolumeEffectField s) => s.fieldName == fieldFlags.fieldName);
								VolumeEffectField volumeEffectField2 = volumeEffectComponent2.fields.Find((VolumeEffectField s) => s.fieldName == fieldFlags.fieldName);
								if (volumeEffectField != null && volumeEffectField2 != null)
								{
									VolumeEffectField volumeEffectField3 = new VolumeEffectField(volumeEffectField.fieldName, volumeEffectField.fieldType);
									string fieldType = volumeEffectField3.fieldType;
									switch (fieldType)
									{
									case "System.Single":
										volumeEffectField3.valueSingle = Mathf.Lerp(volumeEffectField.valueSingle, volumeEffectField2.valueSingle, blend);
										break;
									case "System.Boolean":
										volumeEffectField3.valueBoolean = volumeEffectField2.valueBoolean;
										break;
									case "UnityEngine.Vector2":
										volumeEffectField3.valueVector2 = Vector2.Lerp(volumeEffectField.valueVector2, volumeEffectField2.valueVector2, blend);
										break;
									case "UnityEngine.Vector3":
										volumeEffectField3.valueVector3 = Vector3.Lerp(volumeEffectField.valueVector3, volumeEffectField2.valueVector3, blend);
										break;
									case "UnityEngine.Vector4":
										volumeEffectField3.valueVector4 = Vector4.Lerp(volumeEffectField.valueVector4, volumeEffectField2.valueVector4, blend);
										break;
									case "UnityEngine.Color":
										volumeEffectField3.valueColor = Color.Lerp(volumeEffectField.valueColor, volumeEffectField2.valueColor, blend);
										break;
									}
									volumeEffectComponent3.fields.Add(volumeEffectField3);
								}
							}
						}
						volumeEffect.components.Add(volumeEffectComponent3);
					}
				}
			}
			return volumeEffect;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000045D8 File Offset: 0x000027D8
		public VolumeEffectComponent AddComponent(Component c, VolumeEffectComponentFlags compFlags)
		{
			if (compFlags == null)
			{
				VolumeEffectComponent volumeEffectComponent = new VolumeEffectComponent(c.GetType() + string.Empty);
				this.components.Add(volumeEffectComponent);
				return volumeEffectComponent;
			}
			VolumeEffectComponent volumeEffectComponent2;
			if ((volumeEffectComponent2 = this.components.Find((VolumeEffectComponent s) => s.componentName == c.GetType() + string.Empty)) != null)
			{
				volumeEffectComponent2.UpdateComponent(c, compFlags);
				return volumeEffectComponent2;
			}
			VolumeEffectComponent volumeEffectComponent3 = new VolumeEffectComponent(c, compFlags);
			this.components.Add(volumeEffectComponent3);
			return volumeEffectComponent3;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004668 File Offset: 0x00002868
		public void RemoveEffectComponent(VolumeEffectComponent comp)
		{
			this.components.Remove(comp);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004678 File Offset: 0x00002878
		public void UpdateVolume()
		{
			if (this.gameObject == null)
			{
				return;
			}
			VolumeEffectFlags effectFlags = this.gameObject.EffectFlags;
			foreach (VolumeEffectComponentFlags volumeEffectComponentFlags in effectFlags.components)
			{
				if (volumeEffectComponentFlags.blendFlag)
				{
					Component component = this.gameObject.GetComponent(volumeEffectComponentFlags.componentName);
					if (component != null)
					{
						this.AddComponent(component, volumeEffectComponentFlags);
					}
				}
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000472C File Offset: 0x0000292C
		public void SetValues(AmplifyColorBase targetColor)
		{
			VolumeEffectFlags effectFlags = targetColor.EffectFlags;
			GameObject gameObject = targetColor.gameObject;
			VolumeEffectComponentFlags compFlags;
			foreach (VolumeEffectComponentFlags compFlags2 in effectFlags.components)
			{
				compFlags = compFlags2;
				if (compFlags.blendFlag)
				{
					Component component = gameObject.GetComponent(compFlags.componentName);
					VolumeEffectComponent volumeEffectComponent = this.components.Find((VolumeEffectComponent s) => s.componentName == compFlags.componentName);
					if (!(component == null) && volumeEffectComponent != null)
					{
						VolumeEffectFieldFlags fieldFlags;
						foreach (VolumeEffectFieldFlags fieldFlags2 in compFlags.componentFields)
						{
							fieldFlags = fieldFlags2;
							if (fieldFlags.blendFlag)
							{
								FieldInfo field = component.GetType().GetField(fieldFlags.fieldName);
								VolumeEffectField volumeEffectField = volumeEffectComponent.fields.Find((VolumeEffectField s) => s.fieldName == fieldFlags.fieldName);
								if (field != null && volumeEffectField != null)
								{
									string fullName = field.FieldType.FullName;
									switch (fullName)
									{
									case "System.Single":
										field.SetValue(component, volumeEffectField.valueSingle);
										break;
									case "System.Boolean":
										field.SetValue(component, volumeEffectField.valueBoolean);
										break;
									case "UnityEngine.Vector2":
										field.SetValue(component, volumeEffectField.valueVector2);
										break;
									case "UnityEngine.Vector3":
										field.SetValue(component, volumeEffectField.valueVector3);
										break;
									case "UnityEngine.Vector4":
										field.SetValue(component, volumeEffectField.valueVector4);
										break;
									case "UnityEngine.Color":
										field.SetValue(component, volumeEffectField.valueColor);
										break;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004A0C File Offset: 0x00002C0C
		public void BlendValues(AmplifyColorBase targetColor, VolumeEffect other, float blendAmount)
		{
			VolumeEffectFlags effectFlags = targetColor.EffectFlags;
			GameObject gameObject = targetColor.gameObject;
			VolumeEffectComponentFlags compFlags;
			foreach (VolumeEffectComponentFlags compFlags2 in effectFlags.components)
			{
				compFlags = compFlags2;
				if (compFlags.blendFlag)
				{
					Component component = gameObject.GetComponent(compFlags.componentName);
					VolumeEffectComponent volumeEffectComponent = this.components.Find((VolumeEffectComponent s) => s.componentName == compFlags.componentName);
					VolumeEffectComponent volumeEffectComponent2 = other.components.Find((VolumeEffectComponent s) => s.componentName == compFlags.componentName);
					if (!(component == null) && volumeEffectComponent != null && volumeEffectComponent2 != null)
					{
						VolumeEffectFieldFlags fieldFlags;
						foreach (VolumeEffectFieldFlags fieldFlags2 in compFlags.componentFields)
						{
							fieldFlags = fieldFlags2;
							if (fieldFlags.blendFlag)
							{
								FieldInfo field = component.GetType().GetField(fieldFlags.fieldName);
								VolumeEffectField volumeEffectField = volumeEffectComponent.fields.Find((VolumeEffectField s) => s.fieldName == fieldFlags.fieldName);
								VolumeEffectField volumeEffectField2 = volumeEffectComponent2.fields.Find((VolumeEffectField s) => s.fieldName == fieldFlags.fieldName);
								if (field != null && volumeEffectField != null && volumeEffectField2 != null)
								{
									string fullName = field.FieldType.FullName;
									switch (fullName)
									{
									case "System.Single":
										field.SetValue(component, Mathf.Lerp(volumeEffectField.valueSingle, volumeEffectField2.valueSingle, blendAmount));
										break;
									case "System.Boolean":
										field.SetValue(component, volumeEffectField2.valueBoolean);
										break;
									case "UnityEngine.Vector2":
										field.SetValue(component, Vector2.Lerp(volumeEffectField.valueVector2, volumeEffectField2.valueVector2, blendAmount));
										break;
									case "UnityEngine.Vector3":
										field.SetValue(component, Vector3.Lerp(volumeEffectField.valueVector3, volumeEffectField2.valueVector3, blendAmount));
										break;
									case "UnityEngine.Vector4":
										field.SetValue(component, Vector4.Lerp(volumeEffectField.valueVector4, volumeEffectField2.valueVector4, blendAmount));
										break;
									case "UnityEngine.Color":
										field.SetValue(component, Color.Lerp(volumeEffectField.valueColor, volumeEffectField2.valueColor, blendAmount));
										break;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004D70 File Offset: 0x00002F70
		public VolumeEffectComponent GetEffectComponent(string compName)
		{
			return this.components.Find((VolumeEffectComponent s) => s.componentName == compName);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004DA4 File Offset: 0x00002FA4
		public static Component[] ListAcceptableComponents(AmplifyColorBase go)
		{
			if (go == null)
			{
				return new Component[0];
			}
			Component[] source = go.GetComponents(typeof(Component));
			return (from comp in source
			where comp != null && (!(comp.GetType() + string.Empty).StartsWith("UnityEngine.") && comp.GetType() != typeof(AmplifyColorBase))
			select comp).ToArray<Component>();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004E00 File Offset: 0x00003000
		public string[] GetComponentNames()
		{
			return (from r in this.components
			select r.componentName).ToArray<string>();
		}

		// Token: 0x04000069 RID: 105
		public AmplifyColorBase gameObject;

		// Token: 0x0400006A RID: 106
		public List<VolumeEffectComponent> components;
	}
}
