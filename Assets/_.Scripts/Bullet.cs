using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float disableTime;
    [SerializeField] private GameObject explossionPref;

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * Time.deltaTime, 0);
        StartCoroutine(disableDelay());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            gameObject.SetActive(false);
            GameObject explosion = Instantiate(explossionPref, collision.transform.position, Quaternion.identity);
            Destroy(explosion, 0.75f);
        }
    }

    private IEnumerator disableDelay()
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.SetActive(false);
    }
}
