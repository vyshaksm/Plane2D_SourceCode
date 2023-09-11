using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private const int starCount= 10;

    private void FixedUpdate()
    {
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

    private  IEnumerator disableDelay()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
