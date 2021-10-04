using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPool : MonoBehaviour
{
    [SerializeField] private AudioClip[] songs;

    private float changeTime;
    private AudioSource audioSource;
    public static MusicPool musicPool;

    private List<AudioClip> posibleTracks = new List<AudioClip>();
    private AudioClip lastSong;
    // Start is called before the first frame update
    void Awake()
    {
        if (musicPool == null)
        {
            musicPool = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ChangeSong());
    }



    private AudioClip SelectSong(List<AudioClip> candidates)
    {
        int r = Random.Range(0, candidates.Count);
        changeTime = candidates[r].length;
        return candidates[r];
    }

    private void CreateList()
    {
        posibleTracks = new List<AudioClip>();
        if (lastSong != null)
        {
            for (int i = 0; i < songs.Length; i++)
            {
                if (songs[i] != lastSong)
                {
                    posibleTracks.Add(songs[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < songs.Length; i++)
            {
                posibleTracks.Add(songs[i]);
            }
        }
    }

    private IEnumerator ChangeSong()
    {
        while (true)
        {
            CreateList();
            audioSource.clip = SelectSong(posibleTracks);
            audioSource.Play();
            yield return new WaitForSeconds(changeTime);
        }             
    }
 
}
