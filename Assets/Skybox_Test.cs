using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Skybox_Test : MonoBehaviour
{
    public GameObject skychanger;
    private void OnTriggerEnter(Collider other)
    {
        Skybox_Swap ss = skychanger.GetComponent<Skybox_Swap>();

        ss.OnWeatherChange();
    }
}
