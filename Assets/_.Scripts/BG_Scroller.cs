using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Scroller : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float accelaration = 0.3f;

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

        speed += accelaration;

        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }
    }
}
