using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling objectpoolInstance;

    private List<GameObject> pooledBullets;
    private List<GameObject> pooledMissiles;
    private List<GameObject> pooledStars;
    private List<GameObject> pooledSheild;
    [SerializeField] GameObject fireBullet;
    [SerializeField] GameObject missile;
    [SerializeField] GameObject star;
    [SerializeField] GameObject sheild;
    [SerializeField] private int bulletCount;
    [SerializeField] private int missileCount;
    [SerializeField] private int starCount;
    [SerializeField] private int sheildCount;
    [SerializeField] private Transform bulletParent;
    [SerializeField] private Transform missileParent;
    [SerializeField] private Transform starParent;
    [SerializeField] private Transform sheildParent;
   

    private void Start()
    {
        objectpoolInstance = this;

        pooledBullets = new List<GameObject>();
        GameObject bullet;
        for(int i = 0; i < bulletCount; i++)
        {
            bullet = Instantiate(fireBullet,bulletParent);
            bullet.SetActive(false);
            pooledBullets.Add(bullet);
        }

        pooledMissiles = new List<GameObject>();
        GameObject mis;
        for (int i = 0; i < missileCount; i++)
        {
            mis = Instantiate(missile,missileParent);
            mis.SetActive(false);
            pooledMissiles.Add(mis);
        }

        pooledStars = new List<GameObject>();
        GameObject st;
        for(int i = 0; i < starCount; i++)
        {
            st = Instantiate(star, starParent);
            st.SetActive(false);
            pooledStars.Add(st);
        }

        pooledSheild = new List<GameObject>();
        for(int i = 0; i < sheildCount; i++)
        {
            GameObject shi = Instantiate(sheild, starParent);
            shi.SetActive(false);
            pooledSheild.Add(shi);
        }
    }

    public GameObject poolObjectsBullet()
    {
        for(int i = 0; i < bulletCount; i++)
        {
            if (!pooledBullets[i].activeInHierarchy)
            {
                return pooledBullets[i];
            }
        }
        return null;
    }

    public GameObject poolObjectMissile()
    {
        for (int i = 0; i < missileCount; i++)
        {
            if (!pooledMissiles[i].activeInHierarchy)
            {
                return pooledMissiles[i];
            }
        }
        return null;
    }

    public GameObject poolObjectStar()
    {
        for(int i = 0; i < starCount; i++)
        {
            if (!pooledStars[i].activeInHierarchy)
            {
                return pooledStars[i];
            }
        }
        return null;
    }

    public GameObject poolObjectSheild()
    {
        for(int i = 0; i < sheildCount; i++)
        {
            if (!pooledSheild[i].activeInHierarchy)
            {
                return pooledSheild[i];
            }
        }
        return null;
    }

}
