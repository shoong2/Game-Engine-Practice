using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{
    public float speed = 5f;   
    public float jumpForce = 5f;
    public float rotationSpeed = 3f;
    public bool isGrounded =true;
    bool slotClick = true;

    public float gameTime = 180f;
    public float duration = 0.5f;

    public int bulletCount = 3;
    
    Vector2 slot1Position;
    Vector2 slot2Position;

    public Vector3 spawnPos;

    TMP_Text bulletCountText;

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

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform bulletPos;

    [SerializeField]
    GameManager gameManager;

    GameObject swordPrefab;
    GameObject gunPrefab;

    AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip swordSound;
    public AudioClip flipSound;
    public AudioClip coinSound;

   

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        spawnPos = transform.position + new Vector3(0,0.3f,0);
        audioSource = GetComponent<AudioSource>();
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "Jump":
                audioSource.clip = jumpSound;
                break;
            case "Shot":
                audioSource.clip = shotSound;
                break;
            case "Reload":
                audioSource.clip = reloadSound;
                break;
            case "Sword":
                audioSource.clip = swordSound;
                break;
            case "Flip":
                audioSource.clip = flipSound;
                break;
            case "Coin":
                audioSource.clip = coinSound;
                break;
        }
        audioSource.Play();
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput);

        direction.Normalize();

        Vector3 velocity = direction * speed;

        transform.position += velocity * Time.deltaTime;

        transform.LookAt(transform.position + direction);

       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            PlaySound("Jump");
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        //왼쪽 마우스 버튼을 눌러 현재 무기에 따른 공격
        if (Input.GetMouseButtonDown(0) && nowWeapon!=null)
        {
            if(nowWeapon.tag == "Sword")
            {
                PlaySound("Sword");
                swordAnim.SetTrigger("Attack");
            }
            else if(nowWeapon.tag == "Gun") //&& bulletCount!=0)
            {
                if (bulletCount != 0)
                {
                    PlaySound("Shot");
                    bulletCount--;
                    bulletCountText.text = bulletCount.ToString();
                    gunAnim.SetTrigger("Attack");
                    StartCoroutine(Shot());
                }
                else
                    PlaySound("Reload");
                
            }
        }


        //1, 2번 무기 교체
        if(Input.GetKeyDown(KeyCode.Alpha1) &&nowWeapon!=null &&slotClick)
        {
            
            if(Weapons[0] != null && nowWeapon!=Weapons[0])
            {
                slotClick = false;
                nowWeapon = Weapons[0];
                foreach (GameObject obj in Weapons)
                {
                    obj.SetActive(false);
                }
                Weapons[0].SetActive(true);
                StartCoroutine(ChangeUI());
            }
          
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && nowWeapon != null && slotClick)
        {
           
            if (Weapons[1] != null && nowWeapon != Weapons[1])
            {
                slotClick = false;
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
        //칼 아이템 먹기
        if(other.gameObject.tag =="SwordItem")
        {
            PlaySound("Coin");
            //칼 생성후 플레이어 weapon 자식으로 부착, 위치 조정, 애니메이션 적용, 무기 리스트에 추가
            swordPrefab = Instantiate(sword);
            swordPrefab.transform.SetParent(weapon.transform);
            swordPrefab.transform.localPosition = new Vector3(0, -0.172f, 0);
            swordAnim = swordPrefab.GetComponent<Animator>();
            Weapons.Add(swordPrefab);

            //현재 무기가 없다면 활성화
            if (nowWeapon == null)
            {
                nowWeapon = swordPrefab;
                swordPrefab.gameObject.SetActive(true);
            }

            //칼 UI 생성
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
        //총 아이템 먹기
        if(other.gameObject.tag =="GunItem")
        {
            PlaySound("Coin");
            if (!Weapons.Contains(gunPrefab))
            {
                gunPrefab = Instantiate(gun);
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
                //swordimg.transform.FindChild("BulletCount");
                //총알 개수 표시
                bulletCountText = swordimg.transform.Find("BulletCount").GetComponent<TMP_Text>();
                bulletCountText.text = bulletCount.ToString();
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
            }

            Destroy(other.gameObject);

        }

        //총알 먹기
        if(other.gameObject.tag =="BulletItem" &&Weapons.Contains(gunPrefab))
        {
            PlaySound("Coin");
            bulletCount += 3;
            bulletCountText.text = bulletCount.ToString();
            Destroy(other.gameObject);
        }

        if(other.tag =="EnemyBullet")
        {
            transform.position = spawnPos;
            isGrounded = true;
        }

        if(other.tag =="End")
        {
            gameManager.end();
        }

        //if(other.tag =="EnemyBullet" || other.tag =="Enemy")
        //{
        //    transform.position = spawnPos;
        //}
        
          
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

    //슬롯UI change
    IEnumerator ChangeUI()
    {
        PlaySound("Flip");
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

        slotClick = true;
    }

    IEnumerator Shot()
    {
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50f;
        yield return new WaitForSeconds(0.5f);
        Destroy(instantBullet.gameObject);
    }


    //Enemy에서 죽는다면 총을 갖고 있는 여부 체크 후 아이템 제공
    public bool IsHaveGun()
    {
        if (Weapons.Contains(gunPrefab))
        {
            return true;
        }
        else
            return false;
    }

}
