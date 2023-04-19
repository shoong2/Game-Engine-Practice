using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public float gameTime = 180f;
    public float duration = 0.5f;
    [SerializeField]
    TMP_Text time;

    [SerializeField]
    RectTransform slot1;

    [SerializeField]
    RectTransform slot2;

    [SerializeField]
    GameObject player;

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
        gameTime -= Time.deltaTime;
        time.text = "Time: " + string.Format("{0:F2}", gameTime);

        if(player.transform.position.y <playerPos.y -1f)
        {
            underTime += Time.deltaTime;
            if(underTime>=endTime)
            {
                player.transform.position = player.GetComponent<Player>().spawnPos;
                underTime = 0;

            }
        }
 
    }

}
