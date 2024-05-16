using UnityEngine;
using XLua;

public class HelloWorld02 : MonoBehaviour
{
    void Start()
    {
        //TextAsset ta = Resources.Load<TextAsset>("helloworld");           //加载直接放在Resources文件加下的helloworld.lua文件
        TextAsset ta = Resources.Load<TextAsset>("helloworld.lua");         //加载直接放在Resources文件加下的helloworld.lua.txt文件
        //print(ta);                                                        //输出加载的文件
        LuaEnv env = new LuaEnv();                                          //创建lua运行环境
        //env.DoString(ta.text);                                            //执行lua程序
        env.DoString("require'helloworld'");                                //调用lua脚本
        env.Dispose();                                                      //释放lua运行环境
    }

    #region 说明讲解
    /*  
     *  编写迭代：
     *  一、Resources加载读取helloworld.lua文件：无法加载读取
     *  TextAsset ta = Resources.Load<TextAsset>("helloworld"); 
     *  print(ta);
     *  
     *  二、Resources加载读取helloworld.lua.txt文件：正常加载读取内容
     *  TextAsset ta = Resources.Load<TextAsset>("helloworld.lua");
     *  print(ta);
     *  
     *  三、LuaEnv.DoString(string)加载执行lua程序：正常执行lua程序
     *  TextAsset ta = Resources.Load<TextAsset>("helloworld.lua");
     *  LuaEnv env = new LuaEnv();
     *  env.DoString(ta.text);
     *  env.Dispose();
     *  
     *  四、env.DoString("require'helloworld'")使用lua内置方法加载执行lua脚本文件：正常执行脚本程序
     *  
     *  说明讲解：
     *  1、Resources文件夹放在放在任何地方都可以，故02-HelloWorld By File示例中，
     *      直接把Resources文件夹放在02-HelloWorld By File文件夹下
     *  
     *  2、Resources.Load<类型>("文件路径");
     *      1)类型：加载文件，要说明加载类型；要加载helloworld.lua里的字符串，故使用TextAsset文本格式加载，
     *      2)路径：Resources加载，路径里不用加文件后缀，加载时会根据类型自动加后缀；
     *          helloworld.lua文件是直接放在Resources下的，故路径参数，直接是文件名"helloworld"
     *          即：Resources.Load<TextAsset>("helloworld") 
     *      3)返回值：因加载的是TextAsset类型，返回的也是TextAsset类型，
     *      
     *      故语法是：TextAsset ta = Resources.Load<TextAsset>("helloworld");
     *      
     *   3、加载helloworld.lua：
     *      1)
     *      TextAsset ta = Resources.Load<TextAsset>("helloworld");          //加载直接放在Resources文件加下的helloworld.lua文件
     *      print(ta);                                                       //加载失败，ta为 Null
     *          输出：Null
     *          
     *          注：
     *          Resource只支持有限的后缀，对于lua后缀不支持加载读写；故识别不出、找不到helloworld.lua文件，不能正常加载输出；
     *          若修改helloworld.lua后缀，则无法区分是lua程序和txt文本。故修改helloworld.lua后缀成helloworld.lua.txt;
     *          
     *          由于文件改为helloworld.lua.txt，Resources加载里不加后缀，自动根据类型加后缀，
     *          故写成 Resources.Load<TextAsset>("helloworld.lua");
     *          而不是Resources.Load<TextAsset>("helloworld")，也不是Resources.Load<TextAsset>("helloworld.lua.txt");
     *              
     *      即：
     *      TextAsset ta = Resources.Load<TextAsset>("helloworld.lua");      //加载直接放在Resources文件加下的helloworld.lua.txt文件
     *      print(ta);                                                       //加载成功，输出返回 ta里的内容
     *          控制台输出：print("Hello world from file") a=2 b=3 print(a+b)
     *      
     *      2)
     *      TextAsset ta = Resources.Load<TextAsset>("helloworld.lua");         //加载直接放在Resources文件加下的helloworld.lua.txt文件
     *      LuaEnv env = new LuaEnv();                                          //创建lua运行环境
     *      env.DoString(ta.text);                                              //执行lua程序
     *      env.Dispose();                                                      //释放lua运行环境
     *          控制台输出： 
     *              LUA: Hello world from file
     *              LUA: 5
     *      
     *          env.DoString(ta);会报错，
     *          env.DoString传入的是字符串，ta是TextAsset类型，
     *          故使用TextAsset的.text方法，获取里面的字符串，也可以通过.toString获取
     *          即：env.DoString(ta.text);或 env.DoString(ta.ToString()); 
     *          
     *  4、env.DoString("require'helloworld'");                                //调用lua脚本 
     *          控制台输出：
     *              LUA: Hello world from file
     *              LUA: 5
     *          lua中可使用require('文件名')来引用一个文件，使用require引用lua脚本时，它会使用loader来加载脚本
     *          内置的loader会通过Resources进行加载，会给文件名后面加上.lua和.txt，然后加载这个文件。
     *          
     *          require引用时，可能会有多个loader,每个loader有不同的加载方式，它会一个个loader去加载，
     *          加载失败就加载其他的loader，直到有一个loader加载成功返回字符串，就不执行其他的loader
     *          
     *          除了内置的loader，还可以自定义一个loader
     *          内置的loader通过Resources进行加载文件，若文件不是放在Resources文件夹下的，则无法找不到。
     *          有时候我们文件放在其他文件夹中或服务器中，使用内置loader是没办法找到的，这时候就需要自定义loader。
     *      
     *      可参考03-Define Loader 自定义loader
     */
    #endregion
}
