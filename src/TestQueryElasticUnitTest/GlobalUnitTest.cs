using Bb.Elastic.Runtimes;
using Bb.Elastic.Runtimes.Models;
using Bb.Elastic.SqlParser.Models;
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
    public class GlobalUnitTest
    {
        private readonly DirectoryInfo _directoryRoot;
        private readonly DirectoryInfo _directoryDocTests;
        private readonly DirectoryInfo _directoryout;

        public GlobalUnitTest()
        {


            this._directoryRoot = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, @"..\..\..\.."));
            this._directoryDocTests = new DirectoryInfo(Path.Combine(_directoryRoot.FullName, @"DocTests"));
            this._directoryout = new DirectoryInfo(Path.Combine(_directoryRoot.FullName, @"outTests"));

            if (!this._directoryout.Exists)
                this._directoryout.Create();

        }


        [TestMethod]
        public void TestMethod1()
        {

            var nodes = new Uri[] { new Uri("http://localhost"), };
            var pool = new StaticConnectionPool(nodes);
            var settings = new ConnectionSettings(pool);
            settings.EnableDebugMode();
            settings.ThrowExceptions(alwaysThrow: true); // I like exceptions
            settings.PrettyJson(); // Good for DEBUG


            string[] lines = ExtractDatas("Test1.txt");

            for (int i = 0; i < lines.Length; i++)
            {

                ElasticConnectionList cnxs = GetserverEndpoints(settings, lines[i++].Split('|'));

                var sql = lines[i++];
                var e = cnxs.GetExecutor();

                ContextExecutor responses2 = e.Plan(new StringBuilder(sql), $"file sample line {i}");
                var response3 = e.Execute(new StringBuilder(sql), $"file sample line {i}");

                foreach (ECall item in responses2.Request.ExecutableQueries)
                {

                    var name = item.Connection.ConnectionName;
                    string table = item.Index;

                    var query = item.Query.Serialize().ToString(Newtonsoft.Json.Formatting.None);

                    var _expected = lines[i].Split("->");
                    var expectedIndex = _expected[0];
                    var expectedquery = _expected[1];

                    try
                    {
                        Assert.AreEqual(expectedIndex, table);
                        Assert.AreEqual(query, expectedquery);
                    }
                    catch (Exception)
                    {

                        if (System.Diagnostics.Debugger.IsAttached)
                            System.Diagnostics.Debugger.Break();
                        throw;
                    }

                }


            }


        }

        private static ElasticConnectionList GetserverEndpoints(IConnectionConfigurationValues settings, string[] servers)
        {

            var cnxList = new ConfigurationList();
            foreach (var item in servers)
            {
                var configuration = new ElasticConfiguration(item)
                {
                    Name = "srv1",
                    EnableDebugMode = true,
                    ThrowExceptions = true,
                    PrettyJson = true,
                };
                cnxList.Add(configuration);
            }

            var cnxs = cnxList.GetElasticConnections();

            return cnxs;
        }

        private string[] ExtractDatas(string filename)
        {
            var f = new FileInfo(Path.Combine(this._directoryDocTests.FullName, filename));
            if (!f.Exists)
                throw new FileNotFoundException(f.FullName);

            var datas = File.ReadAllText(f.FullName);
            var lines = datas.Split("\r\n");
            return lines;
        }

        private FileInfo GetPathDoc(string filename)
        {
            return new FileInfo(Path.Combine(_directoryDocTests.FullName, filename));
        }

    }

}
