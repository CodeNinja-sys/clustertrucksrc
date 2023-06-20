using System;
using UnityEngine;

[Serializable]
public class FilmicCurve
{
	[SerializeField]
	public float m_BlackPoint;
	[SerializeField]
	public float m_WhitePoint;
	[SerializeField]
	public float m_CrossOverPoint;
	[SerializeField]
	public float m_ToeStrength;
	[SerializeField]
	public float m_ShoulderStrength;
	[SerializeField]
	public float m_LuminositySaturationPoint;
	public bool m_UseLegacyCurve;
	public Vector4 m_ToeCoef;
	public Vector4 m_ShoulderCoef;
	public float m_k;
}
