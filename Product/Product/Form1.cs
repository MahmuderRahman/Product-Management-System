using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Product
{
    public partial class productForm : Form
    {
        Models.Product product = new Models.Product();
        private int productId;
        public productForm()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            product.ProductCode = productCodeTextBox.Text;
            product.Description = descriptionTextBox.Text;
            product.Quantity = Convert.ToInt32(quantityTextBox.Text);
            string msg = product.SaveOrProduct(product);
            DisplayProductList();
            MessageBox.Show(msg);
        }

        private void DisplayProductList()
        {
            List<Models.Product> products = product.GetProductList();
            dataGridView1.DataSource = products;

            totalQuantityTextBox.Text = products.Sum(s => s.Quantity).ToString();


        }

        private void productForm_Load(object sender, EventArgs e)
        {
            DisplayProductList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            productId = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            productCodeTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            descriptionTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            quantityTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

        }
    }
}
