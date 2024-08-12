using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieGame.Utility;

namespace ZombieGame.Object
{
    public class Zombie : People
    {
        private List<AstarNode> openNode;
        private List<AstarNode> closeNode;
        private AstarNode search;
        public Zombie(int x, int y) : base(x, y)
        {

        }

        public override char GetSign()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            return 'Z';
        }

        public void Move(User user, Map map)
        {
            
            openNode = new List<AstarNode>();
            closeNode = new List<AstarNode>();
            AstarNode root = new AstarNode(this.GetPosition(), AstarPath.CalcEuclid(this.GetPosition(), user.GetPosition()), 1, null);
            search = root;
            closeNode.Add(root);

            while (true)
            {
                Search(search, user, map);
                
                if (openNode.Count == 0)
                {
                    return;
                }

                Select();

                if (search.position == user.GetPosition())
                {
                    if (search.parent.parent == null)
                    {
                        this.SetPostion(search.position.GetX(), search.position.GetY());
                    } else
                    {
                        AstarNode temp = search.parent;

                        while (temp.parent.parent != null)
                        {
                            temp = temp.parent;
                        }
                        this.SetPostion(temp.position.GetX(), temp.position.GetY());
                    }
                   
                    break;
                }
            }

        }

    

        private void Select()
        {
            double min = openNode[0].Fscore;
            int index = 0;
            for (int i = 1; i < openNode.Count; i++)
            {
                if (min > openNode[i].Fscore)
                {
                    min = openNode[i].Fscore;
                    index = i;
                }
            }
            search = openNode[index];
            closeNode.Add(openNode[index]);
            openNode.Remove(openNode[index]);
        }

        private void Search(AstarNode search, User user, Map map)
        {
            if (map.GetMap()[search.position.GetX() + map.GetWidth() * (search.position.GetY() - 1)].IsGo())
            {
                Vec2 postion = new Vec2(search.position.GetX(), search.position.GetY() - 1);
                AstarNode target = new AstarNode(postion, AstarPath.CalcEuclid(postion, user.GetPosition()), search.Gscore + 1 ,search);
                if (!closeNode.Contains(target)) {
                    if (openNode.Contains(target))
                    {
                        if (openNode[openNode.IndexOf(target)].Fscore > target.Fscore)
                        {
                            openNode[openNode.IndexOf(target)] = target;
                        }
                    }
                    else
                    {
                        openNode.Add(new AstarNode(postion, AstarPath.CalcEuclid(postion, user.GetPosition()), search.Gscore + 1, search));
                    }
                }
            }

            if (map.GetMap()[search.position.GetX() + map.GetWidth() * (search.position.GetY() + 1)].IsGo())
            {
                Vec2 postion = new Vec2(search.position.GetX(), search.position.GetY() + 1);
                AstarNode target = new AstarNode(postion, AstarPath.CalcEuclid(postion, user.GetPosition()), search.Gscore + 1, search);
                if (!closeNode.Contains(target))
                {
                    if (openNode.Contains(target))
                    {
                        if (openNode[openNode.IndexOf(target)].Fscore > target.Fscore)
                        {
                            openNode[openNode.IndexOf(target)] = target;
                        }
                    }
                    else
                    {
                        openNode.Add(new AstarNode(postion, AstarPath.CalcEuclid(postion, user.GetPosition()), search.Gscore + 1, search));
                    }
                }
            }

            if (map.GetMap()[(search.position.GetX() - 1) + map.GetWidth() * search.position.GetY()].IsGo())
            {
                Vec2 postion = new Vec2(search.position.GetX() - 1, search.position.GetY());
                AstarNode target = new AstarNode(postion, AstarPath.CalcEuclid(postion, user.GetPosition()), search.Gscore + 1, search);
                if (!closeNode.Contains(target))
                {
                    if (openNode.Contains(target))
                    {
                        if (openNode[openNode.IndexOf(target)].Fscore > target.Fscore)
                        {
                            openNode[openNode.IndexOf(target)] = target;
                        }
                    }
                    else
                    {
                        openNode.Add(new AstarNode(postion, AstarPath.CalcEuclid(postion, user.GetPosition()), search.Gscore + 1, search));
                    }
                }
                    
            }

            if (map.GetMap()[(search.position.GetX() + 1) + map.GetWidth() * search.position.GetY()].IsGo())
            {
                Vec2 postion = new Vec2(search.position.GetX() + 1, search.position.GetY());
                AstarNode target = new AstarNode(postion, AstarPath.CalcEuclid(postion, user.GetPosition()), search.Gscore + 1, search);
                if (!closeNode.Contains(target))
                {
                    if (openNode.Contains(target))
                    {
                        if (openNode[openNode.IndexOf(target)].Fscore > target.Fscore)
                        {
                            openNode[openNode.IndexOf(target)] = target;
                        }
                    }
                    else
                    {
                        openNode.Add(new AstarNode(postion, AstarPath.CalcEuclid(postion, user.GetPosition()), search.Gscore + 1, search));
                    }
                }  
            }
        }
    }
}
