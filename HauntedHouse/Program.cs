using System;

namespace HauntedHouse
{
    /// <summary>
    /// Haunted House Adventure
    /// Original BASIC version from the 1983 Usborne computer book
    /// Write Your Own Adventure Programs for Your Microcomputer
    /// by Jenny Tyler && Les Howarth (ISBN 0-86020-741-2)
    /// Adapted from lau version by Marc Lepage (May 2014) -- https://github.com/mlepage/haunted-house
    /// Also see version in Ruby by JOSHUA M. CLULOW (2010) -- URL ?
    /// Found also another Ruby one -- https://github.com/dagbrown/haunted-house
    /// Java Version created by Ivan Stuart on 03/10/2016 -- https://gist.github.com/ivstuart/50112a31a6657001ca52c2d94c30d4b4
    /// From this one, I created a C# Version - Stephan Huewe 12/2019
  
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("HAUNTED HOUSE");
            Console.WriteLine("--------------");

            HauntedHouse hauntedHouse = new HauntedHouse();

            for (;;)
            {
                hauntedHouse.main();
            }
        }
    }



    public partial class HauntedHouse
    {
        private bool InstanceFieldsInitialized = false;

        private void InitializeInstanceFields()
        {
            C = new int[W + 1];
            F = new int[W + 1];
        }


        private int V = 25;
        private int W = 36;
        private int G = 18;

        private int[] C;

        private int[] L = new int[] {46, 38, 35, 50, 13, 18, 28, 42, 10, 25, 26, 4, 2, 7, 47, 60, 43, 32};

        private string[] AS = new string[]
        {
            "HELP", "CARRYING", "GO", "N", "S", "W", "E", "U", "D", "GET", "TAKE", "OPEN", "EXAMINE", "READ", "SAY",
            "DIG", "SWING", "CLIMB", "LIGHT", "UNLIGHT", "SPRAY", "USE", "UNLOCK", "LEAVE", "SCORE"
        };

        private string[] RS = new string[]
        {
            "SE", "WE", "WE", "SWE", "WE", "WE", "SWE", "WS", "NS", "SE", "WE", "NW", "SE", "W", "NE", "NSW", "NS",
            "NS", "SE", "WE", "NWUD", "SE", "WSUD", "NS", "N", "NS", "NSE", "WE", "WE", "NSW", "NS", "NS", "S", "NSE",
            "NSW", "S", "NSUD", "N", "N", "NS", "NE", "NW", "NE", "W", "NSE", "WE", "W", "NS", "SE", "NSW", "E", "WE",
            "NW", "S", "SW", "NW", "NE", "NWE", "WE", "WE", "WE", "NWE", "NWE", "W"
        };

        private string[] DS = new string[]
        {
            "DARK CORNER", "OVERGROWN GARDEN", "BY LARGE WOODPILE", "YARD BY RUBBISH", "WEED PATCH", "FOREST",
            "THICK FOREST", "BLASTED TREE", "CORNER OF HOUSE", "ENTRANCE TO KITCHEN", "KITCHEN AND GRIMY COOKER",
            "SCULLERY DOOR", "ROOM WITH INCHES OF DUST", "REAR TURRET ROOM", "CLEARING BY HOUSE", "PATH",
            "SIDE OF HOUSE", "BACK OF HALLWAY", "DARK ALCOVE", "SMALL DARK ROOM", "BOTTOM OF SPIRAL STAIRCASE",
            "WIDE PASSAGE", "SLIPPERY STEPS", "CLIFFTOP", "NEAR CRUMBLING WALL", "GLOOMY PASSAGE", "POOL OF LIGHT",
            "IMPRESSIVE VAULTED HALLWAY", "HALL BY THICK WOODEN DOOR", "TROPHY ROOM", "CELLAR WITH BARRED WINDOW",
            "CLIFF PATH", "CUPBOARD WITH HANGING COAT", "FRONT HALL", "SITTING ROOM", "SECRET ROOM",
            "STEEP MARBLE STAIRS", "DINING ROOM", "DEEP CELLAR WITH COFFIN", " CLIFF PATH", "CLOSET", "FRONT LOBBY",
            "LIBRARY OF EVIL BOOKS", "STUDY WITH DESK & HOLE IN WALL", "WEIRD COBWEBBY ROOM", "VERY COLD CHAMBER",
            "SPOOKY ROOM", "CLIFF PATH BY MARSH", "RUBBLE-STREWN VERANDA", "FRONT PORCH", "FRONT TOWER",
            "SLOPING CORRIDOR", "UPPER GALLERY", "MARSH BY WALL", "MARSH", "SOGGY PATH", "BY TWISTED RAILING",
            "PATH THROUGH IRON GATE", "BY RAILINGS", "BENEATH FRONT TOWER", "DEBRIS FROM CRUMBLING FACADE",
            "LARGE FALLEN BRICKWORK", "ROTTING STONE ARCH", "CRUMBLING CLIFFTOP"
        };

        private string[] OS = new string[]
        {
            "PAINTING", "RING", "MAGIC SPELLS", "GOBLET", "SCROLL", "COINS", "STATUE", "CANDLESTICK", "MATCHES",
            "VACUUM", "BATTERIES", "SHOVEL", "AXE", "ROPE", "BOAT", "AEROSOL", "CANDLE", "KEY", "NORTH", "SOUTH",
            "WEST", "EAST", "UP", "DOWN", "DOOR", "BATS", "GHOSTS", "DRAWER", "DESK", "COAT", "RUBBISH", "COFFIN",
            "BOOKS", "XZANFAR", "WALL", "SPELLS"
        };

        private int LL = 60;
        private int RM = 57;
        private string MS = "OK";

        private int VB = 0;
        private int OB = 0;
        private string VS = "";
        private string WS = "";

        private int[] F;

        public HauntedHouse()
        {
            if (!InstanceFieldsInitialized)
            {
                InitializeInstanceFields();
                InstanceFieldsInitialized = true;
            }

            F[18] = 1;
            F[17] = 1;
            F[2] = 1;
            F[26] = 1;
            F[28] = 1;
            F[23] = 1;
        }

        public void help()
        {
            Console.WriteLine("WORDS I KNOW:");
            foreach (string verb in AS)
            {
                Console.WriteLine(verb);
            }
        }

        public void carrying()
        {
            Console.WriteLine("YOU ARE CARRYING:");
            for (int i = 1; i < C.Length; i++)
            {
                if (C[i] == 1)
                {
                    Console.WriteLine(OS[i - 1]);
                }
            }
        }

        public void go()
        {
            int D = 0;
            if (OB == 0)
            {
                D = VB - 3;
            }

            if (OB == 19)
            {
                D = 1;
            }

            if (OB == 20)
            {
                D = 2;
            }

            if (OB == 21)
            {
                D = 3;
            }

            if (OB == 22)
            {
                D = 4;
            }

            if (OB == 23)
            {
                D = 5;
            }

            if (OB == 24)
            {
                D = 6;
            }

            if (RM == 20 && D == 5)
            {
                D = 1;
            }

            if (RM == 20 && D == 6)
            {
                D = 3;
            }

            if (RM == 22 && D == 6)
            {
                D = 2;
            }

            if (RM == 22 && D == 5)
            {
                D = 3;
            }

            if (RM == 36 && D == 6)
            {
                D = 1;
            }

            if (RM == 36 && D == 5)
            {
                D = 2;
            }

            if (F[14] == 1)
            {
                MS = "CRASH! YOU FELL OUT OF THE TREE!";
                F[14] = 0;
                return;
            }

            if (F[27] == 1 && RM == 52)
            {
                MS = "GHOSTS WILL NOT LET YOU MOVE";
                return;
            }

            if (RM == 45 && C[1] == 1 && F[34] == 0)
            {
                MS = "A MAGICAL BARRIER TO THE WEST";
                return;
            }

            if ((RM == 26 && F[0] == 0) && (D == 1 || D == 4))
            {
                MS = "YOU NEED A LIGHT";
                return;
            }

            if (RM == 54 && C[15] != 1)
            {
                MS = "YOU'RE STUCK!";
                return;
            }

            if (C[15] == 1 && !(RM == 53 || RM == 54 || RM == 55 || RM == 47))
            {
                MS = "YOU CAN'T CARRY A BOAT!";
                return;
            }

            if (RM > 26 && RM < 30 && F[0] == 0)
            {
                MS = "TOO DARK TO MOVE";
                return;
            }

            F[35] = 0;
            for (int i = 0; i < (RS[RM]).Length; i++)
            {
                string US = RS[RM].Substring(i, 1);

                if (US.Equals("N", StringComparison.OrdinalIgnoreCase) && D == 1 && F[35] == 0)
                {
                    RM = RM - 8;
                    F[35] = 1;
                    break;
                }

                if (US.Equals("S", StringComparison.OrdinalIgnoreCase) && D == 2 && F[35] == 0)
                {
                    RM = RM + 8;
                    F[35] = 1;
                    break;
                }

                if (US.Equals("W", StringComparison.OrdinalIgnoreCase) && D == 3 && F[35] == 0)
                {
                    RM = RM - 1;
                    F[35] = 1;
                    break;
                }

                if (US.Equals("E", StringComparison.OrdinalIgnoreCase) && D == 4 && F[35] == 0)
                {
                    RM = RM + 1;
                    F[35] = 1;
                    break;
                }
            }

            MS = "OK";
            if (F[35] == 0)
            {
                MS = "CAN'T GO THAT WAY!";
            }

            if (D < 1)
            {
                MS = "GO WHERE?";
            }

            if (RM == 41 && F[23] == 1)
            {
                RS[49] = "SW";
                MS = "THE DOOR SLAMS SHUT!";
                F[23] = 0;
            }
        }

        public void get()
        {
            if (OB > G || OB == 0)
            {
                MS = "I CAN'T GET " + WS;
                return;
            }

            if (L[OB - 1] != RM)
            {
                MS = "IT ISN'T HERE";
            }

            if (F[OB] != 0)
            {
                MS = "WHAT " + WS + "?";
            }

            if (C[OB] == 1)
            {
                MS = "YOU ALREADY HAVE IT";
            }

            if (OB > 0 && L[OB - 1] == RM && F[OB] == 0)
            {
                C[OB] = 1;
                L[OB - 1] = 65;
                MS = "YOU HAVE THE " + WS;
            }
        }


        public void open()
        {
            if (RM == 43 && (OB == 28 || OB == 29))
            {
                F[17] = 0;
            }

            MS = "DRAWER OPEN";
            if ((RM == 28) && (OB == 25))
            {
                MS = "IT'S LOCKED";
            }

            if ((RM == 38) && (OB == 32))
            {
                MS = "THAT'S CREEPY!";
            }

            F[2] = 0;
        }

        public void examine()
        {
            if (OB == 30)
            {
                F[18] = 0;
                MS = "SOMETHING HERE!";
            }

            if (OB == 31)
            {
                MS = "THAT'S DISGUSTING!";
            }

            if (OB == 28 || OB == 29)
            {
                MS = "THERE IS A DRAWER";
            }

            if ((OB == 33) || (OB == 5))
            {
                read();
            }

            if ((RM == 43) && (OB == 35))
            {
                MS = "THERE IS SOMETHING BEYOND...";
            }

            if (OB == 32)
            {
                open();
            }
        }

        public void read()
        {
            if (RM == 42 && OB == 33)
            {
                MS = "THEY ARE DEMONIC WORKS";
            }

            if ((OB == 3 || OB == 36) && (C[3] == 1 && F[34] == 0))
            {
                MS = "USE THIS WORD WITH CARE 'XZANFAR'";
            }

            if (C[5] == 1 && OB == 5)
            {
                MS = "THE SCRIPT IS IN AN ALIEN TONGUE";
            }
        }

        public void say()
        {
            MS = "OK '" + WS + "'";
            if ((C[3] == 1) && (OB == 34))
            {
                MS = "*MAGIC OCCURS*";
            }

            if (RM != 45)
            {
                RM = random(63);
            }

            if (C[3] == 1 && OB == 34 && RM == 45)
            {
                F[34] = 1;
            }
        }

        public void dig()
        {
            if (C[12] == 1)
            {
                MS = "YOU MADE A HOLE";
            }

            if (C[12] == 1 && RM == 30)
            {
                MS = "DUG THE BARS OUT";
                DS[RM] = "HOLE IN WALL";
                RS[RM] = "NSE";
            }
        }

        public void swing()
        {
            if (C[14] != 1 && RM == 7)
            {
                MS = "THIS IS NO TIME TO PLAY GAMES";
            }

            if (OB == 14 && C[14] == 1)
            {
                MS = "YOU SWUNG IT";
            }

            if (OB == 13 && C[13] == 1)
            {
                MS = "WHOOSH";
            }

            if (OB == 13 && C[13] == 1 && RM == 43)
            {
                RS[RM] = "WN";
                DS[RM] = "STUDY WITH SECRET ROOM";
                MS = "YOU BROKE THE THIN WALL";
            }
        }

        public void climb()
        {
            if (OB == 14 && C[14] == 1)
            {
                MS = "IT ISN'T ATTACHED TO ANYTHING!";
            }

            if (OB == 14 && C[14] != 1 && RM == 7 && F[14] == 0)
            {
                MS = "YOU SEE THICK FOREST AND CLIFF SOUTH";
                F[14] = 1;
                return;
            }

            if (OB == 14 && C[14] != 1 && RM == 7 && F[14] == 1)
            {
                MS = "GOING DOWN!";
                F[14] = 0;
            }
        }

        public void light()
        {
            if (OB == 17 && C[17] == 1 && C[8] == 0)
            {
                MS = "IT WILL BURN YOUR HANDS";
            }

            if (OB == 17 && C[17] == 1 && C[9] == 0)
            {
                MS = "NOTHING TO LIGHT IT WITH";
            }

            if (OB == 17 && C[17] == 1 && C[9] == 1 && C[8] == 1)
            {
                MS = "IT CASTS A FLICKERING LIGHT";
                F[0] = 1;
            }
        }

        public void unlight()
        {
            if (F[0] == 1)
            {
                F[0] = 0;
                MS = "EXTINGUISHED";
            }
        }

        public void spray()
        {
            if (OB == 26 && C[16] == 1)
            {
                MS = "HISSSS";
            }

            if (OB == 26 && C[16] == 1 && F[26] == 1 && RM == 13)
            {
                F[26] = 0;
                MS = "PFFT! GOT THEM";
            }
        }

        public void use()
        {
            if (OB == 10 && C[10] == 1 && C[11] == 1)
            {
                MS = "SWITCHED ON";
                F[24] = 1;
            }

            if (F[27] == 1 && F[24] == 1)
            {
                MS = "WHIZZ- VACUUMED THE GHOSTS UP!";
                F[27] = 0;
            }
        }

        public void unlock()
        {
            if (RM == 43 && (OB == 27 || OB == 28))
            {
                open();
            }

            if (RM == 28 && OB == 25 && F[25] == 0 && C[18] == 1)
            {
                F[25] = 1;
                RS[RM] = "SEW";
                DS[RM] = "HUGE OPEN DOOR";
                MS = "THE KEY TURNS!";
            }
        }

        public void leave()
        {
            if (C[OB] == 1)
            {
                C[OB] = 0;
                L[OB - 1] = RM;
                MS = "DONE";
            }
        }


        public void score()
        {
            int S = 0;
            for (int i = 0; i < C.Length; i++)
            {
                if (C[i] == 1)
                {
                    S++;
                }
            }

            if (S == 17 && C[15] != 1 && RM != 57)
            {
                Console.WriteLine("YOU HAVE EVERYTHING");
                Console.WriteLine("RETURN TO THE GATE FOR FINAL SCORE");
            }

            if (S == 17 && RM == 57)
            {
                Console.WriteLine("DOUBLE SCORE FOR REACHING HERE!");
                S = S * 2;
            }

            Console.WriteLine("YOUR SCORE=" + S);
            if (S > 18)
            {
                Console.WriteLine("WELL DONE! YOU FINISHED THE GAME");
                Environment.Exit(0);
            }
        }


        public void main()
        {
            Console.WriteLine("YOUR LOCATION");
            Console.WriteLine((DS[RM]));

            Console.WriteLine("EXITS:");
            Console.WriteLine((RS[RM]));
            
            for (int i = 0; i < G; i++)
            {

                if (L[i] == RM && F[i + 1] == 0)
                {
                    Console.WriteLine("YOU CAN SEE " + OS[i] + " HERE");
                }
            }


            Console.WriteLine("============================");
            Console.WriteLine((MS));
            MS = "WHAT";


            Console.WriteLine("WHAT WILL YOU DO NOW ");
            string QS = Console.ReadLine();

            VS = "";
            WS = "";
            VB = 0;
            OB = 0;

            string[] words = QS.Split(" ", 2);

            VS = words[0];
            if (words.Length == 2)
            {
                WS = words[1];
            }

            if (words.Length == 1)
            {
                VS = QS;
            }

            for (int i = 0; i < AS.Length; i++)
            {
                if (VS.Equals(AS[i], StringComparison.InvariantCultureIgnoreCase))
                {
                    VB = i + 1;
                    break;
                }
            }

            for (int i = 0; i < OS.Length; i++)
            {
                if (WS.Equals(OS[i], StringComparison.InvariantCultureIgnoreCase))
                {
                    OB = i + 1;
                    break;
                }
            }


            if (WS.Length > 0 && OB == 0)
            {
                MS = "THAT'S SILLY";
            }

            if (VB == 0)
            {
                VB = V + 1;
            }

            if (WS.Equals(""))
            {
                MS = "I NEED TWO WORDS";
            }

            if (VB > V && OB > 0)
            {
                MS = "YOU CAN'T '" + QS + "'";
            }

            if (VB > V && OB == 0)
            {
                MS = "YOU DON'T MAKE SENSE";
                return;
            }

            if (VB < V && OB > 0 && C[OB] == 0)
            {
                MS = "YOU DON'T HAVE '" + WS + "'";
            }

            if (F[26] == 1 && RM == 13 && random(3) != 3 && VB != 21)
            {
                MS = "BATS ATTACKING!";
                return;
            }

            if (RM == 44 && random(2) == 1 && F[24] != 1)
            {
                F[27] = 1;
            }

            if (F[0] == 1)
            {
                LL = LL - 1;
            }

            if (LL < 1)
            {
                F[0] = 0;
            }
            
            if (VB > 2 && VB < 9)
            {
                go();
            }
            else if (VB == 10 || VB == 11)
            {
                get();
            }
            else
            {
                if (VB > 0)
                {

                    Type c = typeof(HauntedHouse);
                    try
                    {
                        System.Reflection.MethodInfo method = c.GetMethod(VS.ToLower());
                        method.Invoke(this, null);
                    }
                    catch (Exception ex)
                    {
                        MS = "YOU CAN'T '" + QS + "'";
                    }
                    
                }
            }

            if (LL == 10)
            {
                MS = "YOUR CANDLE IS WANING!";
            }

            if (LL == 1)
            {
                MS = "YOUR CANDLE IS OUT!";
            }

        }

        public  int random(int range)
        {
            return (int) (1 + (GlobalRandom.NextDouble * range));
        }

        internal static class GlobalRandom
        {
            private static System.Random randomInstance = null;

            public static double NextDouble
            {
                get
                {
                    if (randomInstance == null)
                        randomInstance = new System.Random();

                    return randomInstance.NextDouble();
                }
            }
        }
    }
}
