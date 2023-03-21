using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUI : MonoBehaviour
{
    public TextMeshProUGUI highScore;
    // Start is called before the first frame update
    void Start()
    {
        UpdateHighScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHighScoreText()
    {
        highScore.text = "High Score: " + MainManager.Instance.HighScore.ToString("F3") + " seconds";
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        MainManager.Instance.SaveHighscore();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit(); // original code to quit Unity player
#endif
    }
}
