这个项目代码的主要目的是为了实现和展示在 www.AlgMain.com 中所谈到的一些算法。这些算法包括自然语言处理（NLP）算法，也包括哈希树查找算法（详见知乎：https://zhuanlan.zhihu.com/p/290832847 或者CSDN：https://blog.csdn.net/weixin_42574918/article/details/109718660 ）。该项目的多数代码由原来的Java代码移植至C#（Visual Studio）。由于两种语言所面对的平台和环境不同，相应地做了不少修改和调整。

项目SimpleTeam中的主要目录及其部分代码文件说明：

#（1）Case目录

  这里主要是一些测试用的例程。除了检测和验证的代码的正确性和效率，也相当于展示项目代码基本功能的例程。

  DataFileOperatorCase.cs：主要展示了基础的存储页分配和释放，以及数据增删功能。
  
  EndianCase.cs：主要展示了编码字节序的功能，包括：Little Endian（主机序）和Big Endian（网络序）。

  IndexFileOperatorCase.cs：主要展示了基于文件操作的HashTree查找算法。
  
  LogCase.cs：展示了项目有关日志的基本功能。

  PropertiesContainerCase.cs：展示了属性值容器的基本功能。

  QueueFileOperator.cs：展示了队列数据存储操作的基本功能。
  
  SimpleHashCase.cs：展示了基于纯内存对象的哈系树查找算法。
  
  SimpleListContainerCase.cs：展示了基于纯内存对象的双向循环链表算法。
  
  StringBinCase.cs：展示了不同类型的字符串（ToString、StringBuilder、RecycledString、LogString）对象在使用时间消耗上的差异。
  
  SystemTimeCase.cs：展示了项目自定义时间体系的基本功能。

#（2）Constant目录

  这里主要是一些系统常量的定义。这里面有一些常量定义和MMS-WAP协议相关。
  
  Prime.cs：定义了质数的基本算法，包括求取余数函数GetRemainder，小于1024的质数表，小于256的互质数表（用于哈希树查找算法）。

#（3）Container目录

  这里主要是定义了数据容器，及其相关算法操作。

  File目录：基于文件操作的一些算法的实现。
  
  Hash目录：基于纯内存对象的哈系数查找算法的具体实现。
  
  List目录：基于纯内存对象的双向循环链表算法的具体实现。

  Properties目录：基于Hashtable的属性容器的具体实现。

  Queue目录：基于纯内存对象的双向循环链表队列的具体实现。

  KeyElements.cs：key-value元素的基本定义。
  
  SimpleContainer.cs：简单容器的基本定义。
  
  SimpleElement.cs：简单元素的基本定义。

#（4）Function目录

  一些常用零散的函数功能。
  
  BigEndian.cs；按照网络序，将一些常见数据类型转换成字节数组，或者从字节数组转换成常用数据类型。
  
  Comparer.cs：对字符串和字节数组进行数据比较，可以允许输入参数为null。
  
  DecimalFormat.cs：将数字字符串解析成常用数据类型。
  
  Even.cs：偶数判定。
  
  HexFormat：将常用数据类型转换成16进制字节格式，或者从16进制字节格式转换成常用数据类型。
  
  LittleEndian.cs；按照主机序，将一些常见数据类型转换成字节数组，或者从字节数组转换成常用数据类型。
  
  Odd.cs：奇数判定。
  
  SimpleHash.cs：求取字符串或者字节数组的哈希值（非MD5）算法。
  
  SimpleRandom.cs：求取常用伪随机变量。使用了唯一的伪随机变量，该变量会随时间不同，而给予不同的初始值。

（5）IO目录

  定义了一些常用的IO操作。【该功能尚未完善】

  SimpleBuffer.cs：一个抽象的Buffer类。在Java中，由于NewIO的引入，导致有三种Buffer：原有的byte[]，Direct内存和内存映射。这里为了保证兼容性，而保留了这个定义，以兼容将来可能的其他Buffer类型。

  ByteBuffer.cs：C#中的SimpleBuffer抽象类的具体实现。

  Printer.cs：一个打印工具函数。主要用于输出多行文本。虽然其中有三个函数，但是仅有一个函数在C#中效率表现最佳。其余两个为了兼容性，而保留在代码中。

（6）Log目录

  定义了系统日志的实现模块。该模块还定义了可回收字符串的机制，以兼容其他模块。
  
（7）Time目录

  定义了系统的常用时间常量。
  
  Java系统中所使用的是以毫秒为基础，而C#系统中所使用的是以纳秒（Tick）为基础。此目录的功能主要是为了兼容一些常量的变换，方便后续迁移代码。
  
  SimpleTime.cs：兼具Java中的Time和Calendar的作用。

--------------------------------------------------------------------------------
（1）时间操作上，频繁获取当前时间是很消耗系统资源的。因此在Log模块中，采用了由定时器负责每间隔1秒刷新一下时间。

（2）Java的参数是可以配置为null的。而C#中，有不允许null的提示，而且这种警告还不少。这些警告，又不能完全忽视和屏蔽。因此只能视实际需求情况来定义。

（3）在Java中，通过回收和再次使用已创建的对象，可以提升一定的程序效率。而在C#中，这种操作反而会使得性能下降。提升效率的最直接办法就是用完即抛。在C#中回收和重用反而导致性能严重下降。

（4）哈希数查找算法中，绝大部分算法时间均在求取余数上。可执行代码，在Java和C#上的表现也差异较大。

哈希树查找算法在Java上的表现与内置的Hashtable效率相差不算太大。但是在C#上，两者差异非常明显。因此，这里的哈希树查找算法仅作为一种算法进行实现和研究用。

另外，为了使得内存消耗控制在一定范围内，HasChild函数也必须调用。而HasChild函数在CPU消耗上排第二，仅在GetRemainder函数之后。

（5）在Printer中，PrintMultiline函数可以输出多行至文件中。该函数中单个字符直接输出的方式，居然比先输出到string，再写入文件要快。

（6）在C#中，使用内存映射操作超大文件（>4GB）的速度的确要高于Java。在设备内存充足的情况下，C#基于内存映射的随机读写效率远高于Java。根据之前的测试结果，Java在文件超过2GB之后，即使使用内存映射文件访问，读写速度也下降得非常厉害。

给作者捐赠：

 ![微信捐赠](https://github.com/forestluo/AlgMain/blob/main/weixin.jpg?raw=true)
 
 ![支付宝捐赠](https://github.com/forestluo/AlgMain/blob/main/zhifubao.jpg?raw=true)
