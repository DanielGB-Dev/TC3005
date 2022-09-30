using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    
    public GameObject bulletPrefab;
    public Transform spawnBullet;

    void Start() 
    {
        if (IsLocalPlayer)
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
            Vector3 posicionSpawn = new Vector3(Random.Range(-5.0f, 5.0f), 0.0f, Random.Range(-5.0f, 5.0f));
            Quaternion rotacionSpawn = Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f);

            gameObject.transform.position = posicionSpawn;
            gameObject.transform.rotation = rotacionSpawn;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootServerRpc();
        }
    }

    [ServerRpc]
    void ShootServerRpc()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, spawnBullet.position, spawnBullet.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 5.0f;
        NetworkObject.Spawn(bullet);
        Destroy(bullet, 3);
    }
}
