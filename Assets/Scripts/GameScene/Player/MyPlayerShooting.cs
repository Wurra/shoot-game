using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

//长按是连发并且会造成伤害

public class MyPlayerShooting : MonoBehaviour
{
    [Header("连发相关参数")]
    //射击间隔时间
    public float timeBetweenBullets;
    //射击间隔时间计数器的速度
    public float timeCounterSpeed;
    //射击间隔时间计数器
    private float timeCounter;
    //是否可以射击
    private bool couldShoot;


    public AudioSource audioSource; // 用于播放射击音效
    private Light gunLight;// 用于射击时的光效
    private LineRenderer lineRenderer; // 用于射击线条效果
    private ParticleSystem gunParticles; // 用于射击时的粒子效果
    public float shootLightEffectPercentage;//射击枪光在射击间隔时间内可以存在的时间占比
    
    //射线检测相关变量
    private Ray shootRay;
    private RaycastHit shootHit; // 用于存储射线检测的结果
    private int shootableMask; // 用于存储可射击的层的掩码


    public MyEnemyAttack enemyAttack; // 引用敌人的攻击脚本，以便在射击时调用敌人的受伤方法
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        lineRenderer = GetComponent<LineRenderer>();
        gunParticles = GetComponent<ParticleSystem>();
        //获取射线检测的图层
        shootableMask = LayerMask.GetMask("Shootable");
    }
    public void Update()
    {
        //如果按下鼠标左键，并且可以射击，执行射击方法
        if (Input.GetMouseButton(0))
        {
            if (couldShoot)
            {
                Shoot();
            }
        }
        //如果射击间隔时间计数器达到射击间隔时间的百分比，说明光效持续时间已经结束，关闭光效
        if (timeCounter >= timeBetweenBullets * shootLightEffectPercentage)
        {
            ShootFinish();
        }
        //如果不能射击，说明正在射击间隔时间内，继续计时
        if (!couldShoot)
            TimeCounter();
    }
    private void Shoot()
    {
        couldShoot = false;
        timeCounter = 0f; // 在射击时重置计时器
        gunLight.enabled = true;
        lineRenderer.enabled = true; // 在射击时启用子弹射线

        
        audioSource.Play(); // 播放射击音效
        gunParticles.Play(); // 播放射击粒子效果

        //发射射线，检测射击是否击中敌人
        //定义一个Ray，定义一个Mask，定义Hit信息
        //设置Ray的起点和终点
        shootRay.origin = transform.position; // 射线起点为玩家位置
        shootRay.direction = transform.forward; // 射线方向为玩家前方
        //子弹射线起点
        lineRenderer.SetPosition(0, transform.position);
        #region 射线检测逻辑（错误版本）
        //这里我曾经试过以下被注释的代码来完成，可是会出现敌人被击中后，光束穿透敌人的效果
        //原因是没有保证无论如何都更新射线的终点
        //if (Physics.Raycast(shootRay, out shootHit, 100, shootableMask)&& Input.GetMouseButtonUp(0))
        //{
        //    lineRenderer.SetPosition(1, shootHit.point);
        //    MyEnemyHealth enemyHealth = shootHit.collider.GetComponent<MyEnemyHealth>();
        //    enemyHealth.takedamage(10);
        //}
        #endregion
        //子弹射线终点
        if (Physics.Raycast(shootRay, out shootHit, 100, shootableMask))
        {
            lineRenderer.SetPosition(1, shootHit.point);
           // 如果射线击中目标，调用目标的受伤方法
           // 这里假设敌人有一个MyEnemyHealth脚本，负责处理受伤逻辑
           //这里比较难理解，建议多看两遍
            MyEnemyHealth enemyHealth = shootHit.collider.GetComponent<MyEnemyHealth>();
                enemyHealth.takedamage(30,shootHit.point);
                
        }
        else
        {
            // 如果射线没有击中任何目标，设置射线的终点为一条长线
            lineRenderer.SetPosition(1, transform.position + transform.forward * 100);
        }

    }
    void ShootFinish()
    {
        gunLight.enabled = false;
        lineRenderer.enabled = false; // 在这里禁用子弹射线
    }
    //计时器方法，控制射击间隔时间和光效持续时间
    private void TimeCounter()
    {
        timeCounter+=timeCounterSpeed*Time.deltaTime;
        if (timeCounter >= timeBetweenBullets)
        { 
            couldShoot = true; 
        }
    }
    
}
