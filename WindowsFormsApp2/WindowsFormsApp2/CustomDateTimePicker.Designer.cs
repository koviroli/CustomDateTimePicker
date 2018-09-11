namespace WindowsFormsApp2
{
    partial class CustomDateTimePicker
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbDate = new System.Windows.Forms.TextBox();
            this.btnCalendar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbDate
            // 
            this.tbDate.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbDate.Location = new System.Drawing.Point(3, 3);
            this.tbDate.MaxLength = 10;
            this.tbDate.Name = "tbDate";
            this.tbDate.Size = new System.Drawing.Size(105, 20);
            this.tbDate.TabIndex = 0;
            this.tbDate.Text = "DD/MM/YYYY";
            this.tbDate.Click += new System.EventHandler(this.tbDate_Click);
            this.tbDate.TextChanged += new System.EventHandler(this.tbDate_TextChanged);
            this.tbDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDate_KeyPress);
            this.tbDate.Leave += new System.EventHandler(this.tbDate_Leave);
            this.tbDate.MouseHover += new System.EventHandler(this.tbDate_MouseHover);
            // 
            // btnCalendar
            // 
            this.btnCalendar.BackColor = System.Drawing.SystemColors.Window;
            this.btnCalendar.BackgroundImage = global::WindowsFormsApp2.Properties.Resources.calendar_16x16;
            this.btnCalendar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCalendar.FlatAppearance.BorderSize = 0;
            this.btnCalendar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalendar.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCalendar.Location = new System.Drawing.Point(80, 4);
            this.btnCalendar.Name = "btnCalendar";
            this.btnCalendar.Size = new System.Drawing.Size(27, 18);
            this.btnCalendar.TabIndex = 1;
            this.btnCalendar.Text = "▼";
            this.btnCalendar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCalendar.UseVisualStyleBackColor = false;
            this.btnCalendar.Click += new System.EventHandler(this.btnCalendar_Click);
            // 
            // CustomDateTimePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCalendar);
            this.Controls.Add(this.tbDate);
            this.Name = "CustomDateTimePicker";
            this.Size = new System.Drawing.Size(113, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbDate;
        private System.Windows.Forms.Button btnCalendar;
    }
}
