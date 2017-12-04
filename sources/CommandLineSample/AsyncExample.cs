using System.Net.Http;
using System.Threading.Tasks;

namespace CommandLineSample
{

    /// <summary>
    /// 
    /// </summary>
    public class AsyncExample
    {


        /// <summary>
        /// 这种加了async、await叫不叫异步呢？答案肯定不是的。我们可以这样叫这种方法：加了async、await标记的同步方法。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static async Task<int> AddAsync(int x, int y)
        {
            return await Task.Factory.StartNew(() => { return x + y; });
        }



        // Three things to note in the signature:  
        //  - The method has an async modifier.   
        //  - The return type is Task or Task<T>. (See "Return Types" section.)  
        //    Here, it is Task<int> because the return statement returns an integer.  
        //  - The method name ends in "Async."  
        async Task<int> AccessTheWebAsync()
        {
            // You need to add a reference to System.Net.Http to declare client.  
            HttpClient client = new HttpClient();

            // GetStringAsync returns a Task<string>. That means that when you await the  
            // task you'll get a string (urlContents).  
            Task<string> getStringTask = client.GetStringAsync("http://msdn.microsoft.com");

            // You can do work here that doesn't rely on the string from GetStringAsync.  
            DoIndependentWork();

            // The await operator suspends AccessTheWebAsync.  
            //  - AccessTheWebAsync can't continue until getStringTask is complete.  
            //  - Meanwhile, control returns to the caller of AccessTheWebAsync.  
            //  - Control resumes here when getStringTask is complete.   
            //  - The await operator then retrieves the string result from getStringTask.  
            string urlContents = await getStringTask;

            // The return statement specifies an integer result.  
            // Any methods that are awaiting AccessTheWebAsync retrieve the length value.  
            return urlContents.Length;
        }

        /*
         以下特征总结了使上一个示例成为异步方法的原因。
        方法签名包含 async 修饰符。
        按照约定，异步方法的名称以“Async”后缀结尾。
        返回类型为下列类型之一：
        如果你的方法有操作数为 TResult 类型的返回语句，则为 Task<TResult>。
        如果你的方法没有返回语句或具有没有操作数的返回语句，则为 Task。
        Void：如果要编写异步事件处理程序。
        包含 GetAwaiter 方法的其他任何类型（自 C# 7 起）。
        有关详细信息，请参见本主题后面的“返回类型和参数”。
        方法通常包含至少一个 await 表达式，该表达式标记一个点，在该点上，直到等待的异步操作完成方法才能继续。 同时，将方法挂起，并且控件返回到方法的调用方。 本主题的下一节将解释悬挂点发生的情况。
        在异步方法中，可使用提供的关键字和类型来指示需要完成的操作，且编译器会完成其余操作，其中包括持续跟踪控件以挂起方法返回等待点时发生的情况。 一些常规流程（例如，循环和异常处理）在传统异步代码中处理起来可能很困难。 在异步方法中，元素的编写频率与同步解决方案相同且此问题得到解决。


            简单一句话，并不是所有的加了async和await关键字的方法就是异步方法。可以这样理解 await 的位置决定了到底是不是异步方法，如果直接await xxxAsync那么是挂起当前方法直到拿到返回值才会执行下面的逻辑，这样就是一种同步方法了。异步是类似这样的 代码块：


             */



        void DoIndependentWork()
        {
        }

    }
}