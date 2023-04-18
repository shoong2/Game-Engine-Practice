using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;

    GameObject target;
    Player targetScript;

    [SerializeField]
    GameObject gunItem;
    [SerializeField]
    GameObject bulletItem;

    bool attack = false;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        targetScript = target.GetComponent<Player>();
    }
        

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        agent.SetDestination(target.transform.position);
        if(Input.GetMouseButtonDown(0))
        {
            attack = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            attack = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Bullet" || (attack && other.tag == "Sword"))
        {
            Vector3 w = gameObject.transform.position;
            
            if(targetScript.IsHaveGun())
            {
                StartCoroutine(test(w,"Bullet"));
                //Instantiate(bulletItem, gameObject.transform.position, Quaternion.identity);
            }
            else
            {
                StartCoroutine(test(w,"Gun"));
                //Instantiate(gunItem, gameObject.transform.position, Quaternion.identity);
            }
            //Destroy(gameObject);
        }

        
        
    }

    IEnumerator test(Vector3 a, string b)
    {
        //yield return new WaitForSeconds(1f);
        if(b=="Gun")
            Instantiate(gunItem, a - Vector3.back, Quaternion.Euler(0,-61,0));
        else
            Instantiate(bulletItem, a +Vector3.down, Quaternion.identity);

        Destroy(gameObject);
        yield return null;
    }
}
