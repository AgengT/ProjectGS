using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private List<AudioList> audioList;
    private Dictionary<string, AudioList> audioDictionary;
    private AudioSource audioSource;

    public float audioVolume = 1f;

    public static Action OnStopAudio;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        
        audioDictionary = new Dictionary<string, AudioList>();
        foreach (var audio in audioList)
        {
            if(audio.clip != null && !audioDictionary.ContainsKey(audio.name))
            {
                if(!audioDictionary.ContainsKey(audio.name))
                {
                    audioDictionary.Add(audio.name, audio);
                }
                else
                {
                    Debug.LogWarning("Duplicate audio name found: " + audio.name);
                }
            }
        }
    }

    public static void PlayOneShot(string name)
    {
        if (Instance.audioDictionary.TryGetValue(name, out AudioList audio))
        {
            if(audio.clip != null)
            {
                Instance.audioSource.PlayOneShot(audio.clip, audio.volume * Instance.audioVolume);
            } else
            {
                Debug.LogWarning("Audio clip is null for: " + name);
            }
        }
        else
        {
            Debug.LogWarning("Audio not found: " + name);
        }
    }

    public static AudioClip GetAudioClip(string name)
    {
        if(Instance.audioDictionary.TryGetValue(name, out AudioList audio))
        {
            if(audio.clip != null)
            {
                return audio.clip;
            }
        }

        return null;
    }

    public static void PlayLoop(string name)
    {
        if (Instance.audioDictionary.TryGetValue(name, out AudioList audio))
        {
            if(audio.clip != null)
            {
                Instance.audioSource.clip = audio.clip;
                Instance.audioSource.volume = audio.volume * Instance.audioVolume;
                Instance.audioSource.loop = true;
                Instance.audioSource.Play();
            } else
            {
                Debug.LogWarning("Audio clip is null for: " + name);
            }
        }
        else
        {
            Debug.LogWarning("Audio not found: " + name);
        }
    }

    public void StopAllSounds()
    {
        if(audioSource != null){
            audioSource.Stop();
            
        }
        OnStopAudio?.Invoke();
    }
}

[Serializable]
public struct AudioList
{
    public AudioClip clip;
    public string name;
    [Range (0,2)] public float volume;
}
