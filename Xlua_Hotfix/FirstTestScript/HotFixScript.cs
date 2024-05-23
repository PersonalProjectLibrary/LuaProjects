using UnityEngine;
using XLua;
using System.IO;

public class HotFixScript : MonoBehaviour
{
    private LuaEnv luaEnv;//lua环境

    /*
     * 读取lua文件/脚本的语句原来是放在Start方法里执行。
     * 因其他脚本的Start方法里的逻辑在后续被lua修改，
     * 因生命周期里Start只执行一次，导致lua脚本读取后，
     * 其他脚本的Start方法已经执行，故lua修改后的Start不会执行。
     * 导致lua补丁修改失败。
     * 避免出现这种无法补丁修改的问题，这里把读取lua文件的语句放进Awake()方法中执行
     */
    private void Awake()
    {
        luaEnv = new LuaEnv();//初始化lua环境
        luaEnv.AddLoader(MyLoader);//自定义加载方式MyLoader
        luaEnv.DoString("require 'FishUpdates'");//读取FishUpdates.lua.txt文件
    }

    /// <summary>
    /// 读取文件并把文件内容转化为UTF8格式的byte数组
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>
    private byte[] MyLoader(ref string fileName)
    {
        string adsPath = @"C:\Users\RG\Desktop\XluaProject\FishProject\Assets\XluaFiles\" + fileName + ".lua.txt";
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(adsPath));
    }
    /// <summary>
    /// 确保luaEnv.Dispose()前，把注册的委托都置空释放
    /// </summary>
    private void OnDisable()
    {
        luaEnv.DoString("require'FishDispose'");
    }

    private void OnDestroy()
    {
        luaEnv.Dispose();//销毁当前lua环境
    }
}
