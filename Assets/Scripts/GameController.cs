using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameUI))]
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField]
    private int knifeCount=8;
    [SerializeField]
    private Vector2 knifeSpawnPosition;
    [SerializeField]
    private GameObject knifeObject;
    [SerializeField]
    private GameObject woodObject;
    [SerializeField]
    private GameObject woodDestr;
    [SerializeField]
    private GameObject appleObject;
    [SerializeField]
    private List<GameObject> knivesInWood;
    [SerializeField]
    public bool game = true;
    [SerializeField]
    private int knivesScoreCount=0;
    [SerializeField]
    private int appleCount=0;
    [Header("Scriptable Objects")]
    [SerializeField]
    private ItemData apple;


    public GameUI GameUI {get;private set;}

    private void Awake()
    {
        Instance = this;
        GameUI = GetComponent<GameUI>();
    }

    private void Start()
    {
        GameUI.ShowInitializedKnife(knifeCount);
        SpawnKnife();
        SpawnApple();
        SpawnKnivesInWood();

        if (PlayerPrefs.HasKey("AppleScore"))
        {
            appleCount = PlayerPrefs.GetInt("AppleScore");
            GameUI.ShowAppleScore(appleCount);
        }
        else
        {
            PlayerPrefs.SetInt("AppleScore", 0);
            PlayerPrefs.Save();
            GameUI.ShowAppleScore(0);
        }

        if (PlayerPrefs.HasKey("CurrentScore")&& PlayerPrefs.GetInt("CurrentScore")>0)
        {
            knivesScoreCount = PlayerPrefs.GetInt("CurrentScore");
            GameUI.ShowScore(knivesScoreCount);
        }
        else
        {
            knivesScoreCount = 0;
            PlayerPrefs.SetInt("CurrentScore", 0);
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.HasKey("Stage"))
        {
            if (PlayerPrefs.GetInt("Stage") > 1)
            {
                GameUI.ShowStage(PlayerPrefs.GetInt("Stage"));
            }
            else
            {
                PlayerPrefs.SetInt("CurrentScore", 0);
                PlayerPrefs.Save();
                GameUI.ShowStage(1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Stage", 1);
            PlayerPrefs.Save();
            GameUI.ShowStage(1);
        }

        if (!PlayerPrefs.HasKey("BestStage"))
        {
            PlayerPrefs.SetInt("BestStage", 1);
            PlayerPrefs.Save();
        }
    }

    
    private void SpawnKnivesInWood()
    {
        int count = Random.Range(1, 4);
        for(int i = 0; i < count; i++)
        {
            int index = Random.Range(0, 6);
            if (knivesInWood[index].activeSelf == false)
            {
                knivesInWood[index].SetActive(true);
            }
            else
            {
                i--;
            }
        }
    }
    private void SpawnApple()
    {
        int rand = Random.Range(1, 101);
        if (rand <= apple.ItemChance)
        {
            appleObject.SetActive(true);
        }
    }

   

    private void SpawnKnife()
    {
        knifeCount--;
        Instantiate(knifeObject, knifeSpawnPosition, Quaternion.identity);
    }

    

    private IEnumerator GameOverCoroutine(bool win)
    {
        if (win)
        {
            int stage = PlayerPrefs.GetInt("Stage");
            stage++;
            if (PlayerPrefs.GetInt("BestStage") < stage)
            {
                PlayerPrefs.SetInt("BestStage", stage);
                PlayerPrefs.Save();
            }
            PlayerPrefs.SetInt("BestStage", stage);
            PlayerPrefs.Save();

            PlayerPrefs.SetInt("Stage", stage);
            PlayerPrefs.Save();

            PlayerPrefs.SetInt("CurrentScore", knivesScoreCount);
            PlayerPrefs.Save();
            WoodDestroy();
            yield return new WaitForSeconds(1f);
            RestartGame();
        }
        else
        {
            PlayerPrefs.SetInt("Stage", 1);
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("CurrentScore", 0);
            PlayerPrefs.Save();
            GameUI.ShowRestartButton();
        }
    }
    private void WoodDestroy()
    {
        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Knife"))
        {
            i.transform.SetParent(null);
            i.GetComponent<Rigidbody2D>().gravityScale = 1;
            i.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            i.GetComponent<Rigidbody2D>().freezeRotation = false;
            if (Random.Range(0, 2) == 0)
            {
                i.GetComponent<Rigidbody2D>().AddForce(Vector2.left * Random.Range(10, 20), ForceMode2D.Impulse);
            }
            else
            {
                i.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Random.Range(10, 20), ForceMode2D.Impulse);
            }
            i.transform.Rotate(0, 0, Random.Range(-200, 200) * Time.deltaTime);

        }
        Destroy(woodObject);
        Instantiate(woodDestr, transform.position, transform.rotation);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void StartGameOver(bool win)
    {
        if (PlayerPrefs.HasKey("ScoreKnives"))
        {
            if (knivesScoreCount > PlayerPrefs.GetInt("ScoreKnives"))
            {
                PlayerPrefs.SetInt("ScoreKnives", knivesScoreCount);
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetInt("ScoreKnives", knivesScoreCount);
            PlayerPrefs.Save();
        }
        StartCoroutine("GameOverCoroutine", win);
    }
    public void KnivesScore()
    {
        knivesScoreCount++;
        GameUI.ShowScore(knivesScoreCount);
    }

    public void AppleHit()
    {
        appleCount++;
        PlayerPrefs.SetInt("AppleScore", appleCount);
        PlayerPrefs.Save();
        GameUI.ShowAppleScore(appleCount);
    }

    public void OnKnifeHit()
    {
        if (knifeCount > 0)
        {
            if (game)
            {
                SpawnKnife();
            }
        }
        else
        {
            game = false;
            StartGameOver(true);
        }
    }
}
