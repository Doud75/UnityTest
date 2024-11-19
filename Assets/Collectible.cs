using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public GameObject AreaEffects;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
        collision.gameObject.GetComponent<Transform>().localScale *= 1.2f;

        AreaEffects.GetComponent<Transform>().localScale *= 1.2f;
    }
}
