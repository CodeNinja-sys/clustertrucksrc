using System;
using UnityEngine;

// Token: 0x02000174 RID: 372
[Serializable]
public class FilmicCurve
{
	// Token: 0x0600085C RID: 2140 RVA: 0x000375A4 File Offset: 0x000357A4
	public float ComputeK(float t, float c, float b, float s, float w)
	{
		float num = (1f - t) * (c - b);
		float num2 = (1f - s) * (w - c) + (1f - t) * (c - b);
		return num / num2;
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x000375DC File Offset: 0x000357DC
	public float Toe(float x, float t, float c, float b, float s, float w, float k)
	{
		float num = this.m_ToeCoef.x * x;
		float num2 = this.m_ToeCoef.y * x;
		return (num + this.m_ToeCoef.z) / (num2 + this.m_ToeCoef.w);
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x00037620 File Offset: 0x00035820
	public float Shoulder(float x, float t, float c, float b, float s, float w, float k)
	{
		float num = this.m_ShoulderCoef.x * x;
		float num2 = this.m_ShoulderCoef.y * x;
		return (num + this.m_ShoulderCoef.z) / (num2 + this.m_ShoulderCoef.w) + k;
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x00037668 File Offset: 0x00035868
	public float Graph(float x, float t, float c, float b, float s, float w, float k)
	{
		if (x <= this.m_CrossOverPoint)
		{
			return this.Toe(x, t, c, b, s, w, k);
		}
		return this.Shoulder(x, t, c, b, s, w, k);
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x000376A4 File Offset: 0x000358A4
	public void StoreK()
	{
		this.m_k = this.ComputeK(this.m_ToeStrength, this.m_CrossOverPoint, this.m_BlackPoint, this.m_ShoulderStrength, this.m_WhitePoint);
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x000376DC File Offset: 0x000358DC
	public void ComputeShaderCoefficientsLegacy(float t, float c, float b, float s, float w, float k)
	{
		float x = k * (1f - t);
		float z = k * (1f - t) * -b;
		float y = -t;
		float w2 = c - (1f - t) * b;
		this.m_ToeCoef = new Vector4(x, y, z, w2);
		float x2 = 1f - k;
		float z2 = (1f - k) * -c;
		float w3 = (1f - s) * w - c;
		this.m_ShoulderCoef = new Vector4(x2, s, z2, w3);
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x00037760 File Offset: 0x00035960
	private float ToRange(float x)
	{
		return (x + 1f) * 0.5f;
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x00037770 File Offset: 0x00035970
	public float GraphHD(float _lOll01ll00)
	{
		float num = Mathf.Log10(_lOll01ll00);
		float x = this.m_ToeCoef.x;
		float w = this.m_ToeCoef.w;
		float x2 = this.m_ShoulderCoef.x;
		float num2 = this.m_ToeCoef.y / (1f + Mathf.Exp(this.m_ToeCoef.z * (num - x)));
		float num3 = 1f - this.m_ShoulderCoef.y / (1f + Mathf.Exp(this.m_ShoulderCoef.z * (num - x2)));
		float num4 = this.m_ShoulderCoef.w * (num + w);
		float t = Mathf.Clamp01((num - x) / (x2 - x));
		float num5 = (num >= x) ? num4 : num2;
		float num6 = (num <= x2) ? num4 : num3;
		float result;
		if (x > x2)
		{
			result = Mathf.Lerp(num6, num5, t);
		}
		else
		{
			result = Mathf.Lerp(num5, num6, t);
		}
		return result;
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x00037870 File Offset: 0x00035A70
	public void ComputeShaderCoefficientsHD(float _001l0l01Ol, float _ll00Ol1O0O, float _ll00l0O1O1, float _OlOO100ll0, float _OOlO1l0ll0, float _OOO1O00O0l)
	{
		float num = (1f - this.ToRange(_001l0l01Ol)) / 1.25f;
		float num2 = this.ToRange(_OlOO100ll0);
		float num3 = Mathf.Clamp01(_ll00Ol1O0O * 0.5f + 0.5f);
		float num4 = 1f - num;
		float num5 = 1f - num2;
		float num6 = 0.18f;
		float num7 = -0.5f * Mathf.Log((1f + (num6 / num4 - 1f)) / (1f - (num6 / num4 - 1f))) * (num4 / num3) + Mathf.Log10(num6);
		float num8 = num4 / num3 - num7;
		float x = num2 / num3 - num8;
		float y = 2f * num4;
		float z = -2f * num3 / num4;
		float y2 = 2f * num5;
		float z2 = 2f * num3 / num5;
		this.m_ToeCoef = new Vector4(num7, y, z, num8);
		this.m_ShoulderCoef = new Vector4(x, y2, z2, num3);
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x00037960 File Offset: 0x00035B60
	public void UpdateCoefficients()
	{
		if (this.m_UseLegacyCurve)
		{
			this.StoreK();
			this.ComputeShaderCoefficientsLegacy(this.m_ToeStrength, this.m_CrossOverPoint, this.m_BlackPoint, this.m_ShoulderStrength, this.m_WhitePoint, this.m_k);
			return;
		}
		this.ComputeShaderCoefficientsHD(this.m_ToeStrength, this.m_CrossOverPoint, this.m_BlackPoint, this.m_ShoulderStrength, this.m_WhitePoint, this.m_k);
	}

	// Token: 0x040006A5 RID: 1701
	[SerializeField]
	public float m_BlackPoint;

	// Token: 0x040006A6 RID: 1702
	[SerializeField]
	public float m_WhitePoint = 1f;

	// Token: 0x040006A7 RID: 1703
	[SerializeField]
	public float m_CrossOverPoint = 0.5f;

	// Token: 0x040006A8 RID: 1704
	[SerializeField]
	public float m_ToeStrength;

	// Token: 0x040006A9 RID: 1705
	[SerializeField]
	public float m_ShoulderStrength;

	// Token: 0x040006AA RID: 1706
	[SerializeField]
	public float m_LuminositySaturationPoint = 0.95f;

	// Token: 0x040006AB RID: 1707
	public bool m_UseLegacyCurve;

	// Token: 0x040006AC RID: 1708
	public Vector4 m_ToeCoef;

	// Token: 0x040006AD RID: 1709
	public Vector4 m_ShoulderCoef;

	// Token: 0x040006AE RID: 1710
	public float m_k;
}
