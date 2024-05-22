using UnityEngine;
using XLua;
using System.IO;

public class HotFixScript : MonoBehaviour
{
    private LuaEnv luaEnv;//lua环境

    void Start()
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
        string adsPath = @"C:\Users\RG\Desktop\XluaProject\XluaFiles\" + fileName + ".lua.txt";
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(adsPath));
    }

    private void OnDestroy()
    {
        luaEnv.Dispose();//销毁当前lua环境
    }
}
