using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// Token: 0x02000204 RID: 516
public class ParameterJSONConverter : JsonConverter
{
	// Token: 0x06000C3D RID: 3133 RVA: 0x0004C820 File Offset: 0x0004AA20
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(getModifierOptions.Parameter);
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x0004C830 File Offset: 0x0004AA30
	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		JObject jobject = JObject.Load(reader);
		if (jobject.SelectToken("JSONType").ToString() == "A")
		{
			return jobject.ToObject<getModifierOptions.SliderParameter>(serializer);
		}
		if (jobject["JSONType"].ToString().Equals("B"))
		{
			return jobject.ToObject<getModifierOptions.BoolParameter>(serializer);
		}
		if (jobject["JSONType"].ToString().Equals("C"))
		{
			return jobject.ToObject<getModifierOptions.DropdownParameter>(serializer);
		}
		if (jobject["JSONType"].ToString().Equals("D"))
		{
			return jobject.ToObject<getModifierOptions.ColorParameter>(serializer);
		}
		if (jobject["JSONType"].ToString().Equals("E"))
		{
			return jobject.ToObject<getModifierOptions.InputFieldParameter>(serializer);
		}
		throw new Exception("Invalid Json TypE: " + jobject["JSONType"].ToString());
	}

	// Token: 0x1700025D RID: 605
	// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0004C92C File Offset: 0x0004AB2C
	public override bool CanWrite
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000C40 RID: 3136 RVA: 0x0004C930 File Offset: 0x0004AB30
	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		throw new NotImplementedException();
	}
}
