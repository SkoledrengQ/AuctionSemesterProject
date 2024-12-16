using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Net.Mime.MediaTypeNames;
using Timer = System.Windows.Forms.Timer;

namespace WinFormsApp
{
    public partial class Form2 : Form
    {
        // Genre lists
        private readonly List<string> MangaGenre;

        private readonly List<string> BookGenre;

        private readonly List<string> ComicGenre;

        public EventHandler Tick { get; private set; }
        public object listBox3 { get; private set; }

        public Form2()
        {
            InitializeComponent();
            InitializeEventHandlers();


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

        private void InitializeEventHandlers()
        {
            checkBoxBook.CheckedChanged += checkBoxBook_CheckedChanged;
            checkBoxManga.CheckedChanged += checkBoxManga_CheckedChanged;
            checkBoxComic.CheckedChanged += checkBoxComic_CheckedChanged;
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
            // Get the value from the DateTimePicker
            DateTime selectedDateTime = dateTimePicker1.Value;

            // Get only the time part of the selected DateTime
            TimeSpan time = selectedDateTime.TimeOfDay;

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

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            listBox1.Visible = true;
            Debug.WriteLine("UpdateDomainUpDownItems called");
            // Clear and repopulate DomainUpDown based on selected checkbox
            domainUpDown1.Items.Clear();
            Debug.WriteLine("DomainUpDown items cleared");

            if (checkBoxBook.Checked)
            {
                foreach (string item in BookGenre)
                {

                    domainUpDown1.Items.Add(item);

                }
            }

            if (checkBoxManga.Checked)
            {
                foreach (string item in MangaGenre)
                {
                    domainUpDown1.Items.Add(item);
                }
            }

            if (checkBoxComic.Checked)
            {
                foreach (string item in ComicGenre)
                {
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

            if (checkBoxBook.Checked)
            {

                checkBoxManga.Checked = false;
                checkBoxComic.Checked = false;
            }


        }

        private void checkBoxManga_CheckedChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("checkBoxManga_CheckedChanged triggered");
            Debug.WriteLine($"checkBoxManga.Checked: {checkBoxManga.Checked}");
            domainUpDown1_SelectedItemChanged(sender, e);

            if (checkBoxManga.Checked)
            {

                checkBoxBook.Checked = false;
                checkBoxComic.Checked = false;
            }

        }

        private void checkBoxComic_CheckedChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("checkBoxComic_CheckedChanged triggered");
            Debug.WriteLine($"checkBoxComic.Checked: {checkBoxComic.Checked}");
            domainUpDown1_SelectedItemChanged(sender, e);

            if (checkBoxComic.Checked)
            {

                checkBoxManga.Checked = false;
                checkBoxBook.Checked = false;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox richTextBox1 = new TextBox();
            string Summery = richTextBox1.Text;
            textBox4.Text = "Enter text here";
            string currentText = richTextBox1.Text;
            listBox1.Visible = true;

            string filteredText = new string(currentText.Where(c => Char.IsLetter(c) || c == ' ').ToArray());


            if (currentText != filteredText)
            {
                richTextBox1.Text = filteredText;

                richTextBox1.SelectionStart = richTextBox1.Text.Length;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            listBox1.Visible = true;
            TextBox textBox3 = new TextBox();
            string Title = textBox3.Text;
            textBox3.Text = "Enter text here";
            string currentText = textBox3.Text;


            string filteredText = new string(currentText.Where(c => Char.IsLetter(c) || c == ' ').ToArray());


            if (currentText != filteredText)
            {
                textBox3.Text = filteredText;

                textBox3.SelectionStart = textBox3.Text.Length;
            }

        }





        private void listBox1_SelectedIndexChanged(object sender, EventArgs e, string text, string textTitle, string textSizeOfBook, string DomainUpDown, string textrichTextBox1)
        {





        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            listBox1.GetItemText("Item " + (listBox1.SelectedItems));
            listBox1.DataSource = null;
            listBox1.DataSource = listBox1.Items;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox4 = new TextBox();
            string ReleaseDate = textBox4.Text;

            string currentText = textBox4.Text;


            string filteredText = new string(currentText.Where(c => Char.IsLetter(c) || c == ' ').ToArray());
            listBox1.Visible = true;

            if (currentText != filteredText)
            {
                textBox4.Text = filteredText;

                textBox4.SelectionStart = textBox4.Text.Length;
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox2 = new TextBox();
            string textSizeOfBook = textBox2.Text;

            string currentText = textBox2.Text;
            string filteredText = new string(currentText.Where(c => Char.IsLetter(c) || c == ' ').ToArray());
            listBox1.Visible = true;

            if (currentText != filteredText)
            {
                textBox2.Text = filteredText;

                textBox2.SelectionStart = textBox2.Text.Length;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {



            if (!string.IsNullOrWhiteSpace(Text))
            {

                listBox1.Items.Add("genre: " + domainUpDown1.Text);
                listBox1.Items.Add("Title: " + textBox3.Text);
                listBox1.Items.Add("size: " + textBox2.Text + " pages");
                listBox1.Items.Add("ReleaseDate:  " + textBox4.Text);
                listBox1.Items.Add("summery: " + richTextBox1.Text);

            }
            else
            {
                MessageBox.Show("Please enter some text before adding to the list.");
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                string imagePath = (@"C:Users\Pictures\auction\image.jpg");
                
                if (System.IO.File.Exists(imagePath))
                {
                    // Load the image from the file path
                    pictureBox1.Image = System.Drawing.Image.FromFile(imagePath);

                    // Optional: Set the size mode to zoom to fit the image in the PictureBox
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    MessageBox.Show("Image file does not exist.");
                }
            }
            catch (Exception ex)
            {
                // Display any errors that occur during image loading
                MessageBox.Show("Error loading image: " + ex.Message);
            }
        }
    }
}




