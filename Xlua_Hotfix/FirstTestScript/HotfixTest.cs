using UnityEngine;
using XLua;

namespace XLuaTest
{
    /* 正常不加Hotfix标签，若要修改该脚本，则在该类上加Hotfix标签，之后可以对该类里的方法进行更改*/
    [Hotfix]
    public class HotfixTest : MonoBehaviour
    {
        LuaEnv luaenv = new LuaEnv();

        private int tick = 0;   //如果是private的，在lua设置xlua.private_accessible(CS.HotfixTest)后可访问

        // Use this for initialization
        void Start()
        {
        }

        //下方lua修改了Update，Update添加LuaCallCSharp标签
        [LuaCallCSharp]
        void Update()
        {
            if (++tick % 50 == 0)
            {
                Debug.Log(">>>>>>>>Update in C#, tick = " + tick);
            }
        }

        void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 300, 80), "Hotfix"))
            {
                /* 格式：luaenv.DoString(@"xlua.hotfix(CS.命名空间.类名, '想要修复的方法的方法名', 替换修复方法的补丁方法)")*/
                luaenv.DoString(@"
                xlua.hotfix(CS.XLuaTest.HotfixTest, 'Update', function(self)
                    --[[
                    self.tick = self.tick + 1
                    if (self.tick % 50) == 0 then
                        print('<<<<<<<<Update in lua, tick = ' .. self.tick)
                    end
                    ]]
                    local a = CS.UnityEngine.GameObject.Find('Main Camera')
                    print(a.name)--使用lua输出
                    CS.UnityEngine.Debug.Log(a.name)--使用C#输出
                end)
                ");
            }

            string chHint = @"在运行该示例之前，请细致阅读xLua文档，并执行以下步骤：

1.宏定义：添加 HOTFIX_ENABLE 到 'Edit > Project Settings > Player > Other Settings > Scripting Define Symbols'。
（注意：各平台需要分别设置）

2.生成代码：执行 'XLua > Generate Code' 菜单，等待Unity编译完成。

3.注入：执行 'XLua > Hotfix Inject In Editor' 菜单。注入成功会打印 'hotfix inject finish!' 或者 'had injected!' 。";
            string enHint = @"Read documents carefully before you run this example, then follow the steps below:

1. Define: Add 'HOTFIX_ENABLE' to 'Edit > Project Settings > Player > Other Settings > Scripting Define Symbols'.
(Note: Each platform needs to set this respectively)

2.Generate Code: Execute menu 'XLua > Generate Code', wait for Unity's compilation.


3.Inject: Execute menu 'XLua > Hotfix Inject In Editor'.There should be 'hotfix inject finish!' or 'had injected!' print in the Console if the Injection is successful.";
            GUIStyle style = GUI.skin.textArea;
            style.normal.textColor = Color.red;
            style.fontSize = 16;
            GUI.TextArea(new Rect(10, 100, 500, 290), chHint, style);
            GUI.TextArea(new Rect(10, 400, 500, 290), enHint, style);
        }
    }
}
