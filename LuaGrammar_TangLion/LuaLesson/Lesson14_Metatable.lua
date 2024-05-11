print("*********元表********")
--[[元表	类似与父类	下面把有元表的表，自称为 子表
	任何表变量 都可以作为另一个表变量的元表
	任何表变量都可以有自己的元表
	当我们子表中进行一些特定操作时 会执行元表中的内容
	更新表后	需要重新绑定下元表
	]]

print("*********设置/获取元表********")
meta ={}
myTable = {}
setmetatable(myTable,meta)						--设置元表	setmatetable(子表，元表)
getmetatable(myTable)							--得到/获取元表	getmetatable(子表)

print("*********特定操作——__tostring	注意都是两个下划线********")
--当子表要被当作字符串使用时 会默认调用这个元表中的tostring方法
meta2 ={
	__tostring =function()
		return "唐老狮2"
	end
}
myTable2 = {}
setmetatable(myTable2,meta2)
print(myTable)									--输出：元表的表地址		元表meta中没有设置tostring 
print(myTable2)									--输出：唐老狮2			元表meta2中设置了tostring

--只支持子表获取元表设置，元表无法获取子表设置
--有时候需要用子表的设置，而不是元表的设置，可以通过传参设置
--tostring添加参数，会默认把调用的子表作为参数传入，方便执行/重载为子表所需设置
meta2 ={ 
	__tostring =function(t) 
		return t.name 
	end 
}
myTable2 = { name = "xxx" }
setmetatable(myTable2,meta2)					--更新表后需要重新绑定下元表关系
print(myTable2)									--输出：xxx		使用元表的tostring，但引用了子表设置

print("*********特定操作——__call********")
--表直接作为函数使用	没有元表或__call函数，直接把表作为函数使用会报错
--当子表被当作一个函数来使用时	会默认调用元表中的__call中的内容
meta3 ={ 
	__call = function() 
		print("唐老狮你好") 
	end
 }
myTable3 = {}
setmetatable(myTable3,meta3)
myTable3()										--输出：唐老狮你好

--子表作为函数调用时，会默认把自己(子表)作为第一个参数以字符串形式传入
--优化：添加tostring函数，传入的子表时，转化为调用tostring的设置
meta3 ={
	__call = function(a)						--call传参会默认把子表作为第一个参数传入
		print(a)								--输出时，打印的是子表 而不是传入的参数
	end
}
meta4 ={
	__tostring =function(t)							
		return t.name 
	end,
	__call = function(a,b)						--子表以字符串形式作为第一个参数a传入会调用tostring
		print(a,b) 								--第二个参数b才为我们自定义传入的参数123
	end
}
myTable3 = { name = "yyy" }
myTable4 = { name = "yyy" }
setmetatable(myTable3,meta3)
setmetatable(myTable4,meta4)
myTable3(123)									--输出：表	既不是yyy 也不是123
myTable4(123)									--输出：yyy  123

print("*********特定操作——__运算符重载********")
--这里举一个运算符重载操作，其他运算符重载类似
--当子表调用运算符时，会调用元表里对应的运算符方法	相当于运算符重载
--运算符+	__add
meta5 ={
	__add = function(t1,t2)
		return t1.age+t2.age
	end
}
myTable5 = { age = 10 }
temp = { age =5 }
setmetatable(myTable5,meta5)
--正常情况下，两个表直接使用+运算符 会报错，这里元表有重载，会执行元表里的+运算符方法
print(myTable5+temp)							--输出：15

--[[其他运算符函数——关键字：
	运算符-	__sub；	运算符*	__mul;	运算符/	__div;	运算符%	__mod；	运算符^	__pow;
	条件运算符==	__eq；	运算符<	_lt;	运算符<=	__le;	>,>=,~=没有对应的运算符
	拼接运算符..	__concat
	注：如果要用条件运算符 来比较两个对象		注意返回值是boolean;
	另外	这两个对象的元表一定要一致/同一个	才能准确调用方法;
	]]

meta5 ={
	__eq = function(t1,t2)
		return true
	end
}
myTable5 = { age = 10 }
temp = { age =5 }								--myTable5与temp不是同个元表
temp2 ={ age =5 }								--myTable5与temp2是同个元表
setmetatable(myTable5,meta5)
setmetatable(temp2,meta5)						--同个元表才正确准确调用：
print(myTable5==temp)							--输出：false
print(myTable5==temp2)							--输出：true	

print("*********特定操作——__index、rawget和__newIndex、rawset********")
--__index 有点类似get
--__index：当子表中找不到某一个属性时	会到元表中__index指向的表里面找属性
meta ={age ="meta"}
myTable = {}
temp ={age ="temp"}
setmetatable(myTable,meta)
print(myTable.age)								--输出 nil	myTable中没有age属性，虽然元表有age属性
meta.__index =temp								--对mytable的元表meta添加index，index指向temp表
print(myTable.age)								--输出 temp 	使用元表的index指向的表里的age属性

meta ={__index = {age="内部声明"}}				--index指向的表也可以直接元表内直接申明

--meta ={_index = mate}							--这种index是错误的，mate还没初始化成功，会报错

myTable = {}
setmetatable(myTable,meta)
print(myTable.age)

meta={age ="外部"}
meta.__index=meta 								--index可以设置成其他表也可以设置成自己
setmetatable(myTable,meta)						--注意表更新后重新绑定元表
print(myTable.age)

--获取属性，获取不到时，去index指向的元表找 一层层向上找，最后找不到输出nil
meta2 ={}
myTable2 = {}
tMeta ={}
temp ={}
target ={age ="target"}
setmetatable(myTable2,meta2)					--元表
setmetatable(temp,tMeta)
meta2.__index =temp2							--元表的index指向的表
tMeta.__index =target
print(myTable2.age)								--输出target		一层层往上找，最终找到target里的age属性

print("*********特定操作——__newindex 	类似set")
--newindex：当赋值时，如果赋值一个不存在的属性 那么会把这个值	赋值到newindex所指的表中 不会修改自己
--newindex 有点类似set		修改属性，当目标属性找不到时，赋值到newindex指向的表里的属性
--类似index也可一层层往上找
meta ={}
myTable = {}
setmetatable(myTable,meta)
print(myTable.age)								--输出 nil 	表中无age属性
myTable.age = "333"								--虽然表中无age属性，这个赋值相当在表中新增个age属性
print(myTable.age)								--输出 333	子表自身多了age属性

meta2 ={}
myTable2 = {}
temp = {}
setmetatable(myTable2,meta2)
meta2.__newindex=temp							--设置了元表的newindex
--修改自身没有的属性时，元表newindex指向的表（temp）会新增/修改对应属性，而不会往自身（myTable2）上添加age属性
myTable2.age = "555"
print(myTable2.age)								--输出：nil
print(temp.age)									--输出：555

print("*********特定操作——rawget 对比index")
--rawget 类似强制get读取		rawget(表，属性)
--会忽略Index的设置		当我们使用时 不论有没有元表，都只会去找子表（自己身上）有没有这个属性
meta ={}
myTable = {}
temp ={age ="get"}
setmetatable(myTable,meta)
meta.__index = temp
--正常情况下，表里没属性，找元表的index指向的表里属性	rawget强制获取表里的属性，不管有没有设置元表、index
print(myTable.age)								--输出：get
print(rawget(myTable,"age"))					--输出：nil
print(rawget(temp,"age"))						--输出：get

print("*********特定操作——rawset 对比newindex")
--rawset 类似rawget	类似强制set写入	rawset(表，属性，值)
--会忽略newIndex的设置	只会改自己身上(子表)的属性变量
meta ={}
myTable = {}
temp ={age ="table"}
setmetatable(myTable,meta)
meta.__newindex = temp
print(myTable.age)								--输出： nil
print(temp.age)									--输出： table
--正常情况下，表里没属性时，修改元表newindex指向的表里属性
myTable.age = "my"									
print(myTable.age)								--输出： nil
print(temp.age)									--输出： my
--rawset时，强制修改/新增表里的属性，忽略元表、newindex
rawset(myTable,"age",1)							
print(myTable.age)								--输出： 1

--无论有没有元表、设置newindex，rawget只会强制修改/新增自身属性，
meta ={}
myTable = {}
temp ={age ="000"}
setmetatable(myTable,meta)
meta.__newindex = temp
rawset(myTable,"age",2)
print(myTable.age)								--输出： 1 		被强制修改
print(temp.age)									--输出：table    不被影响