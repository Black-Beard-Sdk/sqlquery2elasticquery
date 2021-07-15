//using Bb;
//using Bb.Elasticsearch.Configurations;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Reflection;

//namespace TestQueryElasticUnitTest
//{


//    [TestClass]
//    public class UnitTestEncrypt
//    {


//        [TestMethod]
//        public void TestMethod1()
//        {

//            var e = AsymetricSecure.Get(AsymetricKindEnum.TripleDES);

//            e.Initialize();

//            var expected = "test";
//            var crypted = e.Crypt(expected);

//            var decrypted = e.Decrypt(crypted);

//            Assert.AreEqual(expected, decrypted);

//            var key = e.GetKeys();

//            var f = AsymetricSecure.Get(AsymetricKindEnum.TripleDES);
//            f.Initialize(key);
//            var crypted2 = f.Crypt(expected);
//            Assert.AreEqual(crypted, crypted2);


//        }

//        [TestMethod]
//        public void TestMethod2()
//        {

//            var config = ElasticConfigurations.Load("");

//            var c = new ElasticConfiguration()
//            {
//                Name = "elastic-1",
//                Uris = new List<string>() { "http://elasticsearch-local.fr" },
//                Authentication = new ElasticConfigurationLogin()
//                {
//                    Key1 = "",
//                    Key2 = "",
//                    Kind = ElasticConfigurationLoginKindEnum.None
//                }
//            };

//            config.Add(c);
//            // http://elasticsearch-rest.paris.pickup.local

//        }
//    }

//}
