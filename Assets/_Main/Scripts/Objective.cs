using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{

    [SerializeField] private ParticleSystem completeVFX;
    [SerializeField] private GameObject complete;
    [SerializeField] private GameObject nonComplete;
    [SerializeField] private bool placeCompleteChildren = false;
    [SerializeField] private float placeTime;
   
    private int placeIndex = 0;

    private void Awake()
    {
        if (completeVFX)
        {
            completeVFX.Stop();
        }     
    }

    public void OnComplete()
    {
        nonComplete.SetActive(false);
        complete.SetActive(true);
        if (placeCompleteChildren)
        {
            placeIndex = complete.transform.childCount;
            StartCoroutine(PlaceObjects());
        }

        if (completeVFX)
        {
            completeVFX.Play();
        }
    }

    private IEnumerator PlaceObjects()
    {
        while (true)
        {
            placeIndex--;
            yield return new WaitForSeconds(placeTime);

            if (placeIndex < 0)
            {
                break;
            }
            else
            {
                complete.transform.GetChild(placeIndex).gameObject.SetActive(true);
            }
        }
    }
}
