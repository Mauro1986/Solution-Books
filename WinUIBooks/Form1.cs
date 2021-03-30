using LibraryBooks.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinUIBooks
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {//formload dient voor zaken die moeten geladen worden bij het opstarten van app(dubbel klik op form)
            LoadBooks();
            PopulateCountries();
        }
        private void PopulateCountries()
        {
            BookRepo repo = new BookRepo();
            CmbxCountry.DisplayMember = "Name";
            CmbxCountry.ValueMember = "Id";
            CmbxCountry.DataSource = repo.GetCountries();
            LoadBooks();
        }
        private void LoadBooks()
        {
            BookRepo repo = new BookRepo();
            grdBooks.DataSource = null;
            grdBooks.DataSource = repo.GetBooks();
        }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            Book book = new Book();
            book.Title = txtTitle.Text;
            book.Author = txtAuthor.Text;
            book.Price = decimal.Parse(txtPrice.Text);
            book.Description = txtDescription.Text;
            book.CountryId = (int)CmbxCountry.SelectedValue;

            BookRepo repo = new BookRepo();
            repo.AddBook(book);
        }
        private void btnInsertandGetId_Click(object sender, EventArgs e)
        {
            Book book = new Book();
            book.Title = txtTitle.Text;
            book.Author = txtAuthor.Text;
            book.Price = decimal.Parse(txtPrice.Text);
            book.Description = txtDescription.Text;
            book.CountryId = 2;

            BookRepo repo = new BookRepo();
            int id = repo.AddBookReturnId(book);
            MessageBox.Show($"The new Id in the table is: {id} ");
            LoadBooks();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Book book = new Book();
            book.Id = int.Parse(lblId.Text);
            book.Title = txtTitle.Text;
            book.Author = txtAuthor.Text;
            book.Price = decimal.Parse(txtPrice.Text);
            book.Description = txtDescription.Text;
            book.CountryId = (int)CmbxCountry.SelectedValue;

            BookRepo repo = new BookRepo();
            repo.UpdateBook(book);
            LoadBooks();
        }
        private void grdBooks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblId.Text = grdBooks.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTitle.Text = grdBooks.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtAuthor.Text = grdBooks.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPrice.Text = grdBooks.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtDescription.Text = grdBooks.Rows[e.RowIndex].Cells[4].Value.ToString();
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Book book = new Book();
            book.Id = int.Parse(lblId.Text);
            book.Title = txtTitle.Text;
            book.Author = txtAuthor.Text;
            book.Price = decimal.Parse(txtPrice.Text);
            book.Description = txtDescription.Text;
            book.CountryId = 2;

            BookRepo repo = new BookRepo();
            repo.DeleteBook(book.Id);
            LoadBooks();
        }
        private void CmbxCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            BookRepo repo = new BookRepo();
            string value = CmbxCountry.SelectedValue.ToString();
            var list = repo.GetAuthorsOfSelectedCountry((Convert.ToInt32(value)));
            lstAuthor.DisplayMember = "Author";
            lstAuthor.DataSource = list;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (rbGreen.Checked)
            {
                this.BackColor = Color.Green;
            }
            if (rbRed.Checked)
            {
                this.BackColor = Color.Red;
            }
        }
    }
}
