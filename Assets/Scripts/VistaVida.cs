using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VistaVida : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
