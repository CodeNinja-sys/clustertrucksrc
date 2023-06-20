using System;

[Serializable]
public struct SerializableQuaternion
{
	public SerializableQuaternion(float rX, float rY, float rZ, float rW) : this()
	{
	}

	public float x;
	public float y;
	public float z;
	public float w;
}
