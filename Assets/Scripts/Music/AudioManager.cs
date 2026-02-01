using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    // Declare the AudioSources
    private AudioSource sfxSource;
    private AudioSource musicSource;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return; // stop executing Awake if duplicate
        }

        // Automatically assign the AudioSources attached to this GameObject
        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length >= 2)
        {
            sfxSource = sources[0];
            musicSource = sources[1];
        }
        else
        {
            Debug.LogError("AudioManager needs two AudioSource components on the same GameObject!");
        }
    }

    // Play a one-shot SFX
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (sfxSource != null)
            sfxSource.PlayOneShot(clip, volume);
    }

    // Play background music
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (musicSource != null)
        {
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }
    }
}
