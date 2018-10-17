using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabiryntCode
{
    public partial class FormCode : Form
    {
        public int cols, rows;
        int CellNumber;
        MazeLogic ml;
        //int move;                

        public FormCode()
        {
            InitializeComponent();

            CellNumber = 40;
            this.Size = new Size(800, 800);

            this.DoubleBuffered = true;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.Blue;
           
            cols = this.Width / CellNumber;
            rows = this.Height / CellNumber;
            ml = new MazeLogic(cols, rows);
        }
       
        private void FormCode_Paint(object sender, PaintEventArgs e)
        {            
            Graphics g = e.Graphics;
            Pen pen = new Pen((Color.Black));                     
           
            ml.checkNeighbours(ml.currentCell);
                        
            foreach (Cell c in ml.MazeGrid)
            {
                var x = c.posX * CellNumber;
                var y = c.posY * CellNumber;
                
                if (c.visited == true)
                {
                    Rectangle rec = new Rectangle((int)x, (int)y, (int)CellNumber, (int)CellNumber);
                    g.FillRectangle(Brushes.Green, rec);
                }
                if (c.koniec == true)
                {
                    Rectangle rec = new Rectangle((int)x, (int)y, (int)CellNumber, (int)CellNumber);
                    g.FillRectangle(Brushes.Silver, rec);
                }
                if (c.Start == true)
                {
                    Rectangle rec = new Rectangle((int)x, (int)y, (int)CellNumber, (int)CellNumber);
                    g.FillRectangle(Brushes.Gold, rec);
                }
                if (c.current == true)
                {
                    Rectangle recCur = new Rectangle((int)x, (int)y, (int)CellNumber, (int)CellNumber);
                    g.FillRectangle(Brushes.Red, recCur);
                }

                if (c.topWall == true)
                {
                    g.DrawLine(pen, x, y, x + CellNumber, y);
                }
                if (c.rightWall == true)
                {
                    g.DrawLine(pen, x + CellNumber, y, x + CellNumber, y + CellNumber);
                }
                if (c.rightWall == true)
                {
                    g.DrawLine(pen, x + CellNumber, y + CellNumber, x, y + CellNumber);
                }
                if (c.rightWall == true)
                {
                    g.DrawLine(pen, x, y + CellNumber, x, y);
                }
            }
            this.Invalidate();
            
            System.Threading.Thread.Sleep(1000 / 120);

        }
    }
}