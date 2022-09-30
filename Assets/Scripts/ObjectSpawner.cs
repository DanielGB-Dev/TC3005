using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ObjectSpawner : NetworkBehaviour
{
    public GameObject objetoPrefab;
    public int elementos;

    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < elementos; i++)
            {
                Vector3 posicionSpawn = new Vector3(Random.Range(-10.0f, 10.0f), 0.0f, Random.Range(-10.0f, 10.0f));
                Quaternion rotacionSpawn = Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f);

                GameObject objeto = GameObject.Instantiate(objetoPrefab, posicionSpawn, rotacionSpawn);
                objeto.GetComponent<NetworkObject>().Spawn();
            }
    }
}
