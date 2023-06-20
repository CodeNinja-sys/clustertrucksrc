using System;
using UnityEngine;

// Token: 0x020002D5 RID: 725
public class SENBDLMainCube : MonoBehaviour
{
	// Token: 0x06001113 RID: 4371 RVA: 0x0006ECEC File Offset: 0x0006CEEC
	private void Start()
	{
		this.glowColors[0] = new Color(1f, 0.47058824f, 0.050980393f);
		this.glowColors[2] = new Color(0.32941177f, 0.6392157f, 1f);
		this.glowColors[1] = new Color(0.60784316f, 1f, 0.11764706f);
		this.glowColors[3] = new Color(1f, 0.18431373f, 0f);
		this.currentColor = this.glowColors[0];
		SENBDLGlobal.sphereOfCubesRotation = Quaternion.identity;
		for (int i = 0; i < 150; i++)
		{
			UnityEngine.Object.Instantiate(this.orbitingCube, Vector3.zero, Quaternion.identity);
		}
		for (int j = 0; j < 19; j++)
		{
			UnityEngine.Object.Instantiate(this.glowingOrbitingCube, Vector3.zero, Quaternion.identity);
		}
		Camera.main.backgroundColor = new Color(0.08f, 0.08f, 0.08f);
		SENBDLGlobal.mainCube = this;
		this.bloomShader = Camera.main.GetComponent<SENaturalBloomAndDirtyLens>();
	}

	// Token: 0x06001114 RID: 4372 RVA: 0x0006EE38 File Offset: 0x0006D038
	private void OnGUI()
	{
	}

	// Token: 0x06001115 RID: 4373 RVA: 0x0006EE3C File Offset: 0x0006D03C
	private void Update()
	{
		this.deltaTime = Time.deltaTime / Time.timeScale;
		this.AnimateColor();
		this.RotateSphereOfCubes();
		float d = 40f;
		Vector3 vector = Vector3.up * d;
		vector = Quaternion.Euler(Vector3.right * Time.time * d * 0.5f) * vector;
		base.transform.Rotate(vector * Time.deltaTime);
		this.IncrementCounters();
		this.GetInput();
		this.UpdateShaderValues();
		this.SmoothFPSCounter();
	}

	// Token: 0x06001116 RID: 4374 RVA: 0x0006EED4 File Offset: 0x0006D0D4
	private void AnimateColor()
	{
		if (this.newColorCounter >= 8f)
		{
			this.newColorCounter = 0f;
			this.currentColorIndex = (this.currentColorIndex + 1) % this.glowColors.Length;
			this.previousColor = this.currentColor;
			this.currentColor = this.glowColors[this.currentColorIndex];
		}
		float t = Mathf.Clamp01(this.newColorCounter / 8f * 5f);
		this.glowColor = Color.Lerp(this.previousColor, this.currentColor, t);
		Color color = this.glowColor * Mathf.Pow(Mathf.Sin(Time.time) * 0.48f + 0.52f, 4f);
		this.cubeEmissivePart.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
		base.GetComponent<Light>().color = color;
		Color color2 = Color.Lerp(new Color
		{
			r = 1f - this.glowColor.r,
			g = 1f - this.glowColor.g,
			b = 1f - this.glowColor.b
		}, Color.white, 0.1f);
		this.particles.GetComponent<Renderer>().material.SetColor("_TintColor", color2);
	}

	// Token: 0x06001117 RID: 4375 RVA: 0x0006F03C File Offset: 0x0006D23C
	private void RotateSphereOfCubes()
	{
		SENBDLGlobal.sphereOfCubesRotation = Quaternion.Euler(Vector3.up * Time.time * 20f);
	}

	// Token: 0x06001118 RID: 4376 RVA: 0x0006F064 File Offset: 0x0006D264
	private void IncrementCounters()
	{
		this.newColorCounter += Time.deltaTime;
	}

	// Token: 0x06001119 RID: 4377 RVA: 0x0006F078 File Offset: 0x0006D278
	private void GetInput()
	{
		if (Input.GetKey(KeyCode.RightArrow))
		{
			this.bloomAmount += 0.2f * this.deltaTime;
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			this.bloomAmount -= 0.2f * this.deltaTime;
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			this.lensDirtAmount += 0.4f * this.deltaTime;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			this.lensDirtAmount -= 0.4f * this.deltaTime;
		}
		if (Input.GetKey(KeyCode.Period))
		{
			Time.timeScale += 0.5f * this.deltaTime;
		}
		if (Input.GetKey(KeyCode.Comma))
		{
			Time.timeScale -= 0.5f * this.deltaTime;
		}
		this.bloomAmount = Mathf.Clamp(this.bloomAmount, 0f, 0.4f);
		this.lensDirtAmount = Mathf.Clamp(this.lensDirtAmount, 0f, 0.95f);
		Time.timeScale = Mathf.Clamp(Time.timeScale, 0.1f, 1f);
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.bloomAmount = 0.05f;
			this.lensDirtAmount = 0.1f;
			Time.timeScale = 1f;
		}
	}

	// Token: 0x0600111A RID: 4378 RVA: 0x0006F1E8 File Offset: 0x0006D3E8
	private void UpdateShaderValues()
	{
		this.bloomShader.bloomIntensity = this.bloomAmount;
		this.bloomShader.lensDirtIntensity = this.lensDirtAmount;
	}

	// Token: 0x0600111B RID: 4379 RVA: 0x0006F218 File Offset: 0x0006D418
	private void SmoothFPSCounter()
	{
		this.fps = Mathf.Lerp(this.fps, 1f / this.deltaTime, 5f * this.deltaTime);
	}

	// Token: 0x04000E2C RID: 3628
	private const float newColorFrequency = 8f;

	// Token: 0x04000E2D RID: 3629
	private Color[] glowColors = new Color[4];

	// Token: 0x04000E2E RID: 3630
	public GameObject orbitingCube;

	// Token: 0x04000E2F RID: 3631
	public GameObject glowingOrbitingCube;

	// Token: 0x04000E30 RID: 3632
	public GameObject cubeEmissivePart;

	// Token: 0x04000E31 RID: 3633
	public GameObject particles;

	// Token: 0x04000E32 RID: 3634
	private float newColorCounter;

	// Token: 0x04000E33 RID: 3635
	private Color currentColor;

	// Token: 0x04000E34 RID: 3636
	private Color previousColor;

	// Token: 0x04000E35 RID: 3637
	[HideInInspector]
	public Color glowColor;

	// Token: 0x04000E36 RID: 3638
	private int currentColorIndex;

	// Token: 0x04000E37 RID: 3639
	private float bloomAmount = 0.04f;

	// Token: 0x04000E38 RID: 3640
	private float lensDirtAmount = 0.3f;

	// Token: 0x04000E39 RID: 3641
	private float fps;

	// Token: 0x04000E3A RID: 3642
	private float deltaTime;

	// Token: 0x04000E3B RID: 3643
	private SENaturalBloomAndDirtyLens bloomShader;
}
