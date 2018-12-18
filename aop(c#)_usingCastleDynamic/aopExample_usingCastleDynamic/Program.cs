using System;
using System.Diagnostics;
using System.Threading;
using Castle.DynamicProxy;



namespace aopExample_usingCastleDynamic
{
    class Program
    {
        static void Main(string[] args)
        {

            ProxyGenerator generator = new ProxyGenerator();
            LoggingInterceptor interceptor = new LoggingInterceptor();
            TimeInterceptor timeInterceptor = new TimeInterceptor();
            Rocket entity = generator.CreateClassProxy<Rocket>(timeInterceptor, interceptor);
            entity.Launch(3);

            Console.ReadLine();
        }
    }

    public interface IRocket
    {
        void Launch(int delaySeconds);
    }

    public class Rocket : IRocket
    {
        public string Name { get; set; }
        public string Model { get; set; }

        virtual public void Launch(int delaySeconds) //Only virtual method can be intercept
        {
            Console.WriteLine(string.Format(" {0} 秒收启动火箭发射", delaySeconds));
            Thread.Sleep(1000 * delaySeconds);
            Console.WriteLine("恭喜， 你的火箭发射成功");
        }
    }


    internal class LoggingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;
            try
            {
                Console.WriteLine(string.Format("执行方法:{0}, 参数: {1}", methodName, string.Join(",", invocation.Arguments)));
                invocation.Proceed();
                Console.WriteLine(string.Format("成功执行了方法:{0}", methodName));
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("方法:{0}, 异常信息:{1}", methodName, e.Message));
                throw;
            }
            finally
            {
                Console.WriteLine(string.Format("退出方法:{0}", methodName));
            }
        }
    }


    internal class TimeInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                invocation.Proceed();
                sw.Stop();
                Console.WriteLine(string.Format("{0} duration: {1}", methodName, sw.ElapsedMilliseconds));
            }
            catch (Exception e)
            {
                
                throw;
            }
            finally
            {
                
            }
        }
    }

}
