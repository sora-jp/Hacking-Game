using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class RandomText : MonoBehaviour 
{
    public Text text;
    public int arrLength;
    public float timeBetweenShit;
    
    public int minl;
    public int maxl;
    
    Queue<string> str;
    
    IEnumerator co() 
    {
        while(true) 
        {
            string s = GetRandomAlphaNumeric(Random.Range(minl, maxl));
            str.Enqueue(0, s);
            
            if (str.Count >= arrLength) str.Dequeue();
            
            text.text = string.Join("\n", str.ToArray());
            
            yield return new WaitForSecondsRealtime(timeBetweenShit);
        }
    }
    
    public static string GetRandomAlphaNumeric(int length)
    {
        var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(chars.Select(c => chars[Random.Range(0, chars.Length)]).Take(length).ToArray());
    }
    
    void Start() 
    {
        str = new Queue<string>(arrLength);
        StartCoroutine(co());
    }
}
