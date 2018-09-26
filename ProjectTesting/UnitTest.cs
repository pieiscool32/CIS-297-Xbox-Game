
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windows.UI.Xaml.Shapes;
using System.Collections.Generic;
using Breakout;
//using Xunit;
using Windows.UI.Xaml;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;

namespace ProjectTesting
{
    [TestClass]
    public class UnitTest
    {
        Rectangle brick;
        Ellipse ball;
        List<int> acc;

        [TestInitialize]
        public void TextBuild()
        {
            brick = new Rectangle()
            {
                Name = "brick",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(100, 0, 0, 100),
                Height = 20,
                Width = 50
            };

            ball = new Ellipse()
            {
                Name = "ball",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Height = 5,
                Width = 5
            };

            acc = new List<int>() { 1, 0 };
        }

        /*  Top Right  150 120 brick corner */

        [UITestMethod]
        public void topRightAt225Go2()
        {
            ball.Margin = new Thickness(150, 0, 0, 120);
            acc[0] = 2;
            acc[1] = 225;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(45, acc[1]);
        }

        [UITestMethod]
        public void topRightAt200Go2()
        {
            ball.Margin = new Thickness(150, 0, 0, 120);
            acc[0] = 2;
            acc[1] = 200;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(160, acc[1]);
        }

        [UITestMethod]
        public void topRightAt250Go2()
        {
            ball.Margin = new Thickness(150, 0, 0, 120);
            acc[0] = 2;
            acc[1] = 250;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(290, acc[1]);
        }

        /*  Top Left  100 120 brick corner */

        [UITestMethod]
        public void topLeftAt315Go2()
        {
            ball.Margin = new Thickness(95, 0, 0, 120);
            acc[0] = 2;
            acc[1] = 315;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(135, acc[1]);
        }

        [UITestMethod]
        public void topLeftAt290Go2()
        {
            ball.Margin = new Thickness(95, 0, 0, 120);
            acc[0] = 2;
            acc[1] = 290;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(250, acc[1]);
        }

        [UITestMethod]
        public void topLeftAt340Go2()
        {
            ball.Margin = new Thickness(95, 0, 0, 120);
            acc[0] = 2;
            acc[1] = 340;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(20, acc[1]);
        }

        /*  Bottom Right  150 100 brick corner */

        [UITestMethod]
        public void bottomRightAt135Go2()
        {
            ball.Margin = new Thickness(150, 0, 0, 95);
            acc[0] = 2;
            acc[1] = 135;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(315, acc[1]);
        }

        [UITestMethod]
        public void bottomRightAt110Go2()
        {
            ball.Margin = new Thickness(150, 0, 0, 95);
            acc[0] = 2;
            acc[1] = 110;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(70, acc[1]);
        }

        [UITestMethod]
        public void bottomRightAt160Go2()
        {
            ball.Margin = new Thickness(150, 0, 0, 95);
            acc[0] = 2;
            acc[1] = 160;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(290, acc[1]);
        }

        /*  Bottom Left  100 100 brick corner */

        [UITestMethod]
        public void bottomLeftAt45Go2()
        {
            ball.Margin = new Thickness(95, 0, 0, 95);
            acc[0] = 2;
            acc[1] = 45;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(225, acc[1]);
        }

        [UITestMethod]
        public void bottomLeftAt20Go2()
        {
            ball.Margin = new Thickness(95, 0, 0, 95);
            acc[0] = 2;
            acc[1] = 20;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(340, acc[1]);
        }

        [UITestMethod]
        public void bottomLeftAt70Go2()
        {
            ball.Margin = new Thickness(95, 0, 0, 95);
            acc[0] = 2;
            acc[1] = 70;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(110, acc[1]);
        }

        /*  Right  150 brick side */

        [UITestMethod]
        public void rightMidAt135Go2()
        {
            ball.Margin = new Thickness(150, 0, 0, 110);
            acc[0] = 2;
            acc[1] = 135;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(45, acc[1]);
        }

        [UITestMethod]
        public void rightMidAt225Go2()
        {
            ball.Margin = new Thickness(150, 0, 0, 110);
            acc[0] = 2;
            acc[1] = 225;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(315, acc[1]);
        }

        [UITestMethod]
        public void rightMidAt110Go2()
        {
            ball.Margin = new Thickness(150, 0, 0, 110);
            acc[0] = 2;
            acc[1] = 110;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(70, acc[1]);
        }

        [UITestMethod]
        public void rightMidAt250Go2()
        {
            ball.Margin = new Thickness(150, 0, 0, 110);
            acc[0] = 2;
            acc[1] = 250;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(290, acc[1]);
        }

        /*  Left  100 brick side */

        [UITestMethod]
        public void leftMidAt45Go2()
        {
            ball.Margin = new Thickness(95, 0, 0, 110);
            acc[0] = 2;
            acc[1] = 45;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(135, acc[1]);
        }

        [UITestMethod]
        public void leftMidAt315Go2()
        {
            ball.Margin = new Thickness(95, 0, 0, 110);
            acc[0] = 2;
            acc[1] = 315;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(225, acc[1]);
        }

        [UITestMethod]
        public void leftMidAt70Go2()
        {
            ball.Margin = new Thickness(95, 0, 0, 110);
            acc[0] = 2;
            acc[1] = 70;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(110, acc[1]);
        }

        [UITestMethod]
        public void leftMidAt290Go2()
        {
            ball.Margin = new Thickness(95, 0, 0, 110);
            acc[0] = 2;
            acc[1] = 290;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(250, acc[1]);
        }

        /*  Top  120 brick side */

        [UITestMethod]
        public void topMidAt315Go2()
        {
            ball.Margin = new Thickness(125, 0, 0, 120);
            acc[0] = 2;
            acc[1] = 315;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(45, acc[1]);
        }

        [UITestMethod]
        public void topMidAt225Go2()
        {
            ball.Margin = new Thickness(125, 0, 0, 120);
            acc[0] = 2;
            acc[1] = 225;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(135, acc[1]);
        }

        [UITestMethod]
        public void topMidAt200Go2()
        {
            ball.Margin = new Thickness(125, 0, 0, 120);
            acc[0] = 2;
            acc[1] = 200;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(160, acc[1]);
        }

        [UITestMethod]
        public void topMidAt340Go2()
        {
            ball.Margin = new Thickness(125, 0, 0, 120);
            acc[0] = 2;
            acc[1] = 340;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(20, acc[1]);
        }

        /*  Bottom  100 brick side */

        [UITestMethod]
        public void bottomMidAt45Go2()
        {
            ball.Margin = new Thickness(125, 0, 0, 95);
            acc[0] = 2;
            acc[1] = 45;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(315, acc[1]);
        }

        [UITestMethod]
        public void bottomMidAt135Go2()
        {
            ball.Margin = new Thickness(125, 0, 0, 95);
            acc[0] = 2;
            acc[1] = 135;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(225, acc[1]);
        }

        [UITestMethod]
        public void bottomMidAt160Go2()
        {
            ball.Margin = new Thickness(125, 0, 0, 95);
            acc[0] = 2;
            acc[1] = 160;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(200, acc[1]);
        }

        [UITestMethod]
        public void bottomMidAt20Go2()
        {
            ball.Margin = new Thickness(125, 0, 0, 95);
            acc[0] = 2;
            acc[1] = 20;
            Assert.IsTrue(Collide.hit(ball, acc, brick));
            Assert.AreEqual(340, acc[1]);
        }
    }
}
