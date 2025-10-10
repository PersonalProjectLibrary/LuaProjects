--方法1：使用EmmyLua-Unity插件，运行->附加到进程->选择Unity.exe程序进行附加
--然后就可以正常断点调试了

--方法2：使用配置EmmyDebuggerNew，
--如果是Tcp(IDE Connect debugger)，对应dbg.tcpListen('localhost', 9966)，先运行Unity，再执行调试
--如果是Tcp(debugger Connect IDE)，对应dbg.tcpConnect('localhost', 9966)，先执行调试，再运行Unity
--然后在lua运行入口脚本复制执行下面注释掉的语句
--(本项目lua入口就是当前脚本，去掉注释符号即可直接使用，注意替换为自己电脑里emmy_core.dll的路径)

--[[
local dbg = package.loadlib('C:/Users/13066/AppData/Roaming/JetBrains/IntelliJIdea2023.3/plugins/EmmyLua/debugger/emmy/windows/x64/emmy_core.dll', 'luaopen_emmy_core')()
--dbg.tcpConnect('localhost', 9966)
dbg.tcpListen('localhost', 9966)
]]
--print(debug.getinfo(1).source)--这是查看当前脚本文件的api方法

--注，之前附加调试正常连接后，断点不执行，以及EmmyluaDebugger(NEW)调试，连接后，切换场景就断开连接不断点
--因为随着场景切换，对应的HotFix脚本也被销毁了，导致不能进行断点调试
--项目里，单独建立空对象绑定HotFix脚本，并设置随场景切换不销毁，始终存在，这样两种断点调试方法都正常执行了

print('测试：123')

print('———————————1.1————————')
local UnityEngine = CS.UnityEngine;

print('———————————1-宝箱领取的金币钻石拥挤问题修复————————')
xlua.hotfix(CS.Treasure,'CreatePrize',function(self)
    for i = 0,4,1 do
        local go = UnityEngine.GameObject.Instantiate(self.gold,self.transform.position + UnityEngine.Vector3(-10 + i * 40, 0, 0),self.transform.rotation)
        go.transform.SetParent(go.transform,self.canvas)

        local go1 = UnityEngine.GameObject.Instantiate(self.diamonds,self.transform.position + UnityEngine.Vector3(0, 40, 0) + UnityEngine.Vector3(-10 + i * 40, 0, 0),self.transform.rotation)
        go1.transform.SetParent(go1.transform,self.canvas)
    end
end)

print('———————————2-玩家金币钻石不够时没有相应处理修复————————')
xlua.private_accessible(CS.Gun)         --这样就可以访问修改Gun里的私有对象了
xlua.hotfix(CS.Gun,'Attack',function(self)
    if UnityEngine.Input.GetMouseButtonDown(0) then

        if self.gold < 1 + (self.gunLevel - 1) * 2 or self.gold == 0 then
            return                      --修复玩家金币不够时没有相应处理
        end

        self.bulletAudio.clip = self.bulletAudios[self.gunLevel - 1]
        --self.bulletAudio.Play(self)   --第一种调用方式
        self.bulletAudio:Play()         --第二种调用方式

        if self.Butterfly then
            UnityEngine.GameObject.Instantiate(self.Bullets[self.gunLevel - 1],self.attackPos.position,self.attackPos.rotation * UnityEngine.Quaternion.Euler(0, 0, 20))
            UnityEngine.GameObject.Instantiate(self.Bullets[self.gunLevel - 1], self.attackPos.position, self.attackPos.rotation * UnityEngine.Quaternion.Euler(0, 0, -20));
        end

        UnityEngine.GameObject.Instantiate(self.Bullets[self.gunLevel - 1], self.attackPos.position, self.attackPos.rotation);

        if not self.canShootForFree then
            self:CurrencyChange(CS.CurrencyType.Gold, -1 - (self.gunLevel - 1) * 2)
        end

        self.attackCD = 0
        self.attack = false
    end
end)