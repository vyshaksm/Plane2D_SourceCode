using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval; 
    [SerializeField] private float spawnDistance = 1f;
    [SerializeField] private float speed=5f;
    [SerializeField] private float maxSpeed=20f;
    [SerializeField] private float accelaration=0.3f;

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
            SpawnObjecMissile();
            SpawnObjectStar();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnObjecMissile()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        Vector3 spawnPosition = new Vector3(cameraPosition.x + cameraWidth + spawnDistance, Random.Range(cameraPosition.y - cameraHeight, cameraPosition.y + cameraHeight), 0f);

        GameObject missiles = ObjectPooling.objectpoolInstance.poolObjectMissile();
        missiles.SetActive(true);
        missiles.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
        missiles.transform.position = spawnPosition;
        missiles.transform.rotation = transform.rotation;

        speed += accelaration ;
        
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
    private void SpawnObjectStar()
    {
        //GameObject star = ObjectPooling.objectpoolInstance.poolObjectsAsteroid();
        //star.SetActive(true);
        //star.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
        //star.transform.position = spawnPosition;
        //star.transform.rotation = transform.rotation;

        //speed += accelaration;

        //if (speed >= maxSpeed)
        //{
        //    speed = maxSpeed;
        //    spawnInterval = 0.35f;
        //}
        //else if (speed > 10f)
        //{
        //    spawnInterval = 0.45f;
        //}
    }
}



