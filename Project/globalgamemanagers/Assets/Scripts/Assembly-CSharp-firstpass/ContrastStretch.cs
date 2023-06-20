using UnityEngine;

public class ContrastStretch : MonoBehaviour
{
	public float adaptationSpeed;
	public float limitMinimum;
	public float limitMaximum;
	public Shader shaderLum;
	public Shader shaderReduce;
	public Shader shaderAdapt;
	public Shader shaderApply;
}
