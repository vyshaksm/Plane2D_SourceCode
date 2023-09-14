using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacles : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Obstacles obj;
    private int starCount;
    private float speed;

    private void Awake()
    {
        speed = obj.speed;
        starCount = obj.point;
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(-speed, 0);
        StartCoroutine(disableDelay());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EventManager.onStarCollect?.Invoke(starCount);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator disableDelay()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
