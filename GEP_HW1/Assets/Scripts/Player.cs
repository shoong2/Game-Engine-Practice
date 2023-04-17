using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public float speed = 5f;   
    public float jumpForce = 5f;        
    bool isGrounded =true;

    public float gameTime = 180f;
    public float duration = 0.5f;

    Vector2 slot1Position;
    Vector2 slot2Position;

    Rigidbody rigid;

    [SerializeField]
    GameObject weapon;

    [SerializeField]
    GameObject sword;

    Animator swordAnim;

    [SerializeField]
    GameObject gun;

    Animator gunAnim;

    [SerializeField]
    GameObject nowWeapon;

    [SerializeField]
    List<GameObject> Weapons;

    [SerializeField]
    RectTransform slot1;

    [SerializeField]
    RectTransform slot2;

    [SerializeField]
    GameObject swordImage;
    [SerializeField]
    GameObject gunImage;



    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput);

        direction.Normalize();

        Vector3 velocity = direction * speed;

        transform.position += velocity * Time.deltaTime;

   
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if(Input.GetMouseButtonDown(0) && nowWeapon!=null)
        {
            if(nowWeapon.tag == "Sword")
            {
                swordAnim.SetTrigger("Attack");
            }
            else if(nowWeapon.name == "Gun")
            {

            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha1) &&nowWeapon!=null)
        {
            if(Weapons[0] != null && nowWeapon!=Weapons[0])
            {
                nowWeapon = Weapons[0];
                foreach (GameObject obj in Weapons)
                {
                    obj.SetActive(false);
                }
                Weapons[0].SetActive(true);
                StartCoroutine(ChangeUI());
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && nowWeapon != null)
        {
            if (Weapons[1] != null && nowWeapon != Weapons[1])
            {
                nowWeapon = Weapons[1];
                foreach (GameObject obj in Weapons)
                {
                    obj.SetActive(false);
                }
                Weapons[1].SetActive(true);
                StartCoroutine(ChangeUI());
            }
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name =="SwordItem")
        {
            GameObject swordPrefab = Instantiate(sword);
            swordPrefab.transform.SetParent(weapon.transform);
            swordPrefab.transform.localPosition = new Vector3(0, -0.172f, 0);
            swordAnim = swordPrefab.GetComponent<Animator>();
            Weapons.Add(swordPrefab);

            if (nowWeapon == null)
            {
                nowWeapon = swordPrefab;
                swordPrefab.gameObject.SetActive(true);
            }

            GameObject swordimg = Instantiate(swordImage);
            if(slot1.childCount ==0)
            {
                swordimg.transform.SetParent(slot1);
                swordimg.transform.localPosition = new Vector3(0, 0, 0);
            }
            else
            {
                swordimg.transform.SetParent(slot2);
                swordimg.transform.localPosition = new Vector3(0, 0, 0);
            }

            Destroy(other.gameObject);
        }
        else if(other.name =="GunItem")
        {
            GameObject gunPrefab = Instantiate(gun);
            gunPrefab.transform.SetParent(weapon.transform);
            gunPrefab.transform.localPosition = new Vector3(0, 0, 0);
            gunAnim = gunPrefab.GetComponent<Animator>();
            Weapons.Add(gunPrefab);

            if (nowWeapon == null)
            {
                nowWeapon = gunPrefab;
                gunPrefab.gameObject.SetActive(true);
            }

            GameObject swordimg = Instantiate(gunImage);
            if (slot1.childCount == 0)
            {
                swordimg.transform.SetParent(slot1);
                swordimg.transform.localPosition = new Vector3(0, 0, 0);
            }
            else
            {
                swordimg.transform.SetParent(slot2);
                swordimg.transform.localPosition = new Vector3(0, 0, 0);
            }

            Destroy(other.gameObject);
        }
          
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

    IEnumerator ChangeUI()
    {
        slot1Position = slot1.anchoredPosition;
        slot2Position = slot2.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            slot1.anchoredPosition = Vector2.Lerp(slot1Position, slot2Position, (elapsedTime / duration));
            slot2.anchoredPosition = Vector2.Lerp(slot2Position, slot1Position, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;

        }
        slot1.anchoredPosition = slot2Position;
        slot2.anchoredPosition = slot1Position;


    }

}
