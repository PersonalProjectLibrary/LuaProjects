print("*********函数********")
--函数一定跟着关键字function的
--function 函数名()
--end

--a = function()
--end

--函数申明之前不能调用，函数的调用必须放到申明后

print("*********无参数无返回值********")
function F1()  --申明函数
	print("F1函数")
end
F1()  --调用函数

--有点类似C#中的 委托和事件
F2 = function()
	print("F2函数")
end
F2()

print("*********有参数********")
--不需要指定参数类型；
function F3(a)
	print(a)
end
F3(1)
F3("str")
F3(true)
--传入的参数 和函数参数个数不匹配
--不会报错 只会补空nil 或丢弃多传入的参数
F3()
F3(7,8,9)

print("*********有返回值********")
--lesson12里有重复再次讲解	多返回值
function F4(a)
	return a,"123",true
end
--多返回值时 在前面申明多个变量来接取即可
--如果变量不够 不影响 只接取对应位置的返回值
--如果变量多了 不影响 直接赋值nil
temp,temp2 = F4("1")
print(temp)
print(temp2)

temp,temp2,temp3 = F4("1")
print(temp)
print(temp2)
print(temp3)

temp,temp2,temp3,temp4 = F4("1")
print(temp)
print(temp2)
print(temp3)
print(temp4)

print("*********函数的类型********")
--函数的类型 就是 function
F5 = function()
	print("123")
end
print(type(F5))

print("*********函数的重载********")
--重载：函数名相同 参数类型不同 或参数个数不同
--lua中 函数不支持重载 默认调用最后一个申明的函数
function F6()
	print("唐老狮帅帅的")
end

function F6(str)
	print(str)
end

--调用的是F6(str)，而不是函数F6()
--调用F6(str)，此时无参默认传入的参为nil
F6()

print("*********变长参数********")
--括号内参数填三个点... 表示变长参数

function F7(...)
	arg ={...}   --变长参数使用 用一个表存起来 再用
	for i=1,#arg do --在表名前用#，获取表的长度，同获取字符串长度方法，#是通用的获取长度的关键字
		print(arg[i])
	end
end

F7(1,false,"str")

print("*********函数嵌套********")
--相当于在函数里申明另一个函数F9再把函数F9返回出去
function F8()
	F9 = function()
		print("123")
	end
	return F9
end

f9 = F8() --用变量f9，存储F8()

-- 执行f9，即执行F8()，
-- 执行F8()时，会执行内部嵌套的F9()函数
f9() 

--上面写法改成 直接返回函数的写法
function F8()
	return function() -- 这里不要给函数取名了，直接返回函数变量
		print("456")
	end
end
f9 = F8()
f9() 

print("*********面试考点：函数嵌套--闭包********")
--[[ 在lua里闭包的体现是什么？
在函数里面返回一个函数，改变这个函数里面临时变量的生命周期。
换句话说：
里面的函数引用外面函数的一个变量，延长变量声明周期不被释放。
]]
--闭包
function F9(x)
	--改变传入的临时参数x的生命周期
	return function(y)
		return x+y
	end
end
--[[F9（x）的变量x，被function（y）函数包裹进去，也就是被闭包了，
也就实现了：对F9传入的参数x的生命周期改变的需求。]]

f10 = F9(10) --F9(10) 是返回function(y)，其中function(y)里的x被赋值为10
print(f10(5)) --f10(5),即 function(5) return 10+5 end，即15

--f10(5)，通过F9（10）延长了变量10的生命周期，
--直到f10(5)的参数5传进来，执行function（y）里的两个参数相加语句后，
--才释放F9(10)传进来的10变量