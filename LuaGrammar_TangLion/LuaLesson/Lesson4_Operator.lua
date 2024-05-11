print("*********运算符********")
print("*********算数运算符********")
-- + - * / % ^
--没有自增 自减 ++ --
--没有复合运算符 += -= /= *= %=
--字符串可以进行 算数运算符操作 字符串会自动转换为number数值
print("加法运算"..1+2)
a = 2
b = 5
print(a+b)
print("123"+2)
print("123.3"+3)
print("减法运算"..1-2)
print("123.3"-3)
print("乘法运算"..1*2)
print("123.3"*3)
print("除法运算"..1/2)
print("123.3"/3)
print("取余运算"..1%2)
print("123.3"%3)
print("幂运算"..2^5)
print("123.3"^3)

print("*********条件运算符********")
--大于> ，小于< ，大于等于>= ，小于等于<= ，等于== ，不等于~=
print(3>1)
print(3<1)
print(3>=1)
print(3<=1)
print(3==1)
print(3~=1)

print("*********逻辑运算符********")
--lesson12里有重复再次讲解	逻辑运算符
--C#中 逻辑与&& ，逻辑或|| ，逻辑非/取反! ，"短路"
--lua中 逻辑与and ，逻辑或or ,逻辑非/取反not

print("*****and or 在lua中也支持 短路 操作*****")
--and 有假则假，全真才真：and运算符前面的满足才继续判断后面的条件
print( true and false)								--输出：false
print( true and true)								--输出：true

--or 有真则真，全假才假：or运算符前面的不满足才继续判断后面的条件
print( true or false)								--输出：true
print( false or false)								--输出：false

print( not false)									--输出：true

print("*****print函数不会返回boolean*****")
print(true and print("123"))						--输出：123 nil
print(false and print("123"))						--输出：false

print("*********三目运算符********")
--不支持三目运算符 ？：

print("*********位运算符********")
--不支持位运算符& |，需要自己实现，不过现在有不少第三方库支持实现