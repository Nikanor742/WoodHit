using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject bestScore;
    [SerializeField]
    private GameObject bestStage;
    [SerializeField]
    private GameObject apples;
    void Start()
    {
        if (PlayerPrefs.HasKey("BestStage"))
        {
            bestStage.GetComponent<Text>().text ="STAGE "+ PlayerPrefs.GetInt("BestStage").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("BestStage", 0);
            PlayerPrefs.Save();
            bestStage.GetComponent<Text>().text ="STAGE 1";
        }

        if (PlayerPrefs.HasKey("ScoreKnives"))
        {
            bestScore.GetComponent<Text>().text ="Best score " + PlayerPrefs.GetInt("ScoreKnives").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("ScoreKnives", 0);
            PlayerPrefs.Save();
            bestScore.GetComponent<Text>().text ="Best score 0";
        }

        if (PlayerPrefs.HasKey("AppleScore"))
        {
            apples.GetComponent<Text>().text = PlayerPrefs.GetInt("AppleScore").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("AppleScore", 0);
            PlayerPrefs.Save();
            apples.GetComponent<Text>().text = "0";
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
