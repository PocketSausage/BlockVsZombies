
# TetrisZombieDefense（俄罗斯方块僵尸防御）

一个使用 Unity 开发的创意小游戏，将经典的俄罗斯方块玩法与僵尸防御机制结合在一起。

---

## 🎮 游戏玩法简介

- 玩家控制下落的俄罗斯方块（七种形状）
- 每秒自动下落一次，支持左右移动与旋转
- 填满整行即可自动消除
- 僵尸会不断靠近顶部区域，如果撞到顶墙，则玩家“失去一点生命”
- 顶部有一个血条（初始值为 10），血条归零则游戏失败

---

## 🧠 项目功能亮点

- ✅ 经典俄罗斯方块核心玩法
- ✅ 结构体旋转与碰撞检测
- ✅ 消除系统 + 行下坠逻辑
- ✅ 僵尸生成、移动、碰撞检测
- ✅ 僵尸触顶扣血 + 自动销毁机制
- ✅ 顶部血条显示与游戏失败判定
- ✅ 使用对象池优化性能（避免频繁 Instantiate/Destroy）
- ✅ 多场景切换：开始界面 / 游戏中 / 游戏结束
- ✅ UI 界面按钮控制（开始、重试、返回）

---

## 🛠 技术栈

- **开发引擎**：Unity 2021+
- **脚本语言**：C#
- **目标平台**：PC / WebGL
- **使用功能**：
  - MonoBehaviour 生命周期管理
  - 物理碰撞系统（Collider、Trigger）
  - UI 系统（Canvas、Slider、Button）
  - 场景管理（SceneManager）
  - 对象池（Object Pool）管理优化

---

## 📂 项目结构（部分）

```
Assets/
├── Prefabs/             # 俄罗斯方块和僵尸预制体
├── Scripts/
│   ├── Generator.cs         # 结构生成器
│   ├── Controller.cs        # 控制结构体移动和旋转
│   ├── ClearManager.cs      # 行消除逻辑
│   ├── HealthManager.cs     # 顶部血条管理
│   ├── ZombieManager.cs     # 僵尸生成与注册
│   ├── GameManager.cs       # 游戏流程控制
│   └── UI 控制脚本等
├── Scenes/
│   ├── StartScene
│   ├── GameScene
│   └── GameOverScene
```

---

## 🚀 如何开始游戏

1. 克隆本仓库：
   ```bash
   git clone https://github.com/你的用户名/TetrisZombieDefense.git
   ```

2. 打开 Unity，选择 `TetrisZombieDefense` 项目目录

3. 打开 `StartScene` 并点击运行按钮（▶）

---

## 📌 后续计划（To Do）

- [ ] 僵尸动画与AI完善
- [ ] 增加关卡与速度难度提升
- [ ] 增加天赋卡系统
- [ ] 加入音效与特效反馈
- [ ] WebGL 打包并部署试玩版本

---

## 💡 作者

该项目由一位 Unity 新手开发者独立完成，旨在练习游戏逻辑、UI 管理与场景切换等功能。如果你喜欢这个创意，欢迎点赞、关注与建议！
