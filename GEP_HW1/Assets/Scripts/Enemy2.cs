using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject gunPos;

    [SerializeField]
    GameObject bullet;

    public float distance=20;
    public float bulletSpeed = 30f;
    public float bulletTime = 1f;
    public float bulletRate;
    float randomRate;

    Player targetScript;

    [SerializeField]
    GameObject gunItem;
    [SerializeField]
    GameObject bulletItem;

    bool attack = false;
    bool look = true;
    bool isDistacne = false;

    void Start()
    {
        targetScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= distance)
        {
            isDistacne = true;
            transform.LookAt(player.transform.position);
            if (look)
            {
                StartCoroutine(Shot());
                look = false;
            }
        }
        else
        {
            isDistacne = false;
            look = true;
        }
           

        if (Input.GetMouseButtonDown(0))
        {
            attack = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            attack = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet" || (attack && other.tag == "Sword"))
        {
            Vector3 w = gameObject.transform.position;

            if (targetScript.IsHaveGun())
            {
                StartCoroutine(test(w, "Bullet"));
                //Instantiate(bulletItem, gameObject.transform.position, Quaternion.identity);
            }
            else
            {
                StartCoroutine(test(w, "Gun"));
                //Instantiate(gunItem, gameObject.transform.position, Quaternion.identity);
            }
            //Destroy(gameObject);
        }



    }

    IEnumerator test(Vector3 a, string b)
    {
        //yield return new WaitForSeconds(1f);
        if (b == "Gun")
            Instantiate(gunItem, a - Vector3.back, Quaternion.Euler(0, -61, 0));
        else
            Instantiate(bulletItem, a + Vector3.down, Quaternion.identity);

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(bulletRate);
        GameObject instantBullet = Instantiate(bullet, gunPos.transform.position, gunPos.transform.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = gunPos.transform.forward * bulletSpeed;
        if(isDistacne)
            StartCoroutine(Shot());
        yield return new WaitForSeconds(bulletTime);
        Destroy(instantBullet.gameObject);
    }
}
