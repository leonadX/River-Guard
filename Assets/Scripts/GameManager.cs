using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { set; get; }

    public GameObject[] FishPrefabs;
    public GameObject parentCanvas;
    public GameObject GameOverScreen;
    public GameObject player;
    public GameObject bullet;
    public Text scoreText;

    private float spawnPositionY;
    private int enemyIndex;
    public float lowerLimit, commonDifference;
    public int score;
    public float speedMultiplier;
    public bool isGameActive;

    private void Awake()
    {
        speedMultiplier = 1;
        Instance = this; 
    }

    private void Start()
    {
        // play theme music
        FindObjectOfType<AudioManager>().Play("themeMusic");
    }

    // Update is called once per frame
    void Update()
    {
        //For shooting
        if (Input.GetKeyDown("space") && isGameActive)
        {
            Instantiate(bullet, new Vector3(player.GetComponent<RectTransform>().position.x * 2 + 20, player.transform.position.y, 0), transform.rotation, parentCanvas.transform);

            // play gunShot sound
            FindObjectOfType<AudioManager>().Play("gunShot");
        }
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Replay()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void UpdateScore(int pointToAdd)
    {
        score += pointToAdd;
        scoreText.text = "Score: " + score.ToString();
    } 

    IEnumerator SpawnEnemyCountdown()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(3);
            enemyIndex = Random.Range(0, FishPrefabs.Length);

            if (enemyIndex == FishPrefabs.Length - 1 && Random.Range(0, 3) != 1)
                enemyIndex = Random.Range(0, FishPrefabs.Length - 1);

            lowerLimit = (Screen.height / 2) - 200;
            commonDifference = (lowerLimit * 2) / 3;
            spawnPositionY = -lowerLimit + commonDifference * Random.Range(0, 4);
            Instantiate(FishPrefabs[enemyIndex], new Vector3(Screen.width, spawnPositionY, 0) + parentCanvas.transform.position, transform.rotation, parentCanvas.transform);
        }
    }

    IEnumerator IncreaseSpeed()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(20);
            speedMultiplier *= 1.15f;
        }
    }

    public void SelectCharacter(Sprite playerCharacter)
    {
        GameManager.Instance.player.GetComponent<Image>().sprite = playerCharacter;
        isGameActive = true;
        StartCoroutine(SpawnEnemyCountdown());
        StartCoroutine(IncreaseSpeed());
    }

    public void ContinueGame()
    {
        isGameActive = true;
        StartCoroutine(SpawnEnemyCountdown());
        StartCoroutine(IncreaseSpeed());
    }

    // Shooting Button
    public void Shoot()
    {
        if(isGameActive)
        {
            Instantiate(bullet, new Vector3(player.GetComponent<RectTransform>().position.x * 2 + 20, player.transform.position.y, 0), transform.rotation, parentCanvas.transform);

            // play gunShot sound
            FindObjectOfType<AudioManager>().Play("gunShot");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

}
