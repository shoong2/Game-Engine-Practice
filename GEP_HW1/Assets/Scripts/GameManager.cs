using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public float gameTime = 180f;
    [SerializeField]
    TMP_Text time;
    void Start()
    {
        time.text ="Time: " + gameTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        gameTime -= Time.deltaTime;
        time.text = "Time: " + string.Format("{0:F2}", gameTime);
    }
}
