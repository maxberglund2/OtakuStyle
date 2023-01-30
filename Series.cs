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
using System.Xml.Linq;
using System.IO;
using System.Xml.Schema;
using static System.Windows.Forms.LinkLabel;

namespace GYA___Max
{
    public partial class Series : Form
    {
        List<ListClass> SeriesList = new List<ListClass>();
        public Series()
        {
            InitializeComponent();
        }
        private void SeriesGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (SeriesGridView.SelectedRows.Count > 0)
            {
                var selectedRow = SeriesGridView.SelectedRows[0];
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
        private void BackToMenuS_Click(object sender, EventArgs e)
        {
            this.Close();
            Form Menu = new Menu();
            Menu.Show();
        }
        private void editButtonSeries_Click(object sender, EventArgs e)
        {
            string value = SeriesGridView.SelectedRows[0].Cells[0].Value.ToString();
            ListClass obj = SeriesList.Find(x => x.Title == value);
            SeriesList.Remove(obj);
            SeriesGridView.Rows.Clear();
            SeriesClass seriesClass = new SeriesClass(SeriesTitleText.Text, StatusBoxSeries.Text, ratingBox.Text, (int)ProgressSeriesUD.Value);
            SeriesList.Add(seriesClass);
            foreach (ListClass ListClass in SeriesList)
            {
                SeriesGridView.Rows.Add(ListClass.GetInfoInToArray());
            }
            ClearEverything();
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OtakuStyle");
            string filePath = Path.Combine(folderPath, "otakustyle_series.txt");
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (SeriesClass SeriesClass in SeriesList)
                {
                    writer.WriteLine(SeriesClass.Title + "," + SeriesClass.Status + "," + SeriesClass.Score + "," + SeriesClass.LenghtEpisodic);
                }
            }
        }
        private void SeriesGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ClearEverything();
        }
        private void Series_Click(object sender, EventArgs e)
        {
            ClearEverything();
        }
        public void PrepEverything()
        {
            RemoveSeriesButton.Enabled = true;
            RemoveSeriesButton.ForeColor = Color.Red;
            EditButtonSeries.Enabled = true;
            AddSereisButton.Enabled = false;
            RemoveSeriesButton.ForeColor = Color.Black;
            RemoveSeriesButton.BackColor = Color.Red;
            SeriesTitleText.Text = SeriesGridView.SelectedRows[0].Cells[0].Value.ToString();
            StatusBoxSeries.Text = SeriesGridView.SelectedRows[0].Cells[1].Value.ToString();
            ratingBox.Text = SeriesGridView.SelectedRows[0].Cells[2].Value.ToString();
            ProgressSeriesUD.Text = SeriesGridView.SelectedRows[0].Cells[3].Value.ToString();
        }
        public void ClearEverything()
        {
            SeriesGridView.ClearSelection();
            EditButtonSeries.Enabled = false;
            AddSereisButton.Enabled = true;
            SeriesTitleText.Text = null;
            ProgressSeriesUD.Value = 0;
            StatusBoxSeries.Text = "Select";
            ratingBox.Text = "None";
            RemoveSeriesButton.Enabled = false;
            RemoveSeriesButton.ForeColor = Color.Lime;
            RemoveSeriesButton.BackColor = Color.Black;
        }
        private void Series_Load(object sender, EventArgs e)
        {
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OtakuStyle");
            string filePath = Path.Combine(folderPath, "otakustyle_series.txt");

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
                        int lenghtEpisodic = int.Parse(parts[3]);
                        SeriesClass seriesClass = new SeriesClass(title, status, rating, lenghtEpisodic);
                        SeriesList.Add(seriesClass);
                    }
                    foreach (ListClass ListClass in SeriesList)
                    {
                        SeriesGridView.Rows.Add(ListClass.GetInfoInToArray());
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(folderPath);
                File.CreateText(filePath);
            }
        }
        private void AddSereisButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(SeriesTitleText.Text) || StatusBoxSeries.Text == "Select")
            {
                MessageBox.Show("Input is missing, choose a 'TITLE' and 'STATUS' to continue!", "Error Message!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SeriesClass sereisClass = new SeriesClass(SeriesTitleText.Text, StatusBoxSeries.Text, ratingBox.Text, (int)ProgressSeriesUD.Value);
                SeriesList.Add(sereisClass);
                SeriesGridView.Rows.Add(sereisClass.GetInfoInToArray());
                ClearEverything();
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OtakuStyle");
                string filePath = Path.Combine(folderPath, "otakustyle_series.txt");
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (SeriesClass SeriesClass in SeriesList)
                    {
                        writer.WriteLine(SeriesClass.Title + "," + SeriesClass.Status + "," + SeriesClass.Score + "," + SeriesClass.LenghtEpisodic);
                    }
                }
            }
        }
        private void AddSereisButton_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            TextRenderer.DrawText(e.Graphics, AddSereisButton.Text, btn.Font, e.ClipRectangle, btn.ForeColor);
        }
        private void EditButtonSeries_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            TextRenderer.DrawText(e.Graphics, EditButtonSeries.Text, btn.Font, e.ClipRectangle, btn.ForeColor);
        }
        private void RemoveSeriesButton_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            TextRenderer.DrawText(e.Graphics, RemoveSeriesButton.Text, btn.Font, e.ClipRectangle, btn.ForeColor);
        }
        private void RemoveSeriesButton_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = SeriesGridView.SelectedRows[0];
            string value = selectedRow.Cells[0].Value.ToString();
            ListClass obj = SeriesList.Find(x => x.Title == value);
            SeriesList.Remove(obj);
            SeriesGridView.Rows.Clear();

            foreach (ListClass ListClass in SeriesList)
            {
                SeriesGridView.Rows.Add(ListClass.GetInfoInToArray());
            }
            ClearEverything();
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OtakuStyle");
            string filePath = Path.Combine(folderPath, "otakustyle_series.txt");
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (SeriesClass SeriesClass in SeriesList)
                {
                    writer.WriteLine(SeriesClass.Title + "," + SeriesClass.Status + "," + SeriesClass.Score + "," + SeriesClass.LenghtEpisodic);
                }
            }
        }

    }
}
