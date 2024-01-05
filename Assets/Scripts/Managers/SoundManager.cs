using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Music")]
    [SerializeField] private AudioClip[] mainThemes;

    private AudioSource audioSource;
    private ObjectPooler pooler;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pooler = GetComponent<ObjectPooler>();

        PlayMusic();
    }

    // Plays our background music
    private void PlayMusic()
    {
        if (audioSource == null)
        {
            return;
        }

        int randomTheme = Random.Range(0, mainThemes.Length);

        audioSource.clip = mainThemes[randomTheme];
        audioSource.Play();
    }

    // Plays a sound
    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        // Get AudioSource GameObject
        GameObject newAudioSource = pooler.GetObjectFromPool();
        newAudioSource.SetActive(true);

        // Get AudioSource from object
        AudioSource source = newAudioSource.GetComponent<AudioSource>();

        // Setup AudioSource
        source.clip = clip;
        source.volume = volume;
        source.Play();

        StartCoroutine(IEReturnToPool(newAudioSource, clip.length + 0.1f));
    }

    // Return on sound object back to the pool
    private IEnumerator IEReturnToPool(GameObject objectToReturn, float time)
    {
        yield return new WaitForSeconds(time);
        objectToReturn.SetActive(false);
    }
}
