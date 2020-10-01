void Sample3DTexLOD_float(Texture3D Texture, float3 UV, SamplerState Sampler, float LOD, out float4 Out)
{
	Out = float4(0, 0, 0, 0);

	Out = Texture.SampleLevel(Sampler, UV, LOD);
}