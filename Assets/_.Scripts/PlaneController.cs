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
    [SerializeField] private AudioSource explodeAudio;
    [SerializeField] private float disableTime;
    [SerializeField] private GameObject explossionPref;
    private float lastShotTime = 0f;
    private bool activeShield;
    private int boostCount=0;


    [SerializeField] private BG_Scroller scroller;
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
            StartCoroutine(DeactivateShieldPowerUp());
            collision.gameObject.SetActive(false);
        }

        if (collision.CompareTag("Missile"))
        {
            if (activeShield)
            {
                return;
            }
            GetComponent<SpriteRenderer>().enabled = false;
            GameObject explosion = Instantiate(explossionPref, transform.position, Quaternion.identity);
            Destroy(explosion, 0.5f);
            StartCoroutine(GameEndDelay());
        }


        if (collision.CompareTag("Speed"))
        {
            if (boostCount > 0)
            {
                return;
            }
            EventManager.onSpeedBoostOn?.Invoke();
            boostCount++;
            StartCoroutine(DeactivateBoost());
        }
    }

    private IEnumerator DeactivateShieldPowerUp()
    {
        yield return new WaitForSeconds(5f);
        activeShield = false;
        powerSheildPref.SetActive(false);
    }

    private IEnumerator GameEndDelay()
    {
        explodeAudio.Play();
        yield return new WaitForSeconds(0.5f);
        EventManager.onGameOver?.Invoke();
    }

    public IEnumerator DeactivateBoost()
    {
        yield return new WaitForSeconds(10f);
        boostCount = 0;
        EventManager.onSpeedBoostOff?.Invoke();
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
            GameObject bullet = ObjectPooling.instance.GetObject(4);
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
