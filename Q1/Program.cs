using System;
using System.Collections.Generic;

namespace Q1
{
    public class Program
    {
        protected static string _sOutput;
        private static List<Area> lstAreas;
        public static List<Area> getTable()
        {
            lstAreas = new List<Area>();
            lstAreas.Add(new Area(1, "Continent",-1));
            lstAreas.Add(new Area(2, "Country"  ,1));
            lstAreas.Add(new Area(3, "Province" ,2));
            lstAreas.Add(new Area(4, "City1"    ,3));
            lstAreas.Add(new Area(5, "Suburb1"  ,6));
            lstAreas.Add(new Area(6, "City2"    ,3));
            lstAreas.Add(new Area(7, "Suburb2"  ,6));
            lstAreas.Add(new Area(8, "Suburb3"  ,6));
            lstAreas.Add(new Area(9, "Suburb4"  ,4));
            lstAreas.Add(new Area(10,"House1"   ,7));
            lstAreas.Add(new Area(11,"House3"   ,9));
            lstAreas.Add(new Area(12,"House4"   ,8));
            lstAreas.Add(new Area(13,"House5"   ,8));
            lstAreas.Add(new Area(14,"House6"   ,7));
            return lstAreas;
        }

        private static AreaTree setupTree()
        {
            List<AreaTree> lstTrees = new List<AreaTree>();
            foreach (Area area in getTable())
            {
                lstTrees.Add(new AreaTree(area));
            }

            foreach (AreaTree tree in lstTrees)
            {
                List<AreaTree> lstTempTrees = new List<AreaTree>();
                lstTempTrees = lstTrees.FindAll(t => t._areaData.ParentID == tree.ID);
                foreach (AreaTree tree1 in lstTempTrees) //Child parent assignment.
                {
                    tree1._areaData.sPrefix = tree._areaData.sPrefix + "-";
                    tree.AddChild(tree1);
                }
            }

            return lstTrees.Find(n => n._areaData.ParentID == -1);
        }

        public static void Main()
        {
            AreaTree headOfTree = setupTree();
            List<AreaTree> lstTrees = headOfTree.DepthFirst();

            foreach (AreaTree tree in lstTrees)
            {
                Console.WriteLine(tree.ToDisplay());
            }
        }
    }

    public class Area
    {
        private int _ID;
        private string _Description;
        private int _ParentID;
        public string sPrefix;

        public Area() 
        {
            
        }

        public Area(int _id, string _desc, int _parentid)
        {
            sPrefix = "-";
            _ID = _id;
            _Description = _desc;
            _ParentID = _parentid;
        }

        
        public int ID { get { return _ID; } }
        public string Description { get { return _Description;} }
        public int ParentID { get { return _ParentID;} }
    }

    public class AreaTree
    {
        public Area _areaData;
        public int ID { get { return _areaData.ID; } }

        private List<AreaTree> lstChildren { get; set; }
        public AreaTree(Area areaData) 
        {
            lstChildren = new List<AreaTree>();
            _areaData = areaData;
        }

        public void AddChild(AreaTree tree)
        {
            lstChildren.Add(tree); 
        }

        public string ToDisplay()
        {
            return _areaData.sPrefix + _areaData.Description;
        }

        public List<AreaTree> DepthFirst()
        {
            List<AreaTree> lstAreaTrees = new List<AreaTree>();
            this.DepthFirstRecursive(this, lstAreaTrees);
            return lstAreaTrees;
        }

        private void DepthFirstRecursive(AreaTree tree, List<AreaTree> lstAreaTrees)
        { 
            lstAreaTrees.Add(tree);
            foreach (AreaTree tree1 in tree.lstChildren)
                DepthFirstRecursive(tree1, lstAreaTrees);
        }



    }
}
