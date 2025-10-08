
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

        UnityEngine.GameObject.Instantiate(self.Bullets[gunLevel - 1], self.attackPos.position, self.attackPos.rotation);

        if not self.canShootForFree then
            self:CurrencyChange(CS.CurrencyType.Gold, -1 - (self.gunLevel - 1) * 2)
        end

        self.attackCD = 0
        self.attack = false
    end
end)