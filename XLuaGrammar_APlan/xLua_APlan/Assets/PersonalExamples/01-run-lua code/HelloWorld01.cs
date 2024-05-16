using UnityEngine;
using XLua;//引入XLua库

public class HelloWorld01 : MonoBehaviour
{
    private LuaEnv luaEnv;  //lua运行环境

    void Start()
    {
        luaEnv = new LuaEnv();

        luaEnv.DoString("print('Hello world!')");//执行lua的print程序

        luaEnv.DoString("CS.UnityEngine.Debug.Log('Hello world!')");//官方示例，lua调用C#的print程序
    }

    private void OnDestroy()
    {
        luaEnv.Dispose();//释放luaEnv
    }

    #region 讲解说明
    /*  有时候编写脚本后，出现无法保存情况，可试着直接关闭脚本，
     *  文本未保存直接关闭，会跳出提醒框问是否保存，实现脚本的保存。
     *  
     *  lua执行:
     *  using XLua;                                                         //引入XLua库（使用Lua的第一步）
     *  LuaEnv luaEnv = new LuaEnv();                                       //lua运行环境（使用Lua的第二步，非常重要）
     *  luaEnv.DoString("Lua方法/程序");                                    //使用luaEnv的函数调用方法DoString，执行lua程序（第三步，使用lua程序）
     *  luaEnv.DoString("CS.UnityEngine.Debug.Log('Hello world!')");        //官方示例
     *  luaEnv.DoString("print('Hello world!')");                           //执行lua的print程序
     *  luaEnv.Dispose();                                                   //执行完lua程序后，释放luaEnv，也可以把释放放到OnDestory方法中（第四步，释放lua环境）
     *  
     *  一个LuaEnv实例，对应lua虚拟机，建议全局唯一。
     *  
     *  DoString("lua代码");   Do表运行lua的方法，
     *  （1）DoString方法括号里传入的内容是： 一切合法的lua代码 
     *  （2）DoString方法要求传入格式是字符串格式：即"lua代码"；
     *  （3）如执行lua的打印程序：
     *      1）luaEnv.DoString("print('Hello world!')");
     *          输出：LUA: Hello world!
     *          会直接输出到unity控制台上，输出前面会有个 LUA的标识，
     *          LUA：标识 提醒是LUA输出，可避免与Debug输出混淆
     *      2）luaEnv.DoString("CS.UnityEngine.Debug.Log('Hello world!')");
     *          输出：Hello world!
     *          这里是lua调用C#的类：Unity的Debug方法,输出到控制台，无LUA标识
     *          代码里CS是C#的标识，UnityEngine是完整的命名空间，Debug.Log是调用的方法，括号里是传入的参数
     *      3）print方法里使用单引号''：
     *          lua里字符串可以使用双引号""，也可以使用单引号''
     *          C#里字符串要求使用双引号""
     *          故C#调用Lua程序，print程序里这里使用单引号''；
     *          
     *  上述两种使用lua的方法，平时写程序时，不会这样写，会写单独的lua文件
     *  可以把lua程序放入Resources文件夹里加载，得到lua文件里所有的字符串，
     *  把得到的字符串当作参数放入DoString里执行，这样也是可以的，可参考02-HelloWorld By File
     *  
     *  Resources文件夹放在放在任何地方都可以，故02-HelloWorld By File示例中，
     *  直接把Resources文件夹放在02-HelloWorld By File文件夹下
     *  
     *  OnDestory：当游戏退出时，或游戏场景转换时才调用；
     */
    #endregion
}
