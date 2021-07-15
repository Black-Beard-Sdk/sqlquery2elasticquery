using Bb.Elastic.Runtimes;
using Bb.Elastic.Runtimes.Models;
using Bb.Elasticsearch.Configurations;
using Elasticsearch.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nest;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace TestQueryElasticUnitTest
{


    [TestClass]
    public class UnitTest1
    {

        
        public UnitTest1()
        {
            this._directoryRoot = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, @"..\..\..\.."));
            this._directoryDocTests = new DirectoryInfo(Path.Combine(_directoryRoot.FullName, @"DocTests"));
            this._directoryout = new DirectoryInfo(Path.Combine(_directoryRoot.FullName, @"outTests"));

            if (!this._directoryout.Exists)
                this._directoryout.Create();

        }


        [TestMethod]
        public void TestSql()
        {

            string sql = "select * from srv1.squid s WHERE s.server = '40.68.176.16'";

            var cnxList = new ConfigurationList();
            var configuration = new ElasticConfiguration("http://elasticsearch-rest.local")
            {
                Name = "srv1",
                EnableDebugMode = true,
                ThrowExceptions = true,
                PrettyJson = true,
            };

            cnxList.Add(configuration);

            var cnxs = cnxList.GetElasticConnections();

            var e = cnxs.GetExecutor();

            var responses2 = e.Plan(new StringBuilder(sql), "Text");


        }


        [TestMethod]
        public void TestQueryElastic()
        {

            string sql = "{\"from\":0,\"query\":{\"bool\":{\"filter\":[{\"term\":{\"server\":\"40.68.176.16\"}}]}},\"size\":10}";


            var cnxList = new ConfigurationList();
            var configuration = new ElasticConfiguration("http://elasticsearch-rest.local")
            {
                Name = "srv1",
                EnableDebugMode = true,
                ThrowExceptions = true,
                PrettyJson = true,
            };

            cnxList.Add(configuration);

            var cnxs = cnxList.GetElasticConnections();

            var e = cnxs.GetExecutor();

            var responses2 = e.Execute(JObject.Parse(sql), "Text", "squid", "srv1");


        }


        private readonly DirectoryInfo _directoryRoot;
        private readonly DirectoryInfo _directoryout;
        private readonly DirectoryInfo _directoryDocTests;

    }

}
