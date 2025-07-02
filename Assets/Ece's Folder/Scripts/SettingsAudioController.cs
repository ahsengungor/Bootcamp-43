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
        // Baþlangýç deðerlerini ayarla
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        // Sliderlarý mixer ile senkronize et (isteðe baðlý, mixer ayarýna göre)
        float musicVol;
        if (mixer.GetFloat("MusicVolume", out musicVol))
            musicSlider.value = Mathf.Pow(10, musicVol / 20f);

        float sfxVol;
        if (mixer.GetFloat("SFXVolume", out sfxVol))
            sfxSlider.value = Mathf.Pow(10, sfxVol / 20f);
    }

    public void SetMusicVolume(float value)
    {
        // Logaritmik dönüþümle mixer volume ayarý
        mixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        musicValueText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        sfxValueText.text = Mathf.RoundToInt(value * 100) + "%";
    }
}

