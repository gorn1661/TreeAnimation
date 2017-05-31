using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LBProject
{
    public partial class Form2 : Form
    {
        Bitmap mBitmap = null; //create bitmap for panel (where is drawing everything)
        Bitmap tree = null; //bitmap with tree
        Bitmap leaf = null; //and leaves

        Bitmap leaf2 = null;
        int[] mniej = new int[40];
        ArrayList mn = new ArrayList();
        int[] wiecej = new int[40];
        ArrayList wi = new ArrayList();
        int licznik1 = 0;
        int licznik2 = 0;

        int leaves;
        Random r = new Random();
        Point[] rememberXY = new Point[40];
        int[] lifeTime = new int[40];

        //constructor
        public Form2()
        {
            InitializeComponent();
            mBitmap = new Bitmap(panel1.Width, panel1.Height); //initialize bitmap for panel
            tree = new Bitmap("C:\\Users\\mikol\\Documents\\Visual Studio 2017\\Projects\\LBProject\\jasnyzielonyTree.png"); //initialize bitmap (tree) create file path
            leaf = new Bitmap("C:\\Users\\mikol\\Documents\\Visual Studio 2017\\Projects\\LBProject\\jasnyzielony.png"); //initialize bitmap (leaf) create file path

            leaf2 = new Bitmap("C:\\Users\\mikol\\Documents\\Visual Studio 2017\\Projects\\LBProject\\czerwony.png"); //initialize bitmap (leaf) create file path

            leaves = r.Next(10, 40); // count of leaves
            for (int i = 0; i < leaves; i++)
            {
                Point startPoint = new Point(r.Next(30, 350), r.Next(60, 200)); //Random place where leaves will be appear
                rememberXY[i] = new Point(startPoint.X, startPoint.Y); // adding point to table with start points
                lifeTime[i] = r.Next(520, 650); //Random life time thats leaves
                if(lifeTime[i] >= 600)
                {

                    // wiecej[licznik1] = i;
                    wi.Add(i);
                   

                    licznik1++;
                }
                else
                {
                    

                   // mniej[licznik2] = i;

                    mn.Add(i);

                    licznik2++;
                }
            }


            licznik1 = 0;
            licznik2 = 0;


            timer1.Interval = 30; //change timer interval for better vision
            timer1.Start(); //and timer start
        }

        //destructor
        ~Form2()
        {
            mBitmap.Dispose(); //dispose bitmaps for memory
            tree.Dispose();
            leaf.Dispose();
            timer1.Stop(); //and stop timer
        }

        //draw only panel bitmap after that you can draw anything you want on there
        private void draw(Graphics g)
        {
            try
            {
                g.DrawImageUnscaled(mBitmap, 0, 0);
            }
            finally
            {
                g.Dispose();
            }
        }

        

       
        //!!!!!!!!! TIME !!!!!!! ONE YEAR IS 800!!! REMEMBER    
        int time = 0;

        //draw tree
        private void drawTree()
        {
            using(Graphics g = Graphics.FromImage(mBitmap))
            {
                g.Clear(Color.White); //for redraw
                g.DrawImage(tree, 0, 0, 450, 350); //draw tree

                if(time <= 600)
                    foreach(Point r in rememberXY)
                    {
                        g.DrawImage(leaf, r.X, r.Y, 50, 50); //draw leaves
                    }
                else
                {
                    foreach(int i in wi)
                    {
                        g.DrawImage(leaf2, rememberXY[i].X, rememberXY[i].Y, 50, 50);
                    }
                    foreach (int i in mn)
                    {
                        g.DrawImage(leaf, rememberXY[i].X, rememberXY[i].Y, 50, 50);
                    }
                }

                for(int i = 0; i < lifeTime.Length; i++)
                {
                    if (time > lifeTime[i])
                    {
                        rememberXY[i].Y += 7; //if leaves life time ended they are falling
                    }
                }

                if (time == 200) //after that leaves and tree are normal green
                {
                    leaf = new Bitmap("C:\\Users\\mikol\\Documents\\Visual Studio 2017\\Projects\\LBProject\\zielony.png");
                    tree = new Bitmap("C:\\Users\\mikol\\Documents\\Visual Studio 2017\\Projects\\LBProject\\zielonyTree.png");
                }
                if (time == 400) //after that leaves and tree are yellow
                {
                    leaf = new Bitmap("C:\\Users\\mikol\\Documents\\Visual Studio 2017\\Projects\\LBProject\\zolty.png");
                    tree = new Bitmap("C:\\Users\\mikol\\Documents\\Visual Studio 2017\\Projects\\LBProject\\zoltyTree.png");
                }
                if (time == 600) //after that leaves and tree are red
                {
                   // leaf = new Bitmap("C:\\Users\\mikol\\Documents\\Visual Studio 2017\\Projects\\LBProject\\czerwony.png");
                    tree = new Bitmap("C:\\Users\\mikol\\Documents\\Visual Studio 2017\\Projects\\LBProject\\czerwonyTree.png");
                }
                if (time == 700) //after that leaves and tree are red
                {
                    tree = new Bitmap("C:\\Users\\mikol\\Documents\\Visual Studio 2017\\Projects\\LBProject\\TreeNull.png");
                }




                if (time == 800) //the end of year
                {
                    wi.Clear();
                    mn.Clear();
                    leaves = r.Next(10, 40); //new count of leaves
                    for (int i = 0; i < leaves; i++)
                    {
                        Point startPoint = new Point(r.Next(30, 350), r.Next(60, 200)); //Random place where leaves will be appear
                        rememberXY[i] = new Point(startPoint.X, startPoint.Y); // adding point to table with start points
                        lifeTime[i] = r.Next(520, 650);
                        if (lifeTime[i] > 600)
                        {
                           // wiecej[licznik1] = i;

                            wi.Add(i);

                            licznik1++;
                        }
                        else
                        {
                           // mniej[licznik2] = i;

                            mn.Add(i);

                            licznik2++;
                        }
                    }

                    licznik1 = 0;
                    licznik2 = 0;

                    //and again light green for spring
                    leaf = new Bitmap("C:\\Users\\mikol\\Documents\\Visual Studio 2017\\Projects\\LBProject\\jasnyzielony.png");
                    tree = new Bitmap("C:\\Users\\mikol\\Documents\\Visual Studio 2017\\Projects\\LBProject\\jasnyzielonyTree.png");
                    time = 0;
                }
               
                time++;

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            drawTree();
            draw(panel1.CreateGraphics());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            draw(e.Graphics);
        }
    }
}
