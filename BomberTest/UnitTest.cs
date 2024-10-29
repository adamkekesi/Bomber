using Bomber.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberTest
{
    [TestClass]
    public class UnitTest
    {
        class TestUnit : Unit
        {
            public TestUnit(Point startingPos) : base(startingPos)
            {
            }

            public override void OnCollision(IField otherField, Point point)
            {
                
            }
        }

        [TestMethod]
        public void TestMovement()
        {
            Direction dir = Direction.Up;
            Unit unit = new TestUnit(new Point(0, 0));
            List<Unit.MovedEventArgs> eventArgs = new List<Unit.MovedEventArgs>();
            unit.Moved += (sender, e) => eventArgs.Add(e);

            unit.Move(dir);

            Assert.AreEqual(1, eventArgs.Count);
            Assert.AreEqual(dir, eventArgs[0].Direction);
            Assert.AreEqual(unit.Position, eventArgs[0].CurrentPos);

        }

        [TestMethod]
        public void TestMovementDead()
        {
            Direction dir = Direction.Up;
            Unit unit = new TestUnit(new Point(0, 0));
            List<Unit.MovedEventArgs> eventArgs = new List<Unit.MovedEventArgs>();
            unit.Moved += (sender, e) => eventArgs.Add(e);

            unit.Kill();
            unit.Move(dir);

            Assert.AreEqual(0, eventArgs.Count);
        }

        [TestMethod]
        public void TestKill()
        {
            Unit unit = new TestUnit(new Point(0, 0));
            List<EventArgs> eventArgs = new List<EventArgs>();
            unit.Died += (sender, e) => eventArgs.Add(e);

            unit.Kill();

            Assert.AreEqual(1, eventArgs.Count);
        }
    }
}
