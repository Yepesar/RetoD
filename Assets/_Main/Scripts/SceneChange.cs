using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private Image transition;
    [SerializeField] private float transitionTime;

    private void Start()
    {
        transition.gameObject.SetActive(true);
        transition.DOFade(0, transitionTime);
    }

    public void CallScene(int index)
    {
        transition.DOFade(1, transitionTime);
        StartCoroutine(ChangeScene(index));
    }

    private IEnumerator ChangeScene(int scene)
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadSceneAsync(scene);
    }
}
