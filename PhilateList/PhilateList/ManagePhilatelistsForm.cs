using System;
using System.Linq;
using System.Windows.Forms;
    public partial class ManagePhilatelistsForm : Form
    {
        private CollectionManager collectionManager;
        private string jsonFilePath;
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox lstPhilatelists;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtCountry;
        private System.Windows.Forms.TextBox txtContactDetails;
        private System.Windows.Forms.Button btnAddPhilatelist;
        private System.Windows.Forms.Button btnUpdatePhilatelist;
        private System.Windows.Forms.Button btnRemovePhilatelist;
        private System.Windows.Forms.Button btnManageCollection;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.Label lblContactDetails;

        public ManagePhilatelistsForm(CollectionManager collectionManager, string jsonFilePath)
        {
            InitializeComponent();
            this.collectionManager = collectionManager;
            this.jsonFilePath = jsonFilePath;
            LoadPhilatelists();
        }

    private void InitializeComponent()
    {
        lstPhilatelists = new ListBox();
        txtName = new TextBox();
        txtCountry = new TextBox();
        txtContactDetails = new TextBox();
        btnAddPhilatelist = new Button();
        btnUpdatePhilatelist = new Button();
        btnRemovePhilatelist = new Button();
        btnManageCollection = new Button();
        lblName = new Label();
        lblCountry = new Label();
        lblContactDetails = new Label();
        SuspendLayout();
        // 
        // lstPhilatelists
        // 
        lstPhilatelists.FormattingEnabled = true;
        lstPhilatelists.Location = new Point(12, 26);
        lstPhilatelists.Name = "lstPhilatelists";
        lstPhilatelists.Size = new Size(354, 384);
        lstPhilatelists.TabIndex = 0;
        lstPhilatelists.SelectedIndexChanged += new EventHandler(lstPhilatelists_SelectedIndexChanged); // Attach the event handler
        // 
        // txtName
        // 
        txtName.Location = new Point(525, 70);
        txtName.Name = "txtName";
        txtName.Size = new Size(200, 27);
        txtName.TabIndex = 1;
        // 
        // txtCountry
        // 
        txtCountry.Location = new Point(525, 129);
        txtCountry.Name = "txtCountry";
        txtCountry.Size = new Size(200, 27);
        txtCountry.TabIndex = 2;
        // 
        // txtContactDetails
        // 
        txtContactDetails.Location = new Point(525, 174);
        txtContactDetails.Name = "txtContactDetails";
        txtContactDetails.Size = new Size(200, 27);
        txtContactDetails.TabIndex = 3;
        // 
        // btnAddPhilatelist
        // 
        btnAddPhilatelist.Location = new Point(411, 261);
        btnAddPhilatelist.Name = "btnAddPhilatelist";
        btnAddPhilatelist.Size = new Size(75, 32);
        btnAddPhilatelist.TabIndex = 4;
        btnAddPhilatelist.Text = "Add";
        btnAddPhilatelist.UseVisualStyleBackColor = true;
        btnAddPhilatelist.Click += btnAddPhilatelist_Click;
        // 
        // btnUpdatePhilatelist
        // 
        btnUpdatePhilatelist.Location = new Point(531, 261);
        btnUpdatePhilatelist.Name = "btnUpdatePhilatelist";
        btnUpdatePhilatelist.Size = new Size(75, 32);
        btnUpdatePhilatelist.TabIndex = 5;
        btnUpdatePhilatelist.Text = "Update";
        btnUpdatePhilatelist.UseVisualStyleBackColor = true;
        btnUpdatePhilatelist.Click += btnUpdatePhilatelist_Click;
        // 
        // btnRemovePhilatelist
        // 
        btnRemovePhilatelist.Location = new Point(650, 261);
        btnRemovePhilatelist.Name = "btnRemovePhilatelist";
        btnRemovePhilatelist.Size = new Size(75, 32);
        btnRemovePhilatelist.TabIndex = 6;
        btnRemovePhilatelist.Text = "Remove";
        btnRemovePhilatelist.UseVisualStyleBackColor = true;
        btnRemovePhilatelist.Click += btnRemovePhilatelist_Click;
        // 
        // btnManageCollection
        // 
        btnManageCollection.Location = new Point(411, 316);
        btnManageCollection.Name = "btnManageCollection";
        btnManageCollection.Size = new Size(314, 48);
        btnManageCollection.TabIndex = 7;
        btnManageCollection.Text = "Manage Collection";
        btnManageCollection.UseVisualStyleBackColor = true;
        btnManageCollection.Click += btnManageCollection_Click;
        // 
        // lblName
        // 
        lblName.AutoSize = true;
        lblName.Location = new Point(411, 73);
        lblName.Name = "lblName";
        lblName.Size = new Size(49, 20);
        lblName.TabIndex = 8;
        lblName.Text = "Name";
        // 
        // lblCountry
        // 
        lblCountry.AutoSize = true;
        lblCountry.Location = new Point(411, 132);
        lblCountry.Name = "lblCountry";
        lblCountry.Size = new Size(60, 20);
        lblCountry.TabIndex = 9;
        lblCountry.Text = "Country";
        // 
        // lblContactDetails
        // 
        lblContactDetails.AutoSize = true;
        lblContactDetails.Location = new Point(411, 177);
        lblContactDetails.Name = "lblContactDetails";
        lblContactDetails.Size = new Size(110, 20);
        lblContactDetails.TabIndex = 10;
        lblContactDetails.Text = "Contact Details";
        // 
        // ManagePhilatelistsForm
        // 
        ClientSize = new Size(818, 480);
        Controls.Add(lblContactDetails);
        Controls.Add(lblCountry);
        Controls.Add(lblName);
        Controls.Add(btnManageCollection);
        Controls.Add(btnRemovePhilatelist);
        Controls.Add(btnUpdatePhilatelist);
        Controls.Add(btnAddPhilatelist);
        Controls.Add(txtContactDetails);
        Controls.Add(txtCountry);
        Controls.Add(txtName);
        Controls.Add(lstPhilatelists);
        Name = "ManagePhilatelistsForm";
        ResumeLayout(false);
        PerformLayout();
    }
    // This method will be triggered when a philatelist is selected in the ListBox.
    private void lstPhilatelists_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstPhilatelists.SelectedItem != null)
        {
            var selectedPhilatelistName = lstPhilatelists.SelectedItem.ToString();
            var philatelist = collectionManager.GetPhilatelistByName(selectedPhilatelistName);

            if (philatelist != null)
            {
                // Populate the text fields with the philatelist's details
                txtName.Text = philatelist.Name;
                txtCountry.Text = philatelist.Country;
                txtContactDetails.Text = philatelist.ContactDetails;
            }
        }
    }
    private void LoadPhilatelists()
        {
            lstPhilatelists.Items.Clear();
            foreach (var philatelist in collectionManager.Philatelists)
            {
                lstPhilatelists.Items.Add(philatelist.Name);
            }
        }

    private void btnAddPhilatelist_Click(object sender, EventArgs e)
    {
        var name = txtName.Text.Trim();
        var country = txtCountry.Text.Trim();
        var contactDetails = txtContactDetails.Text.Trim();

        // Ensure that all fields are non-empty
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(contactDetails))
        {
            MessageBox.Show("All fields must be filled in.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Check if the same philatelist already exists
        bool isDuplicate = collectionManager.Philatelists.Any(p =>
            p.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
            p.Country.Equals(country, StringComparison.OrdinalIgnoreCase) &&
            p.ContactDetails.Equals(contactDetails, StringComparison.OrdinalIgnoreCase));

        if (isDuplicate)
        {
            MessageBox.Show("A philatelist with the same details already exists.", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Add new philatelist
        var philatelist = new Philatelist(name, country, contactDetails);
        collectionManager.AddPhilatelist(philatelist);
        LoadPhilatelists();
        JSONHandler.SaveToJson(jsonFilePath, collectionManager);
    }

    private void btnUpdatePhilatelist_Click(object sender, EventArgs e)
    {
        if (lstPhilatelists.SelectedItem != null)
        {
            var oldName = lstPhilatelists.SelectedItem.ToString();
            var newName = txtName.Text.Trim();
            var country = txtCountry.Text.Trim();
            var contactDetails = txtContactDetails.Text.Trim();

            // Ensure that all fields are non-empty
            if (string.IsNullOrWhiteSpace(newName) || string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(contactDetails))
            {
                MessageBox.Show("All fields must be filled in.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Find the selected philatelist
            var selectedPhilatelist = collectionManager.Philatelists.FirstOrDefault(p => p.Name.Equals(oldName, StringComparison.OrdinalIgnoreCase));
            if (selectedPhilatelist != null)
            {
                // Check if any of the fields are different from the current data
                if (!selectedPhilatelist.Name.Equals(newName, StringComparison.OrdinalIgnoreCase) ||
                    !selectedPhilatelist.Country.Equals(country, StringComparison.OrdinalIgnoreCase) ||
                    !selectedPhilatelist.ContactDetails.Equals(contactDetails, StringComparison.OrdinalIgnoreCase))
                {
                    // Check if the updated name conflicts with another philatelist
                    if (!oldName.Equals(newName, StringComparison.OrdinalIgnoreCase) &&
                        collectionManager.Philatelists.Any(p => p.Name.Equals(newName, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show("A philatelist with this name already exists.", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Update the philatelist
                    var updatedPhilatelist = new Philatelist(newName, country, contactDetails);
                    collectionManager.UpdatePhilatelist(oldName, updatedPhilatelist);
                    LoadPhilatelists();
                    JSONHandler.SaveToJson(jsonFilePath, collectionManager);
                }
                else
                {
                    MessageBox.Show("No changes detected.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        else
        {
            MessageBox.Show("Please select a philatelist to update.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


    private void btnRemovePhilatelist_Click(object sender, EventArgs e)
        {
            if (lstPhilatelists.SelectedItem != null)
            {
                var name = lstPhilatelists.SelectedItem.ToString();
                collectionManager.RemovePhilatelist(name);
                LoadPhilatelists();
                JSONHandler.SaveToJson(jsonFilePath, collectionManager);
            }
        }

        private void btnManageCollection_Click(object sender, EventArgs e)
        {
            if (lstPhilatelists.SelectedItem != null)
            {
                var selectedPhilatelistName = lstPhilatelists.SelectedItem.ToString();
                var philatelist = collectionManager.GetPhilatelistByName(selectedPhilatelistName);
                ManageCollectionForm manageCollectionForm = new ManageCollectionForm(philatelist, collectionManager, jsonFilePath);
                manageCollectionForm.ShowDialog();
            }
        }
    }

