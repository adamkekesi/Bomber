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
            public TestUnit(Point startingPos,IMap map) : base(startingPos,map)
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
            var map = MockMap().Object;
            Unit unit = new TestUnit(new Point(0, 0),map);

            unit.Move(dir);

            Mock.Get(map).Verify(m=>m.Move(new Point(0,0), dir),Times.Exactly(1));

        }

        [TestMethod]
        public void TestMovementDead()
        {
            Direction dir = Direction.Up;
            var map = MockMap().Object;

            Unit unit = new TestUnit(new Point(0, 0), map);

            unit.Kill();
            unit.Move(dir);

            Mock.Get(map).Verify(m => m.Move(It.IsAny<Point>(), It.IsAny<Direction>()), Times.Never);
        }

        [TestMethod]
        public void TestKill()
        {
            var map = MockMap().Object;
            Unit unit = new TestUnit(new Point(0, 0),map);
            List<EventArgs> eventArgs = new List<EventArgs>();
            unit.Died += (sender, e) => eventArgs.Add(e);

            unit.Kill();

            Assert.AreEqual(1, eventArgs.Count);
        }

        private Mock<IMap> MockMap()
        {
            Mock<IMap> mock = new Mock<IMap>();
            mock.Setup(m => m.ForEachInArea(It.IsAny<Point>(), It.IsAny<int>(), It.IsAny<Action<IField>>()));
            mock.Setup(m => m.Move(It.IsAny<Point>(), It.IsAny<Direction>()));
            mock.Setup(m => m.RemoveField(It.IsAny<Point>()));
            return mock;
        }
    }
}
