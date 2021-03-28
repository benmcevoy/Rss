//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using Lucene.Net.Analysis;
//using Lucene.Net.Analysis.Standard;
//using Lucene.Net.Store;
//using Rss.Server.Models;
//using Directory = Lucene.Net.Store.Directory;
//using Version = Lucene.Net.Util.Version;

//namespace Rss.Indexer
//{
//    // TODO: yes, should be two classes, i know...
//    // and should be out of this project
//    public class FeedsDbContextIndexSource : IIndexer<Document>, ISearchConfig
//    {
//        private readonly FeedsDbEntities _context;

//        public FeedsDbContextIndexSource(FeedsDbEntities context, int batchSize)
//        {
//            _context = context;

//            IndexBatchSize = batchSize;
//            SearchResultLimit = batchSize;
//            Directory = new SimpleFSDirectory(new DirectoryInfo(@"D:\Dev\git\Rss\Rss.Server.Tests\TestIndex"));
//            Analyzer = new StandardAnalyzer(Version.LUCENE_30);
//        }

//        public IEnumerable<Document> GetDocumentsToIndex()
//        {
//            throw new NotImplementedException("TODO: the database schema regressed, IndexedDateTimedoes not exist");
//            //return _context.Items
//            //    .Where(item => item.IndexedDateTime == null)
//            //    .Take(IndexBatchSize)
//            //    .Select(item => new Document
//            //{
//            //    Content = item.Content,
//            //    Id = item.Id,
//            //    Title = item.Name
//            //});
//        }

//        public void SetDocumentsIndexed(IEnumerable<Document> documents)
//        {
//            throw new NotImplementedException("TODO: the database schema regressed, IndexedDateTimedoes not exist");
//            //documents.ForEach(doc =>
//            //{
//            //    var item = _context.Items.Single(i => i.Id == doc.Id);
//            //    item.IndexedDateTime = DateTime.Now;
//            //});

//            //_context.SaveChanges();
//        }

//        public int SearchResultLimit { get; private set; }

//        public int IndexBatchSize { get; private set; }

//        public Directory Directory { get; private set; }

//        public Analyzer Analyzer { get; private set; }
//    }
//}