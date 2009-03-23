using NUnit.Framework;
using Rhino.Mocks;

namespace Mike.RhinoMocksDemo.Tests.Spikes
{
    [TestFixture]
    public class Demonstrate_argument_constrains
    {
        private IThing mock;

        private IThing thing1;
        private IThing thing2;

        [SetUp]
        public void SetUp()
        {
            mock = MockRepository.GenerateMock<IThing>();

            thing1 = new Thing { Name = "Thing 1" };
            thing2 = new Thing { Name = "Thing 2" };
        }

        [Test]
        public void Basic_argument_constraint()
        {
            mock.Expect(m => m.DoSomething(thing1)).Return(thing2);
            Assert.That(mock.DoSomething(thing1), Is.SameAs(thing2));
        }

        [Test]
        public void Basic_argument_constraint_with_arg_T()
        {
            mock.Expect(m => m.DoSomething(Arg.Is(thing1))).Return(thing2);
            Assert.That(mock.DoSomething(thing1), Is.SameAs(thing2));
        }

        [Test]
        public void Is_anything_always_matches()
        {
            mock.Expect(m => m.DoSomething(Arg<IThing>.Is.Anything)).Return(thing1)
                .Repeat.Any();

            Assert.That(mock.DoSomething(null), Is.SameAs(thing1));
            Assert.That(mock.DoSomething(thing1), Is.SameAs(thing1));
            Assert.That(mock.DoSomething(thing2), Is.SameAs(thing1));
        }

        [Test]
        public void Can_use_with_AssertWasCalled()
        {
            mock.DoSomething(thing1);

            mock.AssertWasCalled(m => m.DoSomething(Arg<IThing>.Is.Anything));
        }

        [Test]
        public void Is_not_null_matches()
        {
            mock.Expect(m => m.DoSomething(Arg<IThing>.Is.NotNull)).Return(thing1);

            Assert.That(mock.DoSomething(null), Is.Null);
            Assert.That(mock.DoSomething(thing2), Is.SameAs(thing1));
        }

        [Test]
        public void GreaterThan_and_LessThan_are_useful()
        {
            mock.Expect(m => m.SomeIntOperation(Arg<int>.Is.GreaterThan(0))).Return(1);
            mock.Expect(m => m.SomeIntOperation(0)).Return(0);
            mock.Expect(m => m.SomeIntOperation(Arg<int>.Is.LessThan(0))).Return(-1);

            Assert.That(mock.SomeIntOperation(23), Is.EqualTo(1));
            Assert.That(mock.SomeIntOperation(0), Is.EqualTo(0));
            Assert.That(mock.SomeIntOperation(-4), Is.EqualTo(-1));
        }

        [Test]
        public void Matches_is_advanced()
        {
            mock.Expect(m => m.DoSomething(Arg<IThing>.Matches(x => x.Name == "Mike"))).Return(thing1);

            var mikeThing = new OtherThing {Name = "Mike"};
            var jonThing = new Thing {Name = "Jon"};

            Assert.That(mock.DoSomething(mikeThing), Is.SameAs(thing1));
            Assert.That(mock.DoSomething(jonThing), Is.Null);
        }

        // TODO: Why doesn't this work?
        [Test]
        public void Use_List_to_match_on_a_possible_list()
        {
            var matches = new[] {thing1, thing2};
            var other = new OtherThing();

            mock.Expect(m => m.DoSomething(Arg<IThing>.List.OneOf(matches))).Return(thing1);

            Assert.That(mock.DoSomething(thing1), Is.SameAs(thing1));
            Assert.That(mock.DoSomething(thing2), Is.SameAs(thing1));
            Assert.That(mock.DoSomething(other), Is.Null);
        }

        [Test]
        public void Use_List_to_match_enumerable_args()
        {
            
        }

    }
}