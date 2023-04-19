using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float speed = 20f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag =="Player")
        {
            collision.transform.GetComponent<Rigidbody>().AddForce(new Vector3(0,speed, 3), ForceMode.Impulse);
        }
    }
}
