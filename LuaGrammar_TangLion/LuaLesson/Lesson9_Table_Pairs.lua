print("*********迭代器遍历********")
--迭代器遍历 主要是用来遍历表的
--#得到长度 其实并不准确 一边不用#来遍历表

a={[0]=0,1,[-1]=-1,2,3,[4]=4,[6]=6}

print("*********ipairs迭代器遍历********")
--关键字： ipairs
--i：表示 键； k：表示 值；
--还是 从1开始往后遍历 小于等于0的值得不到
--只能找到连续索引的 键 如果中间断序了 也无法遍历出后面的内容
for i,k in ipairs(a) do    
	print("ipairs遍历，键："..i.."，值："..k)
end

print("*********ipairs迭代器只遍历键********")
for i in ipairs(a) do    
	print("ipairs遍历，键："..i)
end

print("*********pairs迭代器遍历********")
--关键字： pairs
--i：表示 键； v：表示 值；
--它能够把所有的键都找到，通过键可以找到值；
--先顺序遍历默认索引1-3，再遍历两端自定义索引0，6，最后遍历中间自定义索引-1，4
for i,v in pairs(a) do    
	print("pairs遍历，键："..i.."，值："..v)
end

print("*********pairs迭代器只遍历键********")
for i in pairs(a) do    
	print("pairs遍历，键："..i)
end

print("*********面试考点——ipairs和pairs之间区别********")
--[[ipairs和pairs之间区别:
ipairs：只能找到连续索引的 键 如果中间断序了 也无法遍历出后面的内容；
pairs：它能够把所有的键都找到，通过键可以找到值；
建议使用pairs遍历各种不规则的表
]]