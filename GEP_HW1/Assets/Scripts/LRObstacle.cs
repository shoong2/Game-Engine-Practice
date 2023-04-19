using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRObstacle : MonoBehaviour
{
    public float speed = 2f; // 발판 이동 속도
    public float distance = 3f; // 발판 이동 거리

    private float initialPosition;
    private float direction = 1f;
    GameObject playerScale;

    bool isGround = false;
    void Start()
    {
        initialPosition = transform.position.x; // 발판의 초기 위치를 저장합니다.
        playerScale = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        // 발판을 좌우로 이동합니다.
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
        if (other.gameObject.CompareTag("Player")) // 충돌한 오브젝트가 플레이어인 경우
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
        //if (other.gameObject.CompareTag("Player")) // 충돌한 오브젝트가 플레이어인 경우
        //{
        //    other.gameObject.transform.parent = null; // 플레이어의 부모 오브젝트를 해제합니다.
        //    other.transform.localScale = new Vector3(1f, 1, 1f);
        //}
        isGround = false;
    }
}
