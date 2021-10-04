using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;

    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayAudio(int index)
    {
        source.PlayOneShot(clips[index]);
    }

    public void PlayAudio(AudioClip audioClip)
    {
        source.PlayOneShot(audioClip);
    }

    public void PlayFootSteps()
    {
        source.PlayOneShot(clips[0]);
    }
}
