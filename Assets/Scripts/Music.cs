using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    GameObject[] MusicSources;
    bool notfirst = false;
    void Awake()
    {
        MusicSources = GameObject.FindGameObjectsWithTag("Music");
        foreach(GameObject MusicSource in MusicSources)
        {
            if(MusicSource.scene.buildIndex == -1)
            {
                notfirst = true;
            }
        }
        if (notfirst)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
    }
    
    public void PlayMusic()
    {
        if(audioSource.isPlaying) return;
        audioSource.Play();
    }
    // Update is called once per frame
    public void StopMusic()
    {
        audioSource.Stop();
    }
}
