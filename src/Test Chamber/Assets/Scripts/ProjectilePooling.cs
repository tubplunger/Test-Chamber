using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooling : MonoBehaviour
{
    public static ProjectilePooling instance;

    public GameObject projectilePrefab;
    public int startSize = 40;

    private List<GameObject> pool = new List<GameObject>();

    void Awake()
    {
        instance = this;

        for (int i = 0; i < startSize; i++)
        {
            GameObject obj = Instantiate(projectilePrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetProjectile()
    {
        foreach (GameObject proj in pool)
        {
            if (!proj.activeInHierarchy)
            {
                proj.SetActive(true);
                return proj;
            }
        }

        GameObject newProj = Instantiate(projectilePrefab);
        pool.Add(newProj);
        return newProj;
    }
}
