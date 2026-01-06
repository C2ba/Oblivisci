using UnityEngine;
using UnityEngine.UI;

public class SoundManager1 : MonoBehaviour
{
    FMOD.Studio.Bus musicBus;
    FMOD.Studio.Bus soundsBus;

    Slider slider;
    [SerializeField] bool isMusic;

    void Awake()
    {
        slider = GetComponent<Slider>();

        if (isMusic)
        {
            musicBus = FMODUnity.RuntimeManager.GetBus("bus:/music thing");
            slider.onValueChanged.AddListener(SetMusicVolume);

            slider.value = GameManager.instance.MusicVolume;
            musicBus.setVolume(GameManager.instance.MusicVolume);
        }
        else
        {
            soundsBus = FMODUnity.RuntimeManager.GetBus("bus:/sounds thing");
            slider.onValueChanged.AddListener(SetSFXVolume);

            slider.value = GameManager.instance.SFXVolume;
            soundsBus.setVolume(GameManager.instance.SFXVolume);
        }
    }

    void SetMusicVolume(float value)
    {
        GameManager.instance.MusicVolume = value;
        musicBus.setVolume(value);
    }
    void SetSFXVolume(float value)
    {
        GameManager.instance.SFXVolume = value;
        soundsBus.setVolume(value);
    }
}
