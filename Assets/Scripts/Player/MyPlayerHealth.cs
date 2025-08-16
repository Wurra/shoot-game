using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MyPlayerHealth : MonoBehaviour
{
    private Animator playerami;
    public AudioClip Deathvoice;
    private AudioSource audioSource;
    private PlayerMovement playerMovement; // 假设有一个玩家移动脚本
    public TextMeshProUGUI healthText; // 用于显示玩家当前生命值的文本
    public static bool isDead = false;

    [SerializeField]
    private int _health = 100; // 私有健康值变量

    // 公共属性，确保健康值在0-100之间
    public int health
    {
        get { return _health; }
        set { _health = Mathf.Clamp(value, 0, 100); } // 使用Mathf.Clamp限制值在0-100之间
    }

    private void Awake()
    {
        playerami = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>(); // 获取玩家移动脚本
        

    }
     
   
    public void Die()
    {
        // 检查是否已经死亡，避免重复执行死亡逻辑
        //这里非常重要，避免重复播放死亡动画和音效
        if (isDead)
            return;
        //播放死亡动画
        if (health <= 0)
        {
            isDead = true; // 设置死亡状态
            playerami.SetTrigger("PlayerDeath");
            audioSource.clip = Deathvoice;
            audioSource.Play();
            playerMovement.enabled = false; // 禁用玩家移动脚本
            //使用协程等待动画播放完毕
            StartCoroutine(DelayedReset());
        }
    }
    public void playerDamage()
    {
        healthText.text = health.ToString(); // 更新UI文本显示当前生命值
        if (health > 0 && health < 100)
        {
            audioSource.Play();
            
        }
    }
    private IEnumerator DelayedReset()
    {
        // 等待动画播放完成（根据实际动画长度调整时间）
        yield return new WaitForSeconds(3f);
        Reset(100);
    }
    public void Reset(int health)
    {
        this.health = health; // 重置玩家生命值
        SceneManager.LoadScene(0); // 重新加载当前场景
        playerMovement.enabled = true; // 重新启用玩家移动脚本

        Debug.Log("Player has been reset with health: " + health);
    }
    public void RestartLevel()
    {
 
    }
   



}
