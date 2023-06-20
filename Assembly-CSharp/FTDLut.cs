using System;
using UnityEngine;

// Token: 0x02000173 RID: 371
public class FTDLut
{
	// Token: 0x06000857 RID: 2135 RVA: 0x00037380 File Offset: 0x00035580
	public bool Validate2DLut(Texture2D texture)
	{
		if (!texture)
		{
			return false;
		}
		int height = texture.height;
		return height == Mathf.FloorToInt(Mathf.Sqrt((float)texture.width));
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x000373BC File Offset: 0x000355BC
	public Texture3D Allocate3DLut(Texture2D source)
	{
		int height = source.height;
		return new Texture3D(height, height, height, TextureFormat.ARGB32, false);
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x000373DC File Offset: 0x000355DC
	public void SetNeutralLUT(Texture2D destination)
	{
		int height = destination.height;
		Color[] array = new Color[height * height * height];
		float num = 1f / (1f * (float)height - 1f);
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < height; j++)
			{
				for (int k = 0; k < height; k++)
				{
					int num2 = height - j - 1;
					array[k * height + i + num2 * height * height] = new Color((float)i * 1f * num, (float)j * 1f * num, (float)k * 1f * num, 1f);
				}
			}
		}
		destination.SetPixels(array);
		destination.Apply();
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x000374A8 File Offset: 0x000356A8
	public bool ConvertLUT(Texture2D source, Texture3D destination)
	{
		int height = source.height;
		if (!this.Validate2DLut(source))
		{
			Debug.LogWarning("The given 2D texture " + source.name + " cannot be used as a 3D LUT.");
			return false;
		}
		Color[] pixels = source.GetPixels();
		Color[] array = new Color[pixels.Length];
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < height; j++)
			{
				for (int k = 0; k < height; k++)
				{
					int num = height - j - 1;
					array[i + j * height + k * height * height] = pixels[k * height + i + num * height * height];
				}
			}
		}
		destination.SetPixels(array);
		destination.Apply();
		return true;
	}
}
