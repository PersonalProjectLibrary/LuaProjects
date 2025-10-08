
#region 脚本功能、创建时间和文件作者
/**************************************
    文件：HotFixScript.cs
    作者：LoriaRujoy
    邮箱：2659635618@qq.com
    时间：2025-10-04 
    功能：Lua虚拟环境搭建
***************************************/
#endregion

using System.IO;
using UnityEngine;
using XLua;

public class HotFixScript : MonoBehaviour
{
    private LuaEnv luaEnv;
    private string filePath;
    void Start()
    {
        luaEnv = new LuaEnv();
        //相对路径，Asset同级目录下的LuaScripts文件夹
        filePath = Application.dataPath + "/../LuaScripts/";
        //加载Lua文件
        luaEnv.AddLoader(MyLoader);
        //运行Lua文件
        luaEnv.DoString("require 'Fish'");
    }

    private byte[] MyLoader(ref string luaFilePath)
    {
        string localFile = filePath + luaFilePath + ".lua";
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(localFile));
    }

    private void OnDisable()
    {
        luaEnv.DoString("require 'FishDispose'");
    }

    private void OnDestroy()
    {
        //销毁Lua虚拟环境
        luaEnv.Dispose();
    }
}