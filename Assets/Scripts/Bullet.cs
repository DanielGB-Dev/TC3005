using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision other) 
    {
        GameObject golpe = other.gameObject;
        Vidas vidaActual = golpe.GetComponent<Vidas>();
        if (vidaActual != null)
        {
            vidaActual.Damage(5);
        }
        
        Destroy(gameObject);
    }
}
