using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GYA___Max
{
    public partial class Movies : Form
    {
        List<ListClass> MoviesList = new List<ListClass>();
        public Movies()
        {
            InitializeComponent();
        }

        private void AddMovieButton_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(MovieTitleTextBox.Text) || StatusBoxMovies.Text == "Select")
            {
                MessageBox.Show("Input is missing, choose a 'TITLE' and 'STATUS' to continue!", "Error Message!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            else
            {
                MoviesClass moviesClass = new MoviesClass(MovieTitleTextBox.Text, StatusBoxMovies.Text, ratingBoxMovies.Text, (int)HoursMovies.Value, (int)MinMovies.Value);
                MoviesList.Add(moviesClass);
                MoviesGridView.Rows.Add(moviesClass.GetInfoInToArray());
                ClearEverything();

                string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OtakuStyle");
                string filePath = Path.Combine(folderPath, "otakustyle_movies.txt");
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (MoviesClass MoviesClass in MoviesList)
                    {
                        writer.WriteLine(MoviesClass.Title + "," + MoviesClass.Status + "," + MoviesClass.Score + "," + MoviesClass.Hours + "," + MoviesClass.Minutes);
                    }
                }
            }
        }

        private void EditButtonMovie_Click(object sender, EventArgs e)
        {
            string value = MoviesGridView.SelectedRows[0].Cells[0].Value.ToString();
            ListClass obj = MoviesList.Find(x => x.Title == value);
            MoviesList.Remove(obj);
            MoviesGridView.Rows.Clear();
            MoviesClass moviesClass = new MoviesClass(MovieTitleTextBox.Text, StatusBoxMovies.Text, ratingBoxMovies.Text, (int)HoursMovies.Value, (int)MinMovies.Value);
            MoviesList.Add(moviesClass);
            foreach (ListClass ListClass in MoviesList)
            {
                MoviesGridView.Rows.Add(ListClass.GetInfoInToArray());
            }
            ClearEverything();
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OtakuStyle");
            string filePath = Path.Combine(folderPath, "otakustyle_movies.txt");
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (MoviesClass MoviesClass in MoviesList)
                {
                    writer.WriteLine(MoviesClass.Title + "," + MoviesClass.Status + "," + MoviesClass.Score + "," + MoviesClass.Hours + "," + MoviesClass.Minutes);
                }
            }
        }

        private void RemoveMovieButton_Click(object sender, EventArgs e)
        {

            DataGridViewRow selectedRow = MoviesGridView.SelectedRows[0];
            string value = selectedRow.Cells[0].Value.ToString();
            ListClass obj = MoviesList.Find(x => x.Title == value);
            MoviesList.Remove(obj);
            MoviesGridView.Rows.Clear();

            foreach (ListClass ListClass in MoviesList)
            {
                MoviesGridView.Rows.Add(ListClass.GetInfoInToArray());
            }
            ClearEverything();

            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OtakuStyle");
            string filePath = Path.Combine(folderPath, "otakustyle_movies.txt");
            File.WriteAllText(filePath, string.Empty);
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (MoviesClass MoviesClass in MoviesList)
                {
                    writer.WriteLine(MoviesClass.Title + "," + MoviesClass.Status + "," + MoviesClass.Score + "," + MoviesClass.Hours + "," + MoviesClass.Minutes);
                }
            }
        }

        private void BackToMenu_Click(object sender, EventArgs e)
        {
            this.Close();
            Form Menu = new Menu();
            Menu.Show();
        }

        private void SeriesGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (MoviesGridView.SelectedRows.Count > 0)
            {
                var selectedRow = MoviesGridView.SelectedRows[0];
                if (selectedRow.Cells.Count > 0)
                {
                    var selectedCell = selectedRow.Cells[0];
                    if (selectedCell.Value != null)
                    {
                        PrepEverything();
                    }
                }
            }
            else
            {
                ClearEverything();
            }
        }

        public void PrepEverything()
        {
            RemoveMovieButton.Enabled = true;
            EditButtonMovie.Enabled = true;
            AddMovieButton.Enabled = false;
            RemoveMovieButton.ForeColor = Color.Black;
            RemoveMovieButton.BackColor = Color.Red;
            MovieTitleTextBox.Text = MoviesGridView.SelectedRows[0].Cells[0].Value.ToString();
            StatusBoxMovies.Text = MoviesGridView.SelectedRows[0].Cells[1].Value.ToString();
            ratingBoxMovies.Text = MoviesGridView.SelectedRows[0].Cells[2].Value.ToString();
            HoursMovies.Text = MoviesGridView.SelectedRows[0].Cells[3].Value.ToString();
            MinMovies.Text = MoviesGridView.SelectedRows[0].Cells[4].Value.ToString();
        }
        public void ClearEverything()
        {
            MoviesGridView.ClearSelection();
            EditButtonMovie.Enabled = false;
            AddMovieButton.Enabled = true;
            MovieTitleTextBox.Text = null;
            HoursMovies.Value = 0;
            MinMovies.Value = 0;
            StatusBoxMovies.Text = "Select";
            ratingBoxMovies.Text = "None";
            RemoveMovieButton.Enabled = false;
            RemoveMovieButton.ForeColor = Color.Lime;
            RemoveMovieButton.BackColor = Color.Black;
        }

        private void MoviesGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ClearEverything();
        }

        private void Movies_Load(object sender, EventArgs e)
        {

            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OtakuStyle");
            string filePath = Path.Combine(folderPath, "otakustyle_movies.txt");

            if (File.Exists(filePath))
            {
                using (StreamReader file = new StreamReader(filePath))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        string title = parts[0];
                        string status = parts[1];
                        string rating = parts[2];
                        int hours = int.Parse(parts[3]);
                        int mins = int.Parse(parts[4]);
                        MoviesClass moviesClass = new MoviesClass(title, status, rating, hours, mins);
                        MoviesList.Add(moviesClass);
                    }
                    foreach (ListClass ListClass in MoviesList)
                    {
                        MoviesGridView.Rows.Add(ListClass.GetInfoInToArray());
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(folderPath);
                File.CreateText(filePath);
            }
        }

        private void Movies_Click(object sender, EventArgs e)
        {
            ClearEverything();
        }

        private void AddMovieButton_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            TextRenderer.DrawText(e.Graphics, AddMovieButton.Text, btn.Font, e.ClipRectangle, btn.ForeColor);
        }

        private void EditButtonMovie_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            TextRenderer.DrawText(e.Graphics, EditButtonMovie.Text, btn.Font, e.ClipRectangle, btn.ForeColor);
        }

        private void RemoveMovieButton_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            TextRenderer.DrawText(e.Graphics, RemoveMovieButton.Text, btn.Font, e.ClipRectangle, btn.ForeColor);
        }
    }
}
