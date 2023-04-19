using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public float gameTime = 120f;
    public float duration = 0.5f;
    [SerializeField]
    TMP_Text time;

    [SerializeField]
    RectTransform slot1;

    [SerializeField]
    RectTransform slot2;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject endImg;

    [SerializeField]
    GameObject timeOverImg;

    public TMP_Text endTimeText;

    bool isEnd = false;
    bool isTimeOver = false;

    Vector3 playerPos;
    public float endTime = 2f;
    float underTime = 0;

    Vector2 slot1Position;
    Vector2 slot2Position;

    void Start()
    {
        time.text ="Time: " + gameTime.ToString();
        playerPos = player.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isEnd && !isTimeOver)
        {
            gameTime -= Time.deltaTime;
            time.text = "Time: " + string.Format("{0:F2}", gameTime);
        }

        if(player.transform.position.y <playerPos.y -1f)
        {
            underTime += Time.deltaTime;
            if(underTime>=endTime)
            {
                player.transform.position = player.GetComponent<Player>().spawnPos;
                player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                underTime = 0;

            }
        }

        if(gameTime<=0)
        {
            isTimeOver = true;
            timeOverImg.SetActive(true);
        }
 
    }

    public void Title()
    {
        SceneManager.LoadScene("Title");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }

    public void end()
    {
        endImg.SetActive(true);
        isEnd = true;
        endTimeText.text = "Time: " + (int)gameTime;
    }

}
