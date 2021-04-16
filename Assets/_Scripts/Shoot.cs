using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("Player Sounds")]
    public AudioSource shootSound;
    //定义子弹预制体
    public GameObject BulletPrefab;

    //定义一个公有类型的Transform 用来实例化一个要跟随的对象
    public Transform BulletSpawn;

    //定义一个子弹发射的速度
    public float BulletSpeed = 2000;

    public bool isShooting=true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnShootButtonPressed()
    {
        shootSound.Play();
        //创建一个游戏对象bullet 
        //Instantitate（一个游戏对象，游戏对象的位置，游戏对象的角度）
        //我们会在外面把我们制作的子弹预制体赋值给BulletPrefab  然后用bullet来获取子弹预制体的一系列值，这个后面有图片展示
        //这个语句就相当于把子弹预制体的位置和角度参数给了bullet  经过编写bullet来控制子弹的一系列功能
        GameObject bullet = Instantiate(BulletPrefab, BulletSpawn.position, BulletSpawn.rotation) as GameObject;

        //我们要给子弹添加刚体属性 添加刚体属性后才会有力学特征 才会动
        //bullet.GetComponent<Rigidbody>().velocity代表着获取刚体的速度矢量，表示刚体的位置变化率
        //简单来说 这条语句代表着把一个向前的速度给这个球
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * BulletSpeed;

        //子弹发射后如果不销毁的话就会一直堆积  毕竟不可能百发百中
        //Destroy（销毁对象，多少时间后）
        Destroy(bullet, 1);
        
        
    }
   
}
