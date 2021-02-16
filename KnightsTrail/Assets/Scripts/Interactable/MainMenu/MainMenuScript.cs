using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Texture2D cursorArrow;
    private string ctnScene;
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] Button restartFromSaveFile;

    public void NewGame()
    {
        PlayerPrefs.SetString("newGame", "true");
        SceneManager.LoadScene("Intro");
    }

    public void Continue()
    {
        //load Save
        PlayerPrefs.SetString("newGame", "false");
        if (ctnScene != null)
            SceneManager.LoadScene(ctnScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        
        /*if (SaveWorker.SaveExists())
        {
            restartFromSaveFile.interactable = true;
            GameData data = SaveWorker.Load();
            ctnScene = data.currentScene;
        } else
        {
            restartFromSaveFile.interactable = false;
        }*/
    }

    public void Main()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void Options()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    List<int> widths = new List<int>() { 1920, 1280, 960 };
    List<int> heights = new List<int>() { 1080, 800, 540 };

    public void SetScreenSize(int index)
    {
        bool fullscreen = Screen.fullScreen;
        int width = widths[index];
        int height = heights[index];
        PlayerPrefs.SetInt("ResolutionIndex", index);
        Screen.SetResolution(width, height, fullscreen);
    }

    public void SetFullscreen(bool _fullscreen)
    {
        Screen.fullScreen = _fullscreen;
        PlayerPrefs.SetString("isFullscreen", _fullscreen.ToString());
    }

    [SerializeField] Slider volumeSlider;
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] TMP_Dropdown resolutionDropdown;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            SetVolume(PlayerPrefs.GetFloat("Volume"));
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        if (PlayerPrefs.HasKey("isFullscreen"))
        {
            if (PlayerPrefs.GetString("isFullscreen") == "False")
                fullscreenToggle.isOn = false;
            else
                fullscreenToggle.isOn = true;
        }
        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            resolutionDropdown.SetValueWithoutNotify(PlayerPrefs.GetInt("ResolutionIndex"));
        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
