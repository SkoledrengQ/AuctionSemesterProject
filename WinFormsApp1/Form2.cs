using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace WinFormsApp
{
    public partial class Form2 : Form
    {

        // Genre lists
        private readonly List<string> MangaGenre;

        private readonly List<string> BookGenre;

        private readonly List<string> ComicGenre; 

        public Form2()
        {
            InitializeComponent();
            
            BookGenre = new List<string>
        {
            "Fantasy", "Literary Fiction", "Historical Fiction", "Science Fiction",
            "Mystery", "Thriller", "Horror", "Adventure", "Biography", "Autobiography",
            "Self-Help", "Travel", "History", "Philosophy", "Science",
            "Historical Romance", "Science Fiction Fantasy", "Dystopian Fiction",
            "Magical Realism", "Young Adult", "Middle Grade", "Graphic Novels",
            "Poetry", "Crime/Thriller"
        };
            Debug.WriteLine("BookGenre initialized: " + string.Join(", ", BookGenre));

            MangaGenre = new List<string>
        {
            "Fantasy", "Literary Fiction", "Historical Fiction", "Science Fiction",
            "Mystery", "Thriller", "Horror", "Adventure", "Science",
            "Crime/Thriller", "History", "Young Adult"
        };
            Debug.WriteLine("MangaGenre initialized: " + string.Join(", ", MangaGenre));

            ComicGenre = new List<string>
        {
            "Fantasy", "Literary Fiction", "Historical Fiction", "Science Fiction",
            "Mystery", "Thriller", "Horror", "Adventure", "Science",
            "Crime/Thriller", "Superhero", "Villain Story"
        };
            Debug.WriteLine("ComicGenre initialized: " + string.Join(", ", ComicGenre));

            //Populate DomainUpDown with default data(ChatGpt)
            domainUpDown1_SelectedItemChanged(null, null);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Ensure initialization on form load
            domainUpDown1_SelectedItemChanged(null, null);
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

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

            // Prevent adding items if already added
            if (domainUpDown1.Items.Count > 0)
            {
                return;
            }

            Debug.WriteLine("UpdateDomainUpDownItems called");
            // Clear and repopulate DomainUpDown based on selected checkbox
            domainUpDown1.Items.Clear();
            Debug.WriteLine("DomainUpDown items cleared");

            if (checkBoxBook.Checked)
            {
                foreach (string item in BookGenre)
                {
                    Debug.WriteLine($"Adding item to DomainUpDown: {item}");
                    domainUpDown1.Items.Add(item);
                }
            }
            if (checkBoxManga.Checked)
            {
                Debug.WriteLine("Manga checkbox is checked, adding MangaGenre items");
                foreach (string item in MangaGenre)
                {
                    Debug.WriteLine($"Adding item to DomainUpDown: {item}");
                    domainUpDown1.Items.Add(item);
                }
            }
            if (checkBoxComic.Checked)
            {
                Debug.WriteLine("Comic checkbox is checked, adding ComicGenre items");
                foreach (string item in ComicGenre)
                {
                    Debug.WriteLine($"Adding item to DomainUpDown: {item}");
                    domainUpDown1.Items.Add(item);
                }
            }

            // Select the first item, if available
            Debug.WriteLine($"Total items in DomainUpDown: {domainUpDown1.Items.Count}");
            if (domainUpDown1.Items.Count > 0)
            {
                domainUpDown1.SelectedItem = domainUpDown1.Items[0];
            }

            else
            {
                domainUpDown1.Text = string.Empty; // Clear text if no items exist
            }
        }

        private void checkBoxBook_CheckedChanged(object sender, EventArgs e)
        {
            // Trigger item update when the checkbox state changes
            Debug.WriteLine("checkBoxBook_CheckedChanged triggered");
            Debug.WriteLine($"checkBoxBook.Checked: {checkBoxBook.Checked}");
            domainUpDown1_SelectedItemChanged(sender, e);
        }

        private void checkBoxManga_CheckedChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("checkBoxManga_CheckedChanged triggered");
            Debug.WriteLine($"checkBoxManga.Checked: {checkBoxManga.Checked}");
            domainUpDown1_SelectedItemChanged(sender, e);
        }

        private void checkBoxComic_CheckedChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("checkBoxComic_CheckedChanged triggered");
            Debug.WriteLine($"checkBoxComic.Checked: {checkBoxComic.Checked}");
            domainUpDown1_SelectedItemChanged(sender, e);
        }
    }

        }
      
    

