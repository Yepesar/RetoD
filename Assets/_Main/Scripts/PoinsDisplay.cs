using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PoinsDisplay : MonoBehaviour
{
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void DisplayPoints(int points)
    {
        if (text)
        {
            text.text = points.ToString();
        }       
    }
}
