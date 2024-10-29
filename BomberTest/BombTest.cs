using Bomber.Model;
using System.Drawing;

namespace BomberTest
{
    [TestClass]
    public class BombTest
    {
        [TestMethod]
        public void TestExplode()
        {
            int timeTillExplosion = 1000;
            Bomb bomb = InitTestBomb(timeTillExplosion);
            bool exploded = false;
            bomb.Exploded += (sender, args) => exploded = true;

            Thread.Sleep(timeTillExplosion + 100);

            Assert.IsTrue(exploded);

            bomb.Dispose();
        }

        [TestMethod]
        public void TestPause()
        {
            int timeTillExplosion = 1000;
            Bomb bomb = InitTestBomb(timeTillExplosion);
            bool exploded = false;
            bomb.Exploded += (sender, args) => exploded = true;

            bomb.Pause();
            Thread.Sleep(timeTillExplosion + 100);

            Assert.IsFalse(exploded);

            bomb.Dispose();

        }

        [TestMethod]
        public void TestResume()
        {
            int timeTillExplosion = 1000;
            Bomb bomb = InitTestBomb(timeTillExplosion);
            bool exploded = false;
            bomb.Exploded += (sender, args) => exploded = true;

            bomb.Pause();
            Thread.Sleep(500);
            bomb.Resume();
            Thread.Sleep(timeTillExplosion + 100);

            Assert.IsTrue(exploded);

            bomb.Dispose();
        }

        public void TestDispose()
        {
            int timeTillExplosion = 1000;
            Bomb bomb = InitTestBomb(timeTillExplosion);
            bool exploded = false;
            bomb.Exploded += (sender, args) => exploded = true;

            bomb.Dispose();
            Thread.Sleep(timeTillExplosion + 100);

            Assert.IsFalse(exploded);
        }

        private Bomb InitTestBomb(int timeTillExplosion)
        {
            return new Bomb(new Point(0, 0), timeTillExplosion, 5);

        }
    }
}