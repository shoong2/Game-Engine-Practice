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


    bool look = true;
    void Start()
    {
        randomRate = Random.Range(0f, 2f);
        Debug.Log(randomRate);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) <= distance)
        {
            transform.LookAt(player.transform.position);
            if (look)
            {
                StartCoroutine(Shot());
                look = false;
            }
        }
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(bulletRate);
        GameObject instantBullet = Instantiate(bullet, gunPos.transform.position, gunPos.transform.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = gunPos.transform.forward * bulletSpeed;
        StartCoroutine(Shot());
        yield return new WaitForSeconds(bulletTime);
        Destroy(instantBullet.gameObject);
    }
}
