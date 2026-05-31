using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemyManager : MonoBehaviour
{
   public GameObject enemyPrefab; // 敌人预制体
    public GameObject createEnemypoint; // 敌人生成点
    public float createEnemyTime = 0f; // 敌人生成时间
    public float spawnInterval = 2f; // 敌人生成间隔

    public void Start()
    {
        InvokeRepeating("spawn", createEnemyTime, spawnInterval); // 每2秒生成一个敌人
    }
    public void spawn()
    {
        Instantiate(enemyPrefab, createEnemypoint.transform.position, createEnemypoint.transform.rotation); ;
    }
}
