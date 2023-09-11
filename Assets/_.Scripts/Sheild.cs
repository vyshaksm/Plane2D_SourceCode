using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheild : MonoBehaviour
{
    private void FixedUpdate()
    {
        StartCoroutine(disableDelay());
    }

    private IEnumerator disableDelay()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
