print("封装：")
Object ={}

print("实例化 new方法：")
function Object:new()
	local obj ={}
	self.__index=self
	setmetatable(obj,self)
	return obj
end

print("继承 subClass方法：")
function Object:subClass(className)
	_G[className] = {}
	local obj = _G[className]
	self.__index=self
	setmetatable(obj,self)
	obj.base = self
end

print("声明 GameObjet类：")
Object:subClass("GameObject")
GameObject.posX = 0;
GameObject.posY = 0;
function GameObject:Move()
	self.posX = self.posX+1
	self.posY = self.posX+1
end

print("实例化 obj obj2对象：")
local obj = GameObject:new()
print(obj.posX)										--输出：0
obj:Move()
print(obj.posX)										--输出：1

local obj2 = GameObject:new()
print(obj2.posX)									--输出：0
obj2:Move()
print(obj2.posX)									--输出：1

print("多态 Player的Move方法：")
GameObject:subClass("Player")
function Player:Move()
	self.base.Move(self)
	print("Player：("..self.posX..","..self.posY..")")
end
local p1 = Player:new()
print(p1.posX)										--输出：0
p1:Move()											--输出：Player(1,1)
print(p1.posX)										--输出：1