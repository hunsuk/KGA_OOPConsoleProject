using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZombieGame.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZombieGame.Utility
{
    public class Map
    {
        private int width;
        private int hight;
        private int invenHight;
        private List<Pixel> map = null;

        public Map(int width, int hight, int invenHight)
        {
            this.width = width;
            this.hight = hight;
            this.invenHight = invenHight;

            InitMap();
        }

        private void InitMap()
        {
            map = new List<Pixel>((hight + invenHight) * width);
            for (int i = 0; i < hight + invenHight; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map.Add(new Pixel(new Vec2(i, j)));
                }
            }
        }

        public List<Pixel> GetMap()
        {
            return map;
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHight()
        {
            return hight + invenHight;
        }

        public int GetInvenHight()
        {
            return invenHight;
        }

        public void UpdateObjects(List<Isbeing> objects)
        {
            InitMap();
            foreach (Isbeing isbeing in objects)
            {
                map[isbeing.GetPosition().GetX() + width * isbeing.GetPosition().GetY()].SetIsBeing(isbeing);
            }
        }
    }
}
