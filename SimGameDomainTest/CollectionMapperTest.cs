//using System.Collections.Generic;
//using System.Linq;
//using AutoMapper;
//using NUnit.Framework;
//using Should;
//using SimGame.Domain;
//
//namespace SimGameDomainTest
//{
//    [TestFixture]
//    public class CollectionMapperTest
//    {
//        [TestFixtureSetUp()]
//        public void Setup()
//        {
//            Mapper.CreateMap<TestObject1, TestObject2>()
//                .ForMember(x => x.TestCollection, opt => opt.ResolveUsing<TestDomainObjectCollectionMapper>());
//        }
//
//        /// <summary>
//        /// Test the regular properties map
//        /// </summary>
//        [Test]
//        public void Scenario1()
//        {
//            var obj1 = new TestObject1
//            {
//                Test = "test"
//            };
//            var obj2 = new TestObject2();
//            Mapper.Map(obj1, obj2);
//            obj2.Test.ShouldEqual("test");
//        }
//        /// <summary>
//        /// Test object gets added
//        /// </summary>
//        [Test]
//        public void Scenario2()
//        {
//            var obj1 = new TestObject1
//            {
//                Test = "test",
//                TestCollection = new List<TestObject1>
//                {
//                    new TestObject1
//                    {
//                        Id = 1,
//                        Test = "subtest"
//                    }
//                }
//            };
//            var obj2 = new TestObject2();
//            Mapper.Map(obj1, obj2);
//            obj2.Test.ShouldEqual("test");
//            obj2.TestCollection.Count.ShouldEqual(1);
//            obj2.TestCollection.First().Id.ShouldEqual(1);
//            obj2.TestCollection.First().Test.ShouldEqual("subtest");
//        }
//        /// <summary>
//        /// Test object gets marked for deletion.
//        /// </summary>
//        [Test]
//        public void Scenario3()
//        {
//            var obj1 = new TestObject1
//            {
//                Test = "test"
//            };
//            var obj2 = new TestObject2
//            {
//                TestCollection = new List<TestObject2>
//                {
//                    new TestObject2
//                    {
//                        Id = 1
//                    }
//                }
//            };
//            Mapper.Map(obj1, obj2);
//            obj2.Test.ShouldEqual("test");
//            obj2.TestCollection.Count.ShouldEqual(1);
//            obj2.TestCollection.First().Id.ShouldEqual(1);
//            obj2.TestCollection.First().Deleted.ShouldBeTrue();
//        }
//
//        /// <summary>
//        /// Test object gets marked for deletion.
//        /// </summary>
//        [Test]
//        public void Scenario4()
//        {
//            var obj1 = new TestObject1
//            {
//                Test = "test",
//                TestCollection = new List<TestObject1>
//                {
//                    new TestObject1
//                    {
//                        Id = 1,
//                        Test = "subTest"
//                    }
//                }
//            };
//            var obj2 = new TestObject2
//            {
//                TestCollection = new List<TestObject2>
//                {
//                    new TestObject2
//                    {
//                        Id = 1
//                    }
//                }
//            };
//            Mapper.Map(obj1, obj2);
//            obj2.Test.ShouldEqual("test");
//            obj2.TestCollection.Count.ShouldEqual(1);
//            obj2.TestCollection.First().Id.ShouldEqual(1);
//            obj2.TestCollection.First().Test.ShouldEqual("subTest");
//            
//        }
//       
//    }
//
//    internal class TestDomainObjectCollectionMapper : DomainObjectCollectionMapper<TestObject1, TestObject2>
//    {
//        protected override ICollection<TestObject1> GetSourceCollection(object sourceValue)
//        {
//            var parent = sourceValue as TestObject1;
//            if (parent == null)
//                return null;
//            return parent.TestCollection;
//        }
//
//        protected override ICollection<TestObject2> GetDestinationCollection(object sourceValue)
//        {
//            var parent = sourceValue as TestObject2;
//            if (parent == null)
//                return null;
//            return parent.TestCollection;
//        }
//    }
//
//    internal class TestObject1 : DomainObject
//    {
//        public TestObject1()
//        {
//            TestCollection = new List<TestObject1>();
//        }
//
//        public override int Id { get; set; }
//        public string Test { get; set; }
//        public ICollection<TestObject1> TestCollection { get; set; }
//    }
//
//    internal class TestObject2 : DomainObject
//    {
//        public TestObject2()
//        {
//            TestCollection = new List<TestObject2>();
//        }
//        public override int Id { get; set; }
//        public string Test { get; set; }
//        public ICollection<TestObject2> TestCollection { get; set; }
//    }
//}
