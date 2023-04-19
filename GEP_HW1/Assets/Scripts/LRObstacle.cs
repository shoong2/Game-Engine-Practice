using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRObstacle : MonoBehaviour
{
    public float speed = 2f; // ���� �̵� �ӵ�
    public float distance = 3f; // ���� �̵� �Ÿ�

    private float initialPosition;
    private float direction = 1f;
    GameObject playerScale;

    bool isGround = false;
    void Start()
    {
        initialPosition = transform.position.x; // ������ �ʱ� ��ġ�� �����մϴ�.
        playerScale = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        // ������ �¿�� �̵��մϴ�.
        float newPosition = initialPosition + direction * distance * Mathf.PingPong(Time.time * speed, 1f);
        float playerNewPos = playerScale.transform.position.x +direction*distance * Mathf.PingPong(Time.time * speed, 1f);

        transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);

        if(isGround)
        {
            playerScale.transform.position = new Vector3(speed, playerScale.transform.position.y, playerScale.transform.position.z);
        }

    }


    

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) // �浹�� ������Ʈ�� �÷��̾��� ���
        {
            isGround = true;
           // other.transform.SetParent(transform);
        }

        //Vector3 parentScale = transform.lossyScale;
       // Vector3 newScale = new Vector3(playerScale.x / parentScale.x, playerScale.y / parentScale.y, playerScale.z / parentScale.z);
       // other.transform.localScale = newScale;
    }


    void OnCollisionExit(Collision other)
    {
        //if (other.gameObject.CompareTag("Player")) // �浹�� ������Ʈ�� �÷��̾��� ���
        //{
        //    other.gameObject.transform.parent = null; // �÷��̾��� �θ� ������Ʈ�� �����մϴ�.
        //    other.transform.localScale = new Vector3(1f, 1, 1f);
        //}
        isGround = false;
    }
}
