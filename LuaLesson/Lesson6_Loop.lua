print("*********循环语句********")

print("*********while语句********")
num =0
--while 条件 do ....end 注：条件是进入条件
while num<3 do
	print(num)
	num=num+1
end

print("*********do while语句********")
num =0
-- repeat .... until 条件 注：条件是结束条件
repeat
	print(num)
	num=num+1
until num>3

print("*********for语句********")
--for 变量名=起始值，结束值（，增量 （可省略默认+1）） do ... end
--逗号表示：满足什么条件结束；
for i =7,9 do --lua中会默认自增，i会默认+1
	print(i)
end

for i=1,5,2 do --第三个参数表示增量，i每次+2
	print(i)
end

for i=2,-1,-1 do --第三个参数表示增量，i每次+2
	print(i)
end

