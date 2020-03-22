namespace BasicSynth
{
    partial class BasicSynth
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
            this.oscillator1 = new global::BasicSynth.Oscillator();
            this.SuspendLayout();
            // 
            // oscillator1
            // 
            this.oscillator1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.oscillator1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.oscillator1.Location = new System.Drawing.Point(12, 32);
            this.oscillator1.Name = "oscillator1";
            this.oscillator1.Size = new System.Drawing.Size(350, 100);
            this.oscillator1.TabIndex = 0;
            this.oscillator1.TabStop = false;
            // 
            // BasicSynthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(390, 338);
            this.Controls.Add(this.oscillator1);
            this.KeyPreview = true;
            this.Name = "BasicSynthForm";
            this.Text = "Basic Synth";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BasicSynthForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Oscillator oscillator1;
    }
}

