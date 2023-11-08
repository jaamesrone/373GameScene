using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox_Swap : MonoBehaviour
{
    public Material skybox_default;
    public Material skybox_overcast;

    private void Awake()
    {
        RenderSettings.skybox = skybox_default;
        RenderSettings.ambientIntensity = 1.0f;
    }

    public void OnWeatherChange()
    {
        RenderSettings.skybox = skybox_overcast;
        RenderSettings.ambientIntensity = .05f;
    }
}
