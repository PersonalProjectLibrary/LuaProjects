print("*********自带库、常用第三方库********")
--之前提过的 提供的公共方法 string table
--除了下面的常用三方库，还可以从去了解_G里了解（末尾讲解）

print("*********时间相关 系统时间库os********")
print("系统时间："..os.time())									--从1990年到现在的秒数

--带当前时间 转化后的系统时间
print("从指定时间起："..os.time({year =2024,month = 5,day = 1}))

--获取更多时间参数	os.date("*t")	有固定格式写法
local nowTime = os.date("*t")									--表的形式存储时间
print(nowTime)													--输出：表
print("获取指定时间："..nowTime.year.."年"..nowTime.month.."月"..nowTime.day.."日")
for i,v in pairs(nowTime) do
	print(i,v)													--输出时间表里的全部数据
end

print("*********数学运算 math库********")
print("绝对值："..math.abs(-11))									--输出：11
print("弧度转角度："..math.deg(math.pi))							--输出：180
print("三角函数："..math.cos(math.pi))							--输出：180	注：传弧度进去
print("2.6向下取整："..math.floor(2.6))							--输出：2
print("2.6向上取整："..math.ceil(2.6))							--输出：3
print("1,2取最大值："..math.max(1,2))							--输出：2
print("1,2取最小值："..math.min(1,2))							--输出：1
print("3.5小数整数分离：")
print(math.modf(3.5))											--输出：3 0.5 
print("幂运算："..math.pow(2,5))									--输出：32
--随机数：要先设置 随机数种子 
--否则不管怎么运行，随机数值都是第一次运行出来的值
print("不设置随机数种子："..math.random(100))						--输出：不管怎么重运行，都是1
print("不设置随机数种子："..math.random(100))						--输出：不管怎么重运行，都是57

--设置随机数种子
math.randomseed(os.time())										--传的参数，可以随便传参
print("设置种子后："..math.random(100))							--输出：受随机数种子影响 种子变才变
print("设置种子后："..math.random(100))							--输出：每次运行都不一样

--参考弹幕 这样设置随机数种子
math.randomseed(tostring(os.time()):reverse():sub(1,7))			--随机数种子总是变的
print("优化："..math.random(100))								--输出：每次运行都不一样
print("优化："..math.random(100))								--输出：每次运行都不一样

print("81的开方："..math.sqrt(81))								--输出：9

print("*********路径 package********")
--lesson11-局部全局变量及脚本卸载加载里，多脚本运行里使用到路径相关简单知识
print("打印lua脚本加载路径："..package.path)						--路径上不同文件夹之间用分号;隔开
--可以修改路径
package.path = package.path..";C:\\"							--添加路径;C:\ 其中C：和\中间多个转义字符\
print("修改后的路径："..package.path)								--对比上次路径输出，尾部多了 ;C:\

print("除了下面的常用三方库，还可以从去了解_G里了解：")
--_G里每个表都是个库
for k,v in pairs(_G) do
	print(k,v)
end