using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    
    void Start() 
    {
        if (IsLocalPlayer)
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!IsLocalPlayer)
        {
            return;
        }
        float r = Input.GetAxis("Horizontal") * Time.deltaTime * 50.0f;
        float t = Input.GetAxis("Vertical") * Time.deltaTime * 2.0f;
        transform.Rotate(0, r, 0);
        transform.Translate(0, 0 ,t);


    }
}
