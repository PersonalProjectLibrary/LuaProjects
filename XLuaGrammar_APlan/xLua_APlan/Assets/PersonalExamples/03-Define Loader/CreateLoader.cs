using UnityEngine;
using XLua;
using System.IO;

public class CreateLoader : MonoBehaviour
{
    void Start()
    {
        LuaEnv env = new LuaEnv();

        env.AddLoader(MyLoader);                                            //通过AddLoader调用自定义的Loader方法：MyLoader；
        //env.DoString("require 'xxxxx'");                                  //使用require调用不存在的文件
        //env.DoString("require'helloworld'");                              //使用require调用Rescource文件夹下的文件
        env.DoString("require'test007'");                                   //使用require调用StreamingAsset文件夹下的文件

        env.Dispose();
    }

    //定义满足CustomLoader条件的方法
    private byte[] MyLoader(ref string filePath)
    {
        //streamingAssets文件夹下目标文件完整路径
        string absPath = Application.streamingAssetsPath + "/" + filePath + ".lua.txt";
        //根据路径读取文件内容，需要用到using System.IO;对文件进行读写
        //将文本里的数据取出返回
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(absPath));
    }

    #region 说明讲解
    /*  
     *  编写迭代：
     *  LuaEnv env = new LuaEnv();
     *  env.AddLoader(MyLoader);
     *  //env.DoString("require 'xxxxx'");                                  //使用require调用不存在的文件
     *  //env.DoString("require'helloworld'");                              //使用require调用Rescource文件夹下的文件
     *  env.DoString("require'test007'");                                   //使用require调用StreamingAsset文件夹下的文件
     *  env.Dispose();
     *  
     *  一、MyLoader直接返回null；
     *  private byte[] MyLoader(ref string filePath){return null;}
     *  
     *  二、MyLoader先执行打印操作，在返回null
     *  private byte[] MyLoader(ref string filePath)
     *  {
     *      print(filePath);                                                    //执行打印文件名的操作
     *      return null;
     *  }
     *  
     *  三、MyLoader有返回字符,但返回内容与加载文件无关
     *  private byte[] MyLoader(ref string filePath)
     *  {
     *      string s = "print(123)";                                            //返回出去的字符串会在lua中作为代码执行，这里实际返回执行print方法
     *      return System.Text.Encoding.UTF8.GetBytes(s);                       //字符串s转化为字节数组，并返回出去
     *  }
     *  
     *  四、MyLoader从指定文件加下找目标文件，并返回文件内字符
     *  private byte[] MyLoader(ref string filePath)
     *  {
     *      string s = "print(123)";                                            //返回出去的字符串会在lua中作为代码执行，这里实际返回执行print方法
     *      return System.Text.Encoding.UTF8.GetBytes(s);                       //字符串s转化为字节数组，并返回出去
     *  }
     *  
     *  五、
     *  private byte[] MyLoader(ref string filePath)
     *  {
     *      string absPath = Application.streamingAssetsPath + "/" + filePath + ".lua.txt";
     *      return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(absPath));    //将文本里的数据取出返回
     *  }
     *  
     *  说明讲解：
     *  1、LuaEnv.AddLoader(LuaEnv.CustomLoader loader);                       //通过AddLoader调用自定义的Loader方法：MyLoader；
     *      （1）通过LuaEnv.AddLoader，添加自定义Loader方法MyLoader。
     *          参数 LuaEnv.CustomLoader，通过定位查看源码可知，是一个方法类型，要求该方法可以实现：传入字符串，返回字节列表；
     *          如上面的MyLoader方法就是一个符合条件的CustomLoader/Loader方法，通过LuaEnv.AddLoader，当作为一个Loader方法使用：
     *      （2）使用require，启动loader方法调用脚本，获取可执行的字符串
     *          require引用时，可能会有多个loader,每个loader有不同的加载方式，它会一个个loader去加载，
     *          加载失败就加载其他的loader，直到有一个loader加载成功返回字符串，就不执行其他的loader；
     *          如果自定义Loader方法都不行，会执行内置的Loader方法;
     *          
     *  2、MyLoader直接返回null；
     *      （1）输出：会报错 module 'xxxxx' not found:no field package.preload['xxxxx']....
     *      
     *      （2）因为MyLoader里是直接返回null，没有获取到字符串，自定义loader失败。
     *          使用内置loader加载xxxxx文件，也找不到带有xxxxx名的文件,所以报错：找不到目标文件。
     *          
     *      （3）xxxxx换成helloworld可找到正常加载，输出：LUA: Hello world from file LUA: 5
     *          MyLoader，MyLoader返回null，加载失败，执行内置的loader，从Resources文件夹里找到helloworld文件并返回字符串
     *          
     *  3、MyLoader先执行打印操作，在返回null
     *      （1）输出：先输出 xxxxx，然后报错：module 'xxxxx' not found:no field package.preload['xxxxx']....
     *      
     *      （2）因为require请求时，逐一执行多个loader方法，其中MyLoader会执行打印操作，但无字符串返回，还是属于加载失败。
     *          即使使用内置loader，也找不到带有xxxxx名的文件,进行报错：找不到目标文件。
     *          
     *      （3）xxxxx换成helloworld，输出：helloworld，LUA: Hello world from file， LUA: 5
     *          因为require请求时，逐一执行多个loader方法，其中MyLoader会执行打印操作，但无字符串返回，还是属于加载失败。
     *          然后使用内置loader，找到helloworld文件,把helloworld返回字符传入env.DoString()进行执行。
     *          
     *  4、MyLoader有返回字符,但返回内容与加载文件无关
     *      （1）输出：LUA: 123；
     *      
     *      （2）原因是执行MyLoader，返回字符s/print(123)，
     *          1）返回的字符，与加载的脚本文件里的字符/程序内容无关，
     *          2）有loader成功返回字符后，就不会再执行其他loader,不会继续使用内置的loader加载。
     *              执行env.DoString("require 'xxxxx'");相当于执行env.DoString("s");
     *              而不是执行env.DoString("xxxxx文件加载返回的内容");故输出是LUA: 123；
     *      3）xxxxx换成helloworld，也还是输出：LUA: 123；原因同上
     *          
     *  5、MyLoader从指定文件加下找目标文件，并返回文件内字符
     *      （1）输出：LUA: I am 007!
     *      （2）StreamingAsset文件夹位置固定，必须得放到根目录Assets文件夹下;
     *              可用专门的api获取streamingAssets文件所在路径：Application.streamingAssetsPath
     *              StreamingAsset文件夹里目标文件的完整路径：Application.streamingAssetsPath + "/" + filePath + ".lua.txt";
     *              注：用"/"分割，文件要加后缀".lua.txt"
     */
    #endregion
}
