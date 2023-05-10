using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Score : MonoBehaviour
{
    public float score;
    public Text ScoreUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreUI.text = score.ToString("0");
    }

    // void AddScore(int scoreBonus)
    //{
    //    score += scoreBonus;
    // }

    public void AddScore(float scoreBonus)
    {
        score += scoreBonus;
       
    }
}
