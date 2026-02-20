using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixer mixer;

    private float musicVolume = 0.6f;
    private float sfxVolume = 0.6f;


    public void SetMusic(float value) { musicVolume = value; ApplyVolumes(); }
    public float GetMusic() { return musicVolume; }
    public void SetSFX(float value) { sfxVolume = value; ApplyVolumes(); }
    public float GetSFX() { return sfxVolume; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        ApplyVolumes();
    }

    void ApplyVolumes()
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
    }
}
