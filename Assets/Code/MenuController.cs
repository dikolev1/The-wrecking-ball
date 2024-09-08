using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    public GameObject endButtons;
    public GameObject winScreen;
    public GameObject failSreen;
    public GameObject pauseScreen;

    public TextMeshProUGUI winText;
    public GameObject nextLevelButton;

    private bool isPaused = false;

    private void Awake()
    {
        EventManager.OnWin.AddListener(ShowWinScreen);
        EventManager.OnFail.AddListener(ShowFailSreen);
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            pauseScreen.SetActive(isPaused);
            if (isPaused)
            {
                endButtons.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Continue();
            }
        }
    }

    public void Continue()
    {
        isPaused = false;
        endButtons.SetActive(false);
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    private void  ShowWinScreen()
    {
        winScreen.SetActive(true);
        endButtons.SetActive(true);

        if (PlayerPrefs.GetInt("MaxLevel", 6) == SceneManager.GetActiveScene().buildIndex)
        {
            winText.text = "Вы прощли игру!";
            nextLevelButton.SetActive(false);
        }
    }
    private void ShowFailSreen()
    {
        failSreen.SetActive(true);
        endButtons.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToManeMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
