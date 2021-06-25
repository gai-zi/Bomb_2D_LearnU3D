# Bomb_2D_LearnU3D
## 项目素材及部份工程文件出自[Unity中文课堂 (u3d.cn)](https://learn.u3d.cn/)

Unity Edition：2019.4.3(LTS)

Play it Online：[BombGuy - Unity Play](https://play.unity.com/mg/other/bombguy-6)

---

## <font color=orange>**Package**</font>

1. WebGL Publisher

2. Advertisement

3. [Joystick Pack](https://assetstore.unity.com/packages/tools/input-management/joystick-pack-107631)

   手机端触屏控制插件

4. [2D Tilemap Extras ](https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@1.6/manual/index.html)

   使用 Rule Tile 按自定规则绘制地图

   - [Animated Tile](https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@1.6/manual/AnimatedTile.html)
   - [Rule Tile（规则地图）](https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@1.6/manual/RuleTile.html)
   - [Rule Override Tile](https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@1.6/manual/RuleOverrideTile.html)

---

## <font color=orange>materials</font>

[Pixel Frog - itch.io](https://pixelfrog-assets.itch.io/)

---

## 项目组成

1. 五种敌人，行为逻辑相同，技能不同
2. 五个关卡（最后一关BOSS关）
3. 玩家角色(移动、跳跃、受击、死亡、跳跃FX、落地FX、跑动FX)
4. 场景中包含可互动的多种环境物体
5. 通往下一关卡的门
6. UI Manager、Game Manager

### UI部分

1. 暂停按钮、暂停菜单
2. 游戏结束菜单
3. 开始菜单
4. 玩家角色Health Bar
5. BOSS Health Bar
6. 安卓端触摸按键

---

## UML

![image-20210625161041775](https://github.com/gai-zi/Bomb_2D_LearnU3D/images/BombGuy_UML.png)

---

## <font color=orange>程序设计</font>

### <font color =green>**FSM有限状态机**</font>,在子类中定义具体逻辑

```c#
public abstract class EnemyBasState  //有限状态机
{
    public abstract void EnterState(Enemy enemy);
    public abstract void OnUpdate(Enemy enemy);

}
```

---

### **Enemy  of  <font color=green>Animator</font> **

#### Parameters：

1. state : int 
2. attack : trigger
3. hit : trigger
4. skill : trigger
5. dead : bool

#### Layer：

0. Base Layer
1. Attack Layer
2. Dead Layer

使用`state`切换`layer`：

1. 当`state=1`时敌人正在`Move to Target`，`state=0`时正在等待`idle`动画结束，从而实现敌人到巡逻点后停滞一个idle动画的长度
2. 当`state=2`时转换到`Attack Layer`，此Layer权重为1，完全覆盖其他Layer动画

---

### 单例模式

```c#
public class GameManager : MonoBehaviour
{
    public static GameManager instance;        //单例模式
    public void Awake()
    { 
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    ...
}
```

```c#
//调用单例模式
GameManager.instance.XX;
```

### <font color=green>PlayerPrefs类</font>

记录数据

---

