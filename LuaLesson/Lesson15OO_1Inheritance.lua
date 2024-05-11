print("*********面向对象——继承********")
--[[C# class 类名：继承类
	lua 写一个用于继承的方法 来实现继承功能

	知识点 _G表 可参考Lesson11里介绍
	_G 是总表 所有声明的全局变量 都以键值对的形式存在_G中
	参考lesson10_Dic的字典 _G的一种用法：通过_G['键名']=值 _G.属性名 =值 的形式，写入/修改全局变量
	]]

Object = {}
Object.id = 1
function Object:Fun()									--Object里的一个普通方法Fun()
	print(self.id)
end
function Object:new()									--用于实现实例化的方法new()
	local obj = {}
	self.__index = self;
	setmetatable(obj,self)
	return obj
end
function Object:subClass(className)						--利用_G特性写一个用于继承的方法subClass()
	_G[className] = {}									--构建一张空表放入_G中
	--写相关继承的规则	
	--用到元表的知识  可参考Lesson15OO_Encapsulation里元表的使用
	self.__index = self
	local obj = _G[className] 							--将声明的空表取出进行设置
	setmetatable(obj,self)
end

print("声明一个Person类，继承Object")
Object:subClass("Person")								--声明一个指定名字的表通过subClass方法继承Object
print(Person.id)										--输出：1  因为继承关系/函数 自身没有id但继承父类id
local p1 = Person:new()									--对person实例化赋值给p1
print(p1.id)											--输出1 成功实例化 p1元表是person person的index是自己

p1.id = 100												--修改也只修改自己属性
print(p1.id)											--输出：100
print(Person.id)										--输出：1
print(Object.id)										--输出：1

Object:subClass("Monster")								--新声明类Monster，继承于Object
local m1 = Monster:new()								--实例化Monster 为m1
print(m1.id)											--输出：1