print("*********协同程序：协程********")
fun =function()							--先创建个函数，后面作为协程调用的函数
	print("fun")
end

print("*********协程的创建********")
--第一种常用方式	固定函数创建协程：coroutine.create()		
co = coroutine.create(fun)
print(type(co))							--查看协程类型 输出 thread	本质线程对象

--也可以直接写成
co = coroutine.create(
	function()
		print("fun1")
	end
)

--第二种创建协程方式：coroutine.wrap()
co2 = coroutine.wrap(fun)
print(type(co2))						--输出协程类型 输出function	本质函数方法

co2 = coroutine.wrap(
	function()
		print("fun3")
	end
)

print("*********协程的运行********")
--第一种create 创建的协程的调用方法 coroutine.resume()
coroutine.resume(co)

--第二种wrap 创建的协程的调用方法	本质是函数方法，直接函数调用方法 相当于运行了协程
co2()

--两种协程方式：一个是多开子线程运行，一个是在主线程上运行？？？

print("*********协程的挂起********")
--使用 coroutine.yield()
fun2 =function()
	local i =1
	while true do						--死循环函数
		print(i)
		i=i+1
		coroutine.yield()				--协程的挂起函数
	end
end

co3 = coroutine.create(fun2)			--创建协程
coroutine.resume(co3)					--执行协程，因函数里有挂起函数，运行后就挂起不会一直执行，输出 1
coroutine.resume(co3)					--继续上次运行后，从挂起到恢复，再次继续执行协程			 输出 2
coroutine.resume(co3)					--不是重复执行fun2函数，从上次挂起处，继续执行while循环  输出 3

co4 = coroutine.wrap(fun2)				--同create方法创建的协程的	运行-挂起-运行-挂起
co4()
co4()
co4()

print("*********协程的挂起 yield 返回值")
fun3 =function()
	local i =0
	while true do
		i=i+1
		coroutine.yield(i)				--yield可以有返回值的，传参进去就相当于有返回值
	end
end

co5 = coroutine.create(fun3)			--create()方式创建的协程 默认返回boolean参数，表示协程是否启动成功	
isOk,tempI = coroutine.resume(co5)		--多参数返回 后面返回的参数才是我们希望返回的参数
print(isOk,tempI)						--输出 true 1
print(coroutine.resume(co5))			--输出 true 2


co6 = coroutine.wrap(fun3)				--wrap()方法创建的协程 没有默认参数，直接是我们希望返回的参数
print("返回值："..co6())					--输出 返回值：1
print("返回值："..co6())					--输出 返回值：2

print("*********协程的状态********")
print("*********协程的状态——关键字 status")
--固定的API coroutine.status(协程对象)		只能用于create()创建的协程，wrap()创建的协程会报错
--四种状态：dead 结束；		suspended 暂停；		running	进行中；		normal  活动但不在运行；
print(coroutine.status(co))				--输出 dead 表示协程已结束
print(coroutine.status(co3))			--输出 suspended 表示协程被挂起暂停

--外部无法得到 进行中 的协程状态
fun4 =function()
	print(coroutine.status(co7))		--协程运行过程中监测输出协程状态
end

co7 = coroutine.create(fun4)
coroutine.resume(co7)					--输出 running	在运行协程过程中输出协程状态

print("*********协程的状态——关键字 running")
--coroutine.running() 这个函数可以得到 当前正在运行的协程的	线程号
--两种协程创建方式都支持使用
print(coroutine.running())				--输出 nil  目前没有正在运行的协程

fun5 =function()
	print(coroutine.running())			--协程运行过程中监测输出协程状态
end

co8 = coroutine.create(fun5)
coroutine.resume(co8)					--输出 thread: 00F220C0	在协程运行中执行了coroutine.running()

co9 = coroutine.wrap(fun5)
co9()									--输出 thread: 00F224E0	协程每次执行时的协程号会变
