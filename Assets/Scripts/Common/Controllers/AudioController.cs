using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource sfxAudioSource; // Sound effect
    [SerializeField] private AudioSource bgmAudioSource; // Background music
    [SerializeField] private List<AudioClip> laserAudioClips = new();
    [SerializeField] private List<AudioClip> explosionAudioClips = new();
    [SerializeField] private List<AudioClip> buttonSelectAudioClips = new();
    [SerializeField] private AudioClip mainBGMAudioClip;
    [SerializeField] private AudioClip bossBGMAudioClip;
    [SerializeField] private AudioClip gameOverAudioClip;

    public static AudioController Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Player.Dead += OnPlayerDead;
        GameController.Replay += OnGameReplay;
    }

    private void OnDestroy()
    {
        Player.Dead -= OnPlayerDead;
        GameController.Replay -= OnGameReplay;
    }

    private void Start()
    {
        //PlayMusic(mainBGMAudioClip);
    }

    public void SetVolumeSfx(float value) => sfxAudioSource.volume = value;
    public void SetVolumeBgm(float value) => bgmAudioSource.volume = value;
    public float GetVolumeSfx() => sfxAudioSource.volume;
    public float GetVolumeBgm() => bgmAudioSource.volume;

    public void PlaySelectButtonSFX()
    {
        PlayRandomFromList(buttonSelectAudioClips);
    }

    public void PlayLaserSFX()
    {
        PlayRandomFromList(laserAudioClips);
    }

    public void PlayBossBGM()
    {
        PlayMusic(bossBGMAudioClip);
    }

    public void PlayExplosionSFX()
    {
        PlayRandomFromList(explosionAudioClips);
    }

    private void PlayRandomFromList(List<AudioClip> audioClipList)
    {
        if (audioClipList == null || audioClipList.Count <= 0)
        {
            return;
        }

        var rnd = new System.Random();
        var index = rnd.Next(audioClipList.Count);
        var clip = audioClipList[index];
        PlaySFX(clip);
    }

    private void PlaySFX(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }

    private void PlayMusic(AudioClip clip)
    {
        bgmAudioSource.clip = clip;
        bgmAudioSource.Play();
        bgmAudioSource.loop = true;
    }

    private void OnPlayerDead()
    {
        PlayExplosionSFX();
        PlayMusic(gameOverAudioClip);
    }

    private void OnGameReplay()
    {
        PlayMusic(mainBGMAudioClip);
    }
}