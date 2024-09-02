using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager: MonoBehaviour
{
    public Sound[] sounds;
    public static SoundManager main;
    private void Start()
    {
        if (main == null)
            main = this;
        else
            Destroy(gameObject);
        foreach (Sound s in sounds)
        {
            if (s.source == null)
            {
                s.source = gameObject.AddComponent<AudioSource>();
            }
            if (s.source != null)
            {
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.loop = s.loop;
            }
        
        }

        Play("Loop1");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            return;
        s.source.clip = s.clip;
        s.source.Play();
    }

    public void PlayOneShoot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            return;
        s.source.clip = s.clip;
        s.source.PlayOneShot(s.clip);
    }
    public void PlayClipsSequentially(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        AudioClip footstepSound = s.clips[Random.Range(0, s.clips.Length)];
        s.source.clip = footstepSound;
        s.source.Play();
 
    }

   
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            return;

        s.source.Stop();
    }

}
