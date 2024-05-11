print("Test测试")

testA ="123"

local testLocalA = "xxx"

--测试require的另一种用法时，添加的
--把本地变量返回出去，其他脚本调用该脚本时，可以获取返回出去的变量
return testLocalA
