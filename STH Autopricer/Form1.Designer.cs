namespace STH_Autopricer
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.BGAutoprice = new System.ComponentModel.BackgroundWorker();
            this.ParsePricesButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.OrderQuantityNUD = new System.Windows.Forms.NumericUpDown();
            this.StatusDot = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.MinProfitNUD = new System.Windows.Forms.NumericUpDown();
            this.MaxProfitNUD = new System.Windows.Forms.NumericUpDown();
            this.QuantityCheckBox = new System.Windows.Forms.CheckBox();
            this.ProxyCheck = new System.Windows.Forms.CheckBox();
            this.CheckOnlySellPrice = new System.Windows.Forms.CheckBox();
            this.MaxOrderPriceCheckBox = new System.Windows.Forms.CheckBox();
            this.OrderMaxPriceNUD = new System.Windows.Forms.NumericUpDown();
            this.ChangeOnlyUp = new System.Windows.Forms.CheckBox();
            this.ProxyCount = new System.Windows.Forms.NumericUpDown();
            this.LinearRegression = new System.Windows.Forms.CheckBox();
            this.RegressionValue = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.AutologinCheckBox = new System.Windows.Forms.CheckBox();
            this.EmailTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.OrderQuantityNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinProfitNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxProfitNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderMaxPriceNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProxyCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RegressionValue)).BeginInit();
            this.SuspendLayout();
            // 
            // BGAutoprice
            // 
            this.BGAutoprice.WorkerSupportsCancellation = true;
            this.BGAutoprice.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BGAutoprice_DoWork);
            // 
            // ParsePricesButton
            // 
            this.ParsePricesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ParsePricesButton.Location = new System.Drawing.Point(12, 12);
            this.ParsePricesButton.Name = "ParsePricesButton";
            this.ParsePricesButton.Size = new System.Drawing.Size(360, 63);
            this.ParsePricesButton.TabIndex = 0;
            this.ParsePricesButton.Text = "Обновить список";
            this.ParsePricesButton.UseVisualStyleBackColor = true;
            this.ParsePricesButton.Click += new System.EventHandler(this.ParsePricesButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 13);
            this.label1.TabIndex = 1;
            this.label1.Tag = "";
            this.label1.Text = "Чистая прибыль от продаж (от-до,%):  - - - - - - - - - - ";
            // 
            // OrderQuantityNUD
            // 
            this.OrderQuantityNUD.Location = new System.Drawing.Point(325, 169);
            this.OrderQuantityNUD.Name = "OrderQuantityNUD";
            this.OrderQuantityNUD.Size = new System.Drawing.Size(42, 20);
            this.OrderQuantityNUD.TabIndex = 3;
            this.OrderQuantityNUD.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.OrderQuantityNUD.ValueChanged += new System.EventHandler(this.OrderQuantityNUD_ValueChanged);
            // 
            // StatusDot
            // 
            this.StatusDot.AutoSize = true;
            this.StatusDot.BackColor = System.Drawing.Color.Transparent;
            this.StatusDot.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StatusDot.ForeColor = System.Drawing.Color.Red;
            this.StatusDot.Location = new System.Drawing.Point(54, 77);
            this.StatusDot.Name = "StatusDot";
            this.StatusDot.Size = new System.Drawing.Size(23, 22);
            this.StatusDot.TabIndex = 5;
            this.StatusDot.Text = "●";
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(15, 81);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(44, 13);
            this.StatusLabel.TabIndex = 6;
            this.StatusLabel.Text = "Статус:";
            // 
            // MinProfitNUD
            // 
            this.MinProfitNUD.Location = new System.Drawing.Point(277, 117);
            this.MinProfitNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MinProfitNUD.Name = "MinProfitNUD";
            this.MinProfitNUD.Size = new System.Drawing.Size(42, 20);
            this.MinProfitNUD.TabIndex = 1;
            this.MinProfitNUD.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.MinProfitNUD.ValueChanged += new System.EventHandler(this.MinProfitNUD_ValueChanged);
            // 
            // MaxProfitNUD
            // 
            this.MaxProfitNUD.Location = new System.Drawing.Point(325, 117);
            this.MaxProfitNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxProfitNUD.Name = "MaxProfitNUD";
            this.MaxProfitNUD.Size = new System.Drawing.Size(42, 20);
            this.MaxProfitNUD.TabIndex = 2;
            this.MaxProfitNUD.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.MaxProfitNUD.ValueChanged += new System.EventHandler(this.MaxProfitNUD_ValueChanged);
            // 
            // QuantityCheckBox
            // 
            this.QuantityCheckBox.AutoSize = true;
            this.QuantityCheckBox.Checked = true;
            this.QuantityCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.QuantityCheckBox.Location = new System.Drawing.Point(22, 170);
            this.QuantityCheckBox.Name = "QuantityCheckBox";
            this.QuantityCheckBox.Size = new System.Drawing.Size(301, 17);
            this.QuantityCheckBox.TabIndex = 11;
            this.QuantityCheckBox.Text = "Количество в заказе от суточной продажи (%):  - - - - - -";
            this.QuantityCheckBox.UseVisualStyleBackColor = true;
            this.QuantityCheckBox.CheckedChanged += new System.EventHandler(this.QuantityCheckBox_CheckedChanged);
            // 
            // ProxyCheck
            // 
            this.ProxyCheck.AutoSize = true;
            this.ProxyCheck.Checked = true;
            this.ProxyCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ProxyCheck.Location = new System.Drawing.Point(22, 268);
            this.ProxyCheck.Name = "ProxyCheck";
            this.ProxyCheck.Size = new System.Drawing.Size(301, 17);
            this.ProxyCheck.TabIndex = 14;
            this.ProxyCheck.Text = "Использовать прокси (шт.):  - - - - - - - - - - - - - - - - - - - - - -";
            this.ProxyCheck.UseVisualStyleBackColor = true;
            this.ProxyCheck.CheckedChanged += new System.EventHandler(this.ProxyCheck_CheckedChanged);
            // 
            // CheckOnlySellPrice
            // 
            this.CheckOnlySellPrice.AutoSize = true;
            this.CheckOnlySellPrice.Location = new System.Drawing.Point(21, 222);
            this.CheckOnlySellPrice.Name = "CheckOnlySellPrice";
            this.CheckOnlySellPrice.Size = new System.Drawing.Size(220, 17);
            this.CheckOnlySellPrice.TabIndex = 15;
            this.CheckOnlySellPrice.Text = "Корректировать только цену продажи";
            this.CheckOnlySellPrice.UseVisualStyleBackColor = true;
            this.CheckOnlySellPrice.CheckedChanged += new System.EventHandler(this.CheckOnlySellPrice_CheckedChanged);
            // 
            // MaxOrderPriceCheckBox
            // 
            this.MaxOrderPriceCheckBox.AutoSize = true;
            this.MaxOrderPriceCheckBox.Checked = true;
            this.MaxOrderPriceCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MaxOrderPriceCheckBox.Location = new System.Drawing.Point(37, 196);
            this.MaxOrderPriceCheckBox.Name = "MaxOrderPriceCheckBox";
            this.MaxOrderPriceCheckBox.Size = new System.Drawing.Size(285, 17);
            this.MaxOrderPriceCheckBox.TabIndex = 16;
            this.MaxOrderPriceCheckBox.Text = "Максимальная цена для одного ордера ($):  - - - - - -";
            this.MaxOrderPriceCheckBox.UseVisualStyleBackColor = true;
            this.MaxOrderPriceCheckBox.CheckedChanged += new System.EventHandler(this.MaxOrderPriceCheckBox_CheckedChanged);
            // 
            // OrderMaxPriceNUD
            // 
            this.OrderMaxPriceNUD.Location = new System.Drawing.Point(325, 195);
            this.OrderMaxPriceNUD.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.OrderMaxPriceNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.OrderMaxPriceNUD.Name = "OrderMaxPriceNUD";
            this.OrderMaxPriceNUD.Size = new System.Drawing.Size(42, 20);
            this.OrderMaxPriceNUD.TabIndex = 17;
            this.OrderMaxPriceNUD.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.OrderMaxPriceNUD.ValueChanged += new System.EventHandler(this.OrderMaxPriceNUD_ValueChanged);
            // 
            // ChangeOnlyUp
            // 
            this.ChangeOnlyUp.AutoSize = true;
            this.ChangeOnlyUp.Enabled = false;
            this.ChangeOnlyUp.Location = new System.Drawing.Point(37, 245);
            this.ChangeOnlyUp.Name = "ChangeOnlyUp";
            this.ChangeOnlyUp.Size = new System.Drawing.Size(185, 17);
            this.ChangeOnlyUp.TabIndex = 19;
            this.ChangeOnlyUp.Text = "Корректировать только в плюс";
            this.ChangeOnlyUp.UseVisualStyleBackColor = true;
            this.ChangeOnlyUp.CheckedChanged += new System.EventHandler(this.ChangeOnlyUp_CheckedChanged);
            // 
            // ProxyCount
            // 
            this.ProxyCount.Location = new System.Drawing.Point(325, 267);
            this.ProxyCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ProxyCount.Name = "ProxyCount";
            this.ProxyCount.Size = new System.Drawing.Size(42, 20);
            this.ProxyCount.TabIndex = 21;
            this.ProxyCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ProxyCount.ValueChanged += new System.EventHandler(this.ProxyCount_ValueChanged);
            // 
            // LinearRegression
            // 
            this.LinearRegression.AutoSize = true;
            this.LinearRegression.Checked = true;
            this.LinearRegression.CheckState = System.Windows.Forms.CheckState.Checked;
            this.LinearRegression.Location = new System.Drawing.Point(37, 144);
            this.LinearRegression.Name = "LinearRegression";
            this.LinearRegression.Size = new System.Drawing.Size(286, 17);
            this.LinearRegression.TabIndex = 23;
            this.LinearRegression.Text = "Изменять относительно цены (constanta): - - - - - - - -";
            this.LinearRegression.UseVisualStyleBackColor = true;
            this.LinearRegression.CheckedChanged += new System.EventHandler(this.LinearRegression_CheckedChanged);
            // 
            // RegressionValue
            // 
            this.RegressionValue.DecimalPlaces = 2;
            this.RegressionValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.RegressionValue.Location = new System.Drawing.Point(325, 143);
            this.RegressionValue.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            131072});
            this.RegressionValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.RegressionValue.Name = "RegressionValue";
            this.RegressionValue.Size = new System.Drawing.Size(42, 20);
            this.RegressionValue.TabIndex = 27;
            this.RegressionValue.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.RegressionValue.ValueChanged += new System.EventHandler(this.RegressionValue_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(22, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 16);
            this.label6.TabIndex = 29;
            this.label6.Text = "L";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 16);
            this.label3.TabIndex = 34;
            this.label3.Text = "L";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(22, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 16);
            this.label4.TabIndex = 35;
            this.label4.Text = "L";
            // 
            // AutologinCheckBox
            // 
            this.AutologinCheckBox.AutoSize = true;
            this.AutologinCheckBox.Location = new System.Drawing.Point(22, 295);
            this.AutologinCheckBox.Name = "AutologinCheckBox";
            this.AutologinCheckBox.Size = new System.Drawing.Size(82, 17);
            this.AutologinCheckBox.TabIndex = 36;
            this.AutologinCheckBox.Text = "Автологин:";
            this.AutologinCheckBox.UseVisualStyleBackColor = true;
            this.AutologinCheckBox.CheckedChanged += new System.EventHandler(this.AutologinBox_CheckedChanged);
            // 
            // EmailTextBox
            // 
            this.EmailTextBox.Location = new System.Drawing.Point(110, 293);
            this.EmailTextBox.Name = "EmailTextBox";
            this.EmailTextBox.Size = new System.Drawing.Size(161, 20);
            this.EmailTextBox.TabIndex = 37;
            this.EmailTextBox.Text = "e-mail";
            this.EmailTextBox.TextChanged += new System.EventHandler(this.EmailTextBox_TextChanged);
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(277, 293);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '•';
            this.PasswordTextBox.Size = new System.Drawing.Size(90, 20);
            this.PasswordTextBox.TabIndex = 38;
            this.PasswordTextBox.Text = "password";
            this.PasswordTextBox.TextChanged += new System.EventHandler(this.PasswordTextBox_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 331);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.EmailTextBox);
            this.Controls.Add(this.AutologinCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.RegressionValue);
            this.Controls.Add(this.LinearRegression);
            this.Controls.Add(this.ProxyCount);
            this.Controls.Add(this.ChangeOnlyUp);
            this.Controls.Add(this.OrderMaxPriceNUD);
            this.Controls.Add(this.MaxOrderPriceCheckBox);
            this.Controls.Add(this.CheckOnlySellPrice);
            this.Controls.Add(this.ProxyCheck);
            this.Controls.Add(this.QuantityCheckBox);
            this.Controls.Add(this.MaxProfitNUD);
            this.Controls.Add(this.MinProfitNUD);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.StatusDot);
            this.Controls.Add(this.OrderQuantityNUD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ParsePricesButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "STH Autopricer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OrderQuantityNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinProfitNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxProfitNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderMaxPriceNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProxyCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RegressionValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker BGAutoprice;
        private System.Windows.Forms.Button ParsePricesButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown OrderQuantityNUD;
        private System.Windows.Forms.Label StatusDot;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.NumericUpDown MinProfitNUD;
        private System.Windows.Forms.NumericUpDown MaxProfitNUD;
        private System.Windows.Forms.CheckBox QuantityCheckBox;
        private System.Windows.Forms.CheckBox ProxyCheck;
        private System.Windows.Forms.CheckBox CheckOnlySellPrice;
        private System.Windows.Forms.CheckBox MaxOrderPriceCheckBox;
        private System.Windows.Forms.NumericUpDown OrderMaxPriceNUD;
        private System.Windows.Forms.CheckBox ChangeOnlyUp;
        private System.Windows.Forms.NumericUpDown ProxyCount;
        private System.Windows.Forms.CheckBox LinearRegression;
        private System.Windows.Forms.NumericUpDown RegressionValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox AutologinCheckBox;
        private System.Windows.Forms.TextBox EmailTextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
    }
}

