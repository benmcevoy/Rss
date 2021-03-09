//using System.Diagnostics;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Rss.Indexer;
//using Rss.Server.Models;

//namespace Rss.Server.Tests.Indexer
//{
//    [TestClass]
//    public class UnitTest1
//    {
//        [TestMethod]
//        public void Indexer_Index()
//        {
//            var indexsource = new FeedsDbContextIndexSource(new FeedsDbEntities(), 100);
//            var indexer = new Indexer<Document>(indexsource);

//            indexer.Index();
//        }

//        ///<summary>
//        /// Summary
//        ///</summary>
//        [TestMethod]
//        public void Searcher_When_Searched_ReturnsDocuments()
//        {
//            // Arrange
//            var indexsource = new FeedsDbContextIndexSource(new FeedsDbEntities(), 100);
//            var searcher = new Searcher<Document>(indexsource);

//            // Act
//            var results = searcher.Search("net").ToList();

//            // Assert
//            Assert.IsTrue(results.Any());
//        }

//        [TestMethod]
//        public void Searcher_When_SearcherWarmedUp_SearchIsFaster()
//        {
//            // Arrange
//            var stopwatch = new Stopwatch();
//            var indexsource = new FeedsDbContextIndexSource(new FeedsDbEntities(), 100);
//            var searcher = new Searcher<Document>(indexsource);

//            // Act
//            stopwatch.Start();
//            var results = searcher.Search("net").ToList();
//            stopwatch.Stop();

//            var elapsed = stopwatch.ElapsedMilliseconds;
//            stopwatch.Reset();

//            Debug.WriteLine(elapsed);

//            stopwatch.Start();
//            results = searcher.Search("net").ToList();
//            stopwatch.Stop();

//            var elapsed2 = stopwatch.ElapsedMilliseconds;

//            Debug.WriteLine(elapsed2);

//            // Assert
//            Assert.IsTrue(elapsed2 < elapsed);
//        }
//    }
//}
