print("*********条件分支语句********")
a=9
--单分支： if 条件 then ... end
if a>5 then
	print("123")
end

--双分支：if 条件 then ... else ... end
if a<5 then
	print("456")
else
	print("321")
end

--多分支：if 条件 then ... elseif 条件 then ... else ... end
if a<5 then
	print("456")
--lua中 elseif 一定是连着写 不能分开写
elseif a==6 then
	print("6")
elseif a==7 then
	print("7")
else 
	print("9")
end

--lua中没有switch 需要自己实现