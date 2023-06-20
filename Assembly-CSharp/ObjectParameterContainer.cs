using System;
using Newtonsoft.Json;

// Token: 0x02000251 RID: 593
public class ObjectParameterContainer
{
	// Token: 0x06000E7C RID: 3708 RVA: 0x0005E0B0 File Offset: 0x0005C2B0
	[JsonConstructor]
	public ObjectParameterContainer(string _name, string _value)
	{
		this.Name = _name;
		this.Value = _value;
	}

	// Token: 0x06000E7D RID: 3709 RVA: 0x0005E0C8 File Offset: 0x0005C2C8
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	// Token: 0x06000E7E RID: 3710 RVA: 0x0005E0D0 File Offset: 0x0005C2D0
	public override string ToString()
	{
		return base.ToString();
	}

	// Token: 0x06000E7F RID: 3711 RVA: 0x0005E0D8 File Offset: 0x0005C2D8
	public override bool Equals(object obj)
	{
		ObjectParameterContainer objectParameterContainer = obj as ObjectParameterContainer;
		return objectParameterContainer.Name == this.Name && objectParameterContainer.Value == this.Value;
	}

	// Token: 0x06000E80 RID: 3712 RVA: 0x0005E118 File Offset: 0x0005C318
	public string getName()
	{
		return this.Name ?? "UNDEFINED";
	}

	// Token: 0x06000E81 RID: 3713 RVA: 0x0005E12C File Offset: 0x0005C32C
	public string getValue()
	{
		return this.Value ?? "UNDEFINED";
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x0005E140 File Offset: 0x0005C340
	public void setValue(string val)
	{
		this.Value = val;
	}

	// Token: 0x04000B11 RID: 2833
	public string Name;

	// Token: 0x04000B12 RID: 2834
	public string Value;
}
