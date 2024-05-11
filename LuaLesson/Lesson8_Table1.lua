print("*********复杂数据类型 table********")
--所有的复杂类型本质都是table(表)

print("*********数组********")
a={}											--空表
b={1,true,"123",5.1,"test",nil}
c={1,true,"123",nil,"test",5.1}
d={1,true,"123",nil,nil,"test"}
e={1,true,"123",nil,"test",nil}
print(b)   										--输出的table:00509680 表示的是表的地址
print(b[0])										--lua中，索引从1开始
print(b[1])
print(b[2])

print(#a)										--#是通用的获取长度的关键字

--[[打印长度时，空会被忽略
如果表中（数组中）某一位变成nil 会影响#获取长度
最后元素是nil时，计算长度只会算到第一个nil
]]
print(#b) 										--输出 5 ，末尾nil,中间无nil，末尾nil被忽略
print(#c) 										--输出 6 ，末尾非nil，中间的nil没被忽略
print(#d) 										--输出 6 ，末尾非nil，中间多个nil都没被忽略
print(#e) 										--输出 3 ，末尾nil，从中间第一个nil位置截断，认为后面都是空

print("*********数组的遍历********")
print("遍历数组b，末尾nil:")
for i=1,#b do
	print(b[i])
end

print("遍历数组c，中间一个nil:")
for i=1,#c do
	print(c[i])
end

print("遍历数组d，中间两个nil:")
for i=1,#d do
	print(d[i])
end

print("遍历数组e，中间和末尾nil:")
for i=1,#e do
	print(e[i])
end

print("*********二维数组********")
--lua实际没有数组，只是用表的形式体现出数组
a ={{},{}}										--表中的表
b = {{1,2,3},{4,5,6}}
print(b[1][1])
print(b[2][2])

print("*********二维数组的遍历********")
--存在的坑：同一维数组————nil存在对数组的影响
for i=1,#b do
	c = b[i]
	for j=1,#c do
		print(c[j])
	end
end
print("*********自定义索引********")
--自定义索引虽然穿插在非自定义索引中间，但数组整体还是依次排索引的
aa = {
	[0]=1,									 --虽然是lua索引是从1开始，但可以自定义索引0
	2,3,
	[-1]=4,									 --自定义索引-1，注可以自定义成这种，但平时不建议这样不规范的写法
	5  
}
--计算长度时，会忽略小于等于0的索引
print(#aa) 	 							--输出：3 ，对应aa里的2，3，5这三个，自定义索引[0],[-1]不认识，没获取

print(aa[0]) 							--输出：1
print(aa[-1])							--输出：4

print(aa[1]) 							--输出：2
print(aa[2]) 							--输出：3
print(aa[3]) 							--输出：5

--自定义索引跳跃性设置，如果只跳一格，不会断掉，长度受自定义索引最大值影响
print("打印自定义数组aa，bb,中间缺不连续的多个索引：")
print("索引1-6，缺少的索引2，4，只打印aa[1]")
aa ={
	[1]=1,
	[3]=3,
	[5]=5,
	[6]=6
}
print("长度："..#aa) 								
for i=1,#aa do
	print(aa[i])
end

print("索引1-6，缺少的索引3，5： 1-6都打印")
bb={
	[1]=1,
	[2]=2,
	[4]=4,
	[6]=6,
}
print("长度："..#bb) 
for i=1,#bb do
	print(bb[i])
end

print("打印自定义数组cc,中间缺连续的多个索引：")
print("索引1-6，6个元素，缺少的索引3，4，直接截断： 只打印1-2")
cc={
	[1]=1,
	[2]=2,
	[5]=5,
	[6]=6
}
print("长度："..#cc)
for i=1,#cc do
	print(cc[i])
end