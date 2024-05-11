print("*********多脚本执行********")
--lua的变量默认是全局的，只有函数参数变量是默认局部的

print("*********全局变量和本地变量********")
--全局变量
a=1
b="123"										--a,b都是全局变量
for i=1,2 do
	c="全局"									--c也是全局变量
end
print(c)									--出了循环，变量c依旧存在，c不是局部/临时变量

--本地（局部）变量 关键字 local
for i=1,2 do
	local  d = "本地"
	if i==1 then
	 print("循环中的d："..d)
	end
end
print(d)									--输入为 nil 不是 本地，因为d是局部变量，循环外找不到

--函数内变量的局部与全局
--若想变量不是全局的，需加local
fun = function()
	tt="funfun"
end
print(tt)									--输出 nil  函数没运行过，函数里的变量是局部的
fun()
print(tt)									--输出 funfun  函数运行过，函数里的变量也是全局的
Sad = function()
	local ss="sadsad"
end
print(ss)									--输出 nil  函数没运行过，函数里的变量是局部的
fun()
print(ss)									--输出 sadsad  函数运行过，但有关键字限制，函数里的变量还是局部的

--全局环境里/在方法/循环外声明局部变量
local mm ="555"
print(mm)									--输出 555  mm是局部变量 这个脚本里还是能读取，其他脚本里无法读取

print("*********多脚本执行********")
--关键字 require('脚本名')  单引号，双引号都可以
--同一路径内不需要路径直接文件名，不同路径需要把路径名写上
require("Test")								--输入 Test测试
print(testA) 								--Test脚本里的全局变量 testA
print(testLocalA)							--Test脚本里的局部变量 testLocalA

--如果是require加载执行的脚本 加载一次后不会再被执行
require("Test")								--没有输出 不会再执行Test脚本

print("*********脚本卸载********")
--Lesson16里有讲解路径相关知识
--package.loaded["脚本名"] 用于判断脚本是否被加载过，单引号 双引号都可以
--返回值是boolean 意思是 该脚本是否被执行
print(package.loaded['Test'])				--输出 true，表示被执行过

--卸载，对package.loaded["脚本名"]赋值为nil或false  
--注：loaded本质还是一个表，故卸载操作，是把对应字段赋值nil或false
package.loaded["Test"] = nil
print(package.loaded['Test'])				--输出 nil，表示没执行/加载过，或已卸载
require('Test')								--正常重新加载Test脚本

--require的另一个作用用法：接收其他脚本返回的参数
--require 执行一个脚本时 可以在该脚本最后返回一个外部希望获取的内容
print('*********require 另一种用法********')
package.loaded["Test"] = false				-- 上面加载过，先卸载
local testLA= require("Test")				-- 加载Test脚本 会输出 Test测试
print(testLA)								-- 可接收输出Test脚本的本地变量testLA的值： xxx
--require检查文件 如果需要使用文件内的局部变量，需要将其添加到表中，然后把该表返回

print("*********大G表********")
--大G表 固定写法： _G 	_G是一个总表(table)  
--它将我们声明的(这个脚本的)所有全局变量都存储再其中

print("_G的一种用法：")
--lesson15_Inheritance里有应用到大G表
--参考lesson10_Dic的字典：
--通过_G['键名']=值 _G.属性名 =值 的形式，写入/修改全局变量
_G["a"] = 10					
_G.b = "123"
print(a)						--输出 1
print(b)						--输出 123

print("*******遍历_G表：*******")
--本地变量 加了local的变量 不会存到大_G表中
for k,v in pairs(_G) do
	print(k,v)
end
