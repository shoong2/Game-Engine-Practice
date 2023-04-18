using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 3f;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }
    void Update()
    {
        transform.Translate(transform.forward * bulletSpeed);
    }

 
}
