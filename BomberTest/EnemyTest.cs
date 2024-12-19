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
    public class EnemyTest
    {
        [TestMethod]
        public void TestMove()
        {
            Mock<Enemy> mock = new Mock<Enemy>(new Random(), MockMap().Object, new Point(0, 0)) { CallBase = true };

            mock.Setup(m => m.Move(It.IsAny<Direction>()));

            mock.Object.Move();

            Mock.Get(mock.Object).Verify(m => m.Move(mock.Object.Orientation), Times.Exactly(1));
        }

        [TestMethod]
        public void TestCollisionWithPlayer()
        {
            IMap map = MockMap().Object;
            Enemy enemy = new Enemy(new Random(), map, new Point(0, 0));
            Mock<Player> mock = new Mock<Player>(map,new Point(0, 0));

            mock.Setup(m => m.Kill());

            enemy.OnCollision(mock.Object, new Point(0, 0));

            Mock.Get(mock.Object).Verify(m => m.Kill(), Times.Exactly(1));
        }

        [TestMethod]
        public void TestCollisionWithWall()
        {
            Enemy enemy = new Enemy(new Random(), MockMap().Object, new Point(0, 0));

            enemy.OnCollision(new Wall(), new Point(0, 0));

            Assert.AreNotEqual(Direction.Up, enemy.Orientation);
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
