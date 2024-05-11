print("*********复杂数据类型——表2——表的公共操作********")
--表中 table 提供的一些公共方法的讲解

t1 = {{age=1,name="123"},{age=2,name="xxx"}}
t2 = {name ="唐老狮",sex = true}

print("*********插入insert********")
print(#t1)
table.insert(t1,t2) 							--把t2插入到t1表后面，作为t1的一个元素
print(#t1)

print(t1[1])  									--输出的00A39320 表示表的地址
print(t2)
print(t1[3])  									--可以看到和t2的地址一样
print(t1[3].name)								--进一步验证，输出 唐老狮

print("*********删除指定元素remove********")
--remove方法 不传参数
table.remove(t1)								--传表进去 会移除最后一个索引的内容
print(#t1)
print(t1[1].name)
print(t1[2].name)
print(t1[3])
--remove方法 传两个参数
--第一个参数 指要移除内容的表；第二个参数 是要移除内容的索引
table.remove(t1,1) 								--把表t1的第一个元素删除
print(t1[1].name)

print("*********排序sort	默认升序********")
t2 ={5,2,7,9,5}
table.sort(t2)									--一般数组数字排序，其他格式得自定义排序规则
for _,v in pairs(t2) do
	print(v)
end

print("*********自定义规则排序sort	降序********")
--传入两个参数	第一个是用于排序的表	第二个是排序规则函数
table.sort( t2, function (a,b)
	if a>b then									-- 降序排序
		return true 							--理解成，如果a大于b，就交换位置 参考C#数组sort方法自定义排序
	end											--冒泡排序逻辑，相当于拉姆达写法，比它大的就左移
end )
for _,v in pairs(t2) do
	print(v)
end

print("*********拼接concat********")
tb = {"123","xyz","789","asd"}

--连接函数 用于拼接表中元素 返回值是一个字符串 一般用于数字、字符串拼接，其他格式不支持
--string = table.concat( tablename, ", ", start_index, end_index )
--第一个参数 是要拼接的表名； 第二个参数 是元素的分割符、连接符
str = table.concat( tb, "; ")	
print(str)
