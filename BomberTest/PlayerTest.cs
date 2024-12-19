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
    public class PlayerTest
    {
        [TestMethod]
        public void TestPlantBombAlive()
        {
            Player player = new Player(MockMap().Object, new Point(0, 0));
            int timeTillExplosion = 1000;
            int radius = 3;
            Bomb bomb = player.PlantBomb(timeTillExplosion, radius);
            Assert.AreEqual(radius, bomb.Radius);
            Assert.AreEqual(player.Position, bomb.Position);
        }

        [TestMethod]
        [ExpectedException(typeof(Player.PlayerDeadException))]
        public void TestPlantBombDead()
        {
            Player player = new Player(MockMap().Object, new Point(0, 0));
            player.Kill();
            player.PlantBomb(1000, 3);
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
