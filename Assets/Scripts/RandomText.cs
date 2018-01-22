using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class RandomText : MonoBehaviour 
{
    public Text text;
    public int arrLength;
    
    public int minl;
    public int maxl;
    
    List<string> str;
    
    IEnumerator co() 
    {
        
    }
    
    void Start() 
    {
        str = new List<string>(arrLength);
    }
}
