using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsDisplay : MonoBehaviour
{
    [SerializeField] private Transform stars;
    [SerializeField] private int maxValue = 300;
    [SerializeField] private int minumunValue = 100;
    [SerializeField] private Color winColor;
    [SerializeField] private Color loseColor;


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < stars.childCount; i++)
        {
            Image img = stars.GetChild(i).gameObject.GetComponent<Image>();
            img.color = loseColor;
        }
    }

   public void DisplayStars(int points)
    {
        if (points >= maxValue)
        {
            for (int i = 0; i < stars.childCount; i++)
            {
                Image img = stars.GetChild(i).gameObject.GetComponent<Image>();
                img.color = winColor;               
            }
        }
        else if (points < minumunValue)
        {
            for (int i = 0; i < stars.childCount; i++)
            {
                Image img = stars.GetChild(i).gameObject.GetComponent<Image>();
                img.color = loseColor;
            }
        }
        else if (points < maxValue && points > minumunValue)
        {
            for (int i = 0; i < stars.childCount-1; i++)
            {
                Image img = stars.GetChild(i).gameObject.GetComponent<Image>();
                img.color = winColor;
            }
        }
        else if (points == minumunValue)
        {
            for (int i = 0; i < 1; i++)
            {
                Image img = stars.GetChild(i).gameObject.GetComponent<Image>();
                img.color = winColor;
            }
        }
    }
}
