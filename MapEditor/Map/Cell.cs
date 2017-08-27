using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor 
{
    public enum Landscape { None, Field, Desert, Forest, Water, Bricks, Lava, Sign }

    public class Cell
    {
        public string RussianMessage;
        public string EnglishMessage;
        public Landscape LandscapeId;
        public bool IsPassable;
        public bool IsTaskable;
        public int TaskId;
        public bool IsTeleport;
        public int[] TeleportCoords;

        public override string ToString()
        {
            string result = "";

            result += "RM=" + RussianMessage + "|";
            result += "EM=" + EnglishMessage + "|";

            string lnd;

            switch (LandscapeId)
            {
                case Landscape.Field:
                    lnd = "Fl";
                    break;
                case Landscape.Desert:
                    lnd = "Ds";
                    break;
                case Landscape.Forest:
                    lnd = "Fr";
                    break;
                case Landscape.Water:
                    lnd = "Wt";
                    break;
                case Landscape.Bricks:
                    lnd = "Br";
                    break;
                case Landscape.Lava:
                    lnd = "Lv";
                    break;
                case Landscape.Sign:
                    lnd = "Sg";
                    break;
                default:
                    lnd = "";
                    break;
            }

            result += "L=" + lnd + "|";
            result += "P=" + (IsPassable ? "1" : "0") + "|";
            result += "T=" + (IsTaskable ? TaskId.ToString() : "") + "|";
            result += "Tp=" + (IsTeleport ? String.Join(",", TeleportCoords) : "");

            return result;
        }

        public Cell()
        {
            
        }

        public Cell(string configurarion)
        {
            foreach (var configstring in configurarion.Split('|'))
            {
                if (configstring.StartsWith("RM="))
                    RussianMessage = configstring.Substring("RM=".Length);

                if (configstring.StartsWith("EM="))
                    EnglishMessage = configstring.Substring("EM=".Length);

                if (configstring.StartsWith("L="))
                {
                    switch (configstring.Substring("L=".Length))
                    {
                        case "Fl":
                            LandscapeId = Landscape.Field;
                            break;
                        case "Ds":
                            LandscapeId = Landscape.Desert;
                            break;
                        case "Fr":
                            LandscapeId = Landscape.Forest;
                            break;
                        case "Wt":
                            LandscapeId = Landscape.Water;
                            break;
                        case "Br":
                            LandscapeId = Landscape.Bricks;
                            break;
                        case "Lv":
                            LandscapeId = Landscape.Lava;
                            break;
                        case "Sg":
                            LandscapeId = Landscape.Lava;
                            break;
                        default:
                            LandscapeId = Landscape.None;
                            break;
                    }
                }
                
                if (configstring.StartsWith("P="))
                {
                    if (configstring.Substring("P=".Length) == "1")
                        IsPassable = true;
                    else
                        IsPassable = false;
                }

                if (configstring.StartsWith("T="))
                {
                    if (configstring.Substring("T=".Length) == "")
                        IsTaskable = false;
                    else
                    {
                        IsTaskable = true;
                        int id;
                        if (int.TryParse(configstring.Substring("T=".Length), out id))
                        {
                            TaskId = id;
                        }
                        else
                        {
                            IsTaskable = false;
                        }
                    }
                }

                if (configstring.StartsWith("Tp="))
                {
                    if (configstring.Substring("Tp=".Length) == "")
                    {
                        IsTeleport = false;
                    }
                    else
                    {
                        try
                        {
                            var c = configstring.Substring("Tp=".Length).Split(',');

                            int x = int.Parse(c[0]), y = int.Parse(c[1]);

                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.Message);
                            IsTeleport = false;
                        }
                    }
                }
            }
        }
    }
}