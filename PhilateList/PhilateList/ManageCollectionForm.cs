using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

public partial class ManageCollectionForm : Form
{
    private Philatelist philatelist;
    private CollectionManager collectionManager;
    private string jsonFilePath;

    public ManageCollectionForm(Philatelist philatelist, CollectionManager collectionManager, string jsonFilePath)
    {
        InitializeComponent();
        this.philatelist = philatelist;
        this.collectionManager = collectionManager;
        this.jsonFilePath = jsonFilePath;
        LoadStamps();
    }

    private void LoadStamps()
    {
        lstStamps.Items.Clear();
        foreach (var stamp in philatelist.RareStamps)
        {
            lstStamps.Items.Add(stamp.ToString());
        }
    }

    private void btnAddStamp_Click(object sender, EventArgs e)
    {
        try
        {
            var country = txtStampCountry.Text.Trim();
            var faceValue = decimal.Parse(txtStampFaceValue.Text.Trim());
            var yearOfIssue = int.Parse(txtStampYearOfIssue.Text.Trim());
            var circulation = int.Parse(txtStampCirculation.Text.Trim());
            var features = txtStampFeatures.Text.Trim();

            // Check for duplicate stamp
            bool isDuplicate = philatelist.RareStamps.Any(s =>
                s.Country.Equals(country, StringComparison.OrdinalIgnoreCase) &&
                s.FaceValue == faceValue &&
                s.YearOfIssue == yearOfIssue &&
                s.Circulation == circulation &&
                s.Features.Equals(features, StringComparison.OrdinalIgnoreCase));

            if (isDuplicate)
            {
                MessageBox.Show("This stamp already exists in the collection.", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var stamp = new Stamp(country, faceValue, yearOfIssue, circulation, features);
            philatelist.AddRareStamp(stamp);
            LoadStamps();
            JSONHandler.SaveToJson(jsonFilePath, collectionManager);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error adding stamp: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnRemoveStamp_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstStamps.SelectedItem != null)
            {
                var selectedStampIndex = lstStamps.SelectedIndex;
                philatelist.RareStamps.RemoveAt(selectedStampIndex);
                LoadStamps();
                JSONHandler.SaveToJson(jsonFilePath, collectionManager);
            }
            else
            {
                MessageBox.Show("Please select a stamp to remove.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error removing stamp: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnUpdateStamp_Click(object sender, EventArgs e)
    {
        if (lstStamps.SelectedItem != null)
        {
            try
            {
                var selectedStampIndex = lstStamps.SelectedIndex;

                var country = txtStampCountry.Text.Trim();
                var faceValue = decimal.Parse(txtStampFaceValue.Text.Trim());
                var yearOfIssue = int.Parse(txtStampYearOfIssue.Text.Trim());
                var circulation = int.Parse(txtStampCirculation.Text.Trim());
                var features = txtStampFeatures.Text.Trim();

                // Check for duplicate with updated information
                bool isDuplicate = false;
                for (int i = 0; i < philatelist.RareStamps.Count; i++)
                {
                    if (i != selectedStampIndex &&
                        philatelist.RareStamps[i].Country.Equals(country, StringComparison.OrdinalIgnoreCase) &&
                        philatelist.RareStamps[i].FaceValue == faceValue &&
                        philatelist.RareStamps[i].YearOfIssue == yearOfIssue &&
                        philatelist.RareStamps[i].Circulation == circulation &&
                        philatelist.RareStamps[i].Features.Equals(features, StringComparison.OrdinalIgnoreCase))
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                if (isDuplicate)
                {
                    MessageBox.Show("Another stamp with the same details already exists.", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var updatedStamp = new Stamp(country, faceValue, yearOfIssue, circulation, features);
                philatelist.RareStamps[selectedStampIndex] = updatedStamp;
                LoadStamps();
                JSONHandler.SaveToJson(jsonFilePath, collectionManager);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating stamp: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        else
        {
            MessageBox.Show("Please select a stamp to update.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void lstStamps_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstStamps.SelectedItem != null)
        {
            var selectedStampIndex = lstStamps.SelectedIndex;
            var selectedStamp = philatelist.RareStamps[selectedStampIndex];

            txtStampCountry.Text = selectedStamp.Country;
            txtStampFaceValue.Text = selectedStamp.FaceValue.ToString();
            txtStampYearOfIssue.Text = selectedStamp.YearOfIssue.ToString();
            txtStampCirculation.Text = selectedStamp.Circulation.ToString();
            txtStampFeatures.Text = selectedStamp.Features;
        }
    }

    #region Designer Code
    private System.ComponentModel.IContainer components = null;
    private ListBox lstStamps;
    private TextBox txtStampCountry;
    private TextBox txtStampFaceValue;
    private TextBox txtStampYearOfIssue;
    private TextBox txtStampCirculation;
    private TextBox txtStampFeatures;
    private Button btnAddStamp;
    private Button btnRemoveStamp;
    private Button btnUpdateStamp;
    private Label lblStampCountry;
    private Label lblStampFaceValue;
    private Label lblStampYearOfIssue;
    private Label lblStampCirculation;
    private Label lblStampFeatures;

    private void InitializeComponent()
    {
        lstStamps = new ListBox();
        txtStampCountry = new TextBox();
        txtStampFaceValue = new TextBox();
        txtStampYearOfIssue = new TextBox();
        txtStampCirculation = new TextBox();
        txtStampFeatures = new TextBox();
        btnAddStamp = new Button();
        btnRemoveStamp = new Button();
        btnUpdateStamp = new Button();
        lblStampCountry = new Label();
        lblStampFaceValue = new Label();
        lblStampYearOfIssue = new Label();
        lblStampCirculation = new Label();
        lblStampFeatures = new Label();
        SuspendLayout();
        
        // 
        // lstStamps
        // 
        lstStamps.FormattingEnabled = true;
        lstStamps.Location = new System.Drawing.Point(12, 12);
        lstStamps.Name = "lstStamps";
        lstStamps.Size = new System.Drawing.Size(388, 384);
        lstStamps.TabIndex = 0;
        lstStamps.SelectedIndexChanged += new EventHandler(lstStamps_SelectedIndexChanged);
        
        // 
        // txtStampCountry
        // 
        txtStampCountry.Location = new System.Drawing.Point(515, 15);
        txtStampCountry.Name = "txtStampCountry";
        txtStampCountry.Size = new System.Drawing.Size(150, 27);
        txtStampCountry.TabIndex = 2;
        
        // 
        // txtStampFaceValue
        // 
        txtStampFaceValue.Location = new System.Drawing.Point(515, 54);
        txtStampFaceValue.Name = "txtStampFaceValue";
        txtStampFaceValue.Size = new System.Drawing.Size(150, 27);
        txtStampFaceValue.TabIndex = 4;
        
        // 
        // txtStampYearOfIssue
        // 
        txtStampYearOfIssue.Location = new System.Drawing.Point(515, 99);
        txtStampYearOfIssue.Name = "txtStampYearOfIssue";
        txtStampYearOfIssue.Size = new System.Drawing.Size(150, 27);
        txtStampYearOfIssue.TabIndex = 6;
        
        // 
        // txtStampCirculation
        // 
        txtStampCirculation.Location = new System.Drawing.Point(515, 156);
        txtStampCirculation.Name = "txtStampCirculation";
        txtStampCirculation.Size = new System.Drawing.Size(150, 27);
        txtStampCirculation.TabIndex = 8;
        
        // 
        // txtStampFeatures
        // 
        txtStampFeatures.Location = new System.Drawing.Point(515, 210);
        txtStampFeatures.Name = "txtStampFeatures";
        txtStampFeatures.Size = new System.Drawing.Size(150, 27);
        txtStampFeatures.TabIndex = 10;
        
        // 
        // btnAddStamp
        // 
        btnAddStamp.Location = new System.Drawing.Point(475, 274);
        btnAddStamp.Name = "btnAddStamp";
        btnAddStamp.Size = new System.Drawing.Size(92, 32);
        btnAddStamp.TabIndex = 11;
        btnAddStamp.Text = "Add Stamp";
        btnAddStamp.UseVisualStyleBackColor = true;
        btnAddStamp.Click += new EventHandler(btnAddStamp_Click);
        
        // 
        // btnRemoveStamp
        // 
        btnRemoveStamp.Location = new System.Drawing.Point(613, 274);
        btnRemoveStamp.Name = "btnRemoveStamp";
        btnRemoveStamp.Size = new System.Drawing.Size(92, 32);
        btnRemoveStamp.TabIndex = 12;
        btnRemoveStamp.Text = "Remove Stamp";
        btnRemoveStamp.UseVisualStyleBackColor = true;
        btnRemoveStamp.Click += new EventHandler(btnRemoveStamp_Click);
        
        // 
        // btnUpdateStamp
        // 
        btnUpdateStamp.Location = new System.Drawing.Point(545, 330);
        btnUpdateStamp.Name = "btnUpdateStamp";
        btnUpdateStamp.Size = new System.Drawing.Size(92, 32);
        btnUpdateStamp.TabIndex = 13;
        btnUpdateStamp.Text = "Update Stamp";
        btnUpdateStamp.UseVisualStyleBackColor = true;
        btnUpdateStamp.Click += new EventHandler(btnUpdateStamp_Click);

        // 
        // lblStampCountry
        // 
        lblStampCountry.AutoSize = true;
        lblStampCountry.Location = new System.Drawing.Point(412, 18);
        lblStampCountry.Name = "lblStampCountry";
        lblStampCountry.Size = new System.Drawing.Size(59, 20);
        lblStampCountry.TabIndex = 1;
        lblStampCountry.Text = "Country:";
        
        // 
        // lblStampFaceValue
        // 
        lblStampFaceValue.AutoSize = true;
        lblStampFaceValue.Location = new System.Drawing.Point(412, 57);
        lblStampFaceValue.Name = "lblStampFaceValue";
        lblStampFaceValue.Size = new System.Drawing.Size(79, 20);
        lblStampFaceValue.TabIndex = 3;
        lblStampFaceValue.Text = "Face Value:";
        
        // 
        // lblStampYearOfIssue
        // 
        lblStampYearOfIssue.AutoSize = true;
        lblStampYearOfIssue.Location = new System.Drawing.Point(412, 102);
        lblStampYearOfIssue.Name = "lblStampYearOfIssue";
        lblStampYearOfIssue.Size = new System.Drawing.Size(92, 20);
        lblStampYearOfIssue.TabIndex = 5;
        lblStampYearOfIssue.Text = "Year of Issue:";
        
        // 
        // lblStampCirculation
        // 
        lblStampCirculation.AutoSize = true;
        lblStampCirculation.Location = new System.Drawing.Point(412, 159);
        lblStampCirculation.Name = "lblStampCirculation";
        lblStampCirculation.Size = new System.Drawing.Size(82, 20);
        lblStampCirculation.TabIndex = 7;
        lblStampCirculation.Text = "Circulation:";
        
        // 
        // lblStampFeatures
        // 
        lblStampFeatures.AutoSize = true;
        lblStampFeatures.Location = new System.Drawing.Point(412, 213);
        lblStampFeatures.Name = "lblStampFeatures";
        lblStampFeatures.Size = new System.Drawing.Size(69, 20);
        lblStampFeatures.TabIndex = 9;
        lblStampFeatures.Text = "Features:";
        
        // 
        // ManageCollectionForm
        // 
        ClientSize = new System.Drawing.Size(746, 428);
        Controls.Add(btnUpdateStamp);
        Controls.Add(btnRemoveStamp);
        Controls.Add(btnAddStamp);
        Controls.Add(lblStampFeatures);
        Controls.Add(txtStampFeatures);
        Controls.Add(lblStampCirculation);
        Controls.Add(txtStampCirculation);
        Controls.Add(lblStampYearOfIssue);
        Controls.Add(txtStampYearOfIssue);
        Controls.Add(lblStampFaceValue);
        Controls.Add(txtStampFaceValue);
        Controls.Add(lblStampCountry);
        Controls.Add(txtStampCountry);
        Controls.Add(lstStamps);
        Name = "ManageCollectionForm";
        Text = "Manage Collection";
        ResumeLayout(false);
        PerformLayout();
    }
    #endregion
}
