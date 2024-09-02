using System;
using System.Collections;
using UnityEngine;

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


    public IEnumerator PlayClipsSequentially(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        foreach (AudioClip clip in s.clips)
        {
            s.source.clip = clip;
            s.source.Play();
            // Espera a que el clip termine de reproducirse
            yield return new WaitForSeconds(clip.length);
        }
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            return;

        s.source.Stop();
    }

}
