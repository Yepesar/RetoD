using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI botName;
    [SerializeField] private TextMeshProUGUI botSpeed;
    [SerializeField] private TextMeshProUGUI botResistance;
    [SerializeField] private WayPointsCreator wayPointsCreator;
    [SerializeField] private Transform movesDisplay;
    [SerializeField] private Color chargedColor;
    [SerializeField] private Color unchargedColor;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        UpdateMovements();
    }

    public void UpdateUI()
    {
        if (wayPointsCreator.ActualBot)
        {
            Bot b = wayPointsCreator.ActualBot;
            botName.text = b.BotName;
            botName.color = b.NameColor;
            botSpeed.text = b.Speed.ToString();
            botResistance.text = b.Resistance.ToString();
        }    
        else
        {
            botName.text = "Selecciona un robot";
            botName.color = Color.grey;
            botSpeed.text = "0";
            botResistance.text = "0";
        }      
    }

    private void UpdateMovements()
    {
        for (int i = 0; i < movesDisplay.childCount; i++)
        {
            Image img = movesDisplay.GetChild(i).GetComponent<Image>();

            if (i < wayPointsCreator.Moves)
            {
                img.color = chargedColor;
            }
            else
            {
                img.color = unchargedColor;
            }
        }
    }
}
