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
    public class BombCollectionTest
    {
        [TestMethod]
        public void TestPlantBomb()
        {
            List<Bomb> innerList = new List<Bomb>();
            BombCollection bombCollection = new BombCollection(innerList);
            List<BombCollection.BombsChangedEventArgs> eventArgs = new List<BombCollection.BombsChangedEventArgs>();
            bombCollection.BombsChanged += (sender, e) => eventArgs.Add(e);

            Bomb bomb = InitTestBomb(1000);
            bombCollection.PlantBomb(bomb);

            Assert.AreEqual(1, eventArgs.Count);
            Assert.AreEqual(bomb, eventArgs[0].Bomb);
            Assert.AreEqual(BombCollection.ChangeType.Added, eventArgs[0].Change);
            Assert.AreEqual(1, innerList.Count);
            Assert.AreEqual(bomb, innerList[0]);

            bombCollection.Dispose();
        }

        [TestMethod]
        public void TestPlantBombWithMultipleBombs()
        {
            List<Bomb> innerList = new List<Bomb>();
            BombCollection bombCollection = new BombCollection(innerList);

            List<BombCollection.BombsChangedEventArgs> eventArgs = new List<BombCollection.BombsChangedEventArgs>();
            bombCollection.BombsChanged += (sender, e) => eventArgs.Add(e);

            for (int i = 0; i < 3; i++)
            {
                Bomb bomb = InitTestBomb(1000);
                bombCollection.PlantBomb(bomb);
                Assert.AreEqual(i + 1, eventArgs.Count);
                Assert.AreEqual(bomb, eventArgs[i].Bomb);
                Assert.AreEqual(BombCollection.ChangeType.Added, eventArgs[i].Change);
                Assert.AreEqual(i + 1, innerList.Count);
                Assert.AreEqual(bomb, innerList[i]);

            }

            bombCollection.Dispose();
        }

        [TestMethod]
        public void TestBombExplosion()
        {
            int timeTillExplosion = 1000;

            Mock<BombCollection> mock = new Mock<BombCollection>() { CallBase = true };
            mock.Setup(m => m.Remove(It.IsAny<Bomb>()));

            Bomb bomb = InitTestBomb(timeTillExplosion);
            mock.Object.PlantBomb(bomb);

            Thread.Sleep(timeTillExplosion + 100);

            Mock.Get(mock.Object).Verify(m => m.Remove(It.IsAny<Bomb>()), Times.Exactly(1));

            mock.Object.Dispose();
        }

        [TestMethod]
        public void TestRemove()
        {
            List<Bomb> innerList = new List<Bomb>();
            BombCollection bombCollection = new BombCollection(innerList);

            Mock<Bomb> mock = new Mock<Bomb>(new Point(0, 0), 1000, 5);
            mock.Setup(m => m.Dispose());
            bombCollection.PlantBomb(mock.Object);

            List<BombCollection.BombsChangedEventArgs> eventArgs = new List<BombCollection.BombsChangedEventArgs>();
            bombCollection.BombsChanged += (sender, e) => eventArgs.Add(e);
            bombCollection.Remove(mock.Object);

            Assert.AreEqual(1, eventArgs.Count);
            Assert.AreEqual(mock.Object, eventArgs[0].Bomb);
            Assert.AreEqual(BombCollection.ChangeType.Removed, eventArgs[0].Change);
            Assert.AreEqual(0, innerList.Count);

            Mock.Get(mock.Object).Verify(m => m.Dispose(), Times.Exactly(1));

            bombCollection.Dispose();
        }

        [TestMethod]
        public void TestPause()
        {
            List<Bomb> innerList = new List<Bomb>();
            BombCollection bombCollection = new BombCollection(innerList);

            Mock<Bomb> mock = new Mock<Bomb>(new Point(0, 0), 1000, 5);
            mock.Setup(m => m.Pause());
            bombCollection.PlantBomb(mock.Object);

            bombCollection.Pause();
            Mock.Get(mock.Object).Verify(m => m.Pause(), Times.Exactly(1));
        }

        [TestMethod]
        public void TestResume()
        {
            List<Bomb> innerList = new List<Bomb>();
            BombCollection bombCollection = new BombCollection(innerList);

            Mock<Bomb> mock = new Mock<Bomb>(new Point(0, 0), 1000, 5);
            mock.Setup(m => m.Resume());
            bombCollection.PlantBomb(mock.Object);

            bombCollection.Resume();
            Mock.Get(mock.Object).Verify(m => m.Resume(), Times.Exactly(1));
        }

        [TestMethod]
        public void TestDispose()
        {
            List<Bomb> innerList = new List<Bomb>();
            BombCollection bombCollection = new BombCollection(innerList);

            Mock<Bomb> mock = new Mock<Bomb>(new Point(0, 0), 1000, 5);
            mock.Setup(m => m.Dispose());
            bombCollection.PlantBomb(mock.Object);

            bombCollection.Dispose();
            Mock.Get(mock.Object).Verify(m => m.Dispose(), Times.Exactly(1));
        }

        private Bomb InitTestBomb(int timeTillExplosion)
        {
            return new Bomb(new Point(0, 0), timeTillExplosion, 5);

        }
    }
}
