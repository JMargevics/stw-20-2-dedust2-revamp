using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class FlowSettings : MonoBehaviour
{
    MaterialPropertyBlock mpb;

    public Vector2 multiplierInOut = new Vector2(1.0f, 0f);
    public Vector4 fadeDistances = new Vector4(.25f, .25f, .25f, .25f);
    public Vector4 fadeWeights = new Vector4(1, 1, 1, 1);
    [Range(0f, 360f)]
    public float rotation = 0f;

    public bool forcedValue = false;
    public Vector3 value = Vector3.zero;

    [Range(0, 1)]
    public float globalMultiplier = 1f;
    [Range(0, 1)]
    public float velocityMultiplier = 1f;

    private void OnValidate()
    {
        if (mpb == null) mpb = new MaterialPropertyBlock();

        multiplierInOut.x = Mathf.Clamp01(multiplierInOut.x);
        multiplierInOut.y = Mathf.Clamp01(multiplierInOut.y);

        fadeDistances.x = Mathf.Clamp01(fadeDistances.x);
        fadeDistances.y = Mathf.Clamp01(fadeDistances.y);
        fadeDistances.z = Mathf.Clamp01(fadeDistances.z);
        fadeDistances.w = Mathf.Clamp01(fadeDistances.w);

        fadeWeights.x = Mathf.Clamp01(fadeWeights.x);
        fadeWeights.y = Mathf.Clamp01(fadeWeights.y);
        fadeWeights.z = Mathf.Clamp01(fadeWeights.z);
        fadeWeights.w = Mathf.Clamp01(fadeWeights.w);

        mpb.SetFloat("_In", multiplierInOut.x);
        mpb.SetFloat("_Out", multiplierInOut.y);
        mpb.SetVector("_FadeDistances", fadeDistances);
        mpb.SetVector("_FadeWeights", fadeWeights);
        mpb.SetFloat("_Rotation", rotation);
        mpb.SetFloat("_ForcedValue", forcedValue?1:0);
        mpb.SetVector("_Value", value);
        mpb.SetFloat("_GlobalMultiplier", globalMultiplier);
        mpb.SetFloat("_VelocityMultiplier", velocityMultiplier);

        GetComponent<Renderer>().SetPropertyBlock(mpb);
    }
}
