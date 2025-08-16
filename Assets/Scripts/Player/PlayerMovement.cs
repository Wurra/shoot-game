using JetBrains.Annotations;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    //玩家移动速度
    public float Speed = 6f;
    //玩家的Animator组件，用于控制动画
    private Animator animator;
    //解锁新的错误，以后最好还是暴露出来，刚刚忘记获取组件，不然我们一启动就会报空
    public Rigidbody rb;
   
    private void Awake()
    {
        //获取玩家的Animator组件
        animator = GetComponent<Animator>();
        //获取玩家的Rigidbody组件
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        //下面两行代码的意思是获取玩家在水平和垂直方向上的输入轴值
        //Horizontal(水平)Vertical(垂直)
        //GetAxisRaw是能让玩家一摁方向键就能立马反应，GetAxis是那种需要长摁才会动的，适合开车的动作，input是获取的意思
        float h = Input.GetAxisRaw("Horizontal");//不过原来GetAxisRaw已经是一个很完善的方法了，可以直接调用
        float v = Input.GetAxisRaw("Vertical");
        Move(h,v);
        Turning();
        Animating(h, v);
    }
    //设置一个需要传参的方法，传入水平和垂直方向的输入值，这样代码里边就不会报错显示无法访问，调用的时候再传入参数
    public void Move(float h,float v)
    {
        Vector3 direction = new Vector3(h, 0, v);
        transform.Translate( direction * Time.deltaTime * Speed );
    }
    public void Turning()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //获取摄像机的射线
        //•	Camera.main：获取场景中标签为"MainCamera"的相机
        //•	ScreenPointToRay：将屏幕上的点(鼠标位置)转换为从相机发出的射线
        //•	Input.mousePosition：获取当前鼠标在屏幕上的位置(x, y, z)坐标

        int groundLayer = LayerMask.GetMask("Ground");
        //获取地面层的LayerMask
        //•	LayerMask.GetMask：将层名称转换为整数形式的层掩码，用于筛选射线只与特定层的对象进行碰撞检测

        RaycastHit hit;
        //定义一个RaycastHit变量来存储射线检测的结果
        //•	RaycastHit：存储射线碰撞信息的结构体，包含碰撞点、法线等
        
        bool isTouchingGround = Physics.Raycast(cameraRay, out hit,100, groundLayer);
        //使用Physics.Raycast来检测射线是否与地面层碰撞
        //•	Physics.Raycast：发射射线并检测碰撞，参数分别是:
        //•	射线
        //•	输出的碰撞信息
        //•	最大检测距离(100单位)
        //•	层掩码(只检测Ground层)
        //•	返回布尔值表示是否检测到碰撞
        if (isTouchingGround)
        {
            //如果射线检测到地面层
            Vector3 playerToMouse = hit.point - transform.position;
            //获取玩家到鼠标点击位置的向量
            //•	hit.point：射线与地面碰撞的具体位置
            //•	减去玩家位置(transform.position)得到从玩家指向鼠标的方向向量
            playerToMouse.y = 0; //保持水平面
            //将玩家到鼠标点击位置的向量的y分量设置为0，以保持水平面
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            //•	Quaternion.LookRotation：创建一个四元数，表示朝向特定方向的旋转
            //•	这个方法使对象的z轴(前向)对齐到指定的向量方向

            rb.MoveRotation(newRotation);
            //•	rb.MoveRotation：是Rigidbody组件的方法，用于物理系统中平滑地旋转刚体
            //•	相比直接设置transform.rotation，这种方式更适合在物理系统中使用，可以与物理模拟正确交互

            Debug.DrawLine(cameraRay.origin, hit.point, Color.red);
        }   
    }
    private void Animating(float h, float v)
    {
       if(h != 0 || v != 0)
        {
            //如果玩家在水平或垂直方向上有输入
            animator.SetBool("iswalking", true);
            //设置Animator的"Running"参数为true，触发跑步动画
        }
        else
        {
            //如果玩家没有输入
            animator.SetBool("iswalking", false);
            //设置Animator的"Running"参数为false，停止跑步动画
        }
    }
}
