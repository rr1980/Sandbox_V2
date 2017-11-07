using Microsoft.Extensions.Logging;
using System;

namespace Main
{
    public interface IExampleService
    {
        void DoSomeWork();
    }

    public class ExampleService : IExampleService
    {
        readonly ILogger<ExampleService> _logger;

        public ExampleService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExampleService>();

            _logger.LogInformation("ExampleService configured");
        }

        public void DoSomeWork()
        {
            _logger.LogDebug("ExampleService DoSomeWork");
            Console.WriteLine("inside ExampleService.DoSomeWork()");

            try
            {
                var tmp = _getName("Rene", 21);

            }
            catch(Exception ex)
            {
                throw new Exception("BÄM 2", ex);
            }
            Console.ReadLine();
            _logger.LogDebug("ExampleService DoSomeWork ends");
        }

        private string _getName(string name, int alter)
        {
            try
            {
                var tmp = _getName2("Rene", 21);

            }
            catch (Exception ex)
            {
                throw new Exception("BÄM 2", ex);
            }

            _logger.LogWarning("ExampleService _getName ends");
            return name + " ist " + alter + " JAhre alt!";
        }

        private string _getName2(string name, int alter)
        {
            try
            {
                var tmp = _getName3("Rene", 21);

            }
            catch (Exception ex)
            {
                throw new Exception("BÄM 2", ex);
            }

            _logger.LogWarning("ExampleService _getName ends");
            return name + " ist " + alter + " JAhre alt!";
        }

        private string _getName3(string name, int alter)
        {
            try
            {
                var tmp = _getName4("Rene", 21);

            }
            catch (Exception ex)
            {
                throw new Exception("BÄM 2", ex);
            }

            _logger.LogWarning("ExampleService _getName ends");
            return name + " ist " + alter + " JAhre alt!";
        }

        private string _getName4(string name, int alter)
        {

                throw new Exception("BÄM END!");

            return name + " ist " + alter + " JAhre alt!";
        }
    }
}
