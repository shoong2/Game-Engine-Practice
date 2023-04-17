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

    Vector2 slot1Position;
    Vector2 slot2Position;

    void Start()
    {
        time.text ="Time: " + gameTime.ToString();

        
    }

    // Update is called once per frame
    void Update()
    {
        gameTime -= Time.deltaTime;
        time.text = "Time: " + string.Format("{0:F2}", gameTime);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //StartCoroutine(ChangeUI());
        }
    }

    IEnumerator ChangeUI()
    {
        slot1Position = slot1.anchoredPosition;
        slot2Position = slot2.anchoredPosition;
        float elapsedTime = 0f;

        while(elapsedTime < duration)
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
