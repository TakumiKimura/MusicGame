using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    static SoundManager instance = null;

    public CriAtom criAtom;
    public CriAtomSource sourceSE;
    public CriAtomSource sourceGameMusic;

	void Start () {
		if(instance != null)
        {
            enabled = false;
            DestroyImmediate(this);
            instance = null;
            return;
        }
        instance = this;
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            PlaySE("Tap");
        }
    }
	
    void _playGameMusic(ref string _cueName)
    {
        if(sourceGameMusic != null)
        {
            sourceGameMusic.Play(_cueName);
        }
    }

    public static void PlayGameMusic(string _cueName)
    {
        if(instance != null)
        {
            instance._playGameMusic(ref _cueName);
        }
    }

    void _playSE(ref string _cueName)
    {
        if(sourceSE != null)
        {
            sourceSE.Play(_cueName);
        }
    }

    public static void PlaySE(string _cueName)
    {
        if(instance != null)
        {
            instance._playSE(ref _cueName);
        }
    }
}
