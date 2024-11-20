using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject OneLaser;
    public float LaserSpeed;
    public GameObject LaserSpawnPosition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject l = Instantiate(OneLaser);
        l.transform.position = LaserSpawnPosition.transform.position;
        l.transform.rotation = transform.rotation;
        l.GetComponent<Rigidbody>().AddForce(l.transform.forward * LaserSpeed);
    }
}
