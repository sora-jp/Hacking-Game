using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ASTest : MonoBehaviour
{
    void Update()
    {
        float[] a = new float[256];

        GetComponent<AudioSource>().GetSpectrumData(a, 0, FFTWindow.BlackmanHarris);

        for (int i = 15; i < a.Length - 1; i+=8)
        {
            float sample = 0;
            for (int j = 0; j < 8; j++)
            {
                sample += a[i - j];
            }
            sample /= 8;
            Debug.DrawLine(new Vector3(i / 8, -sample * 250, 0) + transform.position, new Vector3(i / 8, sample * 200, 0) + transform.position, Color.red);
        }
    }
}