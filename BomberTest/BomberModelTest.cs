using Bomber.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberTest
{
    [TestClass]
    public class BomberModelTest
    {
        [TestMethod]
        public void TestPlayerStep()
        {
            Mock<BomberModel> game = InitGameWithoutEnemies(out Mock<Player> player, out Mock<IMap> map, out _);

            game.Object.PlayerStep(Direction.Right);

            Mock.Get(player.Object).Verify(m => m.Move(Direction.Right), Times.Exactly(1));
            Mock.Get(map.Object).Verify(m => m.Move(new Point(0, 0), Direction.Right), Times.Exactly(1));

            game.Object.Dispose();
        }

        [TestMethod]
        public void TestPlantBomb()
        {
            Mock<BomberModel> game = InitGameWithoutEnemies(out Mock<Player> player, out Mock<IMap> map, out Mock<BombCollection> bombs);
            Bomb bomb = new Bomb(new Point(0, 0), 1000, 3);
            player.Setup(m => m.PlantBomb(It.IsAny<int>(), It.IsAny<int>())).Returns(bomb);
            bombs.Setup(b => b.PlantBomb(It.IsAny<Bomb>()));

            game.Object.PlantBomb();

            Mock.Get(player.Object).Verify(m => m.PlantBomb(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(1));
            Mock.Get(bombs.Object).Verify(m => m.PlantBomb(bomb), Times.Exactly(1));

            game.Object.Dispose();
        }

        [TestMethod]
        public void TestPause()
        {
            Mock<BomberModel> game = InitGameWithOneEnemy(out Mock<Player> player, out Mock<IMap> map, out Mock<Enemy> enemy, out Mock<BombCollection> bombs);
            enemy.Setup(m => m.Move());

            bool timeElapsed = false;
            game.Object.TimeElapsed += (sender, args) => timeElapsed = true;

            game.Object.PauseToggle();

            Mock.Get(bombs.Object).Verify(m => m.Pause(), Times.Exactly(1));
            Thread.Sleep(2000);

            Assert.IsFalse(timeElapsed);
            Mock.Get(enemy.Object).Verify(m => m.Move(), Times.Never());

            game.Object.Dispose();
        }

        [TestMethod]
        public void TestEnemySteps()
        {
            Mock<BomberModel> game = InitGameWithOneEnemy(out Mock<Player> player, out Mock<IMap> map, out Mock<Enemy> enemy, out Mock<BombCollection> bombs);
            enemy.Setup(m => m.Move());
            var a = game.Object;
            Thread.Sleep(1400 + 300);

            Mock.Get(enemy.Object).Verify(m => m.Move(), Times.Exactly(1));

            game.Object.Dispose();

        }

        [TestMethod]
        public void TestTimeElapsed()
        {
            Mock<BomberModel> game = InitGameWithoutEnemies(out Mock<Player> player, out Mock<IMap> map, out Mock<BombCollection> bombs);

            bool timeElapsed = false;
            game.Object.TimeElapsed += (sender, args) => timeElapsed = true;

            Thread.Sleep(100 + 50);
            Assert.IsTrue(timeElapsed);
        }

        private Mock<BomberModel> InitGameWithoutEnemies(out Mock<Player> player, out Mock<IMap> map, out Mock<BombCollection> bombs)
        {
            map = new Mock<IMap>();
            player = new Mock<Player>(map.Object, new Point(0, 0)) { CallBase = true };
            
            bombs = new Mock<BombCollection>(map.Object);
            Mock<BomberModel> game = new Mock<BomberModel>(player.Object, map.Object, new List<Enemy>(), bombs.Object) { CallBase = true };

            return game;
        }

        private Mock<BomberModel> InitGameWithOneEnemy(out Mock<Player> player, out Mock<IMap> map, out Mock<Enemy> enemy, out Mock<BombCollection> bombs)
        {
            Random r = new Random();
            map = new Mock<IMap>();
            player = new Mock<Player>(map.Object, new Point(0, 0)) { CallBase = true };
            enemy = new Mock<Enemy>(r, map.Object, new Point(1, 1));

            bombs = new Mock<BombCollection>(map.Object);
            var enemies = new List<Enemy>() { enemy.Object };
            Mock<BomberModel> game = new Mock<BomberModel>(player.Object, map.Object, enemies, bombs.Object) { CallBase = true };

            return game;
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
