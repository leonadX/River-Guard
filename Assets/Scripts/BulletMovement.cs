using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameActive)
            transform.Translate(Vector3.right * 8 * GameManager.Instance.speedMultiplier);

        if (transform.localPosition.x > (Screen.width * 3) / 4)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<EnemyMovement>().enemyHealth -= 1;
        collision.gameObject.GetComponent<EnemyMovement>().health.value = collision.gameObject.GetComponent<EnemyMovement>().enemyHealth;

        if (collision.CompareTag("Enemy") && collision.gameObject.GetComponent<EnemyMovement>().enemyHealth == 0)
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GameManager.Instance.UpdateScore(collision.gameObject.GetComponent<EnemyMovement>().enemy.point);
        }else if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
