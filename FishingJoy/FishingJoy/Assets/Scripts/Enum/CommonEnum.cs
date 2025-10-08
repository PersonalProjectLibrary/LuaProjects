
#region 脚本功能、创建时间和文件作者
/**************************************
    文件：CommonEnum.cs
    作者：LoriaRujoy
    邮箱：2659635618@qq.com
    时间：2025-10-08
    功能：通用枚举类型
***************************************/
#endregion

/// <summary>
/// 枪类型切换
/// </summary>
public enum SwitchGunType
{
    /// <summary>
    /// 升级枪
    /// </summary>
    Up,
    /// <summary>
    /// 降级枪
    /// </summary>
    Down
}

/// <summary>
/// 货币类型
/// </summary>
public enum CurrencyType
{
    /// <summary>
    /// 金币
    /// </summary>
    Gold,
    /// <summary>
    /// 钻石
    /// </summary>
    Diamond,
}

    /// <summary>
    /// 货币目标位置类型
    /// </summary>
    public enum CurrencyPlaceType
    {
        /// <summary>
        /// 捕鱼开出来的金币
        /// </summary>
        Gold,
        /// <summary>
        /// 捕鱼开出来的钻石
        /// </summary>
        Diamand,
        /// <summary>
        /// 宝箱开出来的金币
        /// </summary>
        GoldImg,
        /// <summary>
        /// 宝箱开出来的钻石
        /// </summary>
        DiamandImg,
    }