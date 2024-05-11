print("*********复杂数据类型——表2——类/结构体********")
--lua中是默认没有面向对象的 需要我们自己来实现 成员变量 成员函数...等

print("*********类/结构体的实现********")
--[[注意事项：
1、表内声明/添加 变量、方法
		（1）变量属性——直接声明添加			参照age、sex属性
		（2）方法：
		第一种：表名.属性 或 表名.方法  		参照Up()函数
		想要在表内部函数中 调用表本身的属性或者方法，一定要指明是谁的

		第二种：在自己内部调用自己属性或者函数的方法：传参
		把自己作为一个参数传进来 在内部访问		参照Learn()函数

		第三种：使用冒号：来声明函数	会隐性/默认的把调用者 作为第一个参数传入方法中 
		lua中 关键字 self 表示 默认传入的第一个参数 参照Speak3()函数
2、表外添加/声明变量、方法
		第一种：表名.属性 或 表名.方法 		参照name属性、Speak/Speak2方法
]]

--在声明表时，从表内部添加表的变量和方法
Student={
	age =1, 											--年龄
	sex = true, 										--性别
	Up = function() 									--函数，不用申明函数名
		--错误使用方法： print(age) 这里的age不是表里的age，是全局变量age
		print(Student.age)								
		print("我成长了")
	end,
	Learn =function(t)
		print(t.sex)  
		print("好好学习，天天向上")
	end
}

print("外部声明属性使用点.	表名.变量")
Student.name ="唐老狮"

print("外部声明方法使用点.	表名.方法")
Student.Speak = function()
	print("说话")
end
function Student.Speak2()
	print("说话2")
end

print("外部声明方法使用冒号：	表名：方法")
function Student:Speak3()
	print(self.name.."说话")	
end

print("对表的属性、方法的调用：")
--C#要是使用类：实例化对象new 静态直接点.xx
--Lua中类的表现 更像是一个类中有很多 静态变量和函数
print(Student.age)
print(Student.name)
--内部声明的函数的调用/使用
Student.Up()										
Student.Learn(Student)								--第二种 外部使用点.调用 		并把自己作为参数传入
Student:Learn()										--第三种 外部使用冒号：调用   会默认把自己当参数传入
--外部声明的函数的使用调用
Student.Speak()										--第一种 使用点.
Student.Speak2()									--第一种 使用点.
Student.Speak3(Student)								--第二种 使用点.调用 			并把自己作为参数传入
Student:Speak3()									--第三种 使用冒号：			会默认把自己当参数传入

print("*********面试考点：Lua中点.和冒号：的区别以及self********")
--Lesson15面向对象里会再次提到使用 点 冒号 self

--[[Lua中点.和冒号：的区别是什么？以及self的使用
点.   正常的调用函数：调用成员方法时，有什么参数传入什么参数			Student.Learn(Student)
冒号: 会默认把调用者作为第一个参数传入方法中						Student:Learn()
self  代表第一个传入的参数，参考Speak3的声明与使用
注：self不是this!!!!!!
]]

--闭包例子：
Student={
	name="xxx",
	Learn =function(t)
		print(t.name ) 
	end
}
function Student:Speak2()
	print(self.name.."说话")
end
Student.Learn(Student)			--把Student作为参数传入
Student:Learn()					--冒号 即把Student作为参数传入，
Student.Speak2(Student)			--把Student作为参数传入
Student:Speak2()				--只有一个参数，故Speak里的self 表示就是Student的意思