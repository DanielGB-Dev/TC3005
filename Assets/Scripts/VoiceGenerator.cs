using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
  Grump, Normal, Cute, Crazy,
}

public class VoiceGenerator : MonoBehaviour
{
    public Character characterType;
    [TextArea(2,4)]
    public string dialogue;
    [Space]
    public AudioSource source;
    
    private float pitch;
    
    // Start is called before the first frame update
    void Start()
    {
        switch (characterType)
        {
            case Character.Grump:
                pitch = 0.85f;
                break;
            case Character.Normal:
                pitch = 1;
                break;
            case Character.Cute:
                pitch = 1.5f;
                break;
            case Character.Crazy:
                pitch = 2;
                break;
            default:
                pitch = 1;
                break;
        }
        StartCoroutine(ReproduceSound(dialogue));
    }

    IEnumerator ReproduceSound(string dialogue)
    {
        foreach (char c in dialogue)
        {
            if (char.IsLetter(c))
            {
                string audioClip = "Audio/Animalese_" + char.ToUpper(c);
                source.PlayOneShot(Resources.Load<AudioClip>(audioClip));
                source.pitch = pitch;
                yield return new WaitForSeconds(0.085f);
            }
            else
            {
                yield return new WaitForSeconds(0.085f);
            }
        }
    }

    
}


