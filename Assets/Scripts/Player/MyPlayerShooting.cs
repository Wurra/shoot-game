using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerShooting : MonoBehaviour
{
    private AudioSource audioSource; // 用于播放射击音效
    private Light light;// 用于射击时的光效
    private LineRenderer lineRenderer; // 用于射击线条效果
    private ParticleSystem particleSystem; // 用于射击时的粒子效果
    //射线检测相关变量
    private Ray shootRay;
    private RaycastHit shootHit; // 用于存储射线检测的结果
    private int shootableMask; // 用于存储可射击的层的掩码

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        light = GetComponent<Light>();
        lineRenderer = GetComponent<LineRenderer>();
        particleSystem = GetComponent<ParticleSystem>();
        //获取射线检测的图层
        shootableMask = LayerMask.GetMask("Shootable");
    }
    public void Update()
    {
        Shoot();

    }
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0)) // 0代表鼠标左键
        {
            audioSource.Play(); // 播放射击音效
            light.enabled = true; // 开启光效
            lineRenderer.enabled = true; // 开启射击线条效果
            lineRenderer.SetPosition(0, transform.position);
            particleSystem.Play(); // 播放射击粒子效果

        }
        if (Input.GetMouseButtonUp(0)) // 0代表鼠标左键
        {
            light.enabled = false; // 关闭光效
            lineRenderer.enabled = false; // 关闭射击线条效果
        }
        //发射射线，检测射击是否击中敌人
        //定义一个Ray，定义一个Mask，定义Hit信息
        //设置Ray的起点和终点
        shootRay.origin = transform.position; // 射线起点为玩家位置
        shootRay.direction = transform.forward; // 射线方向为玩家前方
        //这里我曾经试过以下被注释的代码来完成，可是会出现敌人被击中后，光束穿透敌人的效果
        //原因是没有保证无论如何都更新射线的终点
        //if (Physics.Raycast(shootRay, out shootHit, 100, shootableMask)&& Input.GetMouseButtonUp(0))
        //{
        //    lineRenderer.SetPosition(1, shootHit.point);
        //    MyEnemyHealth enemyHealth = shootHit.collider.GetComponent<MyEnemyHealth>();
        //    enemyHealth.takedamage(10);
        //}
            if (Physics.Raycast(shootRay, out shootHit, 100, shootableMask))
        {
            lineRenderer.SetPosition(1, shootHit.point);
           if(Input.GetMouseButtonUp(0))
            {
                // 如果射线击中目标，调用目标的受伤方法
                // 这里假设敌人有一个MyEnemyHealth脚本，负责处理受伤逻辑
                //这里比较难理解，建议多看两遍
                MyEnemyHealth enemyHealth = shootHit.collider.GetComponent<MyEnemyHealth>();
                enemyHealth.takedamage(20,shootHit.point);
                
            }
        }
        else
        {
            lineRenderer.SetPosition(1, transform.position + transform.forward * 100);
        }

    }

}
