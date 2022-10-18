using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
  Grump, Cute, Crazy,
}

public class VoiceGenerator : MonoBehaviour
{
    public Character characterType;
    [TextArea(2,4)]
    public string[] dialogues; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


