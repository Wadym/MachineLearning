namespace MLSuite
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TestSuiteTabControl = new System.Windows.Forms.TabControl();
            this.tabTestSuite = new System.Windows.Forms.TabPage();
            this.tabWork = new System.Windows.Forms.TabPage();
            this.ButtonRunClusterizationModule = new System.Windows.Forms.Button();
            this.TestSuiteTabControl.SuspendLayout();
            this.tabTestSuite.SuspendLayout();
            this.SuspendLayout();
            // 
            // TestSuiteTabControl
            // 
            this.TestSuiteTabControl.Controls.Add(this.tabTestSuite);
            this.TestSuiteTabControl.Controls.Add(this.tabWork);
            this.TestSuiteTabControl.Location = new System.Drawing.Point(1, -3);
            this.TestSuiteTabControl.Name = "TestSuiteTabControl";
            this.TestSuiteTabControl.SelectedIndex = 0;
            this.TestSuiteTabControl.Size = new System.Drawing.Size(255, 301);
            this.TestSuiteTabControl.TabIndex = 0;
            // 
            // tabTestSuite
            // 
            this.tabTestSuite.Controls.Add(this.ButtonRunClusterizationModule);
            this.tabTestSuite.Location = new System.Drawing.Point(4, 22);
            this.tabTestSuite.Name = "tabTestSuite";
            this.tabTestSuite.Padding = new System.Windows.Forms.Padding(3);
            this.tabTestSuite.Size = new System.Drawing.Size(247, 275);
            this.tabTestSuite.TabIndex = 0;
            this.tabTestSuite.Text = "TestSuite";
            this.tabTestSuite.UseVisualStyleBackColor = true;
            // 
            // tabWork
            // 
            this.tabWork.Location = new System.Drawing.Point(4, 22);
            this.tabWork.Name = "tabWork";
            this.tabWork.Padding = new System.Windows.Forms.Padding(3);
            this.tabWork.Size = new System.Drawing.Size(247, 275);
            this.tabWork.TabIndex = 1;
            this.tabWork.Text = "Work";
            this.tabWork.UseVisualStyleBackColor = true;
            // 
            // ButtonRunClusterizationModule
            // 
            this.ButtonRunClusterizationModule.Location = new System.Drawing.Point(8, 7);
            this.ButtonRunClusterizationModule.Name = "ButtonRunClusterizationModule";
            this.ButtonRunClusterizationModule.Size = new System.Drawing.Size(149, 23);
            this.ButtonRunClusterizationModule.TabIndex = 0;
            this.ButtonRunClusterizationModule.Text = "Run Clusterization Module";
            this.ButtonRunClusterizationModule.UseVisualStyleBackColor = true;
            this.ButtonRunClusterizationModule.Click += new System.EventHandler(this.ButtonRunClusterizationModule_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 296);
            this.Controls.Add(this.TestSuiteTabControl);
            this.Name = "Main";
            this.Text = "Main";
            this.TestSuiteTabControl.ResumeLayout(false);
            this.tabTestSuite.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TestSuiteTabControl;
        private System.Windows.Forms.TabPage tabTestSuite;
        private System.Windows.Forms.TabPage tabWork;
        private System.Windows.Forms.Button ButtonRunClusterizationModule;
    }
}

