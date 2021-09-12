using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace projectTestImages
{

    public partial class Form1 : Form
    {
        
        List<string> ComplListBox = new List<string>();

        List<int> multiselcetedimages = new List<int>();

        private int Imagenumber=0;

        bool multimoodenow = false;


        public Form1()
        {
            InitializeComponent();
      
            pictureBox2.Visible = false;
            listBox1.Visible = false;
            panel.Visible = false;

            panel.Width = this.Width*3/4;
            listBox1.Width = this.Width / 4;
            listBox1.Height = panel.Height;

        }
       
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Please Select Images";
            open.Multiselect = true;
            open.Filter = "JPG|*.JPG|JPEG|*.JPeg|GIF|*.gif|PNG|*.png";
            DialogResult dr = open.ShowDialog();


            timer1.Stop();
            panel.Visible = false;
            pictureBox2.Visible = false;
            toolStripStatusLabel1.Visible = false;
            

            if (dr == DialogResult.OK)
            {
                
                string[] files = open.FileNames;
                listBox1.Visible = true;
              
                foreach (string img in files)
                {

                    ComplListBox.Add(img.ToString());

                    String imageNT = Path.GetFileName(img.ToString());

                    // remove type of image 
                    listBox1.Items.Add(imageNT.Substring(0, imageNT.LastIndexOf(".")));

                }
            }      
                }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                
        }

        private void singlePictureToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //  Stop other modes
            timer1.Stop();
            toolStripStatusLabel1.Visible = false;
            pictureBox2.Controls.Clear();
            pictureBox2.Visible = false;
            panel.Controls.Clear();
            


            if (listBox1.SelectedItems.Count == 0 && listBox1.Items.Count != 0)
            {
                MessageBox.Show("Please  Select  one image", " Single Picture Mode ");
            }
            else if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("List Box is Empty Please Select Images", " Single Picture Mode ");
            }
          
            else if (listBox1.SelectedItems.Count == 1)
            {
                multimoodenow = false;

               
                pictureBox2.Visible = true;

                panel.Visible = true;
             

                int number = listBox1.SelectedIndex;
                Image image = Image.FromFile(ComplListBox[number]);

                this.pictureBox2.Image = image;

               
                panel.Controls.Add(this.pictureBox2);              

            }
            // if select more than one image
            else
            {
                pictureBox2.Controls.Clear();
                panel.Controls.Clear();
                MessageBox.Show("You Select "+ listBox1.SelectedItems.Count+" images...Please Select Just one photo", " Single Picture Mode ");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

           Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            

        }

        private void multiPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  Stop other modes

            toolStripStatusLabel1.Visible = false;
            pictureBox2.Visible = false;
            timer1.Stop();
            panel.Visible = true;
    
            if (listBox1.SelectedItems.Count == 0 && listBox1.Items.Count != 0)
            {
                panel.Controls.Clear();
                MessageBox.Show("You Dont Select any Image Please Select More Than one photo", " Multi Picture Mode ");
            }
            else if(listBox1.Items.Count == 0)
            {
                panel.Controls.Clear();
                MessageBox.Show("List Box is Empty Please Select Images", " Multi Picture Mode ");
            }
            else if (listBox1.SelectedItems.Count == 1 && listBox1.Items.Count != 1)
            {
                panel.Controls.Clear();
                MessageBox.Show("Please Select More Than one photo", " Multi Picture Mode ");

            }
            else if (listBox1.SelectedItems.Count == 1&& listBox1.Items.Count==1)
            {
                panel.Controls.Clear();
                MessageBox.Show("List Box has one Images You can Use Single Option insteade Or Add More Images", " Multi Picture Mode ");

            }

            else
            {
                
                multiMode();
                
            }
        }

        private void multiMode()
        {
            panel.Controls.Clear();
            pictureBox2.Controls.Clear();
            multiselcetedimages.Clear();

            multimoodenow = true;


            int x = 5;
            int y = 15;
            int maxHeight = -1;

            foreach (int index in listBox1.SelectedIndices)
            {
                multiselcetedimages.Add(index);
            }

            if (multiselcetedimages.Count > 60)
            {
                MessageBox.Show("Number of Image Should be 60 Or less ", " Multi Picture Mode ");
            }
            else
            {
                foreach (int index in multiselcetedimages)
                {

                    // control size of images less than 10 images
                    int count = 2;
                    PictureBox pic = new PictureBox();

                    Size size;

                    if (listBox1.SelectedItems.Count % 2 == 0 && listBox1.SelectedItems.Count != 2)
                    {
                        count = listBox1.SelectedItems.Count / 2;
                    }
                    if (listBox1.SelectedItems.Count % 2 != 0)
                    {
                        count = (listBox1.SelectedItems.Count / 2) + 1;
                    }
                    if (listBox1.SelectedItems.Count <= 10)
                    {
                        size = new Size((this.panel.Width / count) - 10, (this.panel.Height / count) - 10);
                        pic.Size = size;
                    }
                    if (listBox1.SelectedItems.Count > 10)
                    {
                        size = new Size(100, 100);
                        pic.Size = size;
                    }

                    // get element from (ComplListBox) 
                    pic.Image = Image.FromFile(ComplListBox[index].ToString());
                    pic.Location = new Point(x, y);
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    x += pic.Width + 10;
                    maxHeight = Math.Max(pic.Height, maxHeight);
                    if (x > this.panel.Width - pic.Width)
                    {

                        x = 5;
                        y += maxHeight + 15;

                    }
                    pic.BorderStyle = BorderStyle.Fixed3D;
                    this.panel.Controls.Add(pic);

                }
            }

        }

        // muti images if user change size of form 

        private  void MutiNewSize()
        {
            panel.Controls.Clear();
            pictureBox2.Controls.Clear();
           
            multimoodenow = true;


            int x = 5;
            int y = 15;
            int maxHeight = -1;
            foreach (int index in multiselcetedimages)
            {
                int count = 2;
                PictureBox pic = new PictureBox();

                Size size;

                if (listBox1.SelectedItems.Count % 2 == 0 && listBox1.SelectedItems.Count != 2)
                {
                    count = listBox1.SelectedItems.Count / 2;
                }
                if (listBox1.SelectedItems.Count % 2 != 0)
                {
                    count = (listBox1.SelectedItems.Count / 2) + 1;
                }
                if (listBox1.SelectedItems.Count <= 10)
                {
                    size = new Size((this.panel.Width / count) - 10, (this.panel.Height / count) - 10);
                    pic.Size = size;
                }
                if (listBox1.SelectedItems.Count > 10)
                {
                    size = new Size(100, 100);
                    pic.Size = size;
                }

                // get element from (ComplListBox) 
                pic.Image = Image.FromFile(ComplListBox[index].ToString());
                pic.Location = new Point(x, y);
                pic.SizeMode = PictureBoxSizeMode.StretchImage;
                x += pic.Width + 10;
                maxHeight = Math.Max(pic.Height, maxHeight);
                if (x > this.panel.Width -pic.Width)
                {

                    x = 5;
                    y += maxHeight + 15;

                }
                pic.BorderStyle = BorderStyle.Fixed3D;
                this.panel.Controls.Add(pic);

            }
        }

        private void slider()
        {
            
          
            // show images again
            if(Imagenumber== ComplListBox.Count)
            {
                Imagenumber = 0;
            }
    
                pictureBox2.Image = Image.FromFile(ComplListBox[Imagenumber].ToString());
                panel.Controls.Add(this.pictureBox2);

                String imageNT = Path.GetFileName(ComplListBox[Imagenumber].ToString());
                
                toolStripStatusLabel1.Text = imageNT.Substring(0, imageNT.LastIndexOf("."));
                toolStripStatusLabel1.Visible = true;
                
                Imagenumber++;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        
            slider();

        }

        private void slideShowToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if(listBox1.Items.Count==0)
            {
                MessageBox.Show("List Box is Empty Please Select Images", " Slide Show Mode ");
                
            }

            else if (listBox1.Items.Count == 1)
            {
                MessageBox.Show("List Box has one Images You can Use Single Option insteade Or Add More Images", " Slide Show Mode ");

            }
            else
            {
                multimoodenow = false;

                listBox1.Visible = false;

                pictureBox2.Controls.Clear();
                panel.Controls.Clear();
                pictureBox2.Visible = true;
                panel.Visible = true;
                Imagenumber = 0;
                timer1.Start();
            }

        }

        private void panelImages_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {

            panel.Width = this.Width * 3 / 4;
            listBox1.Width = this.Width / 4;

            listBox1.Height = panel.Height;

            if (multimoodenow) {

                
                MutiNewSize(); 

           }

        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            listBox1.Visible = true;
            timer1.Stop();
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    }

