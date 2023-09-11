using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaneController : MonoBehaviour
{
    private PlayerInput inputs;
    private PlayerInput.MobileInputActions inputAction;

    [SerializeField] private AudioSource fireSound;
    [SerializeField] private AudioSource sheildOnSound;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform bulletStartPos;
    [SerializeField] private GameObject powerSheildPref;
    [SerializeField]private float shotCooldown = 1f;
    [SerializeField] private float turnAngle;
    [SerializeField] private float trunSpeed;
    private float lastShotTime = 0f;
    private bool activeShield;

    private void Awake()
    {
        inputs = new PlayerInput();
        inputAction = inputs.MobileInput;
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void FixedUpdate()
    {
        moveUP_and_DOWN(inputAction.Move.ReadValue<float>());
    }

    private void Update()
    {
        shootBullet();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sheild"))
        {
            activeShield = true;
            sheildOnSound.Play();
            powerSheildPref.SetActive(true);
            EventManager.onPowerUP?.Invoke(activeShield);
            StartCoroutine(DeactivateShieldPowerUp());
            collision.gameObject.SetActive(false);
        }
    }

    private IEnumerator DeactivateShieldPowerUp()
    {
        yield return new WaitForSeconds(5f);
        activeShield = false;
        EventManager.onPowerUP?.Invoke(activeShield);
        powerSheildPref.SetActive(false);
    }

    private void moveUP_and_DOWN(float inputValue)
    {
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, -3.65f, 3.5f));
        rb.velocity = new Vector2(0, inputValue * speed * Time.deltaTime);

        float ySpeed = rb.velocity.y;
        Quaternion newRotarion;

        if (ySpeed > 0)
        {
            newRotarion = Quaternion.Euler(0, 0, turnAngle);
        }
        else if (ySpeed < 0)
        {
            newRotarion = Quaternion.Euler(0, 0, -turnAngle);
        }
        else
        {
            newRotarion = Quaternion.Euler(0, 0, 0);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotarion, trunSpeed);
    }

    private void shootBullet()
    {
        if (inputAction.Shoot.triggered && Time.time-lastShotTime>=shotCooldown)
        {
            fireSound.Play();
            anim.SetBool("Shoot1", true);
            GameObject bullet= ObjectPooling.objectpoolInstance.poolObjectsBullet();
            bullet.SetActive(true);
            bullet.transform.rotation = transform.rotation;
            bullet.transform.position = bulletStartPos.position;
            lastShotTime = Time.time;
        }
        else
        {
            anim.SetBool("Shoot1", false);
        }
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }


}
