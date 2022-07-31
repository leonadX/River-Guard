using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public Enemy enemy;
    public int enemyHealth;
    public Slider health;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = enemy.health;
        health.maxValue = enemyHealth;
        health.value = enemyHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameManager.Instance.isGameActive)
            transform.Translate(Vector3.left * enemy.speed * GameManager.Instance.speedMultiplier);

        if (transform.localPosition.x < -Screen.width)
            Destroy(gameObject);
    }
}
