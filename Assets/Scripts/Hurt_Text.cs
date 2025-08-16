using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hurt_Text : MonoBehaviour
{
    private TMP_Text text;//字符文本组件

    float alpha;//阿尔法值，控制字体逐渐消失的效果

    [Range(0, 10)]
    public float speed;//飘动速度

    [Range(0, 1)]
    public float speed_weak;//速度衰弱

    [Range(0, 50)]
    public float dis_time;//消失时间

    [Range(0, 10)]
    public float weak_time;//开始衰弱时间

    public Color color;//文本颜色

    //这个必须提醒一下，必须使用Awake而Start不行，因为在上一段文本赋值时
    //到文本对象脚本的Start还没有执行到，会造成text为null异常
    void Awake()
    {
        alpha = 1;
        text = GetComponentInChildren<TMP_Text>();
        Destroy(gameObject, dis_time);
        text.color = color;
    }

    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);//使字体向上移动
        if (speed > 0)
        {
            speed -= speed_weak;//使得向上移动的速度逐渐减少
        }
        else if (speed <= 0 && weak_time > 0)
        {
            weak_time -= Time.deltaTime;//当速度为0后，字体颜色开始透明的时间
        }
        else if (alpha > 0 && weak_time <= 0)
        {
            alpha -= 0.01f;//控制字体逐渐透明
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);//修改字体颜色，透明度等
    }

    public void Init(int hurt)
    {
        text.text = hurt.ToString();//设置文本
    }
}
