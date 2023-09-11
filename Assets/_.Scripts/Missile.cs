using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioSource explodeAudio;
    [SerializeField] private float disableTime;
    [SerializeField] private GameObject explossionPref;

    private const int starCount = 5;

    private void FixedUpdate()
    {
        StartCoroutine(disableDelay());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            GameObject explosion = Instantiate(explossionPref, collision.transform.position, Quaternion.identity);
            Destroy(explosion, 0.75f);
            StartCoroutine(GameEndDelay());
        }

        if (collision.CompareTag("Bullet"))
        {
            EventManager.onStarCollect?.Invoke(starCount);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator GameEndDelay()
    {
        explodeAudio.Play();
        yield return new WaitForSeconds(0.5f);
        EventManager.onGameOver?.Invoke();
    }

    private IEnumerator disableDelay()
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.SetActive(false);
    }

    public void RemoveAndEnableCollider(bool state)
    {
        GetComponent<CapsuleCollider2D>().enabled = !state;
    }


    private void OnEnable()
    {
        EventManager.onPowerUP += RemoveAndEnableCollider;
    }

    private void OnDisable()
    {
        EventManager.onPowerUP -= RemoveAndEnableCollider;
    }
}
