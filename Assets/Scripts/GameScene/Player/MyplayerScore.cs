using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MyplayerScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // 用于显示分数的文本
    
    public static int score = 0; // 静态变量，用于存储分数

    // 新增一个公共的静态方法来重置分数
    public static void ResetScore()
    {
        score = 0;
    }

    public void Update()
    {
        scoreText.text = "Score: " + score.ToString(); // 更新分数文本
    }
}
