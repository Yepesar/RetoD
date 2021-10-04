using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private SceneChange sceneChanger;
    [SerializeField] private AudioPlayer audioPlayer;
    [SerializeField] private int nextLevl;
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sceneChanger.CallScene(nextLevl);
            audioPlayer.PlayAudio(0);
        }
    }
}
