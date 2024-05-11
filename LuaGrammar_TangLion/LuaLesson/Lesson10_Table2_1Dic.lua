print("*********复杂数据类型——表2——字典********")
--字典是由键值对构成的

a={["name"]="唐老狮",["age"]="14",["1"]=5}

print("*********字典的申明********")
print(a["name"])									--访问单个变量 用[键] 来访问
print(a.age)										--还可以用 .成员变量 的形式得到值

print(".成员变量 方法 得到值：")
print(a["1"]) 										--print(a.1) 是错误的写法，会报错
a["name"]="TLS"										--其中成员变量不能是数字、汉字等不支持的格式
print(a.name);

print("字典新增：")
a["sex"]=false;										--直接新增
print(a.sex)

print("字典删除：")
a.sex = nil     									--赋值nil会把内存回收，等于删除了
print(a["sex"]) 									--删除后的字段 输出：nil
print(a.test)   									--验证 从没有过的字段 输出：nil

print("*********字典的遍历********")
--不能使用ipairs遍历，ipairs只能遍历索引是连续数字的
print("可以传多个参数 一样可以打印出来：")				--如果要模拟字典 遍历一定用pairs
for k,v in pairs(a) do
	print(k,v,3)
end

print("遍历只输出单个键：")
for k in pairs(a) do
	print(k)
	print(a[k])
end

print("省略写法：")									--省略写法（实际也还是遍历了键）
for _,v in pairs(a) do
	print(v)
	print(_,v)
end
