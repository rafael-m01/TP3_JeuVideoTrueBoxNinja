using UnityEngine;
using UnityEngine.UI;

public class GameSettingsPanel : MonoBehaviour
{

    [Header("UI Handles")]
    public Slider volumeSlider;
    public Toggle particlesToggle;



    void Start()
    {

        if (volumeSlider != null)
        {
            volumeSlider.value = GameSettings.MusicVolume;
        }

        if (particlesToggle != null)
        {
            particlesToggle.isOn = GameSettings.ShowParticles;
        }


        volumeSlider.onValueChanged.AddListener(UpdateVolume);


        particlesToggle.onValueChanged.AddListener(UpdateParticles);
    }


    public void UpdateVolume(float value)
    {

        GameSettings.SetMusicVolume(value);
    }


    public void UpdateParticles(bool value)
    {

        GameSettings.SetShowParticles(value);
    }


    public void ClosePanel()
    {

        gameObject.SetActive(false);
    }
}