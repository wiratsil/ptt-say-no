using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;


public class SayNo_GameManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void OpenInTab(string url);

    public GameObject player;
    public Collider2D colliderBound;
    public GameObject result;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float timer;
    [SerializeField]
    private int lifePoint = 3;
    [Space]
    public List<GameObject> rightItemList;
    public List<GameObject> wrongItemList;
    public List<GameObject> bonusItemList;
    public List<GameObject> lifePointList;
    [Space]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI resultText;
    enum ItemType
    {
        Right,Wrong,Bonus
    }
    [Space]
    [SerializeField]
    float rightItemSpeed = 3;
    [SerializeField]
    float wrongItemSpeed = 3;
    [SerializeField]
    float bonusItemSpeed = 3;
    [SerializeField]
    bool playable = false;
    [SerializeField]
    int score;

    float horizoltal;

    // Start is called before the first frame update
    void Start()
    {
        _StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (playable && timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = Mathf.CeilToInt(timer).ToString();
        }
        else if (timer <= 0 && playable)
        {
            playable = false;
            Result();
        }
        if (lifePoint <= 0 && playable)
        {
            playable = false;
            Result();
        }
        //Movement player
        if (Input.GetMouseButton(0) && playable)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            player.transform.position = new Vector3(mousePos.x, player.transform.position.y, player.transform.position.z);

            if (player.transform.position.x <= colliderBound.bounds.min.x)
            {
                player.transform.position = new Vector3(colliderBound.bounds.min.x, player.transform.position.y, player.transform.position.z);
            }
            else if (player.transform.position.x >= colliderBound.bounds.max.x)
            {
                player.transform.position = new Vector3 (colliderBound.bounds.max.x , player.transform.position.y, player.transform.position.z);
            }


            if (mousePos.x < 0 )
            {
                //if (mousePos.x <= colliderBound.bounds.min.x)
                //    return;

                    //player.transform.position += Vector3.left * speed;
            }
            else
            {
                //if (mousePos.x >= colliderBound.bounds.max.x)
                //    return;

                    //player.transform.position += Vector3.right * speed;
            }
        }
        horizoltal = Input.GetAxis("Horizontal");
        if (horizoltal < 0 && playable)
        {
            if (player.transform.position.x <= colliderBound.bounds.min.x)
                return;

            player.transform.position += Vector3.left * speed;
        }
        else if (horizoltal > 0 && playable)
        {
            if (player.transform.position.x >= colliderBound.bounds.max.x)
                return;

            player.transform.position += Vector3.right * speed;
        }
    }

    public void _StartGame()
    {
        playable = true;
        StartCoroutine(SpawnItem(ItemType.Right, 1, 2, rightItemSpeed));
        StartCoroutine(SpawnItem(ItemType.Wrong, 1, 3, wrongItemSpeed));
        StartCoroutine(SpawnItem(ItemType.Bonus, 6, 8, bonusItemSpeed));
    }

    public void AddScore(string tag)
    {
        if (!playable)
            return;

        if (tag == "RightItem")
        {
            score ++;
        }
        else if (tag == "WrongItem")
        {
            score --;
            lifePoint --;
            lifePointList[lifePointList.Count - 1].transform.GetChild(0).gameObject.SetActive(false) ;
            lifePointList.RemoveAt(lifePointList.Count - 1);
        }
        else if (tag == "BonusItem")
        {
            score += 5;
        }
        scoreText.text = score.ToString();
        resultText.text = score.ToString();
    }

    IEnumerator SpawnItem(ItemType itemType, float timeMin, float timeMax, float speed)
    {
        yield return new WaitForSeconds(Random.Range(timeMin, timeMax));

        int indexItem = -1;
        GameObject clone = null;
        switch (itemType)
        {
            case ItemType.Right:
                indexItem = Random.Range(0, rightItemList.Count);
                clone = Instantiate(rightItemList[indexItem], RandomInBound(), Quaternion.identity);
                break;
            case ItemType.Wrong:
                indexItem = Random.Range(0, wrongItemList.Count);
                clone = Instantiate(wrongItemList[indexItem], RandomInBound(), Quaternion.identity);
                break;
            case ItemType.Bonus:
                indexItem = Random.Range(0, bonusItemList.Count);
                clone = Instantiate(bonusItemList[indexItem], RandomInBound(), Quaternion.identity);
                break;
        }

        clone.transform.DOMoveY(-10, speed, snapping: false).OnStepComplete(() => Destroy(clone));
        StartCoroutine(SpawnItem(itemType, timeMin, timeMax, speed));
    }

    //Random posintion in bounds
    Vector2 RandomInBound()
    {
        Vector2 vector2 = new Vector2(Random.RandomRange(colliderBound.bounds.min.x, colliderBound.bounds.max.x), 4);
        return vector2;
    }

    void Result()
    {
        result.SetActive(true);
        API_AddScore.Instance.AddScore(score);
    }

    public void _ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void _OpenUrl(string url)
    {
        //Application.OpenURL(url);
        OpenInTab(url);
    }
}
