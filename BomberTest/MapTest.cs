using Bomber.Model;
using Bomber.Persistence;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberTest
{
    [TestClass]
    public class MapTest
    {
        [TestMethod]
        public void TestInitialization()
        {
            Map map = InitTestMap(out List<Enemy> enemies);

            Assert.AreEqual(map.Size, 3);
            Assert.AreEqual(4, enemies.Count);
            Assert.IsInstanceOfType<Player>(map[0, 0]);
            Assert.IsInstanceOfType<Wall>(map[0, 1]);
            Assert.IsInstanceOfType<Enemy>(map[0, 2]);
            Assert.AreEqual(new Point(0, 2), enemies[0].Position);
            Assert.IsInstanceOfType<Wall>(map[1, 0]);
            Assert.IsInstanceOfType<Wall>(map[1, 1]);
            Assert.IsInstanceOfType<Enemy>(map[1, 2]);
            Assert.AreEqual(new Point(1, 2), enemies[1].Position);
            Assert.IsInstanceOfType<Enemy>(map[2, 0]);
            Assert.AreEqual(new Point(2, 0), enemies[2].Position);
            Assert.IsInstanceOfType<Wall>(map[2, 1]);
            Assert.IsInstanceOfType<Enemy>(map[2, 2]);
            Assert.AreEqual(new Point(2, 2), enemies[3].Position);
        }

        [TestMethod]
        [ExpectedException(typeof(Map.OutOfMapException))]
        public void TestRemoveFieldOutOfMap1()
        {
            Map map = InitTestMap(out List<Enemy> enemies);
            map.RemoveField(new Point(-1, 0));
        }

        [TestMethod]
        [ExpectedException(typeof(Map.OutOfMapException))]
        public void TestRemoveFieldOutOfMap2()
        {
            Map map = InitTestMap(out List<Enemy> enemies);
            map.RemoveField(new Point(0, 3));
        }

        [TestMethod]
        public void TestRemoveField()
        {
            Map map = InitTestMap(out List<Enemy> enemies);
            List<Map.MapChangedEventArgs> eventArgs = new List<Map.MapChangedEventArgs>();
            map.MapChanged += (sender, e) => eventArgs.Add(e);
            map.RemoveField(new Point(0, 0));

            Assert.AreEqual(map[0, 0], null);
            Assert.AreEqual(1, eventArgs.Count);
            Assert.AreEqual(1, eventArgs[0].AffectedCells.Length);
            Assert.AreEqual(new Point(0, 0), eventArgs[0].AffectedCells[0]);
        }

        [TestMethod]
        public void TestMovementAgainstWall()
        {
            Map map = InitTestMapForMovement(out List<Enemy> enemies);
            map.Move(new Point(0, 0), Direction.Down);
            List<Map.MapChangedEventArgs> eventArgs = new List<Map.MapChangedEventArgs>();
            map.MapChanged += (sender, e) => eventArgs.Add(e);

            Assert.IsInstanceOfType<Player>(map[0, 0]);
            Assert.IsInstanceOfType<Wall>(map[0, 1]);
            Assert.AreEqual(0, eventArgs.Count);

        }

        [TestMethod]
        public void TestMovementOutOfMap()
        {
            Map map = InitTestMapForMovement(out List<Enemy> enemies);
            map.Move(new Point(0, 0), Direction.Up);
            List<Map.MapChangedEventArgs> eventArgs = new List<Map.MapChangedEventArgs>();
            map.MapChanged += (sender, e) => eventArgs.Add(e);

            Assert.IsInstanceOfType<Player>(map[0, 0]);
            Assert.AreEqual(0, eventArgs.Count);
        }

        [TestMethod]
        public void TestMovementAgainstEmptySpaceRight()
        {
            Map map = InitTestMapForMovement(out List<Enemy> enemies);
            List<Map.MapChangedEventArgs> eventArgs = new List<Map.MapChangedEventArgs>();
            map.MapChanged += (sender, e) => eventArgs.Add(e);
            map.Move(new Point(0, 0), Direction.Right);

            Assert.IsNull(map[0, 0]);
            Assert.IsInstanceOfType<Player>(map[1, 0]);
            Assert.AreEqual(1, eventArgs.Count);
            Assert.AreEqual(2, eventArgs[0].AffectedCells.Length);
            Assert.AreEqual(new Point(0, 0), eventArgs[0].AffectedCells[0]);
            Assert.AreEqual(new Point(1, 0), eventArgs[0].AffectedCells[1]);


        }

        [TestMethod]
        public void TestMovementAgainstEmptySpaceLeft()
        {
            Map map = InitTestMapForMovement(out List<Enemy> enemies);
            List<Map.MapChangedEventArgs> eventArgs = new List<Map.MapChangedEventArgs>();
            map.MapChanged += (sender, e) => eventArgs.Add(e);
            map.Move(new Point(2, 0), Direction.Left);

            Assert.IsNull(map[2, 0]);
            Assert.IsInstanceOfType<Enemy>(map[1, 0]);
            Assert.AreEqual(1, eventArgs.Count);
            Assert.AreEqual(2, eventArgs[0].AffectedCells.Length);
            Assert.AreEqual(new Point(2, 0), eventArgs[0].AffectedCells[0]);
            Assert.AreEqual(new Point(1, 0), eventArgs[0].AffectedCells[1]);
        }

        [TestMethod]
        public void TestMovementAgainstEmptySpaceUp()
        {
            Map map = InitTestMapForMovement(out List<Enemy> enemies);
            List<Map.MapChangedEventArgs> eventArgs = new List<Map.MapChangedEventArgs>();
            map.MapChanged += (sender, e) => eventArgs.Add(e);
            map.Move(new Point(1, 1), Direction.Up);

            Assert.IsNull(map[1, 1]);
            Assert.IsInstanceOfType<Enemy>(map[1, 0]);
            Assert.AreEqual(1, eventArgs.Count);
            Assert.AreEqual(2, eventArgs[0].AffectedCells.Length);
            Assert.AreEqual(new Point(1, 1), eventArgs[0].AffectedCells[0]);
            Assert.AreEqual(new Point(1, 0), eventArgs[0].AffectedCells[1]);
        }

        [TestMethod]
        public void TestMovementAgainstEmptySpaceDown()
        {
            Map map = InitTestMapForMovement(out List<Enemy> enemies);
            List<Map.MapChangedEventArgs> eventArgs = new List<Map.MapChangedEventArgs>();
            map.MapChanged += (sender, e) => eventArgs.Add(e);
            map.Move(new Point(2, 1), Direction.Down);

            Assert.IsNull(map[2, 1]);
            Assert.IsInstanceOfType<Enemy>(map[2, 2]);
            Assert.AreEqual(1, eventArgs.Count);
            Assert.AreEqual(2, eventArgs[0].AffectedCells.Length);
            Assert.AreEqual(new Point(2, 1), eventArgs[0].AffectedCells[0]);
            Assert.AreEqual(new Point(2, 2), eventArgs[0].AffectedCells[1]);
        }

        [TestMethod]
        public void TestCollisions()
        {
            Map map = InitTestMapWithMockedFields();
            List<Map.MapChangedEventArgs> eventArgs = new List<Map.MapChangedEventArgs>();
            map.MapChanged += (sender, e) => eventArgs.Add(e);
            map.Move(new Point(0, 0), Direction.Right);

            Mock.Get(map[0, 0]!).Verify(m => m.OnCollision(map[1, 0]!, new Point(1, 0)), Times.Exactly(1));
            Mock.Get(map[1, 0]!).Verify(m => m.OnCollision(map[0, 0]!, new Point(1, 0)), Times.Exactly(1));
            Assert.AreEqual(0, eventArgs.Count);
        }

        [TestMethod]
        public void TestForeachRadius1()
        {
            Map map = InitTestMapWithMockedFields();
            List<IField> traversedFields = new List<IField>();
            map.ForEachInArea(new Point(0, 0), 1, traversedFields.Add);

            Assert.AreEqual(2, traversedFields.Count);
            Assert.IsTrue(traversedFields.Contains(map[0,0]!));
            Assert.IsTrue(traversedFields.Contains(map[1, 0]!));

        }

        [TestMethod]
        public void TestForeachRadius2()
        {
            Map map = InitTestMapWithMockedFields();
            List<IField> traversedFields = new List<IField>();
            map.ForEachInArea(new Point(0, 0), 2, traversedFields.Add);

            Assert.AreEqual(3, traversedFields.Count);
            Assert.IsTrue(traversedFields.Contains(map[0, 0]!));
            Assert.IsTrue(traversedFields.Contains(map[1, 0]!));
            Assert.IsTrue(traversedFields.Contains(map[0, 2]!));


        }

        private Map InitTestMap(out List<Enemy> enemies)
        {
            CellContent[,] cells = new CellContent[,]
            {
                {CellContent.Empty, CellContent.Wall,CellContent.Enemy },
                {CellContent.Wall, CellContent.Wall,CellContent.Enemy },
                {CellContent.Enemy, CellContent.Wall,CellContent.Enemy }
            };

            Random r = new Random();
            var map = new Map(cells, r, out enemies);
            Mock<Player> player = new Mock<Player>(map,new Point(0, 0));
            map.PlacePlayer(player.Object);
            return map;
        }

        private Map InitTestMapForMovement(out List<Enemy> enemies)
        {
            CellContent[,] cells = new CellContent[,]
            {
                {CellContent.Empty, CellContent.Wall,CellContent.Enemy },
                {CellContent.Empty, CellContent.Enemy,CellContent.Enemy },
                {CellContent.Enemy, CellContent.Enemy,CellContent.Empty}
            };

            Random r = new Random();
            var map = new Map(cells, r, out enemies);
            Mock<Player> player = new Mock<Player>(map,new Point(0, 0));
            map.PlacePlayer(player.Object);
            return map;
        }

        private Map InitTestMapWithMockedFields()
        {
            Mock<IField> field1 = new Mock<IField>();
            Mock<IField> field2 = new Mock<IField>();
            Mock<IField> field3 = new Mock<IField>();

            field1.Setup(m => m.OnCollision(It.IsAny<IField>(), It.IsAny<Point>()));
            field2.Setup(m => m.OnCollision(It.IsAny<IField>(), It.IsAny<Point>()));
            field3.Setup(m => m.OnCollision(It.IsAny<IField>(), It.IsAny<Point>()));


            IField?[,] fields = new IField?[,]
            {
                { field1.Object, null,  field3.Object},
                { field2.Object, null, null },
                { null, null, null },


            };

            return new Map(fields);
        }
    }
}
