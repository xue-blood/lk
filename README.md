# lk
Create symbol link by GUI on windows

## 文件链接主要有这些用途
1. 复制超大的文件
2. 文件对目录都特殊要求（比如很多软件只能装在 C盘）
3. 文件要很多副本

## 用法
window:
1. 解压 win.zip 到一个目录，点击 install.bat (卸载就点 uninstall.bat)
2. 选好要链接的文件,点右键 可以看到 有个"文件链接"菜单，然后选 "选择"
3. 在目标文件夹下选择 "创建",如果失败就用 "管理员"

## 命令:
mklink /j <目标文件夹> <源文件夹> 
(同一分区)：mklink /h <目标文件> <源文件> 
(不同分区，要管理员权限)：mklink <目标文件> <源文件> 

## mac:
在终端下执行: ln -s <源路径> <目标路径>

## 安装
![](https://user-images.githubusercontent.com/18024882/37070820-6cf0f322-21f4-11e8-8b69-ed66b392c4ba.png)

## 使用
![](https://user-images.githubusercontent.com/18024882/37070820-6cf0f322-21f4-11e8-8b69-ed66b392c4ba.png)
