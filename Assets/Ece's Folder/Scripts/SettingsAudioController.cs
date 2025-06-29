using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsAudioController : MonoBehaviour
{
    public AudioMixer mixer; // Mixer dosyan
    public Slider musicSlider;
    public Slider sfxSlider;
    public TextMeshProUGUI musicValueText;
    public TextMeshProUGUI sfxValueText;

    void Start()
    {
        // Ba�lang�� de�erlerini ayarla
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        // Sliderlar� mixer ile senkronize et (iste�e ba�l�, mixer ayar�na g�re)
        float musicVol;
        if (mixer.GetFloat("MusicVolume", out musicVol))
            musicSlider.value = Mathf.Pow(10, musicVol / 20f);

        float sfxVol;
        if (mixer.GetFloat("SFXVolume", out sfxVol))
            sfxSlider.value = Mathf.Pow(10, sfxVol / 20f);
    }

    public void SetMusicVolume(float value)
    {
        // Logaritmik d�n���mle mixer volume ayar�
        mixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        musicValueText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        sfxValueText.text = Mathf.RoundToInt(value * 100) + "%";
    }
}

