using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Object : NetworkBehaviour
{
    void OnCollisionEnter(Collision other) 
    {
        GameObject golpe = other.gameObject;
        Vidas vidaActual = golpe.GetComponent<Vidas>();
        if (vidaActual != null)
        {
            vidaActual.Aumentar(5);
            Destroy(gameObject);
        }
    }
}
