using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyEnemyMovement : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent navMeshAgent; // 导航网格代理，用于敌人的移动
    private Animator animator;
    private MyEnemyHealth enemyHealth; // 敌人的生命脚本
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 假设玩家有一个标签为"Player"
        navMeshAgent = GetComponent<NavMeshAgent>(); // 获取导航网格代理组件
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<MyEnemyHealth>(); // 获取敌人的生命脚本
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth.currenthealth>0)
        navMeshAgent.SetDestination(player.transform.position); // 设置敌人的目标位置为玩家的位置
        Animating(); // 调用动画方法
        

    }
    public void Animating()
    {
        if (navMeshAgent.velocity.magnitude > 0.1f) // 检查敌人的速度是否大于一个小阈值
        {
            // 如果敌人正在移动  
            animator.SetBool("iswalking", true);
        }
        else
        {
            // 如果敌人停止移动  
            animator.SetBool("iswalking", false);
        }
    }
}
