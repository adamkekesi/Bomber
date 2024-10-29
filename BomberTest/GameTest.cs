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
    public class GameTest
    {
        [TestMethod]
        public void TestPlayerStep()
        {
            Mock<Game> game = InitGameWithoutEnemies(out Mock<Player> player, out Mock<Map> map, out _);

            game.Object.PlayerStep(Direction.Right);

            Mock.Get(player.Object).Verify(m => m.Move(Direction.Right), Times.Exactly(1));
            Mock.Get(map.Object).Verify(m => m.Move(new Point(0, 0), Direction.Right), Times.Exactly(1));

            game.Object.Dispose();
        }

        [TestMethod]
        public void TestPlantBomb()
        {
            Mock<Game> game = InitGameWithoutEnemies(out Mock<Player> player, out Mock<Map> map, out Mock<BombCollection> bombs);
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
            Mock<Game> game = InitGameWithOneEnemy(out Mock<Player> player, out Mock<Map> map, out Mock<Enemy> enemy, out Mock<BombCollection> bombs);
            enemy.Setup(m => m.Move());

            bool timeElapsed = false;
            game.Object.TimeElapsed += (sender, args) => timeElapsed = true;

            game.Object.Pause();

            Mock.Get(bombs.Object).Verify(m => m.Pause(), Times.Exactly(1));
            Thread.Sleep(2000);

            Assert.IsFalse(timeElapsed);
            Mock.Get(enemy.Object).Verify(m => m.Move(), Times.Never());

            game.Object.Dispose();
        }

        [TestMethod]
        public void TestEnemySteps()
        {
            Mock<Game> game = InitGameWithOneEnemy(out Mock<Player> player, out Mock<Map> map, out Mock<Enemy> enemy, out Mock<BombCollection> bombs);
            enemy.Setup(m => m.Move());
            var a = game.Object;
            Thread.Sleep(1400+300);

            Mock.Get(enemy.Object).Verify(m => m.Move(), Times.Exactly(1));

            game.Object.Dispose();

        }

        [TestMethod]
        public void TestTimeElapsed()
        {
            Mock<Game> game = InitGameWithoutEnemies(out Mock<Player> player, out Mock<Map> map, out Mock<BombCollection> bombs);

            bool timeElapsed = false;
            game.Object.TimeElapsed += (sender, args) => timeElapsed = true;

            Thread.Sleep(100 + 50);
            Assert.IsTrue(timeElapsed);
        }

        private Mock<Game> InitGameWithoutEnemies(out Mock<Player> player, out Mock<Map> map, out Mock<BombCollection> bombs)
        {
            player = new Mock<Player>(new Point(0, 0)) { CallBase = true };
            map = new Mock<Map>(new IField?[,]
            {
                {player.Object, null },
                {null,null}
            });
            bombs = new Mock<BombCollection>();
            Mock<Game> game = new Mock<Game>(player.Object, map.Object, new List<Enemy>(), bombs.Object) { CallBase = true };

            return game;
        }

        private Mock<Game> InitGameWithOneEnemy(out Mock<Player> player, out Mock<Map> map, out Mock<Enemy> enemy, out Mock<BombCollection> bombs)
        {
            Random r = new Random();
            player = new Mock<Player>(new Point(0, 0)) { CallBase = true };
            enemy = new Mock<Enemy>(r, new Point(1, 1));
            map = new Mock<Map>(new IField?[,]
            {
                {player.Object, null },
                {null,enemy.Object }
            });
            bombs = new Mock<BombCollection>();
            var enemies = new List<Enemy>() { enemy.Object };
            Mock<Game> game = new Mock<Game>(player.Object, map.Object, enemies, bombs.Object) { CallBase = true };

            return game;
        }
    }
}
