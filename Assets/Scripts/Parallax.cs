using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    //public GameObject cam;
    public float parallaxEffect;

    private void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isGameActive)
        {
            float temp = transform.position.x;
            transform.Translate(Vector3.left * (1 - parallaxEffect) * 0.05f * GameManager.Instance.speedMultiplier);
            if (temp < startpos - length) transform.position = new Vector3(startpos, transform.position.y, transform.position.z);
        }
    }
}
