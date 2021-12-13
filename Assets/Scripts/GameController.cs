using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static Vector2 bottomLeft;
    public static Vector2 topRight;
    public static GameController Instance { get { return _instance; } }
    private static GameController _instance;

    public Player player;
    public GameObject[] itemPointsSpecial ,itemPoints, itemGifts;
    public GameObject wave, life1, life2, life3, deathLife1, deathLife2, deathLife3;
    public GameOverBehavior GameOverBehavior;

    private float maxPlayTime = 60f;
    private bool timerIsRunning = false;
    private bool isAlive = true;
    public Text timeText, playerPoints;
    public int points = 0,maxLife = 3;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        SetupStartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive)
        {
            TimeStoper();
            DisplayPoint(points);
            CheckLife(); 
        }
        
        
    }

    public void SetupStartGame()
    {
        Instantiate(player);
        StartCoroutine(SpawnPointItems());
        StartCoroutine(SpawnSpacialPointItems());
        StartCoroutine(SpawnGiftItems());
        timerIsRunning = true;
        timeText.gameObject.SetActive(true);
        playerPoints.gameObject.SetActive(true); 
        life1.gameObject.SetActive(true);
        life2.gameObject.SetActive(true);
        life3.gameObject.SetActive(true);
        deathLife1.gameObject.SetActive(false);
        deathLife2.gameObject.SetActive(false);
        deathLife3.gameObject.SetActive(false);

    }

    IEnumerator SpawnPointItems() 
    {
       
        yield return new WaitForSeconds(Random.Range(1,2));
        
        int randomNum = Random.Range(0, itemPoints.Length);
        GameObject coinWave = Instantiate(wave, Vector2.zero, Quaternion.identity, transform);
        float rnd = Random.Range(0.5f, 5f);
        float x = rnd/5;
        Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * x, Screen.height));
        pos += Vector2.up * itemPoints[randomNum].transform.localScale.y;
        Instantiate(itemPoints[randomNum], pos , Quaternion.identity, coinWave.transform);

        StartCoroutine(SpawnPointItems());
    }

    IEnumerator SpawnSpacialPointItems() 
    {
       
        yield return new WaitForSeconds(Random.Range(6,8));
        
        int randomNum = Random.Range(0, itemPointsSpecial.Length);
        GameObject coinWave = Instantiate(wave, Vector2.zero, Quaternion.identity, transform);
        float rnd = Random.Range(0.5f, 5f);
        float x = rnd/5;
        Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * x, Screen.height));
        pos += Vector2.up * itemPointsSpecial[randomNum].transform.localScale.y;
        Instantiate(itemPointsSpecial[randomNum], pos , Quaternion.identity, coinWave.transform);

        StartCoroutine(SpawnSpacialPointItems());
    }

    IEnumerator SpawnGiftItems() 
    {
       
        yield return new WaitForSeconds(Random.Range(1,3));
        
        int randomNum = Random.Range(0, itemGifts.Length);
        GameObject coinWave = Instantiate(wave, Vector2.zero, Quaternion.identity, transform);
        float rnd = Random.Range(0.5f, 5f);
        float x = rnd/5;
        Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * x, Screen.height));
        pos += Vector2.up * itemGifts[randomNum].transform.localScale.y;
        Instantiate(itemGifts[randomNum], pos , Quaternion.identity, coinWave.transform);

        StartCoroutine(SpawnGiftItems());
    }

    public void GameOver()
    {
        GameOverBehavior.EndGame();
    }

    void TimeStoper(){
        if (timerIsRunning)
        {
            if (maxPlayTime > 0)
            {
                maxPlayTime -= Time.deltaTime;
                DisplayTime(maxPlayTime);
            }
            else
            {
                maxPlayTime = 0;
                timerIsRunning = false;
                GameOver();
            }
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{00}", seconds);
    }

    private void DisplayPoint(int points)
    {
        playerPoints.text = string.Format("{0}", points);
    }

    void CheckLife()
    {
        if(isAlive){
            switch (maxLife)
            {
                case 3:
                life1.gameObject.SetActive(true);
                life2.gameObject.SetActive(true);
                life3.gameObject.SetActive(true);
                deathLife1.gameObject.SetActive(false);
                deathLife2.gameObject.SetActive(false);
                deathLife3.gameObject.SetActive(false);
                break;
                case 2:
                life1.gameObject.SetActive(true);
                life2.gameObject.SetActive(true);
                life3.gameObject.SetActive(false);
                deathLife1.gameObject.SetActive(false);
                deathLife2.gameObject.SetActive(false);
                deathLife3.gameObject.SetActive(true);
                break;
                case 1:
                life1.gameObject.SetActive(true);
                life2.gameObject.SetActive(false);
                life3.gameObject.SetActive(false);
                deathLife1.gameObject.SetActive(false);
                deathLife2.gameObject.SetActive(true);
                deathLife3.gameObject.SetActive(true);
                break;
                case 0:
                life1.gameObject.SetActive(false);
                life2.gameObject.SetActive(false);
                life3.gameObject.SetActive(false);
                deathLife1.gameObject.SetActive(true);
                deathLife2.gameObject.SetActive(true);
                deathLife3.gameObject.SetActive(true);
                isAlive = false;
                GameOver();
                break;
            }
        }
    }
}
