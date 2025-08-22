using System.Collections.Generic;
using RobotBattle.Robot;
using RobotBattle.Weapon;

namespace RobotBattle.Game
{
    public class Robots
    {
        public List<RobotData> robots;

        private static Robots _instance;

        public static Robots instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Robots();
                }

                return _instance;
            }
        }

        private Robots()
        {
            initRobots();
        }

        private void initRobots()
        {
            robots = new List<RobotData>();
            RobotData robotData;

            //=====================Plower======================//
            robotData = new RobotData();
            robotData.name = "Plower";
            robotData.robotType = RobotType.Plower;
            robotData.currencyType = Currencies.Silver;
            robotData.price = 80000;
            robotData.health = 44000;
            robotData.shieldHealth = 22000;
            robotData.speed = 42;
            robotData.isBought = true;

            robotData.jump = true;
            robotData.fly = false;

            robotData.weapons = new WeaponParams[2]
            {
                new (WeaponType.Shot, 30, 20, 100, 10, 800),
                new (WeaponType.Rocket, 1830, 10, 1, 20, 600)
            };

            robots.Add(robotData);

            //=====================Dawn======================//
            robotData = new RobotData();
            robotData.name = "Dawn";
            robotData.robotType = RobotType.Dawn;

            robotData.currencyType = Currencies.Silver;
            robotData.price = 90000;

            robotData.health = 71000;
            robotData.shieldHealth = 35500;
            robotData.speed = 30;
            robotData.jump = false;
            robotData.fly = false;

            robotData.weapons = new WeaponParams[2]
            {
                new (WeaponType.Shot, 30, 20, 100, 10, 800),
                new (WeaponType.Fire, 30, 40, 200, 15, 700)
            };

            robots.Add(robotData);

            //=====================Phantom======================//
            robotData = new RobotData();
            robotData.name = "Phantom";
            robotData.robotType = RobotType.Phantom;

            robotData.currencyType = Currencies.Silver;
            robotData.price = 160000;

            robotData.health = 40000;
            robotData.shieldHealth = 20000;
            robotData.speed = 50;
            robotData.jump = true;
            robotData.fly = false;

            robotData.weapons = new WeaponParams[]
            {
                new (WeaponType.Rocket, 600, 15, 10, 18, 1100),
            };

            robots.Add(robotData);

            //=====================Drag Racer======================//
            robotData = new RobotData();
            robotData.name = "Drag Racer";
            robotData.robotType = RobotType.DragRacer;

            robotData.currencyType = Currencies.Silver;
            robotData.price = 240000;

            robotData.health = 85000;
            robotData.shieldHealth = 42500;
            robotData.speed = 32;
            robotData.jump = false;
            robotData.fly = false;

            robotData.weapons = new WeaponParams[]
            {
                new (WeaponType.Shot, 30, 30, 200, 10, 500),
                new (WeaponType.Laser, 900, 20, 5, 15, 1100),
                new (WeaponType.Rocket, 1830, 10, 2, 10, 1000)
            };

            robots.Add(robotData);

            //=====================Ghost======================//
            robotData = new RobotData();
            robotData.name = "Ghost";
            robotData.robotType = RobotType.Ghost;

            robotData.currencyType = Currencies.Gold;
            robotData.price = 400;

            robotData.health = 52700;
            robotData.shieldHealth = 26350;
            robotData.speed = 70;
            robotData.jump = true;
            robotData.fly = true;

            robotData.weapons = new WeaponParams[2]
            {
                new (WeaponType.Fire, 30, 40, 200, 15, 300),
                new (WeaponType.Shot, 30, 20, 100, 10, 800)
            };

            robots.Add(robotData);

            //=====================Black Pheonix======================//
            robotData = new RobotData();
            robotData.name = "Black Pheonix";
            robotData.robotType = RobotType.BlackPheonix;

            robotData.currencyType = Currencies.Silver;
            robotData.price = 700000;

            robotData.health = 76000;
            robotData.shieldHealth = 38000;
            robotData.speed = 43;
            robotData.jump = true;
            robotData.fly = false;

            robotData.weapons = new WeaponParams[2]
            {
                new (WeaponType.Laser, 900, 20, 5, 10, 1000),
                new (WeaponType.Rocket, 300, 6, 5, 10, 400)
            };

            robots.Add(robotData);

            //=====================Outcast======================//
            robotData = new RobotData();
            robotData.name = "Outcast";
            robotData.robotType = RobotType.Outcast;

            robotData.currencyType = Currencies.Silver;
            robotData.price = 2000000;

            robotData.health = 126000;
            robotData.shieldHealth = 63000;
            robotData.speed = 35;
            robotData.jump = false;
            robotData.fly = false;

            robotData.weapons = new WeaponParams[]
            {
                new (WeaponType.Shot, 30, 20, 100, 10, 800),
                new (WeaponType.Laser, 1350, 20, 5, 15, 1100),
            };

            robots.Add(robotData);

            //=====================Weaver======================//
            robotData = new RobotData();
            robotData.name = "Weaver";
            robotData.robotType = RobotType.Weaver;

            robotData.currencyType = Currencies.Silver;
            robotData.price = 3500000;

            robotData.health = 116480;
            robotData.shieldHealth = 58240;
            robotData.speed = 38;
            robotData.jump = true;
            robotData.fly = false;

            robotData.weapons = new WeaponParams[]
            {
                new (WeaponType.Laser, 900, 20, 5, 15, 1100),
                new (WeaponType.Rocket, 1830, 10, 2, 12, 600),
            };

            robots.Add(robotData);

            //=====================Jugernout======================//
            robotData = new RobotData();
            robotData.name = "Jugernout";
            robotData.robotType = RobotType.Jugernout;

            robotData.currencyType = Currencies.Gold;
            robotData.price = 4000;

            robotData.health = 141000;
            robotData.shieldHealth = 70500;
            robotData.speed = 31;
            robotData.jump = false;
            robotData.fly = false;

            robotData.weapons = new WeaponParams[]
            {
                new (WeaponType.Fire, 30, 40, 200, 15, 300),
                new (WeaponType.Rocket, 600, 15, 10, 17, 800),
                new (WeaponType.Shot, 30, 20, 100, 10, 800),
            };

            robots.Add(robotData);
        }

        public RobotData getRobotByType(RobotType robotType)
        {
            foreach (var robot in robots)
            {
                if (robot.robotType == robotType)
                {
                    return robot;
                }
            }

            return null;
        }
    }
}