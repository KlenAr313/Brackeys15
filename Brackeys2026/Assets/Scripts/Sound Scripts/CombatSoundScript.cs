using UnityEngine;
using UnityEngine.Audio;

public class CombatSoundScript : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] hitSounds;
    [SerializeField] private AudioClip[] getHitSounds;
    [SerializeField] private AudioClip[] missSounds;
    [SerializeField] private AudioClip[] deathSounds;
    [SerializeField] private AudioMixer mixer;
    public static CombatSoundScript Instance;

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            return;
        }
    }

    public void PlayHit()
    {
        PlayRandom(hitSounds);
    }
    
    public void PlayGetHit()
    {
        PlayRandom(getHitSounds);
    }

    public void PlayMiss()
    {
        PlayRandom(missSounds);
    }

    public void PlayDeath(GameObject enemyObject)
    {
        PlayRandom(deathSounds, enemyObject);
        Debug.Log(enemyObject);
    }

    public void PlayRandom(AudioClip[] clips, GameObject enemyObject = null)
    {
        int index = Random.Range(0, clips.Length);
        

        if (enemyObject == null)
        {
            source.pitch = Random.Range(0.9f, 1.1f);
            source.PlayOneShot(clips[index]);
        }
        else
        {
            GameObject audioObject = new GameObject("DeathSoundEnemy");
            audioObject.transform.position = enemyObject.transform.position;
            AudioSource tempAudioSource = audioObject.AddComponent<AudioSource>();
            tempAudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFXVolume")[0];
            tempAudioSource.clip = clips[index];
            tempAudioSource.pitch = Random.Range(0.9f, 1.1f);
            tempAudioSource.spatialBlend = 1f;
            tempAudioSource.maxDistance = 1.5f;
            tempAudioSource.volume = 0.5f;
            tempAudioSource.Play();
            Destroy(audioObject, clips[index].length / tempAudioSource.pitch);
        }
    }
}
