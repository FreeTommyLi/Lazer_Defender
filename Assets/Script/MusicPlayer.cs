using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    [SerializeField] AudioClip startClip;
    [SerializeField] AudioClip gameClip;
    [SerializeField] AudioClip endClip;

    static MusicPlayer instance = null;
    private AudioSource music;

    void Awake()
    {
        
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            AudioListener.DontDestroyOnLoad(this);
            music = GetComponent<AudioSource>();
            music.clip = startClip;
            music.loop = true;
            
        }
    }

    // Use this for initialization
    void Start () {
       
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnLevelWasLoaded(int level) {

        if(instance != null) {
            return;
        }
         music.Stop();

        if (level == 0) {
            music.clip = startClip;
        }
        if (level == 1) {
            music.clip = gameClip;
        }
        if (level == 2) {
            music.clip = endClip;
        }

        if (level == 3) {
            music.clip = endClip;
        }

        music.loop = true;
        music.Play();

    }
}
