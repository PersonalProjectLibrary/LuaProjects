--lua中有8种变量类型：4种简单的，4种复杂的
print("*********变量********")
--lua当中的简单的4种变量类型
--nil number string boolean
--lua中所有的变量申明 都不需要申明变量类型 它会自动判断类型
--类似C#里面的 var
--lua中的一个变量 可以随便赋值  ——自动识别类型
--通过 type 函数 我们可以得到变量的类型 type返回值是string类型

--lua中使用没有申明过的变量，不会报错 默认值 是nil
--b是从来没有赋值过的对象
print(b)

--nil 有点类似 C#中的null
print("*********nil********")
a = nil
print(a)
--查看a的类型：nil
print(type(a))
--查看type(a)的类型/查看type的返回值类型：string
print(type(type(a)))

--number 所有的数值都是number
print("*********number********")
a=1
print(a)
--查看a的类型：number
print(type(a))

a=1.2
print(a)
print(type(a))

--string lua里 没有char
print("*********string********")
a = "12312"
print(a)
--查看a的类型：string
print(type(a))

--字符串的申明 使用单引号或双引号包裹
a='123'
print(a)
print(type(a))

print("*********boolean********")
a=true
print(a)
a=false
print(a)
--查看a的类型：boolean
print(type(a))

--复杂的4种数据类型
--函数 function
--表 table 
--数据结构 userdata
--协同程序 thread(线程)