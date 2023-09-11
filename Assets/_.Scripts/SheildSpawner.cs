using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildSpawner : MonoBehaviour
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
            StartCoroutine(SpawnObjectSheild());
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private IEnumerator SpawnObjectSheild()
    {
        yield return new WaitForSeconds(5f);

        Vector3 cameraPosition = mainCamera.transform.position;
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        Vector3 spawnPosition = new Vector3(cameraPosition.x + cameraWidth + spawnDistance, Random.Range(cameraPosition.y - cameraHeight, cameraPosition.y + cameraHeight), 0f);

        GameObject sheild = ObjectPooling.objectpoolInstance.poolObjectSheild();
        sheild.SetActive(true);
        sheild.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
        sheild.transform.position = spawnPosition;
        sheild.transform.rotation = transform.rotation;

        speed += accelaration;
    }
}
