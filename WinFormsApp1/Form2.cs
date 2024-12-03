using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace WinFormsApp
{
    public partial class Form2 : Form
    {
        private class Genre
        {
            ArrayList genre = new ArrayList();

            public Genre()
            {




                ArrayList BookGenre = new ArrayList();

                BookGenre.Add("fantasy");
                BookGenre.Add("Literary Fiction:");
                BookGenre.Add("Historical Fiction:");
                BookGenre.Add("Science Fiction");
                BookGenre.Add("Mystery");
                BookGenre.Add("Thriller");
                BookGenre.Add("Horror");
                BookGenre.Add("Adventure");
                BookGenre.Add("Biography");
                BookGenre.Add("Autobiography");
                BookGenre.Add("Self-Help");
                BookGenre.Add("Travel");
                BookGenre.Add("History");
                BookGenre.Add("Philosophy");
                BookGenre.Add("Science");
                BookGenre.Add("Historical Romance");
                BookGenre.Add("Science Fiction Fantasy");
                BookGenre.Add("Dystopian Fiction");
                BookGenre.Add("Magical Realism");
                BookGenre.Add("Young Adult");
                BookGenre.Add("Middle Grade");
                BookGenre.Add("Graphic Novels");
                BookGenre.Add("Poetry");
                BookGenre.Add("Crime/Thriller");


                ArrayList MangaGenre = new ArrayList();
                MangaGenre.Add("fantasy");
                MangaGenre.Add("Literary Fiction:");
                MangaGenre.Add("Historical Fiction:");
                MangaGenre.Add("Science Fiction");
                MangaGenre.Add("Mystery");
                MangaGenre.Add("Thriller");
                MangaGenre.Add("Horror");
                MangaGenre.Add("Adventure");
                MangaGenre.Add("Science");
                MangaGenre.Add("Crime/Thriller");
                MangaGenre.Add("History");
                MangaGenre.Add("Historical Fiction:");
                MangaGenre.Add("Young Adult");


                ArrayList ComicGenre = new ArrayList();
                ComicGenre.Add("fantasy");
                ComicGenre.Add("Literary Fiction:");
                ComicGenre.Add("Historical Fiction:");
                ComicGenre.Add("Science Fiction");
                ComicGenre.Add("Mystery");
                ComicGenre.Add("Thriller");
                ComicGenre.Add("Horror");
                ComicGenre.Add("Adventure");
                ComicGenre.Add("Science");
                ComicGenre.Add("Crime/Thriller");
                ComicGenre.Add("superhero");
                ComicGenre.Add("villainStory");
            }
        }





        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (BookGenre.Checked)
            {
                // Uncheck other checkboxes if checkBox1 is checked
                mangaGenre.Checked = false;
                ComicGenre.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (mangaGenre.Checked)
            {
                // Uncheck other checkboxes if checkBox2 is checked
                ComicGenre.Checked = false;
                BookGenre.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (ComicGenre.Checked)
            {
                // Uncheck other checkboxes if checkBox3 is checked
                mangaGenre.Checked = false;
                BookGenre.Checked = false;
            }
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
             if (BookGenre.Checked)
            {
                // If checkbox is checked, show array1 in DomainUpDown
                domainUpDown1.Items.Clear();
                domainUpDown1.Items.AddRange((ICollection)BookGenre);
            }
          
             if (mangaGenre.Checked)
                {
                    // If checkbox is checked, show array1 in DomainUpDown
                    domainUpDown1.Items.Clear();
                    domainUpDown1.Items.AddRange((ICollection)mangaGenre);
                }
            if (ComicGenre.Checked)
            {
                domainUpDown1.Items.Clear();
                domainUpDown1.Items.AddRange((ICollection)ComicGenre);
            }

            

        }
    }
  }

