using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip mainMenuMusic;
    public AudioClip gameMusic;

    private AudioSource source;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        source.clip = mainMenuMusic;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void HandleGameStateChanged(GameState current, GameState previous)
    {
        if (current == GameState.RUNNING)
        {
            StartCoroutine(AudioFading.MusicFadeOut(source));
            source.clip = gameMusic;
            source.Play();
            StartCoroutine(AudioFading.MusicFadeIn(source));
        }
        if (current == GameState.PREGAME)
        {
            StartCoroutine(AudioFading.MusicFadeOut(source));
            source.clip = mainMenuMusic;
            source.Play();
            StartCoroutine(AudioFading.MusicFadeIn(source));

        }
    }


}


public static class AudioFading
{
    public static IEnumerator MusicFadeOut(AudioSource audioSource)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= 0.01f * Time.deltaTime;

            yield return null;
        }
    }

    public static IEnumerator MusicFadeIn(AudioSource audioSource)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume < 0.2f)
        {
            audioSource.volume += 0.01f * Time.deltaTime;

            yield return null;
        }
    }

}