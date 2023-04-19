using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnParticleCollision(GameObject other)
    {
        if(other.tag =="Player")
        {
            //Debug.Log("check point");
            other.GetComponent<Player>().spawnPos = transform.position +new Vector3(0,0.3f,0);
        }

    }

    
}
