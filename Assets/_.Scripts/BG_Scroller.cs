using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Scroller : MonoBehaviour
{
     public float speed;
    private Vector3 startPosistion;

    private void Start()
    {
        startPosistion = transform.position;
    }

    private void FixedUpdate()
    {
        transform.Translate(-Vector3.right*speed*Time.deltaTime);
        if (transform.position.x < -21.4f)
        {
            transform.position = startPosistion;
        }
    }
}
