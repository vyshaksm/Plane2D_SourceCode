using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnDistance = 1f;
    [SerializeField] private float[] spawnIntervals = { 1f, 2f, 5f, 7f };
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float acceleration = 0.01f;

    private float[] nextSpawnTimes = new float[4];
    private Camera mainCamera;
    private float currentGameSpeed=1;
    private bool isBoostOn;
    private void Start()
    {
        mainCamera = Camera.main;
        ResetSpawnTimes();
    }

    private void Update()
    {
        currentGameSpeed += 0.01f * Time.deltaTime;
        

        for (int i = 0; i < nextSpawnTimes.Length; i++)
        {
            if (Time.time >= nextSpawnTimes[i])
            {
                SpawnObstacle(i);
                nextSpawnTimes[i] = Time.time + spawnIntervals[i];


                if (isBoostOn)
                {
                    Time.timeScale = currentGameSpeed+1.5f;
                }
                else
                {
                    Time.timeScale = currentGameSpeed;
                }

                if (Time.timeScale >= maxSpeed)
                {
                    Time.timeScale = maxSpeed;
                }
            }
        }
    }

    private void ResetSpawnTimes()
    {
        for (int i = 0; i < nextSpawnTimes.Length; i++)
        {
            nextSpawnTimes[i] = Time.time + spawnIntervals[i];
        }
    }

    private Vector3 SpawnObjPos()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        Vector3 spawnPosition = new Vector3(cameraPosition.x + cameraWidth + spawnDistance, Random.Range(cameraPosition.y - cameraHeight, cameraPosition.y + cameraHeight), 0f);
        return spawnPosition;
    }

    private void SpawnObstacle(int obstacleIndex)
    {
        GameObject obstacle = ObjectPooling.instance.GetObject(obstacleIndex);
        obstacle.SetActive(true);
        obstacle.transform.position = SpawnObjPos();
    }

    public void speedBoostOn()
    {
        currentGameSpeed = Time.timeScale;
        isBoostOn = true;
    }

    public void speedBoostOff()
    {
        isBoostOn = false;
    }

    private void OnEnable()
    {
        EventManager.onSpeedBoostOn += speedBoostOn;
        EventManager.onSpeedBoostOff += speedBoostOff;
    }
    private void OnDisable()
    {
        EventManager.onSpeedBoostOn -= speedBoostOn;
        EventManager.onSpeedBoostOff -= speedBoostOff;
    }

}



