using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ASTest : MonoBehaviour
{
    public float strength;
    public float radius;

    void Update()
    {
        float[] samples = new float[512 / 8];
        GetAudioSample(samples);

        for (int i = 0; i < samples.Length; i++)
        {
            Vector2 circle = new Vector2(Mathf.Sin(360 * Mathf.Deg2Rad * ((float)(i-1) / samples.Length)), Mathf.Cos(360 * Mathf.Deg2Rad * ((float)(i-1) / samples.Length)));
            Vector2 circle2 = new Vector2(Mathf.Sin(360 * Mathf.Deg2Rad * ((float)(i) / samples.Length)), Mathf.Cos(360 * Mathf.Deg2Rad * ((float)(i) / samples.Length)));

            float sample = samples[i];
            //Debug.Log(i + " : " + sample);
            Debug.DrawLine(circle * radius + (circle * sample * strength) + (Vector2)transform.position, circle2 * radius + (circle2 * samples[(i+1)%samples.Length] * strength) + (Vector2)transform.position, Color.red);
        }
    }

    void GetAudioSample(float[] o)
    {
        float[] a = new float[512];

        GetComponent<AudioSource>().GetSpectrumData(a, 0, FFTWindow.Rectangular);

        for (int i = 9; i < a.Length - 1; i += 8)
        {
            float sample = 0;
            for (int j = 0; j < 8; j++)
            {
                sample += a[i - j];
            }
            sample /= 8;
            o[i/8] = sample;
        }
    }
}