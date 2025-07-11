using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Sound Effects")]
    public AudioClip flipSound;
    public AudioClip scoreSound;
    public AudioClip failSound;
    //public AudioClip buttonClickSound;

    [Header("Music Tracks")]
    public AudioClip[] backgroundMusicClips;

    private int lastPlayedIndex = -1;
    private bool musicShouldContinue = true;

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: persist between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Optional: Start music automatically
        // StartRandomMusicLoop();
    }

    // ?? SOUND EFFECTS
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void PlayClick()
    {
       // PlaySound(buttonClickSound);
    }

    // ?? RANDOM MUSIC LOOP
    public void StartRandomMusicLoop()
    {
        musicShouldContinue = true;
        StartCoroutine(PlayRandomLoop());
    }

    private IEnumerator PlayRandomLoop()
    {
        while (musicShouldContinue && backgroundMusicClips.Length > 0)
        {
            int index = GetNextRandomIndex();
            AudioClip nextClip = backgroundMusicClips[index];
            lastPlayedIndex = index;

            if (musicSource != null && nextClip != null)
            {
                musicSource.clip = nextClip;
                musicSource.Play();
                yield return new WaitForSeconds(nextClip.length);
            }
            else
            {
                yield return null;
            }
        }
    }

    private int GetNextRandomIndex()
    {
        if (backgroundMusicClips.Length <= 1)
            return 0;

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, backgroundMusicClips.Length);
        } while (randomIndex == lastPlayedIndex);

        return randomIndex;
    }

    public void StopMusicLoop()
    {
        musicShouldContinue = false;
        if (musicSource != null)
            musicSource.Stop();
    }
}
