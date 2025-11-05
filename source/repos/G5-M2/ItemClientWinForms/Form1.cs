using System.IO;
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

        // ✅ List to hold original data for searching
        private List<Item> _itemsList = new List<Item>();

        public Form1()
        {
            InitializeComponent();
            _client = new HttpClient { BaseAddress = new Uri("https://localhost:7298/") };
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadItems();
        }

        // ✅ Load data into list AND grid
        private async Task LoadItems()
        {
            try
            {
                _itemsList = await _client.GetFromJsonAsync<List<Item>>("api/items");
                dgvItems.DataSource = null;
                dgvItems.DataSource = _itemsList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading items: {ex.Message}");
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e) => await LoadItems();

        // ✅ Add
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
                else MessageBox.Show($"Error adding item: {resp.StatusCode}");
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Connection error: {ex.Message}");
            }
        }

        // ✅ Update
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
            else MessageBox.Show($"Error updating item: {resp.StatusCode}");
        }

        // ✅ Delete
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
            else MessageBox.Show($"Error deleting item: {resp.StatusCode}");
        }

        // ✅ When selecting row
        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvItems.CurrentRow == null) return;
            var item = (Item)dgvItems.CurrentRow.DataBoundItem;

            txtName.Text = item.Name;
            txtCode.Text = item.Code;
            txtBrand.Text = item.Brand;
            txtUnitPrice.Text = item.UnitPrice.ToString();
        }

        private void ClearFields()
        {
            txtName.Clear();
            txtCode.Clear();
            txtBrand.Clear();
            txtUnitPrice.Clear();
        }

        // ✅ FIXED SEARCH — case-insensitive & no crash
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string q = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(q))
            {
                dgvItems.DataSource = null;
                dgvItems.DataSource = _itemsList; // restore original data
                return;
            }

            var filtered = _itemsList.FindAll(item =>
                (item.Name != null && item.Name.ToLower().Contains(q)) ||
                (item.Code != null && item.Code.ToLower().Contains(q)) ||
                (item.Brand != null && item.Brand.ToLower().Contains(q)) ||
                item.UnitPrice.ToString().ToLower().Contains(q)
            );

            dgvItems.DataSource = null;
            dgvItems.DataSource = filtered;
        }

        // 📤 Export to CSV
        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV file (*.csv)|*.csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {
                    for (int i = 0; i < dgvItems.Columns.Count; i++)
                        sw.Write(dgvItems.Columns[i].HeaderText + (i < dgvItems.Columns.Count - 1 ? "," : ""));

                    sw.WriteLine();

                    foreach (DataGridViewRow row in dgvItems.Rows)
                    {
                        if (row.IsNewRow) continue;

                        for (int i = 0; i < dgvItems.Columns.Count; i++)
                            sw.Write(row.Cells[i].Value?.ToString() + (i < dgvItems.Columns.Count - 1 ? "," : ""));

                        sw.WriteLine();
                    }
                }
            }
        }
    }
}
