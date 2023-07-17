using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverUI;
    public Text gameOverText;
    public Text distanceScore;

    GameManager gm;

    private void Start()
    {
        GameManager.instance = gm;
    }
    void Update()
    {
        distanceScore.text = gm.distanceScore.ToString()+" m";

        if (gm.gameOverTimer >= 4) 
        {
            gameOverUI.SetActive(true);
        }

        
    }
}
