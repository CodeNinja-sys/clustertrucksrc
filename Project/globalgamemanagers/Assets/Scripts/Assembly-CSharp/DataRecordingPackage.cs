using System;

[Serializable]
public class DataRecordingPackage
{
	public SerializableVector3 mPosition;
	public SerializableQuaternion mQuaternion;
	public SerializableVector3 mAngularVelocity;
	public SerializableVector3 mVelocity;
	public SerializableVector3 mExtra;
	public int mScene;
	public int mInstanceID;
	public bool mAddon;
	public float mTime;
	public string mName;
	public string mPrefabName;
	public bool mFirstPackage;
}
