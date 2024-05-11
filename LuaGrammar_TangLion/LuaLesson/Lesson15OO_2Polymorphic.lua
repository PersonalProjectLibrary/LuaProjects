print("*********面向对象——多态********")
--用到之前 封装与继承 的知识
Object = {}
function Object:new()									--用于实现实例化的方法new()
	local obj = {}
	self.__index = self;
	setmetatable(obj,self)
	return obj
end
function Object:subClass(className)						--用于继承的方法subClass()
	_G[className] = {}
	local obj = _G[className]
	self.__index = self
	setmetatable(obj,self)
end

Object:subClass("GameObject")							--声明GameObject类，继承Object
GameObject.posX = 0										--往GameObject里添加属性、方法
GameObject.posY = 0
function GameObject:Move()
	self.posX = self.posX+1	
	self.posY = self.posY+1
	print("("..self.posX..","..self.posY..")")					
end
GameObject:subClass("Player")							--声明Player类，继承GameObject
local p1 =Player:new()									--实例化Player类为p1
p1:Move()												--输出：(1,1)	执行p1(实例化对象里)的Move()

print("*********多态********")
--[[相同行为 不同表现 就是多态；  相同方法 不同执行逻辑 就是多态
	就是 子类  相对于父类 同一个方法有不同的表现
	]]
print("1、重写子类方法，覆盖父类同名方法：")
--Player方法继承于GameObject方法，但Player里Move方法与GameObject的Move方法不同，
function Player:Move()									--同名重写即可实现
	print("重写Move")
end
p1.Move()												--Player的Move重写，p1执行的是重写后的逻辑

print("2、重写子类方法，且保留父类同名方法：")
--C#里重写方法的同时，保留父类方法逻辑，使用 base.方法
--在lua里 构建、实现base 功能的方法

--使用subClass方法时：父类:subClass(子类)；冒号默认传入调用者，调用者是父类，self指代的就是传进来的父类
function Object:subClass(className)						--这里重写了下 用于继承的方法subClass()
	_G[className] = {}
	self.__index = self									
	local obj = _G[className]
	obj.base = self										--定义个base属性 代表父类，方便调用使用父类的逻辑
	setmetatable(obj,self)								
end
--上面重写了下Object类，下面使用的base传参会用到父类，这里重新继承下GameObject
GameObject:subClass("Player")							
function Player:Move()									--重写Player的Move()
	print("保留父类Move")
	self.base:Move()									--使用base属性，保留父类的Move逻辑
end
local p1 = Player:new()									--重新更新本地变量，避免没有更新
p1:Move()												--输出：保留父类Move （1,1）

--上面多态存在的坑：不同对象使用的成员变量 是相同的(父类的)成员变量 不少自己的
local p2 = Player:new()									--新本地变量p2和p1公用同个父类
p2:Move()												--输出：保留父类Move （2,2）
p1:Move()												--输出：保留父类Move （3,3）

--[[分析坑存在的原因：以p1为例，p2同p1
对于实例化对象new方法：
	调用者:new() 实例化对象 其中： self==调用者，obj==实例化出来的对象，
	因 	self.__index = self; setmetatable(obj,self)
	被实例化出来的对象 的元表是 调用者;	实例化对象的元表的 index指向的表是 调用者,也就是其元表;
对于继承subClass方法：
	调用者：subClass(继承者) 其中：self==调用者，obj==继承者
	因 	self.__index = self; setmetatable(obj,self); obj.base = self;
	继承者 的元表是 调用者;	继承者的元表的 index指向的表是 调用者，也就是元表;	继承者有base属性，base==调用者

故：p1，Player，GameObject，Object之间关系有：
1、元表、index关系：
	对于new和subClass，都是self==调用者 setmetatable(self,被实例/继承者)  self.__index = self
	p1由Player:new()实例化出来,				故p1的元表是Player,			p1的元表的index是Player;
	player由GameObject:subClass()继承而来,	故Player的元表是GameObject,	Player的index是GameObject;
	GameObject由Object:subClass()继承来,		故GameObject的元表是Object,	GameObject的index是Object;
	故有：
	p1里找不到的方法属性，从Player里找，
	Player里找不到的方法属性，从GameObject里找，
	GameObject里找不到的方法属性，从Object里找；
2、base关系：
	base，只在subClass里，逻辑语法是	调用者:subClass(继承者)	继承者.base = 调用者
	p1是player实例化new出来的,故			p1无base属性，需要从player里找base属性;
	player由GameObject:subClass继承来，	Player有base属性，其base==GameObject;
	GameObject由Object: subClass继承来	GameObject有base属性 其base==Object;
3、Move执行：
	p1执行move，自身没有move，找元表index的move，即Player里的move，实际上p1执行的是Player的Move方法；
	Player：Move（）中的self.base：Move(),调用者是Player，故self实际是Player，执行Player.base：Move()；
	Player有base属性，其base==GameObject，故Player.base：Move(),实际上执行的是GameObject:Move；
4、坑出现原因：
	即每次执行p1:Move();实际执行Player:Move的重载内容,同时执行GameObject:Move()内容；
	而每次执行GameObject:Move()，都会修改其中的posX、posY变量；(执行Player:Move非Base内容，没有变量修改)
	故依次执行p1:Move() p2:Move() p1:Move()后，GameObjectMove执行了三次，输出分别为(1,1) (2,2) (3,3)

5、问题所在：
	因self==函数调用者，继承者.base==继承者的父类/基类； 故self.base：Move()==调用者的父类/基类：Move();
	这种方式调用，相当于把基类 作为第一个参数传入方法中：
	Player:Move()self.base:Move() == GameObject:Move()
	故到GameObject:Move这里执行Move操作是，修改的self.posX/posY 是GameObject.posX/posY
	而不是修改p1表里的属性，故调用p1,p2,p1的move，是执行了三次GameObject:Move，修改了三次GameObject的属性
6、修改方法：
	使用base时，避免把基类表 传入到方法中 这样相当于就是公用一张表的属性 但不改变公共表的属性
]]

print("优化解决base的坑：这里特别特别重要的坑")
GameObject:subClass("Player")							
function Player:Move()
	print("优化修改坑")
	--执行父类逻辑，不要直接使用冒号:传参，使用点.自己指定传参。		这里特别注意！！！
	self.base.Move(self)						--base这里使用点. 不用冒号: 避免把基类表传入方法中
end
local p1 = Player:new()
p1:Move()										--输出：优化修改坑(4,4)前面测试已经把父类初始值改成3，不是0
local p2 = Player:new()
p2:Move()										--输出：优化修改坑(4,4)
p1:Move()										--输出：优化修改坑(5,5)前面p1初始值为4，这里再执行p1，变成5
p1:Move()
p1:Move()										--输出：优化修改坑(7,7)
p2:Move()										--输出：优化修改坑(5,5) 不管p1怎么执行，都只修改p2身上的属性