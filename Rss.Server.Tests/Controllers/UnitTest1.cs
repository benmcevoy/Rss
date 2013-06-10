using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rss.Server.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var types = Assembly.GetAssembly(typeof (Rss.Server.WebApiConfig)).GetTypes();

            var vm = types.Where(t => t.Name.EndsWith("ViewModel"));

            foreach (var viewModel in vm)
            {
                Debug.WriteLine(viewModel.Name);  
            }
        }
    }
}
