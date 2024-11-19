using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffect : MonoBehaviour
{
    public float Bump;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Transform>().localScale /= 1.2f;
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, Bump, 0));
    }
}
