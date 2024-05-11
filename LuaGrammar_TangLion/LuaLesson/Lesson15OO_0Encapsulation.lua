print("*********面向对象——封装********")
--[[面向对象 类 其实都是基于 table 来实现
	会用的 元表 相关的知识点 可参考Lesson14关于元表的讲解
	点 冒号 self 相关知识点 可参考lesson10_Table3_Class里的讲解

	C#里要把类实例化成一个对象才能去使用，除非是静态类或类里有静态变量，才能通过 类.属性 的形式去使用
	lua里 类更像是一种静态类 类里全是静态的属性、静态的方法，使用时都是通过 类.属性 类.方法 来使用
	冒号:	是会自动将调用这个函数的对象 作为第一个参数传入	的写法
	self  代表我们/默认传入的第一个参数
	]]

Object = {}										--声明一个封装类
Object.id = 1 									--直接给object加个属性
function Object:Fun()							--建议声明函数用冒号，有时候可能需要调用函数的对象里的属性
	print(self.id)
end

--[[为lua里的对象 实现一个实例化方法：
	为Object 实现一个 new 的方法
	new函数的目的/作用： 返回出一个新的对象/变量 本质上返回出去一个表
	]]
function Object:new()							--注意冒号的使用
	local obj = {}								--声明本地变量，只会在函数内部存在，不会作为全局变量
	--使声明出来的对象是按原对象来声明出来的，这里使用继承/元表 __index
	self.__index = self;						--把自己作为index指向的表
	setmetatable(obj,self)						--把self作为声明的对象的元表
	return obj
end

local myObj = Object:new()						--声明一个Object对象
print(myObj.id)									--输出：1	验证 声明的对象和声明的模板对象保持一致
myObj:Fun()										--输出：1
myObj.id = 2 									--设置新建类的属性
print(Object.id)								--输出：1 对新建类进行操作 不影响原本的父类/父本
print(myObj.id)									--输出：2
myObj:Fun()										--输出：2 fun函数传入的是调用者 这里的self代指的是myObject
