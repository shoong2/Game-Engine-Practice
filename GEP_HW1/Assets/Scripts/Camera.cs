using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    
    public float cameraSpeed = 10f;

    public GameObject target;               // Ä«¸Þ¶ó°¡ µû¶ó´Ù´Ò Å¸°Ù

    //float z;
    //float y;
    //private void Start()
    //{
    //    z = transform.position.z - target.transform.position.z;
    //    y = transform.position.y - target.transform.position.y;
    //}
    //private void FixedUpdate()
    //{
    //    transform.position = new Vector3(0, 
    //        target.transform.position.y +  y, 
    //        target.transform.position.z + z);
    //}

    Vector3 distance;

    private void Start()
    {
        distance = transform.position - target.transform.position;
        //distance = new Vector3(Mathf.Abs(distance.x), Mathf.Abs(distance.y), Mathf.Abs(distance.z));

    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + distance, cameraSpeed);
    }
}
