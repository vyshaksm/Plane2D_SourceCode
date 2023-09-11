using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnDistance = 1f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float accelaration = 0.3f;

    private float nextSpawnTime;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        nextSpawnTime = Time.time;
    }

    private void FixedUpdate()
    {

        if (Time.time >= nextSpawnTime)
        {
            StartCoroutine(SpawnObjecMissile());
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private IEnumerator SpawnObjecMissile()
    {
        yield return new WaitForSeconds(2f);

        Vector3 cameraPosition = mainCamera.transform.position;
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        Vector3 spawnPosition = new Vector3(cameraPosition.x + cameraWidth + spawnDistance, Random.Range(cameraPosition.y - cameraHeight, cameraPosition.y + cameraHeight), 0f);

        GameObject stars = ObjectPooling.objectpoolInstance.poolObjectStar();
        stars.SetActive(true);
        stars.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
        stars.transform.position = spawnPosition;
        stars.transform.rotation = transform.rotation;

        speed += accelaration;

        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
            spawnInterval = 0.35f;
        }
        else if (speed > 10f)
        {
            spawnInterval = 0.45f;
        }
    }
}
