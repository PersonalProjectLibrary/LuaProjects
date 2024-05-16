using System;                           //事件Action，委托delegate需要
using System.Collections.Generic;       //通过Dictionary、List映射需要
using UnityEngine;
using XLua;

public class CSharpCallLua : MonoBehaviour
{
    LuaEnv luaEnv;

    private void Awake()
    {
        luaEnv = new LuaEnv();
        luaEnv.DoString("require 'CSharpCallLua'");
    }

    void Start()
    {
        //获取lua里的变量
        /*
        int a = luaEnv.Global.Get<int>("a");
        string str = luaEnv.Global.Get<string>("str");
        float b = luaEnv.Global.Get<float>("b");
        bool isDie = luaEnv.Global.Get<bool>("isDie");
        print(a + str + b + isDie);
        */

        //table映射到class
        /*
        Person p = luaEnv.Global.Get<Person>("person");
        print(p.name + "," + p.age);
        p.age = 20;
        luaEnv.DoString("print(person.age)");
        */

        //table映射到interface
        /**/
        IPerson p = luaEnv.Global.Get<IPerson>("person");
        luaEnv.DoString("print(person.age)");
        p.eat();                                                    //执行lua里的无参eat方法，               输出：LUA: 我正在吃饭
        p.work(2f, 6f);                                             //执行lua里的work(arg,a,b)方法，         输出：LUA: 8.0
        p.work2(3f, 3f);                                            //执行lua里的person:work2(a,b)方法，     输出：LUA: 6.0
        /**/

        //通过Dictionary、List映射
        /*
        Dictionary<string, object> dict = luaEnv.Global.Get<Dictionary<string, object>>("person");      //通过字典映射
        foreach (string key in dict.Keys)                          //遍历字典进行输出
        {
            print(key + "：" + dict[key]);                         //通过输出字典里的值查看哪些可以被成功映射
        }
        List<object> list = luaEnv.Global.Get<List<object>>("person");                                  //通过List映射
        foreach (var o in list)                                   //遍历List进行输出
        {
            print(o);                                              //通过输出list里的值查看哪些可以被成功映射
        }
        */

        //映射到LuaTable类里
        /*
        LuaTable tab = luaEnv.Global.Get<LuaTable>("person");
        print(tab.Get<string>("name") + "，长度：" + tab.Length);
        */

        //访问lua全局函数function：事件Action，委托delegate
        /*
        //方法1、对于无参函数，可用最简单的luaenv.Get<Action>方法以事件委托的方法获取lua的函数
        Action act1 = luaEnv.Global.Get<Action>("eat2");
        act1();                                                                 //调用委托/函数   输出：LUA: 我正在吃饭

        //方法2、对于带参函数，也可用Get<Action>事件委托的方法获取lua的函数，注：对于带参函数的委托要生成代码
        //Action<int, int> act2 = luaEnv.Global.Get<Action<int, int>>("work2"); 
        //act2(13, 5);

        //方法3、对于带参函数，使用自定义委托，如下方的Work2();
        //Work2 work2 = luaEnv.Global.Get<Work2>("work2");                       //报错未实例化,work2获取未空，猜测可能先获取到person表后再获取work/work2
        //Work2 work2 = new Work2(luaEnv.Global.Get<Work2>("work2"));           //报错，Work
        Work2 work2 = new Work2(test);                                          //委托必须先实例化 new下
        work2 += luaEnv.Global.Get<Work2>("work2");                             //这样成功运行
        work2(12, 6);
        */

        //访问lua全局函数function：LuaFunction
        //LuaFunction func = luaEnv.Global.Get<LuaFunction>("eat");         //早前用法，现在版本变更，待解决实例化不成功问题
        //func.Call();
    }

    class Person
    {
        public string name;
        public int age;
    }

    [CSharpCallLua]
    public interface IPerson
    {
        string name { get; set; }                                   //接口不能包含字段，声明为get，set的方法
        int age { get; set; }
        void eat();                                                 //lua里对应函数无参数和返回值，这里声明void即可直接使用eat方法
        void work(float a, float b);                                //对应lua里work(arg,a,b)方法，比lua里少定义一个参数
        void work2(float a, float b);                               //对应lua里的work2(a,b)方法
    }

    //自定义委托
    [CSharpCallLua]   //注，该委托用于用于接收lua的函数调用，必须得加CSharpCallLua，否则会报错
    delegate void Work2(int a, int b);
    public void test(int a, int b) { print("ces：" + a + b); }



    private void OnDestroy()
    {
        luaEnv.Dispose();
    }

    #region 说明讲解
    //加载文件
    /*  
     *  1、CSharpCallLua：
     *  print("hi!")                                                            //输出到控制台判断是否加载成功
     *  
     *  对应使用 C#：
     *  luaEnv.DoString("require 'CSharpCallLua'");                             加载lua文件
     *  
     *  讲解说明：
     *  当调用完DoString时，CSharpCallLua里的变量已经存在与虚拟机（luaEnv）里的;
     *  DoString之前是取不到lua文件里的变量，DoString后（实际是中间的loader加载完文件后）可以取到
     */

    //获取lua文件里的基本数据变量
    /* 
     *  2、CSharpCallLua：
     *      a = 100 str = "siki" b=25.66 isDie = false                        //简单基本数据类型获取
     *  对应 C#：
     *      float b = luaEnv.Global.Get<float>("b");                         获取lua文件加载后放在luaEnv里的变量
     *  
     *  说明讲解：
     *      获取lua里的全局基本数据类型：luaEnv.Global.Get<泛型/类型>("lua文件里变量名");
     *      注意lua和C#中数据类型对应：
     *      lua number  -- C# int float double ，lua中number是小数，需要用float或double接收，否则会无法正确接收;
     *      lua string  -- C# string char
     *      lua boolean -- C# bool
     */

    //访问lua里的全局table：映射到class/struct
    /* 
     * 3、CSharpCallLua：
     *      person = {                                                          //lua新增person表
     *      name = "siki", age = "18",
     *      12,6,7,eat = function() print("我正在吃饭") end                     //多出来未对应映射的变量/函数
     *      }
     *   对应 C#：
     *   （1）定义Person类
     *      class Person{ public string name; public int age; }                 //接收person表的Person类
     *   （2）Person p = luaEnv.Global.Get<Person>("person");                   //将lua里的person表与class映射赋值
     *   （3）p.age = 20;                                                       //修改变量
     *        luaEnv.DoString("print(person.age)");                             //输出：LUA：18     table与class更新不同步
     *   
     *   说明讲解：
     *   （1）需要创建class或struct，与表的字段变量对应；
     *      如果table有一些变量/函数，class里没对应，只会把对应的变量赋值过来，不影响映射;
     *      反之class有table没有的字段也不影响映射，只不过对应变量没赋值。不过尽量保持映射一致。
     *   （2）同简单数据类型获取 luaEnv.Global.Get<泛型/类型>("lua文件里表名");
     *   （3）注：这过程是值拷贝，是lua new个实例并把对应字段赋值过去，如果class比较复杂代价会比较大，比较耗费性能；
     *      且修改class的字段值不会同步到table，反之也一样，修改table不会同步到class。
     *      这种映射可以通过把类型加到GCOptimize生成降低开销节约性能。
     */

    //访问lua里的全局table：映射到interface接口（最推荐）
    /*  
     *  4、CSharpCallLua：
     *  person = {
     *          eat = function() print("我正在吃饭") end,                         //无参函数
     *          --work = function(a,b) print(a+b end                              //需要传两个参数ab，这样写会报错，实际有隐藏参数被忽略
     *          work = function(arg,a,b) print(a+b end                            //带参函数第一个参数是隐式默认参数，多写个arg参数来存储隐式的参数值
     *          }
     *  对应 C#：
     *      1、定义IPerson接口
     *          [CSharpCallLua]                                                  //使用接口映射必须加特性：[CSharpCallLua]，否则报错
     *          public interface IPerson{                                        //是public，否则会报错
     *              string name { get; set; }                                    //接口不能包含字段，声明为get，set的方法
     *              int age { get; set; }
     *              void eat();                                                  //lua里无参数函数，这里声明void eat()即可
     *              void work(float a, float b);                                 //实际执行是work(p，a, b);
     *              void work2(float a, float b);                                //lua里冒号调用的work2(a,b)方法,
     *              }
     *              
     *      2、将lua里的person表与interface IPerson映射对应
     *          IPerson p = luaEnv.Global.Get<IPerson>("person");
     *      
     *      3、执行获取到的lua里的eat、work方法
     *          p.eat();                                                         //直接调用接口的eat方法，输出：LUA: 我正在吃饭
     *          p.work(2f, 6f);                                                  //实际调用是p.work(p,2f, 6f);
     *          p.work2(3f, 3f);
     *          
     *  说明讲解：
     *      1、接口不能包含字段，映射lua里对应的字段声明为get，set的方法
     *      2、使用接口映射时，接口必须要加个特性[CSharpCallLua]，xlua会为接口生成一些代码来映射，并且接口是public，否则会报错
     *      3、除了基本数据类型，也支持映射函数,如接口中eat方法，映射lua中eat方法
     *      4、映射lua里带参函数，注意隐式的参数，lua那边多个参数，C#这边少个参数，lua那边的隐式参数是方法对象本身
     *          如：function work(arg,a,b) print(a+b) end 对应 void work(float a, float b);  p.work(2f, 6.6f);
     *      5、可采用冒号（会自动把自己作为参数传入函数中）调用函数的方式，来处理lua和c#两边参数不统一问题。
     *          对于冒号调用的函数，lua会自动传入函数自身作为隐式参数，函数里会有隐式self变量存储传入的隐式参数/第一个参数。
     *      6、使用接口映射，接口和lua相当于引用，interface这边修改值后，lua的table对应的字段值也会同步修改
     */

    //访问lua里的全局table：映射到Dictory/Lis  更轻量级的by value方式
    /*
     * 5、CSharpCallLua：
     * 还是使用luaEnv的Get方法：luaEnv.Global.Get<泛型/类型>("lua文件里表名");方法获取
     * 
     * person = {
     *      name = "siki",age = "18",12,6,"siki",6.1,6,7,
     *      eat = function() print("我正在吃饭") end,
     *      work = function(arg,a,b) print(a+b) end
     * }
     * function person:work2(a,b) print(a+b);end
     * 
     * 对应 C#:
     * //使用Dic 映射：
     * Dictionary<string, object> dict = luaEnv.Global.Get<Dictionary<string, object>>("person");
     * //遍历字典进行输出，查看哪些可以被成功映射
     * foreach (string key in dict.Keys) { print(key + "：" + dict[key]);}
     * 输出结果：
     * age：18       work：function :13       name：siki       work2：function :12      eat：function :10
     *  
     *  //使用List 映射：
     *  List<object> list = luaEnv.Global.Get<List<object>>("person");
     *  //遍历List进行输出,查看哪些可以被成功映射
     *  foreach (var o in list){ print(o);}
     *  输出结果： 12, 6, siki, 6.1, 6, 7,
     *  
     * 说明讲解：
     *  （1）使用Dic映射：Get<Dictionary<string, object>>
     *      lua里变量/表名都是string，值有不同类型，所以字典里key用string类型，值用object类型：
     *      通过输出可知道 
     *          1）有key的对象包括函数都可以被映射过来，如：name、eat；
     *          2）只有值没有key的没有映射，如：12,6,7；
     *  （2）使用List映射：Get<List<object>>("person")
     *      为了映射更多，这里使用object类型;同时也试过将object换成int，string类型输出；
     *      通过输出可知，
     *          1）只能映射没有key的值，如：12,6,"siki",6.1,6,7；
     *          2）有key的无法映射，如：name、eat；
     *  （3）可通过Dic和List结合映射所有的数据。
     */

    //访问lua里的全局table：映射到LuaTable类 另外的一种by ref方式
    /* 
     * 6、对应 C#：
     *    LuaTable tab = luaEnv.Global.Get<LuaTable>("person");
     *    print(tab.Get<string>("name") + "，长度：" + tab.Length);     //输出：siki，长度：6
     *    
     * 还是使用luaEnv的Get方法，不过是指定类型LuaTable：luaEnv.Global.Get<LuaTable>("lua文件里表名");
     * 使用LuaTable的方法如：Getkeys：获得所有的键；length：获得长度；获取映射到LuaTable里的数据；
     * 
     * 好处是不用生成代码；基本所有类型都可以得到
     * 坏处：比前面使用class/struct、interface方式映射慢一个数量级，且没有类型检查等   
     */

    //访问lua全局函数function：事件，自定义委托
    /*
     * 7、
     * 最简单的luaenv.Get<Action>方法以事件委托的方法获取lua的函数
     * 优点，性能好，类型安全。缺点：在使用带参函数时，原生Action需要生成代码，否则会抛出 InvalidCastException异常。
     * 采用自定义委托，注意委托先实例化new一个，再+=的方式把lua的方法获取添加到委托中，避免委托未实例化成功，获取lua中方法失败等问题。
     * 
     *  C#
     *  方法1、可用最简单的luaenv.Get<Action>方法以事件委托的方法获取lua的函数
     * Action act1 = luaEnv.Global.Get<Action>("eat2");
     * act1();                                                                                                  //调用委托/函数
     * //act1 = null;                                                                                           //参见注1
     * 
     * 方法2、对于带参函数，也可用Get<Action>事件委托的方法获取lua的函数，注：对于带参函数的委托要生成代码
     * //Action<int, int> act2 = luaEnv.Global.Get<Action<int, int>>("work2");                                  //参见注2
     * //act2(13, 5);
     * 
     * //方法3、对于带参函数，使用自定义委托，如下方的Work2();
     * //Work2 work2 = luaEnv.Global.Get<Work2>("work");                        //报错委托未实例化，work2获取未空，猜测可能先获取到person表后再获取work/work2
     * //Work2 work2 = new Work2(luaEnv.Global.Get<Work2>("work2"));            //报错，Work2(输出越界)
     *   Work2 work2 = new Work2(test);                                         //委托必须先实例化 new下
     *   work2 += luaEnv.Global.Get<Work2>("work2");                            //这样成功实例化，成功运行
     *   work2(12, 6);
     * 
     * //自定义委托
     * [CSharpCallLua]                                                          //注，该委托用于用于接收lua的函数调用，必须得加CSharpCallLua，否则会报错
     * delegate void Work2(int a, int b); 
     * public void test(int a, int b) { print("ces：" + a + b); }               //辅助委托实例化
     * 
     * private void OnDestroy(){ luaEnv.Dispose();}
     * 
     * 注：
     * 1、act1=null这里，目的是把委托置空，避免一直不释放对eat2的引用，导致luaEnv.Dispose();
     *      但实际因为act1与Dispose都是在Start()函数里，在执行Start时一下子会同步执行很多方法，大概率act1还没执行完就执行Dispose()。
     *      而执行完act1会自动释放对应的lua资源，无需把act置空，所以提示显示该语句没必要未使用。
     *      因为大概率act1还未执行完，就执行Dispose()，导致报错。故这里把Dispose()放到OnDestory()里执行。
     * 
     * 2、对于带参函数委托要生成代码，
     *      生成代码：在委托上面添加CSharpCallLua的标识，配置下系统提供的Action方法，
     *      但目前还未接触配置，故上面注释掉暂时不考虑该解决方法
     * 
     */

    //访问lua全局函数function：LuaFunction
    /*
     * 8、
     * 优点：支持变参，可传任意类型、任意个数参数。返回值是object数组
     * 缺点：速度性能比较慢,不太建议使用该方法
     * 
     * luaenv.Get<Luafunction>方法获取接收lua的函数
     * 然后使用Luafunction函数里的Call方法调用
     */
    #endregion
}
