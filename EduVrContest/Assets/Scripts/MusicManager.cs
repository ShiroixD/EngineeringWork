using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public SubSceneManager SubSceneManager;
    public AudioSource AudioPlayer;
    public AudioClip[] ForestMusic;
    public AudioClip[] TavernMusic;
    public AudioClip[] SchoolMusic;
    public AudioClip[] ControlRoomMusic;
    private System.Random _rnd = new System.Random();
    private bool _musicIsChanging = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public IEnumerator DelayedMuteMusic()
    {
        float volumeRate = 1.0f;
        while (_musicIsChanging)
        {
            yield return new WaitForSeconds(0.001f);
        }
        _musicIsChanging = true;
        do
        {
            volumeRate -= 0.05f;
            AudioPlayer.volume = volumeRate;
            yield return new WaitForSeconds(0.05f);
        } while (volumeRate > 0.0f);
        AudioPlayer.volume = 0.0f;
        _musicIsChanging = false;
    }

    IEnumerator DelayedUnmuteMusic(string worldName)
    {
        switch (worldName)
        {
            case "ControlRoom":
                {
                    int number = _rnd.Next(0, ControlRoomMusic.Length);
                    AudioPlayer.clip = ControlRoomMusic[number];
                    break;
                }
            case "Forest":
                {
                    int number = _rnd.Next(0, ForestMusic.Length);
                    AudioPlayer.clip = ForestMusic[number];
                    break;
                }
            case "Tavern":
                {
                    int number = _rnd.Next(0, TavernMusic.Length);
                    AudioPlayer.clip = TavernMusic[number];
                    break;
                }
            case "School":
                {
                    int number = _rnd.Next(0, SchoolMusic.Length);
                    AudioPlayer.clip = SchoolMusic[number];
                    break;
                }
            default:
                {
                    if (AudioPlayer.clip != null)
                    {
                        AudioPlayer.clip.UnloadAudioData();
                    }
                    break;
                }

        }
        AudioPlayer.Play();
        float volumeRate = 0.0f;
        do
        {
            yield return new WaitForSeconds(0.5f);
            volumeRate += 0.1f;
            AudioPlayer.volume = volumeRate;
        } while (volumeRate < 1.0f);
        AudioPlayer.volume = 1.0f;
        _musicIsChanging = false;
    }

    IEnumerator MusicTransition(string worldName)
    {
        while (_musicIsChanging)
        {
            yield return new WaitForSeconds(0.001f);
        }
        _musicIsChanging = true;
        StartCoroutine(DelayedUnmuteMusic(worldName));
        yield return new WaitForSeconds(0.001f);
    }

    public void PlayMusic(string worldName)
    {
        StartCoroutine(MusicTransition(worldName));
    }
}
