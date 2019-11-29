using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public Text scoreText;
    public int footyValue;
    public int methValue;

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Footy"))
        {
            score += footyValue;
        }
        else
        {
            score -= methValue;
        }
            
        UpdateScore();
    }

    // Update is called once per frame
    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
