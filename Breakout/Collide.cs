using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Shapes;

namespace Breakout
{
    public static class Collide
    {
        public static bool hit(Ellipse ball, List<int> acceleration, Rectangle obj)
        {
            //http://stackoverflow.com/questions/563198/how-do-you-detect-where-two-line-segments-intersect
            //http://stackoverflow.com/questions/1073336/circle-line-segment-collision-detection-algorithm
            // x left-right: cos y up-down: sin
            double moveX = acceleration[0] * Math.Cos((acceleration[1] * Math.PI) / 180);
            double moveY = acceleration[0] * Math.Sin((acceleration[1] * Math.PI) / 180);
            double mult = Math.Pow(acceleration[0] * 4, 4);

            double centerX = ball.Margin.Left + (ball.Width / 2);
            double centerY = ball.Margin.Bottom + (ball.Height / 2);
            double radius = (ball.Height / 2);

            //sqtr((x2-x1)^2 + (y2-y1)^2) 2 is the wall, 1 is the ball
            double dist = 0;
            double moveDist = 0;
            double a;
            double b;
            double c;
            double d;
            double t1;
            double t2;

            double leftBrick = obj.Margin.Left;
            double rightBrick = obj.Margin.Left + obj.Width;
            double bottomBrick = obj.Margin.Bottom;
            double topBrick = obj.Margin.Bottom + obj.Height;

            /*        for top right corner of brick         */
            dist = Math.Sqrt(Math.Pow(rightBrick - centerX, 2) + Math.Pow(topBrick - centerY, 2));
            moveDist = Math.Sqrt(Math.Pow(rightBrick - (centerX + moveX), 2) + Math.Pow(topBrick - (centerY + moveY), 2));
            if (dist >= radius && moveDist <= radius && acceleration[1] == 225)
            {
                acceleration[1] = acceleration[1] - 180;
                return true;
            }

            /*        for top left corner of brick         */
            dist = Math.Sqrt(Math.Pow(leftBrick - centerX, 2) + Math.Pow(topBrick - centerY, 2));
            moveDist = Math.Sqrt(Math.Pow(leftBrick - (centerX + moveX), 2) + Math.Pow(topBrick - (centerY + moveY), 2));
            if (dist >= radius && moveDist <= radius && acceleration[1] == 315)
            {
                acceleration[1] = acceleration[1] - 180;
                return true;
            }

            /*        for bottom right corner of brick         */
            dist = Math.Sqrt(Math.Pow(rightBrick - centerX, 2) + Math.Pow(bottomBrick - centerY, 2));
            moveDist = Math.Sqrt(Math.Pow(rightBrick - (centerX + moveX), 2) + Math.Pow(bottomBrick - (centerY + moveY), 2));
            if (dist >= radius && moveDist <= radius && acceleration[1] == 135)
            {
                acceleration[1] = acceleration[1] + 180;
                return true;
            }

            /*        for bottom left corner of brick         */
            dist = Math.Sqrt(Math.Pow(leftBrick - centerX, 2) + Math.Pow(bottomBrick - centerY, 2));
            moveDist = Math.Sqrt(Math.Pow(leftBrick - (centerX + moveX), 2) + Math.Pow(bottomBrick - (centerY + moveY), 2));
            if (dist >= radius && moveDist <= radius && acceleration[1] == 45)
            {
                acceleration[1] = acceleration[1] + 180;
                return true;
            }

            /*        for top of brick         */
            a = Math.Pow(rightBrick - leftBrick, 2);
            b = 2 * ((leftBrick - (centerX + moveX)) * (rightBrick - leftBrick));
            c = (Math.Pow(leftBrick - (centerX + moveX), 2) + Math.Pow(topBrick - (centerY + moveY), 2)) - Math.Pow(radius, 2);
            d = Math.Pow(b, 2) - (4 * a * c);
            t1 = (-b - Math.Sqrt(d)) / (2 * a);
            t2 = (-b + Math.Sqrt(d)) / (2 * a);

            if ((t1 >= 0 && t1 <= 1 && t2 >= 0) || (t1 < 0 && t2 >= 0 && t2 <= 1))
            {
                double angle = (180 / Math.PI) * Math.Atan2(Math.Abs(topBrick - (centerY - ( mult * moveY))), Math.Abs(centerX - ( mult * moveX)));
                if (moveX > 0)
                {
                    acceleration[1] = (int)Math.Floor(angle);
                }
                else
                {
                    acceleration[1] = 180 - (int)Math.Ceiling(angle);
                }
                return true;
            }

            /*        for bottom of brick         */
            a = Math.Pow(rightBrick - leftBrick, 2);
            b = 2 * ((leftBrick - (centerX + moveX)) * (rightBrick - leftBrick));
            c = (Math.Pow(leftBrick - (centerX + moveX), 2) + Math.Pow(bottomBrick - (centerY + moveY), 2)) - Math.Pow(radius, 2);
            d = Math.Pow(b, 2) - (4 * a * c);
            t1 = (-b - Math.Sqrt(d)) / (2 * a);
            t2 = (-b + Math.Sqrt(d)) / (2 * a);

            if ((t1 >= 0 && t1 <= 1 && t2 >= 0) || (t1 < 0 && t2 >= 0 && t2 <= 1))
            {
                double angle = (180 / Math.PI) * Math.Atan2(Math.Abs(bottomBrick - (centerY - (mult * moveY))), Math.Abs(centerX - (mult * moveX)));
                if (moveX > 0 )
                {
                    acceleration[1] = 360 - (int)Math.Floor(angle);
                }
                else
                {
                    acceleration[1] = 180 + (int)Math.Ceiling(angle);
                }
                return true;
            }

            /*        for left of brick         */
            a = Math.Pow(topBrick - bottomBrick, 2);
            b = 2 * ((bottomBrick - (centerY + moveY)) * (topBrick - bottomBrick));
            c = (Math.Pow(leftBrick - (centerX + moveX), 2) + Math.Pow(bottomBrick - (centerY + moveY), 2)) - Math.Pow(radius, 2);
            d = Math.Pow(b, 2) - (4 * a * c);
            t1 = (-b - Math.Sqrt(d)) / (2 * a);
            t2 = (-b + Math.Sqrt(d)) / (2 * a);

            if ((t1 >= 0 && t1 <= 1 && t2 >= 0) || (t1 < 0 && t2 >= 0 && t2 <= 1))
            {
                double angle = (180 / Math.PI) * Math.Atan2(Math.Abs(centerY - (mult * moveY)), Math.Abs(leftBrick - (centerX - (mult * moveX))));
                if (moveY > 0)
                {
                    acceleration[1] = 180 - (int)Math.Ceiling(angle);
                }
                else
                {
                    acceleration[1] = 270 - (int)Math.Floor(angle);
                }
                return true;
            }

            /*        for right of brick         */
            a = Math.Pow(topBrick - bottomBrick, 2);
            b = 2 * ((bottomBrick - (centerY + moveY)) * (topBrick - bottomBrick));
            c = (Math.Pow(rightBrick - (centerX + moveX), 2) + Math.Pow(bottomBrick - (centerY + moveY), 2)) - Math.Pow(radius, 2);
            d = Math.Pow(b, 2) - (4 * a * c);
            t1 = (-b - Math.Sqrt(d)) / (2 * a);
            t2 = (-b + Math.Sqrt(d)) / (2 * a);

            if ((t1 >= 0 && t1 <= 1 && t2 >= 0) || (t1 < 0 && t2 >= 0 && t2 <= 1))
            {
                double angle = (180 / Math.PI) * Math.Atan2(Math.Abs(centerY - (mult * moveY)), Math.Abs(rightBrick - (centerX - (mult * moveX))));
                if (moveY > 0)
                {
                    angle = Math.Ceiling(angle);
                    acceleration[1] = (int)angle;
                }
                else
                {
                    angle = Math.Floor(angle);
                    acceleration[1] = 360 - (int)angle;
                }
                return true;
            }
            return false;
        }

        public static void wall(Ellipse ball, List<int> acc, double bounds, char w)
        {
            double xmov = acc[0] * Math.Cos((acc[1] * Math.PI) / 180);
            double ymov = acc[0] * Math.Sin((acc[1] * Math.PI) / 180);

            double left = ball.Margin.Left;
            double right = ball.Margin.Left + ball.Width;
            double top = ball.Margin.Bottom + ball.Height;

            double leftacc = left + xmov;
            double rightacc = right + xmov;
            double topacc = top + ymov;

            if (left >= bounds && leftacc <= bounds && w == 'l')
            { 
                int rot = acc[1] - 90;
                acc[1] = 90 - rot;
            }
            if (right <= bounds && rightacc >= bounds && w == 'r')
            {
                acc[1] = 180 - acc[1];
            }
            if ((top >= bounds || topacc >= bounds) && w == 't')
            {
                acc[1] = 360 - acc[1];
            }
        }
    }
}
