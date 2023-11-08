using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnim;
    public AudioClip door_Open_Noise;
    public AudioClip door_Close_Noise;
    public AudioSource doorNoise;
    public bool open, close;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(open)
            {
                doorAnim.Play("DoorMove", 0, 0.0f);
                doorNoise.clip = door_Open_Noise;
                doorNoise.Play();
                doorAnim.Play("Idle", 0, 0.0f);

                doorAnim.Play("DoorMove_Close", 0, 0.0f);
                doorNoise.clip = door_Close_Noise;
                doorNoise.Play();

            }

            if (close)
            {
                doorAnim.Play("DoorMove_Close", 0, 0.0f);
                doorNoise.clip = door_Close_Noise;
                doorNoise.Play();
                doorAnim.Play("Idle", 0, 0.0f);

                doorAnim.Play("DoorMove", 0, 0.0f);
                doorNoise.clip = door_Open_Noise;
                doorNoise.Play();


            }
        }
    }


}
