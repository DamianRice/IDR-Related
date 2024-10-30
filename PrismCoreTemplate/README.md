# 模板使用方法
打开 TODO 标签，需要改的地方基本就这么多
模板目前没有集成DB,AOP,GenericHost这三个东西
模板目前没有根据DDD分层




# Prism优缺点
Prism WPF是一套极为完善的MVVM框架，包含所有所需的MVVM功能封装，特色功能比如AutoWired VM, Navigation Region Management，Modules模块化管理

优点是：
1. 框架非常成熟，很多MvvM相关Service都已经封装的非常好
2. 和多个UI框架都可以集成，偶尔根据情况写一些适配就可以
3. 文档和Demo非常成熟
4. 默认DryIoC的DI容器性能比较强


缺点是：
1. 和net windows集成度较高(Modules模块)，
2. 没有整体的Solution架构体系，需要自己搭建
3. 没有DB的集成，需要借助MS DI的方式或者做手动DI，
4. 默认容器是DryIoC， MS DI+Autofac目前要收费


# MaterialDesign WPF优缺点
优点：
1. 很成熟的UI库，Bug少，更新稳定，性能不错，有虚拟化支持
2. 集成原生控件库的方式简单，可以通过Style的方式附加在原生Control里
3. 有自定义控件库，填补原生控件库的空缺，还有社区的Extension支持，默认还提供了Dialoghost NotifyIconTray, Popups，Icons,Snackbar等等，实现上比较丰富

缺点
1. 没有集成的ThemeManager Service和TrayIcon，没有比如SpreadSheet,PDF View等进阶Control组件
2. Style需要根据policy重写，但是控件动画重写会比较麻烦，整体全部都是MaterialDesign风格
3. 控件虚拟化支持不如收费库好，数据量大的话界面略微会卡，场景有限




# Create Template过程中发现的
1. Prism 9 Region命名空间变更
2. Prism Ms DI集成，需要Commercial Plus， 499 per developer per annual, 目前降级到8，为了适配
3. DryIoC可以进行AOP, 但是之前没有实现过，看了文档思路上也不难实现；see: https://github.com/dadhi/DryIoc/blob/master/docs/DryIoc.Docs/Interception.md