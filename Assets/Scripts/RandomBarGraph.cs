using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBarGraph : MonoBehaviour {
    public float minTimeBetween;
    public float maxTimeBetween;

    public Image[] images;
    float[] imageStuff;
    float[] newImageStuff;

    float[] ts;
    float[] t;

    private void Start()
    {
        imageStuff = new float[images.Length];
        newImageStuff = new float[images.Length];
        t = new float[images.Length];
        ts = new float[images.Length];
        for (int i = 0; i < images.Length; i++)
        {
            ts[i] = maxTimeBetween;
        }
    }

    private void Update()
    {
        for (int i = 0; i < images.Length; i++)
        {
            t[i] += Time.deltaTime / ts[i];
            images[i].fillAmount = lerp(imageStuff[i], newImageStuff[i], t[i]);

            if (t[i] > 1)
            {
                t[i] = 0;
                ts[i] = Random.Range(minTimeBetween, maxTimeBetween);
                imageStuff[i] = newImageStuff[i];
                newImageStuff[i] = Random.Range(0f, 1f);
            }
        }

        //if (t > 1)
        //{
        //    t = 0;
        //    System.Array.Copy(newImageStuff, imageStuff, imageStuff.Length);
        //    for (int i = 0; i < newImageStuff.Length; i++)
        //    {
        //        newImageStuff[i] = Random.Range(0f, 1f);
        //    }
        //}
    }

    float lerp(float a, float b, float t)
    {
        return a + (b - a) * t;
    }
}
