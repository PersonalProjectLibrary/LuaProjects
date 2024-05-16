using UnityEngine;
using XLua;

public class LuaCallCSharp : MonoBehaviour
{
    LuaEnv luaEnv;

    private void Awake()
    {
        luaEnv = new LuaEnv();
    }

    private void Start()
    {
        luaEnv.DoString("require 'LuaCallCSharp'");
    }

    private void OnDestroy()
    {
        luaEnv.Dispose();
    }

    #region 说明讲解
    /*
     * 这里获取执行lua脚本后，
     * 会自动执行lua里调用C#的方法语句，
     * 如 lua里调用C#创建新物体等操作
     */
    #endregion

}
