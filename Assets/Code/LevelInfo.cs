using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour
{
    public int level;
    public int maxLevel;
    public int hearatsCount = 3;
    public int destroyedBlocks = 0;
    public int maxBlocks;

    public List<GameObject> hearts;
    public TextMeshProUGUI blocksText;
    public TextMeshProUGUI levelText;

    private Platform _platform;
    private Ball _ball;

    private bool _isGameEnded = false;

    private void Awake()
    {
        EventManager.OnWin.AddListener(() => _isGameEnded = true);
        EventManager.OnFail.AddListener(() => _isGameEnded = true);

        _platform = FindObjectOfType<Platform>();
        _ball = FindObjectOfType<Ball>();
    }
    private void Start()
    {
        maxLevel = PlayerPrefs.GetInt("MaxLevel", 6);
        level = SceneManager.GetActiveScene().buildIndex;
        blocksText.text = destroyedBlocks.ToString() + "/" + maxBlocks.ToString();
        levelText.text = "Level " + level.ToString();
    }

    public void Fail()
    {
        if (hearatsCount <= 0 || _isGameEnded)
            return;
        hearts[hearatsCount - 1].gameObject.SetActive(false);
        hearatsCount--;
        if (hearatsCount <= 0)
        {
            EventManager.SendFail();
        }

        if (_isGameEnded)
            return;
        _ball.transform.position = new Vector3(_platform.transform.position.x, _platform.transform.position.y + 0.5f, 0);
        _platform.BallCatched();
        //_ball.transform.position = _platform.transform.position;
    }

    public void BlockDestroyed()
    {
        destroyedBlocks++;

        blocksText.text = destroyedBlocks.ToString() + "/" + maxBlocks.ToString();

        if (destroyedBlocks >= maxBlocks)
        {
            if (level + 1 <= maxLevel)
            {
                PlayerPrefs.SetInt("LastLevel", level + 1);
            }
            EventManager.SendWin();
        }
    }
}
