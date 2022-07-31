using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Character player;
    public Slider health;
    public Text upIndicator, downIndicator;
    public float speed = 8;
    public int playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        speed = (Screen.height - 400) / 8;
        playerHealth = player.health;
        health.maxValue = playerHealth;
        health.value = playerHealth;
        StartCoroutine(ClearIndicators());
    }

    // Update is called once per frame
    void Update()
    {
        //Keeps the player from moving out of range/screen
        if (transform.localPosition.y > (Screen.height / 4))
            transform.localPosition = new Vector3(transform.localPosition.x, (Screen.height / 4), 0);
        else if (transform.localPosition.y < -(Screen.height / 4))
            transform.localPosition = new Vector3(transform.localPosition.x, -(Screen.height / 4), 0);

        //Player Movements (Up and Down)
        if (Input.GetKey("w") && GameManager.Instance.isGameActive)
            transform.Translate(Vector3.up * speed);
        else if (Input.GetKey("s") && GameManager.Instance.isGameActive)
            transform.Translate(Vector2.down * speed);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered");
        if (collision.CompareTag("Enemy"))
        {
            //play scream sound
            FindObjectOfType<AudioManager>().Play("scream");
            
            if (playerHealth - 1 == 0)
            {
                if (GameManager.Instance.score > PlayerPrefs.GetInt("Highscore"))
                    PlayerPrefs.SetInt("Highscore", GameManager.Instance.score);

                health.gameObject.transform.GetChild(1).GetComponentInChildren<Image>().enabled = false;
                GameManager.Instance.isGameActive = false;
                GameManager.Instance.GameOverScreen.SetActive(true);
            }
            playerHealth -= 1;
            health.value = playerHealth;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Healer") && playerHealth < health.maxValue)
        {
            playerHealth += 1;
            health.value = playerHealth;
            Destroy(collision.gameObject);
        }
    }

    IEnumerator ClearIndicators()
    {
        yield return new WaitForSeconds(4);
        upIndicator.enabled = false;
        downIndicator.enabled = false;
    }

    // Move Up Button
    public void Up()
    {
            if(GameManager.Instance.isGameActive)
            {
                transform.Translate(Vector3.up * speed);
            }
    }

    // Move Down Button
    public void Down()
    {
        if(GameManager.Instance.isGameActive)
        {
            transform.Translate(Vector3.down * speed);
        }
    }
}
