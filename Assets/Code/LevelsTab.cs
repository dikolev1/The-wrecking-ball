using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelsTab : MonoBehaviour
{
    public List<GameObject> levels;
    public int lastLevel;

    private void Start()
    {
        lastLevel = PlayerPrefs.GetInt("LastLevel", 1);

        for (int i = 0; i < lastLevel; i++)
        {
            levels[i].GetComponent<Button>().interactable = true;
        }
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
