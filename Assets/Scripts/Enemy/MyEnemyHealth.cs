using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class MyEnemyHealth : MonoBehaviour
{
    public int currenthealth = 100;
    private AudioSource audioSource; // 用于播放敌人受伤音效
    private ParticleSystem enemyParticles;
    public AudioClip deathclip; // 用于播放敌人死亡音效
    private Animator animator;
   // public GameObject Text; // 用于生成伤害文本的预制体
    public GameObject Text; // 用于生成伤害文本的预制体         

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>(); // 获取AudioSource组件
        //这个获取方法是获取子物体的组件，因为脚本所在的父物体没有ParticleSystem组件
        enemyParticles = GetComponentInChildren<ParticleSystem>();
        animator = GetComponent<Animator>(); // 获取Animator组件
    }
    public void takedamage(int amount,Vector3 hitpoint)
    {
        currenthealth -= amount; // 每次受到伤害减少amount点生命值
        audioSource.Play();// 播放受伤音效
        enemyParticles.transform.position = hitpoint; // 设置粒子效果的位置为击中点
        enemyParticles.Play(); // 播放受伤粒子效果

        //generateText();

        if (currenthealth <= 0)
        {
            Die(); // 如果生命值小于等于0，调用死亡方法
        }
    }
    public void Die()
    {
        //播放死亡音效
        audioSource.clip = deathclip; // 设置死亡音效
        audioSource.Play();
        //播放死亡动画
        animator.SetTrigger("Death"); // 设置死亡动画触发器
        //销毁敌人对象
        Destroy(gameObject, 1f); // 延迟1秒后销毁敌人对象
        //击杀分数增加
        MyplayerScore.score += 10; // 增加10分
    }
    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false; // 禁用导航网格代理
    }
   public void generateText()
    {
        //该脚本要挂载都敌人组件上，在敌人的血条改变或者受到伤害时调用该方法
        float rand = Random.Range(-2, 2);//随机-2~2的值

        Vector3 new_position = transform.position;//将受到攻击的敌人的位置转换为Vector3

        //在位置上生成文本对象
        new_position.x += rand;
        GameObject gameObject = Instantiate(Text, new_position, Quaternion.identity);

        //给文本对象中的文本赋值，显示数值
        gameObject.GetComponentInChildren<Hurt_Text>().Init(20);
    }
}
