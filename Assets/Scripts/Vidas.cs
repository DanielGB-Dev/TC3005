using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Vidas : NetworkBehaviour
{

    public const int vidaTotal = 100;
    public NetworkVariable<int> vidaActual = new NetworkVariable<int>(vidaTotal);
    public RectTransform barraVida;

    public void Damage(int cantidad)
    {
        if (!IsServer)
        {
            return;
        }

        vidaActual.Value -= cantidad;
        if (vidaActual.Value <= 0)
        {
            vidaActual.Value = vidaTotal;
            RespawnClientRpc();   
        }
    }

    public void Aumentar(int cantidad)
    {
        if (!IsServer)
        {
            return;
        }

        vidaActual.Value += cantidad;
        if (vidaActual.Value >= 100)
        {
            vidaActual.Value = vidaTotal;
        }
    }

    void CambioVida(int vida)
    {
        barraVida.sizeDelta = new Vector2(vida, barraVida.sizeDelta.y);
    }

    [ClientRpc]
    void RespawnClientRpc()
    {
        if (IsLocalPlayer)
        {
            transform.position = Vector3.zero;
        }
    }
}
