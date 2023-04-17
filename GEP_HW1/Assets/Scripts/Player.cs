using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;    // �̵� �ӵ�
    public float jumpForce = 5f;        // ���� ��
    public float groundCheckDistance = 1f; // ������� �Ÿ�
    public LayerMask groundLayer;        // ���� ���̾�
    bool isGrounded =true;                    // ���� ��Ҵ��� ����

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

        // ���� ��Ҵ��� ���θ� Ȯ���մϴ�.
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // �ٴڿ� ��� �ִ��� üũ�մϴ�.
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
