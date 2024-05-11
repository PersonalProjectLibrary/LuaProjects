print("*********特殊用法********")
print("*********多变量赋值********")
--支持连续赋值
a,b,c = 1,false,"xxx"
print(b)

--如果后面的值不够 会自动补空 
--如果后面的值多了 会自动省略
a,b,c =2,true
print(c)

print("*********多返回值********")
--Lesson7里有讲解过 函数返回值
--多返回值，用几个变量接 就有几个变量
--少接少得，多接自动补空
function Test()
	return 10,20
end
a=Test()
print(a)												--输出 10

a,b,c = Test()
print(c)												--输出nil

print("*********逻辑运算符********")
--Lesson4里有讲解过 逻辑运算符 逻辑与and  逻辑或or
--特殊之处：
--and or 不仅可以连接 boolean 任何东西都可以连接
--在lua中 只有 nil 和 false 才认为是假
--注意逻辑运算符的 "短路"：and 有假则假	or 有真则真

print("*********and 有假则假")
--and 有假则假
print( 1 and 2)		--短路，输出 2 	先判断1，非nil和false，认为是真；再判断2 把2返回出去
print( 2 and 1)		--输出	1
print( 1 and nil)	--输出	nil
print(true and 1)	--输出	1
print( nil and 1)	--输出	nil 	先判断nil，认为是假，and有假则假 短路输出nil 会忽略对后面条件的判断

print("*********or 有真则真")
--同理 or 	有真则真
print( 1 or 2)		--短路，输出 1 	先判断1，非nil和false，认为是真；有真则真 会忽略对后面条件的判断
print( 2 or 1)		--输出	2
print( 1 or nil)	--输出	1
print(true or 1)	--输出	true
print( nil or 1)	--输出	1 		先判断nil，认为是假，or 有真则真；再判断1 把1返回出去


print("*********and or 模拟三目运算符")
--lua不支持三目运算符，但可以利用 and 和or 特性 模拟出三目运算符效果
--and有假则假，or有真则真
x = 3
y = 2
local res = (x>y) and x or y							--返回x	 三目运算符 ? :

print(res)						

x = 2
y = 3
local res = (x>y) and x or y							--返回y
print(res)						