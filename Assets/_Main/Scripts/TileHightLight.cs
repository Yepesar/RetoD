using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHightLight : MonoBehaviour
{
    [SerializeField] private Material normalMat;
    [SerializeField] private Material highlightMat;
    [SerializeField] private bool isObjectiveTile = false;
    [SerializeField] private AudioPlayer audioPlayer;

    private MeshRenderer mRenderer;

    // Start is called before the first frame update
    void Start()
    {
        mRenderer = GetComponent<MeshRenderer>();
    }

    private void OnMouseEnter()
    {
        if (!isObjectiveTile)
        {
            mRenderer.material = highlightMat;
            audioPlayer.PlayAudio(5);
        }
    }

    private void OnMouseExit()
    {
        if (!isObjectiveTile)
        {
            mRenderer.material = normalMat;
        }
    }
}
