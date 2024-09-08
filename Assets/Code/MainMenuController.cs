using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject buttons;
    public GameObject levelsTab;
    public GameObject controlsButton;
    public TextMeshProUGUI controlsText;

    public List<Sprite> controlsSprite;
    public List<string> controlsDescription;

    private void Awake()
    {
        int isMouse = PlayerPrefs.GetInt("IsMouse", 1);
        controlsButton.GetComponent<Image>().sprite = controlsSprite[isMouse];
        PlayerPrefs.SetInt("IsMouse", isMouse);
        PlayerPrefs.Save();
    }

    public void Play()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastLevel", 1));
    }

    public void ShowLevelsTab()
    {
        buttons.SetActive(false);
        levelsTab.SetActive(true);
    }
    public void CloseLevelsTab()
    {
        buttons.SetActive(true);
        levelsTab.SetActive(false);
    }

    public void ChangeControls()
    {
        bool isMouse = PlayerPrefs.GetInt("IsMouse", 1) == 1;
        isMouse = !isMouse;
        PlayerPrefs.SetInt("IsMouse", isMouse ? 1 : 0);
        if (isMouse)
        {
            controlsButton.GetComponent<Image>().sprite = controlsSprite[1];
            controlsText.text = controlsDescription[1];
        }
        else
        {
            controlsButton.GetComponent<Image>().sprite = controlsSprite[0];
            controlsText.text = controlsDescription[0];
        }
        PlayerPrefs.Save();
    }

    public void Quit()
    {
        Debug.Log("Application is closed");
        Application.Quit();
    }
}
