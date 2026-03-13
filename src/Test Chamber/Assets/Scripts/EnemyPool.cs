using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool instance;

    public GameObject basicEnemy;
    public GameObject rangedEnemy;
    public GameObject tankEnemy;

    public int startSize = 20;

    private Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();

    void Awake()
    {
        instance = this;

        CreatePool("basic", basicEnemy);
        CreatePool("ranged", rangedEnemy);
        CreatePool("tank", tankEnemy);
    }

    void CreatePool(string key, GameObject prefab)
    {
        List<GameObject> pool = new List<GameObject>();

        for (int i = 0; i < startSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Add(obj);
        }

        pools[key] = pool;
    }

    public GameObject GetEnemy(string type)
    {
        List<GameObject> pool = pools[type];

        foreach (GameObject enemy in pool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }

        GameObject newEnemy = Instantiate(GetPrefab(type));
        pool.Add(newEnemy);
        return newEnemy;
    }

    GameObject GetPrefab(string type)
    {
        if (type == "basic") return basicEnemy;
        if (type == "ranged") return rangedEnemy;
        return tankEnemy;
    }
}
