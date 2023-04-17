using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;    // 이동 속도
    public float jumpForce = 5f;        // 점프 힘
    public float groundCheckDistance = 1f; // 지면과의 거리
    public LayerMask groundLayer;        // 지면 레이어
    bool isGrounded =true;                    // 땅에 닿았는지 여부

    Rigidbody rigid;

    float mouseX = 0;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * 10;
       // transform.eulerAngles = new Vector3(0, mouseX, 0);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput);

        direction.Normalize();

        Vector3 velocity = direction * speed;

        transform.position += velocity * Time.deltaTime;
//Debug.Log(isGrounded);

        // 땅에 닿았는지 여부를 확인합니다.
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // 바닥에 닿아 있는지 체크합니다.
       // isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }    
    }

}
