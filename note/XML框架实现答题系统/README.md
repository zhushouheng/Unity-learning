# XML框架的意义：
在游戏运行中，存在大量的用户数据信息，对于每一类数据都有单独的XML文件来存储相应的数据信息，如玩家信息PlayerConfig、物品信息ItmeConfig和怪物信息MonsterConfig等等等等，而每一个XML文档的格式也并非一致的，因此只能单独为每个文档提供特定的读取方式，这样代码量会及其恐怖，因此可以通过XML框架来进行限制。

主要逻辑是首先定义一个所有XML文档对应的读取脚本的最基类Config，所有XML文档对应的读取脚本都需要继承这个基类，并对外提供一个公共方法：加载当前脚本对应的XML文档中的内容

然后定义一个用来管理所有读取脚本的管理者ConfigDataManager来管理所有的脚本，同时创建一个容器将所有XML读取脚本读取到的信息都存储进该容器，在需要的时候进行获取，这样就大大减少了代码量