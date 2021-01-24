using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject restartButton;
    [SerializeField]
    private GameObject mainMenuButton;
    [SerializeField]
    private GameObject scoreText;
    [SerializeField]
    private GameObject appleScoreText;
    [SerializeField]
    private GameObject stageText;
    [SerializeField]
    private GameObject panelKnives;
    [SerializeField]
    private GameObject iconKnife;
    [SerializeField]
    private Color usedKnifeIconColor;

    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }

    public void ShowScore(int score)
    {
        scoreText.GetComponent<Text>().text = score.ToString();
    }

    public void ShowStage(int stage)
    {
        stageText.GetComponent<Text>().text ="STAGE "+ stage.ToString();
    }
    public void ShowAppleScore(int score)
    {
        appleScoreText.GetComponent<Text>().text = score.ToString();
    }

    public void ShowInitializedKnife(int count)
    {
        for(int i = 0; i < count; i++)
        {
            Instantiate(iconKnife, panelKnives.transform);
        }
    }

    private int indexToChange = 0;
    public void DecrementDisplayedKnifeCount()
    {
        panelKnives.transform.GetChild(indexToChange++).GetComponent<Image>().color = usedKnifeIconColor;
    }
}
