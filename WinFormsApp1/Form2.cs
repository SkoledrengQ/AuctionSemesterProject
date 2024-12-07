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
        private readonly List<string> BookGenre;
        private readonly List<string> MangaGenre;
        private readonly List<string> ComicGenre;
       


        public Form2()
        {
            InitializeComponent();

            // Initialize genres
            BookGenre = new List<string>
            {
                "Fantasy", "Literary Fiction", "Historical Fiction", "Science Fiction",
                "Mystery", "Thriller", "Horror", "Adventure", "Biography", "Autobiography",
                "Self-Help", "Travel", "History", "Philosophy", "Science",
                "Historical Romance", "Science Fiction Fantasy", "Dystopian Fiction",
                "Magical Realism", "Young Adult", "Middle Grade", "Graphic Novels",
                "Poetry", "Crime/Thriller"
            };

            MangaGenre = new List<string>
            {
                "Fantasy", "Literary Fiction", "Historical Fiction", "Science Fiction",
                "Mystery", "Thriller", "Horror", "Adventure", "Science",
                "Crime/Thriller", "History", "Young Adult"
            };

            ComicGenre = new List<string>
            {
                "Fantasy", "Literary Fiction", "Historical Fiction", "Science Fiction",
                "Mystery", "Thriller", "Horror", "Adventure", "Science",
                "Crime/Thriller", "Superhero", "Villain Story"
            };
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

        private void checkBoxBook_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBook.Checked)
            {
                // Uncheck other checkboxes if checkBox1 is checked
                checkBoxManga.Checked = false;
                checkBoxComic.Checked = false;
                domainUpDown1_SelectedItemChanged(sender, e);
            }
        }

        private void checkBoxManga_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxManga.Checked)
            {
                // Uncheck other checkboxes if checkBox2 is checked
                checkBoxComic.Checked = false;
                checkBoxBook.Checked = false;
                domainUpDown1_SelectedItemChanged(sender, e);
            }
        }

        private void checkBoxComic_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxComic.Checked)
            {
                // Uncheck other checkboxes if checkBox3 is checked
                checkBoxManga.Checked = false;
                checkBoxBook.Checked = false;
                domainUpDown1_SelectedItemChanged(sender, e);
            }
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            domainUpDown1.Items.Clear();

            if (checkBoxBook.Checked)
            {
                // If checkbox is checked, show array1 in DomainUpDown
                domainUpDown1.Items.AddRange(BookGenre.ToArray());
            }
          
            else if (checkBoxManga.Checked)
            {
                // If checkbox is checked, show array1 in DomainUpDown
                domainUpDown1.Items.AddRange(MangaGenre.ToArray());
            }
            else if (checkBoxComic.Checked)
            {
                domainUpDown1.Items.AddRange(ComicGenre.ToArray());
            }

            

        }
    }
  }

