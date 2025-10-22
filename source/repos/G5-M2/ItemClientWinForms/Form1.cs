using ItemClientWinForms.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ItemClientWinForms
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _client;

        public Form1()
        {
            InitializeComponent();
            _client = new HttpClient { BaseAddress = new Uri("https://localhost:7275/") };
        }

        // Load all items from API
        private async Task LoadItems()
        {
            try
            {
                var items = await _client.GetFromJsonAsync<List<Item>>("api/items");
                dgvItems.DataSource = items;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading items: {ex.Message}");
            }
        }

        // Refresh button
        private async void btnRefresh_Click(object sender, EventArgs e) => await LoadItems();

        // Add button
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var item = new Item
            {
                Name = txtName.Text,
                Code = txtCode.Text,
                Brand = txtBrand.Text,
                UnitPrice = decimal.TryParse(txtUnitPrice.Text, out var p) ? p : 0
            };

            try
            {
                var resp = await _client.PostAsJsonAsync("api/items", item);
                if (resp.IsSuccessStatusCode)
                {
                    await LoadItems();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show($"Error adding item: {resp.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Connection error: {ex.Message}");
            }
        }

        // Update button
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvItems.CurrentRow == null) return;
            var item = (Item)dgvItems.CurrentRow.DataBoundItem;

            item.Name = txtName.Text;
            item.Code = txtCode.Text;
            item.Brand = txtBrand.Text;
            item.UnitPrice = decimal.TryParse(txtUnitPrice.Text, out var p) ? p : item.UnitPrice;

            var resp = await _client.PutAsJsonAsync($"api/items/{item.Id}", item);
            if (resp.IsSuccessStatusCode)
            {
                await LoadItems();
                ClearFields();
            }
            else
            {
                MessageBox.Show($"Error updating item: {resp.StatusCode}");
            }
        }

        // Delete button
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvItems.CurrentRow == null) return;
            var item = (Item)dgvItems.CurrentRow.DataBoundItem;

            var resp = await _client.DeleteAsync($"api/items/{item.Id}");
            if (resp.IsSuccessStatusCode)
            {
                await LoadItems();
                ClearFields();
            }
            else
            {
                MessageBox.Show($"Error deleting item: {resp.StatusCode}");
            }
        }

        // When selecting a row in DataGridView
        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvItems.CurrentRow == null) return;
            var item = (Item)dgvItems.CurrentRow.DataBoundItem;

            txtName.Text = item.Name;
            txtCode.Text = item.Code;
            txtBrand.Text = item.Brand;
            txtUnitPrice.Text = item.UnitPrice.ToString();
        }

        // Helper: clear textboxes
        private void ClearFields()
        {
            txtName.Clear();
            txtCode.Clear();
            txtBrand.Clear();
            txtUnitPrice.Clear();
        }

        private void button1_Click(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e) { }

        private void Form1_Load(object sender, EventArgs e) { }

        private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void Form1_Load_1(object sender, EventArgs e) { }

        private void label5_Click(object sender, EventArgs e) { }
    }
}
