
namespace CallApi
{
    partial class Form1
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
            this.btnCallApi = new System.Windows.Forms.Button();
            this.btnPostApi = new System.Windows.Forms.Button();
            this.btnRepo = new System.Windows.Forms.Button();
            this.btnOutRepo = new System.Windows.Forms.Button();
            this.btnUsingRest = new System.Windows.Forms.Button();
            this.btnPost = new System.Windows.Forms.Button();
            this.btnPostOutRecord = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCallApi
            // 
            this.btnCallApi.Location = new System.Drawing.Point(84, 81);
            this.btnCallApi.Name = "btnCallApi";
            this.btnCallApi.Size = new System.Drawing.Size(218, 73);
            this.btnCallApi.TabIndex = 0;
            this.btnCallApi.Text = "Get API";
            this.btnCallApi.UseVisualStyleBackColor = true;
            this.btnCallApi.Click += new System.EventHandler(this.btnCallApi_Click);
            // 
            // btnPostApi
            // 
            this.btnPostApi.Location = new System.Drawing.Point(349, 191);
            this.btnPostApi.Name = "btnPostApi";
            this.btnPostApi.Size = new System.Drawing.Size(220, 64);
            this.btnPostApi.TabIndex = 1;
            this.btnPostApi.Text = "Post API";
            this.btnPostApi.UseVisualStyleBackColor = true;
            this.btnPostApi.Click += new System.EventHandler(this.btnPostApi_Click);
            // 
            // btnRepo
            // 
            this.btnRepo.Location = new System.Drawing.Point(349, 85);
            this.btnRepo.Name = "btnRepo";
            this.btnRepo.Size = new System.Drawing.Size(220, 64);
            this.btnRepo.TabIndex = 2;
            this.btnRepo.Text = "Card-Repo";
            this.btnRepo.UseVisualStyleBackColor = true;
            this.btnRepo.Click += new System.EventHandler(this.btnRepo_Click);
            // 
            // btnOutRepo
            // 
            this.btnOutRepo.Location = new System.Drawing.Point(85, 191);
            this.btnOutRepo.Name = "btnOutRepo";
            this.btnOutRepo.Size = new System.Drawing.Size(234, 64);
            this.btnOutRepo.TabIndex = 3;
            this.btnOutRepo.Text = "Out-Repo";
            this.btnOutRepo.UseVisualStyleBackColor = true;
            this.btnOutRepo.Click += new System.EventHandler(this.btnOutRepo_Click);
            // 
            // btnUsingRest
            // 
            this.btnUsingRest.Location = new System.Drawing.Point(681, 90);
            this.btnUsingRest.Name = "btnUsingRest";
            this.btnUsingRest.Size = new System.Drawing.Size(238, 58);
            this.btnUsingRest.TabIndex = 4;
            this.btnUsingRest.Text = "Get Using Rest";
            this.btnUsingRest.UseVisualStyleBackColor = true;
            this.btnUsingRest.Click += new System.EventHandler(this.btnUsingRest_Click);
            // 
            // btnPost
            // 
            this.btnPost.Location = new System.Drawing.Point(681, 191);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(238, 64);
            this.btnPost.TabIndex = 5;
            this.btnPost.Text = "Post";
            this.btnPost.UseVisualStyleBackColor = true;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // btnPostOutRecord
            // 
            this.btnPostOutRecord.Location = new System.Drawing.Point(976, 201);
            this.btnPostOutRecord.Name = "btnPostOutRecord";
            this.btnPostOutRecord.Size = new System.Drawing.Size(265, 54);
            this.btnPostOutRecord.TabIndex = 6;
            this.btnPostOutRecord.Text = "Post Out Record";
            this.btnPostOutRecord.UseVisualStyleBackColor = true;
            this.btnPostOutRecord.Click += new System.EventHandler(this.btnPostOutRecord_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1866, 628);
            this.Controls.Add(this.btnPostOutRecord);
            this.Controls.Add(this.btnPost);
            this.Controls.Add(this.btnUsingRest);
            this.Controls.Add(this.btnOutRepo);
            this.Controls.Add(this.btnRepo);
            this.Controls.Add(this.btnPostApi);
            this.Controls.Add(this.btnCallApi);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCallApi;
        private System.Windows.Forms.Button btnPostApi;
        private System.Windows.Forms.Button btnRepo;
        private System.Windows.Forms.Button btnOutRepo;
        private System.Windows.Forms.Button btnUsingRest;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.Button btnPostOutRecord;
    }
}

