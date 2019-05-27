using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;
    public new AudioMixer audio;

	public Sound[] sounds;

	private void Awake()
	{
		if(Instance == null){
			Instance = this;
		}
		else{
			Destroy(this.gameObject);
			return;
		}

		foreach (Sound s in sounds)
		{
			s.source = this.gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch; // pitch minimo tem de ser 1
			s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = audio.FindMatchingGroups("Master")[0];
        }
	}

	public void Play(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.Play();
	}

	public void Stop(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.Stop();
	}

    public void SetVolume(float volume)
    {
        audio.SetFloat("Volume", volume);
    }
}
