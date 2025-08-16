using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; // 引入Cinemachine命名空间
using TMPro; // 引入TextMeshPro命名空间
using UnityEngine.UI; // 引入UI命名空间
public class MyEnemyAttack : MonoBehaviour
{
    private MyPlayerHealth playerHealth;
    bool isOnRange = false;
    public float timer=0;
    // 引入Cinemachine的冲击源组件
    private Cinemachine.CinemachineImpulseSource MyInpulse;
    public Image Damageimage;

   
    private void Start()
    {
        // 获取CinemachineImpulseSource组件
        MyInpulse = GetComponent<Cinemachine.CinemachineImpulseSource>();
    }

   
    private void Awake()
    {
        playerHealth = GameObject.FindObjectOfType<MyPlayerHealth>();
    }
    // Update is called once per frame
    void Update()
    {
        //如果在范围内，计时器增加
        timer += Time.deltaTime;
        // 如果计时器大于等于1秒，并且玩家没有死亡，则执行攻击
        if (isOnRange && timer >= 1f&& !MyPlayerHealth.isDead)
            Attack();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isOnRange = true;
        }
    }
    // 当玩家离开触发器时，停止攻击，这个很重要不然玩家一直扣血
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isOnRange = false;
            timer = 0; // 重置计时器
        }
    }
    public void Attack()
    {
        //如果玩家已经死亡，就不再攻击,return表示直接跳出这个方法,真好用^_^
        if (MyPlayerHealth.isDead)
            return;
        //wok，终于找到了怎么调用Cinemachine的冲击源
        //记得给敌人添加CinemachineImpulseSource组件
        MyInpulse.GenerateImpulse();

        timer = 0;
            playerHealth.health -= 20;
            
        playerHealth.playerDamage(); //调用玩家受伤方法
        if (playerHealth.health == 0)
        {
            playerHealth.Die();//调用玩家死亡方法
        }
        
    }
}
