
#region 脚本功能、创建时间和文件作者
/**************************************
    文件：ScriptsInfoRecoder.cs
    作者：LoriaRujoy
    邮箱：2659635618@qq.com
    时间：2025/8/30 12:00
    功能：脚本添加头部注释
***************************************/
#endregion

using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class ScriptsInfoRecoder : UnityEditor.AssetModificationProcessor {
    //存在文件还未生成完全就执行该脚本情况，会导致异常脚本创建失败
    //将OnWillCreateAsset改为OnDidCreateAsset
    //private static void OnWillCreateAsset(string path) {
    private static void OnDidCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        if (path.EndsWith(".cs"))
        {
            string str = File.ReadAllText(path);
            str = str.Replace("#CreateAuthor#", Environment.UserName).Replace(
                              "#CreateTime#", string.Concat(DateTime.Now.Year, "/", DateTime.Now.Month, "/",
                                DateTime.Now.Day, " ", DateTime.Now.Hour, ":", DateTime.Now.Minute));
            File.WriteAllText(path, str);
        }
    }
}