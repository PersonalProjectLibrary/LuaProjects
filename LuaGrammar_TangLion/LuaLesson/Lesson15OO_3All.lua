print("*********面向对象——All********")

print("封装 到new()的实现 已实现封装")
Object ={}											--所有对象的基类 Object

print("实例化对象的方法 new")
function Object:new()								--目的：给空对象设置元表 以及index
	local obj ={}									--一定是本地变量 若是全局变量 每次都是改同一个表
	self.__index=self								--把调用者的index设置自身
	setmetatable(obj,self)							--设置调用者/创建者 为 实例化对象的元表
	return obj
end

print("继承")
function Object:subClass(className)
	_G[className] = {}								--在_G表里添加带名的空表
	local obj = _G[className]						--后面修改的都是局部表 避免每次都是修改同个全局表
	self.__index=self								--调用者的index设置为自己
	setmetatable(obj,self)							--调用者 作为 生成的新表的元表
	obj.base = self									--为新表添加父类属性 base
end

--一般情况下 不会直接使用Object 多是用来继承
print("声明GameObjet 继承Object 用于其他类继承使用")
Object:subClass("GameObject")
GameObject.posX = 0;
GameObject.posY = 0;
function GameObject:Move()
	self.posX = self.posX+1
	self.posY = self.posX+1
end

print("实例化对象使用")
local obj = GameObject:new()
print(obj.posX)										--输出：0
obj:Move()
print(obj.posX)										--输出：1

local obj2 = GameObject:new()
print(obj2.posX)									--输出：0
obj2:Move()
print(obj2.posX)									--输出：1

print("声明Player继承GameObject 实例化对象多态/重载")
GameObject:subClass("Player")
function Player:Move()
	self.base.Move(self)							--base调用父类方法 用点. 自己传第一个参数
	print("Player：("..self.posX..","..self.posY..")")
end
local p1 = Player:new()
print(p1.posX)										--输出：0
p1:Move()											--输出：Player(1,1)
print(p1.posX)										--输出：1