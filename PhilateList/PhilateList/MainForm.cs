using System;
using System.Windows.Forms;

    public partial class MainForm : Form
    {
        private CollectionManager collectionManager;
        private string jsonFilePath = "philatelists.json";

        public MainForm()
        {
            InitializeComponent();
            collectionManager = JSONHandler.LoadFromJson(jsonFilePath) ?? new CollectionManager();
        }
    private void InitializeComponent()
    {
        btnManagePhilatelists = new Button();
        btnExit = new Button();
        SuspendLayout();
        // 
        // btnManagePhilatelists
        // 
        btnManagePhilatelists.BackColor = Color.FromArgb(128, 255, 128);
        btnManagePhilatelists.Location = new Point(213, 53);
        btnManagePhilatelists.Name = "btnManagePhilatelists";
        btnManagePhilatelists.Size = new Size(200, 65);
        btnManagePhilatelists.TabIndex = 0;
        btnManagePhilatelists.Text = "Manage Philatelists";
        btnManagePhilatelists.UseVisualStyleBackColor = false;
        btnManagePhilatelists.Click += btnManagePhilatelists_Click;
        // 
        // btnExit
        // 
        btnExit.BackColor = Color.Red;
        btnExit.Location = new Point(213, 167);
        btnExit.Name = "btnExit";
        btnExit.Size = new Size(200, 65);
        btnExit.TabIndex = 1;
        btnExit.Text = "Exit";
        btnExit.UseVisualStyleBackColor = false;
        btnExit.Click += btnExit_Click;
        // 
        // MainForm
        // 
        ClientSize = new Size(612, 329);
        Controls.Add(btnExit);
        Controls.Add(btnManagePhilatelists);
        Name = "MainForm";
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button btnManagePhilatelists;
        private System.Windows.Forms.Button btnExit;

    private void btnManagePhilatelists_Click(object sender, EventArgs e)
        {
            ManagePhilatelistsForm managePhilatelistsForm = new ManagePhilatelistsForm(collectionManager, jsonFilePath);
            managePhilatelistsForm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
