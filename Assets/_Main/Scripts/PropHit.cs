using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropHit : MonoBehaviour
{
    [SerializeField] private int pointsRemove = 10;
    [SerializeField] private GameEvent onRemovePoints;
    [SerializeField] private AudioPlayer audioPlayer;

    private bool remove = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (remove)
            {
                onRemovePoints.Raise(pointsRemove);
                audioPlayer.PlayAudio(0);
                remove = false;
            }
        }
    }
}
