print("*********变量********")
s = "双引号字符串"
print(s)
s = '单引号字符串'
print(s)

--获取字符串长度
print("*********字符串长度********")
--一个汉字 占3个长度
--英文字符 占1个长度
s="aBcdEfG字符串"
--#是通用的获取长度的关键字
print(#s)

print("*********字符串多行打印********")
--lua中也支持转义字符
--通过 转义字符 或双中括号 [[]] 来实现换/多行打印
print("123\n456")
s=[[我是
唐
老师
]]
print(s)
print([[我是
	唐
老师]])

print("*********字符串拼接********")
--通过两个点 .. 对字符串拼接
print("123".."789")
s1="123123"
s2=111
s3 = 456
print(s1..s2)
print(s2..s3)
--使用 string.format 方法拼接 占位符% + 不同含义字符
print(string.format("我是唐老师，我今年%d岁了",18))
--%d：与数字拼接
--%a：与任何字符拼接
--%s：与字符配对
--....等等自行百度

print("*********别的类型转字符串********")
--使用print打印时会默认转换string打印
--也可以使用显示转移tostring
a=true
print(tostring(a))


print("*********字符串提供的公共方法********")
--这些方法大部分不会改变原字符串，只是返回新字符串
str="aBcdEfGcd"
print(string.upper(str))								--小写转大写的方法upper
print(string.lower(str))								--大写转小写的方法lower
print(string.reverse(str))								--翻转字符串reverse
--字符串索引查找，lua的索引下标从1开始
--输出两个参数，第一个参数是查找到的起始索引，第二个参数是查找到的结束位置索引
print(string.find(str,"EfG"))
print(string.find(str,"c"))
print(string.sub(str,3))								--截取字符串sub，有多个重载
print(string.sub(str,3,5))
print(string.rep(str,3))								--字符串重复(拼接)rep，后面的参数表示重复拼接几次
print(string.gsub(str,"cd","**"))						--字符串修改gsub，返回的第二个数字参数 表示修改了几次
print(str)

--字符转 ASCII码，第二个参数表把指定位置的字符转化为ASCII码
a= string.byte("Lua",1)									--把Lua里的L转为ASCII码值输出 byte
print(a)
print(string.char(a))									--ASCCII码转字符 char
