using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
  Grump, Cute, Crazy,
}

public class VoiceGenerator : MonoBehaviour
{
    //public Character characterType;
    [TextArea(2,4)]
    public string dialogue;
    public AudioClip audio;
    public AudioSource source;
    
    // Start is called before the first frame update
    void Start()
    {
        //ReproduceVoice(dialogue);
        StartCoroutine(ReproduceSound(dialogue));
    }

    IEnumerator ReproduceSound(string dialogue)
    {
        foreach (char c in dialogue)
        {
            //Debug.Log(c);
            if (char.IsLetter(c))
            {
                source.PlayOneShot(audio);
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                yield return new WaitForSeconds(0.15f);
            }
        }
    }

    
}


