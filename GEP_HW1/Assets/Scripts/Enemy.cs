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
        

    void Update()
    {
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
            if(targetScript.IsHaveGun())
            {
                Instantiate(bulletItem, other.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(gunItem, other.transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        //if(Input.GetMouseButtonDown(0) && other.tag =="Sword")
        //{
        //    Destroy(gameObject);
        //}
        
    }
}
