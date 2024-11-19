using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;
    public float Jump;
    public GameObject Laser;
    public GameObject AnimatorChar;

    private int _nbObjectUnderMyFeet = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody>().AddForce(GetComponent<Transform>().right * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody>().AddForce(-GetComponent<Transform>().right * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody>().AddForce(GetComponent<Transform>().forward * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody>().AddForce(-GetComponent<Transform>().forward * Speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<Transform>().Rotate(new Vector3(0, RotationSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<Transform>().Rotate(new Vector3(0, -RotationSpeed * Time.deltaTime, 0));
        }

        if (Input.GetKeyDown(KeyCode.Space) && _nbObjectUnderMyFeet > 0)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, Jump, 0));
        }

        AnimatorChar.GetComponentInChildren<Animator>().SetFloat("Speed", GetComponent<Rigidbody>().velocity.magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            _nbObjectUnderMyFeet++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            _nbObjectUnderMyFeet--;
        }
    }
}
