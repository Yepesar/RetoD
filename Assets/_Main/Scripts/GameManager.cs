using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Bot[] bots;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScereen;
    [SerializeField] private float endDelay;
    [SerializeField] private GameEvent onGameComplete;
    [SerializeField] private AudioPlayer audioPlayer;

    private int points;
    private int botsCompleted = 0;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(() => Play());
    }

    public void BotComplete()
    {
        botsCompleted++;
        if (botsCompleted == bots.Length)
        {
            Invoke("ChecktResult", endDelay);
        }
    }

    private void ChecktResult()
    {
        if (points >= 100)
        {
            winScreen.SetActive(true);
            audioPlayer.PlayAudio(0);
        }
        else
        {
            loseScereen.SetActive(true);
            audioPlayer.PlayAudio(1);
        }

        Invoke("PassResults", 0.1f);
    }

    private void PassResults()
    {
        onGameComplete.Raise(points);

    }

    public void  AddPoints(int ammount)
    {
        points += ammount;
    }

    public void RemovePoints(int ammount)
    {
        points -= ammount;
    }

    private void Play()
    {
        for (int i = 0; i < bots.Length; i++)
        {
            if (bots[i].Alive)
            {
                bots[i].Play();
            }            
        }

        playButton.onClick.RemoveAllListeners();
    }
}
