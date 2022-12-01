using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseManager : MonoBehaviour {

    public string sceneName;

    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;

    public GameObject nonDestroyer;


    Scene scene;//Reset() salvar o buildIndex
    

    [Space(8)] public Slider effectsSlider, musicSlider;
    float effectsVolume, musicVolume; // salva player prefs

    [Space(8)] public Toggle effectsToggle, musicToggle;
    int muteEffects, muteMusic; // salva player prefs

    // Use this for initialization
    void Start() {

        scene = SceneManager.GetActiveScene();//Reset: capturar a buildIndex

        GetPlayerPreferences();
    }
    // Update is called once per frame
    void Update () {

        Lowpass();

        SetPlayerPreferences();
    }

    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0; // se tempo == 0, vira 1; Se tempo == 1, vira 0
        Lowpass();
    }

    public void Reset()
    {
        DontDestroyChildOnLoad(nonDestroyer);

        if (scene.name == sceneName)
            SceneManager.LoadScene(sceneName);

        Time.timeScale = 1;
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    
    void Lowpass()
    {
        if(Time.timeScale == 0)
        {
            paused.TransitionTo(.01f);
        }
        else
        {
            unpaused.TransitionTo(.01f);
        }
    }


    public void GetPlayerPreferences()
    {
        #region Effects
        
        if (PlayerPrefs.GetInt("MuteEffects") == 1)
            effectsToggle.isOn = true;
        else
            effectsToggle.isOn = false;

        #endregion

        #region Music

        if (PlayerPrefs.GetInt("MuteMusic") == 1)
            musicToggle.isOn = true;
        else
            musicToggle.isOn = false;
        #endregion
    }
    void SetPlayerPreferences()
    {
        #region Effects

        if (effectsToggle.isOn)
        {
            PlayerPrefs.SetInt("MuteEffects", 1);
            effectsSlider.value = -65;
        }
        else if(!effectsToggle.isOn)
            PlayerPrefs.SetInt("MuteEffects", 0);

        #endregion

        #region Music

        if (musicToggle.isOn)
        {
            PlayerPrefs.SetInt("MuteMusic", 1);
            musicSlider.value = -65;
        }
        else if (!musicToggle.isOn)
            PlayerPrefs.SetInt("MuteMusic", 0);

        #endregion
    }

    public void DontDestroyChildOnLoad(GameObject child)
    {
        Transform parentTransform = child.transform; // If this object doesn't have a parent then its the root transform. 
        while (parentTransform.parent != null)
        { // Keep going up the chain. 
            parentTransform = parentTransform.parent;
        }
        DontDestroyOnLoad(parentTransform.gameObject);
    }


    public void LessMusic()
    {
        musicSlider.value = musicSlider.value - 10;
    }
    public void MoreMusic()
    {
        musicSlider.value = musicSlider.value + 10;
    }
    public void LessEffect()
    {
        effectsSlider.value = effectsSlider.value - 10;
    }
    public void MoreEffect()
    {
        effectsSlider.value = effectsSlider.value + 10;
    }
}