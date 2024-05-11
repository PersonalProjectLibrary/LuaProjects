print("*********垃圾回收 collectgarbage********")

--垃圾回收关键字 collectgarbage 使用方法：collectgarbage(不同关键字的字符串)

--count： 以 k字节 kB 为单位 获取当前lua占用内存数
--用返回值*1024 就可以得到具体的内存占用数 1kB = 1024B
print("当前lua占用内存："..collectgarbage("count"))

--一般用来东西才占用，这里添加内容后 再测占用多少
test ={id =1,name="123123"}
print("声明一个表后占用内存："..collectgarbage("count"))

--进行垃圾回收 有点像C# 的GC
collectgarbage("collect")										--进行一次垃圾回收
print("一次垃圾回收后占用内存："..collectgarbage("count"))

--lua中的机制 和 C# 的垃圾回收机制很类似：解除羁绊 就是变垃圾
--将test置空后，表中原数据变成无用数据 再次垃圾回收
test = nil
collectgarbage("collect")
print("表置空后占用内存："..collectgarbage("count"))

--lua中 有自动定时进行GC的方法
--这里不做介绍 自行了解
--Unity中热更新开发 尽量不用去用 自动垃圾回收
--一次垃圾回收比较耗性能 
--一般在 切换场景 或 当内存达到一个瓶颈时 手动进行垃圾回收 