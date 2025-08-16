using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MyplayerScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // 用于显示分数的文本
    // 静态变量的好处就是不用每次都去实例化脚本就可以直接访问并且调用改变值
    public static int score = 0; // 静态变量，用于存储分数
    public void Update()
    {
        scoreText.text = "Score: " + score.ToString(); // 更新分数文本
    }
}
