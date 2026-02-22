using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixer mixer;

    private float musicVolume = 0.6f;
    private float sfxVolume = 0.6f;
    private float sens = 0.7f;

    public void SetMusic(float value) { musicVolume = value; ApplyVolumes(); }
    public float GetMusic() { return musicVolume; }
    public void SetSFX(float value) { sfxVolume = value; ApplyVolumes(); }
    public float GetSFX() { return sfxVolume; }
    public void SetSens(float value) { sens = value; if (!PauseScript.paused) { ApplySens(); } }
    public float GetSens() { return sens; }

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
        ApplySens();
    }

    void ApplyVolumes()
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
    }

    void ApplySens()
    {
        if (GameObject.Find("Player") == null)
        {
            return;
        }
        InputHandler inputHandler = GameObject.Find("Player").GetComponent<InputHandler>();
        inputHandler.Sensitivity = sens;
    }
}
