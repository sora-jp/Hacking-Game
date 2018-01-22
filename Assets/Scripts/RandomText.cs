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
    
    List<string> str;
    
    IEnumerator co() 
    {
        while(true) 
        {
            string s = GetRandomAlphaNumeric(Random.Range(minl, maxl));
            str.Insert(0, s);
            
            if (str.Count >= arrLength) str.RemoveAt(str.Count-1);
            
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
        str = new List<string>(arrLength);
        StartCoroutine(co());
    }
}
